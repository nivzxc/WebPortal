//Programmer: Charlie Bachiller 
//Date finished: March 4, 2011

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using STIeForms;
using HRMS;

public partial class Finance_CATA_CATADetailsApprover : System.Web.UI.Page
{
 public string strHotelName = "";
 public static string strPayee = "";

 //public void LoadCATAParticulars()
 //{
 //    string strWrite = "";
 //    double dblTotal = 0;

 //    using (clsCATARequest objCATARequest = new clsCATARequest())
 //    {
 //        objCATARequest.CataCode = Request.QueryString["catacode"];
 //        objCATARequest.Fill();
 //        strHotelName = objCATARequest.HotelName;
 //    }

 //    DataTable tblCATAType = clsCATAType.GetDSLUsed(lblCATANo.Text);
 //    {
 //        foreach (DataRow drw in tblCATAType.Rows)
 //        {
 //            strWrite += "<tr>" +
 //                         "<td class='GridRowsGray' style='text-align:left;padding:3'><b>" + drw["pText"].ToString() + "</b></td>" +
 //                        "</tr>";

 //            DataTable tblRequestDetails = clsCATADetails.GetCATADetails(drw["pValue"].ToString(), lblCATANo.Text);
 //            {
 //                foreach (DataRow drwDetails in tblRequestDetails.Rows)
 //                {
 //                    if (drw["pValue"].ToString() == "01")
 //                    {

 //                        strWrite += "<tr>" +
 //                                     "<td class='GridRows'>" +
 //                                      "<table width='90%'>" +
 //                                       "<tr>" +
 //                                        "<td style='width:60%;text-align:left;text-indent:150px;'>" + clsCATASubtype.GetName(drwDetails["SubTypeCode"].ToString()) + " : " + strHotelName + " (Rate/Day: " + string.Format("{0:n2}", double.Parse(clsCATASettings.GetAmount("01", drwDetails["SubTypeCode"].ToString(), clsEmployee.GetJobGrade(strPayee)))) + ")</td>" +
 //                                        "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwDetails["Amount"].ToString())) + "</td>" +
 //                                       "</tr>" +
 //                                      "</table>" +
 //                                     "</td>" +
 //                                    "</tr>" +
 //                                    "<tr>";
 //                        //"<td td class='GridRows'>" +
 //                        //  "<table width='90%'>" +
 //                        //   "<tr>" +
 //                        //    "<td style='width:60%;text-indent:250px;'>Rate/Day:</td>" +
 //                        //    "<td style='width:40%;text-align: right: padding-right:150px;'>" + string.Format("{0:n2}", double.Parse(clsCATASettings.GetAmount("01", drwDetails["SubTypeCode"].ToString(), clsEmployee.GetJobGrade(strPayee)))) + "</td>" +
 //                        //   "</tr>" +
 //                        //  "</table>" +
 //                        // "</td>" +
 //                        //"</tr>";

 //                    }
 //                    else
 //                    {
 //                        strWrite += "<tr>" +
 //                                     "<td class='GridRows'>" +
 //                                      "<table width='90%'>" +
 //                                       "<tr>" +
 //                                        "<td style='width:60%;text-align:left;text-indent:150px;'>" + clsCATASubtype.GetName(drwDetails["SubTypeCode"].ToString()) + "</td>" +
 //                                        "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwDetails["Amount"].ToString())) + "</td>" +
 //                                       "</tr>" +
 //                                      "</table>" +
 //                                     "</td>" +
 //                                    "</tr>";
 //                    }
 //                    dblTotal = dblTotal + Convert.ToDouble(drwDetails["Amount"].ToString());
 //                }
 //            }
 //        }
 //    }

 //    DataTable tblIncidentals = clsCATAIncedental.GetDSGMainForm(Request.QueryString["catacode"]);
 //    {
 //        if (tblIncidentals.Rows.Count != 0)
 //        {
 //            strWrite += "<tr>" +
 //                          "<td class='GridRowsGray' style='text-align:left;padding:3'><b>Incidentals</b></td>" +
 //                         "</tr>";

 //            foreach (DataRow drwIncidentals in tblIncidentals.Rows)
 //            {
 //                strWrite += "<tr>" +
 //                           "<td class='GridRows'>" +
 //                            "<table width='90%'>" +
 //                             "<tr>" +
 //                              "<td style='width:60%;text-align:left;text-indent:150px;'>" + drwIncidentals["incdental"].ToString() + "</td>" +
 //                              "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwIncidentals["Amount"].ToString())) + "</td>" +
 //                             "</tr>" +
 //                            "</table>" +
 //                           "</td>" +
 //                          "</tr>";
 //                dblTotal = dblTotal + Convert.ToDouble(drwIncidentals["Amount"].ToString());
 //            }
 //        }
 //        strWrite += "<tr>" +
 //                           "<td class='GridRowsGray'>" +
 //                            "<table width='90%'>" +
 //                             "<tr>" +
 //                              "<td style='width:60%;text-align:left;'><b>TOTAL</b></td>" +
 //                              "<td style='width:40%;text-align: right;'><b>P " + string.Format("{0:n2}", dblTotal) + "</b></td>" +
 //                             "</tr>" +
 //                            "</table>" +
 //                           "</td>" +
 //                          "</tr>";

