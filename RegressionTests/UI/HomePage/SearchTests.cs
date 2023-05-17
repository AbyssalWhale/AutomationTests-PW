using NUnit.Framework;
using TestsConfigurator.Fixtures;

namespace RegressionTests.UI.HomePage
{
    public class SearchTests : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanSearch_Game_TES_T6([Values("Grand Theft Auto")] string gameName)
        {
            //Act
            await HomePage.Search.Input_Search_Game(gameName);

            //Assert
            var titles = await HomePage.Table.Get_Cards_Titles();
            Assert.Contains(gameName, titles.ToList(), $"Expected that all test titles contains game {gameName}");
        }
    }
}
