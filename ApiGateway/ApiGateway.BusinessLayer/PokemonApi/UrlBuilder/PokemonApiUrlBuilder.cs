using System;
using System.Web;

namespace ApiGateway.BusinessLayer.PokemonApi.UrlBuilder
{
    public class PokemonApiUrlBuilder : IPokemonApiUrlBuilder
    {
        public string ByNameUrl(string apiUrl, string name)
        {
            if (String.IsNullOrEmpty(apiUrl)) throw new ArgumentNullException(nameof(apiUrl));
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return $"{apiUrl}/byname/{HttpUtility.UrlEncode(name)}";
        }
    }
}
