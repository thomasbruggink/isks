using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITests.TestSupport.Api;

namespace UITests.TestSupport.Models
{
    class Blog
    {
        public string Title { get; set; }
        public string Content { get; set; }

        private string _writerName;

        public Lazy<User> Writer { get; set; }

        public Blog()
        {
            Writer = new Lazy<User>(RetrieveUser);
        }

        public static Blog CreateBlog(string title, string content, string writer)
        {
            var blogApiClient = new BlogApiClient();
            var response = blogApiClient.CreateBlog(title, content, writer);
            Assert.AreEqual(HttpStatusCode.OK, response.Status, $"Could not create blog Writer: {writer}, Title: {title}, Content: {content}");

            return new Blog
            {
                Title = title,
                Content = content,
                _writerName = writer
            };
        }

        private User RetrieveUser()
        {
            return User.GetUserByName(_writerName);
        }
    }
}
