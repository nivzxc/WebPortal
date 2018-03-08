using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HRMS;
public partial class CMD_SER_dwnSemestralCourses : System.Web.UI.Page
{

   protected void LoadRecords()
   {
      DataTable tblNational = clsSchoolNational.DSNational();
      string strWrite = "";
      string[] strProgramCodeChed = new string[] { "BSCOE", "BSECE", "BSIT", "BSCS", "ACT", "BSHRM", "BSBA", "BSENTREP", "BSOA", "AOM", "BSED", "BSN", "MIT" };
      string[] strProgramCodeTesda = new string[] { "DCET", "DIT", "DMA", "HRA", "HRS", "DENTREP", "DOSM", "PNP", "CNAP", "DPN", "IHC", "DHCS", "DHNA", "DAIT", "DCBB", "CYPROG", "DEP", "HCS", "BASS" };
      int intNetworkNS = clsSER.GetNS();
      int intNetworkOS = clsSER.GetOS();

      strWrite += "<tr>" +
                   "<td><b>Network Total:</b></td>" +
                   "<td><b>" + clsValidator.ZeroToDash(intNetworkNS) + "</b></td>" +
                   "<td><b>" + clsValidator.ZeroToDash(intNetworkOS) + "</b></td>" +
                   "<td><b>" + clsValidator.ZeroToDash(intNetworkNS + intNetworkOS) + "</b></td>";

      foreach (string i in strProgramCodeChed)
      {
         strWrite += "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOGS(i, "1")) + "</b></td>" +
                     "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOGS(i, "2")) + "</b></td>";
      }

      foreach (string i in strProgramCodeTesda)
      {
         strWrite += "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOGS(i, "1")) + "</b></td>" +
                     "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOGS(i, "2")) + "</b></td>";
      }

      strWrite += "</tr>";

      foreach (DataRow drwNational in tblNational.Rows)
      {
         string strNationalCode = drwNational["natlcode"].ToString();
         strWrite += "<tr>" +
                      "<td><b>" + drwNational["natlname"] + "</b></td>" +
                      "<td><b>" + clsValidator.ZeroToDash(clsSER.GetNSRegion(strNationalCode)) + "</b></td>" +
                      "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOSRegion(strNationalCode)) + "</b></td>" +
                      "<td><b>" + clsValidator.ZeroToDash(clsSER.GetOGSRegion(strNationalCode)) + "</b></td>";

         DataTable tblSemProgramDumpNational = clsSER.GetProgramSemestralNational(drwNational["natlcode"].ToString());
         foreach (DataRow drwSPD in tblSemProgramDumpNational.Rows)
         {
            strWrite += "<td><b>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</b></td>" +
                        "<td><b>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</b></td>";
         }

         strWrite += "</tr>";

         DataTable tblSchools = clsSchool.DSSchoolsByNational(drwNational["natlcode"].ToString());
         foreach (DataRow drwSchools in tblSchools.Rows)
         {
            DataTable tblSemProgramDump = new DataTable();
            string strSchoolCode = drwSchools["schlcode"].ToString();
            int intSchoolNS = clsSER.GetNS(strSchoolCode);
            int intSchoolOS = clsSER.GetOS(strSchoolCode);
            int intNS1 = clsSER.GetNS(strSchoolCode, "1");
            int intNS2 = clsSER.GetNS(strSchoolCode, "2");
            int intNS3 = clsSER.GetNS(strSchoolCode, "3");
            int intNS4 = clsSER.GetNS(strSchoolCode, "4");
            int intNS5 = clsSER.GetNS(strSchoolCode, "5");
            int intOS1 = clsSER.GetOS(strSchoolCode, "1");
            int intOS2 = clsSER.GetOS(strSchoolCode, "2");
            int intOS3 = clsSER.GetOS(strSchoolCode, "3");
            int intOS4 = clsSER.GetOS(strSchoolCode, "4");
            int intOS5 = clsSER.GetOS(strSchoolCode, "5");

            // School Details
            strWrite += "<tr>" +
                         "<td>" + drwSchools["schlnam2"].ToString() + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intSchoolNS) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intSchoolOS) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intSchoolNS + intSchoolOS) + "</td>";
            //foreach (string i in strProgramCodeChed)
            //{
            // strWrite += "<td class='GridRowsNum' style='text-align:right;'>" + clsValidator.ZeroToDash(clsSER.GetOGS(strSchoolCode, i, "1")) + "</td>" +
            //             "<td class='GridRowsNum' style='text-align:right;'>" + clsValidator.ZeroToDash(clsSER.GetOGS(strSchoolCode, i, "2")) + "</td>";
            //}
            //foreach (string i in strProgramCodeTesda)
            //{
            // strWrite += "<td class='GridRowsNum' style='text-align:right;'>" + clsValidator.ZeroToDash(clsSER.GetOGS(strSchoolCode, i, "1")) + "</td>" +
            //             "<td class='GridRowsNum' style='text-align:right;'>" + clsValidator.ZeroToDash(clsSER.GetOGS(strSchoolCode, i, "2")) + "</td>";
            //}
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode);
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";


            // First Year
            strWrite += "<tr>" +
                         "<td>&nbsp;&nbsp;&nbsp;&nbsp;1st Year</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS1) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intOS1) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS1 + intOS1) + "</td>";
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode, "1");
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";

            // Second Year
            strWrite += "<tr>" +
                         "<td>&nbsp;&nbsp;&nbsp;&nbsp;2nd Year</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS2) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intOS2) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS2 + intOS2) + "</td>";
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode, "2");
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";

            // Third Year
            strWrite += "<tr>" +
                         "<td>&nbsp;&nbsp;&nbsp;&nbsp;3rd Year</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS3) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intOS3) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS3 + intOS3) + "</td>";
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode, "3");
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";

            // Fouth Year
            strWrite += "<tr>" +
                         "<td>&nbsp;&nbsp;&nbsp;&nbsp;4th Year</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS4) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intOS4) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS4 + intOS4) + "</td>";
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode, "4");
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";

            // Fifth Year
            strWrite += "<tr>" +
                         "<td>&nbsp;&nbsp;&nbsp;&nbsp;5th Year</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS5) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intOS5) + "</td>" +
                         "<td>" + clsValidator.ZeroToDash(intNS5 + intOS5) + "</td>";
            tblSemProgramDump = new DataTable();
            tblSemProgramDump = clsSER.GetProgramSemestral(strSchoolCode, "5");
            foreach (DataRow drwSPD in tblSemProgramDump.Rows)
            {
               strWrite += "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem1"].ToString())) + "</td>" +
                           "<td>" + clsValidator.ZeroToDash(clsValidator.CheckInteger(drwSPD["pSem2"].ToString())) + "</td>";
            }
            strWrite += "</tr>";

         }
      }
      Response.Write(strWrite);
   }

   protected void Page_Load(object sender, EventArgs e)
   {
      Response.Clear();
      Response.AddHeader("content-disposition", "attachment;filename=SemestralCourses.xls");
      Response.Charset = "";
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.ContentType = "application/vnd.xls";
   }

}