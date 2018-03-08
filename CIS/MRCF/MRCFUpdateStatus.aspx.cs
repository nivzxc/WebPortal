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
using HRMS;
using STIeForms;
using Oracles;

public partial class CIS_MRCF_MRCFUpdateStatus : System.Web.UI.Page
{

   public void BindAssignStatus()
    {
        DataTable tblAssignStatus = clsMRCF.DDLAssignStatus();
        ddlAssignStatus.DataSource = tblAssignStatus;
        ddlAssignStatus.DataValueField = "pvalue";
        ddlAssignStatus.DataTextField = "ptext";
        ddlAssignStatus.DataBind();
    }

    public void BindEmployee()
    {
        DataTable tblEmployeesAssign = clsMRCF.DDLEmployeeRemoveCurrent("PROC", "1", Request.Cookies["Speedo"]["UserName"].ToString());
        ddlReassign.DataSource = tblEmployeesAssign;
        ddlReassign.DataValueField = "pvalue";
        ddlReassign.DataTextField = "ptext";
        ddlReassign.DataBind();
    }

    protected void BindItems()
    {
        DataTable tblItems = clsMRCF.GetMrcfItems(Request.QueryString["mrcfcode"]);
        DataTable tblNewItems = new DataTable();
        tblNewItems.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("itemspec", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("GLAccount", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("LineType", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("TransactionType", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("Destination", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("Item", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("qty", System.Type.GetType("System.Int32"));
        tblNewItems.Columns.Add("unit", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("dateneed", System.Type.GetType("System.DateTime"));

        tblNewItems.Columns.Add("empname", System.Type.GetType("System.String"));
        tblNewItems.Columns.Add("birthdate", System.Type.GetType("System.String"));

        foreach (DataRow dr in tblItems.Rows)
        {
            DataRow drowCart = tblNewItems.NewRow();
            drowCart["itemdesc"] = dr["itemdesc"].ToString();
            drowCart["itemspec"] = dr["itemspec"].ToString();
            drowCart["GLAccount"] = dr["GLAccount"].ToString();

            drowCart["LineType"] = dr["linetypecode"].ToString();
            drowCart["TransactionType"] = dr["TypeCode"].ToString();
            drowCart["empname"] = dr["empname"].ToString();
            drowCart["birthdate"] = dr["birthdate"].ToString();

            drowCart["Destination"] = dr["Destination"].ToString();
            drowCart["Item"] = dr["itemcode"].ToString();
            drowCart["qty"] = dr["qty"].ToString();
            drowCart["unit"] = dr["unit"].ToString();
            drowCart["dateneed"] = DateTime.Parse(dr["dateneed"].ToString()).ToShortDateString();


            tblNewItems.Rows.Add(drowCart);

        }

        ViewState["Cart"] = tblNewItems;
        dgItems.DataSource = tblNewItems;
        dgItems.DataBind();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        if (!Page.IsPostBack)
        {
            BindItems();
            BindEmployee();
            BindAssignStatus();
            string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnUpdate.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnUpdate, "").ToString());

            ddlType.DataSource = clsMRCF.GetDDLSourceMrcfRequestType().DefaultView;
            ddlType.DataValueField = "pValue";
            ddlType.DataTextField = "pText";
            ddlType.DataBind();

            if (Request.QueryString["updatecode"] != null)
            {
                if (Request.QueryString["updatecode"].ToString() == "000") { DivUpdateStatus.Visible = false; divModal.Visible = false; }
                else { DivUpdateStatus.Visible = true; DivUpdateStatus.Visible = true; }
            }

           // clsMRCF.AuthenticateUser(clsMRCF.MRCFUserType.Requestor, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["mrcfcode"].ToString());
            bool blnReadOnly = false;
            DataTable tblAsset = new DataTable();
            DataTable tblRequestType = new DataTable();

            txtMRCFCode.Text = Request.QueryString["mrcfcode"].ToString();
            txtRequestorName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"].ToString());

            clsMRCF mrcf = new clsMRCF(txtMRCFCode.Text);
            mrcf.Fill();
            txtIntended.Text = mrcf.Intended;
            hdnChargeTo.Value = mrcf.ChargeTo;
            txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);
            ddlType.SelectedValue = mrcf.RequestType;
            txtDateReq.Text = mrcf.DateRequested.ToString("MMMM dd, yyyy");
            hdnGrpHeadCode.Value = mrcf.GroupHead;
            txtGrpHeadRem.Text = mrcf.GroupHeadRemarks;
            hdnDiviHeadCode.Value = mrcf.DivisionHead;
            txtDiviHeadRem.Text = mrcf.DivisionHeadRemarks;
            hdnProcMngrCode.Value = mrcf.ProcurementManager;
            txtProcMngrRem.Text = mrcf.ProcurementManagerRemarks;
            hdnStatus.Value = mrcf.Status;
            txtStat.Text = mrcf.StatusDescription;
            blnReadOnly = (mrcf.Status == "M" ? false : true);

            using (clsUsers users = new clsUsers())
            {
                users.Username = hdnGrpHeadCode.Value;
                users.Fill();
                txtGrpHeadName.Text = users.FullName;
                hdnGrpHeadMail.Value = users.Email;

                users.Username = hdnDiviHeadCode.Value;
                users.Fill();
                txtDiviHeadName.Text = users.FullName;
                hdnDiviHeadMail.Value = users.Email;

                users.Username = hdnProcMngrCode.Value;
                users.Fill();
                txtProcMngrName.Text = users.FullName;
                hdnProcMngrMail.Value = users.Email;
            }
    

        }
    }

   

    protected void UpdateAssignedMRCF()
    {
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            //         cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode = '" + Request.QueryString["mrcfcode"] + "') AND isactive = '1' ORDER BY hdlrcode ASC";
            cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode = '" + Request.QueryString["mrcfcode"] + "') AND isactive = '1' ORDER BY hdlrcode ASC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strWrite = strWrite +
                       "<a href='MRCFDetailsDH.aspx?mrcfcode=" + dr["mrcfcode"] + "' style='font-size:small;'>" + dr["IntendedFor"] + "</a><br>" +
                       "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
                       "Date Assigned: " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy") + "<br>" +
                       "Remarks: " + dr["Remarks"];
            }
            dr.Close();
        }

        Response.Write(strWrite);
    }


    protected void LoadCurrentStatus()
    {
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + Request.QueryString["mrcfcode"] + "' AND isactive = '1') AND isactive = '1' ORDER BY hdlrcode ASC";
            cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT TOP 1 hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + Request.QueryString["mrcfcode"] + "' AND isactive = '1' ORDER BY hdlrcode DESC) AND isactive = '1' ORDER BY hdlrcode ASC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strStatus = "";
                strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
                strWrite = strWrite +

                                dr["StatusDescription"].ToString() +
                           strStatus;

            }
            dr.Close();
        }

