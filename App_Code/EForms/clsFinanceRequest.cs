using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace STIeForms
{
 public class clsFinanceRequest : IDisposable
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

  public string CreatedBy { get { return _strCreatedBy; } set { _strCreatedBy = value; } }
  public DateTime CreatedOn { get { return _dteCreateOn; } set { _dteCreateOn = value; } }
  public string ModifyBy { get { return _strModifyBy; } set { _strModifyBy = value; } }
  public DateTime ModifyOn { get { return _dteModifyOn; } set { _dteModifyOn = value; } }

  public clsFinanceRequest()
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
    cmd.CommandText = "SELECT * FROM Finance.Request WHERE ctrlnmbr=@ctrlnmbr";
    cmd.Parameters.Add(new SqlParameter("@ctrlnmbr", _strControlNumber));
    cn.Open();
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
     _strRequestCode = dr["rqstcode"].ToString();
     _strRequestFor = dr["rqstfor"].ToString();
     _strAPVNumber = dr["apvnmbr"].ToString();
     _strProjectTitle = dr["progttle"].ToString();
     _dteDateNeeeded = Convert.ToDateTime(dr["dateneed"].ToString());
     _strRFANumber = dr["rfanmbr"].ToString();
     _strPayeeName = dr["payename"].ToString();
     _strSupportingDocument = dr["sptrdocu"].ToString();
     _strRemarks = dr["remarks"].ToString();
     _strEndorsedBy1 = dr["endorsby1"].ToString();
     _strEndorsedStatus1 = dr["endrstt1"].ToString();
     _dteEndorsedDate1 = Convert.ToDateTime(dr["e1aprvd"].ToString());
     _strEndorsedBy2 = dr["endorsby2"].ToString();
     _strEndorsedStatus2 = dr["endrstt2"].ToString();
     _dteEndorsedDate2 = Convert.ToDateTime(dr["e2aprvd"].ToString());
     _strCreatedBy = dr["createby"].ToString();
     _dteCreateOn = Convert.ToDateTime(dr["createon"].ToString());
     _strModifyBy = dr["modifyby"].ToString();
     _dteModifyOn = Convert.ToDateTime(dr["modifyon"].ToString());
    }
   }
  }

  public int Insert(DataTable pItems)
  {
   int intReturn = 0;
   int intSeed = 0;
   int intMonth = 0;
   int intYear = 0;
   string strMonth = "";

   SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF);
   cn.Open();
   SqlTransaction tran = cn.BeginTransaction();
   SqlCommand cmd = cn.CreateCommand();
   cmd.Transaction = tran;

   try
   {
    cmd.CommandText = "SELECT xvalue FROM Finance.PrimaryKey Where xkey='CurrentYear'";
    intYear = Convert.ToInt32(cmd.ExecuteScalar().ToString());

    cmd.CommandText = "SELECT xvalue FROM Finance.PrimaryKey Where xkey='CurrentMonth'";
    intMonth = Convert.ToInt32(cmd.ExecuteScalar().ToString());

    if (Convert.ToInt32(DateTime.Now.Month.ToString()) == intMonth)
    {
     strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
    }
    else
    {
     intMonth = intMonth + 1;
     cmd.CommandText = "UPDATE Finance.PrimaryKey SET xvalue=@xvalue Where xkey='CurrentMonth'";
     cmd.Parameters.Add(new SqlParameter("@xvalue", Convert.ToInt32(DateTime.Now.Month.ToString())));
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();

     strMonth = ("00" + intMonth.ToString()).Substring(intMonth.ToString().Length);
     cmd.CommandText = "UPDATE Finance.PrimaryKey SET xvalue=0 Where xkey='ControlNumber'";
     intReturn = cmd.ExecuteNonQuery();
     cmd.Parameters.Clear();
    }

    cmd.CommandText = "SELECT xvalue FROM Finance.PrimaryKey Where xkey='ControlNumber'";
    intSeed = (Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1);

    cmd.CommandText = "UPDATE Finance.PrimaryKey SET xvalue=@xvalue Where xkey='ControlNumber'";
    cmd.Parameters.Add(new SqlParameter("@xvalue", intSeed));
    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    _strControlNumber = (intYear + "-" + strMonth + "-" + ("000" + intSeed.ToString()).Substring(intSeed.ToString().Length));

    cmd.CommandText = "INSERT INTO Finance.Request VALUES(@ctrlnmbr, @rqstcode, @rqstfor,@projttle, @apvnmbr, @dateneed, @rfanmbr, @payename, @sprtdocu, @remarks, @endrsby1,@endrstt1,@e1aprvd,@endrsby2,@endrstt2,@e2aprvd, @createby, @createon,@modifyby,@modifyon)";
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

    cmd.Parameters.Add(new SqlParameter("@createby", _strCreatedBy));
    cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now));

    intReturn = cmd.ExecuteNonQuery();
    cmd.Parameters.Clear();

    //Add RequestDetails

    foreach (DataRow drw in pItems.Rows)
    {
     cmd.CommandText = "SELECT ritmcode FROM Finance.RequestDetails ORDER BY ritmcode DESC";
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

     cmd.CommandText = "INSERT INTO Finance.RequestDetails VALUES (@ritmcode, @cntrlnmbr, @itemdesc,@schlcode, @rccode, @others, @amount)";
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

   return intReturn;
  }

  public string AddedControlNumber(string pLoggedUser)
  {
   string strReturn = "";
   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT ctrlnmbr FROM Finance.Request WHERE createby = @pLoggedUser ORDER BY createon DESC";
    cmd.Parameters.Add(new SqlParameter("@createby", pLoggedUser));
    // cmd.Parameters.Add(new SqlParameter("@createon", DateTime.Now.ToShortDateString()));

    strReturn = _strControlNumber;
   }

   return strReturn;
  }

  //////////////////////////////////
  ///////// Static Members /////////
  //////////////////////////////////
  public static DataTable GetDSGMainForm(string pSelect, string pWhere, int intStart, int intEnd)
  {
   DataTable tblReturn = new DataTable();
   tblReturn.Columns.Add("ControlNumber");
   tblReturn.Columns.Add("RequestCode");
   tblReturn.Columns.Add("RequestFor");
   tblReturn.Columns.Add("ProjectTitle");
   tblReturn.Columns.Add("DateNeeded");
   tblReturn.Columns.Add("PayeeName");
   tblReturn.Columns.Add("CreatedBy");
   tblReturn.Columns.Add("CreatedOn");

   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.Request) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
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
   tblReturn.Columns.Add("PayeeName");
   tblReturn.Columns.Add("CreatedBy");
   tblReturn.Columns.Add("CreatedOn");

   using (SqlConnection cn = new SqlConnection(clsSystemConfigurations.ConnectionStringMRCF))
   {
    SqlCommand cmd = cn.CreateCommand();
    cmd.CommandText = "SELECT * FROM ( SELECT " + pSelect + " , ROW_NUMBER() OVER(ORDER BY createon DESC) AS RowNum FROM Finance.Request WHERE createby = @UserName) as FinanceRequest WHERE RowNum BETWEEN " + intStart + " AND " + intEnd + " ORDER BY createon DESC";
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
     drwNew["CreatedBy"] = dr["createby"].ToString();
     drwNew["CreatedOn"] = Convert.ToDateTime(dr["createon"].ToString());
     tblReturn.Rows.Add(drwNew);
    }
    dr.Close();
   }
   return tblReturn;
  }



 }
}

