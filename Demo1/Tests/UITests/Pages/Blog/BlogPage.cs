﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UITests.Pages.Blog.Parts;

namespace UITests.Pages.Blog
{
    public class BlogPage : PageBase<BlogPage>, IPage
    {
        public List<BlogPart> GetBlogs()
        {
            var elements = Driver.GetDriver().FindElements(By.ClassName("blog"));

            return elements.Select(e => new BlogPart(e)).ToList();
        }

        public CreateBlogPage CreateBlog()
        {
            var createBlogText = Driver.GetDriver().FindElement(By.Id("createblog"));
            createBlogText.Click();

            return NavigationHelper.ResolvePage<CreateBlogPage>();
        }

        public string GetUrl()
        {
            return "/";
        }
    }
}
