using Microsoft.Playwright;

namespace TestsConfigurator.Models.POM.components
{
    public class SearchComponent : ComponentBase
    {
        public SearchComponent(IPage page, string name) : base(page, name)
        {
        }

        private ILocator Input_SearchGames => Page.Locator("//input[@placeholder='Search games...']");

        public async Task<SearchComponent> Input_Search_Game(string gameName)
        {
            await Input_SearchGames.FillAsync(gameName);
            await Input_SearchGames.PressAsync("Enter");
            return this;
        }
    }
}
