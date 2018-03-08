using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace Reward
    {
        public class clsRewardCategory : IDisposable
        {
            public void Dispose() { GC.SuppressFinalize(this); }

            public clsRewardCategory()
            { }

            public static DataTable GetDSL()
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT RewardCategoryCode AS pValue, RewardCategoryName as pText FROM Employee.RewardCategory ORDER BY pText";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static string GetName(int pRewardCategoryCode)
            {
                string strReturn = "";
                try
                {
                    using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                    {
                        using (SqlCommand cmd = cn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT RewardCategoryName FROM Employee.RewardCategory WHERE RewardCategoryCode=@RewardCategoryCode ";
                            cmd.Parameters.Add(new SqlParameter("@RewardCategoryCode", pRewardCategoryCode));
                            cn.Open();
                            strReturn = cmd.ExecuteScalar().ToString();
                        }
                    }
                }
                catch { }
                
                return strReturn;
            }
        }
    }
}