 //    }
 //    Response.Write(strWrite);
 //}

 public void LoadCATAParticulars()
 {
     string strWrite = "";
     double dblTotal = 0;

     using (clsCATARequest objCATARequest = new clsCATARequest())
     {
         objCATARequest.CataCode = Request.QueryString["catacode"];
         objCATARequest.Fill();
         strHotelName = objCATARequest.HotelName;
     }

     DataTable tblCATAType = clsCATAType.GetDSLUsed(lblCATANo.Text);
     {
         foreach (DataRow drw in tblCATAType.Rows)
         {
             strWrite += "<tr>" +
                          "<td class='GridRowsGray' style='text-align:left;padding:3'><b>" + drw["pText"].ToString() + "</b></td>" +
                         "</tr>";

             DataTable tblRequestDetails = clsCATADetails.GetCATADetails(drw["pValue"].ToString(), lblCATANo.Text);
             {
                 foreach (DataRow drwDetails in tblRequestDetails.Rows)
                 {
                     if (drw["pValue"].ToString() == "01")
                     {

                         strWrite += "<tr>" +
                                      "<td class='GridRows'>" +
                                       "<table width='90%'>" +
                                        "<tr>" +
                                         "<td style='width:60%;text-align:left;text-indent:150px;'>" + clsCATASubtype.GetName(drwDetails["SubTypeCode"].ToString()) + " : " + strHotelName + " (Rate/Day: " + string.Format("{0:n2}", double.Parse(clsCATASettings.GetAmount("01", drwDetails["SubTypeCode"].ToString(), clsEmployee.GetJobGrade(strPayee)))) + ")</td>" +
                                         "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwDetails["Amount"].ToString())) + "</td>" +
                                        "</tr>" +
                                       "</table>" +
                                      "</td>" +
                                     "</tr>" +
                                     "<tr>";
                         //"<td td class='GridRows'>" +
                         //  "<table width='90%'>" +
                         //   "<tr>" +
                         //    "<td style='width:60%;text-indent:250px;'>Rate/Day:</td>" +
                         //    "<td style='width:40%;text-align: right: padding-right:150px;'>" + string.Format("{0:n2}", double.Parse(clsCATASettings.GetAmount("01", drwDetails["SubTypeCode"].ToString(), clsEmployee.GetJobGrade(strPayee)))) + "</td>" +
                         //   "</tr>" +
                         //  "</table>" +
                         // "</td>" +
                         //"</tr>";

                     }
                     else
                     {
                         strWrite += "<tr>" +
                                      "<td class='GridRows'>" +
                                       "<table width='90%'>" +
                                        "<tr>" +
                                         "<td style='width:60%;text-align:left;text-indent:150px;'>" + clsCATASubtype.GetName(drwDetails["SubTypeCode"].ToString()) + "</td>" +
                                         "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwDetails["Amount"].ToString())) + "</td>" +
                                        "</tr>" +
                                       "</table>" +
                                      "</td>" +
                                     "</tr>";
                     }
                     dblTotal = dblTotal + Convert.ToDouble(drwDetails["Amount"].ToString());
                 }
             }
         }
     }

     DataTable tblIncidentals = clsCATAIncedental.GetDSGMainForm(Request.QueryString["catacode"]);
     {
         if (tblIncidentals.Rows.Count != 0)
         {
             strWrite += "<tr>" +
                           "<td class='GridRowsGray' style='text-align:left;padding:3'><b>Incidentals</b></td>" +
                          "</tr>";

             foreach (DataRow drwIncidentals in tblIncidentals.Rows)
             {
                 strWrite += "<tr>" +
                            "<td class='GridRows'>" +
                             "<table width='90%'>" +
                              "<tr>" +
                               "<td style='width:60%;text-align:left;text-indent:150px;'>" + drwIncidentals["incdental"].ToString() + "</td>" +
                               "<td style='width:40%;text-align: right;'>" + string.Format("{0:n2}", double.Parse(drwIncidentals["Amount"].ToString())) + "</td>" +
                              "</tr>" +
                             "</table>" +
                            "</td>" +
                           "</tr>";
                 dblTotal = dblTotal + Convert.ToDouble(drwIncidentals["Amount"].ToString());
             }
         }
         strWrite += "<tr>" +
                            "<td class='GridRowsGray'>" +
                             "<table width='90%'>" +
                              "<tr>" +
                               "<td style='width:60%;text-align:left;'><b>TOTAL</b></td>" +
                               "<td style='width:40%;text-align: right;'><b>P " + string.Format("{0:n2}", dblTotal) + "</b></td>" +
                              "</tr>" +
                             "</table>" +
                            "</td>" +
                           "</tr>";

     }
     Response.Write(strWrite);
 }

