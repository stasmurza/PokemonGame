
namespace ApiGateway.BusinessLayer.TranslateApi.UrlBuilder
{
    public interface ITranslateApiUrlBuilder
    {
        public string Build(string apiUrl, string text);
    }
}
