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
        if (!IsPostBack)
        {
           // bind();
        }

    }
    protected void bind()
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

    protected void getPjByJid(object sender, EventArgs e)
    {

        string judgeId = JidTxt.Text;
        string sqlAll = "select PID, PName, CID, P.FName, GID, P.DIVISION from PROJLIST P, Judge J where J.JID=" +
            judgeId + "and P.DIVISION = J.DIVISION and (P.CID = J.CategoryA or P.CID = J.CategoryB or P.CID = J.CategoryC or P.CID = J.CategoryD)";
 /*       string sqlRec = "select PID, PName, CID, P.FName, GID, P.DIVISION from PROJLIST P, Judge J, Assignment A  where J.JID=" +
            judgeId + "and P.CID = J.CategoryA";*/
        SqlCommand cmd = new SqlCommand(sqlAll, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            ProjListGrid.DataSource = dt;
            ProjListGrid.DataBind();

  //        dt.Select("PID=10000");          
            Recommend(con, dt, judgeId);
        }
        finally
        {
            con.Close();
        }
    }

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
 //       recTable = d.Select();
 //       d.Select("CID = J.CategoryA");
    }
  
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }
    protected void ProjListGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
     
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
        
        
    }
    protected void RecProj_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pid = RecProj.SelectedRow.Cells[1].Text;
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
    }
}
