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
using System.Collections.Generic;
using MetaBuilders.WebControls;

public partial class _Default : System.Web.UI.Page
{
    MetaBuilders.WebControls.FileUpload FileUp;
    protected void Page_Load( object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
        string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
        using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
        {
            dbConn.Open();

            
                //.......Her er udskrivningen til en Literal......
                //MySqlDataReader reader = cmd.ExecuteReader();
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        litPictureFrame.Text += reader.GetString(reader.GetOrdinal("fldUsername"));
                //    }
                //}

                //.......Her er udskrivningen til en repeater.......
            if (!User.Identity.IsAuthenticated)
            {
                string sql2 = "SELECT * FROM userinfo WHERE CHAR_LENGTH(fldUsername)<10 AND fldPublic=1 LIMIT 4;";
                using (MySqlCommand cmd = new MySqlCommand(sql2, dbConn))
                {
                    Repeater repeatme = (Repeater)LoginView1.FindControl("repTest"); repeatme.DataSource = cmd.ExecuteReader();
                    Repeater repeatme2 = (Repeater)LoginView1.FindControl("repTest"); repeatme2.DataBind();
                }
            }
            else
            {
                //<div class="darklist"></div>
                //<div class="lightlist"></div>

                string sql = "SELECT * FROM rooms";
                using (MySqlCommand cmd2 = new MySqlCommand(sql, dbConn))
                {
                    Literal litRooms = (Literal)LoginView1.FindControl("litRooms");

                    MySqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        string klasse = "lightlist2";
                        while (reader.Read())
                        {
                            klasse = (klasse == "lightlist2") ? "darklist2" : "lightlist2";
                            litRooms.Text += "<div class='" + klasse + "'><div class='chatroomtextoflink'><a href='Chat.aspx?Room=" + reader.GetInt32(reader.GetOrdinal("fldID")) + "'>" + reader.GetString(reader.GetOrdinal("fldRoomName")) + "</a></div><div class='chatroomnumberofroom'><div class='LoggedInChatRoomNumber'>22</div> <div class='LoggedInChatRoomImg'><img src='Media/img/Minor/LoggedIn/RoomCount_woman.png' /><img src='Media/img/Minor/LoggedIn/RoomCount_man.png' /></div></div></div>";
                        }
                    }
                }                
            }
            dbConn.Close();
        }
        
                    
        }
    }

    protected void VisProfil_Click(object sender, System.EventArgs e)
    {
        Panel pnlVisProfil = (Panel)LoginView1.FindControl("pnlVisProfil"); pnlVisProfil.Visible = true;
    }

    protected void FileUp_FileReceived(Object sender, EventArgs e)
    {
        if (FileUp.FileName != "")
        {
            try
            {
                FileUp.PostedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("/"), FileUp.FileName));
                Response.Write("File Saved");
            }
            catch (Exception ex)
            {
                Response.Write("File Not Saved: " + ex.Message);
            }
        }
        else
        {
            Response.Write("No File Uploaded");
        }
    }

}
