using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STIeForms;
using HRMS;

public partial class CIS_EFormsMain : System.Web.UI.Page
{

 protected void Page_Load(object sender, EventArgs e)
 {
  lblMRCFCount.Text = clsMRCF.CountRecords().ToString();
  lblRequCount.Text = clsRequisition.CountRecords().ToString();
  lblTranCount.Text = clsTransmittal.CountRecords().ToString();
  lblLeaveCount.Text = clsLeave.GetTotalRecords().ToString();
  lblUnderCount.Text = clsUndertime.GetTotalRecords().ToString();
  lblOBCount.Text = clsOB.GetTotalRecords().ToString();
  lblOverCount.Text = clsOvertime.GetTotalRecords().ToString();
 }

}