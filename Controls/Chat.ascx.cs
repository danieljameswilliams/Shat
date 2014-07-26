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

//INSERT INTO chatmessages (fldUserID, fldDateTime, fldMessage) VALUES ('2','2010-06-30 17:23:48','hey Peter! :D')

//'Seneste besked'
//SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1

//'Seneste besked brugeren har sendt'
//SELECT fldID FROM chatmessages WHERE fldUserID=2 ORDER BY fldID DESC LIMIT 1 

//'Sidste besked brugeren har set'
//SELECT fldLastMsgSeen FROM userinfo WHERE fldID=2

//'Check om sidste besked brugeren har set = seneste besked'
//SELECT IF((SELECT fldLastMsgSeen FROM userinfo WHERE fldID=2)=(SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1),'1','0');

//'Sæt sidste besked brugeren har set = seneste besked'
//UPDATE userinfo Set fldLastMsgSeen=(SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1) Where fldID=2

//'Sæt sidste besked brugeren har set = seneste besked brugeren har sendt'
//UPDATE userinfo Set fldLastMsgSeen=(SELECT fldID FROM chatmessages WHERE fldUserID=2 ORDER BY fldID DESC LIMIT 1) Where fldID=2


//'Rod'
//SELECT * FROM chatmessages WHERE fldID>6 ORDER BY fldID ASC

//SELECT C.fldID, C.fldUserID, C.fldDateTime, C.fldMessage, U.fldUsername FROM chatmessages AS C INNER JOIN userinfo AS U ON C.fldUserID=U.fldID WHERE C.fldID>100 ORDER BY fldID ASC

