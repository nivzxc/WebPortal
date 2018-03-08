using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace STIeForms
{
 public static class clsEncryptDecrypt
 {
  const string DESKey = "AQWSEDRF";
  const string DESIV = "HGFEDCBA";
  //public static string DESDecrypt(string stringToDecrypt)//Decrypt the content
  //{

  // byte[] key;
  // byte[] IV;

  // byte[] inputByteArray;
  // try
  // {
  //  key = Convert2ByteArray(DESKey);
  //  IV = Convert2ByteArray(DESIV);
  //  int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);


  //  DESCryptoServiceProvider des = new DESCryptoServiceProvider();
  //  MemoryStream ms = new MemoryStream();
  //  CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
  //  cs.Write(inputByteArray, 0, inputByteArray.Length);
  //  cs.FlushFinalBlock();
  //  Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
  // }
  // catch { }





  //}

  public static string DESEncrypt(string stringToEncrypt)// Encrypt the content
  {

   byte[] key;
   byte[] IV;

   byte[] inputByteArray;
   try
   {

    key = Convert2ByteArray(DESKey);

    IV = Convert2ByteArray(DESIV);

    inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

    MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
    cs.Write(inputByteArray, 0, inputByteArray.Length);

    cs.FlushFinalBlock();

    return Convert.ToBase64String(ms.ToArray());
   }

   catch (System.Exception ex)
   {

    throw ex;
   }

  }
  static byte[] Convert2ByteArray(string strInput)
  {

   int intCounter; char[] arrChar;
   arrChar = strInput.ToCharArray();

   byte[] arrByte = new byte[arrChar.Length];

   for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
    arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

   return arrByte;
  }
  public static string EncryptAndHash(string value)
  {
   MACTripleDES des = new MACTripleDES();
   MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
   des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
   string encrypted = Convert.ToBase64String(des.ComputeHash(Encoding.UTF8.GetBytes(value))) + '-' + Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

   return HttpUtility.UrlEncode(encrypted);
  }
  public static string DecryptWithHash(this string encoded)
  {
   MACTripleDES des = new MACTripleDES();
   MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
   des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(encoded));

   string decoded = HttpUtility.UrlDecode(encoded);
   // in the act of url encoding and decoding, plus (valid base64 value) gets replaced with space (invalid base64 value). this reverses that.
   decoded = decoded.Replace(" ", "+");
   string value = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[1]));
   string savedHash = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[0]));
   string calculatedHash = Encoding.UTF8.GetString(des.ComputeHash(Encoding.UTF8.GetBytes(value)));

   if (savedHash != calculatedHash) return null;

   return value;
  }
 }
}

