using System;
using System.Net.Http;
using Microsoft.Owin.Testing;

namespace Service.Tests.Helpers
{
    public class TestWebSender
    {
        public static void Send(Action<HttpClient> action)
        {
            using (var server = TestServer.Create<Startup>())
            using (var client = server.HttpClient)
            {
                //Recall the test action
                action.Invoke(client);
            }
        }
    }
}
