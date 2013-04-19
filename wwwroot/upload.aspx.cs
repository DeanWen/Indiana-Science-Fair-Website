using System;  
using System.Data;  
using System.Data.OleDb;  
using System.Data.SqlClient;  
using System.IO;  
using System.Text;  
using System.Web;  
using System.Web.UI; 
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class upload : System.Web.UI.Page
{
    static string strcon = "server=edutechservice.com;database=aa_isf_db_dev;uid=isf_db_uname_685219;pwd=isf_db_pwd_685219!";
    SqlConnection conn = new SqlConnection(strcon);//链接数据库

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private DataTable  xsldata(FileUpload fuload,Label lbmsg)
    {
        if(fuload.FileName == "")
        {
            lbmsg.Text = "Please Choose Files";
            return null;
        }
        string fileExtenSion;
        fileExtenSion = Path.GetExtension(fuload.FileName);
        if(fileExtenSion.ToLower() != ".xls" && fileExtenSion.ToLower() != ".xlsx")
        {
            lbmsg.Text = "Invalid Format";
            return null;
        }
        try
        {
            string FileName = "updata/" + Path.GetFileName(fuload.FileName);
            if(File.Exists(Server.MapPath(FileName)))
            {
                File.Delete(Server.MapPath(FileName));
            }
            fuload.SaveAs(Server.MapPath(FileName));
            //HDR=Yes，这代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。系统默认的是YES
            string connstr2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(FileName) + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            string connstr2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(FileName) + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
            OleDbConnection conn;
            if(fileExtenSion.ToLower() == ".xls")
            {
                conn = new OleDbConnection(connstr2003);
            }
            else
            {
                conn = new OleDbConnection(connstr2007);
            }
            conn.Open();
            string sql = "select * from [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            DataTable dt = new DataTable();
            OleDbDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);
            sdr.Close();
            conn.Close();

            //删除服务器里上传的文件                      
            if(File.Exists(Server.MapPath(FileName)))
                {
                    File.Delete(Server.MapPath(FileName));
                }
       
            return dt;
        }
        catch(Exception e)
        {
            return null;
        } 

    }


    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        FileUpload pass = null;
        Label show = null;

        if (sender == Projbtn)
        {
            pass = ProjUpLoad;
            show = ProjStatus;
        }
        else if (sender == Stubtn)
        {
            pass = StuUpLoad;
            show = StuStatus;
        }
        else if (sender == Judgebtn)
        {
            pass = JudgeUpLoad;
            show = JudgeStatus;
        }

        try
        {
            DataTable bu = xsldata(pass, show);
            conn.Open();

            for (int i = 0; i < bu.Rows.Count; i++)
            {

                if (sender == Projbtn)
                {
                    string PID = bu.Rows[i]["PID"].ToString();//dt.Rows[i]["Name"].ToString(); "Name"即为Excel中Name列的表
                    string CID = bu.Rows[i]["CID"].ToString();
                    string GID = bu.Rows[i]["GID"].ToString();
                    string PNAME = bu.Rows[i]["PNAME"].ToString();
                    string sql = "insert into Project values('" + PID + "','" + CID + "','" + GID + "','" + PNAME + "')";
                    SqlCommand comd = new SqlCommand(sql, conn);
                    comd.ExecuteNonQuery();
                    ProjStatus.Text = "successful";
                }
                else if (sender == Stubtn)
                {
                    string SID = bu.Rows[i]["SID"].ToString();//dt.Rows[i]["Name"].ToString(); "Name"即为Excel中Name列的表
                    string FNAME = bu.Rows[i]["FNAME"].ToString();
                    string MNAME = bu.Rows[i]["MNAME"].ToString();
                    string LNAME = bu.Rows[i]["LNAME"].ToString();
                    string PID = bu.Rows[i]["PID"].ToString();
                    string School = bu.Rows[i]["SCHOOL"].ToString();
                    string Age = bu.Rows[i]["AGE"].ToString();
                    string sql = "insert into Student values('" + SID + "','" + FNAME + "','" + MNAME + "','" + LNAME + "','" + PID + "','" + School + "','" + Age + "')";
                    SqlCommand comd = new SqlCommand(sql, conn);
                    comd.ExecuteNonQuery();
                    StuStatus.Text = "successful";

                }
                else if (sender == Judgebtn)
                {
                    string jid = bu.Rows[i]["JID"].ToString();//dt.Rows[i]["Name"].ToString(); "Name"即为Excel中Name列的表
                    string fn = bu.Rows[i]["FNAME"].ToString();
                    string ln = bu.Rows[i]["LNAME"].ToString();
                    string ca = bu.Rows[i]["CA"].ToString();
                    string cb = bu.Rows[i]["CB"].ToString();
                    string cc = bu.Rows[i]["CC"].ToString();
                    string cd = bu.Rows[i]["CD"].ToString();
                    string d = bu.Rows[i]["DIVISION"].ToString();
                    string sql = "insert into judge values('" + jid + "','" + fn + "','" + ln + "','" + ca + "','" + cb + "','" + cc + "','" + cd + "','" + d + "')";
                    SqlCommand comd = new SqlCommand(sql, conn);
                    comd.ExecuteNonQuery();
                    JudgeStatus.Text = "successful";
                }
            }
        }
        catch (Exception err)
        {
            //LblMsg.Text = "Cannot submit information now. Please try again later.";
        }
        finally
        {
            
            conn.Close();
        }

    }
}


            