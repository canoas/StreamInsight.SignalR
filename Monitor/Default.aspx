<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>StreamInsight.SignalR Monitor</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="A StreamInsight Monitor using SignalR">
    <meta name="author" content="joseantonio.silva@devscope.net">

    <!-- Le styles -->
    <link href="Styles/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
        }
    </style>
    <link href="Styles/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <!-- Le fav and touch icons -->
    <link rel="shortcut icon" href="ico/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="ico/apple-touch-icon-57-precomposed.png">
    <script src="scripts/jquery.js"></script>
    <script src="Scripts/jquery.signalR.min.js" type="text/javascript"></script>
    <script src="<% = ResolveClientUrl("~/signalr/hubs") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //create arrays for use in showing formatted date string
            var days = ['Sun', 'Mon', 'Tues', 'Wed', 'Thur', 'Fri', 'Sat'];
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];

            // create proxy that uses in dynamic signalr/hubs file
            var messenger = $.connection.bizEventController;

            // Declare a function on the chat hub so the server can invoke it
            messenger.addEventMsg = function (message) {
                //format date
                var receiptDate = new Date();
                var formattedDt = days[receiptDate.getDay()] + ' ' + months[receiptDate.getMonth()] + ' ' + receiptDate.getDate() + ' ' + receiptDate.getHours() + ':' + receiptDate.getMinutes();
                //add new "message" to deck column
                $('#deck').prepend('<tr><td><span class="label">' + formattedDt + '</span></td><td>' + message + '</td></tr>');
            };

            //act on "subscribe" button
            $("#groupadd").click(function () {
                //call subscription function in server code
                messenger.addSubscription($('#group').val());
                //add entry in "subscriptions" section
                $('#subs').append($('#group').val() + '<hr width=\"50%\" align=\"left\"/>');
            });

            // Start the connection
            $.connection.hub.start();
        });
    </script>
</head>
<body>
    <div class="navbar navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                    class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                </a><a class="brand" href="#">StreamInsight.SignalR Monitor</a>
                <div class="nav-collapse">
                    <ul class="nav">
                        <li class="active"><a href="#">Home</a></li>
                        <li><a href="#about">About</a></li>
                        <li><a href="#contact">Contact</a></li>
                        <li class="dropdown" id="menu1"><a class="dropdown-toggle" data-toggle="dropdown"
                            href="#channels">Channels <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="#">All</a></li>
                                <li><a href="#">Doc Repository Events</a></li>
                                <li><a href="#">Security Events</a></li>
                                <li class="divider"></li>
                                <li><a href="#">Separated link</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span2">
                <!--Sidebar content-->
                <form id="deckForm">
                <select id="group">
                    <option>All</option>
                    <option>Doc Repository Events</option>
                    <option>Security Events</option>
                    <option>Sales Events</option>
                    <option>Industry Events</option>
                    <option>Web</option>
                </select><input type="button" id="groupadd" value="Subscribe!" />
                <span>Subscriptions</span>
                <div id="subs">
                </div>
                </form>
            </div>
            <div class="span10">
                <!--Body content-->
                <h1>
                    Feed</h1>
                
                <table class="table table-striped table-condensed">
                    <thead>
                        <tr><th>When</th><th>Alert</th>
                        </tr>
                    </thead>
                    <tbody id="deck">
                        <tr><td></td><td>------------</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <!-- /container -->
    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!-- <script src="Scripts/jquery.js" type="text/javascript"></script>-->
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <!--    <script src="js/bootstrap-transition.js"></script>
    <script src="js/bootstrap-alert.js"></script>
    <script src="js/bootstrap-modal.js"></script>
    -->
    <script src="Scripts/bootstrap-dropdown.js" type="text/javascript"></script>
    <!--    <script src="js/bootstrap-scrollspy.js"></script>
    <script src="js/bootstrap-tab.js"></script>
    <script src="js/bootstrap-tooltip.js"></script>
    <script src="js/bootstrap-popover.js"></script>
    <script src="js/bootstrap-button.js"></script>
    <script src="js/bootstrap-collapse.js"></script>
    <script src="js/bootstrap-carousel.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
-->
</body>
</html>
