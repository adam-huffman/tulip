using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;

public partial class redirect : System.Web.UI.Page
{
    tulip MyTestTulip = new tulip();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        //Add the Active Directroy group needed to access the Test Environment
        MyTestTulip.ActiveDirectoryGroupsGrantAccess.Add("CN=Users_TEST,OU=SomeOU,OU=SomeOtherOU,DC=domain,DC=topleveldomain");

        // Retrive the Username from the session variable
        string str_UserName = (String)Session["User"];

        string str_Redirect_Path = "";

        // If the username is not blank
        if (str_UserName != "")
        {
            // Attempt to get Active Directory information
            try
            {               
                    //Verify user access
                    if ( MyTestTulip.HasAccess(str_UserName) )
                    {
                        string datetime_NOW = DateTime.Now.ToString();

                        //Create a new SQL connection
                        SqlConnection conn = new SqlConnection();

                        //Use the Connectionstrhing form the web.config
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["tulip"].ConnectionString;

                        //Open the connection
                        conn.Open();

                        //Insert into the table
                        SqlCommand myCommand_INSERT = new SqlCommand("INSERT INTO database_table_name VALUES (NEWID(), '" + str_UserName + "', '" + datetime_NOW + "', 'dev');", conn);

                        //Run the query
                        myCommand_INSERT.ExecuteNonQuery();

                        //Insert into the table
                        SqlCommand myCommand_SELECT = new SqlCommand("SELECT [uid] FROM database_table_name WHERE [username] = '" + str_UserName + "' AND [timestamp] = '" + datetime_NOW + "';", conn);

                        SqlDataReader reader;
                        reader = myCommand_SELECT.ExecuteReader();

                        reader.Read();

                        str_Redirect_Path = "https://subdomain.domain.topleveldomain/protected/default.asp?key=" + reader["uid"].ToString();

                        //Cloase the connection
                        conn.Close();
                    }
                    else
                    {
                        // If they do not have access then dispaly the following message
                        Session["Message"] = "Error: No Access found.";
                        str_Redirect_Path = "default.aspx";
                    }
               // }
              //  else
             //   {
                    // If they are a student send them back to the main page
                    // and dispaly the following message
                    //Session["Message"] = "Error: Student.";
                    //str_Redirect_Path = "default.aspx";
               // }

            }
            catch (Exception live_redirect_ex)
            {

                //If there is an error, most likely that the domain in unavaliable then send the user a 
                //friendly Error message but write the trace to the bottom of the page.
                if (!(live_redirect_ex == null))
                {
                    LiteralControl literalControl = new LiteralControl();
                    literalControl.ID = "divLiteralControl";
                    literalControl.Text = live_redirect_ex.ToString();
                    literalControl.Text = "<!-- Error: " + literalControl.Text.Replace("--->", "") + " --->";
                    this.Page.Controls.Add(literalControl);
                }

                Session["Message"] = "Error: Could not validate account.";
                Session["User"] = "";
                str_Redirect_Path = "default.aspx";
            }

        }
        else
        {
            // If by some reason they made it to this page without a username
            // Send them back to the main page and clear the User session
            str_Redirect_Path = "default.aspx";
            Session["User"] = "";
            Session["Message"] = "Error: Could not validate account.";
        }

        if (str_Redirect_Path.Length > 15)
        {
            // Going to leave the default application so clear session
            // and transfer
            Session.Clear();
            Response.Redirect(str_Redirect_Path);
           
        }
        else
        {
           Server.Transfer(str_Redirect_Path);
        }

    }
    
}