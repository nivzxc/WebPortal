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

public partial class CIS_Requisition_RequDetails : System.Web.UI.Page
{
    protected void MakeCart()
    {
        DataTable tblCart = new DataTable("Cart");
        tblCart.Columns.Add("itemcode", System.Type.GetType("System.String"));
        tblCart.Columns.Add("itemdesc", System.Type.GetType("System.String"));
        tblCart.Columns.Add("qty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("soqty", System.Type.GetType("System.Int32"));
        tblCart.Columns.Add("unit", System.Type.GetType("System.String"));
        tblCart.Columns.Add("price", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("tprice", System.Type.GetType("System.Double"));
        tblCart.Columns.Add("reason", System.Type.GetType("System.String"));
        ViewState["Cart"] = tblCart;
    }

    protected void BindItems()
    {
        double dblTotalPrice = 0.0;
        clsRequisition requisition = new clsRequisition(txtRequCode.Text);
        DataTable tblItems = requisition.DSGItems();

        dgItems.DataSource = tblItems.DefaultView;
        //dblTotalPrice = Convert.ToDouble(tblItems.Compute("SUM(tprice)", String.Empty).ToString());
        //dgItems.Columns[0].FooterText = "&nbsp;Total ordered items [" + tblItems.Rows.Count + "]";
        //dgItems.Columns[4].FooterText = dblTotalPrice.ToString("###,##0.00") + "&nbsp;";
        dgItems.DataBind();

        DataTable tblCart = ViewState["Cart"] as DataTable;

        foreach (DataRow drNew in tblItems.Rows)
        {
            DataRow drowCart = tblCart.NewRow();
            drowCart["itemcode"] = drNew["itemcode"].ToString();
            drowCart["itemdesc"] = drNew["itemcode"].ToString();
            drowCart["qty"] = drNew["qty"].ToString().ToInt();
            drowCart["soqty"] = drNew["soqty"].ToString().ToInt();
            drowCart["unit"] = drNew["unit"].ToString();
            drowCart["price"] = Double.Parse(drNew["price"].ToString());
            drowCart["tprice"] = Double.Parse(drNew["tprice"].ToString());
            drowCart["reason"] = drNew["reason"].ToString();
            tblCart.Rows.Add(drowCart);
        }

        ViewState["Cart"] = tblCart;
    }

    protected double GetTotalCost()
    {
        double dblTotalCost = 0;
        DataTable tblCart = ViewState["Cart"] as DataTable;
        foreach (DataRow itm in tblCart.Rows)
        {
            dblTotalCost = dblTotalCost + Double.Parse(itm["tprice"].ToString());
        }
        return dblTotalCost;
    }

    protected bool HasEnoughBudget()
    {
        bool blnReturn = false;
        double dblBudget = clsRequisition.GetCurrentBudget(hdnChargeTo.Value);
        double dblTotalCost = GetTotalCost();
        double dblRemaining = dblBudget - dblTotalCost;

        lblCurBudget.Text = dblBudget.ToString("###,###.00");
        lblTotalCost.Text = dblTotalCost.ToString("###,###.00");
        lblRemBudget.Text = dblRemaining.ToString("###,###.00");

        if (dblRemaining >= 0)
        {
            imgMessage.ImageUrl = "~/Support/ok64.png";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "The budget is enough.";
            btnSend.Enabled = true;
            blnReturn = true;
        }
        else
        {
            imgMessage.ImageUrl = "~/Support/close64.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Not enough budget.";
            btnSend.Enabled = false;
            blnReturn = false;
        }

        return blnReturn;
    }

    //protected void LoadItems()
    //{
    //    string strOrderedItems = "";
    //    foreach (DataGridItem ditm in dgItems.Items)
    //    {
    //        HiddenField phdnItemCode = (HiddenField)ditm.FindControl("hdnItemCode");
    //        if (strOrderedItems == "")
    //            strOrderedItems = phdnItemCode.Value;
    //        else
    //            strOrderedItems = strOrderedItems + "','" + phdnItemCode.Value;
    //    }

    //    using (SqlConnection cnGP = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
    //    {
    //        DataTable tblItems = ViewState["Cart"] as DataTable;
    //        SqlCommand cmdGP = cnGP.CreateCommand();
    //        if (ddlClassItem.SelectedValue == "ALL")
    //            cmdGP.CommandText = "SELECT itemnmbr,itemdesc FROM iv00101 WHERE itemtype='1' AND itmclscd IN('JS01','OS01','PS01','TS01') AND itemnmbr NOT IN ('" + strOrderedItems + "') ORDER BY itemdesc";
    //        else
    //            cmdGP.CommandText = "SELECT itemnmbr,itemdesc FROM iv00101 WHERE itemtype='1' AND itmclscd='" + ddlClassItem.SelectedValue + "' AND itemnmbr NOT IN ('" + strOrderedItems + "') ORDER BY itemdesc";
    //        cnGP.Open();
    //        SqlDataAdapter daGP = new SqlDataAdapter(cmdGP);
    //        daGP.Fill(tblItems);
    //        ddlItem1.DataSource = tblItems.DefaultView;
    //        ddlItem1.DataValueField = "itemnmbr";
    //        ddlItem1.DataTextField = "itemdesc";
    //        ddlItem1.DataBind();
    //        tblItems.Dispose();

    //        if (ddlItem1.Items.Count != 0)
    //        {
    //            cmdGP.CommandText = "SELECT uomschdl,currcost,stndcost,QTYONHND,ATYALLOC FROM iv00101 INNER JOIN iv00102 ON iv00101.itemnmbr = iv00102.itemnmbr WHERE iv00101.itemnmbr='" + ddlItem1.Items[0].Value + "'";
    //            SqlDataReader drGP = cmdGP.ExecuteReader();
    //            drGP.Read();
    //            if ((Convert.ToInt32(drGP["QTYONHND"]) - Convert.ToInt32(drGP["ATYALLOC"])) <= 0)
    //                lblPriceNew.Text = Convert.ToDouble(drGP["stndcost"]).ToString("######0.00");
    //            else
    //                lblPriceNew.Text = Convert.ToDouble(drGP["currcost"]).ToString("######0.00");
    //            lblUnitNew.Text = drGP["uomschdl"].ToString().Trim();
    //            drGP.Close();
    //        }
    //    }
    //}


    protected void LoadItems(string pItemSubCategory)
    {
        ddlItem1.DataSource = clsRequisitonItemCategory.GetDSLItems(pItemSubCategory);
        ddlItem1.DataTextField = "pText";
        ddlItem1.DataValueField = "pValue";
        ddlItem1.DataBind();

    }

    protected void LoadItemDetails()
    {

        double dblItemCost = clsRequisitionOracle.LoadItemCost(ddlItem1.SelectedValue.ToString());
        string strUOM = clsRequisitionOracle.LoadItemUOM(ddlItem1.SelectedValue.ToString());
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
            bool blnReadOnly;
            clsRequisition.AuthenticateUser(clsRequisition.RequisitionUserType.Requestor, Request.Cookies["Speedo"]["UserName"].ToString(), Request.QueryString["requcode"].ToString());
            MakeCart();
            LoadPrimaryItemCategory();
            LoadItems(ddlItemCategory.SelectedValue.ToString());
            LoadItemDetails();
            txtRequCode.Text = Request.QueryString["requcode"].ToString();
            txtRequestorName.Text = clsUsers.GetName(Request.Cookies["Speedo"]["UserName"]);

            using (clsRequisition requisition = new clsRequisition(txtRequCode.Text))
            {
                requisition.Fill();
                txtDateReq.Text = requisition.DateRequested.ToString();
                txtIntended.Text = requisition.Intended;
                hdnChargeTo.Value = requisition.ChargeTo;
                hdnGrpHeadCode.Value = requisition.GroupHead;
                txtGrpHeadRem.Text = requisition.GroupHeadRemarks;
                hdnDiviHeadCode.Value = requisition.DivisionHead;
                txtHeadRem.Text = requisition.DivisionHeadRemarks;
                hdnSuppCode.Value = requisition.SuppliesCustodian;
                txtSuppRem.Text = requisition.SuppliesCustodianRemarks;
                hdnStatus.Value = requisition.Status;
                txtStat.Text = requisition.StatusDescription;
                blnReadOnly = (hdnStatus.Value == "M" ? false : true);
            }

            txtChargeTo.Text = clsRC.GetRCName(hdnChargeTo.Value);
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

                users.Username = hdnSuppCode.Value;
                users.Fill();
                txtSuppName.Text = users.FullName;
                hdnSuppMail.Value = users.Email;
            }

            txtIntended.ReadOnly = blnReadOnly;
            divButtons.Visible = !blnReadOnly;
            divAddItem.Visible = !blnReadOnly;
            divBudget.Visible = !blnReadOnly;

            BindItems();

            if (blnReadOnly)
            {
                //foreach (DataGridItem itm in dgItems.Items)
                //{
                //    TextBox ptxtQty = (TextBox)itm.FindControl("txtQty");
                //    ptxtQty.ReadOnly = blnReadOnly;
                //    ptxtQty.BackColor = System.Drawing.Color.FromName("#f0f8ff");
                //}
            }
            else
            {
                // LoadItems();
                HasEnoughBudget();
            }
            if (blnReadOnly)
            {
                txtIntended.BackColor = System.Drawing.Color.FromName("#f0f8ff");
                dgItems.Columns[6].Visible = false;
            }
        }
    }

