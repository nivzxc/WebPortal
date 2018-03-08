using System;
using System.Data;
using System.Data.SqlClient;


namespace HqWeb
{
    namespace GroupUpdate
    {
        public class clsGroupUpdate : IDisposable
        {
            public clsGroupUpdate()
            { }

            public void Dispose() { GC.SuppressFinalize(this); }

            private int _intGroupUpdateCode;
            private string _strTitle;
            private string _strDescription;
            private string _strContent;
            private string _strImageFilename;
            private string _strDepartmentCode;
            private string _strDivisionCode;
            private string _strStatus;
            private string _strEnabled;
            private string _strContributor;
            private string _strPhotoSource;
            private string _strCreateBy;
            private DateTime _dteCreateOn;
            private string _strModifyBy;
            private DateTime _dteModifyOn;

            public int GroupUpdateCode { get { return _intGroupUpdateCode; } set { _intGroupUpdateCode = value; } }
            public string Title { get { return _strTitle; } set { _strTitle = value; } }
            public string Description { get { return _strDescription; } set { _strDescription = value; } }
            public string Content { get { return _strContent; } set { _strContent = value; } }
            public string ImageFilename { get { return _strImageFilename; } set { _strImageFilename = value; } }
            public string Status { get { return _strStatus; } set { _strStatus = value; } }
            public string DepartmentCode { get { return _strDepartmentCode; } set { _strDepartmentCode = value; } }
            public string DivisionCode { get { return _strDivisionCode; } set { _strDivisionCode = value; } }
            public string Enabled { get { return _strEnabled; } set { _strEnabled = value; } }
            public string Contributor { get { return _strContributor; } set { _strContributor = value; } }
            public string PhotoSource { get { return _strPhotoSource; } set { _strPhotoSource = value; } }
            public string CreateBy { get { return _strCreateBy; } set { _strCreateBy = value; } }
            public DateTime CreateOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
            public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
            public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

            public enum MRCFMailType
            {
                SentToRequestor = 1,
                SentToApproverGH = 2,
                SentToApproverDH = 3,
                ApproveToRequestor = 4,
                ApproveToApproverGH = 5,
                ApproveToApproverDH = 6,
                DisapproveToRequestor = 7,
                DisapproveToApproverGH = 8,
                DisapproveToApproverDH = 9,
                ModificationToRequestor = 10,
                ModificationToApprover = 11,
                ModifiedSendToRequestor=12,
                ModifiedSendToApprover=13,

                CreateAndApproveGH = 14
            }

