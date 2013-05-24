/* Copyright by Indiana University Purdue University Indianapolis
 * School of Computer & Informatic Science
 * Dian Wen & Rui Wang
 * 2013 Jan-May
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Functions on Projects
/// </summary>
public class projDB
{
    private SqlConnection con;
    private String cs;
    private String jid;
    private List<string> judgeAvail = new List<string>();  //Judge's availability
    private List<project> Projects = new List<project>();  //Projects' list

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

    //Initialize a project list for a particular judge
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
        //Find judge's availability
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

    //Return a list of all the projects which match the division and all the category for a judge
    public List<project> getAllProjByJid()
    {
        string sql = "select PID, PName, CID, P.FName, GID, P.DIVISION, Time from PROJLIST P, Judge J where J.JID=" +
            jid + "and P.DIVISION = J.DIVISION and (P.CID = J.CategoryA or P.CID = J.CategoryB or P.CID = J.CategoryC or P.CID = J.CategoryD)";
        SqlCommand cmd = new SqlCommand(sql, con);
        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Collect a project's availability
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
                // Create the project
                project pj = new project((string)reader["PID"], (string)MyToString(reader["PName"]), (string)MyToString(reader["CID"]), 
                    (string)MyToString(reader["FName"]), (string)MyToString(reader["GID"]), (string)MyToString(reader["DIVISION"]), (int)(reader["Time"]), projAvail);
                string sql2 = "select count(*) from Assignment where JID = " + jid + " and PID = " + (string)reader["PID"];
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                int count = (int)cmd2.ExecuteScalar();
                if(count == 0) // The project hasn't been assigned to the judge 
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

    // Recommend projects by calculating the weight
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
                IEnumerable<project> proj = Projects.OrderByDescending(project => project.Weight);
                foreach (project pj in proj)
                {
                    if (pj.Weight > 0)
                    {
                        recPoj.Add(pj);
                    }
                }
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
    
    // Return a string of an object
    private static string MyToString(object o)
    {
        if (o == DBNull.Value || o == null)
            return "";

        return o.ToString();
    }
}