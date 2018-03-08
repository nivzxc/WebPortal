using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace STIeForms
{
   public class clsRequisitonItemCategory : IDisposable
   {
      private string strItemCategoryCode;
      private string strItemcategoryName;

      public string ItemCategoryCode { get { return strItemCategoryCode; } set { strItemCategoryCode = value; } }
      public string ItemCategoryName { get { return strItemcategoryName; } set { strItemcategoryName = value; } }

      public clsRequisitonItemCategory()
      {
         strItemCategoryCode = "";
         strItemcategoryName = "";
      }

      public void Dispose() { GC.SuppressFinalize(this); }

      public static DataTable GetDSL()
      {
         DataTable tblReturn = new DataTable();
         using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
         {
            using (OracleCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT CATEGORY_ID as pValue, DESCRIPTION as pText FROM MTL_CATEGORIES_TL WHERE DESCRIPTION <> 'Null' AND CATEGORY_ID IN (SELECT CATEGORY_ID FROM MTL_ITEM_CATEGORIES WHERE CATEGORY_ID = MTL_CATEGORIES_TL.CATEGORY_ID) ORDER BY pText";
               cn.Open();
               OracleDataAdapter da = new OracleDataAdapter(cmd);
               try { da.Fill(tblReturn); }
               catch { cn.Dispose(); }
               finally { cn.Dispose(); }
            }
         }
         return tblReturn;
      }

      public static DataTable GetDSLItems(string pCategoryID)
      {
          DataTable tblReturn = new DataTable();
          using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
          {
              using (OracleCommand cmd = cn.CreateCommand())
              {

                  if (pCategoryID != "ALL")
                  {
                      cmd.CommandText = "SELECT DISTINCT INVENTORY_ITEM_ID AS pValue, DESCRIPTION pText FROM INV.MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID='120' AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID='120' AND CATEGORY_ID=:pCATEGORY_ID) ORDER by pText";
                      cmd.Parameters.Add(new OracleParameter("pCATEGORY_ID", pCategoryID));
                  }
                  else
                  {
                      cmd.CommandText = "SELECT DISTINCT INVENTORY_ITEM_ID AS pValue, DESCRIPTION pText FROM INV.MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID='120' AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID='120' AND CATEGORY_ID IN (2123,2124,2125,2126)) ORDER by pText";
                  }
                  cn.Open();
                  OracleDataAdapter da = new OracleDataAdapter(cmd);
                  try { da.Fill(tblReturn); }
                  catch { cn.Dispose(); }
                  finally { cn.Dispose(); }
              }
          }
          return tblReturn;
      }

      public static DataTable GetDSLReq()
      {
          DataTable tblReturn = new DataTable();
          using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
          {
              using (SqlCommand cmd = cn.CreateCommand())
              {
                  cmd.CommandText = "SELECT DISTINCT TransactionTypeCode AS pValue, SubCategoryName AS pText FROM Oracle.MrcfItems WHERE Enabled='1' AND IsRequisition='1' ORDER by pText";
                  cn.Open();
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  try { da.Fill(tblReturn); }
                  catch { cn.Dispose(); }
                  finally { cn.Dispose(); }
              }
          }
          return tblReturn;
      }

      public static DataTable GetDSLReqSub(string pCategoryName)
      {
          DataTable tblReturn = new DataTable();
          using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringRequisition))
          {
              using (SqlCommand cmd = cn.CreateCommand())
              {
                  cmd.CommandText = "SELECT TransactionTypeCode AS pValue, SubCategoryName AS pText FROM  Oracle.MrcfItems WHERE Enabled='1' AND CategoryName = @categoryname  ORDER by pText";
                  cmd.Parameters.Add(new SqlParameter("@categoryname", pCategoryName));
                  cn.Open();
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  try { da.Fill(tblReturn); }
                  catch { cn.Dispose(); }
                  finally { cn.Dispose(); }
              }
          }
          return tblReturn;
      }

   }
}