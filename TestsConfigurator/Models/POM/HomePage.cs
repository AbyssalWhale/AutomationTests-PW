using Core.Managers;
using Microsoft.Playwright;
using TestsConfigurator.Models.POM.components;

namespace TestsConfigurator_PW.Models.POM
{
    public class HomePage : PageBase
    {
        private const string AboutUs_Href = "pages/aboutus.html";
        private const string Teachers_Href = "pages/teachers.html";
        private ILocator Button_AboutUs => Page.GetByRole(AriaRole.Link, new() { Name = "More about us" });
        private ILocator Button_Teachers => Page.GetByText("All teachers");

        public override string Title => "Vite + React + TS";

        public SearchComponent Search => new SearchComponent(Page, "Search Games");
        public TableComponent Table => new TableComponent(Page, "Game Table");

        public GenresComponent Genres => new GenresComponent(Page, "Genres Filter");

        public HomePage(IPage page) : base(page)
        {
        }

        public async Task<HomePage> Navigate()
        {
            var url = RunSettings.InstanceUrl;
            if (url is not null)
            {
                await Page.GotoAsync(url);
                await IsAtPage();
            }

            return this;
        }
    }
}
