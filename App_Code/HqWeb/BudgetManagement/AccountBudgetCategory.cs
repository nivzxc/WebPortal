using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for AccountBudgetCategory
/// </summary>
public class AccountBudgetCategory
{

	public AccountBudgetCategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetDSLBudgetCategory()
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
                cmd.CommandText = "SELECT accnt_bud_cat_code AS pValue, accnt_bud_cat_name AS pText FROM Budget.AccountBudgetCategory WHERE record_status='1' ORDER BY accnt_bud_cat_name ASC";
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

    public static DataTable GetDSLBudgetCategoryWithoutInitialBudget()
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
                cmd.CommandText = "SELECT accnt_bud_cat_code AS pValue, accnt_bud_cat_name AS pText FROM Budget.AccountBudgetCategory WHERE record_status='1' AND accnt_bud_cat_code NOT IN ('01') ORDER BY accnt_bud_cat_name ASC";
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
}