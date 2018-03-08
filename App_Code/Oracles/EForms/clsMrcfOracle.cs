using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using HRMS;
using STIeForms;
namespace Oracles
{
    public class clsOracleMrcf : IDisposable
    {
        public clsOracleMrcf() { }

        public void Dispose() { GC.SuppressFinalize(this); }

        private string strMRCFCode;

        private string dteCreateDate;
        private int intQuantity;
        private int intUnitPrice;
        private int intPreparerId;
        private int intItemId;
        private int intBatchId;
        private string strItemDescription;
        private int intCategoryId;
        private int intChargeAccountId;
        private string strUOMCode;
        private int intLineTypeId;
        private int intDestinationOrgId;
        private int intDelivertoLocationId;
        private int intRequestorId;
        private DateTime dteDateNeeded;

        private string strDestination;

        public string MrcfCode { get { return strMRCFCode; } set { strMRCFCode = value; } }
        public string CreateDate { get { return dteCreateDate; } set { dteCreateDate = value; } }
        public int Quantity { get { return intQuantity; } set { intQuantity = value; } }
        public int UnitPrice { get { return UnitPrice; } set { intUnitPrice = value; } }
        public int PreparerId { get { return intPreparerId; } set { intPreparerId = value; } }
        public int ItemId { get { return intItemId; } set { intItemId = value; } }
        public int BatchId { get { return intBatchId; } set { intBatchId = value; } }
        public string ItemDescription { get { return strItemDescription; } set { strItemDescription = value; } }
        public int CategoryId { get { return intCategoryId; } set { intCategoryId = value; } }
        public int ChargeAccountId { get { return intChargeAccountId; } set { intChargeAccountId = value; } }
        public string UOMCode { get { return strUOMCode; } set { strUOMCode = value; } }
        public int LineTypeId { get { return intLineTypeId; } set { intLineTypeId = value; } }
        public int DestinationOrgId { get { return intDestinationOrgId; } set { intDestinationOrgId = value; } }
        public int DelivertoLocationId { get { return intDelivertoLocationId; } set { intDelivertoLocationId = value; } }
        public int RequestorId { get { return intRequestorId; } set { intRequestorId = value; } }
        public DateTime DateNeeded { get { return dteDateNeeded; } set { dteDateNeeded = value; } }

        public string Destination { get { return strDestination; } set { strDestination = value; } }


