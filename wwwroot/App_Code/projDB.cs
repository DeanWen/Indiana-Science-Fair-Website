using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for projDB
/// </summary>
public class projDB
{
    private SqlConnection con;
    private String cs;
    private String jid;
    private List<string> judgeAvail = new List<string>();
    private List<project> Projects = new List<project>();

	public projDB()
	{
        cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (judgeAvail.Count != 0)
        {
            judgeAvail.Clear();
        }
        if (Projects.Count != 0)
        {
            Projects.Clear();
        }
	}

    public projDB(string JudgeId)
    {
        cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        con = new SqlConnection(cs);
        this.jid = JudgeId;
        if (judgeAvail.Count != 0)
        {
            judgeAvail.Clear();
        }
        if (Projects.Count != 0)
        {
            Projects.Clear();
        }
        string sql = "select PeriodId from Availability where JID = " + jid;
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string avail = (string)reader["PeriodId"];
                judgeAvail.Add(avail);
            }
            reader.Close();
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    public List<project> getAllProjByJid()
    {
        string sql = "select PID, PName, CID, P.FName, GID, P.DIVISION, Time from PROJLIST P, Judge J where J.JID=" +
            jid + "and P.DIVISION = J.DIVISION and (P.CID = J.CategoryA or P.CID = J.CategoryB or P.CID = J.CategoryC or P.CID = J.CategoryD)";
        SqlCommand cmd = new SqlCommand(sql, con);
//        List<project> Projects = new List<project>();
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // collect project's availability
                List<string> projAvail = new List<string>();
                string sql1 = "select PeriodId from ProjectAvailability where PId = '"+ (string)reader["PID"]+"'";
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    string period = (string)reader1["PeriodId"];
                    projAvail.Add(period);
                }
                reader1.Close();
                project pj = new project((string)reader["PID"], (string)MyToString(reader["PName"]), (string)MyToString(reader["CID"]), 
                    (string)MyToString(reader["FName"]), (string)MyToString(reader["GID"]), (string)MyToString(reader["DIVISION"]), (int)(reader["Time"]), projAvail);
                string sql2 = "select count(*) from Assignment where JID = " + jid + " and PID = " + (string)reader["PID"];
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                int count = (int)cmd2.ExecuteScalar();
                if(count == 0)
                {
                    Projects.Add(pj);
                }                
            }
            reader.Close();
            return Projects;
        
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    public List<project> recommendProjById()
    {
        List<project> recPoj = new List<project>();
        string sql = "select JID, CategoryA, CategoryB, CategoryC, CategoryD from Judge where JID=" + jid;
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string catA = (string)MyToString(reader["CategoryA"]);
                string catB = (string)MyToString(reader["CategoryB"]);
                string catC = (string)MyToString(reader["CategoryC"]);
                string catD = (string)MyToString(reader["CategoryD"]);

                foreach (project p in Projects)
                {
                    p.calculateWeight(catA, catB, catC, catD, judgeAvail);
                }
//                Projects.OrderByDescending(project => project.Weight);
                IEnumerable<project> proj = Projects.OrderByDescending(project => project.Weight);
                foreach (project pj in proj)
                {
                    if (pj.Weight > 0)
                    {
                        recPoj.Add(pj);
                    }
                }
/*                int count = 0;
                while (count < 10 && count < Projects.Count)  //no more than 10 projects can be recommended
                {
                    recPoj.Add(Projects[count]);
                    count++;
                }
 */
            }
            return recPoj;
        }
        catch (SqlException err)
        {
            // Replace the error with something less specific.
            // You could also log the error now.
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }
    }

    private static string MyToString(object o)
    {
        if (o == DBNull.Value || o == null)
            return "";

        return o.ToString();
    }
}