            public void Fill()
            {
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Portal.GroupUpdate WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", _intGroupUpdateCode));
                        cn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            _intGroupUpdateCode = dr["GroupUpdateCode"].ToString().ToInt();
                            _strTitle = dr["Title"].ToString().ToString();
                            _strDescription = dr["Description"].ToString();
                            _strContent = dr["Contents"].ToString().ToString();
                            _strImageFilename = dr["ImageFilename"].ToString().ToString();
                            _strDepartmentCode = dr["DepartmentCode"].ToString();
                            _strDivisionCode = dr["DivisionCode"].ToString();
                            _strStatus = dr["Status"].ToString().ToString();
                            _strEnabled = dr["Enabled"].ToString().ToString();
                            _strContributor = dr["Contributor"].ToString();
                            _strPhotoSource = dr["PhotoSource"].ToString();
                            //_strContributor = clsGroupUpdate.GetContributor(_intGroupUpdateCode.ToString());
                            _strCreateBy = dr["CreateBy"].ToString().ToString();
                            _dteCreateOn = DateTime.Parse(dr["CreateOn"].ToString());
                            _strModifyBy = dr["ModifyBy"].ToString().ToString();
                            _dteModifyOn = DateTime.Parse(dr["ModifyOn"].ToString());
                        }

                    }
                }
            }

            public int Insert(DataTable pApprover, bool pIsApprover)
            {
                int intReturn = 0;

                SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = tran;

                try
                {
                    cmd.CommandText = "SELECT pvalue FROM Speedo.Keys WHERE pkey='GUCode'";
                    _intGroupUpdateCode = cmd.ExecuteScalar().ToString().ToInt() + 1;

                    cmd.CommandText = "INSERT INTO Portal.GroupUpdate  (GroupUpdateCode, Title, Description, Contents, ImageFilename, Status, DepartmentCode, DivisionCode, Contributor , PhotoSource, Enabled,CreateBy, CreateOn, ModifyBy,ModifyOn) VALUES (@GroupUpdateCode, @Title, @Description, @Contents, @ImageFilename, @Status, @DepartmentCode, @DivisionCode, @Contributor , @PhotoSource, @Enabled, @CreateBy, GETDATE(), @ModifyBy,GETDATE())";
                    cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", _intGroupUpdateCode));
                    cmd.Parameters.Add(new SqlParameter("@Title", _strTitle));
                    cmd.Parameters.Add(new SqlParameter("@Description", _strDescription));
                    cmd.Parameters.Add(new SqlParameter("@Contents", _strContent));
                    cmd.Parameters.Add(new SqlParameter("@ImageFilename", _strImageFilename));
                    cmd.Parameters.Add(new SqlParameter("@Status", _strStatus));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentCode", _strDepartmentCode));
                    cmd.Parameters.Add(new SqlParameter("@DivisionCode", _strDivisionCode));
                    cmd.Parameters.Add(new SqlParameter("@Enabled", _strEnabled));
                    cmd.Parameters.Add(new SqlParameter("@Contributor", _strContributor));
                    cmd.Parameters.Add(new SqlParameter("@PhotoSource", _strPhotoSource));
                    cmd.Parameters.Add(new SqlParameter("@ModifyBy", _strCreateBy));
                    cmd.Parameters.Add(new SqlParameter("@CreateBy", _strCreateBy));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    foreach (DataRow dr in pApprover.Rows)
                    {
                        cmd.CommandText = "INSERT INTO  Portal.GroupUpdateApproval  (GroupUpdateCode, ApprovalLevel, Username, Remark, Status, DateApprove) VALUES (@GroupUpdateCode, @ApprovalLevel, @Username, '', '0', GETDATE())";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", _intGroupUpdateCode));
                        cmd.Parameters.Add(new SqlParameter("@ApprovalLevel", dr["ApprovalLevel"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@Username", dr["Username"].ToString()));
                        intReturn = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    cmd.CommandText = "UPDATE Speedo.Keys SET pvalue=pvalue+1 WHERE pkey='GUCode'";
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    tran.Commit();
                    intReturn = _intGroupUpdateCode;
                }
                catch
                {
                    tran.Rollback();
                    intReturn = 0;
                }
                finally
                {
                    cn.Close();
                    if (intReturn > 0 && _strStatus != "5")
                    {
                        if (!pIsApprover)
                        {
                            SendNotification(MRCFMailType.SentToRequestor, clsUsers.GetName(_strCreateBy), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(_strCreateBy), _intGroupUpdateCode.ToString());
                            SendNotification(MRCFMailType.SentToApproverGH, clsUsers.GetName(_strCreateBy), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), _intGroupUpdateCode.ToString());
                        }
                        else
                        {
                            GroupUpdateApproval.ApprovedLevel1(_intGroupUpdateCode.ToString());
                            SendNotification(MRCFMailType.CreateAndApproveGH, clsUsers.GetName(_strCreateBy), clsUsers.GetName(_strCreateBy), clsUsers.GetEmail(_strCreateBy), _intGroupUpdateCode.ToString());
                        }
                    }
                }

                return intReturn;
            }

            public static void SendNotification(MRCFMailType pMailType, string pRequestorName, string pApproverName, string pMailTo, string pGroupUpdateCode)
            {
                string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
                string strSubject = "";
                string strBody = "";

                switch (pMailType)
                {
                    case MRCFMailType.SentToRequestor:
                        strSubject = "Delivered: Group Update";
                        strBody = "Hi " + pRequestorName + ",<br><br>" +
                                  "Your Group Update has been delivered to " + pApproverName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.SentToApproverGH:
                        strSubject = "For Your Approval: Group Update";
                        strBody = "Hi " + pApproverName + ",<br><br>" +
                                  "There is an Group Update submitted by " + pRequestorName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.SentToApproverDH:
                        strSubject = "For Your Approval: Group Update";
                        strBody = "Hi " + pApproverName + ",<br><br>" +
                                  "There is an Group Update submitted by " + pRequestorName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.ApproveToRequestor:
                        strSubject = "Approved Group Update";
                        strBody = "Hi " + pRequestorName + ",<br><br>" +
                                  "Your Group Update has been approved by " + pApproverName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to review the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.ApproveToApproverGH:
                        strSubject = "Approved Group Update";
                        strBody = "You approved a Group Update.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.ApproveToApproverDH:
                        strSubject = "Approved Group Update";
                        strBody = "You approved a Group Update.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.DisapproveToRequestor:
                        strSubject = "Disapproved Group Update";
                        strBody = "Hi " + pRequestorName + ",<br><br>" +
                                  "Your Group Update has been disapproved by " + pApproverName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to review the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.DisapproveToApproverGH:
                        strSubject = "Disapproved Group Update";
                        strBody = "You disapproved a Group Update.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.DisapproveToApproverDH:
                        strSubject = "Disapproved Group Update";
                        strBody = "You disapproved a MRCF.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.ModificationToRequestor:
                        strSubject = "Modification Required: Group Update";
                        strBody = "Hi " + pRequestorName + ",<br><br>" +
                                  "Your Group Update requires modification according to " + pApproverName + ".<br>" +
                                  "You need to provide necessary corrections as per " + pApproverName + "'s instruction.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateEdit.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view and edit the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateEdit.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.ModificationToApprover:
                        strSubject = "Modification Required: Group Update";
                        strBody = "You have required modification for a Group Update.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;
                        
                    case MRCFMailType.ModifiedSendToApprover:
                        strSubject = "Modification Done: Group Update";
                        strBody = "Hi " + pApproverName + ",<br><br>" +
                                  "There is an modified Group Update submitted by " + pRequestorName + ".<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateEdit.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;
                        
                    case MRCFMailType.ModifiedSendToRequestor:
                        strSubject = "Modification Done: Group Update";
                        strBody = "You have modified a Group Update request.<br><br>" +
                                  "An email notification has been sent to " + pRequestorName + " to inform him/her regarding this action.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the request</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateGH.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;

                    case MRCFMailType.CreateAndApproveGH:
                        strSubject = "Posted Group Update";
                        strBody = "Group Update has been posted and now viewable in Head Office Portal.<br><br>" +
                                  "<a href='" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "'>Click here to view the group update</a><br><br>" +
                                  "If you can't click on the above link,<br>" +
                                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/GroupUpdate/GroupUpdateDetailsMain.aspx?GroupUpdateCode=" + pGroupUpdateCode + "</i><br><br>" +
                                  "All the best,<br>E-Forms Administrator";
                        break;
                }
                clsSpeedo.SendMail(pMailTo, strSubject, strBody);
            }

            public int Update(bool pIsApprover)
            { 
            int intReturn = 0;

                SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
                cn.Open();
                SqlTransaction tran = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = tran;

                try
                {
                    if (_strImageFilename.Length > 0)
                    {
                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Title=@Title, Description=@Description, Contents=@Contents, ImageFilename=@ImageFilename, Contributor=@Contributor ,PhotoSource=@PhotoSource,Status=@Status, DepartmentCode=@DepartmentCode, DivisionCode=@DivisionCode, Enabled=@Enabled, ModifyBy=@ModifyBy,ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Title=@Title, Description=@Description, Contents=@Contents, Status=@Status, DepartmentCode=@DepartmentCode, DivisionCode=@DivisionCode, Contributor=@Contributor,PhotoSource=@PhotoSource, Enabled=@Enabled, ModifyBy=@ModifyBy,ModifyOn=GETDATE() WHERE GroupUpdateCode=@GroupUpdateCode";
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", _intGroupUpdateCode));
                    cmd.Parameters.Add(new SqlParameter("@Title", _strTitle));
                    cmd.Parameters.Add(new SqlParameter("@Description", _strDescription));
                    cmd.Parameters.Add(new SqlParameter("@Contents", _strContent));
                    cmd.Parameters.Add(new SqlParameter("@ImageFilename", _strImageFilename));
                    cmd.Parameters.Add(new SqlParameter("@Status", _strStatus));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentCode", _strDepartmentCode));
                    cmd.Parameters.Add(new SqlParameter("@DivisionCode", _strDivisionCode));
                    cmd.Parameters.Add(new SqlParameter("@Contributor", _strContributor));
                    cmd.Parameters.Add(new SqlParameter("@PhotoSource", _strPhotoSource));
                    cmd.Parameters.Add(new SqlParameter("@Enabled", _strEnabled));
                    cmd.Parameters.Add(new SqlParameter("@ModifyBy", _strModifyBy));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE Portal.GroupUpdateApproval SET Status='0' WHERE  GroupUpdateCode=@GroupUpdateCode";
                    cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", _intGroupUpdateCode));
                    intReturn = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    tran.Commit();

                    //if (intReturn > 0 && _strEnabled == "0")
                    //{
                    //    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.SentToRequestor, clsUsers.GetName(clsGroupUpdate.GetCreateBy(_intGroupUpdateCode.ToString())), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(_intGroupUpdateCode.ToString())), _intGroupUpdateCode.ToString());
                    //    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.SentToApproverGH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(_intGroupUpdateCode.ToString())), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), _intGroupUpdateCode.ToString());
                    //}
                    //else if (intReturn > 0 && _strStatus == "1")
                    //{
                    //    clsGroupUpdate.SendNotification(clsGroupUpdate.MRCFMailType.CreateAndApproveGH, clsUsers.GetName(clsGroupUpdate.GetCreateBy(_intGroupUpdateCode.ToString())), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(clsGroupUpdate.GetCreateBy(_intGroupUpdateCode.ToString())), _intGroupUpdateCode.ToString());
                    //}

                    if (intReturn > 0 && _strStatus != "5")
                    {
                        if (!pIsApprover)
                        {
                            SendNotification(MRCFMailType.SentToRequestor, clsUsers.GetName(_strCreateBy), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(_strCreateBy), _intGroupUpdateCode.ToString());
                            SendNotification(MRCFMailType.SentToApproverGH, clsUsers.GetName(_strCreateBy), clsUsers.GetName(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), clsUsers.GetEmail(GroupUpdateApproval.ForApproval(_intGroupUpdateCode.ToString())), _intGroupUpdateCode.ToString());
                        }
                        else
                        {
                            GroupUpdateApproval.ApprovedLevel1(_intGroupUpdateCode.ToString());
                            SendNotification(MRCFMailType.CreateAndApproveGH, clsUsers.GetName(_strCreateBy), clsUsers.GetName(_strCreateBy), clsUsers.GetEmail(_strCreateBy), _intGroupUpdateCode.ToString());
                        }
                    }
                }
                catch
                { tran.Rollback(); }
                finally { cn.Close(); }

                return intReturn;
            }

            public static string GetDate()
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT GETDATE()";
                        cn.Open();
                        strReturn = DateTime.Parse(cmd.ExecuteScalar().ToString()).ToString("MMddyyyy");
                    }
                }
                return strReturn;
            }

            public static DataTable GetDSGHome()
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TOP(36) GroupUpdateCode, Title, Description, Contents, ImageFilename, Status, DepartmentCode, DivisionCode, Enabled, Contributor ,CreateBy, (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn, Modifyby, ModifyOn FROM Portal.GroupUpdate WHERE Status='1' ORDER BY (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) DESC";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSG(string pUsername)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Portal.GroupUpdate WHERE CreateBy=@CreateBy ORDER BY CreateOn DESC";
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", pUsername));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
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
                else if (pStatus == "5")
                    strReturn = "Pending.png";
                return strReturn;
            }

            public static bool HasAccess(string username)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Users.UsersModules WHERE modlcode=@modlcode AND username=@username AND pstatus='1'";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", "GROUPDATE"));
                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    blnReturn = dr.Read();
                    dr.Close();
                }
                return blnReturn;
            }

            public static bool IsDivisionApprover(string username)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND applevel='2' AND username=@username AND penabled='1'";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", "GROUPDATE"));
                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    blnReturn = dr.Read();
                    dr.Close();
                }
                return blnReturn;
            }

            public static bool IsGroupApprover(string username)
            {
                bool blnReturn = false;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Speedo.ModuleApprover WHERE modlcode=@modlcode AND applevel='1' AND username=@username AND penabled='1'";
                    cmd.Parameters.Add(new SqlParameter("@modlcode", "GROUPDATE"));
                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    blnReturn = dr.Read();
                    dr.Close();
                }
                return blnReturn;
            }

            //Filter and Querying

            public static DataTable GetDSGLatestNews()
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TOP(6) GroupUpdateCode, Title, DepartmentCode,DivisionCode, Contributor, CreateBy, (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1' ORDER BY (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) DESC";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGLatestNews(string pDivisionCode)
            {
                DataTable tblReturn = new DataTable();
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TOP(6) GroupUpdateCode, Title,DepartmentCode, DivisionCode, Contributor ,CreateBy, (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1'  AND DivisionCode=@DivisionCode ORDER BY (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) DESC";
                        cmd.Parameters.Add(new SqlParameter("@DivisionCode", pDivisionCode));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSLDivision()
            {
                DataTable tblReturn = new DataTable();
                DataTable tblTemporary = new DataTable();

                tblReturn.Columns.Add("pValue");
                tblReturn.Columns.Add("pText");

                DataRow drNew = tblReturn.NewRow();
                drNew["pValue"] = "ALL";
                drNew["pText"] = "ALL";
                tblReturn.Rows.Add(drNew);

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT divicode, division FROM HR.Division ORDER BY division";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            drNew = tblReturn.NewRow();
                            drNew["pValue"] = dr["divicode"].ToString();
                            drNew["pText"] = dr["division"].ToString();
                            tblReturn.Rows.Add(drNew);
                        }
                    }
                }

                return tblReturn;
            }

            public static DataTable GetDSLDepartment(string pDivisionCode)
            {
                DataTable tblReturn = new DataTable();
                DataTable tblTemporary = new DataTable();

                tblReturn.Columns.Add("pValue");
                tblReturn.Columns.Add("pText");

                DataRow drNew = tblReturn.NewRow();
                drNew["pValue"] = "ALL";
                drNew["pText"] = "ALL";
                tblReturn.Rows.Add(drNew);

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT deptcode, deptname FROM  HR.Department WHERE pstatus='1' AND divicode=@divicode ORDER BY deptname";
                        cmd.Parameters.Add(new SqlParameter("@divicode", pDivisionCode));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            drNew = tblReturn.NewRow();
                            drNew["pValue"] = dr["deptcode"].ToString();
                            drNew["pText"] = dr["deptname"].ToString();
                            tblReturn.Rows.Add(drNew);
                        }
                    }
                }

                return tblReturn;
            }

            public static int GetTopNews(string pDivisionCode)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT TOP(1) GroupUpdateCode FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1'  AND DivisionCode=@DivisionCode ORDER BY (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) DESC";
                            cmd.Parameters.Add(new SqlParameter("@DivisionCode", pDivisionCode));
                            cn.Open();
                            intReturn = cmd.ExecuteScalar().ToString().ToInt();
                        }
                        catch
                        { }
                    }
                }
                return intReturn;
            }

            public static DataTable GetDSLDepartment()
            {
                DataTable tblReturn = new DataTable();
                DataTable tblTemporary = new DataTable();

                tblReturn.Columns.Add("pValue");
                tblReturn.Columns.Add("pText");

                DataRow drNew = tblReturn.NewRow();
                drNew["pValue"] = "ALL";
                drNew["pText"] = "ALL";
                tblReturn.Rows.Add(drNew);

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT deptcode, deptname FROM  HR.Department WHERE pstatus='1' ORDER BY deptname";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblTemporary);

                        foreach (DataRow dr in tblTemporary.Rows)
                        {
                            drNew = tblReturn.NewRow();
                            drNew["pValue"] = dr["deptcode"].ToString();
                            drNew["pText"] = dr["deptname"].ToString();
                            tblReturn.Rows.Add(drNew);
                        }
                    }
                }

                return tblReturn;
            }

            public static string GetTitle(int pGroupUpdateCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Title FROM Portal.GroupUpdate WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static string GetDescription(int pGroupUpdateCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Description FROM Portal.GroupUpdate WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;
            }

            public static DataTable GetDSGArchives(string pDivisionCode, string pDepartmentCode, DateTime dtStart, DateTime dtEnd, string pKeyWord)
            {
                DataTable tblReturn = new DataTable();
                string strDivisionCode = "";
                string strDepartmentCode = "";
                string strKeyWord = "";
                if (pDivisionCode != "ALL")
                {
                    strDivisionCode = " AND DivisionCode='" + pDivisionCode + "' ";
                }

                if (pDepartmentCode != "ALL")
                {
                    strDepartmentCode = " AND DepartmentCode='" + pDepartmentCode + "' ";
                }

                if (pKeyWord != string.Empty)
                {
                    strKeyWord = " AND (Title LIKE '%" + pKeyWord + "%' OR Description LIKE '%" + pKeyWord + "%' OR Contents LIKE  '%" + pKeyWord + "%' OR Contributor LIKE  '%" + pKeyWord + "%' OR CreateBy LIKE  '%" + pKeyWord + "%' OR PhotoSource LIKE '%" + pKeyWord + "%') ";
                }

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT GroupUpdateCode, Title,Description,Contents, DepartmentCode, DivisionCode, Contributor, CreateBy , (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1' " + strDivisionCode + strDepartmentCode + strKeyWord + "  AND (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) BETWEEN @dtStart AND @dtEnd ORDER BY (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) DESC";
                        cmd.Parameters.Add(new SqlParameter("@keyword", pKeyWord));
                        cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                        cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGArchives(string pDivisionCode, string pDepartmentCode, DateTime dtStart, DateTime dtEnd, int pLimit, int pOffset, string pKeyWord)
            {
                DataTable tblReturn = new DataTable();
                string strDivisionCode = "";
                string strDepartmentCode = "";
                string strKeyWord = "";

                if (pDivisionCode != "ALL")
                {
                    strDivisionCode = " AND DivisionCode='" + pDivisionCode + "'";
                }

                if (pDepartmentCode != "ALL")
                {
                    strDepartmentCode = " AND DepartmentCode='" + pDepartmentCode + "'";
                }

                if (pKeyWord != string.Empty)
                {
                    strKeyWord = " AND (Title LIKE '%" + pKeyWord + "%' OR Description LIKE '%" + pKeyWord + "%' OR Contents LIKE  '%" + pKeyWord + "%' OR Contributor LIKE  '%" + pKeyWord + "%' OR CreateBy LIKE  '%" + pKeyWord + "%' OR PhotoSource LIKE '%" + pKeyWord + "%') ";
                }

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        //cmd.CommandText = "SELECT GroupUpdateCode, Title,Description,Contents, DepartmentCode, DivisionCode, CreateBy ,CreateOn FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1' " + strDivisionCode + strDepartmentCode + "  AND CreateOn BETWEEN @dtStart AND @dtEnd ORDER BY CreateOn DESC LIMIT " + pLimit + " OFFSET " + pOffset;
                        //cmd.CommandText = "SELECT * FROM (SELECT  row_number() OVER (ORDER BY GroupUpdateCode) AS rownum, *  FROM Portal.GroupUpdate WHERE Enabled='1'  AND Enabled='1' " + strDivisionCode + strDepartmentCode + "  AND CreateOn BETWEEN @dtStart AND @dtEnd) AS A WHERE  A.rownum BETWEEN (" + pOffset + ") AND (" + pOffset + " + " + pLimit + ")  ORDER BY CreateOn DESC";
                        cmd.CommandText = "SELECT RowNum, GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy ,CreateOn FROM (SELECT DISTINCT GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy , (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn,(row_number() OVER (ORDER BY CreateOn DESC)) AS RowNum  FROM Portal.GroupUpdate WHERE (Status='1' AND Enabled='1' " + strDivisionCode + strDepartmentCode + strKeyWord + "  AND (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) BETWEEN @dtStart AND @dtEnd)) AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
                        //cmd.CommandText = "SELECT RowNum, ThreadID,PostedBy, PostedDate FROM (SELECT DISTINCT ThreadID, PostedBy,PostedDate, (row_number() OVER (ORDER BY PostedDate DESC)) AS RowNum  FROM Portal.Threads  WHERE (IsPrivate = '0' AND IsActive = '1' AND PostedDate BETWEEN @dtStart AND @dateend)) AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
                        cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                        cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }

            public static DataTable GetDSGArchives(int pLimit, int pOffset)
            {
                DataTable tblReturn = new DataTable();

                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        //cmd.CommandText = "SELECT GroupUpdateCode, Title,Description,Contents, DepartmentCode, DivisionCode, CreateBy ,CreateOn FROM Portal.GroupUpdate WHERE Status = '1' AND Enabled='1' " + strDivisionCode + strDepartmentCode + "  AND CreateOn BETWEEN @dtStart AND @dtEnd ORDER BY CreateOn DESC LIMIT " + pLimit + " OFFSET " + pOffset;
                        //cmd.CommandText = "SELECT * FROM (SELECT  row_number() OVER (ORDER BY GroupUpdateCode) AS rownum, *  FROM Portal.GroupUpdate WHERE Enabled='1'  AND Enabled='1' " + strDivisionCode + strDepartmentCode + "  AND CreateOn BETWEEN @dtStart AND @dtEnd) AS A WHERE  A.rownum BETWEEN (" + pOffset + ") AND (" + pOffset + " + " + pLimit + ")  ORDER BY CreateOn DESC";
                        cmd.CommandText = "SELECT RowNum, GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy ,CreateOn FROM (SELECT DISTINCT GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy ,CreateOn,(row_number() OVER (ORDER BY CreateOn DESC)) AS RowNum  FROM Portal.GroupUpdate WHERE (Status='1' AND Enabled='1') AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblReturn);
                    }
                }
                return tblReturn;
            }
            
            public static string GetCreateBy(string pGroupUpdateCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Createby FROM Portal.GroupUpdate WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;

            }

            public static string GetContributor(string pGroupUpdateCode)
            {
                string strReturn = "";
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Contributor FROM Portal.GroupUpdate WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        strReturn = cmd.ExecuteScalar().ToString();
                    }
                }
                return strReturn;

            }

            public static int VoidGroupUpdate(string pGroupUpdateCode)
            {
                int intReturn = 0;
                using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
                {
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Portal.GroupUpdate SET Status='3' WHERE GroupUpdateCode=@GroupUpdateCode";
                        cmd.Parameters.Add(new SqlParameter("@GroupUpdateCode", pGroupUpdateCode));
                        cn.Open();
                        intReturn = cmd.ExecuteNonQuery();
                    }
                }
                return intReturn;
            }
        }
    }
}