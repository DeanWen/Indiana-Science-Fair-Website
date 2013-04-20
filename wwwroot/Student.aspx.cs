using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
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
            getAllStudent();
        }
        
    }
/*
    protected void getStudentById(object sender, EventArgs e)
    {
        string studentId = SearchTxt.Text;
        string sql = "select * from Student where SID='" + studentId+"'";
        bind(sql);
    }*/
    protected void getStudent(object sender, EventArgs e)
    {
        string searchBy = SearchArea.SelectedValue;
        string value = SearchTxt.Text;
        if (searchBy == "SID")
        {
            string sql = "select * from Student where  SID = '" + value + "'";
            bind(sql);
        }
        else if (searchBy == "PID")
        {
            string sql = "select * from Student where PID ='" + value + "'";
            bind(sql);
        }

    }
    protected void getAllStudent()
    {
        string sql = "select * from Student";
        bind(sql);
    }

    public void bind(string sql)
    {
//        string sql = "select * from student";
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            ViewState["dspage"] = dt;
            dt.Load(reader);
            StudentGrid.DataSource = dt;
            StudentGrid.DataBind();

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
        getAllStudent();

        // rebind the control
        // in this case of retrieving the data using the xxxDataSoucr control,
        // just do nothing, because the asp.net engine binds the data automatically
    }
    


    protected void StudentGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        StudentGrid.EditIndex = -1;
        getAllStudent();
    }
    protected void StudentGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AdminMaster master = (AdminMaster)Page.Master;
        string sid = StudentGrid.DataKeys[e.RowIndex].Values["SID"].ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from student where sid='" + sid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        getAllStudent();
        master.AlertSuccess("Deleted successfully");
    }
    protected void StudentGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        StudentGrid.EditIndex = e.NewEditIndex;
        this.StudentGrid.DataSource = (DataTable)ViewState["dspage"];
        this.StudentGrid.DataBind();
        //getAllStudent();
    }
    protected void StudentGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sid = StudentGrid.DataKeys[e.RowIndex].Values["SID"].ToString();
        TextBox fn = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox1");
        TextBox mn = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox2");
        TextBox ln = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox3");
        //TextBox pid = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox4");
        DropDownList aa = (DropDownList)StudentGrid.Rows[e.RowIndex].FindControl("ddlPID");
        string pid = aa.SelectedItem.Value;
        TextBox school = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox5");
        TextBox age = (TextBox)StudentGrid.Rows[e.RowIndex].FindControl("textbox6");
        con.Open();
        SqlCommand cmd = new SqlCommand("update Student set FName='" + fn.Text.Trim() + "',MName='" + mn.Text.Trim() + "',LName='" + ln.Text.Trim() + "',pid='" + pid + "',school='" + school.Text.Trim() + "',age='" + age.Text.Trim() + "' where sid='" + sid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        StudentGrid.EditIndex = -1;
        getAllStudent();
        AdminMaster master = (AdminMaster)Page.Master;
        master.AlertSuccess("Update successfully");

    }

    private DataTable RetrievePID()
    {
        //fetch the connection string from web.config
        string connString =
                WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        //SQL statement to fetch entries from products
        string sql = @"Select PID
            from PROJECT ";
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

    protected void StudentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                DropDownList TTl = (DropDownList)e.Row.FindControl("ddlPID");
                TTl.DataTextField = "PID";
                TTl.DataValueField = "PID";
                TTl.DataSource = RetrievePID();
                TTl.DataBind();
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                TTl.SelectedValue = dr1["PID"].ToString();
            }
        }
    }
}
