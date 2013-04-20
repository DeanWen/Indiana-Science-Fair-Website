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

public partial class addNewJudge : System.Web.UI.Page
{
    static string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);
    
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        AdminMaster master = (AdminMaster)Page.Master;
        //always use try/catch for db connecitons
        try
        {
            string uname = username.Text.Trim();
            string fname = firstname.Text.Trim();
            string lname = lastname.Text.Trim();
            string ca = CategoryA.SelectedItem.Value;
            string cb = CategoryB.SelectedItem.Value;
            string cc = CategoryC.SelectedItem.Value;
            string cd = CategoryD.SelectedItem.Value;
            string d = Division.SelectedItem.Value;

            var queryCheckUname = "checkJudge";
            using (con)
            {
                using (var cmd = new SqlCommand(queryCheckUname, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Para_uname", SqlDbType.VarChar, 10).Value = uname;
                    con.Open();
                    //Process results
                    int count = (int)cmd.ExecuteScalar();
                    if (count != 0)
                    {//uname exisits
                        master.AlertError("Judge already existed!");
                    }
                    else
                    {
                        string sql2 = "insert into Judge values('" + uname + "','" + fname + "','" + lname + "','" + ca + "','" + cb + "','" + cc + "','" + cd + "','" + d + "')";
                        //con.Open();
                        SqlCommand cmd2 = new SqlCommand(sql2, con);
                        cmd2.ExecuteNonQuery();
                        master.AlertSuccess("Added Succesfully");
                        ClearTextBoxes(this.Page);
                    }
                }
            }
       
        }

        catch (Exception err)
        {        
        }
        finally //must make sure the connection is properly closed
        { //the finally block will always run whether there is an error or not
            con.Close();           
            //Response.Write("<script>alert('Added Succesfully')</script>");
        }
    }

    protected void ClearTextBoxes(Control p1)
    {
        foreach (Control ctrl in p1.Controls)
        {
            if (ctrl is TextBox)
            {
                TextBox t = ctrl as TextBox;

                if (t != null)
                {
                    t.Text = String.Empty;
                }
            }
            else
            {
                if (ctrl.Controls.Count > 0)
                {
                    ClearTextBoxes(ctrl);
                }
            }
        }
    }
}
