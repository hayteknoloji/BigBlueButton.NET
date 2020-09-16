using System;
using System.Security.Cryptography;
using System.Text;

namespace HayTeknoloji.BigBlueButton.Helpers
{
    public static class UrlHelper
    {
        public static string GetBbbUri(string serverUrl, string salt, string apiType, string parameters)
        {
            var ch = CheckSum(apiType + parameters + salt);
            var baseUri = new Uri(serverUrl);
            var url = new Uri(baseUri, $"bigbluebutton/api/{apiType}?checksum={ch}&{parameters}").ToString();

            return url;
        }

        public static string CheckSum(string s)
        {
            var buffer = Encoding.UTF8.GetBytes(s);
            var cryptoTransformSha1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
    }
}