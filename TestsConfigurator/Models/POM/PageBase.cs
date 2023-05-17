using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace TestsConfigurator_PW.Models.POM
{
    public abstract class PageBase
    {
        protected IPage Page;
        public abstract string Title { get; }

        public PageBase(IPage page)
        {
            Page = page;
        }

        public virtual async Task IsAtPage()
        {
            await Assertions.Expect(Page).ToHaveTitleAsync(Title);
        }
    }
}
