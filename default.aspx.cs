using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default : System.Web.UI.Page
{
    //Create a tulip for this page. The hope is that the class will reduce the code reuse/copy and pasting
    tulip MyTulip = new tulip();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear contents of output
        lbl_LOGIN_Output.Text = "";

        if ((String)Session["Message"] != "")
        {
            lbl_LOGIN_Output.Text = (String)Session["Message"];
        }

        //Show Error place holder
        if (lbl_LOGIN_Output.Text != "")
        {
            panelErrorMessage.Visible = true;
        }
        else
        {
            panelErrorMessage.Visible = false;
        }

        //Clear Session data.
        Session.Clear();

        // Set the default text field
        if (txt_LOGIN_Username.Text == "")
        {
            txt_LOGIN_Username.Focus();
        }
        else
        {
            txt_LOGIN_Password.Focus();
        }

    }

    protected void cmd_WIN_AUTH_Click(object sender, EventArgs e)
    {
        //Send them to the Windows Authenticaion page
        Server.TransferRequest("windows_authentication.aspx");
    }

    protected void cmd_LOGIN_Click(object sender, EventArgs e)
    {
        //Clear Session data.
        Session.Clear();

        // Get Username and Password from the form
        string str_UserName = txt_LOGIN_Username.Text;
        string str_Password = txt_LOGIN_Password.Text;

        // Make sure that there is a username and password
        if (str_UserName == "" || str_Password == "")
        {
            string str_HTML_Break = "";

            if (str_UserName == "")
            {
                str_HTML_Break = "<br />";
                lbl_LOGIN_Output.Text = "Error: Username cannot be blank.";
            }

            if (str_Password == "")
            {
                lbl_LOGIN_Output.Text = lbl_LOGIN_Output.Text + str_HTML_Break + "Error: Password cannot be blank.";
            }
        }
        else
        {

            // If there is a username and password attempt to authenticate against Active Directory
            try
            {
                
                //This will pass if the username and password given actually acuthenticate against Activity Directory
                if ( MyTulip.ValidCredentials(str_UserName, str_Password) )
                {
                    Session["User"] = str_UserName;
                    Server.TransferRequest("redirect.aspx");
                }
                else
                {
                    lbl_LOGIN_Output.Text = "Error: Unknown username or bad password.";
                }
            }
            catch (Exception live_default_ex)
            {
                //If there is an error, most likely that the domain in unavaliable then send the user a 
                //friendly Error message but write the trace to the bottom of the page.

                MyTulip.OutputDebugInfo(live_default_ex.ToString(), this.Page);

                lbl_LOGIN_Output.Text = "Error: Unknown error.";
            }

        }

        //Show Error place holder
        if (lbl_LOGIN_Output.Text != "")
        {
            panelErrorMessage.Visible = true;
        }
        else
        {
            panelErrorMessage.Visible = false;
        }

    }

}