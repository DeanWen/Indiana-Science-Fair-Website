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

public partial class _Default : System.Web.UI.Page
{
    static  string cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
    SqlConnection con = new SqlConnection(cs);
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void submitBtnClick(object sender, EventArgs e)
    {
        if (checkJudge(JidTxt.Text) != 0)
            getPjByJid();
    }

    // Check if the judge exists
    protected int checkJudge(string j)
    {
        AdminMaster master = (AdminMaster)Page.Master;
	    try
        {
            con.Open();       
            SqlCommand cmd0 = new SqlCommand("select count(*) from Judge where  JID ='" + j + "'", con);
            int count = (int)cmd0.ExecuteScalar();
            if (count == 0)  //uname not exisits  
            {                              
                master.AlertError("Judge doesn't exist!");
                return 0;
            }
            else
            {
                return 1;
            }
        }
        finally
        {
            con.Close();
        }
    }

    // Get projects by JID
    protected void getPjByJid()
    {
        string judgeId = JidTxt.Text;
        projDB newPjDb = new projDB(judgeId);
        List<project> pj = newPjDb.getAllProjByJid();
        if (pj.Count == 0)
        {
            AdminMaster master = (AdminMaster)Page.Master;
            master.AlertWarning("No Project Matched");
        }
        ProjListGrid.DataSource = pj;
        ProjListGrid.DataBind();
        RecProjGrid.DataSource = newPjDb.recommendProjById();
        RecProjGrid.DataBind();
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
        getPjByJid();
    }

    protected void RecProj_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pid = RecProjGrid.SelectedRow.Cells[1].Text;
        string jid = JidTxt.Text;
        List<string> availPeriod = new List<string>();
        int count = 0;
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
            string sql2 = "delete from Availability where PeriodId = '" + period + "' and JID = '" + jid + "'";
            string sql3 = "delete from ProjectAvailability where PeriodId = '" + period + "'and PID = '" + pid + "'";
            string sql4= "update Project set Time = Time + 1 where PID = " + pid;
            string sql = sql1 + ";" + sql2 + ";" + sql3 + ";" + sql4;

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
