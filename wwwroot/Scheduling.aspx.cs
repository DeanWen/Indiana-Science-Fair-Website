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
    static  string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);
    protected void Page_Load(object sender, EventArgs e)
    {
        getPjByJid();
    }

    protected void submitBtnClick(object sender, EventArgs e)
    {
        getPjByJid();
    }

    /*    protected void bind()
        {
            string judgeId = JidTxt.Text;
            string sql = "select PID, PName, CID, P.FName, GID, P.DIVISION from PROJLIST P";
            //       , JUDGE J where J.JID=10000 and (P.CID = J.CategoryA or P.CID = J.CategoryB or P.CID = J.CategoryC or P.CID = J.CategoryD)  

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
    object sender, EventArgs e*/
    protected void getPjByJid()
    {
        if (!(JidTxt.Text.Trim() == ""))
        {
            string judgeId = JidTxt.Text;
            projDB newPjDb = new projDB(judgeId);
            //newPjDb.getAllProjByJid();
            //newPjDb.recommendProjById();
            ProjListGrid.DataSource = newPjDb.getAllProjByJid();
            ProjListGrid.DataBind();
            RecProjGrid.DataSource = newPjDb.recommendProjById();
            RecProjGrid.DataBind();

        }
        
/*        string sqlAll = "select PID, PName, CID, P.FName, GID, P.DIVISION from PROJLIST P, Judge J where J.JID=" +
            judgeId + "and P.DIVISION = J.DIVISION and (P.CID = J.CategoryA or P.CID = J.CategoryB or P.CID = J.CategoryC or P.CID = J.CategoryD)";

        SqlCommand cmd = new SqlCommand(sqlAll, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            ProjListGrid.DataSource = dt;
            ProjListGrid.DataBind();        
            Recommend(con, dt, judgeId);
            
        }
        finally
        {
            con.Close();
        };*/
    }
/*
    protected void Recommend(SqlConnection con, DataTable d, string jid)
    {
        string sqlRec = "select PID, PName, CID, P.FName, GID, P.DIVISION from PROJLIST P, Judge J where J.JID=" +
            jid + "and P.DIVISION = J.DIVISION and P.CID = J.CategoryA";
        SqlCommand cmd1 = new SqlCommand(sqlRec, con);
        SqlDataReader reader = cmd1.ExecuteReader();
        DataTable recTable = new DataTable();
        recTable.Load(reader);
        RecProj.DataSource = recTable;
        RecProj.DataBind();
 */
/*
        projDB projJid = new projDB();
        checkAvailability();
        sortByWeight();

    }
  */ 
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
        getPjByJid();
    }

    protected void ProjListGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
/*     
        string pid = ProjListGrid.SelectedRow.Cells[1].Text;
        string jid = JidTxt.Text;
        string sql = "insert into assignment values ('n/a','" + jid + "','" + pid + "','A')";
        try
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand(sql, con);
            cmd1.ExecuteNonQuery();
        }
        finally
        {
            Response.Write("<script>alert('Added successful!');</script>");
            con.Close();
        }
 
 */
        
    }

    protected void RecProj_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pid = RecProjGrid.SelectedRow.Cells[1].Text;
        string jid = JidTxt.Text;
        List<string> availPeriod = new List<string>();
        int count = 0;
//        string sql = "insert into assignment values ('n/a','" + jid + "','" + pid + "','A')";
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Availability.PeriodId from Availability inner join ProjectAvailability on Availability.PeriodId = ProjectAvailability.PeriodId where Availability.JID = " 
                + jid + "and ProjectAvailability.PID = " + pid, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string p = (string)reader["PeriodId"];
                availPeriod.Add(p);
                count++;
            }
            Random ran = new Random();
            int randomPeriod = ran.Next(count - 1);
            string period = availPeriod[randomPeriod];
            string sql1 = "insert into Assignment values ('n/a','" + jid + "','" + pid + "','" + period + "')";
            string sql2 = "delete from Availability where PeriodId = '" + period + "'";
            string sql3 = "delete from ProjectAvailability where PeriodId = '" + period + "'";
            string sql4= "update Project set Time = Time + 1 where PID = " + pid;
            string sql = sql1 + ";" + sql2 + ";" + sql3 + ";" + sql4;

            //            int count = (int)cmd.ExecuteScalar();
            SqlCommand cmd1 = new SqlCommand(sql, con);
            cmd1.ExecuteNonQuery();
            getPjByJid();
        }
        finally
        {
            AdminMaster master = (AdminMaster)Page.Master;
            master.AlertSuccess("Added successfully");
            con.Close();
        }
    }
}
