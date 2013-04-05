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

public partial class newAdmin : System.Web.UI.Page
{

    protected void BtnSubmit_Click1(object sender, EventArgs e)
    {
        string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        //always use try/catch for db connecitons
        try
        {
            //check if the email already exist, email must be unique as this is the username
            string uname = username.Text;
            string RT = right.Text;

            string sql1 = "insert into account values('"+ uname+"','"+RT+"')";

            con.Open();

            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.ExecuteNonQuery();


         
        }

        catch (Exception err)
        {
           
        }
        finally //must make sure the connection is properly closed
        { //the finally block will always run whether there is an error or not
            con.Close();
        }
    }
}
