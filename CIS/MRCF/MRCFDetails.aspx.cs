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

public partial class CIS_MRCF_MRCFDetails : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemspec", System.Type.GetType("System.String"));
        tblCart.Columns.Add("GLAccount", System.Type.GetType("System.String"));
        tblCart.Columns.Add("LineType", System.Type.GetType("System.String"));
        tblCart.Columns.Add("TransactionType", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Destination", System.Type.GetType("System.String"));
        tblCart.Columns.Add("Item", System.Type.GetType("System.String"));
        tblCart.Columns.Add("qty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("unit", System.Type.GetType("System.String"));
        tblCart.Columns.Add("dateneed", System.Type.GetType("System.DateTime"));

        

        ViewState["Cart"] = tblCart;
    }

	protected void SpecificationVisibility(bool pShow)
	{
		foreach (DataGridItem itm in dgItems.Items)
		{
			TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
			ptxtItemSpec.Visible = pShow;
		}
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

    //protected void LoadUnit(string strItemCode)
    //{
    //    ddlUnit.DataSource = clsOracleMrcf.GetMrcfUnit(strItemCode);
    //    ddlUnit.DataBind();
    //}

    protected void LoadUnit()
    {
        //string strPrimaryUOM = clsOracleMrcf.GetPrimaryUOM(ddlItem.SelectedValue.ToString());
        //string strUOMClass = clsOracleMrcf.GetUOMClass(strPrimaryUOM);
        ddlUnit.DataSource = clsOracleMrcf.GetDSLUOM(ddlItem.SelectedValue.ToString());
        ddlUnit.DataTextField = "pText";
        ddlUnit.DataValueField = "pValue";
        ddlUnit.DataBind();
    }

    private void LoadUnitAll()
    {
        ddlUnit.DataSource = clsOracleMrcf.GetMrcfUnitAll();
        ddlUnit.DataTextField = "pText";
        ddlUnit.DataValueField = "pValue";
        ddlUnit.DataBind();
    }

    protected void LoadPrimaryItemCategory()
    {
        DataTable tblPrimaryItemCat = new DataTable();
        tblPrimaryItemCat.Columns.Add("pText");
        tblPrimaryItemCat.Columns.Add("pValue");

        DataRow drPrimaryItemCategory = tblPrimaryItemCat.NewRow();
        drPrimaryItemCategory["pText"] = "ALL";
        drPrimaryItemCategory["pValue"] = "ALL";
        tblPrimaryItemCat.Rows.Add(drPrimaryItemCategory);

        foreach (DataRow dr in clsRequisitonItemCategory.GetDSLReq().Rows)
        {
            drPrimaryItemCategory = tblPrimaryItemCat.NewRow();
            drPrimaryItemCategory["pText"] = dr["pText"].ToString();
            drPrimaryItemCategory["pValue"] = dr["pValue"].ToString();
            tblPrimaryItemCat.Rows.Add(drPrimaryItemCategory);
        }

        ddlItemCategory.DataSource = tblPrimaryItemCat;
        ddlItemCategory.DataTextField = "pText";
        ddlItemCategory.DataValueField = "pValue";
        ddlItemCategory.DataBind();
    }

	protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();
		if (!Page.IsPostBack)
		{
            string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnSavePost.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSavePost, "").ToString());
            
			clsMRCF.AuthenticateUser(clsMRCF.MRCFUserType.Requestor, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["mrcfcode"].ToString());
            MakeCart();
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

   ddlLineType.DataSource = clsMrcfLineType.GetDataSourceListLineType(clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"])).DefaultView;
   ddlLineType.DataBind();
   ddlLineType.Items[0].Selected = true;

   if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
   {
       trItems.Visible = true;
       if (ddlLineType.SelectedValue.ToString() == "1")
       {
           trItemsCategory.Visible = false;
       }
       else
       {
           trItemsCategory.Visible = true;
       }
   }
   else
   {
       trItems.Visible = false;
   }

   LoadPrimaryItemCategory();

   ddlTransactionType.DataSource = clsMRCFTransactionType.GetDataSourceList(ddlLineType.SelectedValue.ToString()).DefaultView;
   ddlTransactionType.DataBind();
   ddlTransactionType.Items[0].Selected = true;


   ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString());
   ddlItem.DataBind();

   LoadUnitAll();

   ddlType.DataSource = clsMRCF.GetDDLSourceMrcfRequestType().DefaultView;
   ddlType.DataValueField = "pValue";
   ddlType.DataTextField = "pText";
   ddlType.DataBind();


			DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
			dteDateNeeded.MinDate = dteDate;
			dteDateNeeded.Date = dteDate;

			BindItems();

			pnlAddItem.Visible = !blnReadOnly;
			divButtons.Visible = !blnReadOnly;
			divButtons2.Visible = !blnReadOnly;
			ddlType.Enabled = !blnReadOnly;
			txtIntended.ReadOnly = blnReadOnly;
            
			if (blnReadOnly)
			{
				foreach (DataGridItem itm in dgItems.Items)
				{
					TextBox ptxtItemDesc = (TextBox)itm.FindControl("txtItemDesc");
					TextBox ptxtItemSpec = (TextBox)itm.FindControl("txtItemSpec");
					Label ptxtQty = (Label)itm.FindControl("lblQty");
					GrayMatterSoft.GMDatePicker pdteDNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDNeeded");
					ptxtItemDesc.ReadOnly = blnReadOnly;
					ptxtItemDesc.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					ptxtItemSpec.ReadOnly = blnReadOnly;
					ptxtItemSpec.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					ptxtQty.BackColor = System.Drawing.Color.FromName("#f0f8ff");
					pdteDNeeded.Enabled = false;
					pdteDNeeded.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				}
				//dgItems.Columns[4].Visible = false;
				//txtIntended.BackColor = System.Drawing.Color.FromName("#f0f8ff");
				//ddlType.BackColor = System.Drawing.Color.FromName("#f0f8ff");
			}
			else
			{
				foreach (DataGridItem itm in dgItems.Items)
				{
					GrayMatterSoft.GMDatePicker pdteDNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDNeeded");
					pdteDNeeded.MinDate = pdteDNeeded.Date;
				}
			}
		}
 }

    protected void btnSavePost_Click(object sender, EventArgs e)
	{
		string strApproverName = "";
		string strApproverMail = "";
		clsMRCF.MRCFMailType mmtApprover = clsMRCF.MRCFMailType.SentToApproverGH;

		using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
		{
			SqlCommand cmd = cn.CreateCommand();

			// Check if the requestor is the division head
			if (Request.Cookies["Speedo"]["UserName"].ToString() == hdnDiviHeadCode.Value)
			{
				strApproverName = txtProcMngrName.Text;
				strApproverMail = hdnProcMngrMail.Value;
				mmtApprover = clsMRCF.MRCFMailType.SentToApproverPM;
				cmd.CommandText = "UPDATE CIS.Mrcf SET status='F',sprvstat='X',headstat='A',procstat='F',reqtype='" + ddlType.SelectedValue + "',intended=@intended WHERE mrcfcode='" + txtMRCFCode.Text + "'";
			}
			// Check if the requestor is the group approver
			else if (Request.Cookies["Speedo"]["UserName"].ToString() == hdnGrpHeadCode.Value)
			{
				// If the approval of the division head is required
				if (clsMRCF.IsHeadApprovalRequired(hdnChargeTo.Value, hdnDiviHeadCode.Value))
				{
					strApproverName = txtDiviHeadName.Text;
					strApproverMail = hdnDiviHeadMail.Value;
					mmtApprover = clsMRCF.MRCFMailType.SentToApproverDH;
					cmd.CommandText = "UPDATE CIS.Mrcf SET status='F',sprvstat='A',headstat='F',procstat='F',reqtype='" + ddlType.SelectedValue + "',intended=@intended WHERE mrcfcode='" + txtMRCFCode.Text + "'";
				}
					// The approval of division head is not required
				else
				{
					strApproverName = txtProcMngrName.Text;
					strApproverMail = hdnProcMngrMail.Value;
					mmtApprover = clsMRCF.MRCFMailType.SentToApproverPM;
					cmd.CommandText = "UPDATE CIS.Mrcf SET status='F',sprvstat='A',headstat='N',procstat='F',reqtype='" + ddlType.SelectedValue + "',intended=@intended WHERE mrcfcode='" + txtMRCFCode.Text + "'";
				}
			}
			// The requestor is an ordinary user
			else
			{
				// The division head is the only approver
				if (hdnGrpHeadCode.Value == "")
				{
					strApproverName = txtDiviHeadName.Text;
					strApproverMail = hdnDiviHeadMail.Value;
					mmtApprover = clsMRCF.MRCFMailType.SentToApproverDH;
					cmd.CommandText = "UPDATE CIS.Mrcf SET status='F',sprvstat='X',headstat='F',procstat='F',reqtype='" + ddlType.SelectedValue + "',intended=@intended WHERE mrcfcode='" + txtMRCFCode.Text + "'";
				}
				// Ordinary MRCF
				else
				{
					strApproverName = txtGrpHeadName.Text;
					strApproverMail = hdnGrpHeadMail.Value;
					mmtApprover = clsMRCF.MRCFMailType.SentToApproverGH;
					cmd.CommandText = "UPDATE CIS.Mrcf SET status='F',sprvstat='F',headstat='F',procstat='F',reqtype='" + ddlType.SelectedValue + "',intended=@intended WHERE mrcfcode='" + txtMRCFCode.Text + "'";
				}
			}

			cmd.Parameters.Add("@intended", SqlDbType.VarChar, 100);
			cmd.Parameters["@intended"].Value = txtIntended.Text;
			cn.Open();
			cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();

            cmd.CommandText = "DELETE FROM CIS.MrcfDetails WHERE mrcfcode=@mrcfcode";
            cmd.Parameters.Add(new SqlParameter("@mrcfcode", txtMRCFCode.Text));
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            DataTable tblCart = ViewState["Cart"] as DataTable;
            foreach (DataRow dr in tblCart.Rows)
            {
                string strItemDescription = "";
                //Item Description
                if (clsMrcfLineType.IsHasItemCode(dr["LineType"].ToString()) == true)
                {
                    strItemDescription = "";
                }
                else
                {
                    strItemDescription = dr["itemdesc"].ToString();
                }

                cmd.CommandText = "INSERT INTO CIS.MrcfDetails(mrcfcode,itemdesc,itemspec,asstcode, ltypcode, itemcode, qty,unit,dateneed,status,GLAccount,Destination) VALUES(@mrcfcode,@itemdesc,@itemspec,@asstcode, @ltypcode, @itemcode, @qty,@unit,@dateneed,'F',@GLAccount,@Destination)";
                cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 100);
                cmd.Parameters.Add("@itemspec", SqlDbType.VarChar, 5000);
                cmd.Parameters["@itemdesc"].Value = strItemDescription;
                cmd.Parameters["@itemspec"].Value = dr["itemspec"].ToString();
                cmd.Parameters.Add(new SqlParameter("@mrcfcode", txtMRCFCode.Text));
                cmd.Parameters.Add(new SqlParameter("@GLAccount", dr["GLAccount"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@Destination", dr["Destination"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@ltypcode", dr["LineType"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@asstcode", dr["TransactionType"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@itemcode", dr["Item"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@qty", dr["qty"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@unit", dr["unit"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@dateneed", dr["dateneed"].ToString()));


                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
		}

		clsMRCF.SendNotification(mmtApprover, txtRequestorName.Text, strApproverName, strApproverMail, txtMRCFCode.Text);
  clsMRCF.SendNotification(clsMRCF.MRCFMailType.SentToRequestor, txtRequestorName.Text, strApproverName, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtMRCFCode.Text);

		Response.Redirect("MRCFMenu.aspx");
	}

    protected void btnReset_Click(object sender, EventArgs e)
	{
		Response.Redirect("MRCFDetails.aspx?mrcfcode=" + Request.QueryString["mrcfcode"]);
	}

    protected void btnVoid_Click(object sender, EventArgs e)
	{
		clsMRCF mrcf = new clsMRCF(Request.QueryString["mrcfcode"].ToString());
		mrcf.Void();

		Response.Redirect("MRCFMenu.aspx");
	}

    //protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
    //{
    //    string strErrMsg = "";

    //    try
    //    {
    //        if (dteDateNeeded.Date < DateTime.Now)
    //            strErrMsg = "<u>Date needed</u> field should be greater than the current date.";
    //    }
    //    catch
    //    {
    //        strErrMsg = "Invalid date supplied for <u>date needed</u>";
    //    }

    //    if (strErrMsg == "")
    //    {
    //        clsMRCF mrcf = new clsMRCF(txtMRCFCode.Text);
    //        mrcf.AddItem(txtItem.Text, txtSpec.Text, ddlAsset.SelectedValue, Convert.ToInt32(txtQty.Text), ddlUnit.SelectedValue, dteDateNeeded.Date, "0");
    //        Response.Redirect("MRCFDetails.aspx?mrcfcode=" + Request.QueryString["mrcfcode"]);
    //    }
    //    else
    //    {
    //        divError.Visible = true;
    //        lblErrMsg.Text = strErrMsg;
    //    }
    //}

    //protected void ddlAsset_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadUnit(ddlAsset.SelectedValue);
    //}

	protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
	{
		if (hdnStatus.Value == "P" || hdnStatus.Value == "M")
		{
			if (dgItems.Items.Count > 1)
			{
                DataTable tblCart = ViewState["Cart"] as DataTable;
                tblCart.Rows[e.Item.ItemIndex].Delete();
                ViewState["Cart"] = tblCart;

                dgItems.DataSource = tblCart;
                dgItems.DataBind();
				divError.Visible = false;
			}
			else
			{
				lblErrMsg.Text = "&nbsp;! Cannot delete this item.<br>";
				divError.Visible = true;
			}
		}
	}

	protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
	{
		DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
		dteDateNeeded.MinDate = dteDate;
		dteDateNeeded.Date = dteDate;
		foreach (DataGridItem itm in dgItems.Items)
		{
			GrayMatterSoft.GMDatePicker pdteDNeeded = (GrayMatterSoft.GMDatePicker)itm.FindControl("dteDNeeded");
			pdteDNeeded.MinDate = dteDate;
			pdteDNeeded.Date = dteDate;
		}
	}

    protected void ddlLineType_SelectedIndexChanged(object sender, EventArgs e)
    {
        trItemsCategory.Visible = false;
        ddlTransactionType.DataSource = clsMRCFTransactionType.GetDataSourceList(ddlLineType.SelectedValue.ToString());
        ddlTransactionType.DataBind();

        if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
        {
            if (ddlLineType.SelectedValue.ToString() == "1")
            {
                trItemsCategory.Visible = false;
            }
            else
            {
                trItemsCategory.Visible = true;
            }

            trItems.Visible = true;
            trCategory.Visible = false;
        }
        else
        {
            trItems.Visible = false;
            trCategory.Visible = true;

        }

        ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedItem.Value.ToString());
        ddlItem.DataBind();


        //Unit of measurement
        //Line type Code for Service
        if (clsMrcfLineType.IsHasUnitOfMeasurement(ddlLineType.SelectedValue.ToString()) == true)
        {
            trItemUnit.Visible = true;

            if (clsMrcfLineType.GetDestinationTypeCode(ddlLineType.SelectedValue.ToString()).ToUpper() == "INVENTORY")
            {
                LoadUnit();
            }

            else
            {
                LoadUnitAll();
            }
        }

        else
        {
            trItemUnit.Visible = false;
        }

        //Item Description
        if (clsMrcfLineType.IsHasItemDesc(ddlLineType.SelectedValue.ToString()) == true)
        {
            trItemDescription.Visible = true;
        }
        else
        {
            trItemDescription.Visible = false;
            txtItem.Text = "";
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUnit();
    }

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            DataRow drowCart = tblCart.NewRow();

            string strItemUnit = "";

            string strItemDescription = "";
            //Item Description
            if (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true)
            {
                strItemDescription = ddlItem.SelectedItem.Text;
            }
            else
            {
                strItemDescription = txtItem.Text;
            }

            //Unit of measurement
            //Line type Code for Service
            if (clsMrcfLineType.IsHasUnitOfMeasurement(ddlLineType.SelectedValue.ToString()) == false)
            {
                strItemUnit = "LOT";
            }

            else
            {
                strItemUnit = ddlUnit.SelectedValue.ToString();
            }

            drowCart["itemdesc"] = strItemDescription;
            drowCart["itemspec"] = txtSpec.Text;
            drowCart["GLAccount"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? clsMRCFItem.GetGLAccountItems(clsOracleMrcf.GetCategoryId(ddlItem.SelectedValue.ToString())) : clsMRCFGLAccount.GetGLAccountCode(ddlTransactionType.SelectedValue.ToString(), clsEmployee.GetDivisionCode(Request.Cookies["Speedo"]["UserName"]), clsEmployee.GetDepartmentCode(Request.Cookies["Speedo"]["UserName"])));
            drowCart["LineType"] = ddlLineType.SelectedValue.ToString();
            drowCart["TransactionType"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? clsOracleMrcf.GetCategoryId(ddlItem.SelectedValue.ToString()) : ddlTransactionType.SelectedValue.ToString());
            drowCart["Destination"] = clsMrcfLineType.GetDestinationTypeCode(ddlLineType.SelectedValue.ToString());
            drowCart["Item"] = (clsMrcfLineType.IsHasItemCode(ddlLineType.SelectedValue.ToString()) == true ? ddlItem.SelectedValue.ToString() : "");
            drowCart["qty"] = txtQty.Text;
            drowCart["unit"] = strItemUnit;
            drowCart["dateneed"] = dteDateNeeded.Date.ToShortDateString();
            tblCart.Rows.Add(drowCart);
            ViewState["Cart"] = tblCart;
            dgItems.DataSource = tblCart;
            dgItems.DataBind();

            txtItem.Text = "";
            txtQty.Text = "";
            DateTime dteDate = clsMRCF.GetMinimumDateNeeded(ddlType.SelectedValue);
            dteDateNeeded.MinDate = dteDate;
            dteDateNeeded.Date = dteDate;
            txtSpec.Text = "";

            trNoRequest.Visible = dgItems.Items.Count == 0;
        }
        catch
        {
            Response.Redirect("MRCFNew.aspx");
        }
    }
    protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlItem.DataSource = clsOracleMrcf.GetDataSourceListItems(ddlLineType.SelectedValue.ToString(), ddlItemCategory.SelectedItem.Value.ToString());
        ddlItem.DataBind();

        LoadUnit();
    }
}
