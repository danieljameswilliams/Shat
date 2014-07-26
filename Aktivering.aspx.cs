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

public partial class Aktivering : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //http://www.shat.dk/Aktivering.aspx?t=Opret&p=Rofistick&v=12
            if (Request.QueryString["t"] == "Opret")
            {
                string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
                using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
                {
                    dbConn.Open();
                    string sql = "SELECT fldUsername FROM userinfo WHERE fldUsername=?brugernavn AND fldActivated=?aktiveret LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                    { 
                    cmd.Parameters.Add("?brugernavn", Request.QueryString["p"]);
                    cmd.Parameters.Add("?aktiveret", Request.QueryString["v"]);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        Status.Text = "Din bruger er nu aktiveret, og du kan nu logge ind!";
                        //pnlLogin.Visible = true;

                        string sql2 = "UPDATE userinfo SET fldActivated=1 WHERE fldUsername=?brugernavn";
                        using (MySqlCommand cmd2 = new MySqlCommand(sql2, dbConn))
                        {
                            cmd2.Parameters.Add("?brugernavn", Request.QueryString["p"]);

                            cmd2.ExecuteNonQuery();
                        }
                        dbConn.Close();
                    }

                    else
                    {
                        Status.Text = "Der findes ingen brugere til denne aktiveringskode.";
                    }
                        }
                }
            }
        }
        catch
        {
            Status.Text = "Der er opstået en fejl";
        }
    }
}
