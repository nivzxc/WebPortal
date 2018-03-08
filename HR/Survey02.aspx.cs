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

public partial class HR_Survey02 : System.Web.UI.Page
{

	protected void CheckItems()
	{
		string strMessage = "";

		if (!rad011.Checked && !rad012.Checked && !rad013.Checked)
			strMessage = strMessage + "<br>Please answer item #01";

		if (!rad021.Checked && !rad022.Checked && !rad023.Checked)
			strMessage = strMessage + "<br>Please answer item #02";

		if (!rad031.Checked && !rad032.Checked && !rad033.Checked)
			strMessage = strMessage + "<br>Please answer item #03";

		if (!rad041.Checked && !rad042.Checked && !rad043.Checked)
			strMessage = strMessage + "<br>Please answer item #04";

		if (!rad051.Checked && !rad052.Checked && !rad053.Checked)
			strMessage = strMessage + "<br>Please answer item #05";

		if (!rad061.Checked && !rad062.Checked && !rad063.Checked)
			strMessage = strMessage + "<br>Please answer item #06";

		if (!rad071.Checked && !rad072.Checked && !rad073.Checked)
			strMessage = strMessage + "<br>Please answer item #07";

		if (!rad081.Checked && !rad082.Checked && !rad083.Checked)
			strMessage = strMessage + "<br>Please answer item #08";

		if (!rad091.Checked && !rad092.Checked && !rad093.Checked)
			strMessage = strMessage + "<br>Please answer item #09";

		if (!rad101.Checked && !rad102.Checked && !rad103.Checked)
			strMessage = strMessage + "<br>Please answer item #10";

		if (!rad111.Checked && !rad112.Checked && !rad113.Checked)
			strMessage = strMessage + "<br>Please answer item #11";

		if (!rad121.Checked && !rad122.Checked && !rad123.Checked)
			strMessage = strMessage + "<br>Please answer item #12";

		if (!rad131.Checked && !rad132.Checked && !rad133.Checked)
			strMessage = strMessage + "<br>Please answer item #13";

		if (!rad14A1.Checked && !rad14A2.Checked && !rad14A3.Checked)
			strMessage = strMessage + "<br>Please answer item #14 A";

		if (!rad14B1.Checked && !rad14B2.Checked && !rad14B3.Checked)
			strMessage = strMessage + "<br>Please answer item #14 B";

		if (!rad151.Checked && !rad152.Checked && !rad153.Checked)
			strMessage = strMessage + "<br>Please answer item #15";

		if (!rad161.Checked && !rad162.Checked && !rad163.Checked)
			strMessage = strMessage + "<br>Please answer item #16";

		if (!rad171.Checked && !rad172.Checked && !rad173.Checked)
			strMessage = strMessage + "<br>Please answer item #17";

		if (!rad181.Checked && !rad182.Checked && !rad183.Checked)
			strMessage = strMessage + "<br>Please answer item #18";

		if (!rad191.Checked && !rad192.Checked && !rad193.Checked)
			strMessage = strMessage + "<br>Please answer item #19";

		if (!rad201.Checked && !rad202.Checked && !rad203.Checked)
			strMessage = strMessage + "<br>Please answer item #20";

		if (!rad211.Checked && !rad212.Checked && !rad213.Checked)
			strMessage = strMessage + "<br>Please answer item #21";

		if (!rad221.Checked && !rad222.Checked && !rad223.Checked)
			strMessage = strMessage + "<br>Please answer item #22";

		lblMessage.Text = strMessage;

		if (strMessage == "")
			trError.Visible = false;
		else
			trError.Visible = true;

	}

