using ApiGateway.BusinessLayer.TranslateApi.Config;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.TranslateApi
{
    public interface ITranslateService
    {
        public Task<string> TranslateAsync(string text);
    }
}
