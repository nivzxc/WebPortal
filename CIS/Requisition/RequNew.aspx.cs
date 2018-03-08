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
using Oracle.DataAccess.Client;
using System.Text.RegularExpressions;
using HRMS;
using STIeForms;
using Oracles;

public partial class CIS_Requisition_RequNew : System.Web.UI.Page
{

    protected void MakeCart_()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemqty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("itemunit", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemprice", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("itemtprice", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("reason", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;
    }

    protected bool IsItemRequested_(string pItemCode)
    {
        bool blnResult = false;
        DataTable tblCart = ViewState["Cart"] as DataTable;
        foreach (DataRow drow in tblCart.Rows)
        {
            if (drow["itemcode"].ToString() == pItemCode)
                blnResult = true;
        }
        return blnResult;
    }

    protected void LoadRC(string pRCCode)
    {
        ddlChargeTo.DataSource = clsRC.GetDdlDs();
        ddlChargeTo.DataValueField = "pValue";
        ddlChargeTo.DataTextField = "pText";
        ddlChargeTo.DataBind();

        foreach (ListItem itm in ddlChargeTo.Items)
        {
            if (itm.Value == pRCCode)
            {
                itm.Selected = true;
                LoadApprover(ddlChargeTo.SelectedValue);
                break;
            }
        }
    }

    protected void LoadApprover(string strRCCode)
    {
        //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        //{
        //    DataTable tblGroupHead = new DataTable();
        //    SqlCommand cmd = cn.CreateCommand();
        //    cmd.CommandText = "SELECT username,firname + ' ' + lastname AS name,email FROM Users.Users WHERE username IN (SELECT username FROM CIS.RequisitionApprover WHERE rccode='" + strRCCode + "' AND pstatus='1' AND userlvl='sprv') ORDER BY firname";
        //    cn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(tblGroupHead);
        //    ddlGrpHead.DataSource = tblGroupHead;
        //    ddlGrpHead.DataValueField = "username";
        //    ddlGrpHead.DataTextField = "name";
        //    ddlGrpHead.DataBind();

        //    if (ddlGrpHead.Items.Count == 0)
        //    {
        //        ListItem itm = new ListItem("No Approver Defined", "none");
        //        ddlGrpHead.Items.Add(itm);
        //    }

        //    cmd.CommandText = "SELECT username,firname + ' ' + lastname AS name,email FROM Users.Users WHERE username IN (SELECT username FROM CIS.RequisitionApprover WHERE rccode='" + strRCCode + "' AND pstatus='1' AND userlvl='head')";
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        btnSave.Enabled = true;
        //        hdnDiviHeadCode.Value = dr["username"].ToString();
        //        hdnDiviHeadMail.Value = dr["email"].ToString();
        //        txtDiviHeadName.Text = dr["name"].ToString();
        //    }
        //    else
        //    {
        //        btnSave.Enabled = false;
        //        hdnDiviHeadCode.Value = "";
        //        hdnDiviHeadMail.Value = "";
        //        txtDiviHeadName.Text = "[No Approver Defined]";
        //    }
        //    dr.Close();
        //}

        //DataTable tblGroupHeadApprover = clsMRCFApprover.DSLGroupHeadApprover(pRcCode);
        DataTable tblGroupHeadApprover = clsModuleApprover.DSLRCApprover(clsModule.RequisitionModule, "1", ddlChargeTo.SelectedValue.ToString());
        ddlGrpHead.DataSource = tblGroupHeadApprover;
        ddlGrpHead.DataValueField = "pvalue";
        ddlGrpHead.DataTextField = "ptext";
        ddlGrpHead.DataBind();
        if (ddlGrpHead.Items.Count == 0)
            ddlGrpHead.Items.Add(new ListItem("No Approver Defined", "none"));


        DataTable tblDivHeadApprover = clsModuleApprover.DSLRCApprover(clsModule.RequisitionModule, "2", ddlChargeTo.SelectedValue.ToString());
        ddlDivision.DataSource = tblDivHeadApprover;
        ddlDivision.DataValueField = "pvalue";
        ddlDivision.DataTextField = "ptext";
        ddlDivision.DataBind();
        if (ddlDivision.Items.Count == 0)
            ddlDivision.Items.Add(new ListItem("No Approver Defined", "none"));

        if (ddlDivision.SelectedValue.ToString() == "none" || ddlGrpHead.SelectedValue.ToString() == "none")
        {
            btnSave.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
            //hdnDiviHeadCode.Value = ddlDivision.SelectedValue.ToString();
            using (clsUsers users = new clsUsers())
            {
                users.Username = ddlDivision.SelectedValue.ToString();
                users.Fill();
                hdnDiviHeadMail.Value = users.Email;
            }
        }
    }

