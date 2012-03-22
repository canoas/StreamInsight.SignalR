namespace SignalRTest.EventProducer.WinUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSendSingleEvent = new System.Windows.Forms.Button();
            this.txtWebHits = new System.Windows.Forms.TextBox();
            this.btnSendWebHits = new System.Windows.Forms.Button();
            this.cboEventMessages = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSendSingleEvent
            // 
            this.btnSendSingleEvent.Location = new System.Drawing.Point(277, 28);
            this.btnSendSingleEvent.Name = "btnSendSingleEvent";
            this.btnSendSingleEvent.Size = new System.Drawing.Size(182, 23);
            this.btnSendSingleEvent.TabIndex = 0;
            this.btnSendSingleEvent.Text = "Send Single Event";
            this.btnSendSingleEvent.UseVisualStyleBackColor = true;
            this.btnSendSingleEvent.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWebHits
            // 
            this.txtWebHits.Location = new System.Drawing.Point(12, 83);
            this.txtWebHits.Name = "txtWebHits";
            this.txtWebHits.Size = new System.Drawing.Size(100, 22);
            this.txtWebHits.TabIndex = 2;
            // 
            // btnSendWebHits
            // 
            this.btnSendWebHits.Location = new System.Drawing.Point(277, 83);
            this.btnSendWebHits.Name = "btnSendWebHits";
            this.btnSendWebHits.Size = new System.Drawing.Size(182, 23);
            this.btnSendWebHits.TabIndex = 4;
            this.btnSendWebHits.Text = "Send Web Hits";
            this.btnSendWebHits.UseVisualStyleBackColor = true;
            this.btnSendWebHits.Click += new System.EventHandler(this.btnSendWebHits_Click);
            // 
            // cboEventMessages
            // 
            this.cboEventMessages.FormattingEnabled = true;
            this.cboEventMessages.Location = new System.Drawing.Point(12, 28);
            this.cboEventMessages.Name = "cboEventMessages";
            this.cboEventMessages.Size = new System.Drawing.Size(240, 24);
            this.cboEventMessages.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 229);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 255);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboEventMessages);
            this.Controls.Add(this.btnSendWebHits);
            this.Controls.Add(this.txtWebHits);
            this.Controls.Add(this.btnSendSingleEvent);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendSingleEvent;
        private System.Windows.Forms.TextBox txtWebHits;
        private System.Windows.Forms.Button btnSendWebHits;
        private System.Windows.Forms.ComboBox cboEventMessages;
        private System.Windows.Forms.Label lblStatus;
    }
}