 private void LoadDetails()
 {
  using (clsCATARequest objCATARequest = new clsCATARequest())
  {
   objCATARequest.CataCode = Request.QueryString["catacode"];
   objCATARequest.Fill();
   lblCATANo.Text = objCATARequest.CataCode;
   lblPayee.Text = clsEmployee.GetName(objCATARequest.RequestedBy);
   lblRequestOn.Text = objCATARequest.CreateOn.ToString();
   lblDaeNeeded.Text = objCATARequest.DateNeeded.ToShortDateString();
   lblAcquiremode.Text = (objCATARequest.AcquireMode == "P") ? "For Pickup" : "For Deposit";
   lblChargedToSchool.Text = clsSchool.GetSchoolName(objCATARequest.SchoolCode);
   lblChargedToRC.Text = clsRC.GetRCName(objCATARequest.RcCode);
   lblChargedToOthers.Text = objCATARequest.Other;
   lblFrom.Text = objCATARequest.LocationFrom;
   lblTo.Text = objCATARequest.LocationTo;
   lblDays.Text = objCATARequest.NumberOfDays.ToString();
   lblDepartureDate.Text = objCATARequest.Departure.ToShortDateString();
   lblDepartureTime.Text = objCATARequest.Departure.ToShortTimeString();
   lblArrivalDate.Text = objCATARequest.Arrival.ToShortDateString();
   lblArrivalTime.Text = objCATARequest.Arrival.ToShortTimeString();
   lblPurpose.Text = objCATARequest.TripPurpose;
   strHotelName = objCATARequest.HotelName;
   strPayee = objCATARequest.RequestedBy;
   lblOBCode.Text = objCATARequest.ObCode;
  }


 }

 protected void Page_Load(object sender, EventArgs e)
 {
  clsSpeedo.Authenticate();

  if (!Page.IsPostBack)
  {
   if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
   {
    LoadDetails();
    


    if (clsCATARequest.IsForApproval(lblCATANo.Text))
    {
        if (clsCATAApproval.IsForApproval(Request.Cookies["Speedo"]["UserName"], lblCATANo.Text))
        {
            divButtons.Visible = true;
            divButtons2.Visible = true;
        }
        else
        {
            divButtons.Visible = false;
            divButtons2.Visible = false;
        }
    }
    else
    {
        divButtons.Visible = false;
        divButtons2.Visible = false;
    }

   }

   //if (clsSystemModule.HasAccess("023", Request.Cookies["Speedo"]["UserName"].ToString()))
   //{
   // LoadDetails(); 
   // divButtons.Visible = false;
   // divButtons2.Visible = false;
   //}
  }

 }

 protected void btnApprove_Click(object sender, EventArgs e)
 {
   string strapproverType = "";

  if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
  {
   if (clsFinanceApprover.IsCanChangeRequestStatus("catacode", lblCATANo.Text, "CATARequest"))
   {

       if (clsCATAApproval.TagApproved(lblCATANo.Text, Request.Cookies["Speedo"]["UserName"], strPayee) > 0)
       {
           Response.Redirect("FinanceCataMenu.aspx");
       }

       else
       {
           divError.Visible = true;
           lblErrMsg.Text = "The request has been approved, disapproved or cancelled by the user";
       }
   }
  }
 }

 protected void btnDisApprove_Click(object sender, EventArgs e)
 {
  string strapproverType = "";

  if (clsFinanceApprover.IsApprover(Request.Cookies["Speedo"]["UserName"]))
  {
   if (clsFinanceApprover.IsCanChangeRequestStatus("catacode", lblCATANo.Text, "CATARequest"))
   {
       if (clsCATAApproval.TagDisapproved(lblCATANo.Text, Request.Cookies["Speedo"]["UserName"], strPayee) > 0)
       {

           Response.Redirect("FinanceCataMenu.aspx");
       }

       else
       {
           divError.Visible = true;
           lblErrMsg.Text = "The request has been approved, disapproved or cancelled by the user";
       }
   }
  }
 }

 protected void btnBack_Click(object sender, EventArgs e)
 {
     Response.Redirect("FinanceCataMenu.aspx");
 }
}
