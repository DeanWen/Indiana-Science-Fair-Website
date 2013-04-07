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
    static string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uname"].ToString() != "10000")
        {
          Response.Write("<script>alert('No Rights,Please Contact Super Admin');window.location.href='ProjList.aspx'</script>");
        }

        if (!IsPostBack)
        {
            bind();
        }
    }

    protected void bind()
    {
        string sql = "select * from Account";
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            admin.DataSource = dt;
            admin.DataBind();

        }
        finally
        {
            con.Close();
        }
    }


    protected void BtnSubmit_Click1(object sender, EventArgs e)
    {
        if (checkMode.Checked == true)
        {
            Response.Write("<script>alert('You are in Edit Mode')</script>");
        }
        else
        {
            //always use try/catch for db connecitons
            try
            {
                //check if the email already exist, email must be unique as this is the username
                string uname = username.Text;
                string RT = right.Text;
                string mail = email.Text;

                string sql1 = "insert into account values('" + uname + "','" + RT + "','" + mail + "')";

                con.Open();

                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.ExecuteNonQuery();
                Response.Write("<script>alert('Added successful')</script>");

            }

            catch (Exception err)
            {
                //bind();
            }
        finally //must make sure the connection is properly closed
            { //the finally block will always run whether there is an error or not
                con.Close();
                bind();
            }
        }
    }


    protected void admin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        admin.EditIndex = -1;
        bind();
    }
    protected void admin_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string jid = admin.DataKeys[e.RowIndex].Values["JID"].ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from account where jid='" + jid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        bind();
    }
    protected void admin_RowEditing(object sender, GridViewEditEventArgs e)
    {
      /*  if (checkMode.Checked != true)
        {
            Response.Write("<script>alert('Please Enter Edit Mode')</script>");
        }
        else
        {
        }
       * */
            admin.EditIndex = e.NewEditIndex;
            bind();
        
        
    }
    protected void admin_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        string jid = admin.DataKeys[e.RowIndex].Values["JID"].ToString();
        TextBox pwd = (TextBox)admin.Rows[e.RowIndex].FindControl("textbox1");
        TextBox email = (TextBox)admin.Rows[e.RowIndex].FindControl("textbox2");

        con.Open();
        SqlCommand cmd = new SqlCommand("update account set pwd='" + pwd.Text + "',email='" + email.Text + "' where jid='" + jid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        admin.EditIndex = -1;
        bind();

    }



    protected void admin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;  // refer to the GridView
        int newPageIndex = 0;

        if (-2 == e.NewPageIndex)
        { // when click the "GO" Button
            TextBox txtNewPageIndex = null;
            //GridViewRow pagerRow = theGrid.Controls[0].Controls[theGrid.Controls[0].Controls.Count - 1] as GridViewRow; // refer to PagerTemplate
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
            }

            if (null != txtNewPageIndex)
            {
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
            }
        }
        else
        {  // when click the first, last, previous and next Button
            newPageIndex = e.NewPageIndex;
        }

        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

        // specify the NewPageIndex
        theGrid.PageIndex = newPageIndex;
        bind();

        // rebind the control
        // in this case of retrieving the data using the xxxDataSoucr control,
        // just do nothing, because the asp.net engine binds the data automatically
    }
    protected void checkMode_CheckedChanged(object sender, EventArgs e)
    {
        RequiredFieldValidator3.Enabled = false;
        RequiredFieldValidator2.Enabled = false;
        RequiredFieldValidator1.Enabled = false;
    }
}