    protected void dgItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        //clsRequisition requisition = new clsRequisition(txtRequCode.Text);
        //if (requisition.CountItem() >= 1)
        //{
        //    HiddenField phdnItemCode = (HiddenField)e.Item.FindControl("hdnItemCode");
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        //    {
        //        using (SqlCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandText = "DELETE FROM CIS.RequisitionDetails WHERE itemcode='" + phdnItemCode.Value + "' AND requcode='" + Request.QueryString["requcode"] + "'";
        //            cn.Open();
        //            cmd.ExecuteNonQuery();
        //            cmd.CommandText = "UPDATE CIS.Requisition SET totcost=(SELECT SUM(tprice) FROM CIS.RequisitionDetails WHERE requcode='" + Request.QueryString["requcode"] + "') WHERE requcode='" + Request.QueryString["requcode"] + "'";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    dgItems.EditItemIndex = -1;
        //    if (dgItems.Items.Count == 1)
        //        dgItems.CurrentPageIndex = dgItems.CurrentPageIndex - 1;
        //    LoadItems();
        //    BindItems();
        //    HasEnoughBudget();
        //    divErr.Visible = false;
        //}
        //else
        //{
        //    lblErrMsg.Text = "&nbsp;! Cannot delete this item.";
        //    divErr.Visible = true;
        //}

