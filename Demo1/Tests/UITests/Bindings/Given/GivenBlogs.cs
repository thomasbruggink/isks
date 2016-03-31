using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using UITests.TestSupport.Models;

namespace UITests.Bindings.Given
{
    [Binding]
    public class GivenBlogs
    {
        [Given(@"The following blogs are created")]
        public void GivenTheFollowingBlogsAreCreated(Table table)
        {

            foreach (var tableRow in table.Rows)
            {
                var title = tableRow["Title"];
                var content = tableRow["Content"];
                var user = tableRow["Writer"];

                Blog.CreateBlog(title, content, user);
            }
        }

    }
}
