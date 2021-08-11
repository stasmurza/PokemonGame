using System;
using System.Web;

namespace ApiGateway.BusinessLayer.TranslateApi.UrlBuilder
{
    public class TranslateApiUrlBuilder: ITranslateApiUrlBuilder
    {
        public string Build(string apiUrl, string text)
        {
            if (String.IsNullOrEmpty(apiUrl)) throw new ArgumentNullException(nameof(apiUrl));
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            return apiUrl + HttpUtility.UrlEncode(text);
        }
    }
}
