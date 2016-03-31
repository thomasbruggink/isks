using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Connections;
using Business.Models;
using Cassandra;

namespace Business.Repositories
{
    public class BlogRepository
    {
        private readonly CassandraConnection _cassandraConnection;
        private readonly PreparedStatement _getAllBlogs;
        private readonly PreparedStatement _createBlog;
        private readonly UserRepository _userRepository;

        public BlogRepository()
        {
            const string getAllBlogs = "SELECT * FROM blogs";
            const string createBlog = "INSERT INTO blogs (id, content, date, title, writer) VALUES (now(), :content, :date, :title, :writer)";

            _cassandraConnection = CassandraConnection.GetInstance();
            _userRepository = new UserRepository();
            _getAllBlogs = _cassandraConnection.PreparedStatement(getAllBlogs);
            _createBlog = _cassandraConnection.PreparedStatement(createBlog);
        } 

        public List<Blog> GetBlogs()
        {
            var results = _cassandraConnection.ExecuteReader(_getAllBlogs.Bind());

            return results.GetRows().Select(row => new Blog
            {
                Id = row.GetValue<Guid>("id"),
                Title = row.GetValue<string>("title"),
                Content = row.GetValue<string>("content"),
                CreatedTime = row.GetValue<DateTime>("date"),
                Writer = _userRepository.GetUserByName(row.GetValue<string>("writer"))
            }).ToList();
        }

        public void CreateBlog(User user, string title, string content)
        {
            var date = ((long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds) * 1000;
            _cassandraConnection.ExecuteNonReader(_createBlog.Bind(new
            {
                writer = user.UserName,
                title = title,
                content = content,
                date = date
            }));
        }
    }
}