    /// <summary>
    /// Refresh the item list (removing the already selected item)
    /// Loading the details of first item selected
    /// </summary>
    /// 
    //protected void RefreshItemList_()
    //{
    //   string strOrderedItems = "";
    //   DataTable tblItems = new DataTable();
    //   foreach (DataGridItem itm in dgItems.Items)
    //   {
    //      HiddenField hdnItemCode = (HiddenField)itm.FindControl("hdnItemCode");
    //      if (strOrderedItems == "")
    //         strOrderedItems = hdnItemCode.Value;
    //      else
    //         strOrderedItems = strOrderedItems + "','" + hdnItemCode.Value;
    //   }

    //   using (SqlConnection cnGP = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
    //   {
    //      SqlCommand cmdGP = cnGP.CreateCommand();
    //      if (ddlClassItem.SelectedValue == "ALL")
    //         cmdGP.CommandText = "SELECT itemnmbr,itemdesc FROM iv00101 WHERE itemtype='1' AND itmclscd IN('JS01','OS01','PS01','TS01') AND itemnmbr NOT IN ('" + strOrderedItems + "') ORDER BY itemdesc";
    //      else
    //         cmdGP.CommandText = "SELECT itemnmbr,itemdesc FROM iv00101 WHERE itemtype='1' AND itmclscd='" + ddlClassItem.SelectedValue + "' AND itemnmbr NOT IN ('" + strOrderedItems + "') ORDER BY itemdesc";
    //      SqlDataAdapter daGP = new SqlDataAdapter(cmdGP);
    //      cnGP.Open();
    //      daGP.Fill(tblItems);
    //   }

    //   ddlItem1.DataSource = tblItems;
    //   ddlItem1.DataValueField = "itemnmbr";
    //   ddlItem1.DataTextField = "itemdesc";
    //   ddlItem1.DataBind();
    //   if (ddlItem1.Items.Count > 1)
    //   {
    //      ddlItem1.Items[0].Selected = true;
    //      LoadItemDetails();
    //   }
    //}

    protected double GetTotalCost()
    {
        double dblTotalCost = 0;
        foreach (DataGridItem itm in dgItems.Items)
        {
            Label lblTPrice = (Label)itm.FindControl("lblTPrice");
            dblTotalCost += Convert.ToDouble(lblTPrice.Text);
        }
        return dblTotalCost;
    }

