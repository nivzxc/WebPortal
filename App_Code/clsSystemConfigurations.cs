using System.Configuration;

public static class clsSystemConfigurations
{
   public static string ConnectionStringMRCF { get { return ConfigurationManager.ConnectionStrings["Speedo"].ToString(); } }
   public static string ConnectionStringRequisition { get { return ConfigurationManager.ConnectionStrings["Speedo"].ToString(); } }
   public static string ConnectionStringTransmittal { get { return ConfigurationManager.ConnectionStrings["Speedo"].ToString(); } }
   public static string PortalRootURL { get { return ConfigurationManager.AppSettings["SpeedoURL"].ToString(); } }
   //Added by Ian for Oracle Data Extraction
   public static string ConnectionStringOracle { get { return ConfigurationManager.ConnectionStrings["ORACLEConStr"].ToString(); } }

}