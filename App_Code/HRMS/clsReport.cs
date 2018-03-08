using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMS
{
 public class clsReport
 {
  public clsReport() { }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public static DataTable DSGHeadCountReport(DateTime pFocusDate)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("JGCode");
   tblReturn.Columns.Add("PCA");
   tblReturn.Columns.Add("FCA");
   tblReturn.Columns.Add("UCA");
   tblReturn.Columns.Add("PCH");
   tblReturn.Columns.Add("FCH");
   tblReturn.Columns.Add("UCH");
   tblReturn.Columns.Add("PCB");
   tblReturn.Columns.Add("FCB");
   tblReturn.Columns.Add("UCB");

   string strJGCode = "";
   int intPlantillaCountHQ = 0;
   int intPlantillaCountBillable = 0;
   int intPlantillaFilledCountHQ = 0;
   int intPlantillaFilledCountBillable = 0;

   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT jgcode,plntcnth,plntcntb FROM HR.JobGrade WHERE jgcode <> 'NA' ORDER BY jgorder DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     strJGCode = dr["jgcode"].ToString();
     intPlantillaCountHQ = clsValidator.CheckInteger(dr["plntcnth"].ToString());
     intPlantillaCountBillable = clsValidator.CheckInteger(dr["plntcntb"].ToString());
     intPlantillaFilledCountHQ = clsJobGrade.CountPlantillaFilledHQ(strJGCode, pFocusDate);
     intPlantillaFilledCountBillable = clsJobGrade.CountPlantillaFilledBillable(strJGCode, pFocusDate);

     DataRow drwN = tblReturn.NewRow();
     drwN["JGCode"] = strJGCode;
     drwN["PCA"] = intPlantillaCountBillable + intPlantillaCountHQ;
     drwN["FCA"] = intPlantillaFilledCountBillable + intPlantillaFilledCountHQ;
     drwN["UCA"] = (intPlantillaCountBillable + intPlantillaCountHQ) - (intPlantillaFilledCountBillable + intPlantillaFilledCountHQ);
     drwN["PCH"] = intPlantillaCountHQ;
     drwN["FCH"] = intPlantillaFilledCountHQ;
     drwN["UCH"] = intPlantillaCountHQ - intPlantillaFilledCountHQ;
     drwN["PCB"] = intPlantillaCountBillable;
     drwN["FCB"] = intPlantillaFilledCountBillable;
     drwN["UCB"] = intPlantillaCountBillable - intPlantillaFilledCountBillable;
     tblReturn.Rows.Add(drwN);
    }
    dr.Close();
   }
   return tblReturn;
  }

 }
}