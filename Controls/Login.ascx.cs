using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web;

public partial class Controls_WebUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                isauth.Visible = true;
                isnotauth.Visible = false;

                    string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
                    using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
                    {
                        dbConn.Open();
                            string sql = "SELECT fldAvatar FROM userinfo WHERE fldUsername='" + Page.User.Identity.Name + "';";
                            using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                            {
                                MySqlDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        AvatarControlPanel.ImageUrl = "../Media/img/Profile/small/" + reader.GetString(reader.GetOrdinal("fldAvatar"));
                                    }
                                }
                            }
                        dbConn.Close();
                    }
                    DataSet DataProfiles = new DataSet();
                    using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
                    {
                        dbConn.Open();

                        string sql = "SELECT * FROM userinfo WHERE fldUsername='" + Page.User.Identity.Name + "' AND fldActivated=1 LIMIT 1;";
                        using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                        {
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    litRightMenu.Text += "<div class='RightMenuItem'><a href='/Profil.aspx?id=" + reader.GetString(reader.GetOrdinal("fldID")) + "'>Vis Profil</a></div>";
                                    litRightMenu.Text += "<div class='RightMenuSplitter'></div>";
                                    litRightMenu.Text += "<div class='RightMenuItem'>Ret Profil</div>";
                                    litRightMenu.Text += "<div class='RightMenuSplitter'></div>";
                                    litRightMenu.Text += "<div class='RightMenuItem'>Indstillinger</div>";
                                    litRightMenu.Text += "<div class='RightMenuSplitter'></div>";
                                    litRightMenu.Text += "<div class='RightMenuItem'>Facebook Sync</div>";
                                    litRightMenu.Text += "<div class='RightMenuSplitter'></div>";
                                    litRightMenu.Text += "<div class='RightMenuItem'>Valider Profil</div>";
                                    litRightMenu.Text += "<div class='RightMenuSplitter'></div>";
                                    litRightMenu.Text += "<div class='RightMenuItem'><a onclick='Deaktiver_Click' nohref='nohref'>Deaktiver Profil</a></div>";


                                    ProfileRealName.Text = reader.GetString(reader.GetOrdinal("fldName")) + " " + reader.GetString(reader.GetOrdinal("fldSirName"));
                                    
                                    if (reader.GetString(reader.GetOrdinal("fldSupportRanked")) == "1")
                                    {
                                        SupportLogon.Visible = true;
                                    }
                                    else 
                                    {
                                        SupportLogon.Visible = false;
                                    }

                                    if (reader.GetString(reader.GetOrdinal("fldSupportOnline")) == "1")
                                    {
                                        SupportLogon.Text = "Log af Supporten";
                                    }
                                    else
                                    {
                                        SupportLogon.Text = "Log på Supporten";
                                    }

                                    //if (reader.GetString(reader.GetOrdinal("fldUsername")) > 0)
                                    //{
                                    //    litProfileUpdates.Text += "<span style='font-weight:bold;'>Du har: " + reader.GetString(reader.GetOrdinal("fldUsername")) + " ansøgninger</span>";
                                    //}

                                }
                            }
                        }
                        dbConn.Close();
                    }
                }
            }
            else
            {
                isauth.Visible = false;
                isnotauth.Visible = true;
            }
        }
    }

    protected void Login1_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
    {
        try
        {
            bool authenticated = AuthenticateFunction(Login1.UserName, Login1.Password, Login1.RememberMeSet);
            if (authenticated)
            {
                string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
                using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
                {
                    dbConn.Open();

                    string sql = "SELECT fldID, fldUsername FROM userinfo WHERE fldUsername='" + Login1.UserName + "';";
                    using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int ID = reader.GetInt32(reader.GetOrdinal("fldID"));
                                string Username = reader.GetString(reader.GetOrdinal("fldUsername"));

                                //
                                //------------------------------------------//

                                // Create cookie
                                FormsAuthenticationTicket authTicket =
                                  new FormsAuthenticationTicket(1,
                                                                Username,
                                                                DateTime.Now,
                                                                DateTime.Now.AddMinutes(30),
                                                                false,
                                                                ID.ToString());
                                string encTicket = FormsAuthentication.Encrypt(authTicket);
                                HttpCookie faCookie =
                                  new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                                Response.Cookies.Add(faCookie);

                                // Redirect
                                string redirectUrl =
                                  FormsAuthentication.GetRedirectUrl(Username, false);
                                Response.Redirect(redirectUrl);

                                //------------------------------------------//

                                //FormsAuthentication.RedirectFromLoginPage(Username, Login1.RememberMeSet);
                            }
                        }
                    }
                    dbConn.Close();
                }
            }
        }
        catch
        {
            Login1.FailureText = "Du har indtastet forkert brugernavn eller adgangskode";
        }
    }

    public bool AuthenticateFunction(string userName, string password, bool rememberUserName)
    {

        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();
            string sql = "SELECT fldUsername FROM userinfo WHERE fldUsername=?Username AND fldPassword=?Password";
            using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
            {
                cmd.Parameters.Add("?Username", Login1.UserName);
                cmd.Parameters.Add("?Password", Login1.Password);

                string strReturn = cmd.ExecuteScalar().ToString();
                dbConn.Close();

                if (strReturn.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    protected void SupportLogon_Click(object sender, EventArgs e)
    { 
        if (SupportLogon.Text == "Log på Supporten")
        {
            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();
                string sqlLogPaa = "UPDATE userinfo SET fldSupportOnline='1', fldSupportLatestSignedIn='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE fldUsername='" + Page.User.Identity.Name + "'";
                MySqlCommand cmdLogPaa = new MySqlCommand(sqlLogPaa, dbConn);
                cmdLogPaa.ExecuteNonQuery();

                dbConn.Close();
            }
            SupportLogon.Text = "Log af Supporten";
        }
        else 
        {
            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();
                string sqlLogAf = "UPDATE userinfo SET fldSupportOnline=0, fldSupportLatestSignedOut='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE fldUsername='" + Page.User.Identity.Name + "'";
                MySqlCommand cmdLogAf = new MySqlCommand(sqlLogAf, dbConn);
                cmdLogAf.ExecuteNonQuery();
                dbConn.Close();
            }
            SupportLogon.Text = "Log på Supporten";
        }
    }
}