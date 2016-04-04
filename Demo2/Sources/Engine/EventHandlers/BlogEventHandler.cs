using Business.Repositories.Blog;

namespace Engine.EventHandlers
{
    public class BlogEventHandler : IEventHandler
    {
        private readonly IBlogRepository _blogRepository;

        public BlogEventHandler()
        {
            _blogRepository = new BlogRepository();
        }

        public void Handle(string key, dynamic data)
        {
            switch (key)
            {
                case "blogCreated":
                {
                    BlogCreated(data);
                    break;
                }
                case "blogRead":
                {
                    BlogRead(data);
                    break;
                }
            }
        }

        private void BlogCreated(dynamic data)
        {
            var name = (string) data.name;
            var writer = (string) data.writer;

            _blogRepository.CreateBlog(name, writer);
        }

        private void BlogRead(dynamic data)
        {
            var reader = (string) data.reader;
            var blogName = (string) data.blogName;

            _blogRepository.BlogRead(reader, blogName);
        }
    }
}