        Response.Write(strWrite);
    }



    protected void LoadCurrentHandler()
    {
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + Request.QueryString["mrcfcode"] + "' AND isactive = '1') AND isactive = '1' ORDER BY hdlrcode ASC";
            cmd.CommandText = "SELECT (SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = (SELECT TOP 1 hdlrcode FROM CIS.MrcfAssign WHERE mrcfcode ='" + Request.QueryString["mrcfcode"] + "' AND isactive = '1' ORDER BY hdlrcode DESC) AND isactive = '1' ORDER BY hdlrcode ASC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlAssignStatus.SelectedValue = dr["statcode"].ToString();
                strWrite = strWrite +

                                dr["Assignto"].ToString();
                           
            }
            dr.Close();
        }

        Response.Write(strWrite);
    }

    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        clsMRCFAssign objAssign = new clsMRCFAssign();
        objAssign.HandlerCode = objAssign.GetHandlerCode(Request.QueryString["mrcfcode"].ToString());
        objAssign.AssignBy = objAssign.GetMRCFAssignedBy(Request.QueryString["mrcfcode"].ToString()); //objAssign.GetProcurementManager("PROCMNGR");
        objAssign.CreateBy = Request.Cookies["Speedo"]["UserName"].ToString();
        objAssign.IsActive = "1"; //1 = Active , 0 = Inactive
        objAssign.AssignTo = objAssign.GetMRCFAssignedto(Request.QueryString["mrcfcode"].ToString());
        objAssign.Remarks = txtAssignRemarks.Text;
        objAssign.StatusCode = ddlAssignStatus.SelectedValue.ToString();
        objAssign.AssignEmployeeDetails("Update Status");
        Response.Redirect("MRCFMenu.aspx");
    }
    protected void btnReAssign_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void btnUpdateReassign_Click(object sender, EventArgs e)
    {
        clsMRCFAssign objAssign = new clsMRCFAssign();
        objAssign.HandlerCode = objAssign.GetHandlerCode(Request.QueryString["mrcfcode"].ToString());
        objAssign.AssignBy = Request.Cookies["Speedo"]["UserName"].ToString();//objAssign.GetProcurementManager("PROCMNGR");
        objAssign.CreateBy = Request.Cookies["Speedo"]["UserName"].ToString();
        objAssign.IsActive = "1"; //1 = Active , 0 = Inactive
        objAssign.AssignTo = ddlReassign.SelectedValue.ToString();
        objAssign.Remarks = txtReassignRemarks.Text;
        objAssign.StatusCode = objAssign.LoadCurrentStatus(Request.QueryString["mrcfcode"]);
        objAssign.AssignEmployeeDetails("Reassign");
        Response.Redirect("MRCFMenu.aspx");
    }

    protected void lbtnHide_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
}