        try
        {
            DataTable tblCart = ViewState["Cart"] as DataTable;
            tblCart.Rows[e.Item.ItemIndex].Delete();
            ViewState["Cart"] = tblCart;

            dgItems.DataSource = tblCart;
            dgItems.DataBind();
            //RefreshItemList();
            //BindItems();
            HasEnoughBudget();
        }
        catch
        {
            Response.Redirect("RequMenu.aspx");
        }
    }

    protected bool HasItemPrice()
    {
        bool blnReturn = false;
        if (Convert.ToDecimal(lblPriceNew.Text) > 0)
        {

            imgMessage.ImageUrl = "~/Support/ok64.png";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "The budget is enough.";
            //if (ddlDivision.SelectedValue.ToString() != "")
            //btnSave.Enabled = true;
            blnReturn = true;
        }
        else
        {

            imgMessage.ImageUrl = "~/Support/close64.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Item don't have price.";
            //btnSave.Enabled = false;
            blnReturn = false;
        }

        lblMessage.Visible = true;
        imgMessage.Visible = true;
        return blnReturn;

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        DataTable tblCart = ViewState["Cart"] as DataTable;

        if (HasEnoughBudget())
        {
            if (tblCart.Rows.Count > 0)
            {
                bool blnHasBudget = true;
                string strApproverName = "";
                string strApproverMail = "";
                clsRequisition.RequisitionMailType rmtApprover = clsRequisition.RequisitionMailType.SentToApproverGH;
                tblCart = ViewState["Cart"] as DataTable;

                SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = tran;

                try
                {

                    cmd.CommandText = "DELETE FROM CIS.RequisitionDetails WHERE requcode='" + Request.QueryString["requcode"] + "'";
                    cmd.ExecuteNonQuery();

                    foreach (DataRow dr in tblCart.Rows)
                    {
                        //HiddenField phdnItemCode = ;
                        //TextBox ptxtQty = (TextBox)itm.FindControl("txtQty");
                        //Label plblPrice = (Label)itm.FindControl("lblPrice");
                        //double dblTPrice = (Convert.ToDouble(ptxtQty.Text) * Convert.ToDouble(plblPrice.Text));
                        //if (clsRequisition.HasBudget(Request.QueryString["requcode"], double.Parse(dr["tqty"].ToString()), hdnChargeTo.Value, dr["itemcode"].ToString()))
                        //{

                        cmd.CommandText = "INSERT INTO CIS.RequisitionDetails (requcode, itemcode, itemdesc, qty, soqty, unit, price, tprice, reason, supprem, status) VALUES(@requcode, @itemcode, @itemdesc, @qty, '0', @unit, @price, @tprice, '', '', '1')";
                        cmd.Parameters.Add(new SqlParameter("@itemCode", dr["itemcode"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@qty", dr["qty"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@unit", dr["unit"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@price", dr["price"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@tprice", dr["tprice"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@itemdesc", dr["itemdesc"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@requcode", Request.QueryString["requcode"]));

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        //divError.Visible = false;
                        //}
                        //else
                        //{
                        //    //ptxtQty.BackColor = System.Drawing.Color.MistyRose;
                        //    lblErrMsg.Text = "&nbsp;! Not Enought Budget.<br>";
                        //    divError.Visible = true;
                        //    blnHasBudget = false;
                        //}
                    }

                    clsRequisition requisition = new clsRequisition(txtRequCode.Text);
                    //requisition.UpdateTotalCost();
                    cmd.CommandText = "UPDATE CIS.Requisition SET totcost=@totcost WHERE requcode='" + Request.QueryString["requcode"] + "'";
                    cmd.Parameters.Add(new SqlParameter("@totcost", GetTotalCost()));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (blnHasBudget)
                    {
                        // the requestor is the group head
                        if (Request.Cookies["Speedo"]["UserName"].ToString() == hdnGrpHeadCode.Value)
                        {
                            strApproverName = txtDiviHeadName.Text;
                            strApproverMail = hdnDiviHeadMail.Value;
                            rmtApprover = clsRequisition.RequisitionMailType.SentToApproverDH;
                            cmd.CommandText = "UPDATE CIS.Requisition SET status='F',sprvstat='A',headstat='F',suppstat='F',userrem=@userrem WHERE requcode='" + txtRequCode.Text + "'";
                        }
                        else
                        {
                            // the default approver is the division head
                            if (hdnGrpHeadCode.Value == "")
                            {
                                strApproverName = txtDiviHeadName.Text;
                                strApproverMail = hdnDiviHeadMail.Value;
                                rmtApprover = clsRequisition.RequisitionMailType.SentToApproverDH;
                                cmd.CommandText = "UPDATE CIS.Requisition SET status='F',sprvstat='X',headstat='F',suppstat='F',userrem=@userrem WHERE requcode='" + txtRequCode.Text + "'";
                            }
                            else
                            {
                                strApproverName = txtGrpHeadName.Text;
                                strApproverMail = hdnGrpHeadMail.Value;
                                rmtApprover = clsRequisition.RequisitionMailType.SentToApproverGH;
                                cmd.CommandText = "UPDATE CIS.Requisition SET status='F',sprvstat='F',headstat='F',suppstat='F',userrem=@userrem WHERE requcode='" + txtRequCode.Text + "'";
                            }
                        }

                        cmd.Parameters.Add("@userrem", SqlDbType.VarChar, 200);
                        cmd.Parameters["@userrem"].Value = txtIntended.Text;
                        cmd.ExecuteNonQuery();
                        tran.Commit();
                        clsRequisition.SendNotification(rmtApprover, txtRequestorName.Text, strApproverName, strApproverMail, txtRequCode.Text);
                        clsRequisition.SendNotification(clsRequisition.RequisitionMailType.SentToRequestor, txtRequestorName.Text, strApproverName, clsUsers.GetEmail(Request.Cookies["Speedo"]["UserName"].ToString()), txtRequCode.Text);
                    
                    
                    }
                }
                catch
                {
                    lblErrMsg.Text = "Error on saving. Please try again.<br>";
                    divError.Visible = true;
                }

                finally 
                {
                    cn.Close();
                    Response.Redirect("RequMenu.aspx");
                }
                
            }
            else
            {
                lblErrMsg.Text = "No item has been added. Cannot continue.<br>";
                divError.Visible = true;
            }
        }

        //HasEnoughBudget();
        //if (!HasItemPrice())
        //{
        //    return;
        //}

        //if (Convert.ToDouble(lblRemBudget.Text) > (Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(lblPriceNew.Text)))
        //{
        //    try
        //    {
        //        DataTable tblCart = ViewState["Cart"] as DataTable;
        //        if (!IsItemRequested(clsRequisitionOracle.GetItemNumber(ddlItem1.SelectedValue.ToString())))
        //        {
        //            DataRow drowCart = tblCart.NewRow();
        //            drowCart["itemcode"] = clsRequisitionOracle.GetItemNumber(ddlItem1.SelectedValue.ToString());
        //            drowCart["itemdesc"] = ddlItem1.SelectedItem.Text;
        //            drowCart["qty"] = txtQty1.Text;
        //            drowCart["unit"] = lblUnitNew.Text;
        //            drowCart["price"] = lblPriceNew.Text;
        //            drowCart["tprice"] = Convert.ToDecimal(txtQty1.Text) * Convert.ToDecimal(lblPriceNew.Text);
        //            drowCart["reason"] = txtReason1.Text;
        //            tblCart.Rows.Add(drowCart);
        //        }
        //        txtQty1.Text = "";
        //        txtReason1.Text = "";
        //        ViewState["Cart"] = tblCart;
        //        dgItems.DataSource = tblCart;
        //        dgItems.DataBind();
        //        HasEnoughBudget();
        //    }
        //    catch
        //    {
        //        Response.Redirect("RequNew.aspx");
        //    }
        //}
        //else
        //{
        //    imgMessage.ImageUrl = "~/Support/close64.png";
        //    lblMessage.ForeColor = System.Drawing.Color.Red;
        //    lblMessage.Text = "Not enough budget.";
        //}
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequDetails.aspx?requcode=" + Request.QueryString["requcode"].ToString());
    }

    protected void btnVoid_Click(object sender, EventArgs e)
    {
        clsRequisition requisition = new clsRequisition(txtRequCode.Text);
        requisition.Void();

        Response.Redirect("RequMenu.aspx");
    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        //clsRequisition requisition = new clsRequisition();
        //if (clsRequisition.HasBudget(Request.QueryString["requcode"].ToString(), (Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(lblPriceNew.Text)), hdnChargeTo.Value))
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        //    {
        //        double dblNewItemPrice = Math.Round((Convert.ToDouble(txtQty1.Text) * Convert.ToDouble(lblPriceNew.Text)), 2);
        //        SqlCommand cmd = cn.CreateCommand();
        //        cmd.CommandText = "INSERT INTO CIS.RequisitionDetails VALUES('" + txtRequCode.Text + "','" + ddlItem1.SelectedValue + "',@itemdesc,'" + txtQty1.Text + "','0','" + lblUnitNew.Text + "','" + lblPriceNew.Text + "','" + dblNewItemPrice + "',@reason,'','1')";
        //        cmd.Parameters.Add("@itemdesc", SqlDbType.VarChar, 60);
        //        cmd.Parameters.Add("@reason", SqlDbType.VarChar, 100);
        //        cmd.Parameters["@itemdesc"].Value = ddlItem1.SelectedItem.Text;
        //        cmd.Parameters["@reason"].Value = txtReason1.Text;
        //        cn.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
        //        cmd.CommandText = "UPDATE CIS.Requisition SET totcost=totcost + '" + dblNewItemPrice + "' WHERE requcode='" + txtRequCode.Text + "'";
        //        cmd.ExecuteNonQuery();
        //    }
        //    Response.Redirect("RequDetails.aspx?requcode=" + Request.QueryString["requcode"]);
        //}
        //else
        //{
        //    lblErrMsg.Text = "&nbsp;! Cannot add item. Not Enought Budget.";
        //    divErr.Visible = true;
        //}
        //divErr.Visible = false;
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
                    drowCart["itemdesc"] = ddlItem1.SelectedItem.Text;
                    drowCart["qty"] = txtQty1.Text;
                    drowCart["soqty"] = "0";
                    drowCart["unit"] = lblUnitNew.Text;
                    drowCart["price"] = lblPriceNew.Text;
                    drowCart["tprice"] = Convert.ToDecimal(txtQty1.Text) * Convert.ToDecimal(lblPriceNew.Text);
                    drowCart["reason"] = txtReason1.Text;
                    tblCart.Rows.Add(drowCart);
                }
                txtQty1.Text = "";
                txtReason1.Text = "";
                ViewState["Cart"] = tblCart;
                //dgItems.DataSource = tblCart;
                //dgItems.DataBind();

                dgItems.DataSource = tblCart.DefaultView;
                dgItems.DataBind();

                //RefreshItemList();
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
            divError.Visible = true;
        }
    }

    protected void ddlClassItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItemDetails();
    }

    protected void ddlItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //using (SqlConnection cnGP = new SqlConnection(ConfigurationManager.ConnectionStrings["GreatPlains"].ToString()))
        //{
        //    SqlCommand cmdP = new SqlCommand("SELECT uomschdl,currcost,stndcost,QTYONHND,ATYALLOC FROM iv00101 INNER JOIN iv00102 ON iv00101.itemnmbr = iv00102.itemnmbr WHERE iv00101.itemnmbr='" + ddlItem1.SelectedValue + "'", cnGP);
        //    cnGP.Open();
        //    SqlDataReader drP = cmdP.ExecuteReader();
        //    drP.Read();
        //    if ((Convert.ToInt32(drP["QTYONHND"]) - Convert.ToInt32(drP["ATYALLOC"])) <= 0)
        //        lblPriceNew.Text = Convert.ToDouble(drP["stndcost"]).ToString("######0.00");
        //    else
        //        lblPriceNew.Text = Convert.ToDouble(drP["currcost"]).ToString("######0.00");
        //    lblUnitNew.Text = drP["uomschdl"].ToString().Trim();
        //    drP.Close();
        //}

        LoadItemDetails();
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadItems(ddlItemCategory.SelectedValue.ToString());
        LoadItemDetails();
    }
}
