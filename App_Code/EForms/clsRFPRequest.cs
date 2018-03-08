using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HRMS;
namespace STIeForms
{
 public class clsRFPRequest : IDisposable
 {
  private string _strRequestCode;
  private int _intItemRequestCode;
  private string _strControlNumber;
  private string _strRequestFor;
  private string _strAPVNumber;
  private string _strProjectTitle;
  private DateTime _dteDateNeeeded;
  private string _strRFANumber;
  private string _strPayeeName;
  private string _strSupportingDocument;
  private string _strRemarks;

  private string _strEndorsedBy1;
  private DateTime _dteEndorsedDate1;
  private string _strEndorsedStatus1;
  private string _strEndorsedBy2;
  private DateTime _dteEndorsedDate2;
  private string _strEndorsedStatus2;
  private string _strAuthorizedBy;
  private string _strAuthorizedStatus;
  private DateTime _dteAuthorizedDate;
  private string _strStatus;
  private string _strCreatedBy;
  private DateTime _dteCreateOn;
  private string _strModifyBy;
  private DateTime _dteModifyOn;

  public string RequestCode { get { return _strRequestCode; } set { _strRequestCode = value; } }
  public int ItemRequestCode { get { return _intItemRequestCode; } set { _intItemRequestCode = value; } }
  public string ControlNumber { get { return _strControlNumber; } set { _strControlNumber = value; } }
  public string RequestFor { get { return _strRequestFor; } set { _strRequestFor = value; } }
  public string APVNumber { get { return _strAPVNumber; } set { _strAPVNumber = value; } }
  public string ProjectTitle { get { return _strProjectTitle; } set { _strProjectTitle = value; } }
  public DateTime DateNeeded { get { return _dteDateNeeeded; } set { _dteDateNeeeded = value; } }
  public string RFANumber { get { return _strRFANumber; } set { _strRFANumber = value; } }
  public string PayeeName { get { return _strPayeeName; } set { _strPayeeName = value; } }
  public string SupportingDoument { get { return _strSupportingDocument; } set { _strSupportingDocument = value; } }
  public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }

  public string EndorsedBy1 { get { return _strEndorsedBy1; } set { _strEndorsedBy1 = value; } }
  public string EndorsedStatus1 { get { return _strEndorsedStatus1; } set { _strEndorsedStatus1 = value; } }
  public DateTime EndorsedDate1 { get { return _dteEndorsedDate1; } set { _dteEndorsedDate1 = value; } }

  public string EndorsedBy2 { get { return _strEndorsedBy2; } set { _strEndorsedBy2 = value; } }
  public string EndorsedStatus2 { get { return _strEndorsedStatus2; } set { _strEndorsedStatus2 = value; } }
  public DateTime EndorsedDate2 { get { return _dteEndorsedDate2; } set { _dteEndorsedDate2 = value; } }
  public string AuthorizedBy { get { return _strAuthorizedBy; } set { _strAuthorizedBy = value; } }
  public string AuthorizeStatus { get { return _strAuthorizedStatus; } set { _strAuthorizedStatus = value; } }
  public DateTime AuthorizedByDate { get { return _dteAuthorizedDate; } set { _dteAuthorizedDate = value; } }
  public string Status { get { return _strStatus; } set { _strStatus = value; } }
  public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
  public DateTime CreatedOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
  public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
  public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

  public clsRFPRequest()
  {
   _strRequestCode = string.Empty;
   _intItemRequestCode = 0;
   _strControlNumber = string.Empty;
   _strRequestFor = string.Empty;
   _strAPVNumber = string.Empty;
   _strProjectTitle = string.Empty;
   _dteDateNeeeded = DateTime.Now;
   _strRFANumber = string.Empty;
   _strPayeeName = string.Empty;
   _strSupportingDocument = string.Empty;
   _strRemarks = string.Empty;
   _strEndorsedBy1 = string.Empty;
   _strEndorsedStatus1 = string.Empty;
   _dteEndorsedDate1 = DateTime.Now;
   _strEndorsedBy2 = string.Empty;
   _strEndorsedStatus2 = string.Empty;
   _dteEndorsedDate2 = DateTime.Now;
   _strAuthorizedBy = "";
   _strAuthorizedStatus = "";
   _dteAuthorizedDate = DateTime.Now;
   _strStatus = "";
   _strCreatedBy = string.Empty;
   _dteCreateOn = DateTime.Now;
   _strModifyBy = string.Empty;
   _dteModifyOn = DateTime.Now;
  }
  public void Dispose() { GC.SuppressFinalize(this); }

  ///////////////////////////
  ///////// Methods /////////
  ///////////////////////////

