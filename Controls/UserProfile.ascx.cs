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
using System.ComponentModel;

using System.Diagnostics;

public partial class Controls_UserProfile : System.Web.UI.UserControl
{
    private int userID;

    public int UserID
    {
        get { return userID; }
        set { userID = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        DataSet DataProfiles = new DataSet();

        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();

            string sql = "SELECT * FROM userinfo WHERE fldID=?fldID;";
            using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
            {
                //.......Her er udskrivningen...... //
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    cmd.Parameters.Add("?fldID", Request.QueryString["id"]);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    litProfil.Text = "<div id='ProfileWrapperforLight'>";
                    litProfil.Text += "<div id='Profileheader'><div id='Profilvisning'><img style='margin-right: 10px;' alt=' ' src='Media/img/Major/User_GenderBlackLarge_" + reader.GetString(reader.GetOrdinal("fldSex")) + ".jpg' />" + reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName")) + " / " + reader.GetString(reader.GetOrdinal("fldUsername")) + "</div><div id='Acticityfeed'></div></div>";

                    litProfil.Text += "<div class='leftwrapper'><div class='picturewrapper'><div class='ProfileSitePicture'><img src='Media/img/Major/ProfilePictures/" + reader.GetString(reader.GetOrdinal("fldAvatar")) + "' /></div></div>";

                    DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("fldBirthday"));
                    // cache the current time
                    DateTime now = DateTime.Today;
                    // get the difference in years
                    int years = now.Year - birthDate.Year;
                    // subtract another year if we're before the
                    // birth day in the current year
                    if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                        --years;

                    litProfil.Text += "<div style='margin-top: 25px' class='ProfileSiteHeadline'>Alder:</div><div class='ProfileSiteName'>" + years + "</div>";
                    
                    //Shat.Users shatUser = new Shat.Users();
                    //string test = shatUser.TestFunktion();

                    string CivilStatus = Shat.Users.GetEnumStatus(reader.GetInt32(reader.GetOrdinal("fldStatus")));
                    litProfil.Text += "<br /><div class='ProfileSiteHeadline'>Civilstatus:</div><div class='ProfileSiteName'>" + CivilStatus + "</div>";
                    
                    litProfil.Text += "<div id='mulighedsbox'><div id='dinemuligheder'>Hvad vil du?</div>";
                    litProfil.Text += "<div id='mulighedswrapper'>";
                    litProfil.Text += "<div class='mulighedsitem'>Tilføj som ven</div>";    litProfil.Text += "<div class='mulighedsitem'>Anbefal Venner</div>";    litProfil.Text += "<div class='mulighedsitem'>Vis Billeder (0000)</div>";
                    litProfil.Text += "<div class='mulighedsitem'>Blokér</div>";            litProfil.Text += "<div class='mulighedsitem'>Validér Profil</div>";    litProfil.Text += "<div class='mulighedsitem'>Fælles Venner (000)</div>";
                    litProfil.Text += "<div class='mulighedsitem'>Send Besked</div>";       litProfil.Text += "<div class='mulighedsitem'>Anmeld</div>";            litProfil.Text += "<div class='mulighedsitem'>Venner (0000)</div>";
                    litProfil.Text += "<div class='mulighedsitem'>Chat med " + reader.GetString(reader.GetOrdinal("fldUsername")) + "</div>"; litProfil.Text += "<div class='mulighedsitem'>Del</div>"; litProfil.Text += "";
                    litProfil.Text += "</div></div></div>";

                    litProfil.Text += "<div class='rightwrapper'>";
                    litProfil.Text += "<div class='Oplysningsfelt'> Oplysninger</div>";
                    litProfil.Text += "<div class='darklist'><div class='rightfieldname'>Fødselsdato:</div><div class='rightfieldanswer'>19. Februar 1992</div></div>";
                    litProfil.Text += "<div class='lightlist'><div class='rightfieldname'>Interesseret i:</div><div class='rightfieldanswerspecial'><img src='Media/img/Major/User_GenderLarge_0.png'></div></div>";
                    litProfil.Text += "<div class='darklist'><div class='rightfieldname'>Søger:</div><div class='rightfieldanswer'>Dating og Venskab</div></div><br />";

                    litProfil.Text += "<div class='lightlist'><div class='rightfieldname'>E-Mail:</div><div class='rightfieldanswer'>mail@danielwilliams.dk</div></div>";
                    litProfil.Text += "<div class='darklist'><div class='rightfieldname'>Messenger:</div><div class='rightfieldanswer'>msn@rofistick.com</div></div>";
                    litProfil.Text += "<div class='lightlist'><div class='rightfieldname'>Website</div><div class='rightfieldanswer'>www.danielwilliams.dk</div></div>";
                    litProfil.Text += "<div class='darklist'><div class='rightfieldname whitecolor'>Link til Facebook</div></div><br />";
                    litProfil.Text += "<div class='lightlist'><div class='rightfieldname'>Mobilnummer:</div><div class='rightfieldanswer'>+45 28 97 95 05</div></div><br />";
                    litProfil.Text += "</div></div>";
                
                }
                dbConn.Close();
            }
        }
    }
}