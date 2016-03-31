using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.Connections;

namespace TestSupport.api
{
    public class DatabaseController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Reset()
        {
            var connection = CassandraConnection.GetInstance();
            foreach (var table in connection.GetTables())
            {
                connection.ExecuteNonReader($"TRUNCATE TABLE {CassandraConnection.KeySpace}.{table}");
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}