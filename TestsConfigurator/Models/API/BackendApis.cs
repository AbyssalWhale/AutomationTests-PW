using Core.Managers;
using TestsConfigurator.Models.API.Routes;

namespace TestsConfigurator.Models.API
{
    public class BackendApis
    {
        private ApiManager apiManager;
        public BackendApis(ApiManager apiManager) 
        { 
            this.apiManager = apiManager;
        }

        public GenresApis Genres => new GenresApis(apiManager);
    }
}
