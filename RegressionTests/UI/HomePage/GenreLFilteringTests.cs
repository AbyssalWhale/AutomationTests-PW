using NUnit.Framework;
using TestsConfigurator.Fixtures;

namespace RegressionTests.UI.HomePage
{
    public class TeachersPage : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanSelect_Game_Genre_TES_T5()
        {
            //Arrange
            var genreName = "Action";
            var genreUnderTest = await backendApis.Genres.Get_Genre(genreName);
            Assert.IsNotNull(genreUnderTest, $"Expected that genre with name '{genreName}' is found");

            //Act
            await HomePage.Genres.Click_Genre_Button(genreUnderTest);

            //Assert
            var gameTitles = await HomePage.Table.Get_Cards_Titles();

            Parallel.ForEach(gameTitles, gameTitle =>
            {
                Assert.IsNotNull(genreUnderTest.games.Where(g => g.name.ToLower().Equals(gameTitle.ToLower())), "Expected game is found on UI after filtering by genre");
            });
        }
    }
}
