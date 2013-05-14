using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Make sure that no one goes to this page without comming from the main page, default.aspx

        // Make sure that the UrlReferrer is not null before using it
        if ( this.Request.UrlReferrer != null )
        {
            // If the URL contains our main application web address then we can assume that
            // we redirected the user to the page.
            if ( this.Request.UrlReferrer.ToString().Contains("https://subdomain.domain.topleveldomain") )
            {
                string user = Request.ServerVariables["LOGON_USER"];

                user = user.Remove(0, 4);

                Session["User"] = user;

                Server.TransferRequest("redirect.aspx");
            }
        }
        // If the UrlReferrer is null then the user accessed the page directly
        // and needs to be sent to the root of the application
        else
        {
            Response.Redirect("~/");
        }
    }
}