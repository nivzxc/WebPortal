using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HRMS;
using STIeForms;
using Microsoft.VisualBasic;

public partial class Synergy_Survey01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
           
            if (SynergySurvey.AlreadyAnsweredSurvey(Request.Cookies["Speedo"]["Username"]))
            {
                Response.Redirect("SurveyThanks.aspx");
            }

            if (SynergySurvey.IsSurveyEnd())
            {
                Response.Redirect("SurveyEnd.aspx");
            }
        
        }
    }

    protected bool IsCorrect()
    {
        bool blnReturn = false;
        int intCheckBoxChecked = 0;
        string strErrorMessage = "";
        divError.Visible = false;


        if (chbx2.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx4.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx6.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx8.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx10.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx12.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx14.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx16.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx18.Checked == true)
        {
            intCheckBoxChecked += 1;
        }


        if (chbx20.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx22.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx24.Checked == true)
        {
            intCheckBoxChecked += 1;
        }

        if (chbx26.Checked == true)
        {
            intCheckBoxChecked += 1;
        }


        if (intCheckBoxChecked > 5)
        {
            strErrorMessage += "<br>Preferred must be maximum of five(5) items.";
        }


        if (strErrorMessage.Length == 0)
        {
            blnReturn = true;
        }
        else
        {
            divError.Visible = true;
            lblError.Text = strErrorMessage;
        }
        return blnReturn;
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (IsCorrect())
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            DataTable tblUserAnswer = new DataTable();
            tblUserAnswer.Columns.Add("username");
            tblUserAnswer.Columns.Add("ItemCode");
            tblUserAnswer.Columns.Add("ItemAnswer");



            //Item Number 1

            if (chbx1.Checked == true)
            { 
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx2.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }


            //Item Number 2
            if (chbx3.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx4.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 3
            if (chbx5.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx6.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 4
            if (chbx7.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx8.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 5
            if (chbx9.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx10.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 6
            if (chbx11.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx12.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 7
            if (chbx13.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx14.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 8
            if (chbx15.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx16.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 9
            if (chbx17.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx18.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 10
            if (chbx19.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx20.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 11
            if (chbx21.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx22.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 12
            if (chbx23.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx24.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 13
            if (chbx25.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx26.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
           
            using (SynergySurvey objSave = new SynergySurvey())
            {
                int intCounter = 0;
                foreach (DataRow dr in tblUserAnswer.Rows)
                {
                    intCounter = intCounter + 1;
                }
                if (intCounter ==0)
                {
                    divConfirmation.Visible = true;
                    lblConfirmation.Text = "No item has been selected. <br /><br /> Are you sure?";
                    btnSaves.Visible = false;
                }
                else
                {
                    if (objSave.Insert(tblUserAnswer, txtOthers.Text, strUsername) > 0)
                    {
                        Response.Redirect("SurveyThanks.aspx");
                    }
                    else
                    {
                        divError.Visible = true;
                        lblError.Text = "Error has occured";
                    }
                }
            }
        }
    }

    protected void chbx1_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx1.Checked == true)
        {
            chbx2.Enabled = true;        
        }
        else
        {
            chbx2.Checked = false;
            chbx2.Enabled = false;
        }
    }
    protected void chbx3_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx3.Checked == true)
        {
            chbx4.Enabled = true;
        }
        else
        {
            chbx4.Checked = false;
            chbx4.Enabled = false;
        }
    }
    protected void chbx5_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx5.Checked == true)
        {
            chbx6.Enabled = true;
        }
        else
        {
            chbx6.Checked = false;
            chbx6.Enabled = false;
        }
    }
    protected void chbx7_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx7.Checked == true)
        {
            chbx8.Enabled = true;
        }
        else
        {
            chbx8.Checked = false;
            chbx8.Enabled = false;
        }
    }
    protected void chbx9_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx9.Checked == true)
        {
            chbx10.Enabled = true;
        }
        else
        {
            chbx10.Checked = false;
            chbx10.Enabled = false;
        }
    }
    protected void chbx11_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx11.Checked == true)
        {
            chbx12.Enabled = true;
        }
        else
        {
            chbx12.Checked = false;
            chbx12.Enabled = false;
        }
    }
    protected void chbx13_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx13.Checked == true)
        {
            chbx14.Enabled = true;
        }
        else
        {
            chbx14.Checked = false;
            chbx14.Enabled = false;
        }
    }
    protected void chbx15_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx15.Checked == true)
        {
            chbx16.Enabled = true;
        }
        else
        {
            chbx16.Checked = false;
            chbx16.Enabled = false;
        }
    }
    protected void chbx17_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx17.Checked == true)
        {
            chbx18.Enabled = true;
        }
        else
        {
            chbx18.Checked = false;
            chbx18.Enabled = false;
        }
    }
    protected void chbx19_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx19.Checked == true)
        {
            chbx20.Enabled = true;
        }
        else
        {
            chbx20.Checked = false;
            chbx20.Enabled = false;
        }
    }
    protected void chbx21_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx21.Checked == true)
        {
            chbx22.Enabled = true;
        }
        else
        {
            chbx22.Checked = false;
            chbx22.Enabled = false;
        }
    }
    protected void chbx23_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx23.Checked == true)
        {
            chbx24.Enabled = true;
        }
        else
        {
            chbx24.Checked = false;
            chbx24.Enabled = false;
        }
    }
    protected void chbx25_CheckedChanged(object sender, EventArgs e)
    {
        if (chbx25.Checked == true)
        {
            chbx26.Enabled = true;
        }
        else
        {
            chbx26.Checked = false;
            chbx26.Enabled = false;
        }
    }

    protected void btnSaves_Click(object sender, EventArgs e)
    {
        if (IsCorrect())
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            DataTable tblUserAnswer = new DataTable();
            tblUserAnswer.Columns.Add("username");
            tblUserAnswer.Columns.Add("ItemCode");
            tblUserAnswer.Columns.Add("ItemAnswer");



            //Item Number 1

            if (chbx1.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx2.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }


            //Item Number 2
            if (chbx3.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx4.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 3
            if (chbx5.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx6.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 4
            if (chbx7.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx8.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 5
            if (chbx9.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx10.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 6
            if (chbx11.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx12.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 7
            if (chbx13.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx14.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 8
            if (chbx15.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx16.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 9
            if (chbx17.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx18.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 10
            if (chbx19.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx20.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 11
            if (chbx21.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx22.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 12
            if (chbx23.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx24.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 13
            if (chbx25.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx26.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }

            using (SynergySurvey objSave = new SynergySurvey())
            {
                int intCounter = 0;
                foreach (DataRow dr in tblUserAnswer.Rows)
                {
                    intCounter = intCounter + 1;
                }
                if (intCounter == 0)
                {
                    divConfirmation.Visible = true;
                    lblConfirmation.Text = "No item has been selected. <br /><br /> Are you sure?";
                    btnSaves.Visible = false;
                }
                else
                {
                    if (objSave.Insert(tblUserAnswer, txtOthers.Text, strUsername) > 0)
                    {
                        Response.Redirect("SurveyThanks.aspx");
                    }
                    else
                    {
                        divError.Visible = true;
                        lblError.Text = "Error has occured";
                    }
                }
            }
        }
    }
    protected void btnYess_Click(object sender, EventArgs e)
    {
        if (IsCorrect())
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            DataTable tblUserAnswer = new DataTable();
            tblUserAnswer.Columns.Add("username");
            tblUserAnswer.Columns.Add("ItemCode");
            tblUserAnswer.Columns.Add("ItemAnswer");



            //Item Number 1

            if (chbx1.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx2.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "1";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }


            //Item Number 2
            if (chbx3.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx4.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "2";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 3
            if (chbx5.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx6.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "3";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 4
            if (chbx7.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx8.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "4";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 5
            if (chbx9.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx10.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "5";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 6
            if (chbx11.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx12.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "6";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 7
            if (chbx13.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx14.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "7";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 8
            if (chbx15.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx16.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "8";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 9
            if (chbx17.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx18.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "9";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 10
            if (chbx19.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx20.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "10";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 11
            if (chbx21.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx22.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "11";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 12
            if (chbx23.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx24.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "12";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }
            //Item Number 13
            if (chbx25.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "Y";
                tblUserAnswer.Rows.Add(drNewY);
            }

            if (chbx26.Checked == true)
            {
                DataRow drNewY = tblUserAnswer.NewRow();
                drNewY["username"] = strUsername;
                drNewY["ItemCode"] = "13";
                drNewY["ItemAnswer"] = "P";
                tblUserAnswer.Rows.Add(drNewY);
            }

            using (SynergySurvey objSave = new SynergySurvey())
            {
                int intCounter = 0;
                foreach (DataRow dr in tblUserAnswer.Rows)
                {
                    intCounter = intCounter + 1;
                }

                if (intCounter == 0)
                {
                    DataRow drNewY = tblUserAnswer.NewRow();
                    drNewY["username"] = strUsername;
                    drNewY["ItemCode"] = "99";
                    drNewY["ItemAnswer"] = "N";
                    tblUserAnswer.Rows.Add(drNewY);
                }

                if (objSave.Insert(tblUserAnswer, txtOthers.Text, strUsername) > 0)
                {
                    Response.Redirect("SurveyThanks.aspx");
                }
                else
                {
                    divError.Visible = true;
                    lblError.Text = "Error has occured";
                }
            }
        }
    }


    protected void btnNo_Click(object sender, EventArgs e)
    {
        divConfirmation.Visible = false;
        btnSaves.Visible = true;

        Response.Redirect("Survey01.aspx");
    }

}