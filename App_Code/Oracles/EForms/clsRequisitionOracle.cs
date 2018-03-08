using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Oracles
{
    public class clsRequisitionOracle : IDisposable
    {
        public clsRequisitionOracle()
        { }


        public void Dispose() { GC.SuppressFinalize(this); }

        public static string LoadItemUOM(string pCategoryID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT INVENTORY_ITEM_ID AS pValue, DESCRIPTION AS pText, ITEM_COST AS pPrice,MATERIAL_COST AS pMPrice, PRIMARY_UOM_CODE as pUnit FROM CST_ITEM_COST_TYPE_V WHERE INVENTORY_ITEM_ID = :INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
                    cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pCategoryID));

                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                            strReturn = dr["pUnit"].ToString();

                    }
                }
            }
            return strReturn;
        
        }

        //public static string LoadItemUOM(string pCategoryID, string pCostTypeID)
        //{
        //    string strReturn = "";
        //    using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
        //    {
        //        using (OracleCommand cmd = cn.CreateCommand())
        //        {
        //            cmd.CommandText = "SELECT INVENTORY_ITEM_ID AS pValue, DESCRIPTION AS pText, ITEM_COST AS pPrice,MATERIAL_COST AS pMPrice, PRIMARY_UOM_CODE as pUnit FROM CST_ITEM_COST_TYPE_V WHERE  DEFAULT_COST_TYPE_ID= :DEFAULT_COST_TYPE_ID AND INVENTORY_ITEM_ID = :INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
        //            cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pCategoryID));
        //            cmd.Parameters.Add(new OracleParameter("DEFAULT_COST_TYPE_ID", pCostTypeID));

        //            cn.Open();
        //            OracleDataReader dr = cmd.ExecuteReader();
        //            if (dr.Read())
        //            {

        //                strReturn = dr["pUnit"].ToString();

        //            }
        //        }
        //    }
        //    return strReturn;

        //}

        public static string LoadItemUOM(string pCategoryID, string pCostTypeID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT INVENTORY_ITEM_ID AS pValue, DESCRIPTION AS pText, ITEM_COST AS pPrice, MATERIAL_COST AS pMPrice, (CASE WHEN (SELECT UOM_CODE FROM INV.MTL_UOM_CONVERSIONS WHERE INVENTORY_ITEM_ID = CST_ITEM_COST_TYPE_V.INVENTORY_ITEM_ID) IS NOT NULL THEN (SELECT UOM_CODE FROM INV.MTL_UOM_CONVERSIONS WHERE INVENTORY_ITEM_ID = CST_ITEM_COST_TYPE_V.INVENTORY_ITEM_ID) ELSE PRIMARY_UOM_CODE END) as pUnit  FROM CST_ITEM_COST_TYPE_V WHERE  DEFAULT_COST_TYPE_ID= :DEFAULT_COST_TYPE_ID AND INVENTORY_ITEM_ID = :INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
                    cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pCategoryID));
                    cmd.Parameters.Add(new OracleParameter("DEFAULT_COST_TYPE_ID", pCostTypeID));

                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["pUnit"].ToString();
                    }
                }
            }
            return strReturn;
        }


        public static double LoadItemCost(string pCategoryID)
        {
            double dblReturn = 0;
            dblReturn = LoadtCost(pCategoryID, "1000");

            if (dblReturn <= 0)
            {
                dblReturn = LoadtCost(pCategoryID, "2");
            }

            return dblReturn;
        }

        public static double LoadtCost(string pCategoryID, string pCostTypeID)
        {
            double dblReturn = 0;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT INVENTORY_ITEM_ID AS pValue, DESCRIPTION AS pText,ITEM_COST, NVL(TO_CHAR(ITEM_COST,'999,999,999.99'),'0') AS  pPrice, NVL(MATERIAL_COST,'0') AS pMPrice, PRIMARY_UOM_CODE as pUnit FROM CST_ITEM_COST_TYPE_V WHERE  DEFAULT_COST_TYPE_ID= :DEFAULT_COST_TYPE_ID AND INVENTORY_ITEM_ID = :INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
                    cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pCategoryID));
                    cmd.Parameters.Add(new OracleParameter("DEFAULT_COST_TYPE_ID", pCostTypeID));
                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (pCostTypeID == "2")
                        {
                            if (double.Parse(dr["pPrice"].ToString()) > 0)
                            {
                                dblReturn = double.Parse(dr["pPrice"].ToString());
                            }
                        }
                        else
                        {
                            if (double.Parse(dr["pPrice"].ToString()) > 0)
                            {
                                dblReturn = double.Parse(dr["pMPrice"].ToString());
                            }
                        }

                    }
                }
            }
            return dblReturn;
        }

        public static string GetItemNumber(string pInventoryItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT SEGMENT1 FROM INV.MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID=:INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
                    cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pInventoryItemID));
                    cn.Open();
                    try
                    {
                        strReturn = cmd.ExecuteOracleScalar().ToString();
                    }
                    catch 
                    {
                        strReturn = "None";
                    }
                }
            }
            return strReturn;
        }

        public static string GetItemName(string pInventoryItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DESCRIPTION FROM INV.MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID=:INVENTORY_ITEM_ID AND ORGANIZATION_ID = '120'";
                    cmd.Parameters.Add(new OracleParameter("INVENTORY_ITEM_ID", pInventoryItemID));
                    cn.Open();
                    try
                    {
                        strReturn = cmd.ExecuteOracleScalar().ToString();
                    }
                    catch
                    {
                        strReturn = "None";
                    }
                }
            }
            return strReturn;
        }



    }
}