  public void Fill()
  {
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strRequestCode = dr["rqstcode"].ToString();
     _strRequestFor = dr["rqstfor"].ToString();
     _strAPVNumber = dr["apvnmbr"].ToString();
     _strProjectTitle = dr["projttle"].ToString();
     _dteDateNeeeded = Convert.ToDateTime(dr["dateneed"].ToString());
     _strRFANumber = dr["rfanmbr"].ToString();
     _strPayeeName = dr["payename"].ToString();
     _strSupportingDocument = dr["sprtdocu"].ToString();
     _strRemarks = dr["remarks"].ToString();
     _strEndorsedBy1 = dr["endrsby1"].ToString();
     _strEndorsedStatus1 = dr["endrstt1"].ToString();
     _dteEndorsedDate1 = Convert.ToDateTime(dr["e1aprvd"].ToString());
     _strEndorsedBy2 = dr["endrsby2"].ToString();
     _strEndorsedStatus2 = dr["endrstt2"].ToString();
     _dteEndorsedDate2 = Convert.ToDateTime(dr["e2aprvd"].ToString());

     _strAuthorizedBy = dr["authrzby"].ToString();
     _strAuthorizedStatus = dr["authstat"].ToString();
     _dteAuthorizedDate = Convert.ToDateTime(dr["aaprdate"].ToString());

     _strStatus = dr["statcode"].ToString();
     _strCreatedBy = dr["createby"].ToString();
     _dteCreateOn = Convert.ToDateTime(dr["createon"].ToString());
     _strModifyBy = dr["modifyby"].ToString();
     _dteModifyOn = Convert.ToDateTime(dr["modifyon"].ToString());
    }
   }
  }

  public int Insert(DataTable pItems, string pSaveType)
  {
      int intReturn = 0;
      int intSeed = 0;
      int intMonth = 0;
      int intYear = 0;
      string strMonth = "";

      DateTime dtDateToday = DateTime.Now;
      SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF);
      cn.Open();
      SqlTransaction tran = cn.BeginTransaction();
      SqlCommand cmd = cn.CreateCommand();
      cmd.Transaction = tran;

      try
      {
          cmd.CommandText = "SELECT xvalue FROM Finance.RFPPrimaryKey Where xkey='CurrentYear'";
          intYear = Convert.ToInt32(cmd.ExecuteScalar().ToString());

          cmd.CommandText = "SELECT xvalue FROM Finance.RFPPrimaryKey Where xkey='CurrentMonth'";
          intMonth = Convert.ToInt32(cmd.ExecuteScalar().ToString());

          if (intYear != dtDateToday.Year.ToString().ToInt())
          {
              intMonth = 1;
              intYear = intYear + 1;
              cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentYear'";
              cmd.Parameters.Add(new SqlParameter("@xvalue", intYear));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();

              cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
              cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();

              strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
              cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=0 Where xkey='ControlNumber'";
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();
          }
          else
          {
              if (Convert.ToInt32(dtDateToday.Month.ToString()) == intMonth)
              {
                  strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
              }
              else
              {
                  intMonth = dtDateToday.Month.ToString().ToInt();

                  cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
                  cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
                  intReturn = cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();

                  strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);

                  cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=0 Where xkey='ControlNumber'";
                  intReturn = cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();
              }
          }


          //if (Convert.ToInt32(dtDateToday.Month.ToString()) == intMonth)
          //{
          //    strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
          //}
          //else
          //{

          //    if (intMonth > 12)
          //    {
          //        //intMonth = 1;
          //        //intYear = intYear + 1;
          //        //cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentYear'";
          //        //cmd.Parameters.Add(new SqlParameter("@xvalue", intYear));
          //        //intReturn = cmd.ExecuteNonQuery();
          //        //cmd.Parameters.Clear();

          //        //cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
          //        //cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
          //        //intReturn = cmd.ExecuteNonQuery();
          //        //cmd.Parameters.Clear();

          //        //strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
          //        //cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=0 Where xkey='ControlNumber'";
          //        //intReturn = cmd.ExecuteNonQuery();
          //        //cmd.Parameters.Clear();
          //    }
          //    else
          //    {
          //        //intMonth = intMonth + 1;

          //        //cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
          //        //cmd.Parameters.Add(new SqlParameter("@xvalue", intMonth));
          //        //intReturn = cmd.ExecuteNonQuery();
          //        //cmd.Parameters.Clear();

          //        //strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);

          //        //cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=0 Where xkey='ControlNumber'";
          //        //intReturn = cmd.ExecuteNonQuery();
          //        //cmd.Parameters.Clear();
          //    }

          //}

          cmd.CommandText = "SELECT xvalue FROM Finance.RFPPrimaryKey Where xkey='ControlNumber'";
          intSeed = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

          cmd.CommandText = "UPDATE Finance.RFPPrimaryKey SET xvalue=@xvalue Where xkey='ControlNumber'";
          cmd.Parameters.Add(new SqlParameter("@xvalue", intSeed));
          intReturn = cmd.ExecuteNonQuery();
          cmd.Parameters.Clear();

          _strControlNumber = (intYear + "-" + strMonth + "-" + ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length));

          cmd.CommandText = "INSERT INTO Finance.RFPRequest VALUES(@ctrlnmbr, @rqstcode, @rqstfor,@projttle, @apvnmbr, @dateneed, @rfanmbr, @payename, @sprtdocu, @remarks, @endrsby1,@endrstt1,@e1aprvd,@endrsby2,@endrstt2,@e2aprvd,@authrzby,@authstat,@aaprdate,'F', @statcode, @createby, @createon,@modifyby,@modifyon)";
          cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
          cmd.Parameters.Add(new SqlParameter("@rqstcode", _strRequestCode));
          cmd.Parameters.Add(new SqlParameter("@rqstfor", _strRequestFor));
          cmd.Parameters.Add(new SqlParameter("@projttle", _strProjectTitle));
          cmd.Parameters.Add(new SqlParameter("@apvnmbr", _strAPVNumber));
          cmd.Parameters.Add(new SqlParameter("@dateneed", Convert.ToDateTime(_dteDateNeeeded).ToShortDateString()));
          cmd.Parameters.Add(new SqlParameter("@rfanmbr", _strRFANumber));
          cmd.Parameters.Add(new SqlParameter("@payename", _strPayeeName));
          cmd.Parameters.Add(new SqlParameter("@sprtdocu", _strSupportingDocument));
          cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
          cmd.Parameters.Add(new SqlParameter("@endrsby1", _strEndorsedBy1));
          cmd.Parameters.Add(new SqlParameter("@endrstt1", _strEndorsedStatus1));
          cmd.Parameters.Add(new SqlParameter("@e1aprvd", Convert.ToDateTime(_dteEndorsedDate1).ToShortDateString()));
          cmd.Parameters.Add(new SqlParameter("@endrsby2", _strEndorsedBy2));
          cmd.Parameters.Add(new SqlParameter("@endrstt2", _strEndorsedStatus2));
          cmd.Parameters.Add(new SqlParameter("@e2aprvd", Convert.ToDateTime(_dteEndorsedDate2).ToShortDateString()));

          cmd.Parameters.Add(new SqlParameter("@authrzby", _strAuthorizedBy));
          cmd.Parameters.Add(new SqlParameter("@authstat", _strAuthorizedStatus));
          cmd.Parameters.Add(new SqlParameter("@aaprdate", Convert.ToDateTime(_dteAuthorizedDate).ToShortDateString()));

          cmd.Parameters.Add(new SqlParameter("@statcode", _strStatus));
          cmd.Parameters.Add(new SqlParameter("@createby", _strCreatedBy));
          cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));
          cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
          cmd.Parameters.Add(new SqlParameter("@modifyon", _dteModifyOn));

          intReturn = cmd.ExecuteNonQuery();
          cmd.Parameters.Clear();

          //Add RequestDetails

          foreach (DataRow drw in pItems.Rows)
          {
              cmd.CommandText = "SELECT ritmcode FROM Finance.RFPRequestDetails ORDER BY ritmcode DESC";
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  dr.Close();
                  _intItemRequestCode = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
              }
              else
              {
                  dr.Close();
                  _intItemRequestCode = 1;
              }

              cmd.CommandText = "INSERT INTO Finance.RFPRequestDetails VALUES (@ritmcode, @cntrlnmbr, @itemdesc,@schlcode, @rccode, @others, @amount)";
              cmd.Parameters.Add(new SqlParameter("@ritmcode", _intItemRequestCode));
              cmd.Parameters.Add(new SqlParameter("@cntrlnmbr", _strControlNumber));
              cmd.Parameters.Add(new SqlParameter("@itemdesc", drw["itemdesc"].ToString().Trim()));
              cmd.Parameters.Add(new SqlParameter("@schlcode", drw["schlcode"].ToString().Trim()));
              cmd.Parameters.Add(new SqlParameter("@rccode", drw["rccode"].ToString().Trim()));
              cmd.Parameters.Add(new SqlParameter("@others", drw["others"].ToString().Trim()));
              cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drw["amount"].ToString())));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();
          }
          tran.Commit();
      }

      catch { tran.Rollback(); }
      finally { cn.Close(); }

      if (pSaveType == "PRINT")
      {
          string strApproverName = "";

          using (SqlConnection con = new SqlConnection(clsSpeedo.SpeedoConnectionString))
          {
              SqlCommand cmcom = con.CreateCommand();
              cmcom.CommandText = "UPDATE Finance.RFPRequest SET statcode='4' WHERE ctrlnmbr=@ctrlnmbr";
              cmcom.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
              con.Open();
              intReturn = cmcom.ExecuteNonQuery();
              cmcom.Parameters.Clear();
          }

          ///////////////////////////////////////////No Endorser/////////////////////////////////////////////
          if (clsFinanceApprover.IsHaveNoEndorser("ctrlnmbr", _strControlNumber, "RFPRequest"))
          {
              strApproverName = clsEmployee.GetName(_strAuthorizedBy);
              SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strAuthorizedBy);
              SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
          }
          ///////////////////////////////////////////With Two Endorser////////////////////////////////////////
          else if (clsFinanceApprover.IsHave2ndEndorser("ctrlnmbr", _strControlNumber, "RFPRequest"))
          {
              strApproverName = clsEmployee.GetName(_strEndorsedBy1) + " and " + clsEmployee.GetName(_strEndorsedBy2);
              SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy1);
              SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy2);
              SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
          }
          ///////////////////////////////////////////One Endorser/////////////////////////////////////////////
          else
          {
              strApproverName = clsEmployee.GetName(_strEndorsedBy1);
              SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy1);
              SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
          }
      }

      return intReturn;
  }

  public int Update(DataTable pItems, string pSaveType)
  {
   int intReturn = 0;

   SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;

   try
   {
    cmd.CommandText = "UPDATE Finance.RFPRequest SET rqstcode=@rqstcode, rqstfor=@rqstfor, projttle=@projttle, apvnmbr=@apvnmbr, dateneed=@dateneed, rfanmbr=@rfanmbr, payename=@payename, sprtdocu=@sprtdocu, remarks=@remarks, endrsby1=@endrsby1, endrstt1=@endrstt1, e1aprvd=@e1aprvd, endrsby2=@endrsby2, endrstt2=@endrstt2, e2aprvd=@e2aprvd, authrzby=@authrzby,authstat=@authstat,aaprdate=@aaprdate, statcode=@statcode, modifyby=@modifyby, modifyon=@modifyon WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
    cmd.Parameters.Add(new SqlParameter("@rqstcode", _strRequestCode));
    cmd.Parameters.Add(new SqlParameter("@rqstfor", _strRequestFor));
    cmd.Parameters.Add(new SqlParameter("@projttle", _strProjectTitle));
    cmd.Parameters.Add(new SqlParameter("@apvnmbr", _strAPVNumber));
    cmd.Parameters.Add(new SqlParameter("@dateneed", Convert.ToDateTime(_dteDateNeeeded).ToShortDateString()));
    cmd.Parameters.Add(new SqlParameter("@rfanmbr", _strRFANumber));
    cmd.Parameters.Add(new SqlParameter("@payename", _strPayeeName));
    cmd.Parameters.Add(new SqlParameter("@sprtdocu", _strSupportingDocument));
    cmd.Parameters.Add(new SqlParameter("@remarks", _strRemarks));
    cmd.Parameters.Add(new SqlParameter("@endrsby1", _strEndorsedBy1));
    cmd.Parameters.Add(new SqlParameter("@endrstt1", _strEndorsedStatus1));
    cmd.Parameters.Add(new SqlParameter("@e1aprvd", Convert.ToDateTime(_dteEndorsedDate1).ToShortDateString()));
    cmd.Parameters.Add(new SqlParameter("@endrsby2", _strEndorsedBy2));
    cmd.Parameters.Add(new SqlParameter("@endrstt2", _strEndorsedStatus2));
    cmd.Parameters.Add(new SqlParameter("@e2aprvd", Convert.ToDateTime(_dteEndorsedDate2).ToShortDateString()));
    cmd.Parameters.Add(new SqlParameter("@authrzby", _strAuthorizedBy));
    cmd.Parameters.Add(new SqlParameter("@authstat", _strAuthorizedStatus));
    cmd.Parameters.Add(new SqlParameter("@aaprdate", Convert.ToDateTime(_dteAuthorizedDate).ToShortDateString()));
    cmd.Parameters.Add(new SqlParameter("@statcode", _strStatus));
    cmd.Parameters.Add(new SqlParameter("@modifyby", _strModifyBy));
    cmd.Parameters.Add(new SqlParameter("@modifyon", DateTime.Now));

    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    //Delete Previous Records
    cmd.CommandText = "DELETE FROM Finance.RFPRequestDetails WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    //Add RequestDetails
    foreach (DataRow drw in pItems.Rows)
    {
     cmd.CommandText = "SELECT ritmcode FROM Finance.RFPRequestDetails ORDER BY ritmcode DESC";
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      dr.Close();
      _intItemRequestCode = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);
     }
     else
     {
      dr.Close();
      _intItemRequestCode = 1;
     }

     cmd.CommandText = "INSERT INTO Finance.RFPRequestDetails VALUES (@ritmcode, @cntrlnmbr, @itemdesc,@schlcode, @rccode, @others, @amount)";
     cmd.Parameters.Add(new SqlParameter("@ritmcode", _intItemRequestCode));
     cmd.Parameters.Add(new SqlParameter("@cntrlnmbr", _strControlNumber));
     cmd.Parameters.Add(new SqlParameter("@itemdesc", drw["itemdesc"].ToString().Trim()));
     cmd.Parameters.Add(new SqlParameter("@schlcode", drw["schlcode"].ToString().Trim()));
     cmd.Parameters.Add(new SqlParameter("@rccode", drw["rccode"].ToString().Trim()));
     cmd.Parameters.Add(new SqlParameter("@others", drw["others"].ToString().Trim()));
     cmd.Parameters.Add(new SqlParameter("@amount", Convert.ToDouble(drw["amount"].ToString())));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();
    }
    tran.Commit();
   }
   catch { tran.Rollback(); }
   finally { cn.Close(); }

   if (pSaveType == "PRINT")
   {
    string strApproverName = "";

    using (SqlConnection con = new SqlConnection(clsSpeedo.SpeedoConnectionString))
    {
     SqlCommand cmcom = con.CreateCommand();
     cmcom.CommandText = "UPDATE Finance.RFPRequest SET statcode='4' WHERE ctrlnmbr=@ctrlnmbr";
     cmcom.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
     con.Open();
     intReturn = cmcom.ExecuteNonQuery();
     cmcom.Parameters.Clear();
    }

    ///////////////////////////////////////////No Endorser/////////////////////////////////////////////
    if (clsFinanceApprover.IsHaveNoEndorser("ctrlnmbr", _strControlNumber, "RFPRequest"))
    {
     strApproverName = clsEmployee.GetName(_strAuthorizedBy);
     SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strAuthorizedBy);
     SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
    }
    ///////////////////////////////////////////With Two Endorser////////////////////////////////////////
    else if (clsFinanceApprover.IsHave2ndEndorser("ctrlnmbr", _strControlNumber, "RFPRequest"))
    {
     strApproverName = clsEmployee.GetName(_strEndorsedBy1) + " and " + clsEmployee.GetName(_strEndorsedBy2);
     SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy1);
     SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy2);
     SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
    }
    ///////////////////////////////////////////One Endorser/////////////////////////////////////////////
    else
    {
     strApproverName = clsEmployee.GetName(_strEndorsedBy1);
     SendEmailNotification("Approver", _strControlNumber, _strCreatedBy, _strEndorsedBy1);
     SendEmailNotification("Requestor", _strControlNumber, _strCreatedBy, strApproverName);
    }
   }

   return intReturn;
  }

  public string AddedControlNumber(string pLoggedUser)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr FROM Finance.RFPRequest WHERE createby = @pLoggedUser ORDER BY createon DESC";
    cmd.Parameters.Add(new SqlParameter("@createby", pLoggedUser));

    strReturn = _strControlNumber;
   }

   return strReturn;
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////

  public void SendEmailNotification(string MailType, string pControlNumber, string pMailFrom, string pMailTo)
  {
   string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   string strSubject = "";
   string strBody = "";

   if (MailType == "Approver")
   {
    strSubject = "For Your Approval: Request For Payment";
    strBody = "Hi " + clsEmployee.GetName(pMailTo) + ",<br><br>" +
              "There is a Request for Payment submitted by " + clsEmployee.GetName(pMailFrom) + ".<br><br>" +
              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
              "If you can't click on the above link,<br>" +
              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
              "All the best,<br>E-Forms Administrator";
    clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);
   }
   else if (MailType == "Requestor")
   {
    strSubject = "Delivered: RFP Request";
    strBody = "Hi " + clsEmployee.GetName(pMailFrom) + ",<br><br>" +
              "Your RFP Request has been successfully sent to your respective approvers.<br>" +
              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
              "If you can't click on the above link,<br>" +
              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
              "All the best,<br>E-Forms Administrator";
    clsSpeedo.SendMail(clsUsers.GetEmail(pMailFrom), strSubject, strBody);
   }



  }

  public static DataTable GetDSGMainForm(string pSelect, string pWhere, int intStart, int intEnd)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("ControlNumber");
   tblReturn.Columns.Add("RequestCode");
   tblReturn.Columns.Add("RequestFor");
   tblReturn.Columns.Add("ProjectTitle");
   tblReturn.Columns.Add("DateNeeded");
   tblReturn.Columns.Add("EndorsedBy1");
   tblReturn.Columns.Add("Endorsed1Status");
   tblReturn.Columns.Add("EndorsedBy2");
   tblReturn.Columns.Add("Endorsed2Status");
   tblReturn.Columns.Add("AuthorizeBy");
   tblReturn.Columns.Add("AuthorizeStatus");
   tblReturn.Columns.Add("Status");
   tblReturn.Columns.Add("PayeeName");
   tblReturn.Columns.Add("CreatedBy");
   tblReturn.Columns.Add("CreatedOn");

   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.RFPRequest) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     DataRow drwNew = tblReturn.NewRow();
     drwNew["ControlNumber"] = dr["ctrlnmbr"].ToString();
     drwNew["RequestCode"] = dr["rqstcode"].ToString();
     drwNew["RequestFor"] = dr["rqstfor"].ToString();
     drwNew["ProjectTitle"] = dr["projttle"].ToString();
     drwNew["DateNeeded"] = Convert.ToDateTime(dr["dateneed"].ToString());
     drwNew["PayeeName"] = dr["payename"].ToString();
     drwNew["EndorsedBy1"] = dr["endrsby1"].ToString();
     drwNew["Endorsed1Status"] = dr["endrstt1"].ToString();
     drwNew["EndorsedBy2"] = dr["endrsby2"].ToString();
     drwNew["Endorsed2Status"] = dr["endrstt2"].ToString();
     drwNew["AuthorizeBy"] = dr["authrzby"].ToString();
     drwNew["AuthorizeStatus"] = dr["authstat"].ToString();
     drwNew["Status"] = dr["statcode"].ToString();
     drwNew["CreatedBy"] = dr["createby"].ToString();
     drwNew["CreatedOn"] = Convert.ToDateTime(dr["createon"].ToString());
     tblReturn.Rows.Add(drwNew);
    }
    dr.Close();
   }
   return tblReturn;
  }

  public static DataTable GetDSGMainFormPerUser(string pSelect, string pUserName, string pWhere, int intStart, int intEnd)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("ControlNumber");
   tblReturn.Columns.Add("RequestCode");
   tblReturn.Columns.Add("RequestFor");
   tblReturn.Columns.Add("ProjectTitle");
   tblReturn.Columns.Add("DateNeeded");
   tblReturn.Columns.Add("EndorsedBy1");
   tblReturn.Columns.Add("Endorsed1Status");
   tblReturn.Columns.Add("EndorsedBy2");
   tblReturn.Columns.Add("Endorsed2Status");
   tblReturn.Columns.Add("AuthorizeBy");
   tblReturn.Columns.Add("AuthorizeStatus");
   tblReturn.Columns.Add("Status");
   tblReturn.Columns.Add("PayeeName");
   tblReturn.Columns.Add("CreatedBy");
   tblReturn.Columns.Add("CreatedOn");

   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.RFPRequest WHERE createby = @UserName) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
    cmd.Parameters.Add(new SqlParameter("@UserName", pUserName));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     DataRow drwNew = tblReturn.NewRow();
     drwNew["ControlNumber"] = dr["ctrlnmbr"].ToString();
     drwNew["RequestCode"] = dr["rqstcode"].ToString();
     drwNew["RequestFor"] = dr["rqstfor"].ToString();
     drwNew["ProjectTitle"] = dr["projttle"].ToString();
     drwNew["DateNeeded"] = Convert.ToDateTime(dr["dateneed"].ToString());
     drwNew["PayeeName"] = dr["payename"].ToString();
     drwNew["EndorsedBy1"] = dr["endrsby1"].ToString();
     drwNew["Endorsed1Status"] = dr["endrstt1"].ToString();
     drwNew["EndorsedBy2"] = dr["endrsby2"].ToString();
     drwNew["Endorsed2Status"] = dr["endrstt2"].ToString();
     drwNew["AuthorizeBy"] = dr["authrzby"].ToString();
     drwNew["AuthorizeStatus"] = dr["authstat"].ToString();
     drwNew["Status"] = dr["statcode"].ToString();
     drwNew["CreatedBy"] = dr["createby"].ToString();
     drwNew["CreatedOn"] = Convert.ToDateTime(dr["createon"].ToString());
     tblReturn.Rows.Add(drwNew);
    }
    dr.Close();
   }
   return tblReturn;
  }

  public static DataTable GetDSGMainFormApprover(string pUsername)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("ControlNumber");
   tblReturn.Columns.Add("RequestCode");
   tblReturn.Columns.Add("RequestFor");
   tblReturn.Columns.Add("ProjectTitle");
   tblReturn.Columns.Add("DateNeeded");
   tblReturn.Columns.Add("EndorsedBy1");
   tblReturn.Columns.Add("Endorsed1Status");
   tblReturn.Columns.Add("EndorsedBy2");
   tblReturn.Columns.Add("Endorsed2Status");
   tblReturn.Columns.Add("AuthorizeBy");
   tblReturn.Columns.Add("AuthorizeStatus");
   tblReturn.Columns.Add("Status");
   tblReturn.Columns.Add("PayeeName");
   tblReturn.Columns.Add("CreatedBy");
   tblReturn.Columns.Add("CreatedOn");

   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr, rqstcode, rqstfor, projttle,dateneed, payename,endrsby1,endrstt1,endrsby2,endrstt2,authrzby,authstat,statcode, createby, createon FROM Finance.RFPRequest WHERE statcode='4' AND (endrsby1=@username OR endrsby2=@username OR authrzby=@username) ORDER BY createon desc";
    cmd.Parameters.Add(new SqlParameter("@username", pUsername));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     DataRow drwNew = tblReturn.NewRow();
     drwNew["ControlNumber"] = dr["ctrlnmbr"].ToString();
     drwNew["RequestCode"] = dr["rqstcode"].ToString();
     drwNew["RequestFor"] = dr["rqstfor"].ToString();
     drwNew["ProjectTitle"] = dr["projttle"].ToString();
     drwNew["DateNeeded"] = Convert.ToDateTime(dr["dateneed"].ToString());
     drwNew["PayeeName"] = dr["payename"].ToString();
     drwNew["EndorsedBy1"] = dr["endrsby1"].ToString();
     drwNew["Endorsed1Status"] = dr["endrstt1"].ToString();
     drwNew["EndorsedBy2"] = dr["endrsby2"].ToString();
     drwNew["Endorsed2Status"] = dr["endrstt2"].ToString();
     drwNew["AuthorizeBy"] = dr["authrzby"].ToString();
     drwNew["AuthorizeStatus"] = dr["authstat"].ToString();
     drwNew["Status"] = dr["statcode"].ToString();
     drwNew["CreatedBy"] = dr["createby"].ToString();
     drwNew["CreatedOn"] = Convert.ToDateTime(dr["createon"].ToString());
     tblReturn.Rows.Add(drwNew);
    }
    dr.Close();
   }
   return tblReturn;
  }

  public static string GetRequestStatusIcon(string pCataStatus)
  {
   string strReturn = "";
   if (pCataStatus == "0")
    strReturn = "Disapproved.png";
   else if (pCataStatus == "2")
    strReturn = "Approval.png";
   else if (pCataStatus == "1")
    strReturn = "print32.png";
   return strReturn;
  }

  public static string GetRequestor(string pControlCode)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT createby FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlCode));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["createby"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

  public static string GetApprover(string pControlCode, string pApproverType)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pApproverType == "endrsby1")
    {
     cmd.CommandText = "SELECT endrsby1 FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlCode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      strReturn = dr["endrsby1"].ToString();
     }
     cn.Close();
    }
    else if (pApproverType == "endrsby2")
    {
     cmd.CommandText = "SELECT endrsby2 FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlCode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      strReturn = dr["endrsby2"].ToString();
     }
     cn.Close();
    }
    else if (pApproverType == "authrzby")
    {
     cmd.CommandText = "SELECT authrzby FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlCode));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      strReturn = dr["authrzby"].ToString();
     }
     cn.Close();
    }
   }
   return strReturn;
  }

  public static string GetAuthority(string pControlNumber)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT authrzby FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["authrzby"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

  public static string GetEndorser1(string pControlNumber)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT endrsby1 FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["endrsby1"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

  public static string GetEndorser2(string pControlNumber)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT endrsby2 FROM Finance.RFPRequest WHERE ctrlnmbr =@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     strReturn = dr["endrsby2"].ToString();
    }
    cn.Close();
   }
   return strReturn;
  }

  public static bool IsHave2ndEndorser(string pControlNumber)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT endrsby2 FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
     if (dr["endrsby2"].ToString().Trim().Length > 0)
     {
      blnReturn = true;
     }
     else
     {
      blnReturn = false;
     }
    }
    dr.Close();
   }
   return blnReturn;
  }

  public static Boolean IsApprovedbyEndorser1(string pControlNumber)
  {
   Boolean blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr AND endrstt1 <> '2'";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();

    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    { blnReturn = true; }
   }
   return blnReturn;
  }

  public static Boolean IsApprovedbyEndorser2(string pControlNumber)
  {
   Boolean blnReturn = false;
   string strResult = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr AND endrstt2 <> '2'";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();

    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    { blnReturn = true; }
   }
   return blnReturn;
  }

  public static int Approve(string pControlNumber, string pApproverType, string pStatus, string pMailTo)
  {
   string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
   int intReturn = 0;
   string strSubject = "";
   string strBody = "";
   string strStatus = (pStatus == "1" ? "Approved" : "Disapproved");

   SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
   cn.Open();
   //SqlTransaction trans = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   //cmd.Transaction = trans;

   try
   {
    if (pStatus == "0")
    {
     cmd.CommandText = "UPDATE Finance.RFPRequest SET statcode=@statcode WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();
    }

    if (pApproverType == "endrsby1")
    {
     cmd.CommandText = "UPDATE Finance.RFPRequest SET endrstt1=@endrstt1,e1aprvd=@e1aprvd WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cmd.Parameters.Add(new SqlParameter("@e1aprvd", DateTime.Now));
     cmd.Parameters.Add(new SqlParameter("@endrstt1", pStatus));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();

     if (!clsRFPRequest.IsHave2ndEndorser(pControlNumber))
     {
      if (intReturn > 0)
      {
       //send email to requestor
       strSubject = strStatus + " Request For Payment";
       strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
               "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
               "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
               "If you can't click on the above link,<br>" +
               "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
              "All the best,<br>E-Forms Administrator";
       clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

       //send email to endorser
       strSubject = strStatus + " Request For Payment";
       strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                 "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                 "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                 "If you can't click on the above link,<br>" +
                 "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                 "All the best,<br>E-Forms Administrator";
       clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

       if (pStatus == "1")
       {
        //send email to authorizer
        strSubject = "For Your Approval: Request For Payment";
        strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                  "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                  "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                  "If you can't click on the above link,<br>" +
                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                  "All the best,<br>E-Forms Administrator";
        clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
       }
      }
     }

     else
     {
      //send email to requestor
      strSubject = strStatus + " Request For Payment";
      strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
              "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
              "If you can't click on the above link,<br>" +
              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
             "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

      //send email to endorser
      strSubject = strStatus + " Request For Payment";
      strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

      if (pStatus == "1")
      {
       if (clsRFPRequest.IsApprovedbyEndorser2(pControlNumber))
       {
        //send email to authorizer
        strSubject = "For Your Approval: Request For Payment";
        strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                  "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                  "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                  "If you can't click on the above link,<br>" +
                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                  "All the best,<br>E-Forms Administrator";
        clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
       }
      }
     }

    }

    else if (pApproverType == "endrsby2")
    {
     cmd.CommandText = "UPDATE Finance.RFPRequest SET endrstt2=@endrstt2,e2aprvd=@e2aprvd WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cmd.Parameters.Add(new SqlParameter("@e2aprvd", DateTime.Now));
     cmd.Parameters.Add(new SqlParameter("@endrstt2", pStatus));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();

     if (intReturn > 0)
     {
      //send email to requestor
      strSubject = strStatus + " Request For Payment";
      strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
              "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
              "If you can't click on the above link,<br>" +
              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
             "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

      //send email to endorser
      strSubject = strStatus + " Request For Payment";
      strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

      if (pStatus == "1")
      {
       if (clsRFPRequest.IsApprovedbyEndorser1(pControlNumber))
       {
        //send email to authorizer
        strSubject = "For Your Approval: Request For Payment";
        strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                  "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                  "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                  "If you can't click on the above link,<br>" +
                  "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                  "All the best,<br>E-Forms Administrator";
        clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
       }
      }
     }
    }

    else if (pApproverType == "authrzby")
    {
     cmd.CommandText = "UPDATE Finance.RFPRequest SET authstat=@authstat,aaprdate=@aaprdate WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cmd.Parameters.Add(new SqlParameter("@aaprdate", DateTime.Now));
     cmd.Parameters.Add(new SqlParameter("@authstat", pStatus));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();

     if (intReturn > 0)
     {
      cmd.CommandText = "UPDATE Finance.RFPRequest SET statcode=@statcode WHERE ctrlnmbr=@ctrlnmbr";
      cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
      cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
      intReturn = cmd.ExecuteNonQuery();
      cmd.Parameters.Clear();

      //send email to Authorize by
      strSubject = strStatus + " Request For Payment";
      strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                "If you can't click on the above link,<br>" +
                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);

      //send email to requestor
      strSubject = strStatus + " Request For Payment";
      strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
              "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ".<br><br>" +
              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
              "If you can't click on the above link,<br>" +
              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
             "All the best,<br>E-Forms Administrator";
      clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

     }
    }
    //trans.Commit();
   }

   catch (Exception ex)
   {
    System.Web.HttpContext.Current.Response.Write("Warning<br>Message: " + ex.Message + "<br>Source: " + ex.TargetSite);
    //trans.Rollback();
    cn.Close();
   }
   finally
   {
    cn.Close();
   }
   return intReturn;
  }

  public static int ApproveDocuReq(string pControlNumber, string pApproverType, string pStatus, string pMailTo, string pDocumentRequiredStatus)
  {
      string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
      int intReturn = 0;
      string strSubject = "";
      string strBody = "";
      string strStatus = (pStatus == "1" ? "Approved" : "Disapproved");

      SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
      cn.Open();
      //SqlTransaction trans = cn.BeginTransaction();
      SqlCommand cmd = cn.CreateCommand();
      //cmd.Transaction = trans;

      try
      {
          if (pStatus == "0")
          {
              cmd.CommandText = "UPDATE Finance.RFPRequest SET statcode=@statcode WHERE ctrlnmbr=@ctrlnmbr";
              cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
              cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();
          }

          if (pApproverType == "endrsby1")
          {
              cmd.CommandText = "UPDATE Finance.RFPRequest SET endrstt1=@endrstt1,e1aprvd=@e1aprvd WHERE ctrlnmbr=@ctrlnmbr";
              cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
              cmd.Parameters.Add(new SqlParameter("@e1aprvd", DateTime.Now));
              cmd.Parameters.Add(new SqlParameter("@endrstt1", pStatus));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();

              if (!clsRFPRequest.IsHave2ndEndorser(pControlNumber))
              {
                  if (intReturn > 0)
                  {
                      //send email to requestor
                      strSubject = strStatus + " Request For Payment";
                      strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
                              "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
                              "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                              "If you can't click on the above link,<br>" +
                              "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                             "All the best,<br>E-Forms Administrator";
                      clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

                      //send email to endorser
                      strSubject = strStatus + " Request For Payment";
                      strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                                "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                                "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                                "If you can't click on the above link,<br>" +
                                "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                                "All the best,<br>E-Forms Administrator";
                      clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                      if (pStatus == "1")
                      {
                          //send email to authorizer
                          strSubject = "For Your Approval: Request For Payment";
                          strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                                    "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                                    "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                                    "If you can't click on the above link,<br>" +
                                    "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                                    "All the best,<br>E-Forms Administrator";
                          clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
                      }
                  }
              }

              else
              {
                  //send email to requestor
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
                          "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

                  //send email to endorser
                  strSubject = strStatus + " Request For Payment";
                  strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                            "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                            "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                            "If you can't click on the above link,<br>" +
                            "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                            "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                  if (pStatus == "1")
                  {
                      if (clsRFPRequest.IsApprovedbyEndorser2(pControlNumber))
                      {
                          //send email to authorizer
                          strSubject = "For Your Approval: Request For Payment";
                          strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                                    "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                                    "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                                    "If you can't click on the above link,<br>" +
                                    "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                                    "All the best,<br>E-Forms Administrator";
                          clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
                      }
                  }
              }

          }

          else if (pApproverType == "endrsby2")
          {
              cmd.CommandText = "UPDATE Finance.RFPRequest SET endrstt2=@endrstt2,e2aprvd=@e2aprvd WHERE ctrlnmbr=@ctrlnmbr";
              cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
              cmd.Parameters.Add(new SqlParameter("@e2aprvd", DateTime.Now));
              cmd.Parameters.Add(new SqlParameter("@endrstt2", pStatus));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();

              if (intReturn > 0)
              {
                  //send email to requestor
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
                          "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetApprover(pControlNumber, pApproverType)) + ".<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

                  //send email to endorser
                  strSubject = strStatus + " Request For Payment";
                  strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                            "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                            "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                            "If you can't click on the above link,<br>" +
                            "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                            "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(pMailTo), strSubject, strBody);

                  if (pStatus == "1")
                  {
                      if (clsRFPRequest.IsApprovedbyEndorser1(pControlNumber))
                      {
                          //send email to authorizer
                          strSubject = "For Your Approval: Request For Payment";
                          strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ",<br><br>" +
                                    "There is a Request for Payment submitted by " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ".<br><br>" +
                                    "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                                    "If you can't click on the above link,<br>" +
                                    "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetailsApprover.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                                    "All the best,<br>E-Forms Administrator";
                          clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);
                      }
                  }
              }
          }

          else if (pApproverType == "authrzby")
          {
              cmd.CommandText = "UPDATE Finance.RFPRequest SET authstat=@authstat,aaprdate=@aaprdate WHERE ctrlnmbr=@ctrlnmbr";
              cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
              cmd.Parameters.Add(new SqlParameter("@aaprdate", DateTime.Now));
              cmd.Parameters.Add(new SqlParameter("@authstat", pStatus));
              intReturn = cmd.ExecuteNonQuery();
              cmd.Parameters.Clear();

              if (intReturn > 0)
              {//updated by Rollie for RFP update 2015-07-28
                  cmd.CommandText = "UPDATE Finance.RFPRequest SET statcode=@statcode,reqdocum=@reqdocum WHERE ctrlnmbr=@ctrlnmbr";
                  cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
                  cmd.Parameters.Add(new SqlParameter("@statcode", pStatus));
                  cmd.Parameters.Add(new SqlParameter("@reqdocum", pDocumentRequiredStatus));
                  intReturn = cmd.ExecuteNonQuery();
                  cmd.Parameters.Clear();

                  //send email to Authorize by
                  strSubject = strStatus + " Request For Payment";
                  strBody = "You " + strStatus.ToLower() + " a Request for Payment.<br><br>" +
                            "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                            "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                            "If you can't click on the above link,<br>" +
                            "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                            "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);

                  //send email to requestor
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
                          "Your RFP has been " + strStatus.ToLower() + " by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ".<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);

              }
          }
          //trans.Commit();
      }

      catch (Exception ex)
      {
          System.Web.HttpContext.Current.Response.Write("Warning<br>Message: " + ex.Message + "<br>Source: " + ex.TargetSite);
          //trans.Rollback();
          cn.Close();
      }
      finally
      {
          cn.Close();
      }
      return intReturn;
  }

  public static int TagDocumentRequired(string pControlNumber, string pApproverType, string pStatus, string pMailTo, string pDocumentRequiredStatus)
  {
      string strSpeedoUrl = System.Configuration.ConfigurationManager.AppSettings["SpeedoURL"].ToString();
      int intReturn = 0;
      string strSubject = "";
      string strBody = "";
      string strStatus = "Tag for Manual Approval";

      SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString);
      cn.Open();
      //SqlTransaction trans = cn.BeginTransaction();
      SqlCommand cmd = cn.CreateCommand();
      //cmd.Transaction = trans;

      try
      {
          cmd.CommandText = "UPDATE Finance.RFPRequest SET authstat=@authstat,aaprdate=@aaprdate,reqdocum=@reqdocum,statcode=@statcode WHERE ctrlnmbr=@ctrlnmbr";
          cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
          cmd.Parameters.Add(new SqlParameter("@aaprdate", DateTime.Now));
          cmd.Parameters.Add(new SqlParameter("@authstat", pStatus));
          cmd.Parameters.Add(new SqlParameter("@reqdocum", "1"));
          cmd.Parameters.Add(new SqlParameter("@statcode", "M"));
          intReturn = cmd.ExecuteNonQuery();
          cmd.Parameters.Clear();

          if (intReturn > 0)
          {
              //send email to Authorize by
              strSubject = strStatus + " Request For Payment";
              strBody = "You requested documents to be submitted for manual approval of the RFP.<br><br>" +
                        "An email notification has been sent to " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + " to inform him/her regarding this action.<br><br>" +
                        "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to view the request</a><br><br>" +
                        "If you can't click on the above link,<br>" +
                        "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                        "All the best,<br>E-Forms Administrator";
              clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetAuthority(pControlNumber)), strSubject, strBody);

              if (!clsRFPRequest.IsHave2ndEndorser(pControlNumber))
              {
                  //send email to Endorser 1
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetEndorser1(pControlNumber)) + ",<br><br>" +
                          "The RFP you endorsed has been tagged for manual approval by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ". Please submit all the necessary documents for review and approval.<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetEndorser1(pControlNumber)), strSubject, strBody);
              }
              else
              {
                  //send email to Endorser 1
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetEndorser1(pControlNumber)) + ",<br><br>" +
                          "The RFP you endorsed has been tagged for manual approval by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ". Please submit all the necessary documents for review and approval.<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetEndorser1(pControlNumber)), strSubject, strBody);

                  //send email to Endorser 2
                  strSubject = strStatus + " Request For Payment";
                  strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetEndorser2(pControlNumber)) + ",<br><br>" +
                          "The RFP you endorsed has been tagged for manual approval by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ". Please submit all the necessary documents for review and approval.<br><br>" +
                          "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                          "If you can't click on the above link,<br>" +
                          "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                         "All the best,<br>E-Forms Administrator";
                  clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetEndorser2(pControlNumber)), strSubject, strBody);
              }

              //send email to requestor
              strSubject = strStatus + " Request For Payment";
              strBody = "Hi " + clsEmployee.GetName(clsRFPRequest.GetRequestor(pControlNumber)) + ",<br><br>" +
                      "Your RFP has been tagged for manual approval by " + clsEmployee.GetName(clsRFPRequest.GetAuthority(pControlNumber)) + ". Please submit all the necessary documents for review and approval.<br><br>" +
                      "<a href='" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "'>Click here to review the request</a><br><br>" +
                      "If you can't click on the above link,<br>" +
                      "you can also review the request by copying and pasting into your browser the following address:<br><i>" + strSpeedoUrl + "/FINANCE/RFP/RFPDetails.aspx?ControlNumber=" + pControlNumber + "</i><br><br>" +
                     "All the best,<br>E-Forms Administrator";
              clsSpeedo.SendMail(clsUsers.GetEmail(clsRFPRequest.GetRequestor(pControlNumber)), strSubject, strBody);
          }
      }

      catch (Exception ex)
      {
          System.Web.HttpContext.Current.Response.Write("Warning<br>Message: " + ex.Message + "<br>Source: " + ex.TargetSite);
          //trans.Rollback();
          cn.Close();
      }
      finally
      {
          cn.Close();
      }
      return intReturn;
  }

  public static bool IsCanStillApprove(string pApproverType, string pControlNumber)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    if (pApproverType == "endrsby1")
    {
     cmd.CommandText = "SELECT endrstt1 FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["endrstt1"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    if (pApproverType == "endrsby2")
    {
     cmd.CommandText = "SELECT endrstt2 FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["endrstt2"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    if (pApproverType == "authrzby")
    {
     cmd.CommandText = "SELECT authstat FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
     cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
     cn.Open();
     SqlDataReader dr = cmd.ExecuteReader();
     if (dr.Read())
     {
      if (dr["authstat"].ToString() == "2") { blnReturn = true; }
      else { blnReturn = false; }
     }
    }

    cn.Close();
   }
   return blnReturn;
  }

  public static bool AuthenticateAccess(string pUsername, string pControlNumber)
  {
   bool blnReturn = false;
   using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr AND (createby=@user OR endrsby1=@user OR endrsby2=@user OR authrzby=@user)";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cmd.Parameters.Add(new SqlParameter("@user", pUsername));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    blnReturn = dr.Read();
    dr.Close();
   }
   return blnReturn;
  }

  public static int GetTotalRecords()
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT COUNT(ctrlnmbr) FROM Finance.RFPRequest";
    cn.Open();
    try { intReturn = int.Parse(cmd.ExecuteScalar().ToString()); }
    catch { }
   }
   return intReturn;
  }

  public static int Cancel(string pControlNumber)
  {
   int intReturn = 0;
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "UPDATE Finance.RFPRequest SET statcode=@statcode WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@statcode", "3"));
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
    cn.Open();
    intReturn = cmd.ExecuteNonQuery();
    return intReturn;
   }
  }

  public static string GetLatestControlNumber(string pUsername)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT TOP 1(ctrlnmbr) FROM Finance.RFPRequest WHERE createby=@createby ORDER by createon DESC";
    cmd.Parameters.Add(new SqlParameter("@createby", pUsername));
    cn.Open();
    strReturn = cmd.ExecuteScalar().ToString();
   }
   return strReturn;
  }

  public static int RequiredDucument(string pControlNumber)
  {
      int intReturn = 0;
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "UPDATE Finance.RFPRequest SET reqdocum=@reqdocum WHERE ctrlnmbr=@ctrlnmbr";
          cmd.Parameters.Add(new SqlParameter("@reqdocum", "1"));
          cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
          cn.Open();
          intReturn = cmd.ExecuteNonQuery();
          return intReturn;
      }
  }

  public static string GetRFPStatus(string pControlNumber)
  {
      string strReturn = "";
      using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
      {
          SqlCommand cmd = cn.CreateCommand();
          cmd.CommandText = "SELECT statcode FROM Finance.RFPRequest WHERE ctrlnmbr=@ctrlnmbr";
          cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", pControlNumber));
          cn.Open();
          try { strReturn = cmd.ExecuteScalar().ToString(); }
          catch { }
      }
      return strReturn;
  }

 }

}
