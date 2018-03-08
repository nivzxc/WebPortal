using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class CMD_ControlPanel : System.Web.UI.Page
{
 
	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
 }

	protected void btnUpdateSD_Click(object sender, ImageClickEventArgs e)
	{
		DataTable tblOmegaSchools = new DataTable();
		using (SqlConnection cnOmega = new SqlConnection(ConfigurationManager.ConnectionStrings["Omega"].ToString()))
		{
			using (SqlCommand cmdOmega = cnOmega.CreateCommand())
			{
				cmdOmega.CommandText = "SELECT * FROM dbo_branches";
				using (SqlDataAdapter daOmega = new SqlDataAdapter(cmdOmega))
				{
					cnOmega.Open();
					daOmega.Fill(tblOmegaSchools);
				}
			}
		}

		using(SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			using (SqlCommand cmd = cn.CreateCommand())
			{
				cmd.CommandText = "UPDATE CM.Schools SET schlname=@schlname,schladdr=@schladdr,ceoname=@ceoname,cooname=@cooname,telnmbr=@telnmbr,lastupdt=@lastupdt WHERE schlcode=@schlcode";
				cmd.Parameters.Add("@schlcode", SqlDbType.VarChar, 3);
				cmd.Parameters.Add("@schlname", SqlDbType.VarChar, 50);
				cmd.Parameters.Add("@schladdr", SqlDbType.VarChar, 100);
				cmd.Parameters.Add("@ceoname", SqlDbType.VarChar, 30);
				cmd.Parameters.Add("@cooname", SqlDbType.VarChar, 30);
				cmd.Parameters.Add("@telnmbr",SqlDbType.VarChar,100);
				cmd.Parameters.Add("@lastupdt", SqlDbType.DateTime);
				cn.Open();
				foreach (DataRow drow in tblOmegaSchools.Rows)
				{
					cmd.Parameters["@schlcode"].Value = drow["branch_code"].ToString();
					cmd.Parameters["@schlname"].Value = drow["branch_name"].ToString();
					cmd.Parameters["@schladdr"].Value = drow["branch_address"].ToString();
					cmd.Parameters["@ceoname"].Value = drow["branch_ceo"].ToString();
					cmd.Parameters["@cooname"].Value = drow["branch_coo"].ToString();
					cmd.Parameters["@telnmbr"].Value = drow["phone_no"].ToString();
					cmd.Parameters["@lastupdt"].Value = drow["d_update"].ToString();
					cmd.ExecuteNonQuery();
				}
			}
		}

	}

}
