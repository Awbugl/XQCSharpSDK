using System.Net;
using System.Text;

namespace XQ.Enhance
{
    public static class HttpHelper
    {
        private static string GetStringFromUrl(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            var wc = new WebClient { Encoding = Encoding.UTF8 };
            return wc.DownloadString(url);
        }
    }
}