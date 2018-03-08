using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace HRMS
{
   public enum EmployeeNameFormat { FirstLast, LastFirst }
   public enum EmployeeWhereParameter { Username, EmployeeNumber, Lastname }

   public class clsEmployee : IDisposable
   {
      private string _strUsername;
      private string _strEmployeeNumber;
      private string _strLastName;
      private string _strFirstName;
      private string _strMiddleName;
      private string _strMiddleInitial;
      private string _strNickName;
      private DateTime _dteBirthDate;
      private string _strGender;
      private string _strEmploymentTypeCode;
      private string _strEmploymentStatusCode;
      private string _strTitle;
      private string _strSuffix;
      private string _strCompanyCode;
      private string _strPosition;
      private string _strRemarks;
      private string _strEmergencyPerson;
      private string _strEmergencyRelation;
      private string _strEmergencyAddress;
      private string _strEmergencyPhone;
      private string _strEmergencyCell;
      private string _strBirthPlace;
      private string _strCitizenship;
      private string _strHeight;
      private string _strWeight;
      private string _strBloodType;
      private string _strHobbies;
      private string _strLanguage;
      private string _strCivilStatus;
      private string _strFatherName;
      private DateTime _dteFatherBirthDate;
      private string _strMotherName;
      private DateTime _dteMotherBirthDate;
      private string _strSpouseName;
      private DateTime _dteSpouseBirthDate;
      private string _strPermanentAddress;
      private string _strPermanentCity;
      private string _strPermanentPhoneNumber;
      private string _strCurrentAddress;
      private string _strCurrentCity;
      private string _strCurrentPhoneNumber;
      private string _strPrimaryMobileNumber;
      private string _strAlternativeMobileNumber;
      private string _strDirectNumber;
      private string _strLocalNumber;
      private string _strFaxNumber;
      private string _strEmailOfficial;
      private string _strEmailPersonal;
      private string _strScheduleCode;
      private string _strSssID;
      private string _strPhilHealthID;
      private string _strTaxID;
      private string _strHdmfID;
      private string _strHmoID;
      private string _strBankAccount;
      private string _strDivisionCode;
      private string _strGroupCode;
      private string _strDepartmentCode;
      private string _strRcCode;
      private string _strJGCode;
      private string _strAssignment;
      private DateTime _dteDateStart;
      private DateTime _dteDateRegular;
      private DateTime _dteDateEnd;
      private string _strSkillPrimary;
      private string _strSkillSecondary;
      private string _strUpdatedBy;
      private DateTime _dteUpdatedOn;
      private string _strStatus;

      public clsEmployee() { }
      public clsEmployee(string pUsername) { _strUsername = pUsername; }

      public string Username { get { return _strUsername; } set { _strUsername = value; } }
      public string EmployeeNumber { get { return _strEmployeeNumber; } set { _strEmployeeNumber = value; } }
      public string LastName { get { return _strLastName; } set { _strLastName = value; } }
      public string FirstName { get { return _strFirstName; } set { _strFirstName = value; } }
      public string MiddleName { get { return _strMiddleName; } set { _strMiddleName = value; } }
      public string MiddleInitial { get { return _strMiddleInitial; } set { _strMiddleInitial = value; } }
      public string NickName { get { return _strNickName; } set { _strNickName = value; } }
      public DateTime BirthDate { get { return _dteBirthDate; } set { _dteBirthDate = value; } }
      public string Gender { get { return _strGender; } set { _strGender = value; } }
      public string EmploymentTypeCode { get { return _strEmploymentTypeCode; } set { _strEmploymentTypeCode = value; } }
      public string EmploymentStatusCode { get { return _strEmploymentStatusCode; } set { _strEmploymentStatusCode = value; } }
      public string Title { get { return _strTitle; } set { _strTitle = value; } }
      public string Suffix { get { return _strSuffix; } set { _strSuffix = value; } }
      public string CompanyCode { get { return _strCompanyCode; } set { _strCompanyCode = value; } }
      public string Position { get { return _strPosition; } set { _strPosition = value; } }
      public string Remarks { get { return _strRemarks; } set { _strRemarks = value; } }
      public string EmergencyPerson { get { return _strEmergencyPerson; } set { _strEmergencyPerson = value; } }
      public string EmergencyRelation { get { return _strEmergencyRelation; } set { _strEmergencyRelation = value; } }
      public string EmergencyAddress { get { return _strEmergencyAddress; } set { _strEmergencyAddress = value; } }
      public string EmergencyPhone { get { return _strEmergencyPhone; } set { _strEmergencyPhone = value; } }
      public string EmergencyCell { get { return _strEmergencyCell; } set { _strEmergencyCell = value; } }
      public string BirthPlace { get { return _strBirthPlace; } set { _strBirthPlace = value; } }
      public string Citizenship { get { return _strCitizenship; } set { _strCitizenship = value; } }
      public string Height { get { return _strHeight; } set { _strHeight = value; } }
      public string Weight { get { return _strWeight; } set { _strWeight = value; } }
      public string BloodType { get { return _strBloodType; } set { _strBloodType = value; } }
      public string Hobbies { get { return _strHobbies; } set { _strHobbies = value; } }
      public string Language { get { return _strLanguage; } set { _strLanguage = value; } }
      public string CivilStatus { get { return _strCivilStatus; } set { _strCivilStatus = value; } }
      public string FatherName { get { return _strFatherName; } set { _strFatherName = value; } }
      public DateTime FatherBirthDate { get { return _dteFatherBirthDate; } set { _dteFatherBirthDate = value; } }
      public string MotherName { get { return _strMotherName; } set { _strMotherName = value; } }
      public DateTime MotherBirthDate { get { return _dteMotherBirthDate; } set { _dteMotherBirthDate = value; } }
      public string SpouseName { get { return _strSpouseName; } set { _strSpouseName = value; } }
      public DateTime SpouseBirthDate { get { return _dteSpouseBirthDate; } set { _dteSpouseBirthDate = value; } }
      public string PermanentAddress { get { return _strPermanentAddress; } set { _strPermanentAddress = value; } }
      public string PermanentCity { get { return _strPermanentCity; } set { _strPermanentCity = value; } }
      public string PermanentPhoneNumber { get { return _strPermanentPhoneNumber; } set { _strPermanentPhoneNumber = value; } }
      public string CurrentAddress { get { return _strCurrentAddress; } set { _strCurrentAddress = value; } }
      public string CurrentCity { get { return _strCurrentCity; } set { _strCurrentCity = value; } }
      public string CurrentPhoneNumber { get { return _strCurrentPhoneNumber; } set { _strCurrentPhoneNumber = value; } }
      public string PrimaryMobileNumber { get { return _strPrimaryMobileNumber; } set { _strPrimaryMobileNumber = value; } }
      public string AlternativeMobileNumber { get { return _strAlternativeMobileNumber; } set { _strAlternativeMobileNumber = value; } }
      public string DirectNumber { get { return _strDirectNumber; } set { _strDirectNumber = value; } }
      public string LocalNumber { get { return _strLocalNumber; } set { _strLocalNumber = value; } }
      public string FaxNumber { get { return _strFaxNumber; } set { _strFaxNumber = value; } }
      public string EmailOfficial { get { return _strEmailOfficial; } set { _strEmailOfficial = value; } }
      public string EmailPersonal { get { return _strEmailPersonal; } set { _strEmailPersonal = value; } }
      public string ScheduleCode { get { return _strScheduleCode; } set { _strScheduleCode = value; } }
      public string SssID { get { return _strSssID; } set { _strSssID = value; } }
      public string PhilHealthID { get { return _strPhilHealthID; } set { _strPhilHealthID = value; } }
      public string TaxID { get { return _strTaxID; } set { _strTaxID = value; } }
      public string HdmfID { get { return _strHdmfID; } set { _strHdmfID = value; } }
      public string HmoID { get { return _strHmoID; } set { _strHmoID = value; } }
      public string BankAccount { get { return _strBankAccount; } set { _strBankAccount = value; } }
      public string DivisionCode { get { return _strDivisionCode; } set { _strDivisionCode = value; } }
      public string GroupCode { get { return _strGroupCode; } set { _strGroupCode = value; } }
      public string DepartmentCode { get { return _strDepartmentCode; } set { _strDepartmentCode = value; } }
      public string RcCode { get { return _strRcCode; } set { _strRcCode = value; } }
      public string JGCode { get { return _strJGCode; } set { _strJGCode = value; } }
      public string Assignment { get { return _strAssignment; } set { _strAssignment = value; } }
      public DateTime DateStart { get { return _dteDateStart; } set { _dteDateStart = value; } }
      public DateTime DateRegular { get { return _dteDateRegular; } set { _dteDateRegular = value; } }
      public DateTime DateEnd { get { return _dteDateEnd; } set { _dteDateEnd = value; } }
      public string SkillPrimary { get { return _strSkillPrimary; } set { _strSkillPrimary = value; } }
      public string SkillSecondary { get { return _strSkillSecondary; } set { _strSkillSecondary = value; } }
      public string UpdatedBy { get { return _strUpdatedBy; } set { _strUpdatedBy = value; } }
      public DateTime UpdatedOn { get { return _dteUpdatedOn; } set { _dteUpdatedOn = value; } }
      public string Status { get { return _strStatus; } set { _strStatus = value; } }

      public void Fill()
      {
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM HR.Employees WHERE username='" + _strUsername + "'";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               _strEmployeeNumber = dr["empnum"].ToString();
               _strLastName = dr["lastname"].ToString();
               _strFirstName = dr["firname"].ToString();
               _strMiddleName = dr["midname"].ToString();
               _strMiddleInitial = dr["midintl"].ToString();
               _strNickName = dr["nickname"].ToString();
               _dteBirthDate = clsValidator.CheckDate(dr["brthdate"].ToString());
               _strGender = dr["gender"].ToString();
               _strEmploymentTypeCode = dr["etypcode"].ToString();
               _strEmploymentStatusCode = dr["esttcode"].ToString();
               _strTitle = dr["title"].ToString();
               _strSuffix = dr["suffix"].ToString();
               _strCompanyCode = dr["comcode"].ToString();
               _strPosition = dr["position"].ToString();
               _strRemarks = dr["remarks"].ToString();
               _strEmergencyPerson = dr["emername"].ToString();
               _strEmergencyRelation = dr["emerrltn"].ToString();
               _strEmergencyAddress = dr["emeraddr"].ToString();
               _strEmergencyPhone = dr["emerphon"].ToString();
               _strEmergencyCell = dr["emercell"].ToString();
               _strBirthPlace = dr["brthplac"].ToString();
               _strCitizenship = dr["citizen"].ToString();
               _strHeight = dr["height"].ToString();
               _strWeight = dr["weigth"].ToString();
               _strBloodType = dr["bloodtyp"].ToString();
               _strHobbies = dr["hobbies"].ToString();
               _strLanguage = dr["langspok"].ToString();
               _strCivilStatus = dr["cstatus"].ToString();
               _strFatherName = dr["fthrname"].ToString();
               _dteFatherBirthDate = clsValidator.CheckDate(dr["fthrdbrt"].ToString());
               _strMotherName = dr["mthrname"].ToString();
               _dteMotherBirthDate = clsValidator.CheckDate(dr["mthrdbrt"].ToString());
               _strSpouseName = dr["spsename"].ToString();
               _dteSpouseBirthDate = clsValidator.CheckDate(dr["spsedbrt"].ToString());
               _strPermanentAddress = dr["permaddr"].ToString();
               _strPermanentCity = dr["permcity"].ToString();
               _strPermanentPhoneNumber = dr["permphon"].ToString();
               _strCurrentAddress = dr["curraddr"].ToString();
               _strCurrentCity = dr["currcity"].ToString();
               _strCurrentPhoneNumber = dr["currphon"].ToString();
               _strPrimaryMobileNumber = dr["primmobl"].ToString();
               _strAlternativeMobileNumber = dr["altrmobl"].ToString();
               _strDirectNumber = dr["drctnmbr"].ToString();
               _strLocalNumber = dr["lcalnmbr"].ToString();
               _strFaxNumber = dr["faxnmbr"].ToString();
               _strEmailOfficial = dr["emailofc"].ToString();
               _strEmailPersonal = dr["emailper"].ToString();
               _strScheduleCode = dr["schdcode"].ToString();
               _strSssID = dr["sssid"].ToString();
               _strPhilHealthID = dr["philid"].ToString();
               _strTaxID = dr["taxid"].ToString();
               _strHdmfID = dr["hdmfid"].ToString();
               _strHmoID = dr["hmoid"].ToString();
               _strBankAccount = dr["bankacct"].ToString();
               _strDivisionCode = dr["divicode"].ToString();
               _strGroupCode = dr["grpcode"].ToString();
               _strDepartmentCode = dr["deptcode"].ToString();
               _strRcCode = dr["rccode"].ToString();
               _strJGCode = dr["jgcode"].ToString();
               _strAssignment = dr["assgnmnt"].ToString();
               _dteDateStart = clsValidator.CheckDate(dr["datestrt"].ToString());
               _dteDateRegular = clsValidator.CheckDate(dr["datereg"].ToString());
               _dteDateEnd = clsValidator.CheckDate(dr["dateend"].ToString());
               _strSkillPrimary = dr["skillpri"].ToString();
               _strSkillSecondary = dr["skillsec"].ToString();
               _strUpdatedBy = dr["updateby"].ToString();
               _dteUpdatedOn = clsValidator.CheckDate(dr["updateon"].ToString());
               _strStatus = dr["pstatus"].ToString();
            }
            dr.Close();
         }
      }

      public int EditCV()
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "UPDATE HR.Employees SET nickname=@nickname, title=@title, suffix=@suffix, emername=@emername, emerrltn=@emerrltn, emeraddr=@emeraddr, emerphon=@emerphon, emercell=@emercell, brthplac=@brthplac, citizen=@citizen, height=@height, weigth=@weigth, bloodtyp=@bloodtyp, hobbies=@hobbies, langspok=@langspok, fthrname=@fthrname, fthrdbrt=@fthrdbrt, mthrname=@mthrname, mthrdbrt=@mthrdbrt, spsename=@spsename, spsedbrt=@spsedbrt, permaddr=@permaddr, permcity=@permcity, permphon=@permphon, curraddr=@curraddr, currcity=@currcity, currphon=@currphon, primmobl=@primmobl, altrmobl=@altrmobl, drctnmbr=@drctnmbr, lcalnmbr=@lcalnmbr, faxnmbr=@faxnmbr, emailper=@emailper, skillpri=@skillpri, skillsec=@skillsec, updateby=@updateby, updateon=@updateon WHERE username=@username";
            cmd.Parameters.Add(new SqlParameter("@username", _strUsername));
            cmd.Parameters.Add(new SqlParameter("@nickname", _strNickName));
            cmd.Parameters.Add(new SqlParameter("@title", _strTitle));
            cmd.Parameters.Add(new SqlParameter("@suffix", _strSuffix));
            cmd.Parameters.Add(new SqlParameter("@emername", _strEmergencyPerson));
            cmd.Parameters.Add(new SqlParameter("@emerrltn", _strEmergencyRelation));
            cmd.Parameters.Add(new SqlParameter("@emeraddr", _strEmergencyAddress));
            cmd.Parameters.Add(new SqlParameter("@emerphon", _strEmergencyPhone));
            cmd.Parameters.Add(new SqlParameter("@emercell", _strEmergencyCell));
            cmd.Parameters.Add(new SqlParameter("@brthplac", _strBirthPlace));
            cmd.Parameters.Add(new SqlParameter("@citizen", _strCitizenship));
            cmd.Parameters.Add(new SqlParameter("@height", _strHeight));
            cmd.Parameters.Add(new SqlParameter("@weigth", _strWeight));
            cmd.Parameters.Add(new SqlParameter("@bloodtyp", _strBloodType));
            cmd.Parameters.Add(new SqlParameter("@hobbies", _strHobbies));
            cmd.Parameters.Add(new SqlParameter("@langspok", _strLanguage));
            cmd.Parameters.Add(new SqlParameter("@fthrname", _strFatherName));
            cmd.Parameters.Add(new SqlParameter("@fthrdbrt", _dteFatherBirthDate));
            cmd.Parameters.Add(new SqlParameter("@mthrname", _strMotherName));
            cmd.Parameters.Add(new SqlParameter("@mthrdbrt", _dteMotherBirthDate));
            cmd.Parameters.Add(new SqlParameter("@spsename", _strSpouseName));
            cmd.Parameters.Add(new SqlParameter("@spsedbrt", _dteSpouseBirthDate));
            cmd.Parameters.Add(new SqlParameter("@permaddr", _strPermanentAddress));
            cmd.Parameters.Add(new SqlParameter("@permcity", _strPermanentCity));
            cmd.Parameters.Add(new SqlParameter("@permphon", _strPermanentPhoneNumber));
            cmd.Parameters.Add(new SqlParameter("@curraddr", _strCurrentAddress));
            cmd.Parameters.Add(new SqlParameter("@currcity", _strCurrentCity));
            cmd.Parameters.Add(new SqlParameter("@currphon", _strCurrentPhoneNumber));
            cmd.Parameters.Add(new SqlParameter("@primmobl", _strPrimaryMobileNumber));
            cmd.Parameters.Add(new SqlParameter("@altrmobl", _strAlternativeMobileNumber));
            cmd.Parameters.Add(new SqlParameter("@drctnmbr", _strDirectNumber));
            cmd.Parameters.Add(new SqlParameter("@lcalnmbr", _strLocalNumber));
            cmd.Parameters.Add(new SqlParameter("@faxnmbr", _strFaxNumber));
            cmd.Parameters.Add(new SqlParameter("@emailper", _strEmailPersonal));
            cmd.Parameters.Add(new SqlParameter("@skillpri", _strSkillPrimary));
            cmd.Parameters.Add(new SqlParameter("@skillsec", _strSkillSecondary));
            cmd.Parameters.Add(new SqlParameter("@updateby", _strUpdatedBy));
            cmd.Parameters.Add(new SqlParameter("@updateon", _dteUpdatedOn));
            cn.Open();
            intReturn = cmd.ExecuteNonQuery();
         }
         return intReturn;
      }

      public void Dispose() { GC.SuppressFinalize(this); }

      //////////////////////////////////
      ///////// Static Members /////////
      //////////////////////////////////

      public static int CountEmployeeManpowerCompliment(string pGender, string pStatus)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM HR.Employees WHERE gender='" + pGender + "' AND pstatus='" + pStatus + "' AND esttcode IN (SELECT esttcode FROM HR.EmploymentStatus WHERE mnpwrcom='1')";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountEmployeeManpowerComplimentAge(int pAgeFrom, int pAgeTo)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(username) AS TotalEmployees FROM HR.Employees WHERE DATEDIFF(YEAR,CONVERT(DATETIME, brthdate),GETDATE()) BETWEEN " + pAgeFrom + " AND " + pAgeTo + " AND pstatus='1' AND esttcode IN (SELECT esttcode FROM HR.EmploymentStatus WHERE mnpwrcom='1')";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static int CountEmployeeManpowerComplimentTenure(int pTenureFrom, int pTenureTo)
      {
         int intReturn = 0;
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(username) AS TotalEmployees FROM HR.Employees WHERE DATEDIFF(YEAR,CONVERT(DATETIME, datestrt),GETDATE()) BETWEEN " + pTenureFrom + " AND " + pTenureTo + " AND pstatus='1' AND esttcode IN (SELECT esttcode FROM HR.EmploymentStatus WHERE mnpwrcom='1')";
            cn.Open();
            try { intReturn = clsValidator.CheckInteger(cmd.ExecuteScalar().ToString()); }
            catch { }
         }
         return intReturn;
      }

      public static string GetScheduleCurrent(string pUsername, DateTime pFocusDate)
      {
         string strReturn = clsEmployeeSchedule.GetScheduleCode(pUsername, pFocusDate);
         if (strReturn == "")
            strReturn = clsEmployee.GetScheduleDefault(pUsername);
         return strReturn;
      }

      public static string GetScheduleDefault(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT schdcode FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static DataTable DSDTREmployee()
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "SELECT username,lastname,firname,nickname,division,datestrt,dateend FROM HR.Employees INNER JOIN HR.Division ON HR.Employees.divicode = HR.Division.divicode WHERE HR.Employees.pstatus='1' AND HR.Employees.username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='002') ORDER BY division,lastname";
            cmd.CommandText = "SELECT username,lastname,firname,nickname,divicode,datestrt,dateend FROM Hr.Employees WHERE pstatus='1' AND username IN (SELECT username FROM Hr.EmployeeCluster WHERE cluscode='002') ORDER BY divicode,lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static DataTable DSDTREmployee(string pDivisionCode)
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username,lastname,firname,nickname,deptname,datestrt,dateend FROM HR.Employees INNER JOIN HR.Department ON HR.Employees.deptcode = HR.Department.deptcode WHERE Hr.Employees.pstatus='1' AND  HR.Employees.divicode='" + pDivisionCode + "' AND HR.Employees.username IN (SELECT username FROM HR.EmployeeCluster WHERE cluscode='001') ORDER BY deptname,lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static DataTable DSLUsername()
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, username AS ptext FROM HR.Employees WHERE pstatus='1' ORDER BY username";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static DataTable DSLEmployeeList()
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username AS pvalue, lastname + ' ' + firname AS ptext FROM HR.Employees WHERE pstatus='1' ORDER BY lastname";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static DataTable DSLEmployeeList(string pDepartmentCode)
      {
          DataTable tblReturn = new DataTable();
          using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
          {
              SqlCommand cmd = cn.CreateCommand();
              cmd.CommandText = "SELECT username AS pvalue, lastname + ' ' + firname AS ptext FROM HR.Employees WHERE pstatus='1' AND deptcode=@deptcode ORDER BY lastname";
              cmd.Parameters.Add(new SqlParameter("@deptcode", pDepartmentCode));
              SqlDataAdapter da = new SqlDataAdapter(cmd);
              da.Fill(tblReturn);
          }
          return tblReturn;
      }

      public static DataTable DSLEmployeeListManagerVPSupervisor(string pUsername)
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  username AS pvalue, firname + ' ' + lastname AS ptext FROM   HR.Employees WHERE   pstatus = '1' AND jgcode IN ('JG4','JG5','MA', 'MB', 'MC', 'VP', 'P', 'EVP', 'AVP') ORDER BY ptext";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
            DataRow drw = tblReturn.NewRow();
            drw[0] = "jun.sagcal";
            drw[1] = "Engracio Sagcal Jr.";
            tblReturn.Rows.Add(drw);

            //Sort items in datatable
            DataView dv = tblReturn.DefaultView;
            dv.Sort = "ptext ASC";
            tblReturn = dv.ToTable();

            // DataRow[] drResult = tblReturn.Select("pvalue='" + pUsername + "'");
            // if (drResult.Length > 0)
            // {
            //  drResult[0].Delete();
            //  tblReturn.AcceptChanges();
            // }
         }
         return tblReturn;
      }

      public static DataTable DSLExecutive()
      {
         DataTable tblReturn = new DataTable();
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT  username AS pvalue, firname + ' ' + lastname AS ptext FROM HR.Employees WHERE   pstatus = '1' AND jgcode IN ('VP', 'P', 'EVP', 'AVP') ORDER BY ptext";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tblReturn);
         }
         return tblReturn;
      }

      public static string GetScheduleCode(string pUsername, DateTime pFocusDate)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT schdcode FROM ";
         }
         return strReturn;
      }

      public static string GetDivisionCode(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT divicode FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetRCCode(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rccode FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetDepartmentCode(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT deptcode FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetRCName(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rcname FROM HR.Rc WHERE rccode IN (SELECT rccode FROM HR.Employees WHERE username='" + pUsername + "')";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetSchedule(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT schdcode FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetEmployeeNumber(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT empnum FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetUsernameByNumber(string pEmployeeNumber)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT username FROM HR.Employees WHERE empnum='" + pEmployeeNumber + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetName(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT firname + ' ' + midintl + '. ' + lastname FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetName(string pUsername, EmployeeNameFormat pNameFormat)
      {
         string strReturn = "";
         string strNameFormat = "";

         if (pNameFormat == EmployeeNameFormat.FirstLast)
            strNameFormat = "firname + ' ' + midintl + '. ' + lastname";
         else if (pNameFormat == EmployeeNameFormat.LastFirst)
            strNameFormat = "lastname + ', ' + firname + ' ' + midintl + '.'";

         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT " + strNameFormat + " FROM HR.Employees WHERE username='" + pUsername + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetName(string pKey, EmployeeWhereParameter pWhere)
      {
         string strReturn = "";
         string strWhere = "";

         if (pWhere == EmployeeWhereParameter.Username)
            strWhere = "username";
         else if (pWhere == EmployeeWhereParameter.EmployeeNumber)
            strWhere = "empnum";

         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT firname + ' ' + midintl + '. ' + lastname FROM HR.Employees WHERE " + strWhere + "='" + pKey + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetName(string pKey, EmployeeNameFormat pNameFormat, EmployeeWhereParameter pWhere)
      {
         string strReturn = "";
         string strNameFormat = "";
         string strWhere = "";

         if (pNameFormat == EmployeeNameFormat.FirstLast)
            strNameFormat = "firname + ' ' + midintl + '. ' + lastname";
         else if (pNameFormat == EmployeeNameFormat.LastFirst)
            strNameFormat = "lastname + ', ' + firname + ' ' + midintl + '.'";

         if (pWhere == EmployeeWhereParameter.Username)
            strNameFormat = "username";
         else if (pWhere == EmployeeWhereParameter.EmployeeNumber)
            strNameFormat = "empnum";

         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT " + strNameFormat + " FROM HR.Employees WHERE " + strWhere + "='" + pKey + "'";
            cn.Open();
            try { strReturn = cmd.ExecuteScalar().ToString(); }
            catch { }
         }
         return strReturn;
      }

      public static string GetPosition(string pUsername)
      {
         string strReturn;
         using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT position FROM HR.Employees WHERE username=@username";
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
         }
         return strReturn;
      }

      //added by charlie
      public static string GetJobGrade(string pUsername)
      {
         string strReturn = "";
         using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT jgcode FROM HR.Employees WHERE username=@username";
            cmd.Parameters.Add(new SqlParameter("@username", pUsername));
            cn.Open();
            strReturn = cmd.ExecuteScalar().ToString();
         }
         return strReturn;
      }
      //added by charlie

      public static int ComputeAge(DateTime pBirthDate)
      {
         return Convert.ToInt32((DateTime.Now - pBirthDate).Days / 365.5);
      }

      public static int ComputeTenure(DateTime pHiredDate)
      {
         return Convert.ToInt32((DateTime.Now - pHiredDate).Days / 365.5);
      }

      public static float ComputeTenureDecimal(DateTime pHiredDate, DateTime pResignedDate)
      {
          return (float)(pResignedDate - pHiredDate).Days / (float)365.5;
      }



      //////////////////////////////////
      ///////// Report Members /////////
      //////////////////////////////////

      public static DataTable DSRResignedEmployees(DateTime pDateFrom, DateTime pDateTo)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Position");
         tblReturn.Columns.Add("BirthDate");
         tblReturn.Columns.Add("Age");
         tblReturn.Columns.Add("DateHired");
         tblReturn.Columns.Add("DateEnd");
         tblReturn.Columns.Add("EmploymentStatus");
         tblReturn.Columns.Add("Tenure");
         tblReturn.Columns.Add("ResignationReason");
         tblReturn.Columns.Add("ResignationRemarks");
         tblReturn.Columns.Add("ResignationDesired");
         tblReturn.Columns.Add("Billable");

         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT firname,lastname,midintl,position,brthdate,datestrt,dateend,esttcode,rsgncode,rsgnrmks,rsgndsrd,billable FROM HR.Employees WHERE pstatus='0' AND dateend BETWEEN '" + pDateFrom + "' AND '" + pDateTo + "' ORDER BY dateend DESC";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               DataRow drwN = tblReturn.NewRow();
               drwN["EmployeeName"] = dr["lastname"].ToString() + ", " + dr["firname"].ToString() + " " + dr["midintl"].ToString() + ".";
               drwN["Position"] = dr["position"].ToString();
               drwN["BirthDate"] = clsValidator.CheckDate(dr["brthdate"].ToString()).ToString("MM/dd/yyyy");
               drwN["Age"] = clsEmployee.ComputeAge(clsValidator.CheckDate(dr["brthdate"].ToString()));
               drwN["DateHired"] = clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("MM/dd/yyyy");
               drwN["DateEnd"] = clsValidator.CheckDate(dr["dateend"].ToString()).ToString("MM/dd/yyyy");
               drwN["EmploymentStatus"] = clsEmploymentStatus.GetEmploymentStatusName(dr["esttcode"].ToString());
               drwN["Tenure"] = clsEmployee.ComputeTenureDecimal(clsValidator.CheckDate(dr["datestrt"].ToString()), clsValidator.CheckDate(dr["dateend"].ToString()));
               drwN["ResignationReason"] = clsResignationReason.GetResignationReasonLabel(dr["rsgncode"].ToString());
               drwN["ResignationRemarks"] = dr["rsgnrmks"].ToString();
               drwN["ResignationDesired"] = (dr["rsgndsrd"].ToString() == "1" ? "Desired" : "Undesired");
               drwN["Billable"] = (dr["billable"].ToString() == "1" ? "Yes" : "No");
               tblReturn.Rows.Add(drwN);
            }
            dr.Close();
         }
         return tblReturn;
      }

      public static DataTable DSRProjectBasedConsultantContractual(DateTime pDateAsOf)
      {
         DataTable tblReturn = new DataTable();
         tblReturn.Columns.Add("RC");
         tblReturn.Columns.Add("EmployeeName");
         tblReturn.Columns.Add("Position");
         tblReturn.Columns.Add("JobGrade");
         tblReturn.Columns.Add("DateStart");
         tblReturn.Columns.Add("DateEnd");
         tblReturn.Columns.Add("Tenure");
         tblReturn.Columns.Add("BirthDate");
         tblReturn.Columns.Add("Age");
         tblReturn.Columns.Add("Gender");
         tblReturn.Columns.Add("EmploymentStatus");
         tblReturn.Columns.Add("Division");
         tblReturn.Columns.Add("Department");
         tblReturn.Columns.Add("Remarks");

         using (SqlConnection cn = new SqlConnection(clsHrms.HrmsConnectionString))
         {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT rccode,firname,lastname,midintl,position,jgcode,datestrt,dateend,brthdate,gender,esttcode,divicode,deptcode,remarks FROM HR.Employees WHERE esttcode IN ('CN','CO','PB') AND ((pstatus='1' AND datestrt <= '" + pDateAsOf + "') OR (pstatus='0' AND dateend >= '" + pDateAsOf + "')) ORDER BY lastname";
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               DataRow drwN = tblReturn.NewRow();
               drwN["RC"] = dr["rccode"].ToString();
               drwN["EmployeeName"] = dr["lastname"].ToString() + ", " + dr["firname"].ToString() + " " + dr["midintl"].ToString() + ".";
               drwN["Position"] = dr["position"].ToString();
               drwN["JobGrade"] = dr["jgcode"].ToString();
               drwN["DateStart"] = clsValidator.CheckDate(dr["datestrt"].ToString()).ToString("MM/dd/yyyy");
               drwN["DateEnd"] = (clsValidator.CheckDate(dr["dateend"].ToString()) == clsDateTime.SystemMinDate ? "-" : clsValidator.CheckDate(dr["dateend"].ToString()).ToString("MM/dd/yyyy"));
               drwN["Tenure"] = clsEmployee.ComputeTenureDecimal(clsValidator.CheckDate(dr["datestrt"].ToString()), clsValidator.CheckDate(dr["dateend"].ToString())).ToString("#0.##");
               drwN["BirthDate"] = clsValidator.CheckDate(dr["brthdate"].ToString()).ToString("MM/dd/yyyy");
               drwN["Age"] = clsEmployee.ComputeAge(clsValidator.CheckDate(dr["brthdate"].ToString()));
               drwN["Gender"] = dr["gender"].ToString();
               drwN["EmploymentStatus"] = clsEmploymentStatus.GetEmploymentStatusName(dr["esttcode"].ToString());
               drwN["Division"] = clsDivision.GetDivisionNameShort(dr["divicode"].ToString());
               drwN["Department"] = clsDepartment.GetName(dr["deptcode"].ToString());
               drwN["Remarks"] = dr["remarks"].ToString();
               tblReturn.Rows.Add(drwN);
            }
            dr.Close();
         }
         return tblReturn;
      }

   }
}