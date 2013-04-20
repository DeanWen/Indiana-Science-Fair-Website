using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//must include this namespace
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (System.Web.HttpContext.Current.Session["uname"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }
        panelMessage.Visible = false;

        loginUser.Text ="Welcome User: "+ Session["uname"].ToString();

         string thisURL = Request.Url.Segments.Last();
         switch (thisURL) { 
             case "ProjList.aspx" :
                 P.Attributes.Add("class", "nav-top-item current");
                 dashboardName.Text = "Project List";
                 break;
             case "Student.aspx":
                 S.Attributes.Add("class", "nav-top-item current");
                 dashboardName.Text = "Student List";
                 break;
             case "Judge.aspx":
                 J.Attributes.Add("class", "nav-top-item current");
                 dashboardName.Text = "Judge List";
                 break;
             case "Assignment.aspx":
                 A.Attributes.Add("class", "nav-top-item current");
                 dashboardName.Text = "Assignment List";
                 break;
             case "Scheduling.aspx":
                 dashboardName.Text = "Scheduling Workspace";
                 break;
             case "upload.aspx":
                 dashboardName.Text = "Upload Workspace";
                 AlertWarning("Note: Duplicate/Incorrect data might upload unsuccesfully,Please CHECK spreadsheet again!");
                 break;
             case "addNewJudge.aspx":
                 dashboardName.Text = "Add New Judge";
                 break;
             case "addNewAdmin.aspx":
                 dashboardName.Text = "Admin Setting";
                 break;
             default :
                 break;                
         }
        
    }


    protected void logout()
    {
        System.Web.HttpContext.Current.Session.Clear();
        Response.Redirect("login.aspx");
    }

    public void AlertWarning(string msg)
    {
        notification.Text = msg;
        messagesText.Attributes["class"] = "notification attention png_bg";
        panelMessage.Visible = true;
    }
    public void AlertSuccess(string msg)
    {
        notification.Text = msg;
        messagesText.Attributes["class"] = "notification success png_bg";
        panelMessage.Visible = true;
    }
    public void AlertError(string msg)
    {
        notification.Text = msg;
        messagesText.Attributes["class"] = "notification error png_bg";
        panelMessage.Visible = true;
    }
}