//SELECT * FROM chatmessages WHERE fldID=(SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1)

    public partial class Controls_Chat : System.Web.UI.UserControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            // Write the QueryString and Form collection to the page
            // using the Ajax.NET JavaScript serializer.

            StringBuilder sb = new StringBuilder();

            sb.Append("<script type=\"text/javascript\">\r\n");
            sb.Append("var queryString = ");
            sb.Append(JavaScriptSerializer.Serialize(Request.QueryString));
            sb.Append(";\r\nvar form = ");
            sb.Append(JavaScriptSerializer.Serialize(Request.Form));
            sb.Append(";\r\n</script>\r\n");
            
            Parent.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"request", sb.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Default.aspx");
            }

            int RoomID = 1;
            if (string.IsNullOrEmpty(Request.QueryString["Room"]))
            {
                Response.Redirect("Chat.aspx?Room=1");
            }
            else
            {
                RoomID = Convert.ToInt32(Request.QueryString["Room"]);
            }

            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                string sql = "SELECT fldRoomName FROM rooms WHERE fldID=?ROOM";
                MySqlCommand cmd2 = new MySqlCommand(sql, dbConn);
                cmd2.Parameters.Add("?ROOM", RoomID);
                object result = cmd2.ExecuteScalar();
                if (result == null)
                {
                    Response.Redirect("Default.aspx");
                }

                // Sæt sidste besked brugeren har set = seneste besked
                sql = "UPDATE userinfo Set fldRoom=?ROOM, fldPingTime=?DATETIME, fldLastMsgSeen=(SELECT fldID FROM chatmessages WHERE fldRoomID=?ROOM ORDER BY fldID DESC LIMIT 20,1) Where fldID=?USERID";
                MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
                cmd3.Parameters.Add("?USERID", UserID);
                cmd3.Parameters.Add("?ROOM", RoomID);
                cmd3.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd3.ExecuteNonQuery();

                dbConn.Close();
            }

            AjaxPro.Utility.RegisterTypeForAjax(typeof(Controls_Chat), this.Page);
        }

        // CHAT ENGINE

        [AjaxMethod()]
        public void AddChatter(int RoomID)
        {
            //FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            //int UserID = Convert.ToInt32(ident.Ticket.UserData);

            //string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            //using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            //{
            //    dbConn.Open();

            //    // Sæt fldRoom og fldPingTime for brugeren
            //    sql = "UPDATE userinfo Set fldRoom=?ROOM, fldPingTime=?DATETIME WHERE fldID=?USERID";
            //    //string sql = "UPDATE userinfo Set fldRoom=1, fldPingTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE fldID=" + UserID;
            //    MySqlCommand cmd = new MySqlCommand(sql, dbConn);
            //    cmd.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //    cmd.Parameters.Add("?ROOM", RoomID);
            //    cmd.Parameters.Add("?USERID", UserID);
            //    cmd.ExecuteNonQuery();

            //    // Sæt sidste besked brugeren har set = seneste besked
            //    sql = "UPDATE userinfo Set  fldRoom=?ROOM, fldPingTime=?DATETIME, fldLastMsgSeen=(SELECT fldID FROM chatmessages WHERE fldRoomID=?ROOM ORDER BY fldID DESC LIMIT 10,1) Where fldID=?USERID";
            //    MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
            //    cmd3.Parameters.Add("?USERID", UserID);
            //    cmd3.Parameters.Add("?ROOM", RoomID);
            //    cmd3.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //    cmd3.ExecuteNonQuery();

            //    dbConn.Close();
            //}
        }

        [AjaxMethod()]
        public void RemoveChatter()
        {
            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                // Sæt fldRoom = 0 (hvilket betyder at brugeren ikke chatter mere)
                string sql = "UPDATE userinfo Set fldRoom=?ROOM Where fldID=?USERID";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.Parameters.Add("?ROOM", 0);
                cmd.Parameters.Add("?USERID", UserID);
                cmd.ExecuteNonQuery();

                dbConn.Close();
            }
        }

        [AjaxMethod()]
        public ChatMsg SendMsg(int RoomID, string msgText)
        {
            DateTime now = DateTime.Now;

            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            ChatMsg msg = new ChatMsg();
            msg.Name = HttpContext.Current.User.Identity.Name;
            msg.Message = HttpUtility.HtmlEncode(msgText);
            msg.TimeStamp = now.ToShortTimeString();
            msg.UserID = UserID;

            //Læg beskeden i databasen
            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                // Indsæt chatbesked
                string sql = "INSERT INTO chatmessages (fldUserID, fldDateTime, fldMessage, fldRoomID) VALUES (?USERID,?DATE,?MESSAGE,?ROOMID)";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.Parameters.Add("?USERID", UserID);
                cmd.Parameters.Add("?DATE", now);
                cmd.Parameters.Add("?MESSAGE", HttpUtility.HtmlEncode(msgText));
                cmd.Parameters.Add("?ROOMID", RoomID);
                cmd.ExecuteNonQuery();

                // Sæt sidste sete besked til denne
                sql = "UPDATE userinfo Set fldLastMsgSeen=(SELECT fldID FROM chatmessages WHERE fldUserID=?USERID ORDER BY fldID DESC LIMIT 1) Where fldID=?USERID";
                MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
                cmd3.Parameters.Add("?USERID", UserID);
                cmd3.ExecuteNonQuery();

                dbConn.Close();
            }

            return msg;
        }

        [AjaxMethod()]
        public ChatMsg SendPrivateMsg(int toUserId, string msgText)
        {
            DateTime now = DateTime.Now;

            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            ChatMsg msg = new ChatMsg();
            msg.Name = HttpContext.Current.User.Identity.Name;
            msg.Message = HttpUtility.HtmlEncode(msgText);
            msg.TimeStamp = now.ToShortTimeString();
            msg.UserID = UserID;

            //Læg beskeden i databasen
            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                // Indsæt chatbesked
                string sql = "INSERT INTO privatechatmessages (fldFrom, fldDateTime, fldMessage, fldTo) VALUES (?FROM,?DATE,?MESSAGE,?TO)";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.Parameters.Add("?FROM", UserID);
                cmd.Parameters.Add("?DATE", now);
                cmd.Parameters.Add("?MESSAGE", HttpUtility.HtmlEncode(msgText));
                cmd.Parameters.Add("?TO", toUserId);
                cmd.ExecuteNonQuery();

                // Sæt sidste sete besked til denne
                sql = "UPDATE userinfo Set fldLastPrivateMsgSeen=(SELECT fldID FROM privatechatmessages WHERE fldFrom=?FROM ORDER BY fldID DESC LIMIT 1) Where fldID=?FROM";
                MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
                cmd3.Parameters.Add("?FROM", UserID);
                cmd3.ExecuteNonQuery();

                dbConn.Close();
            }

            return msg;
        }

        [AjaxMethod()]
        public string GetChatters(int RoomID)
        {
            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            string result = "";
            string sql = "";

            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                // Hent sidste aktivitetstidspunkt for alle brugere i samme rum, 
                // og tjek om det er over 10 sek siden. 
                // Hvis det er, så sæt deres fldRoom til 0
                sql = "SELECT fldID, fldPingTime FROM userinfo WHERE fldRoom=(SELECT fldRoom FROM userinfo WHERE fldID=" + UserID + ");";
                using (MySqlCommand cmd2 = new MySqlCommand(sql, dbConn))
                {
                    DataSet users = new DataSet();
                    //cmd2.Parameters.Add("?USERID", UserID);
                    (new MySqlDataAdapter(cmd2.CommandText, dbConn)).Fill(users);
                    if (users.Tables[0].Rows.Count != 0)
                    {
                        sql = "UPDATE userinfo Set fldRoom=0 WHERE";
                        bool firstRow = true;
                        DateTime pingTime;
                        foreach (DataRow user in users.Tables[0].Rows)
                        {
                            pingTime = Convert.ToDateTime(user["fldPingTime"].ToString());

                            DateTime startTime = pingTime;
                            DateTime endTime = DateTime.Now;
                            TimeSpan span = endTime.Subtract(startTime);

                            if (span.TotalSeconds > 10)
                            {
                                //Debug.WriteLine("ID: " + user["fldID"] + ". Seconds: " + span.TotalSeconds);
                                if (!firstRow)
                                {
                                    sql += " OR";
                                }
                                sql += " fldID=" + user["fldID"];
                                firstRow = false;
                            }
                        }

                        if (sql != "UPDATE userinfo Set fldRoom=0 WHERE")
                        {
                            MySqlCommand cmd4 = new MySqlCommand(sql, dbConn);
                            cmd4.ExecuteNonQuery();
                        }
                    }
                }
                //////////>

                // Sæt PingTime til nu
                sql = "UPDATE userinfo Set fldPingTime=?DATETIME Where fldID=?USERID";
                MySqlCommand cmd = new MySqlCommand(sql, dbConn);
                cmd.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.Add("?USERID", UserID);
                cmd.ExecuteNonQuery();

                //Returner alle brugere der sidder i det rum den nuværende bruger er i
                sql = "SELECT fldID, fldUsername FROM userinfo WHERE fldRoom=" + RoomID;
                using (MySqlCommand cmd3 = new MySqlCommand(sql, dbConn))
                {
                    MySqlDataReader reader = cmd3.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result += "&nbsp;&nbsp;<b><a id='" + reader.GetInt32(reader.GetOrdinal("fldID")) + "'>" + reader.GetString(reader.GetOrdinal("fldUsername")) + "</a></b><br />";
                        }
                    }
                }

                dbConn.Close();
            }
            return result;
        }

        [AjaxMethod()]
        public UserInfo GetProfile(int UserID)
        {
            UserInfo user = new UserInfo();
            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                string sql = "SELECT * FROM userinfo WHERE fldID=" + UserID + ";";
                using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.username = reader.GetString(reader.GetOrdinal("fldUsername"));
                            user.name = reader.GetString(reader.GetOrdinal("fldName"));
                            user.sirname = reader.GetString(reader.GetOrdinal("fldSirname"));
                            user.birthday = reader.GetString(reader.GetOrdinal("fldBirthday"));
                            user.avatar = reader.GetString(reader.GetOrdinal("fldAvatar"));
                            user.id = reader.GetInt32(reader.GetOrdinal("fldID"));

                            //if (reader.GetString(reader.GetOrdinal("fldUsername")) > 0)
                            //{
                            //    litProfileUpdates.Text += "<span style='font-weight:bold;'>Du har: " + reader.GetString(reader.GetOrdinal("fldUsername")) + " ansøgninger</span>";
                            //}

                        }
                    }
                }
                dbConn.Close();
            }
            return user;
        }

        [AjaxMethod()]
        public string GetMsgs(int RoomID)
        {
            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            string theResult = "";

            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                // Check om der er nye beskeder, og tilføj dem til chatboxen hvis der er
                //string sql = "SELECT IF((SELECT fldLastMsgSeen FROM userinfo WHERE fldID=?USERID)=(SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1),0,1);";
                //using (MySqlCommand cmd = new MySqlCommand(sql, dbConn))
                //{
                //    cmd.Parameters.AddWithValue("?USERID", UserID);
                //    int intNewMsgs = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //    if (intNewMsgs == 0)
                //    {
                //        return "";
                //    }
                //    else
                //    {
                // bare brug denne her til if sætningen
                string sql = "SELECT C.fldID, C.fldUserID, C.fldDateTime, C.fldMessage, U.fldUsername FROM chatmessages AS C INNER JOIN userinfo AS U ON C.fldUserID=U.fldID WHERE C.fldID>(SELECT fldLastMsgSeen FROM userinfo WHERE fldID=" + UserID + ") AND fldRoomID=" + RoomID + " ORDER BY fldID ASC";
                using (MySqlCommand cmd2 = new MySqlCommand(sql, dbConn))
                {
                    DataSet messages = new DataSet();
                    (new MySqlDataAdapter(cmd2.CommandText, dbConn)).Fill(messages);
                    cmd2.Parameters.Add("?USERID", UserID);
                    if (messages.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow chatmsg in messages.Tables[0].Rows)
                        {
                            DateTime timeStamp = Convert.ToDateTime(chatmsg["fldDateTime"].ToString());
                            theResult += GetFormattedMsg(chatmsg["fldUsername"].ToString(), timeStamp, chatmsg["fldMessage"].ToString(), Convert.ToInt32(chatmsg["fldUserID"]));
                        }
                    }
                }
                sql = "UPDATE userinfo Set fldLastMsgSeen=(SELECT fldID FROM chatmessages ORDER BY fldID DESC LIMIT 1) Where fldID=?USERID";
                MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
                cmd3.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd3.Parameters.Add("?USERID", UserID);
                cmd3.ExecuteNonQuery();
                //    }
                //}
                dbConn.Close();
            }
            return theResult;
        }

        [AjaxMethod()]
        public int GetThisUsersID()
        {
            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);  
            return UserID;
        }

        [AjaxMethod()]
        public DataSet GetPrivateMsgs()
        {
            FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;
            int UserID = Convert.ToInt32(ident.Ticket.UserData);

            string theResult = "";
            DataSet result = new DataSet();
            result.Tables.Add("messages");
            DataTable resMessages = result.Tables["messages"];
            resMessages.Columns.Add("from", typeof(int));
            resMessages.Columns.Add("message", typeof(string));

            string Connectionstrengen = ConfigurationManager.ConnectionStrings["WebdbConn"].ConnectionString;
            using (MySqlConnection dbConn = new MySqlConnection(Connectionstrengen))
            {
                dbConn.Open();

                string sql = "SELECT C.fldID, C.fldFrom, C.fldDateTime, C.fldMessage, C.fldTo, U.fldUsername FROM privatechatmessages AS C INNER JOIN userinfo AS U ON C.fldFrom=U.fldID WHERE C.fldID>(SELECT fldLastPrivateMsgSeen FROM userinfo WHERE fldID=" + UserID + ") AND fldTo=" + UserID + " ORDER BY fldID ASC";
                using (MySqlCommand cmd2 = new MySqlCommand(sql, dbConn))
                {
                    DataSet messages = new DataSet();
                    (new MySqlDataAdapter(cmd2.CommandText, dbConn)).Fill(messages);
                    cmd2.Parameters.Add("?USERID", UserID);
                    if (messages.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow chatmsg in messages.Tables[0].Rows)
                        {
                            DateTime timeStamp = Convert.ToDateTime(chatmsg["fldDateTime"].ToString());
                            resMessages.Rows.Add(chatmsg["fldFrom"], GetFormattedPrivateMsg(chatmsg["fldUsername"].ToString(), timeStamp, chatmsg["fldMessage"].ToString(), Convert.ToInt32(chatmsg["fldFrom"])));
                        }
                    }
                }
                sql = "UPDATE userinfo Set fldLastPrivateMsgSeen=(SELECT fldID FROM privatechatmessages ORDER BY fldID DESC LIMIT 1) Where fldID=?USERID";
                MySqlCommand cmd3 = new MySqlCommand(sql, dbConn);
                cmd3.Parameters.Add("?DATETIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd3.Parameters.Add("?USERID", UserID);
                cmd3.ExecuteNonQuery();

                dbConn.Close();
            }
            
            return result;
        }
        
        private string GetFormattedMsg(string Name, DateTime TimeStamp, string Message, int UserID)
        {
            string cssClass = (HttpContext.Current.User.Identity.Name == Name) ? " ColorOwn" : "";
            string time = TimeStamp.ToShortTimeString();
            return "<div class='ChatMessageWrapper" + cssClass + "'><div class='ChatNameStyle'>" + time + " | [<b><a id='" + UserID + "'>" + Name + "</a></b>]</div><div class='ChatMessageStyle'>" + Message + "</div></div>";
        }

        private string GetFormattedPrivateMsg(string Name, DateTime TimeStamp, string Message, int UserID)
        {
            string cssClass = (HttpContext.Current.User.Identity.Name == Name) ? " ColorOwn" : "";
            string time = TimeStamp.ToShortTimeString();
            return "<div class='ChatMessageWrapper" + cssClass + "'><div class=\'ChatNameMessage\'>" + time + " | <b>" + Name + ": </b>" + Message + "</div></div>";
        }

        
    }