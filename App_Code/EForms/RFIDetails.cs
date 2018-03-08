using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class RFIDetails
 {
  public RFIDetails()
  {

  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////


  public static DataTable GetDSGPageRFIDetails(string pRFICode)
  {
   DataTable tblReturn = new DataTable();
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM CIS.RFIDetails WHERE rficode=@rficode ORDER BY itemdesc";
    cmd.Parameters.Add(new SqlParameter("@rficode", pRFICode));
    SqlDataAdapter da = new SqlDataAdapter(cmd);
    da.Fill(tblReturn);
   }
   return tblReturn;
  }

 }
}