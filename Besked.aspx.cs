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

public partial class Besked : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] == "profil")
        {
            Status.Text = "Tillykke, du har oprettet din profil.<br />Du vil få en mail som du skal aktivere.";


        }

        if (Request.QueryString["type"] == "glemtkode")
        { 
            Status.Text = "Vi har sendt dig en ny kode, som du kan logge ind med og ændre";
        }

        if (Request.QueryString["type"] == "aktiveret")
        {
            
            Status.Text = "Du har nu aktiveret din profil";
        }
    }
}
