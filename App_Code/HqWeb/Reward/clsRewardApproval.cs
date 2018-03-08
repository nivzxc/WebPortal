using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace Reward
    {
        public class clsRewardApproval : IDisposable
        {
            public void Dispose() { GC.SuppressFinalize(this); }
            public clsRewardApproval()
            { }

            public static bool IsApprovedLevel1(int pTransactionCode)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TransactionCode FROM Employee.RewardApproval WHERE Status='1' AND ApproverLevel='1' AND TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
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

            public static DataTable GetDSGForApprovalLevel1(string pRewardApprover1)
            {
                DataTable tblReturn = new DataTable();
                
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TransactionCode,(SELECT Description FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS Description,(SELECT CreateBy FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS CreateBy,(SELECT CreateOn FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS CreateOn,Username,ApproverLevel,Status FROM Employee.RewardApproval AS ER WHERE Status='0' AND ApproverLevel='1' AND Username=@Username AND (SELECT Status FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) = '0'";
                        cmd.Parameters.Add(new SqlParameter("@Username", pRewardApprover1));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGTransaction(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT     TransactionCode, RewardCategoryCode, Description, Status, CreateBy, CreateOn, ModifyBy, ModifyOn FROM Employee.Reward WHERE CreateBy=@CreateBy ORDER BY TransactionCode DESC";
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", pUsername));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);

                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGForApprovalLevel2(string pRewardApprover2)
            {
                DataTable tblReturn = new DataTable();
                tblReturn.Columns.Add("TransactionCode");
                tblReturn.Columns.Add("Username");
                tblReturn.Columns.Add("ApproverLevel");
                tblReturn.Columns.Add("Description");
                tblReturn.Columns.Add("CreateBy");
                tblReturn.Columns.Add("CreateOn");
                tblReturn.Columns.Add("Status");
                DataTable tblTemporary = new DataTable();
                

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {

                        cn.Open();
                        cmd.CommandText = "SELECT TransactionCode,(SELECT Description FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS Description,(SELECT CreateBy FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS CreateBy,(SELECT CreateOn FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) AS CreateOn,Username,ApproverLevel,Status FROM Employee.RewardApproval AS ER WHERE Status='0' AND ApproverLevel='2' AND Username=@Username  AND (SELECT Status FROM Employee.Reward WHERE TransactionCode=er.TransactionCode) = '0'";
                        cmd.Parameters.Add(new SqlParameter("@Username", pRewardApprover2));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        
                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            if (IsApprovedLevel1(dr["TransactionCode"].ToString().ToInt()))
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["TransactionCode"] = dr["TransactionCode"].ToString();
                                drNew["Username"] = dr["Username"].ToString();
                                drNew["ApproverLevel"] = dr["ApproverLevel"].ToString();
                                drNew["Description"] = dr["Description"].ToString();
                                drNew["CreateBy"] = dr["CreateBy"].ToString();
                                drNew["CreateOn"] = dr["CreateOn"].ToString();
                                drNew["Status"] = dr["Status"].ToString();
                                tblReturn.Rows.Add(drNew);
                            }
                        }

                    }
                }
                return tblReturn;
            }

            public static int ApprovedLevel1(string pTransactionCode,string pUsername)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Employee.RewardApproval SET DateApprove=GETDATE(), Status='1' WHERE Username=@Username AND TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();
                    }
                }
                return intReturn;
            }

            public static int DisApprovedLevel1(string pTransactionCode, string pUsername)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Employee.RewardApproval SET DateApprove=GETDATE(), Status='2' WHERE Username=@Username AND TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Employee.Reward SET Status='2', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }
                }
                return intReturn;
            }

            public static int ApprovedLevel2(string pTransactionCode, string pUsername)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Employee.RewardApproval SET DateApprove=GETDATE(), Status='1' WHERE Username=@Username AND TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Employee.Reward SET Status='1', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                return intReturn;
            }

            public static int DisApprovedLevel2(string pTransactionCode, string pUsername)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Employee.RewardApproval SET DateApprove=GETDATE(), Status='2' WHERE Username=@Username AND TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Employee.Reward SET Status='2', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                return intReturn;
            }

            public static int Modification(string pTransactionCode, string pUsername)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Employee.RewardApproval SET Status='0' WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Employee.Reward SET Status='4', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                return intReturn;
            }

            public static int ForModification()
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "SELECT COUNT(TransactionCode) FROM Employee.Reward WHERE Status='4'";
                        intReturn = cmd.ExecuteScalar().ToString().ToInt();
                        cmd.Parameters.Clear();
                    }
                }
                return intReturn;
            }

            public static bool IsEncoder(string pUsername)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT username FROM Users.UsersModules WHERE pstatus='1' AND modlcode='REWARD' AND username=@username";
                        cmd.Parameters.Add(new SqlParameter("@username", pUsername));
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

           public static bool IsApproverHR(string pUsername)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT username FROM HR.DepartmentApprover WHERE username=@username AND deptcode='020'";
                        cmd.Parameters.Add(new SqlParameter("@username", pUsername));
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

        }
    }
}