using Business.Repositories.Blog;
using TechTalk.SpecFlow;

namespace Engine.Tests.Bindings.Given
{
    [Binding]
    public class GivenBlog
    {
        [Given(@"The following blog created events have been sent")]
        public void GivenTheFollowingBlogCreatedEventsHaveBeenSent(Table table)
        {
            var blogRepository = new BlogRepository();

            foreach (var tableRow in table.Rows)
            {
                var blogName = tableRow["Name"];
                var writer = tableRow["Writer"];

                blogRepository.CreateBlog(blogName, writer);
            }
        }

        [Given(@"The following blog read events are send")]
        public void GivenTheFollowingBlogReadEventsAreSend(Table table)
        {
            var blogRepository = new BlogRepository();

            foreach (var tableRow in table.Rows)
            {
                var blogName = tableRow["Blog Name"];
                var reader = tableRow["Reader"];

                blogRepository.BlogRead(reader, blogName);
            }
        }
    }
}