using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CIS_MRCF_OracleDatabaseProblem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "ACM Database is currently not available.<br/>Please contact the System Administrator.";
    }
}