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
    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        var conn = new SqlConnection(cs);
        //always use try/catch for db connecitons
        try
        {
            //check if the uname exists, if so, check if matches the pwd
            string uname = TxtEm.Text;

            var queryCheckUname = "checkUname";
            using (conn)
            {
                using (var cmd = new SqlCommand(queryCheckUname, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Para_uname", SqlDbType.VarChar, 10).Value = uname;
                    conn.Open();
                    //Process results
                    int count = (int)cmd.ExecuteScalar();
                    if (count != 0)
                    {//uname exisits                  
                        Response.Redirect("Scheduling.aspx?user=" + uname, true);
                        conn.Close();                       
                    }
                    else //user doesn't exists, ask to enter again
                    {
                        LblMsg2.Text = "Username doesn't match with our database. Please try again.";
                    }
                }
            }
        }
        catch (Exception err)
        {

            LblMsg2.Text = "Cannot submit information now. Please try again later.";

        }
            finally //must make sure the connection is properly closed
        { //the finally block will always run whether there is an error or not
            conn.Close();
        }
    }

    protected void logout()
    {
        System.Web.HttpContext.Current.Session.Clear();
        Response.Redirect("login.aspx");
    }

}
