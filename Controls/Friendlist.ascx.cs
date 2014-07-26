using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Controls_Friendlist : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        DataSet DataProfiles = new DataSet();
        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
        dbConn.Open();
            string sql = "SELECT F.fldtheFriend U.fldID FROM friendlist As F INNER JOIN userinfo As U ON F.fldViewer = U.fldID WHERE F.fldViewer='" + Page.User.Identity.Name + "' AND F.flddeleted=0;";
            using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
            {
                //.......Her er udskrivningen til en repeater.......
                Repeatme.DataSource = cmd.ExecuteReader();
                Repeatme.DataBind();
            }
        dbConn.Close();
        }
        }
        catch
        {
            Response.Write("Du er ikke logget ind");
        }
    }
}
