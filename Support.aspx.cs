using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Support : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlWriteSomething.Visible = false;

        if (User.Identity.IsAuthenticated)
        {
            pnlOnline.Visible = true;
        }
        else
        {
            Response.Write("Du er ikke logget ind!");
            pnlOnline.Visible = false;
            Response.Redirect("Default.aspx");
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        
    }
}
