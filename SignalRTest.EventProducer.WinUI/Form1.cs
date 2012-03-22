using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel;
using StreamInsight.Samples.Adapters.Wcf;

namespace SignalRTest.EventProducer.WinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> evtMessages = new Dictionary<string, string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            evtMessages.Add("System Requirement Spec Changed", "Doc Repository Events");
            evtMessages.Add("New Design Spec Version", "Doc Repository Events");
            evtMessages.Add("8 failed logins for user account", "Security Events");
            evtMessages.Add("User login to unexpected server", "Security Events");
            evtMessages.Add("Los Angeles customer added", "Sales Events");
            evtMessages.Add("Competitor price change", "Industry Events");

            cboEventMessages.DataSource = new BindingSource(evtMessages, null);
            cboEventMessages.DisplayMember = "Key";
            cboEventMessages.ValueMember = "Value";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = cboEventMessages.Text;
            string val = evtMessages[key];
        

            WSHttpBinding myBinding = new WSHttpBinding();
//            EndpointAddress addr = new EndpointAddress("http://localhost/StreamInsightv12/RSEROTER/InputAdapter");
            EndpointAddress addr = new EndpointAddress("http://localhost/StreamInsight/Default/InputAdapter");

            ChannelFactory<IPointInputAdapter> factory = new ChannelFactory<IPointInputAdapter>(myBinding, addr);

            IPointInputAdapter client = factory.CreateChannel();

            WcfPointEvent evt = new WcfPointEvent();
            evt.StartTime = DateTime.Now;
            evt.IsInsert = true;
            evt.Payload = new Dictionary<string, object>();
            evt.Payload.Add("Category", val);
            evt.Payload.Add("EventMessage", key);
            
            client.EnqueueEvent(evt);

            lblStatus.Text = "Status: Submitted at " + DateTime.Now.ToString();
        }

        private void btnSendWebHits_Click(object sender, EventArgs e)
        {
            WSHttpBinding myBinding = new WSHttpBinding();
//            EndpointAddress addr = new EndpointAddress("http://localhost/StreamInsightv12/RSEROTER/InputAdapter");
            EndpointAddress addr = new EndpointAddress("http://localhost/StreamInsight/Default/InputAdapter");

            ChannelFactory<IPointInputAdapter> factory = new ChannelFactory<IPointInputAdapter>(myBinding, addr);

            IPointInputAdapter client = factory.CreateChannel();

            for (int i = 0; i < Convert.ToInt32(txtWebHits.Text.ToString()); i++)
            {
                WcfPointEvent evt = new WcfPointEvent();
                evt.StartTime = DateTime.Now;
                evt.IsInsert = true;
                evt.Payload = new Dictionary<string, object>();
                evt.Payload.Add("Category", "Web");
                evt.Payload.Add("EventMessage", "Web Hit");

                client.EnqueueEvent(evt);
            }
            lblStatus.Text = "Status: Submitted at " + DateTime.Now.ToString();
        }

    }
}
