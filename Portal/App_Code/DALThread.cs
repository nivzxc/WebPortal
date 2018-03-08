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
}