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

public partial class CIS_MRCF_MRCFAllAssign : System.Web.UI.Page
{
    int CurPage = 1;
    public void BindEmployee()
    {
        DataTable tblEmployeesAssign = clsMRCF.DDLEmployeeSearch("PROC", "1");
        ddlEmployee.DataSource = tblEmployeesAssign;
        ddlEmployee.DataValueField = "pvalue";
        ddlEmployee.DataTextField = "ptext";
        ddlEmployee.DataBind();
    }

    public void BindAssignStatus()
    {
        DataTable tblAssignStatus = clsMRCF.DDLAssignStatusSearch();
    //    tblAssignStatus.Rows.Add("999", "All");        
        ddlAssignStatus.DataSource = tblAssignStatus;
        ddlAssignStatus.DataValueField = "pvalue";
        ddlAssignStatus.DataTextField = "ptext";
        ddlAssignStatus.DataBind();
    }


    private string Paging(string pUser, string pStatCode)
    {
        string strReturn = "";
        string statCode = (pStatCode == "999" ? "%" : pStatCode);
        string user = (pUser == "999" ? "%" : pUser);
        int TotalPage = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT ISNULL(COUNT(*),0) AS Cnt FROM CIS.mrcfassigndetails WHERE assignto LIKE @assignto AND isactive = '1' and statcode LIKE @statcode";                
                cmd.Parameters.AddWithValue("@statcode", statCode);
                cmd.Parameters.AddWithValue("@assignto", user);

                cn.Open();
                try
                { 
                   TotalPage = Convert.ToInt32(cmd.ExecuteScalar().ToString()) / 20; 
                  TotalPage += ((Convert.ToInt32(cmd.ExecuteScalar().ToString()) % 20) > 0 ? 1 : 0); 
                }
                catch
                { TotalPage = 0; }
                finally
                { cn.Close(); }

                if (TotalPage <2)
                {
                    strReturn = "&lt;&lt;&nbsp;&lt;&nbsp;&gt;&nbsp;&gt;&gt;";
                }
                else if (CurPage==1)
                {
                    strReturn = "&lt;&lt;&nbsp;&lt;&nbsp;" + CurPage + " - " + TotalPage + "&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + (CurPage + 1) + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&gt;</a>&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + TotalPage + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&gt;&gt;</a>";
                }
                else if (CurPage == TotalPage)
                {
                    strReturn = "<a href='MRCFAllAssign.aspx?CurPage=1&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&lt;&lt;</a>&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + (CurPage - 1) + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&lt;</a>&nbsp;" + CurPage + " - " + TotalPage + "&nbsp;&gt;&nbsp;&gt;&gt;";
                }
                else
                {
                    strReturn = "<a href='MRCFAllAssign.aspx?CurPage=1&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&lt;&lt;</a>&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + (CurPage - 1) + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&lt;</a>&nbsp;" + CurPage + " - " + TotalPage + "&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + (CurPage + 1) + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&gt;</a>&nbsp;<a href='MRCFAllAssign.aspx?CurPage=" + TotalPage + "&pUser=" + pUser + "&pStatCode=" + pStatCode + "'>&gt;&gt;</a>";
                }
            }
        }

        return strReturn;
    }

    protected void LoadListAssigned(string pStatCode,string pUser,int PPage)
    {
        int page = ((PPage - 1) * 20) + 1;
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        int intCtr = 0;
       using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT TOP 20 * FROM (SELECT ROW_NUMBER() OVER(ORDER BY hdlrcode DESC) AS RowNo, (SELECT btchcode FROM CIS.MrcfBatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) as btchcode,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE isactive = '1' AND assignto LIKE '" + (pUser == "999" ? "%" : pUser) + "' AND statcode LIKE '" + (pStatCode == "999" ? "%" : pStatCode) + "')AS QRY WHERE (QRY.RowNo >=@RowNo) AND (QRY.RowNo<=@RowNo+20)  ORDER BY hdlrcode DESC";
            //cmd.CommandText = "SELECT TOP 20 * FROM (SELECT ROW_NUMBER() OVER(ORDER BY hdlrcode ASC) AS RowNo, (SELECT btchcode FROM CIS.MrcfBatch WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) as btchcode,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,hdlrcode,assignto,assignby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE isactive = '1' AND assignto LIKE '" + (pUser == "999" ? "%" : pUser) + "' AND statcode LIKE '" + (pStatCode == "999" ? "%" : pStatCode) + "')AS QRY WHERE RowNo >=@RowNo  ORDER BY hdlrcode DESC";
            cmd.Parameters.AddWithValue("@RowNo", page);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
            string strStatus = "";
            if (pUser == "999")
            {
                strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
                strWrite = strWrite + "<tr>" +
           "<td runat='server' class='GridRows'>" +
           "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' ><img src='../../Support/approval.png' alt='' style='padding-bottom:10px;'/></a>" +
           "<center><a href='#' id='" + dr["hdlrcode"].ToString() + "' runat='server' onclick='ModalPop(this.id)'><img src='../../Support/viewtext22.png' alt='' style='padding-bottom:10px;'/></a></center>" +
           "<a href='MRCFPrint.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' onClick=''><img src='../../Support/print32.png' alt='' /></a>" +
           "<td class='GridRows'>" +
            "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "&updatecode=" + "000" + "' style='font-size:small;'>" + dr["IntendedFor"] + "</a><br>" +
               "Current Handler: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignto"] + "'>" + dr["assignto"] + "</a><br>" +
               "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
               "Date Assigned: " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy") + "<br>" +
               "MRCF Code: " + dr["mrcfcode"] + "<br>" +
               "Batch Code: " + dr["btchcode"] + "<br>" +
               "Remarks: " + dr["Remarks"] +
               "</td>" +
               "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
               strStatus +
               "</td>" +
               "</tr>";
                intCtr++;
            }
            else
            {
                try
                {
                    strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
                    strWrite = strWrite + "<tr>" +
               "<td runat='server' class='GridRows'>" +
               "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' ><img src='../../Support/approval.png' alt='' style='padding-bottom:10px;'/></a>" +
               "<center><a href='#' id='" + dr["hdlrcode"].ToString() + "' runat='server' onclick='ModalPop(this.id)'><img src='../../Support/viewtext22.png' alt='' style='padding-bottom:10px;'/></a></center>" +
               "<a href='MRCFPrint.aspx?mrcfcode=" + dr["mrcfcode"] + "' runat='server' onClick=''><img src='../../Support/print32.png' alt='' /></a>" +
               "<td class='GridRows'>" +
                "<a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "&updatecode=" + "000" + "' style='font-size:small;'>" + dr["IntendedFor"] + "</a><br>" +
                  "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
                   "Date Assigned: " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy") + "<br>" +
                   "MRCF Code: " + dr["mrcfcode"] + "<br>" +
                   "Batch Code: " + dr["btchcode"] + "<br>" +
                   "Remarks: " + dr["Remarks"] +
                   "</td>" +
                   "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
                   strStatus +
                   "</td>" +
                   "</tr>";
                    intCtr++;
                }
                catch { }
            }
            }
            dr.Close();

                    }
        //Response.Write(lblSearch.Text);
        if (intCtr == 0)
          strWrite += "<tr><td colspan='3' class='GridRows'>No record found</td></tr>";
        else
            strWrite += "<tr><td colspan='2' class='GridRows'>[ " + intCtr + " records found ]</td><td id='tdPaging' style='text-align:right;' class='GridRows'>" + Paging(GetUser(), ddlAssignStatus.SelectedValue.ToString()) + "</td></tr>";

        lblSearch.Text = strWrite;
    }

    protected void LoadMRCFHistory(string pHandlerCode)
    {
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT assignby FROM CIS.MrcfAssignDetails WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode and isactive='1') AS MainAssignby,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,assignto,assignby,createby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = '"+ pHandlerCode +"' order by createon desc"; 
            
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strStatus = "";
                strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
         strWrite +=   "<tr>" +
           "<td class='GridRows'>" +
           "Assigned to : <a href='../../Userpage/UserPage.aspx?username=" + dr["assignto"] + "'>" + dr["assignto"] + "</a><br>" +
           "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
           "Date Modified: " + Convert.ToDateTime(dr["createon"]).ToString("MMMM dd, yyyy") + "<br>" +
           "Modified by: <a href='../../Userpage/UserPage.aspx?username=" + dr["createby"] + "'>" + dr["createby"] + "</a><br>" +
           "Remarks: " + dr["Remarks"] +
           "</td>" +
           "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
           strStatus +
           "</td>" +
           "</tr>";
                intCtr++;
            }
            dr.Close();
        }
        Response.Write(strWrite);
        if (intCtr == 0)
           Response.Write(strWrite += "<tr><td colspan='2' class='GridRows'>No record found</td></tr>");
        else
           Response.Write(strWrite += "<tr><td colspan='2' class='GridRows'>[ " + intCtr + " records found ]</td></tr>");
 }

    protected void LoadMRCFHistoryHeader(string pHandlerCode)
    {
        //if (Request.QueryString["mrcfcode"] == null) { return; }
        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
        string strWrite = "";
       using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT assignby FROM CIS.MrcfAssignDetails WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode and isactive='1') AS MainAssignby,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,assignto,assignby,createby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = '" + pHandlerCode + "' and isactive= '1' order by createon desc";

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                strWrite += "<div class='GridBorder' style='padding-top: 5px;'> <table width='100%' cellpadding='5' class='grid'> " +
                            "<tr id='tr1' runat='server'> <td class='GridRows' style='width:25%;'>MRCF Code :</td> " +
                            "<td class='GridRows' style='width: 479px'> <a href='MRCFUpdateStatus.aspx?mrcfcode=" + dr["mrcfcode"] + "&updatecode=" + "000" + "' style='font-size:small;'>" + dr["mrcfcode"] + "</a> </td> " +
                            "</tr> <tr id='tr1' runat='server'> <td class='GridRows' style='width:25%;'>MRCF Intended for :</td> " +
                            "<td class='GridRows' style='width: 479px'>" + dr["IntendedFor"] + " </td> " +
                            "</tr> <tr> <td class='GridRows' style='width:25%;'>Assigned by :</td> " +
                            "<td class='GridRows' style='width: 479px'> <a href='../../Userpage/UserPage.aspx?username=" + dr["Mainassignby"] + "'>" + dr["Mainassignby"] + "</a></td> " +
                            "</tr> <tr> <td class='GridRows' style='width:25%;'>Date Assigned :</td> " +
                            "<td class='GridRows' style='width: 479px'> " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy hh:mm:ss tt") + " </td> </tr> </table>  <br /> </div>";
                Response.Write(strWrite);
                dr.Close();
            }
        }
    }

    public void LoadMRCFHistoryDetailed(string pHandlerCode)
    {
        string strWrite;
        string strMRCFHistoryHeader = "";
        string strMRCFHistory = "";
 

        clsMRCFAssign objMRCFAssign = new clsMRCFAssign();
       
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT assignby FROM CIS.MrcfAssignDetails WHERE hdlrcode = '" + pHandlerCode + "' and isactive='1') AS MainAssignby,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,assignto,assignby,createby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = '" + pHandlerCode + "' and isactive= '1' order by createon desc";

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
           while (dr.Read())
                 
            {
                strMRCFHistoryHeader += "<div class='GridBorder' style='padding-top: 5px;'> <table width='100%' cellpadding='5' class='grid'> " +
                            "<tr id='tr1' runat='server'> <td class='GridRows' style='width:25%;'>MRCF Code :</td> " +
                            "<td class='GridRows' style='width: 479px'>" + dr["mrcfcode"] + " </td> " +
                            "</tr> <tr id='tr1' runat='server'> <td class='GridRows' style='width:25%;'>MRCF Intended for :</td> " +
                            "<td class='GridRows' style='width: 479px'>" + dr["IntendedFor"] + " </td> " +
                            "</tr> <tr> <td class='GridRows' style='width:25%;'>Assigned by :</td> " +
                            "<td class='GridRows' style='width: 479px'> " + dr["Mainassignby"] + "</td> " +
                            "</tr> <tr> <td class='GridRows' style='width:25%;'>Date Assigned :</td> " +
                            "<td class='GridRows' style='width: 479px'> " + Convert.ToDateTime(dr["DateAssign"]).ToString("MMMM dd, yyyy hh:mm:ss tt") + " </td> </tr> </table>  <br /> </div>";
               // dr.Close();
            }
        }


        int intCtr = 0;
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Speedo"].ToString()))
        {
            SqlCommand cmd = cn.CreateCommand();

            cmd.CommandText = "SELECT (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode) AS MRCFcode,(SELECT createon FROM CIS.MrcfAssign WHERE hdlrcode= CIS.MrcfAssignDetails.hdlrcode) AS DateAssign,(SELECT assignby FROM CIS.MrcfAssignDetails WHERE hdlrcode = '" + pHandlerCode + "' and isactive='1') AS MainAssignby,(SELECT intended FROM CIS.Mrcf WHERE mrcfcode = (SELECT mrcfcode FROM CIS.MrcfAssign WHERE hdlrcode = CIS.MrcfAssignDetails.hdlrcode)) AS IntendedFor,statcode,assignto,assignby,createby,remarks,(SELECT statdesc FROM CIS.MrcfAssignStatus WHERE statcode = CIS.MrcfAssignDetails.statcode) AS StatusDescription,createon FROM CIS.mrcfassigndetails WHERE hdlrcode = '" + pHandlerCode + "' order by createon desc";

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strStatus = "";
                strStatus = dr["statcode"].ToString() == "000" ? "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='100%' border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='red' style='border-radius:1px; ' width= '100%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>" : "<table width='100%' style='text-align:center; border: 0px solid grey; padding:0px ;'cellpadding='0' cellspacing='0'><tr><td><table height='16px' width='100%' style='border-radius:0px; border: 1px solid grey; padding:1px ; text-align:left;'  cellpadding='0' cellspacing='0'><tr><td width='100%'><table height='10px' width='" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'  border='0' cellpadding='0' cellspacing='0' ><tr><td bgcolor='#1874cd' style='border-radius:1px; ' width= '" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%'></td></tr></table></td></tr></table>" + objMRCFAssign.GetProjectPercentage(dr["statcode"].ToString()).ToString("N0") + "%</tr></td></table>";
                strMRCFHistory += "<tr>" +
                  "<td class='GridRows'>" +
                  "Assigned to : <a href='../../Userpage/UserPage.aspx?username=" + dr["assignto"] + "'>" + dr["assignto"] + "</a><br>" +
                  "Assigned by: <a href='../../Userpage/UserPage.aspx?username=" + dr["assignby"] + "'>" + dr["assignby"] + "</a><br>" +
                  "Date Modified: " + Convert.ToDateTime(dr["createon"]).ToString("MMMM dd, yyyy") + "<br>" +
                  "Modified by: <a href='../../Userpage/UserPage.aspx?username=" + dr["createby"] + "'>" + dr["createby"] + "</a><br>" +
                  "Remarks: " + dr["Remarks"] +
                  "</td>" +
                  "<td class='GridRows'>" + dr["StatusDescription"].ToString() +
                 strStatus +
                  "</td>" +
                  "</tr>";
                intCtr++;
            }
            dr.Close();
        }
       if (intCtr == 0)
           strMRCFHistory += "<tr><td colspan='2' class='GridRows'>No record found</td></tr>";
       else
           strMRCFHistory += "<tr><td colspan='2' class='GridRows'>[ " + intCtr + " records found ]</td></tr>";

        strWrite = strMRCFHistoryHeader +" <table width='100%' cellpadding='5' class='Grid'>" +
               "<tr> <td class='GridColumns' style='width:70%;'><b>MRCF Status Details</b></td> <td class='GridColumns' style='width:30%;'><b>Status</b></td> </tr> " + strMRCFHistory + "</table>";
        //Response.Write( strWrite);
        Label1.Text = strWrite;
    }

    
    /* ******************
     * * *Page Event* * *
     * ******************
    */ 


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CurPage"] == null)
        {
            CurPage = 1;
        }
        else if (Convert.ToInt32(Request.QueryString["CurPage"].ToString()) == 0 || string.IsNullOrEmpty(Request.QueryString["CurPage"].ToString()))
        {
            CurPage = 1;
        }
        else
        {
            CurPage = Convert.ToInt32(Request.QueryString["CurPage"].ToString());
        }
         
         clsSpeedo.Authenticate();
         if (!Page.IsPostBack)
         {
             BindEmployee();
             BindAssignStatus();
             //ddlAssignStatus.SelectedValue = "999";

             if(string.IsNullOrEmpty(Request.QueryString["pUser"]))
             {
                 ddlEmployee.SelectedValue="999";
             }
             else
             {
                 ddlEmployee.SelectedValue=Request.QueryString["pUser"].ToString();
             }

             if(string.IsNullOrEmpty(Request.QueryString["pStatCode"]))
             {
                 ddlAssignStatus.SelectedValue = "999";
             }
             else
             {
                 ddlAssignStatus.SelectedValue = Request.QueryString["pStatCode"].ToString();
             }

             using (clsMRCFAssign objAssign = new clsMRCFAssign())
             {
                 if (objAssign.GetProcurementManager("PROCMNGR").ToString() == Request.Cookies["Speedo"]["UserName"].ToString())
                 {
                     trEmployee.Visible = true;
                 }
                 else
                 {
                     trEmployee.Visible = false;
                 }
             }

         }
         //if (Request.QueryString["hdlrcode"] == null) { return; }
             LoadListAssigned(ddlAssignStatus.SelectedValue.ToString(),GetUser(),CurPage);
          
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurPage = 1;
        LoadListAssigned(ddlAssignStatus.SelectedValue.ToString(), GetUser(),CurPage);
    }


    public string GetUser()
    {
      string strHandler;
        using (clsMRCFAssign objAssign = new clsMRCFAssign())
        {
            if (objAssign.GetProcurementManager("PROCMNGR").ToString() == Request.Cookies["Speedo"]["UserName"].ToString())
            {
                strHandler = ddlEmployee.SelectedValue.ToString();
            }
            else
            {
                strHandler = Request.Cookies["Speedo"]["UserName"].ToString();
            }
        }
        return strHandler;
    }



    protected void btnMPopup_Click(object sender, EventArgs e)
    {
     
            Bewise.Web.UI.WebControls.FlashControl p = Master.FindControl("FlashControl1") as Bewise.Web.UI.WebControls.FlashControl;
            if (p != null)
            {
                p.Visible = false;
            }
            //string wew;
            //wew = Request.QueryString["MRCFhdlrcode"];
            //wew = sender.ToString();
            //if (Request.QueryString["hdlrcoMRCFhdlrcodede"] == null) { return; }
            LoadMRCFHistoryDetailed(inpHide.Value);
            ModalPopupExtender1.Show();
        }



    protected void lbtnHide_Click(object sender, EventArgs e)
    {
        Bewise.Web.UI.WebControls.FlashControl p = Master.FindControl("FlashControl1") as Bewise.Web.UI.WebControls.FlashControl;
        if (p != null)
        {
            p.Visible = true;
        }
        ModalPopupExtender1.Hide();
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strEmployee;
        if (trEmployee.Visible == false) 
        {
            strEmployee = Request.Cookies["Speedo"]["UserName"].ToString();
        }
        else
        {            
            strEmployee = ddlEmployee.SelectedValue;
        }
        Response.Redirect("MRCFExportExcel.aspx?scode=" + ddlAssignStatus.SelectedValue + "&ucode=" + strEmployee);
    }
}