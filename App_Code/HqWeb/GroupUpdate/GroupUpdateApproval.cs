using System;
using System.Data;
using System.Data.SqlClient;

namespace HqWeb
{
    namespace GroupUpdate
    {
        public class GroupUpdateApproval : IDisposable
        {
            public void Dispose() { GC.SuppressFinalize(this); }

            public enum GroupUpdateUserType
            {
                GroupHead = 1,
                DivisionHead = 2,
            }

            public GroupUpdateApproval()
            { }

            public static string GetApprover(GroupUpdateUserType pGroupUpdateUserType, string pDepartmentCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT username FROM Speedo.ModuleApprover WHERE modlcode ='GROUPDATE' AND deptcode=@deptcode AND applevel=@applevel";
                        cmd.Parameters.Add(new SqlParameter("@deptcode", pDepartmentCode));
                        cmd.Parameters.Add(new SqlParameter("@applevel", pGroupUpdateUserType));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static string ForApproval(string pGroupUpdateCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "SELECT TOP (1)  Username FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode=@GroupUpdateCode  AND Status = '0'  ORDER BY ApprovalLevel";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static DataTable GetDSGForApprovalLevel1(string pRewardApprover1)
            {
                DataTable tblReturn = new DataTable();

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  GroupUpdateCode, Title, CreateBy, CreateOn, (SELECT Status FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='1' AND Status='0') AS Status FROM  Portal.GroupUpdate  as PG WHERE Status='0' AND (SELECT Username FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='1' AND Status='0') = @Username ORDER BY CreateOn DESC ";
                        cmd.Parameters.Add(new SqlParameter("@Username", pRewardApprover1));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static int GetCountForModification(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  GroupUpdateCode, Title, CreateBy, CreateOn, Status FROM Portal.GroupUpdate WHERE Status='4' AND CreateBy=@CreateBy";
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", pUsername));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                        try
                        {
                            intReturn = tblReturn.Rows.Count;
                        }
                        catch { intReturn=0; }
                        
                    }
                }
                return intReturn;
            }

            public static int GetCountForApprovalLevel1(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  GroupUpdateCode, Title, CreateBy, CreateOn, (SELECT Status FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='1' AND Status='0') AS Status FROM  Portal.GroupUpdate  as PG WHERE Status='0' AND (SELECT Username FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='1' AND Status='0') = @Username ORDER BY CreateOn DESC ";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                        try
                        {
                            intReturn = tblReturn.Rows.Count;
                        }
                        catch { intReturn = 0; }
                    }
                }
                return intReturn;
            }

            public static int GetCountForApprovalLevel2(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                int intReturn = 0;
                tblReturn.Columns.Add("GroupUpdateCode");
                tblReturn.Columns.Add("Title");
                tblReturn.Columns.Add("Status");
                tblReturn.Columns.Add("CreateBy");
                tblReturn.Columns.Add("CreateOn");
                DataTable tblTemporary = new DataTable();

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {

                        cn.Open();
                        cmd.CommandText = "SELECT  GroupUpdateCode, Title, CreateBy, CreateOn, (SELECT Status FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='2' AND Status='0') AS Status FROM  Portal.GroupUpdate  as PG WHERE Status='0' AND (SELECT Username FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='2' AND Status='0') = @Username ORDER BY CreateOn DESC ";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            if (IsApprovedLevel1(dr["GroupUpdateCode"].ToString().ToInt()))
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["GroupUpdateCode"] = dr["GroupUpdateCode"].ToString();
                                drNew["Title"] = dr["Title"].ToString();
                                drNew["Status"] = dr["Status"].ToString();
                                drNew["CreateBy"] = dr["CreateBy"].ToString();
                                drNew["CreateOn"] = dr["CreateOn"].ToString();
                                tblReturn.Rows.Add(drNew);
                            }
                        }

