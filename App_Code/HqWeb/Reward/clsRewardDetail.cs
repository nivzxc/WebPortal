using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace Reward
    {
        public class clsRewardDetail : IDisposable
        {
            public void Dispose() { GC.SuppressFinalize(this); }

            public clsRewardDetail()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            private int _intEmployeeRewardCode;
            private int _intTransactionCode;
            private string _strUsername;
            private double _dblPoints;
            private DateTime _dteDateAcquired;
            private string _strIsAcquired;
            private string _strCreateBy;
            private DateTime _dteCreateOn;
            private string _strModifyBy;
            private DateTime _dteModifyOn;

            public int EmployeeRewardCode { get { return _intEmployeeRewardCode; } set { _intEmployeeRewardCode = value; } }
            public int TransactionCode { get { return _intTransactionCode; } set { _intTransactionCode = value; } }
            public string Username { get { return _strUsername; } set { _strUsername = value; } }
            public double Points { get { return _dblPoints; } set { _dblPoints = value; } }
            public DateTime DateAcquired { get { return _dteDateAcquired; } set { _dteDateAcquired = value; } }
            public string IsAcquired { get { return _strIsAcquired; } set { _strIsAcquired = value; } }
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
                        cmd.CommandText = "SELECT  EmployeeRewardCode, TransactionCode, Username, Points, DateAcquired, IsIncrease, CreateBy, CreateOn, ModifyBy,ModifyOn FROM Employee.RewardDetail WHERE EmployeeRewardCode=@EmployeeRewardCode";
                        cmd.Parameters.Add(new SqlParameter("@EmployeeRewardCode", _intEmployeeRewardCode));
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            _intEmployeeRewardCode = dr["EmployeeRewardCode"].ToString().ToInt();
                            _intTransactionCode = dr["TransactionCode"].ToString().ToInt();
                            _strUsername = dr["Username"].ToString();
                            _dblPoints = double.Parse(dr["Points"].ToString());
                            _dteDateAcquired = DateTime.Parse(dr["DateAcquired"].ToString());
                            _strIsAcquired = dr["IsIncrease"].ToString();
                            _strCreateBy = dr["CreateBy"].ToString();
                            _dteCreateOn = DateTime.Parse(dr["CreateOn"].ToString());
                            _strModifyBy = dr["ModifyBy"].ToString();
                            _dteModifyOn = DateTime.Parse(dr["ModifyOn"].ToString());
                        }
                    }
                }
            }

            public static int Delete(int pTransactionCode)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Employee.RewardDetail WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();
                    }
                }
                return intReturn;
            }

            public static DataTable GetDSG(int pTransactionCode)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Username,(SELECT lastname + ', ' + firname FROM HR.Employees WHERE username=Employee.RewardDetail.Username) AS Name, Points, DateAcquired, IsIncrease FROM Employee.RewardDetail WHERE TransactionCode=@TransactionCode ORDER BY Name";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGEdit(int pTransactionCode)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT EmployeeRewardCode, TransactionCode, Username, Points, DateAcquired, (SELECT CASE WHEN IsIncrease = '1' THEN 'Add' ELSE 'Deduct' END) AS  IsIncrease ,CreateBy, CreateOn, ModifyBy, ModifyOn FROM Employee.RewardDetail WHERE TransactionCode=@TransactionCode";
                        cmd.Parameters.Add(new SqlParameter("@TransactionCode", pTransactionCode));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static double GetPoints(string pUsername)
            {
                double dblReturn = 0;
                double dblIncrease = 0;
                double dblDecrease = 0;
                try
                {
                    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cn.Open();
                            cmd.CommandText = "SELECT (SELECT CASE WHEN SUM(Points) IS NULL THEN 0 ELSE SUM(Points) END) AS Pointss FROM Employee.RewardDetail WHERE Username =@Username AND TransactionCode IN (SELECT TransactionCode FROM Employee.Reward WHERE Status='1') AND IsIncrease='1'";
                            cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                            dblIncrease = double.Parse(cmd.ExecuteScalar().ToString());
                            cmd.Parameters.Clear();

                            cmd.CommandText = "SELECT (SELECT CASE WHEN SUM(Points) IS NULL THEN 0 ELSE SUM(Points) END) AS Pointss FROM Employee.RewardDetail WHERE Username =@Username AND TransactionCode IN (SELECT TransactionCode FROM Employee.Reward WHERE Status='1') AND IsIncrease='0'";
                            cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                            dblDecrease = double.Parse(cmd.ExecuteScalar().ToString());
                            cmd.Parameters.Clear();

                            dblReturn = dblIncrease - dblDecrease;
                        }
                    }

                }

                catch
                {
                    dblReturn = 0;
                }

                return dblReturn;
            }

            public static DataTable GetDSG(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT EmployeeRewardCode,(SELECT Description FROM Employee.Reward WHERE TransactionCode =Employee.RewardDetail.TransactionCode) AS Description,TransactionCode,(SELECT RewardCategoryName FROM Employee.RewardCategory WHERE RewardCategoryCode = (SELECT RewardCategoryCode FROM Employee.Reward WHERE TransactionCode =Employee.RewardDetail.TransactionCode)) AS RewardCategoryName,(SELECT RewardCategoryCode FROM Employee.Reward WHERE TransactionCode =Employee.RewardDetail.TransactionCode) AS RewardCategoryCode, Username, Points, DateAcquired, IsIncrease, CreateBy, CreateOn, ModifyBy, ModifyOn FROM Employee.RewardDetail WHERE Username =@Username  AND (SELECT Status FROM Employee.Reward WHERE TransactionCode =Employee.RewardDetail.TransactionCode) = '1' ORDER BY CreateOn DESC ";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }
        }
    }
}