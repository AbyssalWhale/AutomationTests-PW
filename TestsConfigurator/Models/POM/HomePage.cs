using Core.Managers;
using Microsoft.Playwright;

namespace TestsConfigurator_PW.Models.POM
{
    public class HomePage : PageBase
    {
        private const string AboutUs_Href = "pages/aboutus.html";
        private const string Teachers_Href = "pages/teachers.html";
        private ILocator Button_AboutUs => Page.GetByRole(AriaRole.Link, new() { Name = "More about us" });
        private ILocator Button_Teachers => Page.GetByText("All teachers");

        public override string Title => "Main Page";

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

        public async Task<AboutUsPage> Click_AboutUs_Button()
        {
            await Assertions.Expect(Button_AboutUs).ToHaveAttributeAsync("href", AboutUs_Href);
            await Button_AboutUs.ClickAsync();
            return new AboutUsPage(Page);
        }

        public async Task<TeachersPage> Click_Tachers_Button()
        {
            await Assertions.Expect(Button_Teachers).ToHaveAttributeAsync("href", Teachers_Href);
            await Button_Teachers.ClickAsync();
            return new TeachersPage(Page);
        }
    }
}