                        try
                        {
                            intReturn = tblReturn.Rows.Count;
                        }
                        catch { intReturn = 0; }

                    }
                }
                return intReturn;
            }

            public static bool IsApprovedLevel1(int pGroupUpdateCode)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT GroupUpdateCode FROM Portal.GroupUpdateApproval WHERE Status='1' AND ApprovalLevel='1' AND GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
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

            public static DataTable GetDSGForApprovalLevel2(string pRewardApprover2)
            {
                DataTable tblReturn = new DataTable();
                tblReturn.Columns.Add("GroupUpdateCode");
                tblReturn.Columns.Add("Title");
                tblReturn.Columns.Add("Status");
                tblReturn.Columns.Add("CreateBy");
                tblReturn.Columns.Add("CreateOn");
                DataTable tblTemporary = new DataTable();


                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {

                        cn.Open();
                        cmd.CommandText = "SELECT  GroupUpdateCode, Title, CreateBy, CreateOn, (SELECT Status FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='2' AND Status='0') AS Status FROM  Portal.GroupUpdate  as PG WHERE Status='0' AND (SELECT Username FROM Portal.GroupUpdateApproval WHERE GroupUpdateCode=pg.GroupUpdateCode AND ApprovalLevel='2' AND Status='0') = @Username ORDER BY CreateOn DESC ";
                        cmd.Parameters.Add(new SqlParameter("@Username", pRewardApprover2));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            if (IsApprovedLevel1(dr["GroupUpdateCode"].ToString().ToInt()))
                            {
                                DataRow drNew = tblReturn.NewRow();
                                drNew["GroupUpdateCode"] = dr["GroupUpdateCode"].ToString();
                                drNew["Title"] = dr["Title"].ToString();
                                drNew["Status"] = dr["Status"].ToString();
                                drNew["CreateBy"] = dr["CreateBy"].ToString();
                                drNew["CreateOn"] = dr["CreateOn"].ToString();
                                tblReturn.Rows.Add(drNew);
                            }
                        }

                    }
                }
                return tblReturn;
            }

            public static int ApprovedLevel1(string pGroupUpdateCode, string pUsername, string pRemark)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET DateApprove=GETDATE(),Remark=@Remark, Status='1' WHERE Username=@Username AND GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@Remark", pRemark));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='1', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (intReturn > 0)
                {
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ApproveToApproverGH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername) , clsUsers.GetEmail(pUsername), pGroupUpdateCode);
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ApproveToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), pGroupUpdateCode);
                }
                return intReturn;
            }

            public static int ApprovedLevel1(string pGroupUpdateCode)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET DateApprove=GETDATE(), Status='1' WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();
                    }
                }
                return intReturn;
            }

            public static int ApprovedLevel2(string pGroupUpdateCode, string pUsername, string pRemark)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET DateApprove=GETDATE(),Remark=@Remark, Status='1' WHERE Username=@Username AND GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@Remark", pRemark));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='1', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                if (intReturn > 0)
                {
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ApproveToApproverDH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(pUsername), pGroupUpdateCode);
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ApproveToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), pGroupUpdateCode);
                }

                return intReturn;
            }

            public static int DisApprovedLevel1(string pGroupUpdateCode, string pUsername, string pRemark)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET DateApprove=GETDATE(), Status='2' WHERE Username=@Username AND GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@Remark", pRemark));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='2', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }

                    if (intReturn > 0)
                    {
                        clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.DisapproveToApproverGH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(pUsername), pGroupUpdateCode);
                        clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.DisapproveToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), pGroupUpdateCode);
                    }
                }
                return intReturn;
            }

            public static int DisApprovedLevel2(string pGroupUpdateCode, string pUsername, string pRemark)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET DateApprove=GETDATE(), Status='2' WHERE Username=@Username AND GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@Remark", pRemark));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='2', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                    }
                }

                if (intReturn > 0)
                {
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.DisapproveToApproverDH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(pUsername), pGroupUpdateCode);
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.DisapproveToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), pGroupUpdateCode);
                }

                return intReturn;
            }

            public static string GetApproverName(string pGroupUpdateCode, string pApproverLevel)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "SELECT Username FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode=@GroupUpdateCode AND ApprovalLevel=@ApprovalLevel";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cmd.Parameters.Add(new SqlParameter("@ApprovalLevel", pApproverLevel));
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static string GetApproverRemarks(string pGroupUpdateCode, string pApproverLevel)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "SELECT Remark FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode=@GroupUpdateCode AND ApprovalLevel=@ApprovalLevel";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cmd.Parameters.Add(new SqlParameter("@ApprovalLevel", pApproverLevel));
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static int Modification(string pGroupUpdateCode, string pUsername, string pRemark)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET Status='0', Remark=@Remark WHERE GroupUpdateCode=@GroupUpdateCode AND Username=@Username";
                        cmd.Parameters.Add(new SqlParameter("@Username", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@Remark", pRemark));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='4', ModifyBy=@ModifyBy, ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@ModifyBy", pUsername));
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                if (intReturn > 0)
                {
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ModificationToApprover, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(pUsername), clsUsers.GetEmail(pUsername), pGroupUpdateCode);
                    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.ModificationToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetName(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(pGroupUpdateCode)), pGroupUpdateCode);
                }
   
                return intReturn;
            }
        }
    }
}