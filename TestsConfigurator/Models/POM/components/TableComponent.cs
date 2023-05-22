using Microsoft.Playwright;

namespace TestsConfigurator.Models.POM.components
{
    public class TableComponent : ComponentBase
    {
        public TableComponent(IPage page, string name) : base(page, name)
        {
        }

        private string Title_Cards => "//h2[@class='chakra-heading css-1xix1js']";
        private string Skeleton_Cards => "chakra-skeleton css-1uzecpb";

        public async Task<IReadOnlyList<string>> Get_Cards_Titles()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            var isSkeletonVisible = await Page.Locator(Skeleton_Cards).IsVisibleAsync();
            if (isSkeletonVisible)
            {
                await Assertions.Expect(Page.Locator(Skeleton_Cards)).ToHaveCountAsync(0);
            }

            await Page.WaitForSelectorAsync(Title_Cards);
            var result = await Page.Locator(Title_Cards).AllInnerTextsAsync();

            return result;
        }
    }
}
