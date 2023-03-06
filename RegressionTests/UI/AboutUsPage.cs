using NUnit.Framework;
using TestsConfigurator.Fixtures;

namespace RegressionTests.UI
{
    public class AboutUsPage : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanNavigateTo_AboutUs_Page_TES_T6()
        {
            await HomePage.Click_AboutUs_Button()
                .Result.IsAtPage();
        }
    }
}