    protected void LoadItemDetails_()
    {
        using (SqlConnection cnGP = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
        {
            SqlCommand cmdGP = cnGP.CreateCommand();
            cmdGP.CommandText = "SELECT uomprice,uofm from iv00108 where itemnmbr='" + ddlItem1.SelectedValue + "'";
            cnGP.Open();
            SqlDataReader drGP = cmdGP.ExecuteReader();
            if (drGP.Read())
            {
                lblPriceNew.Text = Convert.ToDouble(drGP["uomprice"]).ToString("######0.00");
                lblUnitNew.Text = drGP["uofm"].ToString().Trim();
            }
            else
            {
                lblPriceNew.Text = "No Data";
                lblUnitNew.Text = "No Data";
            }
            drGP.Close();
        }
    }

    protected bool HasEnoughBudget()
    {

        bool blnReturn = false;

        //####revised by rollie 2014-04-01######
        double dblBudget = 0;
        try
        {
            dblBudget = clsRequisition.GetCurrentBudget(ddlChargeTo.SelectedValue);
        }
        catch
        {
            Response.Redirect("RequisitionBudgetProblem.aspx");
        }
        //####################################### 
		
        double dblTotalCost = GetTotalCost();
        double dblRemaining = dblBudget - dblTotalCost;

        lblCurBudget.Text = dblBudget.ToString("###,##0.00");
        lblTotalCost.Text = dblTotalCost.ToString("###,##0.00");
        lblRemBudget.Text = dblRemaining.ToString("###,##0.00");

        if (dblRemaining >= 0)
        {
            imgMessage.ImageUrl = "~/Support/ok64.png";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "The budget is enough.";
            if (ddlDivision.SelectedValue.ToString() != "")
                btnSave.Enabled = true;
            blnReturn = true;
        }
        else
        {
            imgMessage.ImageUrl = "~/Support/close64.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Not enough budget.";
            btnSave.Enabled = false;
            blnReturn = false;
        }
        lblMessage.Visible = true;
        imgMessage.Visible = true;
        return blnReturn;
    }

    protected void Page_Load__(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();

        if (!Page.IsPostBack)
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            MakeCart();
            txtRequestorName.Text = clsEmployee.GetName(strUsername);
            hdnRCCode.Value = clsEmployee.GetRCCode(strUsername);

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT firname + ' ' + lastname AS name,email  FROM Users.Users WHERE username='" + ConfigurationManager.AppSettings["SuppliesCustodian"] + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtSuppName.Text = dr["name"].ToString();
                    hdnSuppMail.Value = dr["email"].ToString();
                }
                dr.Close();

                DataTable tblItems = new DataTable();
                using (SqlConnection cnGP = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
                {
                    SqlCommand cmdGP = cnGP.CreateCommand();
                    cmdGP.CommandText = "SELECT itemnmbr,itemdesc FROM iv00101 WHERE itemtype='1' AND itmclscd IN('JS01','OS01','PS01','TS01') ORDER BY itemdesc";
                    SqlDataAdapter daGP = new SqlDataAdapter(cmdGP);
                    cnGP.Open();
                    daGP.Fill(tblItems);
                }
                ddlItem1.DataSource = tblItems.DefaultView;
                ddlItem1.DataValueField = "itemnmbr";
                ddlItem1.DataTextField = "itemdesc";
                ddlItem1.DataBind();
                tblItems.Dispose();
            }

            lblMessage.Visible = false;
            imgMessage.Visible = false;

            LoadItemDetails();
            LoadRC(hdnRCCode.Value);
            HasEnoughBudget();
        }
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCart;
            dgItems.DataBind();
            //RefreshItemList();
            HasEnoughBudget();
        }
        catch
        {
            Response.Redirect("RequNew.aspx");
        }
    }

    protected bool ValidateApprover()
    {
        bool blnReturn = false;
        if (ddlDivision.SelectedValue.ToString().Length == 0 || ddlGrpHead.SelectedValue.ToString().Length == 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to create an MRCF Request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>Department/Division approver was not defined.</td>" +
                              "</tr>" +
                             "</table>";
            blnReturn = false;
        }
        else
        {
            blnReturn = true;
        }
        return blnReturn;
    }

    protected bool HasItemPrice()
    {
        bool blnReturn = false;
        if (Convert.ToDecimal(lblPriceNew.Text) > 0)
        {

            imgMessage.ImageUrl = "~/Support/ok64.png";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "The budget is enough.";
            if (ddlDivision.SelectedValue.ToString() != "")
                btnSave.Enabled = true;
            blnReturn = true;
        }
        else
        {

            imgMessage.ImageUrl = "~/Support/close64.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Item don't have price.";
            btnSave.Enabled = false;
            blnReturn = false;
        }

        lblMessage.Visible = true;
        imgMessage.Visible = true;
        return blnReturn;

    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        HasEnoughBudget();
        if (!HasItemPrice())
        {
            return;
        }

        if (Convert.ToDouble(lblRemBudget.Text) > (Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(lblPriceNew.Text)))
        {
            try
            {
                DataTable tblCart = ViewState["Cart"] as DataTable;
                if (!IsItemRequested(clsRequisitionOracle.GetItemNumber(ddlItem1.SelectedValue.ToString())))
                {
                    DataRow drowCart = tblCart.NewRow();
                    drowCart["itemcode"] = clsRequisitionOracle.GetItemNumber(ddlItem1.SelectedValue.ToString());
                    drowCart["itemdesc"] = ddlItem1.SelectedItem.Text.ToString();
                    drowCart["itemqty"] = txtQty1.Text;
                    drowCart["itemunit"] = lblUnitNew.Text;
                    drowCart["itemprice"] = lblPriceNew.Text;
                    drowCart["itemtprice"] = Convert.ToDecimal(txtQty1.Text) * Convert.ToDecimal(lblPriceNew.Text);
                    drowCart["reason"] = txtReason1.Text;
                    tblCart.Rows.Add(drowCart);
                }
                txtQty1.Text = "";
                txtReason1.Text = "";
                ViewState["Cart"] = tblCart;
                dgItems.DataSource = tblCart;
                dgItems.DataBind();
                RefreshItemList();
                HasEnoughBudget();
            }
            catch
            {
                Response.Redirect("RequNew.aspx");
            }
        }
        else
        {
            imgMessage.ImageUrl = "~/Support/close64.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Not enough budget.";
        }
    }

    protected void ddlItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItemDetails();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!ValidateApprover())
        {
            return;
        }

        string strRequCode = "";
        string strApproverName = "";
        string strApproverMail = "";
        clsRequisition.RequisitionMailType rmtApprover = clsRequisition.RequisitionMailType.SentToApproverGH;

        //using (clsUsers users = new clsUsers())
        //{
        //    users.Username = ddlDivision.SelectedValue.ToString();
        //    users.Fill();
        //    hdnDiviHeadMail.Value = users.Email;
        //}
        hdnDiviHeadMail.Value = clsUsers.GetEmail(ddlDivision.SelectedValue.ToString()).ToString();

        if (dgItems.Items.Count == 0)
        {
            divError.Visible = true;
            lblErrMsg.Text = "Unable to send your request.<br>" +
                             "<table>" +
                              "<tr>" +
                               "<td style='vertical-align:top;'><b>Reason:</b></td>" +
                               "<td>You need to include at least one item to request. Make sure to click <b>Add New Item</b> button to include your requested item.</td>" +
                              "</tr>" +
                             "</table>";
        }
        else
        {
            if (HasEnoughBudget())
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString());
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("spREQUInsert", cn);
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@datereq", SqlDbType.DateTime);
                    cmd.Parameters.Add("@userrem", SqlDbType.VarChar, 255);
                    cmd.Parameters.Add("@rccode", SqlDbType.Char, 3);
                    cmd.Parameters.Add("@sprvcode", SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@sprvstat", SqlDbType.Char, 1);
                    cmd.Parameters.Add("@headcode", SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@headstat", SqlDbType.Char, 1);
                    cmd.Parameters.Add("@suppcode", SqlDbType.VarChar, 30);
                    cmd.Parameters.Add("@suppstat", SqlDbType.Char, 1);
                    cmd.Parameters.Add("@totcost", SqlDbType.Float);
                    cmd.Parameters.Add("@status", SqlDbType.Char);
                    cmd.Parameters.Add("@requcode", SqlDbType.Char, 9);

                    cmd.Parameters["@username"].Value = Request.Cookies["Speedo"]["UserName"];
                    cmd.Parameters["@datereq"].Value = DateTime.Now;
                    cmd.Parameters["@userrem"].Value = txtRemarks.Text;
                    cmd.Parameters["@rccode"].Value = ddlChargeTo.SelectedValue;
                    cmd.Parameters["@headcode"].Value = ddlDivision.SelectedValue.ToString();
                    cmd.Parameters["@suppcode"].Value = ConfigurationManager.AppSettings["SuppliesCustodian"];
                    cmd.Parameters["@suppstat"].Value = "F";
                    cmd.Parameters["@totcost"].Value = Math.Round(Convert.ToDouble(lblTotalCost.Text), 2);

                    // the requestor is the division head
                    if (Request.Cookies["Speedo"]["UserName"].ToString() == ddlDivision.SelectedValue.ToString())
                    {
                        strApproverName = txtSuppName.Text;
                        strApproverMail = hdnSuppMail.Value;
                        rmtApprover = clsRequisition.RequisitionMailType.SentToApproverSC;

                        cmd.Parameters["@sprvcode"].Value = "";
                        cmd.Parameters["@sprvstat"].Value = "X";
                        cmd.Parameters["@headstat"].Value = "A";
                        cmd.Parameters["@status"].Value = "A";
                        clsRequisition.DeductBudget(ddlChargeTo.SelectedValue, Convert.ToDouble(lblTotalCost.Text));
                    }

                    // the requestor is the group head
                    else if (Request.Cookies["Speedo"]["UserName"].ToString() == ddlGrpHead.SelectedValue)
                    {
                        //if (clsRequisition.IsHeadApprovalRequired(ddlChargeTo.SelectedValue, ddlDivision.SelectedValue.ToString()))
                        if (clsModuleApprover.IsDivisionHeadApprovalRequired(clsModule.RequisitionModule, ddlChargeTo.SelectedValue.ToString(), ddlDivision.SelectedValue.ToString()))
                        {
                            strApproverName = ddlDivision.SelectedItem.Text;
                            strApproverMail = hdnDiviHeadMail.Value;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverDH;

                            cmd.Parameters["@sprvcode"].Value = ddlGrpHead.SelectedValue;
                            cmd.Parameters["@sprvstat"].Value = "A";
                            cmd.Parameters["@headstat"].Value = "F";
                            cmd.Parameters["@status"].Value = "F";
                        }
                        else
                        {
                            strApproverName = txtSuppName.Text;
                            strApproverMail = hdnSuppMail.Value;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverSC;

                            cmd.Parameters["@sprvcode"].Value = ddlGrpHead.SelectedValue;
                            cmd.Parameters["@sprvstat"].Value = "A";
                            cmd.Parameters["@headstat"].Value = "N";
                            cmd.Parameters["@status"].Value = "A";
                            clsRequisition.DeductBudget(ddlChargeTo.SelectedValue, Convert.ToDouble(lblTotalCost.Text));
                        }
                    }
                    //Update by Charlie Bachiller 2-20-2013
                    //Normal User
                    else
                    {
                        // the division head is the default approver
                        if (ddlGrpHead.SelectedValue == "none")
                        {
                            strApproverName = ddlDivision.SelectedItem.Text;
                            strApproverMail = hdnDiviHeadMail.Value;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverDH;

                            cmd.Parameters["@sprvcode"].Value = "";
                            cmd.Parameters["@sprvstat"].Value = "X";
                            cmd.Parameters["@headstat"].Value = "F";
                            cmd.Parameters["@status"].Value = "F";
                        }
                        //The division head and group head is the same
                        else if (ddlGrpHead.SelectedValue.ToString() == ddlDivision.SelectedValue.ToString())
                        {
                            strApproverName = ddlDivision.SelectedItem.Text;
                            strApproverMail = hdnDiviHeadMail.Value;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverDH;

                            cmd.Parameters["@sprvcode"].Value = "";
                            cmd.Parameters["@sprvstat"].Value = "X";
                            cmd.Parameters["@headstat"].Value = "F";
                            cmd.Parameters["@status"].Value = "F";
                        }
                        else
                        {
                            strApproverName = ddlGrpHead.SelectedItem.Text;
                            strApproverMail = clsUsers.GetEmail(ddlGrpHead.SelectedValue); ;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverGH;

                            cmd.Parameters["@sprvcode"].Value = ddlGrpHead.SelectedValue;
                            cmd.Parameters["@sprvstat"].Value = "F";
                            cmd.Parameters["@headstat"].Value = "F";
                            cmd.Parameters["@status"].Value = "F";
                        }
                    }

                    cmd.Parameters["@requcode"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    strRequCode = cmd.Parameters["@requcode"].Value.ToString();
                    cmd.Parameters.Clear();

                    cmd.CommandType = CommandType.Text;
                    foreach (DataGridItem itm in dgItems.Items)
                    {
                        HiddenField hdnItemCode = (HiddenField)itm.FindControl("hdnItemCode");
                        Label lblItemDesc = (Label)itm.FindControl("lblItemDesc");
                        Label lblQty = (Label)itm.FindControl("lblQty");
                        Label lblUnit = (Label)itm.FindControl("lblUnit");
                        Label lblPrice = (Label)itm.FindControl("lblPrice");
                        Label lblTPrice = (Label)itm.FindControl("lblTPrice");
                        Label lblReason = (Label)itm.FindControl("lblReason");

                        cmd.CommandText = "INSERT INTO CIS.RequisitionDetails VALUES('" + strRequCode + "',@itemcode,@itemdesc,'" + lblQty.Text + "','0',@unit,'" + lblPrice.Text + "','" + lblTPrice.Text + "',@reason,'','1')";
                        cmd.Parameters.Add("@itemcode", SqlDbType.VarChar, 32);
                        cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 52);
                        cmd.Parameters.Add("@unit", SqlDbType.VarChar, 12);
                        cmd.Parameters.Add("@reason", SqlDbType.VarChar, 100);
                        cmd.Parameters["@itemcode"].Value = hdnItemCode.Value;
                        cmd.Parameters["@itemdesc"].Value = lblItemDesc.Text;
                        cmd.Parameters["@unit"].Value = lblUnit.Text;
                        cmd.Parameters["@reason"].Value = lblReason.Text;
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    cn.Close();
                }

                clsRequisition.SendNotification(rmtApprover, txtRequestorName.Text, strApproverName, strApproverMail, strRequCode);
                clsRequisition.SendNotification(clsRequisition.RequisitionMailType.SentToRequestor, txtRequestorName.Text, strApproverName, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), strRequCode);

                Response.Redirect("RequMenu.aspx");
            }
        }
    }

    protected void ddlChargeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadApprover(ddlChargeTo.SelectedValue);
        HasEnoughBudget();
    }

    protected void ddlClassItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadItems(ddlItemSubCategory.SelectedValue.ToString());
        LoadItemDetails();
    }

    #region Oracle

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

    //protected void LoadSubItemCategory(string pItemCategory)
    //{

    //    ddlItemSubCategory.DataSource = clsRequisitonItemCategory.GetDSLReqSub(pItemCategory);
    //    ddlItemSubCategory.DataTextField = "pText";
    //    ddlItemSubCategory.DataValueField = "pValue";
    //    ddlItemSubCategory.DataBind();
    //}

    protected void LoadItems(string pItemSubCategory)
    {
        ddlItem1.DataSource = clsRequisitonItemCategory.GetDSLItems(pItemSubCategory);
        ddlItem1.DataTextField = "pText";
        ddlItem1.DataValueField = "pValue";
        ddlItem1.DataBind();

    }

    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemqty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("itemunit", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemprice", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("itemtprice", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("reason", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;
    }

    protected bool IsItemRequested(string pItemCode)
    {
        bool blnResult = false;
        DataTable tblCart = ViewState["Cart"] as DataTable;
        foreach (DataRow drow in tblCart.Rows)
        {
            if (drow["itemcode"].ToString() == pItemCode)
                blnResult = true;
        }
        return blnResult;
    }

    protected void RefreshItemList()
    {
        //string strOrderedItems = "";
        //DataTable tblItems = new DataTable();
        //tblItems.Columns.Add("pValue");
        //tblItems.Columns.Add("pText");
        //if (dgItems.Items.Count > 0)
        //{
        //    foreach (DataGridItem itm in dgItems.Items)
        //    {
        //        HiddenField hdnItemCode = (HiddenField)itm.FindControl("hdnItemCode");
        //        if (strOrderedItems == "")
        //            strOrderedItems = hdnItemCode.Value;
        //        else
        //            strOrderedItems = strOrderedItems + "','" + hdnItemCode.Value;
        //    }
        //}

        //using (OracleConnection cnGP = new OracleConnection(ConfigurationManager.ConnectionStrings["ORACLEConStr"].ToString()))
        //{
        //    OracleCommand cmdGP = cnGP.CreateCommand();
        //    if (ddlClassItem.SelectedValue == "ALL")
        //        cmdGP.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID) as pValue,DESCRIPTION as pText FROM MTL_SYSTEM_ITEMS_B ORDER BY pText";
        //    else
        //        cmdGP.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID) as pValue,DESCRIPTION as pText, MARKET_PRICE FROM MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE CATEGORY_ID='" + ddlClassItem.SelectedValue + "') ORDER BY pText";

        //    //OracleDataAdapter daGP = new OracleDataAdapter(cmdGP);
        //    cnGP.Open();
        //    OracleDataReader dr = cmdGP.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        if (dr["MARKET_PRICE"].ToString() != "Null" && dr["MARKET_PRICE"].ToString().ToInt() != 0)
        //        {
        //            DataRow drNew = tblItems.NewRow();
        //            drNew["pValue"] = dr["pValue"].ToString();
        //            drNew["pText"] = dr["pText"].ToString();
        //            tblItems.Rows.Add(drNew);
        //        }
        //    }
        //}

        //ddlItem1.DataSource = tblItems;
        //ddlItem1.DataValueField = "pValue";
        //ddlItem1.DataTextField = "pText";
        //ddlItem1.DataBind();
        //if (ddlItem1.Items.Count > 1)
        //{
        //    ddlItem1.Items[0].Selected = true;
        //    LoadItemDetails();
        //}
    }

    protected void LoadItemDetails()
    {

       double dblItemCost = clsRequisitionOracle.LoadItemCost(ddlItem1.SelectedValue.ToString());
        //double dblItemCost = clsRequisitionOracle.LoadtCost(ddlItem1.SelectedValue.ToString(),"2");
       string strUOM = clsRequisitionOracle.LoadItemUOM(ddlItem1.SelectedValue.ToString(),"2");
       if (dblItemCost > 0)
       {
           lblPriceNew.Text = dblItemCost.ToString("######0.00");
       }
       else
       {
           lblPriceNew.Text = "0.00";
       }

       lblUnitNew.Text = strUOM;
       HasItemPrice();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        clsSpeedo.Authenticate();
        //btnSave.Attributes.Add("onclick", "if(Page_ClientValidate()){this.disabled=true;" + btnSave.Page.ClientScript.GetPostBackEventReference(btnSave, string.Empty).ToString() + ";return CheckIsRepeat();}");
        btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        if (!clsOracleMrcf.IsOracleUp())
        {
            Response.Redirect("OracleDatabaseProblem.aspx");
        }
        if (!Page.IsPostBack)
        {
            string strUsername = Request.Cookies["Speedo"]["UserName"];
            string strProcessScript = "this.value='Processing...';this.disabled=true;";
            btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
            MakeCart();
            //LoadDDL();
            LoadPrimaryItemCategory();
            //LoadSubItemCategory(ddlItemCategory.SelectedValue.ToString());

            LoadItems(ddlItemCategory.SelectedValue.ToString());

            if (ddlItem1.Items.Count > 1)
            {
                ddlItem1.Items[0].Selected = true;
                LoadItemDetails();
            }

            txtRequestorName.Text = clsEmployee.GetName(strUsername);
            hdnRCCode.Value = clsEmployee.GetRCCode(strUsername);

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT firname + ' ' + lastname AS name,email  FROM Users.Users WHERE username='" + ConfigurationManager.AppSettings["SuppliesCustodian"] + "'";
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtSuppName.Text = dr["name"].ToString();
                    hdnSuppMail.Value = dr["email"].ToString();
                }
                dr.Close();
            }

            //   DataTable tblItems = new DataTable();
            //   using (OracleConnection cnGP = new OracleConnection(ConfigurationManager.ConnectionStrings["ORACLEConStr"].ToString()))
            //   {
            //      OracleCommand cmdGP = cnGP.CreateCommand();
            //      //cmdGP.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID) as pValue,DESCRIPTION as pText, MARKET_PRICE as pPrice FROM MTL_SYSTEM_ITEMS_B";
            //      if (ddlClassItem.SelectedValue == "ALL")
            //         cmdGP.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,DESCRIPTION as pText FROM MTL_SYSTEM_ITEMS_B AND INVENTORY_ITEM_ID ORDER BY pText";
            //      else
            //         cmdGP.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,DESCRIPTION as pText FROM MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE CATEGORY_ID='" + ddlClassItem.SelectedValue + "') ORDER BY pText";
            //      OracleDataAdapter daGP = new OracleDataAdapter(cmdGP);
            //      cnGP.Open();
            //      daGP.Fill(tblItems);
            //   }
            //   ddlItem1.DataSource = tblItems.DefaultView;
            //   ddlItem1.DataValueField = "pValue";
            //   ddlItem1.DataTextField = "pText";
            //   ddlItem1.DataBind();



            //   tblItems.Dispose();
            //}

            lblMessage.Visible = false;
            imgMessage.Visible = false;

            LoadItemDetails();
            LoadRC(hdnRCCode.Value);
            HasEnoughBudget();
        }
    }

    #endregion
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadSubItemCategory(ddlItemCategory.SelectedValue.ToString());

        LoadItems(ddlItemCategory.SelectedValue.ToString());
        LoadItemDetails();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
