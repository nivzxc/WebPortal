using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for AccountCategoryType
/// </summary>
public class AccountCategoryType
{
	public AccountCategoryType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetDSLAccountCategoryType()
    {
        DataTable tblReturn = new DataTable();
        DataTable tblTemporary = new DataTable();

        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");

        DataRow drNew = tblReturn.NewRow();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_cat_type_code AS pValue, accnt_cat_type_name AS pText FROM Budget.AccountCategoryType WHERE record_status='1' ORDER BY accnt_cat_type_name ASC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblTemporary);

                foreach (DataRow dr in tblTemporary.Rows)
                {
                    drNew = tblReturn.NewRow();
                    drNew["pValue"] = dr["pValue"].ToString();
                    drNew["pText"] = dr["pText"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
        }

        return tblReturn;
    }

    public static DataTable GetDSG()
    {
        DataTable tblReturn = new DataTable();
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT accnt_cat_type_code,accnt_cat_type_name FROM Budget.AccountCategoryType ORDER BY Accnt_cat_type_name ASC";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }
}