 protected void Page_Load(object sender, EventArgs e)
 {

 }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{
		CheckItems();
		if (!trError.Visible)
		{
			DataTable tblAnswers = Session["answers"] as DataTable;
			SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
			cn.Open();
			SqlTransaction trn = cn.BeginTransaction();
			SqlCommand cmd = cn.CreateCommand();
			cmd.Transaction = trn;
			try
			{
				foreach (DataRow drow in tblAnswers.Rows)
				{
     cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + drow["answer"] + "','" + drow["itemcode"] + "')";
					cmd.ExecuteNonQuery();
				}

				// Item #01
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad011.Checked ? "1" : (rad012.Checked ? "2" : "3")) + "','36')";
				cmd.ExecuteNonQuery();

				// Item #02
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad021.Checked ? "1" : (rad022.Checked ? "2" : "3")) + "','37')";
				cmd.ExecuteNonQuery();

				// Item #03
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad031.Checked ? "1" : (rad032.Checked ? "2" : "3")) + "','38')";
				cmd.ExecuteNonQuery();

				// Item #04
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad041.Checked ? "1" : (rad042.Checked ? "2" : "3")) + "','39')";
				cmd.ExecuteNonQuery();

				// Item #05
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad051.Checked ? "1" : (rad052.Checked ? "2" : "3")) + "','40')";
				cmd.ExecuteNonQuery();

				// Item #06
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad061.Checked ? "1" : (rad062.Checked ? "2" : "3")) + "','41')";
				cmd.ExecuteNonQuery();

				// Item #07
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad071.Checked ? "1" : (rad072.Checked ? "2" : "3")) + "','42')";
				cmd.ExecuteNonQuery();

				// Item #08
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad081.Checked ? "1" : (rad082.Checked ? "2" : "3")) + "','43')";
				cmd.ExecuteNonQuery();

				// Item #09
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad091.Checked ? "1" : (rad092.Checked ? "2" : "3")) + "','44')";
				cmd.ExecuteNonQuery();

				// Item #10
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad101.Checked ? "1" : (rad102.Checked ? "2" : "3")) + "','45')";
				cmd.ExecuteNonQuery();

				// Item #11
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad111.Checked ? "1" : (rad112.Checked ? "2" : "3")) + "','46')";
				cmd.ExecuteNonQuery();

				// Item #12
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad121.Checked ? "1" : (rad122.Checked ? "2" : "3")) + "','47')";
				cmd.ExecuteNonQuery();

				// Item #13
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad131.Checked ? "1" : (rad132.Checked ? "2" : "3")) + "','48')";
				cmd.ExecuteNonQuery();

				// Item #14 A
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad14A1.Checked ? "1" : (rad14A2.Checked ? "2" : "3")) + "','49')";
				cmd.ExecuteNonQuery();

				// Item #14 B
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad14B1.Checked ? "1" : (rad14B2.Checked ? "2" : "3")) + "','50')";
				cmd.ExecuteNonQuery();

				// Item #15
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad151.Checked ? "1" : (rad152.Checked ? "2" : "3")) + "','51')";
				cmd.ExecuteNonQuery();

				// Item #16
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad161.Checked ? "1" : (rad162.Checked ? "2" : "3")) + "','52')";
				cmd.ExecuteNonQuery();

				// Item #17
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad171.Checked ? "1" : (rad172.Checked ? "2" : "3")) + "','53')";
				cmd.ExecuteNonQuery();

				// Item #18
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad181.Checked ? "1" : (rad182.Checked ? "2" : "3")) + "','54')";
				cmd.ExecuteNonQuery();

				// Item #19
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad191.Checked ? "1" : (rad192.Checked ? "2" : "3")) + "','55')";
				cmd.ExecuteNonQuery();

				// Item #20
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad201.Checked ? "1" : (rad202.Checked ? "2" : "3")) + "','56')";
				cmd.ExecuteNonQuery();

				// Item #21
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad211.Checked ? "1" : (rad212.Checked ? "2" : "3")) + "','57')";
				cmd.ExecuteNonQuery();

				// Item #22
    cmd.CommandText = "INSERT INTO survey_users_answers VALUES('" + Session["sempcode"] + "','" + (rad221.Checked ? "1" : (rad222.Checked ? "2" : "3")) + "','58')";
				cmd.ExecuteNonQuery();

				trn.Commit();
			}
			catch
			{
				trn.Rollback();
			}
			finally
			{
				cn.Close();
			}
			Response.Redirect("SurveyThanks.aspx");
		}
	}

}
