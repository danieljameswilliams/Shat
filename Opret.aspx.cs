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

using System.Text;
using AjaxPro;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Net.Mail;

public partial class Opret : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewOpret1.Visible = true;
        ViewOpret2.Visible = false;
    }

    protected void lbNextStep_Click(object sender, EventArgs e)
    {
        ViewState.Add("password", txtPassword.Text);
        txtPassword.Attributes.Add("value", txtPassword.Text);

        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();
            string sqluser = "SELECT fldUsername FROM userinfo WHERE fldUsername=?Username LIMIT 1";
            MySqlCommand cmduser = new MySqlCommand(sqluser, dbConn);
            cmduser.Parameters.Add("?Username", txtUsername.Text);

            string sqlmail = "SELECT fldMail FROM userinfo WHERE fldMail=?Mail LIMIT 1";
            MySqlCommand cmdmail = new MySqlCommand(sqlmail, dbConn);
            cmdmail.Parameters.Add("?Mail", txtEmail.Text);

            bool userAlreadyExist = (cmduser.ExecuteScalar() != null);
            bool mailAlreadyExist = (cmdmail.ExecuteScalar() != null);

            if (userAlreadyExist || mailAlreadyExist)
            {
                if (userAlreadyExist)
                {
                    Response.Write("Der findes allerede en " + txtUsername.Text + "");
                }
                else
                {
                    Response.Write("Der findes allerede en med emailen: " + txtEmail.Text + "<br />Har du glemt dit kodeord? - Så klik <a href='GlemtKodeord.aspx'>her</a>");
                }
            }
            else
            {
                ViewOpret1.Visible = false;
                ViewOpret2.Visible = true;
            }
            dbConn.Close();
        }
    }

    protected void lbFinish_Click(object sender, EventArgs e)
    {
        DateTime outDate = new DateTime();
        bool parsed = DateTime.TryParse("" + txtDay.Text + "-" + txtMonth.Text + "-" + txtYear.Text + "", out outDate);

        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();

            if (parsed == true && ddlSex.SelectedValue != "3" && ddlCivilstatus.SelectedValue != "0")
            {
                // Opretter Brugeren til databasen
                string sql = "INSERT INTO userinfo (fldUsername, fldPassword, fldName, fldSirName, fldSex, fldBirthday, fldMail, fldAvatar, fldFacebook, fldStatus, fldCreated, fldActivated) VALUES (?Username,?Password,?Name,?SirName, ?Sex, ?Birthday, ?Mail, ?Avatar, ?Facebook, ?Status, ?Created, '" + GetRandomCharacters(10) + "')";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.Parameters.Add("?Username", txtUsername.Text);
                cmd.Parameters.Add("?Password", ViewState["password"]);
                cmd.Parameters.Add("?Name", txtName.Text);
                cmd.Parameters.Add("?SirName", txtSirName.Text);
                cmd.Parameters.Add("?Sex", ddlSex.SelectedValue);
                cmd.Parameters.Add("?Birthday", outDate);
                cmd.Parameters.Add("?Mail", txtEmail.Text);
                cmd.Parameters.Add("?Status", ddlCivilstatus.SelectedValue);
                cmd.Parameters.Add("?Avatar", "hilda.jpg");
                cmd.Parameters.Add("?Facebook", "http://www.facebook.com");
                cmd.Parameters.Add("?Created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();

                MailMessage message = new MailMessage();
                message.From = new MailAddress("mail@danielwilliams.dk");
                message.To.Add(new MailAddress(txtEmail.Text));
                message.IsBodyHtml = true;
                message.Subject = "Aktivering af Profil til www.Shat.dk";

                string sql2 = "SELECT fldActivated, fldMail, fldName, fldSirName, fldUsername FROM userinfo WHERE fldMail=?Mailen";
                MySqlCommand cmd2 = new MySqlCommand(sql2, dbConn);
                cmd2.Parameters.Add("?Mailen", txtEmail.Text);
                MySqlDataReader reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
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
                    message.Body += "<th width='137' height='133' scope='col'><img src='TopLeft.jpg' width='94' height='215' /></th>";
                    message.Body += "<th width='326' scope='col'><img src='TopMiddle.jpg' width='535' height='215' /></th>";
                    message.Body += "<th width='215' scope='col'><img src='TopRight.jpg' width='128' height='215' /></th>";
                    message.Body += "</tr>";
                    message.Body += "<tr>";
                    message.Body += "<th scope='row'><img src='MiddleLeft.jpg' width='94' height='152' /></th>";
                    message.Body += "<td><p class='style1'>Hej " + reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName")) + " </p>";
                    message.Body += "<p class='style3'>Din profil er næsten oprettet, det eneste du mangler nu, er at aktivere din profil. Dette kan du gøre ved at klikke på linket nedenunder!</p>";
                    message.Body += "<p class='style4'>http://79.170.40.236/shat.dk/Aktivering.aspx?t=Opret+p=" + reader.GetString(reader.GetOrdinal("fldUsername")) + "+v=" + reader.GetString(reader.GetOrdinal("fldActivated")) + "</p></td>";
                    message.Body += "<td><img src='MiddleRight.jpg' width='128' height='152' /></td>";
                    message.Body += "</tr>";
                    message.Body += "<tr>";
                    message.Body += "<th scope='row'><img src='BottomLeft.jpg' width='94' height='137' /></th>";
                    message.Body += "<td><img src='BottomMiddle.jpg' width='535' height='137' /></td>";
                    message.Body += "<td><img src='BottomRight.jpg' width='128' height='137' /></td>";
                    message.Body += " </tr>";
                    message.Body += "</table>";
                    message.Body += "</body>";
                    message.Body += "</html>";

                    //Response.Write("Hej " + OprettetMail & reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName")) + "<br /><br />Din bruger er nu blevet oprettet, og du skal nu bare åbne dette link for at aktivere din profil: http://79.170.40.236/shat.dk/Aktivering.aspx?t=Opret&p=" + reader.GetString(reader.GetOrdinal("fldUsername")) + "&v=" + reader.GetString(reader.GetOrdinal("fldActivated")) + "<br /><br />");
                }
                SmtpClient client = new SmtpClient("mail.danielwilliams.dk");
                client.Send(message);
            }
            else
            {
                if (parsed == false)
                { 
                    ViewOpret1.Visible = false;
                    ViewOpret2.Visible = true;
                    Response.Write("Din Fødselsdato er ikke korrekt!");

                }
                else if (ddlSex.SelectedValue == "3")
                {
                    ViewOpret1.Visible = false;
                    ViewOpret2.Visible = true;
                    Response.Write("For at systemet kan køre korrekt, skal vi bruge dit <b>Køn</b>");

                }
                else if (ddlCivilstatus.SelectedValue == "0") 
                {
                    ViewOpret1.Visible = false;
                    ViewOpret2.Visible = true;
                    Response.Write("For at systemet kan køre korrekt, skal du angive din <b>Civilstatus</b>");
                }
                
            }
            dbConn.Close();
            //Response.Redirect("Besked.aspx?type=profil");
        }
    }

    public static string GetRandomCharacters(int iLength)
    {
        if (iLength < 1) iLength = 1;
        if (iLength > 40) iLength = 40;
        String sSeed = Guid.NewGuid().ToString().Replace("-", "");
        return sSeed.Substring(0, iLength);
    }

    //public string OprettetMail
    //{
    //    string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
    //    using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
    //    {
    //        dbConn.Open();

    //        string sql12 = "SELECT fldActivated, fldMail, fldName, fldSirName, fldUsername FROM userinfo WHERE fldMail=?Mailen";
    //        MySqlCommand cmd12 = new MySqlCommand(sql12, dbConn);
    //        cmd12.Parameters.Add("?Mailen", txtEmail.Text);
    //        MySqlDataReader reader = cmd12.ExecuteReader();

    //        while (reader.Read())
    //        {
    //            OprettetMail &= "<html>";
    //            OprettetMail &= "<body>";

    //            OprettetMail &= "<style type='text/css'>";
    //            OprettetMail &= "<!--";
    //            OprettetMail &= ".style1 {";
    //            OprettetMail &= "font-size: 24px;";
    //            OprettetMail &= "font-family: Arial, Helvetica, sans-serif;";
    //            OprettetMail &= "font-weight: bold;";
    //            OprettetMail &= "}";
    //            OprettetMail &= ".style3 {font-family: Arial, Helvetica, sans-serif; font-size: 16px;}";
    //            OprettetMail &= ".style4 {";
    //            OprettetMail &= "font-family: Arial, Helvetica, sans-serif;";
    //            OprettetMail &= "font-size: 14px;";
    //            OprettetMail &= "color: #999999;";
    //            OprettetMail &= "}";
    //            OprettetMail &= "-->";
    //            OprettetMail &= "</style>";

    //            OprettetMail &= "<table width='700' height='423' border='0' cellpadding='0' cellspacing='0'>";
    //            OprettetMail &= "<tr>";
    //            OprettetMail &= "<th width='137' height='133' scope='col'><img src='TopLeft.jpg' width='94' height='215' /></th>";
    //            OprettetMail &= "<th width='326' scope='col'><img src='TopMiddle.jpg' width='535' height='215' /></th>";
    //            OprettetMail &= "<th width='215' scope='col'><img src='TopRight.jpg' width='128' height='215' /></th>";
    //            OprettetMail &= "</tr>";
    //            OprettetMail &= "<tr>";
    //            OprettetMail &= "<th scope='row'><img src='MiddleLeft.jpg' width='94' height='152' /></th>";
    //            OprettetMail &= "<td><p class='style1'>Hej " + reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName")) + " </p>";
    //            OprettetMail &= "<p class='style3'>Din profil er næsten oprettet, det eneste du mangler nu, er at aktivere din profil. Dette kan du gøre ved at klikke på linket nedenunder!</p>";
    //            OprettetMail &= "<p class='style4'>http://79.170.40.236/shat.dk/Aktivering.aspx?t=Opret&p=" + reader.GetString(reader.GetOrdinal("fldUsername")) + "&v=" + reader.GetString(reader.GetOrdinal("fldActivated")) + "</p></td>";
    //            OprettetMail &= "<td><img src='MiddleRight.jpg' width='128' height='152' /></td>";
    //            OprettetMail &= "</tr>";
    //            OprettetMail &= "<tr>";
    //            OprettetMail &= "<th scope='row'><img src='BottomLeft.jpg' width='94' height='137' /></th>";
    //            OprettetMail &= "<td><img src='BottomMiddle.jpg' width='535' height='137' /></td>";
    //            OprettetMail &= "<td><img src='BottomRight.jpg' width='128' height='137' /></td>";
    //            OprettetMail &= " </tr>";
    //            OprettetMail &= "</table>";
    //            OprettetMail &= "</body>";
    //            OprettetMail &= "</html>";
    //        }
    //        dbConn.Close();
    //    }
    //}
}