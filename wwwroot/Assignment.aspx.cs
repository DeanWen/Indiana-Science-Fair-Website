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
            string sql = "select * from AssignmentList where  JID = '" + value + "'";
            bind(sql);
        }
        else if (searchBy == "PID")
        {
            string sql = "select * from AssignmentList where PID ='" + value + "'";
            bind(sql);
        }
       
    }

    protected void getAllAssignment()
    {
        string sql = "select * from AssignmentList";
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
            ViewState["dspage"] = dt;
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
        this.Grid1.DataSource = (DataTable)ViewState["dspage"];
        this.Grid1.DataBind();
        //getAllAssignment();
    }

    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        AdminMaster master = (AdminMaster)Page.Master;

        string aid = Grid1.DataKeys[e.RowIndex].Values["AID"].ToString();

        TextBox jid = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox2");
        TextBox pid = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox3");
        TextBox periodid = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox4");
        con.Open();
        if (checkProject(pid.Text) != 0 && checkJudge(jid.Text) != 0)
        {
            SqlCommand cmd = new SqlCommand("update assignment set jid='" + jid.Text + "',pid='" + pid.Text + "',periodID='" + periodid.Text + "' where aid='" + aid + "'", con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            Grid1.EditIndex = -1;
            getAllAssignment();
            master.AlertSuccess("update successfully!");
        }
    }
    protected int checkJudge(string j)
    {
        AdminMaster master = (AdminMaster)Page.Master;
        //---------------------------------        
        SqlCommand cmd0 = new SqlCommand("select count(*) from Judge where  JID ='" + j + "'", con);
        int count = (int)cmd0.ExecuteScalar();
        if (count == 0)
        {//uname not exisits                
            master.AlertError("Judge doesn't exist!");
            return 0;
        }
        else
        {
            return 1;
        }

    }
    protected int checkProject(string p)
    {
        AdminMaster master = (AdminMaster)Page.Master;
        //---------------------------------        
        SqlCommand cmd0 = new SqlCommand("select count(*) from Project where  PID ='" + p + "'", con);
        int count = (int)cmd0.ExecuteScalar();
        if (count == 0)
        {//uname not exisits                
            master.AlertError("Project doesn't exist!");
            return 0;
        }
        else
        {
            return 1;
        }

    }
    protected void PrintAllPages(object sender, EventArgs e)
    {

        Grid1.AllowPaging = false;
        this.Grid1.DataSource = (DataTable)ViewState["dspage"];
        this.Grid1.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        Grid1.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        Grid1.AllowPaging = true;
        this.Grid1.DataSource = (DataTable)ViewState["dspage"];
        this.Grid1.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    public void bind2() 
    {
        Grid1.AutoGenerateDeleteButton = false;
        Grid1.AutoGenerateEditButton = false;
        Grid1.Columns[0].Visible = false;
    }


}