using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace Reward
    {
        public class clsReward : IDisposable
        {
            public void Dispose() { GC.SuppressFinalize(this); }

            public clsReward()
            { }

            public static string ApproverLevel1 { get { return "liezel.diego"; } }
            public static string ApproverLevel2 { get { return "jay.jamandre"; } }

            private int _intTransactionCode;
            private int _intRewardCategoryCode;
            private string _strDescription;
            private string _strStatus;
            private string _strCreateBy;
            private DateTime _dteCreateOn;
            private string _strModifyBy;
            private DateTime _dteModifyOn;


            public int TransactionCode { get { return _intTransactionCode; } set { _intTransactionCode = value; } }
            public int RewardCategoryCode { get { return _intRewardCategoryCode; } set { _intRewardCategoryCode = value; } }
            public string Description { get { return _strDescription; } set { _strDescription = value; } }
            public string Status { get { return _strStatus; } set { _strStatus = value; } }
            public string CreatedBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
            public DateTime CreatedOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
            public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
            public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

            public void Fill()
            {
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TransactionCode,RewardCategoryCode, Description, Status, CreateBy, CreateOn, ModifyBy, ModifyOn FROM Employee.Reward WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            _intTransactionCode = dr["TransactionCode"].ToString().ToInt();
                            _intRewardCategoryCode = dr["RewardCategoryCode"].ToString().ToInt();
                            _strDescription = dr["Description"].ToString();
                            _strStatus = dr["Status"].ToString();
                            _strCreateBy = dr["CreateBy"].ToString();
                            _dteCreateOn = DateTime.Parse(dr["CreateOn"].ToString());
                            _strModifyBy = dr["ModifyBy"].ToString();
                            _dteModifyOn = DateTime.Parse(dr["ModifyOn"].ToString());
                        }
                    }
                }
            }

            public int Insert(DataTable pEmployeeReward)
            {
                int intReturn = 0;
                SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='trnscode'";
                    _intTransactionCode = cmd.ExecuteScalar().ToString().ToInt() + 1;

                    cmd.CommandText = "INSERT INTO  Employee.Reward  (TransactionCode, RewardCategoryCode,Description, Status, CreateBy, CreateOn, ModifyBy, ModifyOn) VALUES(@TransactionCode,@RewardCategoryCode, @Description, @Status, @CreateBy, GETDATE(), @ModifyBy,  GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("RewardCategoryCode", _intRewardCategoryCode));
                    cmd.Parameters.Add(new SqlParameter("Description", _strDescription));
                    cmd.Parameters.Add(new SqlParameter("Status", _strStatus));
                    cmd.Parameters.Add(new SqlParameter("CreateBy", _strCreateBy));
                    cmd.Parameters.Add(new SqlParameter("ModifyBy", _strModifyBy));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();


                    foreach (DataRow drEmployeeReward in pEmployeeReward.Rows)
                    {
                        cmd.CommandText = "INSERT INTO  Employee.RewardDetail (TransactionCode, Username, Points, DateAcquired, IsIncrease, CreateBy, CreateOn, ModifyBy, ModifyOn) VALUES(@TransactionCode, @Username, @Points,@DateAcquired ,@IsIncrease, @CreateBy, GETDATE(), @ModifyBy, GETDATE())";
                        cmd.Parameters.Add(new SqlParameter("TransactionCode", _intTransactionCode));
                        cmd.Parameters.Add(new SqlParameter("Username", drEmployeeReward["Username"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("Points", drEmployeeReward["Points"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("DateAcquired", DateTime.Parse(drEmployeeReward["DateAcquired"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("IsIncrease", drEmployeeReward["IsIncrease"].ToString() == "Add" ? "1" : "0"));
                        cmd.Parameters.Add(new SqlParameter("CreateBy", _strCreateBy));
                        cmd.Parameters.Add(new SqlParameter("ModifyBy", _strModifyBy));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //insert approvers manually HR Department approvers
                    cmd.CommandText = "INSERT INTO Employee.RewardApproval (TransactionCode, Username, ApproverLevel, Status,DateApprove) VALUES(@TransactionCode, @Username, @ApproverLevel, @Status, GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("@Username", ApproverLevel1));
                    cmd.Parameters.Add(new SqlParameter("@ApproverLevel", "1"));
                    cmd.Parameters.Add(new SqlParameter("@Status", "0"));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO Employee.RewardApproval (TransactionCode, Username, ApproverLevel, Status,DateApprove) VALUES(@TransactionCode, @Username, @ApproverLevel, @Status, GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("@Username", ApproverLevel2));
                    cmd.Parameters.Add(new SqlParameter("@ApproverLevel", "2"));
                    cmd.Parameters.Add(new SqlParameter("@Status", "0"));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='trnscode'";
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    cn.Close();
                }
                return intReturn;
            }

            public static string GetRequestStatusIcon(string pStatus)
            {
                string strReturn = "";
                if (pStatus == "3")
                    strReturn = "Disapproved.png";
                else if (pStatus == "2")
                    strReturn = "Disapproved.png";
                else if (pStatus == "0")
                    strReturn = "Approval.png";
                else if (pStatus == "4")
                    strReturn = "Pending.png";
                else if (pStatus == "1")
                    strReturn = "Approved.png";
                return strReturn;
            }

            public int Update(DataTable pEmployeeReward)
            {
                int intReturn = 0;
                SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();

                try
                {
                    cmd.CommandText = "UPDATE  Employee.Reward  SET RewardCategoryCode=@RewardCategoryCode, Description=@Description, Status=@Status, ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE TransactionCode=@TransactionCode";
                    cmd.Parameters.Add(new SqlParameter("TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("RewardCategoryCode", _intRewardCategoryCode));
                    cmd.Parameters.Add(new SqlParameter("Description", _strDescription));
                    cmd.Parameters.Add(new SqlParameter("Status", _strStatus));
                    cmd.Parameters.Add(new SqlParameter("ModifyBy", _strModifyBy));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();


                    cmd.CommandText = "DELETE FROM Employee.RewardDetail WHERE TransactionCode=@TransactionCode";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    foreach (DataRow drEmployeeReward in pEmployeeReward.Rows)
                    {
                        cmd.CommandText = "INSERT INTO  Employee.RewardDetail (TransactionCode, Username, Points, DateAcquired, IsIncrease, CreateBy, CreateOn, ModifyBy, ModifyOn) VALUES(@TransactionCode, @Username, @Points, @DateAcquired, @IsIncrease, @CreateBy, GETDATE(), @ModifyBy, GETDATE())";
                        cmd.Parameters.Add(new SqlParameter("TransactionCode", _intTransactionCode));
                        cmd.Parameters.Add(new SqlParameter("Username", drEmployeeReward["Username"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("Points", drEmployeeReward["Points"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("DateAcquired", DateTime.Parse(drEmployeeReward["DateAcquired"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("IsIncrease", drEmployeeReward["IsIncrease"].ToString() == "Add" ? "1" : "0"));
                        cmd.Parameters.Add(new SqlParameter("CreateBy", _strModifyBy));
                        cmd.Parameters.Add(new SqlParameter("ModifyBy", _strModifyBy));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //delete and insert approvers manually HR Department approvers
                    cmd.CommandText = "DELETE FROM Employee.RewardApproval WHERE TransactionCode=@TransactionCode";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO Employee.RewardApproval (TransactionCode, Username, ApproverLevel, Status,DateApprove) VALUES(@TransactionCode, @Username, @ApproverLevel, @Status,GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("@Username", "liezel.diego"));
                    cmd.Parameters.Add(new SqlParameter("@ApproverLevel", "1"));
                    cmd.Parameters.Add(new SqlParameter("@Status", "0"));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO Employee.RewardApproval (TransactionCode, Username, ApproverLevel, Status,DateApprove) VALUES(@TransactionCode, @Username, @ApproverLevel, @Status,GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("@TransactionCode", _intTransactionCode));
                    cmd.Parameters.Add(new SqlParameter("@Username", "jay.jamandre"));
                    cmd.Parameters.Add(new SqlParameter("@ApproverLevel", "2"));
                    cmd.Parameters.Add(new SqlParameter("@Status", "0"));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
                catch
                {
                    intReturn = 0;
                }
                finally
                {
                    cn.Close();
                }
                return intReturn;
            }

            public static string GetRewardCategoryCode(int pTransactionCode)
            {
                string strReturn = "";
                try
                {
                    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT RewardCategoryCode FROM Employee.Reward WHERE TransactionCode=@TransactionCode";
                            cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                            cn.Open();
                            strReturn = cmd.ExecuteScalar().ToString();
                        }
                    }
                }
                catch { }
                
                return strReturn;
            }

            public static string GetDescription(int pTransactionCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Description FROM Employee.Reward WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }
        }
    }
}