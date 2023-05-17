using Microsoft.Playwright;

namespace TestsConfigurator.Models.POM.components
{
    public abstract class ComponentBase
    {
        protected IPage Page;
        public string Name;

        public ComponentBase(IPage page, string name)
        {
            Page = page;
            Name = name;
        }
    }
}