        public static string GetOrganization(int pInventoryItemId)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ORGANIZATION_ID FROM MTL_SYSTEM_ITEMS_FVL WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID AND ORGANIZATION_ID IN ('119','120')  AND ROWNUM <= 1";
                    cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pInventoryItemId));
                    cn.Open();
                    OracleDataReader oDr = cmd.ExecuteReader();
                    if (oDr.Read())
                    {
                        strReturn = oDr["ORGANIZATION_ID"].ToString();
                    }
                }
                return strReturn;
            }
        }

        public static string GetOrganizationLoc(int pOrganizationID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mtl.organization_id, mtl.organization_code, loc.location_id, loc.location_code from hr_locations loc, mtl_parameters mtl where inventory_organization_id =:pinventory_organization_id AND loc.inventory_organization_id = mtl.organization_id";
                    cmd.Parameters.Add(new OracleParameter("pLocationCode", pOrganizationID));
                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["LOCATION_ID"].ToString();
                    }
                }
            }
            return strReturn;
        }

        public int Insert(DataTable pMRCFItems)
        {
            int intReturn = 0;
            string strOrganizationID = string.Empty;
            string strOrganizationLocationID = string.Empty;
            OracleConnection oCn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle);
            SqlConnection sCn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF);
            sCn.Open();
            oCn.Open();
            OracleTransaction tran = oCn.BeginTransaction();
            OracleCommand cmd = oCn.CreateCommand();
            SqlCommand sqlCMD = sCn.CreateCommand();
            cmd.Transaction = tran;
            try
            {

                foreach (DataRow drw in pMRCFItems.Rows)
                {
                    if (drw["LineTypeCode"].ToString() == "1")
                    {
                        //Warehouse Las Piñas
                        // ITEM is GOODS
                        strOrganizationID = "119";
                        strOrganizationLocationID = GetOrganizationLocation("Warehouse Las Piñas");
                    }
                    else if (drw["LineTypeCode"].ToString() == "1020")
                    {
                        //Warehouse HQ
                        //Item is Supplies
                        strOrganizationID = "120";
                        strOrganizationLocationID = GetOrganizationLoc(strOrganizationID.ToInt());
                    }

                    else
                    {
                        //Warehouse Las Piñas
                        strOrganizationID = "119";
                        strOrganizationLocationID = GetOrganizationLocation("Warehouse Las Piñas");
                    }

                    if (drw["ItemCode"].ToString().Length != 0)
                    {
                        cmd.CommandText = "INSERT INTO PO.PO_REQUISITIONS_INTERFACE_ALL (CREATION_DATE,INTERFACE_SOURCE_CODE,SOURCE_TYPE_CODE,REQUISITION_TYPE,DESTINATION_TYPE_CODE, ITEM_DESCRIPTION,QUANTITY, " +
                                          "UNIT_PRICE,AUTHORIZATION_STATUS,BATCH_ID,PREPARER_ID,AUTOSOURCE_FLAG,ITEM_ID,CHARGE_ACCOUNT_ID,CATEGORY_ID,UOM_CODE,LINE_TYPE_ID,DESTINATION_ORGANIZATION_ID, " +
                                          "DELIVER_TO_LOCATION_ID,DELIVER_TO_REQUESTOR_ID,NEED_BY_DATE,ORG_ID, JUSTIFICATION) " +
                                          "VALUES (TO_DATE(:pCREATION_DATE,'dd-mon-yyyy hh24:mi:ss'),:pINTERFACE_SOURCE_CODE,:pSOURCE_TYPE_CODE,:pREQUISITION_TYPE,:pDESTINATION_TYPE_CODE,:pITEM_DESCRIPTION,:pQUANTITY, " +
                                          ":pUNIT_PRICE,:pAUTHORIZATION_STATUS,:pBATCH_ID,:pPREPARER_ID,:pAUTOSOURCE_FLAG,:pITEM_ID,:pCHARGE_ACCOUNT_ID,:pCATEGORY_ID,:pUOM_CODE,:pLINE_TYPE_ID, :pDESTINATION_ORGANIZATION_ID, " +
                                          ":pDELIVER_TO_LOCATION_ID, :pDELIVER_TO_REQUESTOR_ID,TO_DATE(:pNEED_BY_DATE,'dd-mon-yyyy hh24:mi:ss'), :pORG_ID, :pJUSTIFICATION)";




                        cmd.Parameters.Add(new OracleParameter("pCREATION_DATE", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")));
                        cmd.Parameters.Add(new OracleParameter("pINTERFACE_SOURCE_CODE", "REQ_IMPORT"));
                        cmd.Parameters.Add(new OracleParameter("pSOURCE_TYPE_CODE", "VENDOR"));
                        cmd.Parameters.Add(new OracleParameter("pREQUISITION_TYPE", "PURCHASE"));
                        cmd.Parameters.Add(new OracleParameter("pDESTINATION_TYPE_CODE", drw["Destination"].ToString()));
                        cmd.Parameters.Add(new OracleParameter("pITEM_DESCRIPTION", drw["ItemCode"].ToString() != "" ? GetItemDescription(drw["ItemCode"].ToString()) : drw["ItemDesc"].ToString())); //rlasco 09-25-2014
                        cmd.Parameters.Add(new OracleParameter("pQUANTITY", Convert.ToInt32(drw["Quantity"])));
                        cmd.Parameters.Add(new OracleParameter("pUNIT_PRICE", 1));
                        cmd.Parameters.Add(new OracleParameter("pAUTHORIZATION_STATUS", "INCOMPLETE"));
                        cmd.Parameters.Add(new OracleParameter("pBATCH_ID", BatchId));
                        cmd.Parameters.Add(new OracleParameter("pPREPARER_ID", clsOracleMrcf.GetPersonId(clsOracleMrcf.GetEmpNumber(drw["proccode"].ToString()))));
                        cmd.Parameters.Add(new OracleParameter("pAUTOSOURCE_FLAG", "P"));
                        cmd.Parameters.Add(new OracleParameter("pITEM_ID", Convert.ToInt64(drw["ItemCode"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pCHARGE_ACCOUNT_ID", GetCombinationID(drw["GLAccount"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pCATEGORY_ID", Convert.ToInt64(drw["AssetCode"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pUOM_CODE", drw["Unit"].ToString()));
                        cmd.Parameters.Add(new OracleParameter("pLINE_TYPE_ID", Convert.ToInt64(drw["LineTypeCode"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pDESTINATION_ORGANIZATION_ID", strOrganizationID.ToInt()));
                        cmd.Parameters.Add(new OracleParameter("pDELIVER_TO_LOCATION_ID", strOrganizationLocationID.ToInt()));
                        cmd.Parameters.Add(new OracleParameter("pDELIVER_TO_REQUESTOR_ID", clsOracleMrcf.GetPersonId(clsOracleMrcf.GetEmpNumber(drw["Uname"].ToString()))));
                        cmd.Parameters.Add(new OracleParameter("pNEED_BY_DATE", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")));
                        cmd.Parameters.Add(new OracleParameter("pORG_ID", 81));
                        cmd.Parameters.Add(new OracleParameter("pJUSTIFICATION", drw["MRCFCode"].ToString()));
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO PO.PO_REQUISITIONS_INTERFACE_ALL (CREATION_DATE,INTERFACE_SOURCE_CODE,SOURCE_TYPE_CODE,REQUISITION_TYPE,DESTINATION_TYPE_CODE, ITEM_DESCRIPTION,QUANTITY, " +
                                          "UNIT_PRICE,AUTHORIZATION_STATUS,BATCH_ID,PREPARER_ID,AUTOSOURCE_FLAG,CHARGE_ACCOUNT_ID,CATEGORY_ID,UOM_CODE,LINE_TYPE_ID,DESTINATION_ORGANIZATION_ID, " +
                                          "DELIVER_TO_LOCATION_ID,DELIVER_TO_REQUESTOR_ID,NEED_BY_DATE,ORG_ID, JUSTIFICATION) " +
                                          "VALUES (TO_DATE(:pCREATION_DATE,'dd-mon-yyyy hh24:mi:ss'),:pINTERFACE_SOURCE_CODE,:pSOURCE_TYPE_CODE,:pREQUISITION_TYPE,:pDESTINATION_TYPE_CODE,:pITEM_DESCRIPTION,:pQUANTITY, " +
                                          ":pUNIT_PRICE,:pAUTHORIZATION_STATUS,:pBATCH_ID,:pPREPARER_ID,:pAUTOSOURCE_FLAG,:pCHARGE_ACCOUNT_ID,:pCATEGORY_ID,:pUOM_CODE,:pLINE_TYPE_ID, :pDESTINATION_ORGANIZATION_ID, " +
                                          ":pDELIVER_TO_LOCATION_ID, :pDELIVER_TO_REQUESTOR_ID,TO_DATE(:pNEED_BY_DATE,'dd-mon-yyyy hh24:mi:ss'), :pORG_ID, :pJUSTIFICATION)";

                        cmd.Parameters.Add(new OracleParameter("pCREATION_DATE", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")));
                        cmd.Parameters.Add(new OracleParameter("pINTERFACE_SOURCE_CODE", "REQ_IMPORT"));
                        cmd.Parameters.Add(new OracleParameter("pSOURCE_TYPE_CODE", "VENDOR"));
                        cmd.Parameters.Add(new OracleParameter("pREQUISITION_TYPE", "PURCHASE"));
                        cmd.Parameters.Add(new OracleParameter("pDESTINATION_TYPE_CODE", "EXPENSE"));
                        cmd.Parameters.Add(new OracleParameter("pITEM_DESCRIPTION", drw["ItemCode"].ToString() != "" ? GetItemDescription(drw["ItemCode"].ToString()) : drw["ItemDesc"].ToString())); //rlasco 09 25 2014
                        cmd.Parameters.Add(new OracleParameter("pQUANTITY", Convert.ToInt32(drw["Quantity"])));
                        cmd.Parameters.Add(new OracleParameter("pUNIT_PRICE", 1));
                        cmd.Parameters.Add(new OracleParameter("pAUTHORIZATION_STATUS", "INCOMPLETE"));
                        cmd.Parameters.Add(new OracleParameter("pBATCH_ID", BatchId));
                        cmd.Parameters.Add(new OracleParameter("pPREPARER_ID", clsOracleMrcf.GetPersonId(clsOracleMrcf.GetEmpNumber(drw["proccode"].ToString()))));
                        cmd.Parameters.Add(new OracleParameter("pAUTOSOURCE_FLAG", "P"));
                        cmd.Parameters.Add(new OracleParameter("pCHARGE_ACCOUNT_ID",GetCombinationID(drw["GLAccount"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pCATEGORY_ID", Convert.ToInt16(drw["AssetCode"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pUOM_CODE", drw["Unit"].ToString()));
                        cmd.Parameters.Add(new OracleParameter("pLINE_TYPE_ID", Convert.ToInt16(drw["LineTypeCode"].ToString())));
                        cmd.Parameters.Add(new OracleParameter("pDESTINATION_ORGANIZATION_ID", strOrganizationID.ToInt()));
                        cmd.Parameters.Add(new OracleParameter("pDELIVER_TO_LOCATION_ID", strOrganizationLocationID.ToInt()));
                        cmd.Parameters.Add(new OracleParameter("pDELIVER_TO_REQUESTOR_ID", clsOracleMrcf.GetPersonId(clsOracleMrcf.GetEmpNumber(drw["Uname"].ToString()))));
                        cmd.Parameters.Add(new OracleParameter("pNEED_BY_DATE", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")));
                        cmd.Parameters.Add(new OracleParameter("pORG_ID", 81));
                        cmd.Parameters.Add(new OracleParameter("pJUSTIFICATION", drw["MRCFCode"].ToString()));
                    }
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                oCn.Close();
            }
            return intReturn;
        }

        public static int GetCombinationID(string pGLAccount)
        {
            int intReturn = 0;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CODE_COMBINATION_ID FROM APPS.GL_CODE_COMBINATIONS_KFV WHERE CONCATENATED_SEGMENTS=:CONCATENATED_SEGMENTS";
                    cmd.Parameters.Add(new OracleParameter("@CONCATENATED_SEGMENTS", pGLAccount));
                    cn.Open();
                    intReturn = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return intReturn;
            }

        }


        public string CheckLength(string pProjectTitle)
        {
            string strReturn = "";
            var intLength = 50;
            if ((pProjectTitle.Length > intLength))
            {
                strReturn = pProjectTitle.Substring(0, intLength) + "...";
            }
            else
            {
                strReturn = pProjectTitle;
            }
            return strReturn;
        }

        public static DataTable GetMRCFDetails(string pMRCFCode)
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CIS.MRCF.mrcfcode as MRCFCode, CIS.MRCF.datereq as DateReq,CIS.MRCF.username as Uname, CIS.MRCFDETAILS.qty As Quantity,CIS.MRCFDETAILS.asstcode As AssetCode, CIS.MRCFDETAILS.ltypcode AS LineTypeCode," +
                                      "CIS.MRCFDETAILS.itemcode as ItemCode, CIS.MRCFDETAILS.ItemDesc as ItemDesc,CIS.MRCFDETAILS.unit as Unit,MRCFDETAILS.GLAccount as GLAccount,MRCFDETAILS.Destination as Destination,CIS.MRCFDETAILS.dateneed as DateEnd,CIS.MRCF.proccode as proccode  " +
                                      "FROM cis.mrcf inner join cis.mrcfdetails on CIS.MRCF.mrcfcode = cis.mrcfdetails.mrcfcode WHERE CIS.MRCF.mrcfcode ='" + pMRCFCode + "'";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);

                }
            }
            return tblReturn;
        }

        public static DataTable GetRequisitionDetails()
        {
            DataTable tblReturn = new DataTable();
            using (SqlConnection cn = new SqlConnection("data source=thebar; initial catalog=mystihq; user id=sa; password=thebar"))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "Select CIS.Requisition.datereq AS DateReq, CIS.Requisitiondetails.qty As Quantity,CIS.Requisitiondetails.price AS UnitPrice, " +
                                      "CIS.Requisition.username AS UserName,CIS.Requisitiondetails.itemcode AS ItmCode,CIS.Requisitiondetails.itemdesc AS ItmDesc, " +
                                      "CIS.Requisitiondetails.unit AS Unit,CIS.Requisition.rccode As Rccode,CIS.Requisition.username AS Uname " +
                                      " FROM CIS.Requisition,CIS.Requisitiondetails WHERE CIS.Requisition.requcode = CIS.Requisitiondetails.requcode AND  CIS.Requisition.suppstat='A'";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tblReturn);

                }
            }
            return tblReturn;
        }

        public static string GetExpenseAccount(int pItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT EXPENSE_ACCOUNT FROM INV.MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID";
                    cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pItemID));
                    cn.Open();
                    try
                    {
                        //strReturn = cmd.ExecuteScalar().ToString();
                        strReturn = "2005";
                    }
                    catch { cn.Close(); }
                    finally { cn.Close(); }
                }
            }
            return strReturn;
        }

        public static DataTable GetCategories()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CATEGORY_ID as pValue, DESCRIPTION as pText FROM INV.MTL_CATEGORIES_TL";
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tblReturn);
                }
            }
            return tblReturn;
        }

        public static string GetCategoryID(int pItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CATEGORY_ID FROM INV.MTL_ITEM_CATEGORIES WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID";
                    cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pItemID));
                    cn.Open();
                    try
                    {
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                    catch { cn.Close(); }
                    finally { cn.Close(); }
                }
            }
            return strReturn;
        }

        public static int GetPersonId(string pEmpNumber)
        {
            int intReturn = 0;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    //cmd.CommandText = "SELECT PERSON_ID FROM HR.PER_ALL_PEOPLE_F WHERE KNOWN_AS=:pKNOWN_AS";
                    cmd.CommandText = "SELECT PERSON_ID FROM HR.PER_ALL_PEOPLE_F WHERE EMPLOYEE_NUMBER=:pEMPLOYEE_NUMBER";
                    cmd.Parameters.Add(new OracleParameter("pEMPLOYEE_NUMBER", pEmpNumber));
                    //cmd.Parameters.Add(new OracleParameter("pKNOWN_AS", pEmployeeNumber));
                    cn.Open();
                    OracleDataReader oDr = cmd.ExecuteReader();
                    if (oDr.Read())
                    {
                        intReturn = Convert.ToInt32(oDr["PERSON_ID"].ToString());
                    }
                }
                return intReturn;
            }
        }

        public static string GetEmpNumber(string pUserName)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT empnum FROM HR.Employees WHERE username=@username";
                    cmd.Parameters.Add(new SqlParameter("@username", pUserName));
                    cn.Open();
                    {
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            return strReturn;
        }

        public static int GetBatchId()
        {
            int intReturn = 0;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MAX(BATCH_ID) as batch_id FROM PO.PO_REQUISITIONS_INTERFACE_ALL";
                    cn.Open();
                    OracleDataReader oDr = cmd.ExecuteReader();
                    if (oDr.Read())
                    {
                        intReturn = Convert.ToInt32(oDr["batch_id"].ToString()) + 1;
                    }
                }
                return intReturn;
            }
        }

        public static string GetItemDescription(string pItemID)
        {
                    string strReturn = "";
            if (pItemID != "")
           {
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DESCRIPTION as pText FROM MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID = '" + pItemID + "'";
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
            }
            else { strReturn = ""; }
            return strReturn;
        }

        public static string GetPrimaryUOM(string pItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT PRIMARY_UOM_CODE FROM MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID";
                cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pItemID));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
            return strReturn;
        }

        public static DataTable GetDSLUOM(string pItemID)
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT PRIMARY_UOM_CODE AS pValue,(SELECT UNIT_OF_MEASURE_TL FROM MTL_UNITS_OF_MEASURE_TL WHERE LANGUAGE = 'US' AND UOM_CODE=MTL_SYSTEM_ITEMS_B.PRIMARY_UOM_CODE) AS pText FROM MTL_SYSTEM_ITEMS_B WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID";
                cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pItemID));
                cn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(tblReturn);
                
            }
            return tblReturn;
        }

        public static DataTable GetEmpProc(string pItemID)
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "";
                cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pItemID));
                cn.Open();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(tblReturn);

            }
            return tblReturn;
        }



        public static string GetUOMClass(string pUnitofMeasurement)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT UOM_CLASS FROM MTL_UNITS_OF_MEASURE_TL WHERE UOM_CODE=:pUNIT_OF_MEASURE";
                cmd.Parameters.Add(new OracleParameter("pUNIT_OF_MEASURE", pUnitofMeasurement));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
            return strReturn;
        }

        public static string GetUOMName(string pUOMCode)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT UNITS_OF_MEASURE_TL FROM MTL_UNITS_OF_MEASURE_TL WHERE UOM_CODE=:pUOM_CODE";
                cmd.Parameters.Add(new OracleParameter("pUOM_CODE", pUOMCode));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
            return strReturn;
        }

        public static DataTable GetMrcfUnit(string pUOMClass)
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT UOM_CODE as pValue, UNIT_OF_MEASURE_TL as pText FROM MTL_UNITS_OF_MEASURE_TL WHERE UOM_CLASS=:pUOM_CLASS ORDER BY pText";
                cmd.Parameters.Add(new OracleParameter("pUOM_CLASS", pUOMClass));
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetMrcfUnitAll()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT UOM_CODE as pValue, UNIT_OF_MEASURE_TL as pText FROM MTL_UNITS_OF_MEASURE_TL ORDER BY pText";
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetMRCF(string pBatchNumber)
        {
            string strReturn = "";
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mrcfcode FROM CIS.MrcfBatch WHERE btchcode=@btchcode";
                    cmd.Parameters.Add(new SqlParameter("@btchcode", pBatchNumber));
                    cn.Open();
                    try
                    {
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                    catch
                    { }
                }
            }
            return strReturn;
        }

        public static bool IsBatchVoid(string pBatchNumber)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT btchcode FROM CIS.MrcfBatchVoid WHERE btchcode=@btchcode";
                    cmd.Parameters.Add(new SqlParameter("@btchcode", pBatchNumber));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }

        public static bool IsExist(string pBatchNumber)
        {
            bool blnReturn = false;
            using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mrcfcode FROM CIS.MrcfBatch WHERE btchcode=@btchcode";
                    cmd.Parameters.Add(new SqlParameter("@btchcode", pBatchNumber));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }

        #region Connect to Oracle

        public static DataTable GetMRCFForImport()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DISTINCT BATCH_ID FROM PO_REQUISITIONS_INTERFACE_ALL WHERE PROCESS_FLAG IS NULL ORDER BY BATCH_ID DESC";
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tblReturn);

                }
            }
            return tblReturn;
        }

        public static DataTable GetMRCFError()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DISTINCT BATCH_ID FROM PO_REQUISITIONS_INTERFACE_ALL WHERE TRANSACTION_ID IS NOT Null ORDER BY BATCH_ID DESC";
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tblReturn);

                }
            }
            return tblReturn;
        }

        public static DataTable GetDataSourceListLineType()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT LINE_TYPE_ID AS pValue,LINE_TYPE AS pText FROM PO.PO_LINE_TYPES_TL ORDER BY pText";
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDDLSourceMrcfAssetType()
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT CATEGORY_ID AS pValue,DESCRIPTION AS pText FROM INV.MTL_CATEGORIES_TL WHERE DESCRIPTION <> 'Null' ORDER BY pText";
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static DataTable GetDataSourceListItems(string pLineType, string pCategoryID)
        {
            DataTable tblReturn = new DataTable();
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();

                //GOODS - Las Pinas Warehouse retrieve
                if (pLineType == "1")
                {
                    if (pCategoryID != "ALL")
                    {
                        cmd.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,(SEGMENT1  || ' - ' || DESCRIPTION) as pText FROM MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID IN ('119') AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID IN ('119')  AND CATEGORY_ID = :CATEGORY_ID) ORDER BY pText";
                        cmd.Parameters.Add(new OracleParameter("CATEGORY_ID", pCategoryID));
                    }
                    else
                    {
                        cmd.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,(SEGMENT1  || ' - ' || DESCRIPTION) as pText FROM MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID IN ('119') AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID IN ('119')  AND CATEGORY_ID NOT IN ('2123','2124','2125','2126')) ORDER BY pText";
                    }
                }
                //Supplies - HQ Warehouse retrieve
                else
                {
                    if (pCategoryID != "ALL")
                    {
                        cmd.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,(SEGMENT1  || ' - ' || DESCRIPTION) as pText FROM MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID IN ('120') AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID IN ('120')  AND CATEGORY_ID = :CATEGORY_ID) ORDER BY pText";
                        cmd.Parameters.Add(new OracleParameter("CATEGORY_ID", pCategoryID));
                    }
                    else
                    {
                        cmd.CommandText = "SELECT DISTINCT(INVENTORY_ITEM_ID)  as pValue,(SEGMENT1  || ' - ' || DESCRIPTION) as pText FROM MTL_SYSTEM_ITEMS_B WHERE ORGANIZATION_ID IN ('120') AND INVENTORY_ITEM_ID IN (SELECT INVENTORY_ITEM_ID FROM INV.MTL_ITEM_CATEGORIES WHERE ORGANIZATION_ID IN ('120')  AND CATEGORY_ID IN ('2123','2124','2125')) ORDER BY pText";
                    }
                }
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                da.Fill(tblReturn);
            }
            return tblReturn;
        }

        public static string GetCategoryId(string pInventoryItemID)
        {
            string strReturn = "";
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                OracleCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT CATEGORY_ID FROM INV.MTL_ITEM_CATEGORIES WHERE INVENTORY_ITEM_ID=:pINVENTORY_ITEM_ID AND ROWNUM < 2 ORDER BY CATEGORY_ID DESC";
                cmd.Parameters.Add(new OracleParameter("pINVENTORY_ITEM_ID", pInventoryItemID));
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
            return strReturn;
        }

        public static string GetOrganizationId(string pWarehouseName)
        {
            string strReturn = string.Empty;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mtl.organization_id, mtl.organization_code, loc.location_id, loc.location_code from hr_locations loc, mtl_parameters mtl where inventory_organization_id in (120,119) AND loc.inventory_organization_id = mtl.organization_id AND loc.location_code = :pLocationCode";
                    cmd.Parameters.Add(new OracleParameter("pLocationCode", pWarehouseName));
                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["ORGANIZATION_ID"].ToString();
                    }
                }
            }
            return strReturn;
        }

        public static string GetOrganizationLocation(string pWarehouseName)
        {
            string strReturn = string.Empty;
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mtl.organization_id, mtl.organization_code, loc.location_id, loc.location_code from hr_locations loc, mtl_parameters mtl where inventory_organization_id in (120,119) AND loc.inventory_organization_id = mtl.organization_id AND loc.location_code = :pLocationCode";
                    cmd.Parameters.Add(new OracleParameter("pLocationCode", pWarehouseName));
                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        strReturn = dr["LOCATION_ID"].ToString();
                    }
                }
            }
            return strReturn;
        }

        public static string GetGLCodeCombination(string pGLAccountCombination)
        {
            string strReturn = string.Empty;
            try
            {
                using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
                {
                    using (OracleCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT CODE_COMBINATION_ID FROM GL_CODE_COMBINATIONS_KFV WHERE CONCATENATED_SEGMENTS=:pCONCATENATED_SEGMENTS";
                        cmd.Parameters.Add(new OracleParameter("pCONCATENATED_SEGMENTS", pGLAccountCombination));
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            strReturn = dr["CODE_COMBINATION_ID"].ToString();
                        }
                    }
                }
            }

            catch { }

            return strReturn;
        }

        public static bool IsHasEmployeeDataOnOracle(string pUsername)
        {
            bool blnReturn = false;
            string strEmployeeNumber = clsEmployee.GetEmployeeNumber(pUsername);
            using (OracleConnection cn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT PERSON_ID FROM HR.PER_ALL_PEOPLE_F WHERE EMPLOYEE_NUMBER=:pEMPLOYEE_NUMBER";
                    cmd.Parameters.Add(new OracleParameter("pEMPLOYEE_NUMBER", strEmployeeNumber));
                    cn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        blnReturn = true;
                    }
                }
            }
            return blnReturn;
        }
        # endregion

        public static bool IsOracleUp()
        {
            bool blnReturn = false;
            using (OracleConnection testConn = new OracleConnection(clsSystemConfigurations.ConnectionStringOracle))
            {
                try
                {
                    testConn.Open();
                    blnReturn = true;
                    testConn.Close();
                }
                catch (OracleException)
                {
                    blnReturn = false;
                }

            }
            return blnReturn;
        }

       
    }
}