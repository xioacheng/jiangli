﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using jiangli.Models.Constants;
namespace jiangli.Util
{
    public class GetUsersHelper
    {
        public static LoginResult GetOpenId(string code)
        {
            WebClient wb = new WebClient();
            string url = "https://api.weixin.qq.com/sns/jscode2session?"
            + "appid=" + GlobalConfig.appid
            + "&secret=" + GlobalConfig.secret
            + "&grant_type=authorization_code&js_code=" + code;
            try
            {
                Stream sc = wb.OpenRead(url);
                StreamReader sr = new StreamReader(sc);
                string result = sr.ReadToEnd();
                LoginResult li = JsonConvert.DeserializeObject<LoginResult>(result);
                sc.Close(); sr.Close();
                return li;
            }
            catch (Exception e)
            {
                LogHelper.GetInstance().Error(e.Message);
                return null;
            }
        }
        #region 微信小程用户数据解密

        public static string AesKey;
        public static string AesIV;

        public static string AESDecrypt(string inpudata)
        {
            try
            {
                AesIV = AesIV.Replace(" ", "+");
                AesKey = AesKey.Replace(" ", "+");
                inpudata = inpudata.Replace(" ", "+");
                byte[] encryptedData = Convert.FromBase64String(inpudata);
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Key = Convert.FromBase64String(AesKey);
                rijn.IV = Convert.FromBase64String(AesIV);
                rijn.Mode = CipherMode.CBC;
                rijn.Padding = PaddingMode.PKCS7;
                ICryptoTransform trasform = rijn.CreateDecryptor();
                byte[] plainText = trasform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }
}