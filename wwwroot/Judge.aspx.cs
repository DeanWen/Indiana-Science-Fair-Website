/* Copyright by Indiana University Purdue University Indianapolis
 * School of Computer & Informatic Science
 * Dian Wen & Rui Wang
 * 2013 Jan-May
 */

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

public partial class Judge : System.Web.UI.Page
{
    static string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getAllJudge();
        }
    }

    // Get judge by JID
    protected void getJudgeById(object sender, EventArgs e)
    {
        string judgeId = JidTxt.Text;
        string sql = "select * from Judge where JID= '"+ judgeId+"'";
        bind(sql);
    }

    // Get all the judges
    protected void getAllJudge()
    {
        string sql = "select * from Judge";
        bind(sql);
    }

    protected void bind(string sql) {

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
        getAllJudge();

        // rebind the control
        // in this case of retrieving the data using the xxxDataSoucr control,
        // just do nothing, because the asp.net engine binds the data automatically
    }

    protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Grid1.EditIndex = -1;
        getAllJudge();
    }

    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {      
        string jid = Grid1.DataKeys[e.RowIndex].Values["JID"].ToString();
        con.Open();
        AdminMaster master = (AdminMaster)Page.Master;      
        SqlCommand cmd0 = new SqlCommand("select count(*) from Assignment where  JID ='"+jid+"'",con);
        int count = (int)cmd0.ExecuteScalar();
        if (count != 0)
        {//uname exisits                
            master.AlertError("Judge already existed in Assignment, please delete assignment first!");
        }
        else
        {
            SqlCommand cmd = new SqlCommand("delete from Judge where jid='" + jid + "'", con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            getAllJudge();
            master.AlertSuccess("Deleted successfully");
        }      
    }

    protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       Grid1.EditIndex = e.NewEditIndex;
       this.Grid1.DataSource = (DataTable)ViewState["dspage"];
       this.Grid1.DataBind();
    }

    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        AdminMaster master = (AdminMaster)Page.Master;
        string jid = Grid1.DataKeys[e.RowIndex].Values["JID"].ToString();
        TextBox fn = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox1");
        TextBox ln = (TextBox)Grid1.Rows[e.RowIndex].FindControl("textbox2");

        DropDownList DDLCA = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("CA");
        DropDownList DDLCB = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("CB");
        DropDownList DDLCC = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("CC");
        DropDownList DDLCD = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("CD");
        DropDownList DDLD = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("D");
        string ca = DDLCA.SelectedItem.Value;
        string cb = DDLCB.SelectedItem.Value;
        string cc = DDLCC.SelectedItem.Value;
        string cd = DDLCD.SelectedItem.Value;
        string d = DDLD.SelectedItem.Value;

        con.Open();
        SqlCommand cmd = new SqlCommand("update Judge set FName='" + fn.Text.Trim() + "',LName='" + ln.Text.Trim() + "',CategoryA='" + ca + "',Categoryb='" + cb+ "',CategoryC='" + cc + "', CategoryD='" + cd+ "', Division='" + d + "'  where jid='" + jid + "'", con);
        int result = cmd.ExecuteNonQuery();
        con.Close();
        Grid1.EditIndex = -1;
        getAllJudge();
        master.AlertSuccess("Update successfully");
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList[] ddlSubCategories = new DropDownList[4];
                ddlSubCategories[0] = (DropDownList)e.Row.FindControl("CA");
                ddlSubCategories[1] = (DropDownList)e.Row.FindControl("CB");
                ddlSubCategories[2] = (DropDownList)e.Row.FindControl("CC");
                ddlSubCategories[3] = (DropDownList)e.Row.FindControl("CD");

                //Bind subcategories data to dropdownlist
                string[] CategoryName = new string[4];
                CategoryName[0] = "CategoryA";
                CategoryName[1] = "CategoryB";
                CategoryName[2] = "CategoryC";
                CategoryName[3] = "CategoryD";

                DropDownList  TT = (DropDownList)e.Row.FindControl("D");
                TT.DataTextField = "Division";
                TT.DataValueField = "Division";
                TT.DataSource = RetrieveGrade();
                TT.DataBind();
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                TT.SelectedValue = dr1["Division"].ToString();

                for (int i = 0; i < CategoryName.Count(); i++)
                {
                    ddlSubCategories[i].DataTextField = "CategoryName";
                    ddlSubCategories[i].DataValueField = "CategoryName";
                    ddlSubCategories[i].DataSource = RetrieveSubCategories();
                    ddlSubCategories[i].DataBind();
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddlSubCategories[i].SelectedValue = dr[CategoryName[i]].ToString();
                }
            }
        }
    }

    private DataTable RetrieveSubCategories()
    {
        //fetch the connection string from web.config
        string connString = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        //SQL statement to fetch entries from products
        string sql = @"Select CID as CategoryName from Category";
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

    private DataTable RetrieveGrade()
    {
        //fetch the connection string from web.config
        string connString =
                WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        //SQL statement to fetch entries from products
        string sql = @"Select distinct Division 
                from Grade_Level";
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
}
