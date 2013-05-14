<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" enableViewStateMac="false" %>
<!DOCTYPE html>
<html lang="en">
    <head runat="server">
        <meta charset="us-ascii">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <meta name="author" content="">
        <title>TULIP</title>
        <link rel="icon" type="image/x-icon" href="assets/img/tulip.ico"/>
        <link rel="shortcut icon" type="image/x-icon" href="assets/img/tulip.ico"/>
        <link href="assets/css/main.css" rel="stylesheet">
        <link href="assets/css/bootstrap-responsive.min.css" rel="stylesheet">
        <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
          <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->
    </head>
    <body>
        <div id="wrap">
            <div class="container">
                <form id="form1" runat="server" class="form-signin">
                    <div class="row">
                        <div class="span2">
                            <img src="assets/img/tulip_t_back.png" style="width: 100px;" />
                        </div>      
                        <div class="span5">
                            <h2 style="display: inline;">Welcome to TULIP</h2>
                            <br />
				            <h1 style="display: inline;">The UI Login Portal</h1>
                        </div>
                    </div>
				    <hr />
				    <div>
					    To prevent unauthorized access to UI please login to the Portal using your active directory username and password. 
                        If you are on campus and accessing the site from your office computer you can bypass this screen by clicking Campus Login.		
				    </div>
                    <br />
				    <div style="margin-left: auto; margin-right: auto; width: 97%; font-weight: bold; color: red;">
                        *Note: Student workers must have their supervisor login to the Portal before they can log in to UI.
				    </div>
                    <br />              
                    <asp:Panel ID="panelErrorMessage" runat="server" class="alert alert-error">
                        <asp:Label ID="lbl_LOGIN_Output" runat="server" ></asp:Label>
                    </asp:Panel>
                    <fieldset>
                        <legend>Login</legend>
                        <asp:TextBox ID="txt_LOGIN_Username" runat="server" class="input-block-level" placeholder="Username"></asp:TextBox>
                        <asp:TextBox ID="txt_LOGIN_Password" runat="server" TextMode="Password" class="input-block-level" placeholder="Password"></asp:TextBox>
                        <asp:Button ID="cmd_LOGIN" runat="server" Text="Login" OnClick="cmd_LOGIN_Click" class="btn btn-block" style="float: right; width: 51%; font-size: 16px; margin-bottom: 10px; height: 36px;" />
				        <asp:Button ID="cmd_WIN_AUTH" runat="server" Text="Campus Login" OnClick="cmd_WIN_AUTH_Click" class="btn btn-block" style="float: right; width: 51%; font-size: 16px; height: 36px;" />
                    </fieldset>                          
                    <br />
				    <div style="font-size: smaller;">
					    Before launching UI, please either disable all popup blockers or add this site as an exception to the popup 
                        blocker list in your browser. Be sure to also disable any third-party toolbars that may have additional popup
                        blockers. For more information, contact your system administrator.
				    </div>
                </form>
            </div>
            <div id="push"></div>
        </div>
        <div id="debug"></div>
        <div id="footer">
            <a href="https://github.com/adam-huffman/tulip" title="TULIP on GitHub"><img src="assets/img/tulip_t_back_small.png"/></a>
        </div>
        <script src="assets/js/jquery.js"></script>
        <script src="assets/js/bootstrap-transition.js"></script>
        <script src="assets/js/bootstrap-alert.js"></script>
        <script src="assets/js/bootstrap-modal.js"></script>
        <script src="assets/js/bootstrap-dropdown.js"></script>
        <script src="assets/js/bootstrap-scrollspy.js"></script>
        <script src="assets/js/bootstrap-tab.js"></script>
        <script src="assets/js/bootstrap-tooltip.js"></script>
        <script src="assets/js/bootstrap-popover.js"></script>
        <script src="assets/js/bootstrap-button.js"></script>
        <script src="assets/js/bootstrap-collapse.js"></script>
        <script src="assets/js/bootstrap-carousel.js"></script>
        <script src="assets/js/bootstrap-typeahead.js"></script>
    </body>
</html>