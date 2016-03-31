using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using UITests.Pages;
using UITests.Pages.Blog;

namespace UITests.Bindings.Then
{
    [Binding]
    public class ThenBlog
    {
        [Then(@"I see the following blogs")]
        public void ThenISeeTheFollowingBlogs(Table table)
        {
            var blogPage = NavigationHelper.ResolvePage<BlogPage>();

            var blogs = blogPage.GetBlogs();

            foreach (var tableRow in table.Rows)
            {
                var title = tableRow["Title"];
                var content = tableRow["Content"];
                var writer = tableRow["Writer"];

                var blog = blogs.FirstOrDefault(b => b.Title.Equals(title));
                Assert.IsNotNull(blog, $"Could not find a blog with the title '{title}'");
                Assert.AreEqual(content, blog.Content, "Content does not match");
                Assert.AreEqual(writer, blog.Writer, "Writer does not match");
            }
        }
    }
}
