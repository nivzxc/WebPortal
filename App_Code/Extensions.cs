using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Extension methods
 *  For easier/cleaner data conversion
 * Created by: Mily Opena
 * Date Created: 2/7/2011
 */

public static class Extensions
{
    public static int ToInt(this string value)
    {
        int returnValue;
        if (!int.TryParse(value, out returnValue))
            returnValue = 0;
        return returnValue;
    }

    public static long ToLong(this string value)
    {
        long returnValue;
        if (!long.TryParse(value, out returnValue))
            returnValue = 0;
        return returnValue;
    }

    public static decimal ToDecimal(this string value)
    {
        decimal returnValue;
        if (!decimal.TryParse(value, out returnValue))
            returnValue = 0;
        return returnValue;
    }

    public static long ToLong(this int value)
    {
        long returnValue;
        if (!long.TryParse(value.ToString(), out returnValue))
            returnValue = 0;
        return returnValue;
    }

    public static string Right(this string value, int length)
    {
        return value.Substring(value.Length - length);
    }

    public static int ToSafeInt(this int? value)
    {
        return value ?? 0;
    }

    public static string ToSafeString(this object obj)
    {
        return (obj ?? string.Empty).ToString();
    }

    public static string ToSafeString(this string value)
    {
        return (value ?? string.Empty);
    }

	public static char ToChar(this string value)
	{
		char returnValue;
		if (!char.TryParse(value.ToString(), out returnValue))
			returnValue = ' ';
		return returnValue;
	}
}