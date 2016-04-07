using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Service.Tests.Helpers;
using TechTalk.SpecFlow;

namespace Service.Tests.Bindings.Then
{
    [Binding]
    internal class ThenBlog
    {
        [Then(@"The following blogs are returned")]
        public void ThenTheFollowingBlogsAreReturned(Table table)
        {
            foreach (var row in table.Rows)
            {
                var user = row["User"];

                var response = ApiResultTable.Instance.GetResultByName(user);
                var responseData = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual(200, (int)response.StatusCode, $"Received the following error: '{responseData}'");
                var blogs = JsonConvert.DeserializeObject<string[]>(responseData);
                Assert.AreEqual(table.Rows.Count, blogs.Length, "Received a different amount of blogs");

                var blogName = row["Name"];
                Assert.IsTrue(blogs.Any(b => b.Equals(blogName)), $"Could not find a blog with the name '{blogName}'");
            }
        }
    }
}