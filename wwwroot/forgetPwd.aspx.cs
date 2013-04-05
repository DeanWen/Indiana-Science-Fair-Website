using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//must include this namespace
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;

public partial class forgetPwd :System.Web.UI.Page
{
   
    string cs;
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        cs = WebConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
        con = new SqlConnection(cs);
       
    }
    
    protected void BtnEnter_Click(object sender, EventArgs e)
    {
        string adress = TxtEm.Text;
          MailMessage mail =  new MailMessage() ;
          mail.To.Add(adress);
          //mail.From = new MailAddress("admin@gmail.com");
          mail.Subject = "Forget password";

          mail.Body = "Sending email from ISF";
 
          mail.IsBodyHtml = true;
         SmtpClient smtp =  new SmtpClient() ;
          //smtp.Host = "smtp.gmail.com" ;

          smtp.EnableSsl = false;
          smtp.Port = 26;
          smtp.EnableSsl = false;
          smtp.Send(mail);
         
        //get the user name and see if it exists in the database
     
        //always use try/catch for db connections
/*        try
        {
            //check if the email already exist, email must be unique as this is the username
            string tmpName = TxtEm.Text.Trim();
            string sql = "select count(*) from customer where email = '" + tmpName + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            if (count != 0)
            {
                //email exists, go to the next page
                Response.Redirect("forgetPwd2.aspx?em=" + tmpName, true);
           
               
               
            }
            else {
                LblMsg1.Text = "Username incorrect. Please try again.";
               
                con.Close(); 
            
            }

        }
        catch (Exception err)
        {

            LblMsg1.Text = "Cannot submit information now. Please try again later.";

        }
        finally //must make sure the connection is properly closed
        { //the finally block will always run whether there is an error or not
            con.Close();
        }
        */
        LblMsg1.Text = "An Email has been sent to " + TxtEm.Text;
    }

  
    

}
