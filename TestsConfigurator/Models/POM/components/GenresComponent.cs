
using Microsoft.Playwright;
using TestsConfigurator.Models.API.Models;

namespace TestsConfigurator.Models.POM.components
{
    public class GenresComponent : ComponentBase
    {
        public GenresComponent(IPage page, string name) : base(page, name)
        {
        }

        private string Link_Genre(string genreName) => $"//button[text()='{genreName}']";

        public async Task<IResponse> Click_Genre_Button(Genre genre)
        {
            await Page.Locator(Link_Genre(genre.name)).ClickAsync();
            var clickResponse = await Page.WaitForResponseAsync("**/api/games**&genres=**");
            return clickResponse;
        }
    }
}
