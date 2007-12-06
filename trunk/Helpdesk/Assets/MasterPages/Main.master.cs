using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Main : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		//Sets the path of the javascript file to the correct place, despite whichever page you are visinting.
		ClockJavascript.Text = String.Format("<script src=\"{0}/Assets/js/Clock.js\" type=\"text/javascript\"></script>", Request.ApplicationPath);
	}
}
