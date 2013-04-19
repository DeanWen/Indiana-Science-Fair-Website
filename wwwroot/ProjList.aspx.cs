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
            getAllProj();

        }

    }

    protected void getProjById(object sender, EventArgs e)
    {
        string projectId = SidTxt.Text;
        string sql = "select * from ProjList where PID='" + projectId + "'";
        bind(sql);
    }

    protected void getAllProj()
    {
        string sql = "select * from ProjList";
        bind(sql);
    }

    protected void bind(string sql)
    {

        //string sql = "select * from PROJLIST";
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            ViewState["dspage"] = dt;
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
        getAllProj();

        // rebind the control
        // in this case of retrieving the data using the xxxDataSoucr control,
        // just do nothing, because the asp.net engine binds the data automatically
    }
    protected void ProjListGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ProjListGrid.EditIndex = -1;
        getAllProj();
    }
    protected void ProjListGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string pid = ProjListGrid.DataKeys[e.RowIndex].Values["PID"].ToString();
        con.Open();

        AdminMaster master = (AdminMaster)Page.Master;
        //---------------------------------        
        SqlCommand cmd0 = new SqlCommand("select count(*) from Assignment where  PID ='" + pid + "'", con);
        int count = (int)cmd0.ExecuteScalar();
        if (count != 0)
        {//uname exisits                
            master.AlertError("Project already existed in Assignment, Please delete assignment first!");
        }
        else
        {
            SqlCommand cmd1 = new SqlCommand("select count(*) from student where  PID ='" + pid + "'", con);
            int count1 = (int)cmd1.ExecuteScalar();
            if (count1 != 0)
            {
                master.AlertError("Project already existed in Student, Please delete assignment first!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from Project where pid='" + pid + "'", con);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                getAllProj();
                master.AlertSuccess("Deleted successfully");
            }
        }
    }
    protected void ProjListGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ProjListGrid.EditIndex = e.NewEditIndex;
        this.ProjListGrid.DataSource = (DataTable)ViewState["dspage"];
        this.ProjListGrid.DataBind();
        //getAllProj();
    }
    protected void ProjListGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string pid = ProjListGrid.DataKeys[e.RowIndex].Values["PID"].ToString();
        TextBox pn = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox1");
        DropDownList aa = (DropDownList)ProjListGrid.Rows[e.RowIndex].FindControl("CA");
        string cid = aa.SelectedItem.Value;
        //TextBox cid = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox2");
        TextBox fn = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox3");
        TextBox gid = (TextBox)ProjListGrid.Rows[e.RowIndex].FindControl("textbox4");
        con.Open();
        SqlCommand cmd = new SqlCommand("update Project set PName='" + pn.Text + "',cid='" + cid + "',gid='" + gid.Text + "' where pid='" + pid + "'", con);
        SqlCommand cmd1 = new SqlCommand("update student set FName='" + fn.Text + "' where pid='" + pid + "'", con);
        int result = cmd.ExecuteNonQuery();
        int result1 = cmd1.ExecuteNonQuery();
        con.Close();
        ProjListGrid.EditIndex = -1;
        getAllProj();
        AdminMaster master = (AdminMaster)Page.Master;
        master.AlertSuccess("Update successfully");

    }
   
    private DataTable RetrieveSubCategories()
    {
        //fetch the connection string from web.config
        string connString =
                WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        //SQL statement to fetch entries from products
        string sql = @"Select CID as CategoryName
                from Category";
        DataTable dtSubCategories = new DataTable();
        //Open SQL Connection
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            //Initialize command object
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Fill the result set
                adapter.Fill(dtSubCategories);
            }
        }
        return dtSubCategories;
    }


    protected void ProjListGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                DropDownList ddlSubCategories =
                          (DropDownList)e.Row.FindControl("CA");
                //Bind subcategories data to dropdownlist
                ddlSubCategories.DataTextField = "CategoryName";
                ddlSubCategories.DataValueField = "CategoryName";
                ddlSubCategories.DataSource = RetrieveSubCategories();
                ddlSubCategories.DataBind();
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddlSubCategories.SelectedValue =
                             dr["cid"].ToString();
            }
        }
        
    }
}