using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ChatMsg
/// </summary>
[ Serializable() ]
public class ChatMsg
{
	public ChatMsg()
	{


	}

    public int UserID;
	public string Message;
	public string Name;
	public string TimeStamp;
}
