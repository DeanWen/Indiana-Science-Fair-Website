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
        if (!IsPostBack)
        {
            getAllAssignment();
        }

    }

    protected void getAssignment(object sender, EventArgs e)
    {
        string searchBy = SearchArea.SelectedValue;
        string value = SearchTxt.Text;
        if (searchBy == "JID")
        {
            string sql = "select * from Assignment where  JID = '" + value+"'";
            bind(sql);
        }
        else if (searchBy == "PID")
        {
            string sql = "select * from Assignment where PID ='" + value+"'";
            bind(sql);
        }
       
    }

    protected void getAllAssignment()
    {
        string sql = "select * from Assignment";
        bind(sql);
    }

    protected void bind(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            Grid1.DataSource = dt;
            Grid1.DataBind();

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
        getAllAssignment();

        // rebind the control
        // in this case of retrieving the data using the xxxDataSoucr control,
        // just do nothing, because the asp.net engine binds the data automatically
    }

    protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Grid1.EditIndex = -1;
        getAllAssignment();
    }

    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string aid = Grid1.DataKeys[e.RowIndex].Values["AID"].ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from Assignment where aid='" + aid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        getAllAssignment();
    }

    protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Grid1.EditIndex = e.NewEditIndex;
        getAllAssignment();
    }

    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string aid = Grid1.DataKeys[e.RowIndex].Values["AID"].ToString();
        TextBox fn = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox1");
        TextBox ln = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox2");
        TextBox ca = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox3");
        TextBox cb = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox4");
        con.Open();
        SqlCommand cmd = new SqlCommand("update assignment set score='" + fn.Text + "',jid='" + ln.Text + "',pid='" + ca.Text + "',periodID='" + cb.Text + "' where aid='" + aid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        Grid1.EditIndex = -1;
        getAllAssignment();
    }
}