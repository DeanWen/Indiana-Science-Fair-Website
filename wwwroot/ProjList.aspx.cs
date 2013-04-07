using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Configuration;

public partial class _Default : System.Web.UI.Page
{
    static string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            bind();
        }

    }

     protected void bind()
     {
      
         string sql = "select * from PROJLIST";
         SqlCommand cmd = new SqlCommand(sql, con);
         try
         {
             con.Open();
             SqlDataReader reader = cmd.ExecuteReader();
             DataTable dt = new DataTable();
             dt.Load(reader);
             ProjListGrid.DataSource = dt;
             ProjListGrid.DataBind();

         }
         finally
         {
             con.Close();
         }

     }
     

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
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
    protected void ProjListGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ProjListGrid.EditIndex = -1;
        bind();
    }
    protected void ProjListGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string pid = ProjListGrid.DataKeys[e.RowIndex].Values["PID"].ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from Project where pid='" + pid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        bind();
    }
    protected void ProjListGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ProjListGrid.EditIndex = e.NewEditIndex;
        bind();
    }
    protected void ProjListGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string pid = ProjListGrid.DataKeys[e.RowIndex].Values["PID"].ToString();
        TextBox pn = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox1");
        TextBox cid = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox2");
        TextBox fn = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox3");
        TextBox gid = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox4");
        con.Open();
        SqlCommand cmd = new SqlCommand("update Project set PName='" + pn.Text + "',cid='" + cid.Text + "',gid='" + gid.Text + "' where pid='" + pid + "'", con);
        SqlCommand cmd1 = new SqlCommand("update student set FName='" + fn.Text + "' where pid='" + pid + "'", con);
        int result = cmd.ExecuteNonQuery();
        int result1 = cmd1.ExecuteNonQuery();
        con.Close();
        ProjListGrid.EditIndex = -1;
        bind();

    }
}
