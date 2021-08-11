using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.TranslateApi.Proxy
{
    public interface ITranslateServiceProxy
    {
        public Task<string> TranslateAsync(string url);
    }
}
