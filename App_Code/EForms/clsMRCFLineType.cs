using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace STIeForms
{
    public class clsMrcfLineType : IDisposable
    {
        public clsMrcfLineType()
        { }

        public void Dispose() { GC.SuppressFinalize(this); }

        public static DataTable GetDataSourceList()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT LineTypeCode AS pValue, LineTypeName AS pText FROM Oracle.MrcfLineType WHERE Enabled = '1' ORDER BY pText";
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }

            return tblReturn;
        }

        public static DataTable GetDataSourceListLineType(string pDepartmentCode)
        {
            DataTable tblReturn = new DataTable();
            tblReturn.Columns.Add("pValue");
            tblReturn.Columns.Add("pText");

            //Sales
            DataRow dr = tblReturn.NewRow();

            if (pDepartmentCode == "044")
            {
                dr = tblReturn.NewRow();
                dr["pValue"] = "1021";
                dr["pText"] = "Asset";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1";
                dr["pText"] = "Goods";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1043";
                dr["pText"] = "Others";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1022";
                dr["pText"] = "Service";
                tblReturn.Rows.Add(dr);
            }
            //Inventory
            else if (pDepartmentCode == "058" || pDepartmentCode == "019")
            {
                dr = tblReturn.NewRow();
                dr["pValue"] = "1021";
                dr["pText"] = "Asset";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1043";
                dr["pText"] = "Others";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1022";
                dr["pText"] = "Service";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1020";
                dr["pText"] = "Supplies";
                tblReturn.Rows.Add(dr);
            }
            else
            {
                dr = tblReturn.NewRow();
                dr["pValue"] = "1021";
                dr["pText"] = "Asset";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1043";
                dr["pText"] = "Others";
                tblReturn.Rows.Add(dr);

                dr = tblReturn.NewRow();
                dr["pValue"] = "1022";
                dr["pText"] = "Service";
                tblReturn.Rows.Add(dr);
            }

            return tblReturn;
        }

        public static string GetDescription(string pLineTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT LineTypeName FROM Oracle.MrcfLineType WHERE Enabled = '1' AND LineTypeCode=@LineTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }

        public static string GetDestinationTypeCode(string pLineTypeCode)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Destination FROM Oracle.MrcfLineType WHERE enabled='1' AND LineTypeCode=@LineTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    strReturn = cmd.ExecuteScalar().ToString();
                }
            }
            return strReturn;
        }

        public static bool IsHasItemCode(string pLineTypeCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ItemCodeFlag FROM Oracle.MrcfLineType WHERE enabled='1' AND ItemCodeFlag='1' AND LineTypeCode=@LineTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                    //string strResult = cmd.ExecuteScalar().ToString();
                    //if (strResult == "1")
                    //{
                        
                    //}
                }
            }
            return blnReturn;
        }

        public static bool IsHasItemDesc(string pLineTypeCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ItemDescFlag FROM Oracle.MrcfLineType WHERE enabled='1' AND ItemDescFlag='1' AND LineTypeCode=@LineTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                    //string strResult = cmd.ExecuteScalar().ToString();
                    //if (strResult == "1")
                    //{
                    //    blnReturn = true;
                    //}
                }
            }
            return blnReturn;
        }

        public static bool IsHasUnitOfMeasurement(string pLineTypeCode)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT UomFlag FROM Oracle.MrcfLineType WHERE enabled='1' AND UomFlag='1' AND LineTypeCode=@LineTypeCode";
                    cmd.Parameters.Add(new SqlParameter("@LineTypeCode", pLineTypeCode));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                    //string strResult = cmd.ExecuteScalar().ToString();
                    //if (strResult == "1")
                    //{
                    //    blnReturn = true;
                    //}
                }
            }
            return blnReturn;
        }

    }
}