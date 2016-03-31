using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.Blog;

namespace UITests.Bindings.When
{
    [Binding]
    public class WhenBlog
    {
        [When(@"I create the following blogs")]
        public void WhenICreateTheFollowingBlogs(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var blogPage = NavigationHelper.Navigate<BlogPage>();
                var blogCreatePage = blogPage.CreateBlog();

                var title = tableRow["Title"];
                var content = tableRow["Content"];

                blogCreatePage.Title = title;
                blogCreatePage.Content = content;

                blogCreatePage.SubmitBlog();
            }
        }

    }
}
