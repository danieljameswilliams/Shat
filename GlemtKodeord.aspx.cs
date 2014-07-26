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

using MySql.Data.MySqlClient;
using System.Net.Mail;

public partial class GlemtKodeord : System.Web.UI.Page
{
    protected void SendMeThePassword_Click(object sender, EventArgs e)
    {
        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();

            string sql = "SELECT fldPassword, fldMail, fldName, fldSirName, fldUsername FROM userinfo WHERE fldMail=?Mailen";
            MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            cmd.Parameters.Add("?Mailen", txtGlemtKodeord.Text);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            object result = reader;
            if (result == null)
            {
                Status.Text = "Der findes ingen brugere med den E-Mail";
            }
            else
            {
                //try
                //{                                          
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("mail@danielwilliams.dk");
                    message.To.Add(new MailAddress(txtGlemtKodeord.Text));
                    message.Subject = "Dit Shat kodeord";
                 
                    while (reader.Read())
                    {
                    //message.Body = "Hej " + reader.GetString(reader.GetOrdinal("fldName")) + "<br /><br />Du har efterspurgt dit kodeord til din profil på Shat ved navn '" + reader.GetString(reader.GetOrdinal("fldUsername")) + "'<br /><br />Dit kodeord til denne bruger er: '" + GetRandomPassword(10) + "'";


                        message.IsBodyHtml = true;
                        message.Body += "<html>";
                        message.Body += "<body>";

                        message.Body += "<style type='text/css'>";
                        message.Body += "<!--";
                        message.Body += ".style1 {";
                        message.Body += "font-size: 24px;";
                        message.Body += "font-family: Arial, Helvetica, sans-serif;";
                        message.Body += "font-weight: bold;";
                        message.Body += "}";
                        message.Body += ".style3 {font-family: Arial, Helvetica, sans-serif; font-size: 16px;}";
                        message.Body += ".style4 {";
                        message.Body += "font-family: Arial, Helvetica, sans-serif;";
                        message.Body += "font-size: 14px;";
                        message.Body += "color: #999999;";
                        message.Body += "}";
                        message.Body += "-->";
                        message.Body += "</style>";

                        message.Body += "<table width='700' height='423' border='0' cellpadding='0' cellspacing='0'>";
                        message.Body += "<tr>";
                        message.Body += "<th width='137' height='133' scope='col'><img src='http://www.danielwilliams.dk/ShatMail/TopLeft.jpg' width='94' height='215' /></th>";
                        message.Body += "<th width='326' scope='col'><img src='http://www.danielwilliams.dk/ShatMail/TopMiddle.jpg' width='535' height='215' /></th>";
                        message.Body += "<th width='215' scope='col'><img src='http://www.danielwilliams.dk/ShatMail/TopRight.jpg' width='128' height='215' /></th>";
                        message.Body += "</tr>";
                        message.Body += "<tr>";
                        message.Body += "<th height='152' scope='row'><img src='http://www.danielwilliams.dk/ShatMail/MiddleLeft.jpg' width='94' height='152' /></th>";
                        message.Body += "<td height='152'><p class='style1'>Hej " + reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName")) + " </p>";
                        message.Body += "<p class='style3'>Du har efterspurgt et nyt kodeord, fordi du ikke længere kan huske dit gamle. Derfor har vi lavet et nyt til dig, som du til enhver tid kan lave om igen.</p>";
                        message.Body += "<p class='style4'>Dit nye kodeord: " + GetRandomPassword(10) + "</p></td>";
                        message.Body += "<td height='152'><img src='http://www.danielwilliams.dk/ShatMail/MiddleRight.jpg' width='128' height='152' /></td>";
                        message.Body += "</tr>";
                        message.Body += "<tr>";
                        message.Body += "<th height='137' scope='row'><img src='http://www.danielwilliams.dk/ShatMail/BottomLeft.jpg' width='94' height='137' /></th>";
                        message.Body += "<td height='137'><img src='http://www.danielwilliams.dk/ShatMail/BottomMiddle.jpg' width='535' height='137' /></td>";
                        message.Body += "<td height='137'><img src='http://www.danielwilliams.dk/ShatMail/BottomRight.jpg' width='128' height='137' /></td>";
                        message.Body += " </tr>";
                        message.Body += "</table>";
                        message.Body += "</body>";
                        message.Body += "</html>";
                    
                    }
                    dbConn.Close();

                    dbConn.Open();
                    string sql2 = "UPDATE userinfo Set fldPassword='" + GetRandomPassword(10) + "', fldRecoveredPassword=1 WHERE fldMail=?Mailen";
                    MySqlCommand cmd3 = new MySqlCommand(sql2, dbConn);
                    cmd3.Parameters.Add("?Mailen", txtGlemtKodeord.Text);
                    cmd3.ExecuteNonQuery();
                    dbConn.Close();


                    SmtpClient client = new SmtpClient("mail.danielwilliams.dk");
                    client.Send(message);

                    Response.Redirect("/Besked.aspx?type=glemtkode");
                //}
                //catch(Exception ex)
                //{
                //    Status.Text = "Der er sket en fejl med at sende mailen, prøv igen senere<br />eller kontakt en administrator for Shat";
                //}

            }
        }
    }

    public static string GetRandomPassword(int iLength)
    {
        if (iLength < 1) iLength = 1;
        if (iLength > 40) iLength = 40;
        String sSeed = Guid.NewGuid().ToString().Replace("-", "");
        return sSeed.Substring(0, iLength);
    } 
}
