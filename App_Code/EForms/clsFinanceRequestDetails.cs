//using System;
//using System.Web;
//using System.Data;
//using System.Data.SqlClient;

//namespace STIeForms
//{
// public class clsFinanceRequestDetails:IDisposable
// {
//  private string _strRequestItemCode;
//  private string _strControlNumber;
//  private string _strItemDescription;
//  private string _strSchoolCode;
//  private string _strRcCode;
//  private string _strOthers;
//  private double _dblAmount;

//  public string RequestItemCode { get { return _strRequestItemCode; } set { _strRequestItemCode = value; } }
//  public string ControlNumber { get { return _strControlNumber; } set { _strControlNumber=value; } }
//  public string ItemDescription { get { return _strItemDescription; } set { _strItemDescription = value; } }
//  public string SchoolCode { get { return _strSchoolCode; } set { _strSchoolCode = value; } }
//  public string RcCode { get { return _strRcCode; } set { _strRcCode = value; } }
//  public string Others { get { return _strOthers; } set { _strOthers = value; } }
//  public double Amount { get { return _dblAmount; } set { _dblAmount = value; } }

//  public clsFinanceRequestDetails()
//  {
//   _strRequestItemCode = "";
//   _strControlNumber = "";
//   _strItemDescription = "";
//   _strSchoolCode = "";
//   _strRcCode = "";
//   _strOthers = "";
//   _dblAmount = 0.00;
//  }

//  public void Dispose() { GC.SuppressFinalize(this); }

//  ///////////////////////////
//  ///////// Methods /////////
//  ///////////////////////////

//  public void Fill()
//  {
//   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
//   {
//    SqlCommand cmd = cn.CreateCommand();
//    cmd.CommandText = "SELECT ritmcode, ctrlnmbr, itemdesc, schlcode, rccode, others ,amount FROM Finance.RequestDetails WHERE ctrlnmbr=@ctrlnmbr";
//    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
//    cn.Open();
//    SqlDataReader dr = cmd.ExecuteReader();
//    while (dr.Read())
//    {
//     _strRequestItemCode = dr["ritmcode"].ToString();
//     _strControlNumber = dr["ctrlnmbr"].ToString();
//     _strItemDescription = dr["itemdesc"].ToString();
//     _strSchoolCode = dr["schlcode"].ToString();
//     _strRcCode = dr["rccode"].ToString();
//     _strOthers = dr["others"].ToString();
//     _dblAmount = Convert.ToDouble(dr["amount"].ToString());
//    }
//   }
//  }

//  //////////////////////////////////
//  ///////// Static Members /////////
//  //////////////////////////////////
//  public static DataTable GetDSGMainForm00()
//  {
//   DataTable tblReturn = new DataTable();
//   tblReturn.Columns.Add("RequestItemCode");
//   tblReturn.Columns.Add("ControlNumber");
//   tblReturn.Columns.Add("ItemDescription");
//   tblReturn.Columns.Add("schlcode");
//   tblReturn.Columns.Add("rccode");
//   tblReturn.Columns.Add("others");
//   tblReturn.Columns.Add("Amount");
   
//   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
//   {
//    SqlCommand cmd = cn.CreateCommand();
//    cmd.CommandText = "SELECT * FROM Finance.RequestDetails";
//    cn.Open();

//    SqlDataAdapter da = new SqlDataAdapter(cmd);
//    da.Fill(tblReturn);

//    //SqlDataReader dr = cmd.ExecuteReader();
//    //while (dr.Read())
//    //{
//    // DataRow drwNew = tblReturn.NewRow();
//    // drwNew["RequestItemCode"] = dr["ritmcode"].ToString();
//    // drwNew["ControlNumber"] = dr["ctrlnmbr"].ToString();
//    // drwNew["ItemDescription"] = dr["itemdesc"].ToString();
//    // drwNew["ChargeTo"] = dr["schlcode"].ToString();
//    // drwNew["rccode"] = dr["rccode"].ToString();
//    // drwNew["others"] = dr["others"].ToString();
//    // drwNew["Amount"] = Convert.ToDouble(dr["amount"].ToString());
//    // tblReturn.Rows.Add(drwNew);
//    //}
//    //dr.Close();

//   }
//   return tblReturn;
//  }

// }
//}