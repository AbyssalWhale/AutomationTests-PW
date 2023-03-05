using NUnit.Framework;
using TestsConfigurator_PW.Fixtures;

namespace RegressionTests_PW.UI
{
    //[Parallelizable(ParallelScope.Children)]
    //[TestFixture]
    public class TeachersPage : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanNavigateTo_Teachers_Page_TES_T5()
        {
            await HomePage.Click_Tachers_Button()
                .Result.IsAtPage();
        }
    }
}
