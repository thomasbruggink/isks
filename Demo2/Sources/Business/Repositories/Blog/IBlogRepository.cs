using System.Collections.Generic;

namespace Business.Repositories.Blog
{
    public interface IBlogRepository
    {
        void CreateBlog(string name, string writer);
        void BlogRead(string reader, string blogName);
        IEnumerable<string> GetReadsForUser(string userName);
    }
}
