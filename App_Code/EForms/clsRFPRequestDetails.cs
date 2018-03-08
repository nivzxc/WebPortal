using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsRFPRequestDetails : IDisposable
 {
  private string _strRequestItemCode;
  private string _strControlNumber;
  private string _strItemDescription;
  private string _strSchoolCode;
  private string _strRcCode;
  private string _strOthers;
  private double _dblAmount;

  public string RequestItemCode { get { return _strRequestItemCode; } set { _strRequestItemCode = value; } }
  public string ControlNumber { get { return _strControlNumber; } set { _strControlNumber = value; } }
  public string ItemDescription { get { return _strItemDescription; } set { _strItemDescription = value; } }
  public string SchoolCode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
  public string RcCode { get { return _strRcCode; } set { _strRcCode = value; } }
  public string Others { get { return _strOthers; } set { _strOthers = value; } }
  public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

  public clsRFPRequestDetails()
  {
   _strRequestItemCode = "";
   _strControlNumber = "";
   _strItemDescription = "";
   _strSchoolCode = "";
   _strRcCode = "";
   _strOthers = "";
   _dblAmount = 0.00;
  }
  public void Dispose() { GC.SuppressFinalize(this); }

  ///////////////////////////
  ///////// Methods /////////
  ///////////////////////////

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ritmcode, ctrlnmbr, itemdesc, schlcode, rccode, others ,amount FROM Finance.RFPRequestDetails WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strRequestItemCode = dr["ritmcode"].ToString();
     _strControlNumber = dr["ctrlnmbr"].ToString();
     _strItemDescription = dr["itemdesc"].ToString();
     _strSchoolCode = dr["schlcode"].ToString();
     _strRcCode = dr["rccode"].ToString();
     _strOthers = dr["others"].ToString();
     _dblAmount = Convert.ToDouble(dr["amount"].ToString());
    }
   }
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////
  public static DataTable GetDSG(string pControlNumber)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT itemdesc,schlcode,rccode,others,amount FROM Finance.RFPRequestDetails WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();

    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}