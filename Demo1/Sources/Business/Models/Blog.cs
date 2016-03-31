using System;

namespace Business.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public User Writer { get; set; }
    }
}