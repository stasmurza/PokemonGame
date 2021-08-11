using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.Client
{
    public interface IApiClient
    {
        Task<string> GetAsync(string url);
    }
}
