using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public static class DALThread
{

	public static int DeleteThreadPrivateUser(int threadID, string username)
	{
		int intReturn = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "DELETE FROM Portal.ThreadPrivateUsers WHERE ThreadID=@ThreadID AND Username=@Username";
			cmd.Parameters.Add(new SqlParameter("@ThreadID", threadID));
			cmd.Parameters.Add(new SqlParameter("@Username", username));
			cn.Open();
			intReturn = cmd.ExecuteNonQuery();
		}
		return intReturn;
	}

	public static string GetCategoryName(int threadCategoryID)
	{
		string categoryName = string.Empty;

		using (ThreadDataContext tdc = new ThreadDataContext())
		{
			categoryName = (from tc in tdc.ThreadCategories
							where tc.ThreadCategoryID == threadCategoryID
							select tc.Name).SingleOrDefault();
		}

		return categoryName;
	}

	public static bool IsAllowedPostHome(string username)
	{
		bool isAllowed = false;

		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText = "SELECT username FROM Speedo.ForumAllowedHomePost WHERE username=@username";
			cmd.Parameters.Add(new SqlParameter("@username", username));
			cn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			isAllowed = dr.Read();
			dr.Close();
		}

		return isAllowed;
	}

	public static int UpdateThread(Thread thread)
	{
		int affectedRecords = 0;
		using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
		{
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandText =	"UPDATE Portal.Threads SET ThreadCategoryID = @ThreadCategoryID," + 
								"ThreadTypeID = @ThreadTypeID," + 
								"Title = @Title," + 
								"Description = @Description," + 
								"Contents = @Contents," + 
								"IsAllowedReply = @IsAllowedReply," +
								"IsPosted = @IsPosted," +
								"IsActive = @IsActive," +
								"IsSticky = @IsSticky " + 
								"WHERE ThreadID = @ThreadID";

			cmd.Parameters.Add(new SqlParameter("@ThreadID", thread.ThreadID));
			cmd.Parameters.Add(new SqlParameter("@ThreadCategoryID", thread.ThreadCategoryID));
			cmd.Parameters.Add(new SqlParameter("@ThreadTypeID", thread.ThreadTypeID));
			cmd.Parameters.Add(new SqlParameter("@Title", thread.Title));
			cmd.Parameters.Add(new SqlParameter("@Description", thread.Description));
			cmd.Parameters.Add(new SqlParameter("@Contents", thread.Contents));
			cmd.Parameters.Add(new SqlParameter("@IsAllowedReply", thread.IsAllowedReply));
			cmd.Parameters.Add(new SqlParameter("@IsPosted", thread.IsPosted));
			cmd.Parameters.Add(new SqlParameter("@IsActive", thread.IsActive));
			cmd.Parameters.Add(new SqlParameter("@IsSticky", thread.IsSticky));
			cn.Open();
			affectedRecords = cmd.ExecuteNonQuery();
		}
		return affectedRecords;
	}

    public static DataTable GetThreadCategory()
    {
        DataTable tblReturn = new DataTable();
        tblReturn.Columns.Add("pValue");
        tblReturn.Columns.Add("pText");
        DataTable tblTemporary = new DataTable();

        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT ThreadCategoryID AS pValue, Name AS pText FROM Portal.ThreadCategories WHERE  (IsActive = '1') ORDER BY pText";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblTemporary);

                DataRow drNew = tblReturn.NewRow();
                drNew["pValue"] = "ALL";
                drNew["pText"] = "ALL";
                tblReturn.Rows.Add(drNew);

                foreach (DataRow dr in tblTemporary.Rows)
                {
                    drNew = tblReturn.NewRow();
                    drNew["pValue"] = dr["pValue"].ToString();
                    drNew["pText"] = dr["pText"].ToString();
                    tblReturn.Rows.Add(drNew);
                }
            }
        }

        return tblReturn;
    }

    public static int GetThreadArchiveCount(string pCategory, DateTime dtStart, DateTime dtEnd, string pKeyWord)
    {
        int intReturn = 0;

        if (pCategory != "ALL")
        {
            pCategory = " AND ThreadCategoryID='" + pCategory + "' ";
        }
        else
        {
            pCategory = "";
        }

        if (pKeyWord != string.Empty)
        {
            pKeyWord = " AND (Title LIKE '%" + pKeyWord + "%' OR Description LIKE '%" + pKeyWord + "%' OR Contents LIKE  '%" + pKeyWord + "%' OR PostedBy LIKE  '%" + pKeyWord + "%') ";
        }
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(ThreadID) FROM Portal.Threads WHERE (IsPrivate = '0') AND (IsActive = '1') AND PostedDate BETWEEN @dtStart AND @dtEnd " + pCategory + pKeyWord;
                cmd.Parameters.Add(new SqlParameter("@keyword", pKeyWord));
                cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                cn.Open();
                try
                {
                    intReturn = cmd.ExecuteScalar().ToSafeString().ToInt();
                }
                catch 
                { 
                    intReturn = 0; 
                }
            }
        }
        return intReturn;
    }

    public static DataTable GetThreadArchiveArchives(string pCategory, DateTime dtStart, DateTime dtEnd, int pLimit, int pOffset, string pKeyWord)
    {
        DataTable tblReturn = new DataTable();

        if (pCategory != "ALL")
        {
            pCategory = " AND ThreadCategoryID='" + pCategory + "' ";
        }
        else
        {
            pCategory = "";
        }

        if (pKeyWord != string.Empty)
        {
            pKeyWord = " AND (Title LIKE '%" + pKeyWord + "%' OR Description LIKE '%" + pKeyWord + "%' OR Contents LIKE  '%" + pKeyWord + "%' OR PostedBy LIKE  '%" + pKeyWord + "%') ";
        }
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                //cmd.CommandText = "SELECT * FROM Portal.Threads WHERE (IsPrivate = '0') AND (IsActive = '1') " + pCategory + pKeyWord;
                //SELECT RowNum, GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy ,CreateOn FROM (SELECT DISTINCT GroupUpdateCode, DepartmentCode, DivisionCode, Contributor, CreateBy , (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) AS CreateOn,(row_number() OVER (ORDER BY CreateOn DESC)) AS RowNum  FROM Portal.GroupUpdate WHERE (Status='1' AND Enabled='1' " + strDivisionCode + strDepartmentCode + strKeyWord + "  AND (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) BETWEEN @dtStart AND @dtEnd)) AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")"
                //SELECT RowNum, ThreadID, Title, Description, Contents, PostedBy, PostedDate FROM (SELECT DISTINCT ThreadID, Title, Description, Contents, PostedBy, PostedDate,(row_number() OVER (ORDER BY PostedDate DESC)) AS RowNum  FROM Portal.GroupUpdate WHERE (IsPrivate = '0' AND IsActive = '1' " + pCategory + pKeyWord + "  AND (SELECT TOP(1) DateApprove FROM  Portal.GroupUpdateApproval WHERE GroupUpdateCode = Portal.GroupUpdate.GroupUpdateCode ORDER BY DateApprove DESC) BETWEEN @dtStart AND @dtEnd)) AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")"
                cmd.CommandText = "SELECT RowNum, ThreadID,PostedBy, PostedDate FROM (SELECT DISTINCT ThreadID, PostedBy,PostedDate, (row_number() OVER (ORDER BY PostedDate DESC)) AS RowNum  FROM Portal.Threads  WHERE (IsPrivate = '0' AND IsActive = '1' " + pCategory + pKeyWord + " AND PostedDate BETWEEN @dtStart AND @dtEnd)) AS DataRow WHERE (DataRow.RowNum > " + pOffset + ") AND (DataRow.RowNum<= " + pOffset + " + " + pLimit + ")";
                cmd.Parameters.Add(new SqlParameter("@dtStart", dtStart));
                cmd.Parameters.Add(new SqlParameter("@dtEnd", dtEnd));
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tblReturn);
            }
        }
        return tblReturn;
    }

    public static string GetTitle(int pThreadID)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Title FROM Portal.Threads WHERE ThreadID=@ThreadID";
                cmd.Parameters.Add(new SqlParameter("@ThreadID", pThreadID));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        return strReturn;
    }

    public static string GetDescription(int pThreadID)
    {
        string strReturn = "";
        using (SqlConnection cn = new SqlConnection(clsSpeedo.SpeedoConnectionString))
        {
            using (SqlCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = "SELECT Description FROM Portal.Threads WHERE ThreadID=@ThreadID";
                cmd.Parameters.Add(new SqlParameter("@ThreadID", pThreadID));
                cn.Open();
                strReturn = cmd.ExecuteScalar().ToString();
            }
        }
        return strReturn;
    }

}