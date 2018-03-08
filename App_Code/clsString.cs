using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class clsString
{
    public clsString() { }

    //////////////////////////////////
    ///////// Static Members /////////
    //////////////////////////////////

    public static string NotDefinedApprovers { get { return "<br>Department/Division approver was not defined."; } }


    //MRCF
    public static string Submit { get { return "Submitting"; } }
    public static string OracleUnavailable { get { return "Oracle Database is currently not available.<br/>Please contact the System Administrator."; } }


    //ATW
    public static string InvalidDateStartEnd { get { return "Invalid date start and date end."; } }
    public static string InvalidDateDeadlineReach { get { return "Invalid specified date. Deadline of submission has been reached."; } }

    //UnderTime You already filed an undertime for this date.
    public static string AlreadyFiled { get { return "You already filed an undertime for this date."; } }

    public static string ZeroNumber(string pString, int pStringLength)
    {
        string strLeftPart = "";
        for (int i = 0; i < pStringLength; i++) strLeftPart += "0";
        return (strLeftPart + pString).Substring(pString.Length);
    }

    public static string CutString(string pString, int pCut)
    {
        if (pString.Length <= pCut)
            return pString;
        else
            return pString.Substring(0, pCut) + "...";
    }
}