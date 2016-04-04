using System.Collections.Generic;
using Business.Services;

namespace Business.Repositories.Blog
{
    public class BlogRepository : IBlogRepository
    {
        public void CreateBlog(string name, string writer)
        {
            var neoConnection = NeoConnection.GetConnection();

            neoConnection.GraphClient.Cypher.Create("(b:Blog {Name: {name}})")
                .WithParam("name", name)
                .ExecuteWithoutResults();

            neoConnection.GraphClient.Cypher.Match("(b:Blog {Name: {name}}), (u:User {Name: {writer}})")
                .CreateUnique("(u)-[:CREATED]->(b)")
                .WithParams(new Dictionary<string, object>
                {
                    {"name", name},
                    {"writer", writer}
                })
                .ExecuteWithoutResults();
        }

        public void BlogRead(string reader, string blogName)
        {
            var neoConnection = NeoConnection.GetConnection();

            neoConnection.GraphClient.Cypher.Match("(b:Blog {Name: {name}}), (u:User {Name: {reader}})")
                .CreateUnique("(u)-[:READ]->(b)")
                .WithParams(new Dictionary<string, object>
                {
                    {"name", blogName},
                    {"reader", reader}
                })
                .ExecuteWithoutResults();
        }

        public IEnumerable<string> GetReadsForUser(string userName)
        {
            var neoConnection = NeoConnection.GetConnection();

            var blogList = neoConnection.GraphClient.Cypher.Match("(u:User {Name: {name}})-[:READ]->(b:Blog)")
                .WithParam("name", userName)
                .Return<string>("b.Name").Results;

            return blogList;
        }
    }
}