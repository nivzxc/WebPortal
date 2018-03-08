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

public partial class HR_Survey01 : System.Web.UI.Page
{

	protected bool IsAnswered(string strUsername)
	{
		bool blnResult;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT COUNT(username) FROM survey_users_answers WHERE username='" + strUsername + "'";
			cn.Open();
			try
			{
				blnResult = (Convert.ToDouble(cmd.ExecuteScalar().ToString()) >= 1 ? true : false);
			}
			catch
			{
				blnResult = false;

			}
		}
		if (blnResult)
			lblMessage.Text = "You already casted your vote.";
		return blnResult;
	}

	protected bool IsMiddleNameValid(string strUserName, string strMidName)
	{
		bool blnResult;
		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT empnum FROM HR.Employees WHERE username='" + strUserName + "' AND midname='" + strMidName + "'";
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			blnResult = dr.Read();
			dr.Close();
		}
		if (blnResult)
			lblMessage.Text = "Invalid Middle Name.";
		return blnResult;
	}

	protected string GetUsername(string strEmpNum)
	{
		string strResult = "";
		SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["speedo"].ToString());
		SqlCommand cmd = cn.CreateCommand();
  cmd.CommandText = "SELECT username FROM HR.Employees WHERE empnum='" + strEmpNum + "'";

		try
		{
		 cn.Open();
		 strResult = cmd.ExecuteScalar().ToString();
		}
		catch
		{
			strResult = "na";
		}
		finally
		{
			cn.Close();
		}
		return strResult;
	}

	protected void CheckItems()
	{
		string strMessage = "";

		if (!rad011.Checked && !rad012.Checked && !rad013.Checked && !rad014.Checked && !rad015.Checked && !rad016.Checked && !rad017.Checked)
			strMessage = strMessage + "<br>Please answer item #01";

		if (!rad021.Checked && !rad022.Checked && !rad023.Checked && !rad024.Checked && !rad025.Checked && !rad026.Checked && !rad027.Checked)
			strMessage = strMessage + "<br>Please answer item #02";

		if (!rad031.Checked && !rad032.Checked && !rad033.Checked && !rad034.Checked && !rad035.Checked && !rad036.Checked && !rad037.Checked)
			strMessage = strMessage + "<br>Please answer item #03";

		if (!rad041.Checked && !rad042.Checked && !rad043.Checked && !rad044.Checked && !rad045.Checked && !rad046.Checked && !rad047.Checked)
			strMessage = strMessage + "<br>Please answer item #04";

		if (!rad051.Checked && !rad052.Checked && !rad053.Checked && !rad054.Checked && !rad055.Checked && !rad056.Checked && !rad057.Checked)
			strMessage = strMessage + "<br>Please answer item #05";

		if (!rad061.Checked && !rad062.Checked && !rad063.Checked && !rad064.Checked && !rad065.Checked && !rad066.Checked && !rad067.Checked)
			strMessage = strMessage + "<br>Please answer item #06";

		if (!rad071.Checked && !rad072.Checked && !rad073.Checked && !rad074.Checked && !rad075.Checked && !rad076.Checked && !rad077.Checked)
			strMessage = strMessage + "<br>Please answer item #07";

		if (!rad081.Checked && !rad082.Checked && !rad083.Checked && !rad084.Checked && !rad085.Checked && !rad086.Checked && !rad087.Checked)
			strMessage = strMessage + "<br>Please answer item #08";

		if (!rad091.Checked && !rad092.Checked && !rad093.Checked && !rad094.Checked && !rad095.Checked && !rad096.Checked && !rad097.Checked)
			strMessage = strMessage + "<br>Please answer item #09";

		if (!rad101.Checked && !rad102.Checked && !rad103.Checked && !rad104.Checked && !rad105.Checked && !rad106.Checked && !rad107.Checked)
			strMessage = strMessage + "<br>Please answer item #10";

		if (!rad111.Checked && !rad112.Checked && !rad113.Checked && !rad114.Checked && !rad115.Checked && !rad116.Checked && !rad117.Checked)
			strMessage = strMessage + "<br>Please answer item #11";

		if (!rad121.Checked && !rad122.Checked && !rad123.Checked && !rad124.Checked && !rad125.Checked && !rad126.Checked && !rad127.Checked)
			strMessage = strMessage + "<br>Please answer item #12";

		if (!rad131.Checked && !rad132.Checked && !rad133.Checked && !rad134.Checked && !rad135.Checked && !rad136.Checked && !rad137.Checked)
			strMessage = strMessage + "<br>Please answer item #13";

		if (!rad141.Checked && !rad142.Checked && !rad143.Checked && !rad144.Checked && !rad145.Checked && !rad146.Checked && !rad147.Checked)
			strMessage = strMessage + "<br>Please answer item #14";

		if (!rad151.Checked && !rad152.Checked && !rad153.Checked && !rad154.Checked && !rad155.Checked && !rad156.Checked && !rad157.Checked)
			strMessage = strMessage + "<br>Please answer item #15";

		if (!rad161.Checked && !rad162.Checked && !rad163.Checked && !rad164.Checked && !rad165.Checked && !rad166.Checked && !rad167.Checked)
			strMessage = strMessage + "<br>Please answer item #16";

		if (!rad171.Checked && !rad172.Checked && !rad173.Checked && !rad174.Checked && !rad175.Checked && !rad176.Checked && !rad177.Checked)
			strMessage = strMessage + "<br>Please answer item #17";

		if (!rad181.Checked && !rad182.Checked && !rad183.Checked && !rad184.Checked && !rad185.Checked && !rad186.Checked && !rad187.Checked)
			strMessage = strMessage + "<br>Please answer item #18";

		if (!rad191.Checked && !rad192.Checked && !rad193.Checked && !rad194.Checked && !rad195.Checked && !rad196.Checked && !rad197.Checked)
			strMessage = strMessage + "<br>Please answer item #19";

		if (!rad201.Checked && !rad202.Checked && !rad203.Checked && !rad204.Checked && !rad205.Checked && !rad206.Checked && !rad207.Checked)
			strMessage = strMessage + "<br>Please answer item #20";

		if (!rad211.Checked && !rad212.Checked && !rad213.Checked && !rad214.Checked && !rad215.Checked && !rad216.Checked && !rad217.Checked)
			strMessage = strMessage + "<br>Please answer item #21";

		if (!rad221.Checked && !rad222.Checked && !rad223.Checked && !rad224.Checked && !rad225.Checked && !rad226.Checked && !rad227.Checked)
			strMessage = strMessage + "<br>Please answer item #22";

		if (!rad231.Checked && !rad232.Checked && !rad233.Checked && !rad234.Checked && !rad235.Checked && !rad236.Checked && !rad237.Checked)
			strMessage = strMessage + "<br>Please answer item #23";

		if (!rad241.Checked && !rad242.Checked && !rad243.Checked && !rad244.Checked && !rad245.Checked && !rad246.Checked && !rad247.Checked)
			strMessage = strMessage + "<br>Please answer item #24";

		if (!rad251.Checked && !rad252.Checked && !rad253.Checked && !rad254.Checked && !rad255.Checked && !rad256.Checked && !rad257.Checked)
			strMessage = strMessage + "<br>Please answer item #25";

		if (!rad261.Checked && !rad262.Checked && !rad263.Checked && !rad264.Checked && !rad265.Checked && !rad266.Checked && !rad267.Checked)
			strMessage = strMessage + "<br>Please answer item #26";

		if (!rad271.Checked && !rad272.Checked && !rad273.Checked && !rad274.Checked && !rad275.Checked && !rad276.Checked && !rad277.Checked)
			strMessage = strMessage + "<br>Please answer item #27";

		if (!rad281.Checked && !rad282.Checked && !rad283.Checked && !rad284.Checked && !rad285.Checked && !rad286.Checked && !rad287.Checked)
			strMessage = strMessage + "<br>Please answer item #28";

		if (!rad291.Checked && !rad292.Checked && !rad293.Checked && !rad294.Checked && !rad295.Checked && !rad296.Checked && !rad297.Checked)
			strMessage = strMessage + "<br>Please answer item #29";

		if (!rad301.Checked && !rad302.Checked && !rad303.Checked && !rad304.Checked && !rad305.Checked && !rad306.Checked && !rad307.Checked)
			strMessage = strMessage + "<br>Please answer item #30";

		if (!rad311.Checked && !rad312.Checked && !rad313.Checked && !rad314.Checked && !rad315.Checked && !rad316.Checked && !rad317.Checked)
			strMessage = strMessage + "<br>Please answer item #31";

		if (!rad321.Checked && !rad322.Checked && !rad323.Checked && !rad324.Checked && !rad325.Checked && !rad326.Checked && !rad327.Checked)
			strMessage = strMessage + "<br>Please answer item #32";

		if (!rad331.Checked && !rad332.Checked && !rad333.Checked && !rad334.Checked && !rad335.Checked && !rad336.Checked && !rad337.Checked)
			strMessage = strMessage + "<br>Please answer item #33";

		if (!rad341.Checked && !rad342.Checked && !rad343.Checked && !rad344.Checked && !rad345.Checked && !rad346.Checked && !rad347.Checked)
			strMessage = strMessage + "<br>Please answer item #34";

		if (!rad351.Checked && !rad352.Checked && !rad353.Checked && !rad354.Checked && !rad355.Checked && !rad356.Checked && !rad357.Checked)
			strMessage = strMessage + "<br>Please answer item #35";
		lblMessage.Text = strMessage;

  if (strMessage == "") { trError.Visible = false; }
  else { trError.Visible = true; }
	}
 
	protected void Page_Load(object sender, EventArgs e)
 {

 }

	protected void btnSave_Click(object sender, ImageClickEventArgs e)
	{

				string strUsername = GetUsername(txtEmpNum.Text);
				if (!IsAnswered(strUsername))
				{
					CheckItems();
					if (!trError.Visible)
					{
						DataTable tblAnswers = new DataTable("answers");
						tblAnswers.Columns.Add("answer", System.Type.GetType("System.String"));
						tblAnswers.Columns.Add("itemcode", System.Type.GetType("System.String"));
						Session["username"] = GetUsername(txtEmpNum.Text);
						string strAnswer = "";

						// Item #1
						DataRow drwAnswer01 = tblAnswers.NewRow();
						if (rad011.Checked)
							strAnswer = "1";
						else if (rad012.Checked)
							strAnswer = "2";
						else if (rad013.Checked)
							strAnswer = "3";
						else if (rad014.Checked)
							strAnswer = "4";
						else if (rad015.Checked)
							strAnswer = "5";
						else if (rad016.Checked)
							strAnswer = "6";
						else if (rad017.Checked)
							strAnswer = "7";
						drwAnswer01["answer"] = strAnswer;
						drwAnswer01["itemcode"] = "1";
						tblAnswers.Rows.Add(drwAnswer01);

						// Item #2
						DataRow drwAnswer02 = tblAnswers.NewRow();
						if (rad021.Checked)
							strAnswer = "1";
						else if (rad022.Checked)
							strAnswer = "2";
						else if (rad023.Checked)
							strAnswer = "3";
						else if (rad024.Checked)
							strAnswer = "4";
						else if (rad025.Checked)
							strAnswer = "5";
						else if (rad026.Checked)
							strAnswer = "6";
						else if (rad027.Checked)
							strAnswer = "7";
						drwAnswer02["answer"] = strAnswer;
						drwAnswer02["itemcode"] = "2";
						tblAnswers.Rows.Add(drwAnswer02);

						// Item #3
						DataRow drwAnswer03 = tblAnswers.NewRow();
						if (rad031.Checked)
							strAnswer = "1";
						else if (rad032.Checked)
							strAnswer = "2";
						else if (rad033.Checked)
							strAnswer = "3";
						else if (rad034.Checked)
							strAnswer = "4";
						else if (rad035.Checked)
							strAnswer = "5";
						else if (rad036.Checked)
							strAnswer = "6";
						else if (rad037.Checked)
							strAnswer = "7";
						drwAnswer03["answer"] = strAnswer;
						drwAnswer03["itemcode"] = "3";
						tblAnswers.Rows.Add(drwAnswer03);

						// Item #4
						DataRow drwAnswer04 = tblAnswers.NewRow();
						if (rad041.Checked)
							strAnswer = "1";
						else if (rad042.Checked)
							strAnswer = "2";
						else if (rad043.Checked)
							strAnswer = "3";
						else if (rad044.Checked)
							strAnswer = "4";
						else if (rad045.Checked)
							strAnswer = "5";
						else if (rad046.Checked)
							strAnswer = "6";
						else if (rad047.Checked)
							strAnswer = "7";
						drwAnswer04["answer"] = strAnswer;
						drwAnswer04["itemcode"] = "4";
						tblAnswers.Rows.Add(drwAnswer04);

						// Item #5
						DataRow drwAnswer05 = tblAnswers.NewRow();
						if (rad051.Checked)
							strAnswer = "1";
						else if (rad052.Checked)
							strAnswer = "2";
						else if (rad053.Checked)
							strAnswer = "3";
						else if (rad054.Checked)
							strAnswer = "4";
						else if (rad055.Checked)
							strAnswer = "5";
						else if (rad056.Checked)
							strAnswer = "6";
						else if (rad057.Checked)
							strAnswer = "7";
						drwAnswer05["answer"] = strAnswer;
						drwAnswer05["itemcode"] = "5";
						tblAnswers.Rows.Add(drwAnswer05);

						// Item #6
						DataRow drwAnswer06 = tblAnswers.NewRow();
						if (rad061.Checked)
							strAnswer = "1";
						else if (rad062.Checked)
							strAnswer = "2";
						else if (rad063.Checked)
							strAnswer = "3";
						else if (rad064.Checked)
							strAnswer = "4";
						else if (rad065.Checked)
							strAnswer = "5";
						else if (rad066.Checked)
							strAnswer = "6";
						else if (rad067.Checked)
							strAnswer = "7";
						drwAnswer06["answer"] = strAnswer;
						drwAnswer06["itemcode"] = "6";
						tblAnswers.Rows.Add(drwAnswer06);

						// Item #7
						DataRow drwAnswer07 = tblAnswers.NewRow();
						if (rad071.Checked)
							strAnswer = "1";
						else if (rad072.Checked)
							strAnswer = "2";
						else if (rad073.Checked)
							strAnswer = "3";
						else if (rad074.Checked)
							strAnswer = "4";
						else if (rad075.Checked)
							strAnswer = "5";
						else if (rad076.Checked)
							strAnswer = "6";
						else if (rad077.Checked)
							strAnswer = "7";
						drwAnswer07["answer"] = strAnswer;
						drwAnswer07["itemcode"] = "7";
						tblAnswers.Rows.Add(drwAnswer07);

						// Item #8
						DataRow drwAnswer08 = tblAnswers.NewRow();
						if (rad081.Checked)
							strAnswer = "1";
						else if (rad082.Checked)
							strAnswer = "2";
						else if (rad083.Checked)
							strAnswer = "3";
						else if (rad084.Checked)
							strAnswer = "4";
						else if (rad085.Checked)
							strAnswer = "5";
						else if (rad086.Checked)
							strAnswer = "6";
						else if (rad087.Checked)
							strAnswer = "7";
						drwAnswer08["answer"] = strAnswer;
						drwAnswer08["itemcode"] = "8";
						tblAnswers.Rows.Add(drwAnswer08);

						// Item #9
						DataRow drwAnswer09 = tblAnswers.NewRow();
						if (rad091.Checked)
							strAnswer = "1";
						else if (rad092.Checked)
							strAnswer = "2";
						else if (rad093.Checked)
							strAnswer = "3";
						else if (rad094.Checked)
							strAnswer = "4";
						else if (rad095.Checked)
							strAnswer = "5";
						else if (rad096.Checked)
							strAnswer = "6";
						else if (rad097.Checked)
							strAnswer = "7";
						drwAnswer09["answer"] = strAnswer;
						drwAnswer09["itemcode"] = "9";
						tblAnswers.Rows.Add(drwAnswer09);

						// Item #10
						DataRow drwAnswer10 = tblAnswers.NewRow();
						if (rad101.Checked)
							strAnswer = "1";
						else if (rad102.Checked)
							strAnswer = "2";
						else if (rad103.Checked)
							strAnswer = "3";
						else if (rad104.Checked)
							strAnswer = "4";
						else if (rad105.Checked)
							strAnswer = "5";
						else if (rad106.Checked)
							strAnswer = "6";
						else if (rad107.Checked)
							strAnswer = "7";
						drwAnswer10["answer"] = strAnswer;
						drwAnswer10["itemcode"] = "10";
						tblAnswers.Rows.Add(drwAnswer10);

						// Item #11
						DataRow drwAnswer11 = tblAnswers.NewRow();
						if (rad111.Checked)
							strAnswer = "1";
						else if (rad112.Checked)
							strAnswer = "2";
						else if (rad113.Checked)
							strAnswer = "3";
						else if (rad114.Checked)
							strAnswer = "4";
						else if (rad115.Checked)
							strAnswer = "5";
						else if (rad116.Checked)
							strAnswer = "6";
						else if (rad117.Checked)
							strAnswer = "7";
						drwAnswer11["answer"] = strAnswer;
						drwAnswer11["itemcode"] = "11";
						tblAnswers.Rows.Add(drwAnswer11);

						// Item #12
						DataRow drwAnswer12 = tblAnswers.NewRow();
						if (rad121.Checked)
							strAnswer = "1";
						else if (rad122.Checked)
							strAnswer = "2";
						else if (rad123.Checked)
							strAnswer = "3";
						else if (rad124.Checked)
							strAnswer = "4";
						else if (rad125.Checked)
							strAnswer = "5";
						else if (rad126.Checked)
							strAnswer = "6";
						else if (rad127.Checked)
							strAnswer = "7";
						drwAnswer12["answer"] = strAnswer;
						drwAnswer12["itemcode"] = "12";
						tblAnswers.Rows.Add(drwAnswer12);

						// Item #13
						DataRow drwAnswer13 = tblAnswers.NewRow();
						if (rad131.Checked)
							strAnswer = "1";
						else if (rad132.Checked)
							strAnswer = "2";
						else if (rad133.Checked)
							strAnswer = "3";
						else if (rad134.Checked)
							strAnswer = "4";
						else if (rad135.Checked)
							strAnswer = "5";
						else if (rad136.Checked)
							strAnswer = "6";
						else if (rad137.Checked)
							strAnswer = "7";
						drwAnswer13["answer"] = strAnswer;
						drwAnswer13["itemcode"] = "13";
						tblAnswers.Rows.Add(drwAnswer13);

						// Item #14
						DataRow drwAnswer14 = tblAnswers.NewRow();
						if (rad141.Checked)
							strAnswer = "1";
						else if (rad142.Checked)
							strAnswer = "2";
						else if (rad143.Checked)
							strAnswer = "3";
						else if (rad144.Checked)
							strAnswer = "4";
						else if (rad145.Checked)
							strAnswer = "5";
						else if (rad146.Checked)
							strAnswer = "6";
						else if (rad147.Checked)
							strAnswer = "7";
						drwAnswer14["answer"] = strAnswer;
						drwAnswer14["itemcode"] = "14";
						tblAnswers.Rows.Add(drwAnswer14);

						// Item #15
						DataRow drwAnswer15 = tblAnswers.NewRow();
						if (rad151.Checked)
							strAnswer = "1";
						else if (rad152.Checked)
							strAnswer = "2";
						else if (rad153.Checked)
							strAnswer = "3";
						else if (rad154.Checked)
							strAnswer = "4";
						else if (rad155.Checked)
							strAnswer = "5";
						else if (rad156.Checked)
							strAnswer = "6";
						else if (rad157.Checked)
							strAnswer = "7";
						drwAnswer15["answer"] = strAnswer;
						drwAnswer15["itemcode"] = "15";
						tblAnswers.Rows.Add(drwAnswer15);

						// Item #16
						DataRow drwAnswer16 = tblAnswers.NewRow();
						if (rad161.Checked)
							strAnswer = "1";
						else if (rad162.Checked)
							strAnswer = "2";
						else if (rad163.Checked)
							strAnswer = "3";
						else if (rad164.Checked)
							strAnswer = "4";
						else if (rad165.Checked)
							strAnswer = "5";
						else if (rad166.Checked)
							strAnswer = "6";
						else if (rad167.Checked)
							strAnswer = "7";
						drwAnswer16["answer"] = strAnswer;
						drwAnswer16["itemcode"] = "16";
						tblAnswers.Rows.Add(drwAnswer16);

						// Item #17
						DataRow drwAnswer17 = tblAnswers.NewRow();
						if (rad171.Checked)
							strAnswer = "1";
						else if (rad172.Checked)
							strAnswer = "2";
						else if (rad173.Checked)
							strAnswer = "3";
						else if (rad174.Checked)
							strAnswer = "4";
						else if (rad175.Checked)
							strAnswer = "5";
						else if (rad176.Checked)
							strAnswer = "6";
						else if (rad177.Checked)
							strAnswer = "7";
						drwAnswer17["answer"] = strAnswer;
						drwAnswer17["itemcode"] = "17";
						tblAnswers.Rows.Add(drwAnswer17);

						// Item #18
						DataRow drwAnswer18 = tblAnswers.NewRow();
						if (rad181.Checked)
							strAnswer = "1";
						else if (rad182.Checked)
							strAnswer = "2";
						else if (rad183.Checked)
							strAnswer = "3";
						else if (rad184.Checked)
							strAnswer = "4";
						else if (rad185.Checked)
							strAnswer = "5";
						else if (rad186.Checked)
							strAnswer = "6";
						else if (rad187.Checked)
							strAnswer = "7";
						drwAnswer18["answer"] = strAnswer;
						drwAnswer18["itemcode"] = "18";
						tblAnswers.Rows.Add(drwAnswer18);

						// Item #19
						DataRow drwAnswer19 = tblAnswers.NewRow();
						if (rad191.Checked)
							strAnswer = "1";
						else if (rad192.Checked)
							strAnswer = "2";
						else if (rad193.Checked)
							strAnswer = "3";
						else if (rad194.Checked)
							strAnswer = "4";
						else if (rad195.Checked)
							strAnswer = "5";
						else if (rad196.Checked)
							strAnswer = "6";
						else if (rad197.Checked)
							strAnswer = "7";
						drwAnswer19["answer"] = strAnswer;
						drwAnswer19["itemcode"] = "19";
						tblAnswers.Rows.Add(drwAnswer19);

						// Item #20
						DataRow drwAnswer20 = tblAnswers.NewRow();
						if (rad201.Checked)
							strAnswer = "1";
						else if (rad202.Checked)
							strAnswer = "2";
						else if (rad203.Checked)
							strAnswer = "3";
						else if (rad204.Checked)
							strAnswer = "4";
						else if (rad205.Checked)
							strAnswer = "5";
						else if (rad206.Checked)
							strAnswer = "6";
						else if (rad207.Checked)
							strAnswer = "7";
						drwAnswer20["answer"] = strAnswer;
						drwAnswer20["itemcode"] = "20";
						tblAnswers.Rows.Add(drwAnswer20);

						// Item #21
						DataRow drwAnswer21 = tblAnswers.NewRow();
						if (rad211.Checked)
							strAnswer = "1";
						else if (rad212.Checked)
							strAnswer = "2";
						else if (rad213.Checked)
							strAnswer = "3";
						else if (rad214.Checked)
							strAnswer = "4";
						else if (rad215.Checked)
							strAnswer = "5";
						else if (rad216.Checked)
							strAnswer = "6";
						else if (rad217.Checked)
							strAnswer = "7";
						drwAnswer21["answer"] = strAnswer;
						drwAnswer21["itemcode"] = "21";
						tblAnswers.Rows.Add(drwAnswer21);

						// Item #22
						DataRow drwAnswer22 = tblAnswers.NewRow();
						if (rad221.Checked)
							strAnswer = "1";
						else if (rad222.Checked)
							strAnswer = "2";
						else if (rad223.Checked)
							strAnswer = "3";
						else if (rad224.Checked)
							strAnswer = "4";
						else if (rad225.Checked)
							strAnswer = "5";
						else if (rad226.Checked)
							strAnswer = "6";
						else if (rad227.Checked)
							strAnswer = "7";
						drwAnswer22["answer"] = strAnswer;
						drwAnswer22["itemcode"] = "22";
						tblAnswers.Rows.Add(drwAnswer22);

						// Item #23
						DataRow drwAnswer23 = tblAnswers.NewRow();
						if (rad231.Checked)
							strAnswer = "1";
						else if (rad232.Checked)
							strAnswer = "2";
						else if (rad233.Checked)
							strAnswer = "3";
						else if (rad234.Checked)
							strAnswer = "4";
						else if (rad235.Checked)
							strAnswer = "5";
						else if (rad236.Checked)
							strAnswer = "6";
						else if (rad237.Checked)
							strAnswer = "7";
						drwAnswer23["answer"] = strAnswer;
						drwAnswer23["itemcode"] = "23";
						tblAnswers.Rows.Add(drwAnswer23);

						// Item #24
						DataRow drwAnswer24 = tblAnswers.NewRow();
						if (rad241.Checked)
							strAnswer = "1";
						else if (rad242.Checked)
							strAnswer = "2";
						else if (rad243.Checked)
							strAnswer = "3";
						else if (rad244.Checked)
							strAnswer = "4";
						else if (rad245.Checked)
							strAnswer = "5";
						else if (rad246.Checked)
							strAnswer = "6";
						else if (rad247.Checked)
							strAnswer = "7";
						drwAnswer24["answer"] = strAnswer;
						drwAnswer24["itemcode"] = "24";
						tblAnswers.Rows.Add(drwAnswer24);

						// Item #25
						DataRow drwAnswer25 = tblAnswers.NewRow();
						if (rad251.Checked)
							strAnswer = "1";
						else if (rad252.Checked)
							strAnswer = "2";
						else if (rad253.Checked)
							strAnswer = "3";
						else if (rad254.Checked)
							strAnswer = "4";
						else if (rad255.Checked)
							strAnswer = "5";
						else if (rad256.Checked)
							strAnswer = "6";
						else if (rad257.Checked)
							strAnswer = "7";
						drwAnswer25["answer"] = strAnswer;
						drwAnswer25["itemcode"] = "25";
						tblAnswers.Rows.Add(drwAnswer25);

						// Item #26
						DataRow drwAnswer26 = tblAnswers.NewRow();
						if (rad261.Checked)
							strAnswer = "1";
						else if (rad262.Checked)
							strAnswer = "2";
						else if (rad263.Checked)
							strAnswer = "3";
						else if (rad264.Checked)
							strAnswer = "4";
						else if (rad265.Checked)
							strAnswer = "5";
						else if (rad266.Checked)
							strAnswer = "6";
						else if (rad267.Checked)
							strAnswer = "7";
						drwAnswer26["answer"] = strAnswer;
						drwAnswer26["itemcode"] = "26";
						tblAnswers.Rows.Add(drwAnswer26);

						// Item #27
						DataRow drwAnswer27 = tblAnswers.NewRow();
						if (rad271.Checked)
							strAnswer = "1";
						else if (rad272.Checked)
							strAnswer = "2";
						else if (rad273.Checked)
							strAnswer = "3";
						else if (rad274.Checked)
							strAnswer = "4";
						else if (rad275.Checked)
							strAnswer = "5";
						else if (rad276.Checked)
							strAnswer = "6";
						else if (rad277.Checked)
							strAnswer = "7";
						drwAnswer27["answer"] = strAnswer;
						drwAnswer27["itemcode"] = "27";
						tblAnswers.Rows.Add(drwAnswer27);

						// Item #28
						DataRow drwAnswer28 = tblAnswers.NewRow();
						if (rad281.Checked)
							strAnswer = "1";
						else if (rad282.Checked)
							strAnswer = "2";
						else if (rad283.Checked)
							strAnswer = "3";
						else if (rad284.Checked)
							strAnswer = "4";
						else if (rad285.Checked)
							strAnswer = "5";
						else if (rad286.Checked)
							strAnswer = "6";
						else if (rad287.Checked)
							strAnswer = "7";
						drwAnswer28["answer"] = strAnswer;
						drwAnswer28["itemcode"] = "28";
						tblAnswers.Rows.Add(drwAnswer28);

						// Item #29
						DataRow drwAnswer29 = tblAnswers.NewRow();
						if (rad291.Checked)
							strAnswer = "1";
						else if (rad292.Checked)
							strAnswer = "2";
						else if (rad293.Checked)
							strAnswer = "3";
						else if (rad294.Checked)
							strAnswer = "4";
						else if (rad295.Checked)
							strAnswer = "5";
						else if (rad296.Checked)
							strAnswer = "6";
						else if (rad297.Checked)
							strAnswer = "7";
						drwAnswer29["answer"] = strAnswer;
						drwAnswer29["itemcode"] = "29";
						tblAnswers.Rows.Add(drwAnswer29);

						// Item #30
						DataRow drwAnswer30 = tblAnswers.NewRow();
						if (rad301.Checked)
							strAnswer = "1";
						else if (rad302.Checked)
							strAnswer = "2";
						else if (rad303.Checked)
							strAnswer = "3";
						else if (rad304.Checked)
							strAnswer = "4";
						else if (rad305.Checked)
							strAnswer = "5";
						else if (rad306.Checked)
							strAnswer = "6";
						else if (rad307.Checked)
							strAnswer = "7";
						drwAnswer30["answer"] = strAnswer;
						drwAnswer30["itemcode"] = "30";
						tblAnswers.Rows.Add(drwAnswer30);

						// Item #31
						DataRow drwAnswer31 = tblAnswers.NewRow();
						if (rad311.Checked)
							strAnswer = "1";
						else if (rad312.Checked)
							strAnswer = "2";
						else if (rad313.Checked)
							strAnswer = "3";
						else if (rad314.Checked)
							strAnswer = "4";
						else if (rad315.Checked)
							strAnswer = "5";
						else if (rad316.Checked)
							strAnswer = "6";
						else if (rad317.Checked)
							strAnswer = "7";
						drwAnswer31["answer"] = strAnswer;
						drwAnswer31["itemcode"] = "31";
						tblAnswers.Rows.Add(drwAnswer31);

						// Item #32
						DataRow drwAnswer32 = tblAnswers.NewRow();
						if (rad321.Checked)
							strAnswer = "1";
						else if (rad322.Checked)
							strAnswer = "2";
						else if (rad323.Checked)
							strAnswer = "3";
						else if (rad324.Checked)
							strAnswer = "4";
						else if (rad325.Checked)
							strAnswer = "5";
						else if (rad326.Checked)
							strAnswer = "6";
						else if (rad327.Checked)
							strAnswer = "7";
						drwAnswer32["answer"] = strAnswer;
						drwAnswer32["itemcode"] = "32";
						tblAnswers.Rows.Add(drwAnswer32);

						// Item #33
						DataRow drwAnswer33 = tblAnswers.NewRow();
						if (rad331.Checked)
							strAnswer = "1";
						else if (rad332.Checked)
							strAnswer = "2";
						else if (rad333.Checked)
							strAnswer = "3";
						else if (rad334.Checked)
							strAnswer = "4";
						else if (rad335.Checked)
							strAnswer = "5";
						else if (rad336.Checked)
							strAnswer = "6";
						else if (rad337.Checked)
							strAnswer = "7";
						drwAnswer33["answer"] = strAnswer;
						drwAnswer33["itemcode"] = "33";
						tblAnswers.Rows.Add(drwAnswer33);

						// Item #34
						DataRow drwAnswer34 = tblAnswers.NewRow();
						if (rad341.Checked)
							strAnswer = "1";
						else if (rad342.Checked)
							strAnswer = "2";
						else if (rad343.Checked)
							strAnswer = "3";
						else if (rad344.Checked)
							strAnswer = "4";
						else if (rad345.Checked)
							strAnswer = "5";
						else if (rad346.Checked)
							strAnswer = "6";
						else if (rad347.Checked)
							strAnswer = "7";
						drwAnswer34["answer"] = strAnswer;
						drwAnswer34["itemcode"] = "34";
						tblAnswers.Rows.Add(drwAnswer34);

						// Item #35
						DataRow drwAnswer35 = tblAnswers.NewRow();
						if (rad351.Checked)
							strAnswer = "1";
						else if (rad352.Checked)
							strAnswer = "2";
						else if (rad353.Checked)
							strAnswer = "3";
						else if (rad354.Checked)
							strAnswer = "4";
						else if (rad355.Checked)
							strAnswer = "5";
						else if (rad356.Checked)
							strAnswer = "6";
						else if (rad357.Checked)
							strAnswer = "7";
						drwAnswer35["answer"] = strAnswer;
						drwAnswer35["itemcode"] = "35";
						tblAnswers.Rows.Add(drwAnswer35);

						Session["answers"] = tblAnswers;
						Response.Redirect("survey02.aspx");
					}
				}
				else
				{
					trError.Visible = true;
				}
	}

	protected void btnValidate_Click(object sender, ImageClickEventArgs e)
	{
		string strUsername = GetUsername(txtEmpNum.Text);
		if (IsMiddleNameValid(strUsername, txtMidName.Text))
		{
			if (IsAnswered(strUsername))
			{
				lblMessage.Text = "You already casted your vote.";
				trVote.Visible = false;
				trError.Visible = true;
			}
			else
			{
				trVote.Visible = true;
				trError.Visible = false;
			}
		}
		else
		{
			lblMessage.Text = "Invalid Middle Name.";
			trVote.Visible = false;
			trError.Visible = true;
		}
	}
}
