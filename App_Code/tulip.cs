using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Needed for ActiveDirectory access
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

//Needed for Page control funationailty
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Sets default vaules for variables used in the tulip project 
/// and contains helper functions that are used throughtout the project.
/// </summary>
public class tulip
{
    //Generic get with protected set so the value can only be set within the class file
    public string ActiveDirectoryRoot { get; protected set; }

    //Generic get with protected set so the value can only be set within the class file
    public string ActiveDirectoryDomain { get; protected set; }

    //Generic get with protected set so the value can only be set within the class file
    public string ActiveDirectorySearcherUserName { get; protected set; }

    //Generic get with protected set so the value can only be set within the class file
    public string ActiveDirectorySearcherPassword { get; protected set; }

    // Adding a construct for the username of the user accessing the site would remove the need for
    // us to strore that data in a session.
    // ** NOT sure this is actually going to accomplish my goal unless I can create the first tulip
    // within the GLobal.asax file at Application Start up
    public string UserName { get; set; }
     
    //Geneeric get and set.
    public List<string> ActiveDirectoryGroupsGrantAccess { get; set; }

    //Geneeric get and set.
    public List<string> ActiveDirectoryGroupsDenyAccess { get; set; }

    //Geneeric get and set.
    public List<string> ActiveDirectoryOUGrantAccess { get; set; }

    //Geneeric get and set.
    public List<string> ActiveDirectoryOUDenyAccess { get; set; }

	public tulip()
	{

        //Might be able to rewrite this to use the ActiveDirectoryDomain variable
        //Its not enough to buid a statement to extract the LDAP root from the Domain
        ActiveDirectoryRoot = "LDAP://DC=domain,DC=topleveldomain";

        ActiveDirectoryDomain = "domain.topleveldomain";

        ActiveDirectorySearcherUserName = "Active Directory Account To Perfrom Authenticated Searchers" + "@" + ActiveDirectoryDomain;

        ActiveDirectorySearcherPassword = "Password for Active Directory Search Account";

        ActiveDirectoryGroupsGrantAccess = new List<string>();
        ActiveDirectoryGroupsGrantAccess.Add("CN=GroupThatHasAccess,OU=SomeOU,OU=SomeOtherOU,DC=domain,DC=topleveldomain");

        ActiveDirectoryGroupsDenyAccess = new List<string>();
        ActiveDirectoryGroupsDenyAccess.Add("CN=GroupThatDoesNotHasAccess,OU=SomeOU,OU=SomeOtherOU,DC=domain,DC=topleveldomain");
	} 

    public bool ValidCredentials( string _username, string _password )
    {
        PrincipalContext AccountManagementPC = new PrincipalContext(ContextType.Domain, ActiveDirectoryDomain);

        return AccountManagementPC.ValidateCredentials(_username, _password);
    }

    //Creates a comment at the bottom of the page with the error infromation
    public void OutputDebugInfo(string _execption, System.Web.UI.Page _page)
    {
        if (!(_execption == null))
        {
            LiteralControl literalControl = new LiteralControl();
            literalControl.ID = "divLiteralControl";
            literalControl.Text = _execption.ToString();
            literalControl.Text = "<!-- Debug Information [ " + _page.Page.Header.Title.ToString() + " ] " + literalControl.Text.Replace("--->", "") + " --->";
            _page.Page.Controls.Add(literalControl);
        }
    }

    public bool HasAccess(string _username)
    {
        //If the user should be given access to the system
        bool Access = false;

        // If the user has any group memberships or OUs that grant access
        bool GrantAccess = false;

        // If the user has any group memberships or OUs that deny access
        bool DenyAccess = false;

        // Create an Active Directory searcher then, if possible, locate the user and obtain their
        // OU and group memberships.

        // Bind to the users container.
        // Variables set in class declaration
        DirectoryEntry entry = new DirectoryEntry(ActiveDirectoryRoot, ActiveDirectorySearcherUserName, ActiveDirectorySearcherPassword, AuthenticationTypes.Secure);
        
        // Create a DirectorySearcher object
        DirectorySearcher mySearcher = new DirectorySearcher(entry);

        // Create search filter
        // Variable set when function is called
        mySearcher.Filter = "(&(objectClass=person)(sAMAccountName=" + _username + "))";

        // Get the username from active directory
        mySearcher.PropertiesToLoad.Add("memberOf");

        // Create a SearchResultCollection object to hold a collection of 
        // SearchResults of SearchResults returned by the FindOne method.
        SearchResult result = mySearcher.FindOne();

        int intTotalNumberofGrantAccessGroups = ActiveDirectoryGroupsGrantAccess.Count();
        int intTotalNumberofGrantAccessGroupsSetTrue = 0;
        // Loop through all the group memberships
        foreach (object value in result.Properties["memberof"])
        {
            // Check to see if they are a member of a group that grants access
            if (ActiveDirectoryGroupsGrantAccess.Contains((string)value))
            {
                GrantAccess = true;
                intTotalNumberofGrantAccessGroupsSetTrue++;
            }
            
            else if (ActiveDirectoryGroupsDenyAccess.Contains((string)value))
            {
                DenyAccess = true;
            }
        }

        if (GrantAccess && intTotalNumberofGrantAccessGroups == intTotalNumberofGrantAccessGroupsSetTrue && !(DenyAccess))
        {
            Access = true;
        }
   
        return Access;
    }
}