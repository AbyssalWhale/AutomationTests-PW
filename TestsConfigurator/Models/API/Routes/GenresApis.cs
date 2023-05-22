using Core.Managers;
using RestSharp;
using TestsConfigurator.Models.API.Models;

namespace TestsConfigurator.Models.API.Routes
{
    public class GenresApis : RouteBase
    {
        protected override string RouteUrl => "genres";
        public GenresApis(ApiManager apiManager) : base(apiManager)
        {
        }

        public async Task<Genres> Get_AllGenres()
        {
            var response = await apiManager.ExecuteRequest(RouteUrl, Method.Get, checkResponseCode: true);
            var data = apiManager.Deserialize_ResponseData<Genres>(response);
            return data;
        }

        public async Task<Genre> Get_Genre(string name)
        {
            var allGenres = Get_AllGenres().Result.results;
            var result = allGenres.Where(g => g.name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            return result;
        }
    }
}