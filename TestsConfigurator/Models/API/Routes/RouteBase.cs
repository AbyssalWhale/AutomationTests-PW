using Core.Managers;

namespace TestsConfigurator.Models.API.Routes
{
    public abstract class RouteBase
    {
        protected ApiManager apiManager;
        protected abstract string RouteUrl { get; }

        public RouteBase(ApiManager apiManager)
        {
            this.apiManager = apiManager;
        }
    }
}
