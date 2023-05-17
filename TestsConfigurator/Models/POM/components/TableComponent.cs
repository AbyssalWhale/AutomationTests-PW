using Microsoft.Playwright;

namespace TestsConfigurator.Models.POM.components
{
    public class TableComponent : ComponentBase
    {
        public TableComponent(IPage page, string name) : base(page, name)
        {
        }

        private string Title_Cards => "//h2[@class='chakra-heading css-1xix1js']";

        public async Task<IReadOnlyList<string>> Get_Cards_Titles()
        {
            await Page.WaitForSelectorAsync(Title_Cards);
            var result = await Page.Locator(Title_Cards).AllInnerTextsAsync();
            return result;
        }
    }
}
