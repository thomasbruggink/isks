using System;
using System.Threading;
using Microsoft.Owin.Hosting;

namespace Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Store wait handle inside the try-scope as it gets modified outside the scope.
            // Not copying this will result in weird behavior in the application.
            var handle = new ManualResetEvent(false);

            // When the user presses Ctrl+C the application should close.
            // To do this, the Ctrl+C handler is implemented here and sets a wait handle to enable the application to close.
            Console.CancelKeyPress += (s, e) => { handle.Set(); };

            const string host = "*";
            const int port = 8080;


            using (WebApp.Start<Startup>($"http://{host}:{port}"))
            {
                Console.WriteLine($"Started webserver on http://{host}:{port}");

                handle.WaitOne();
            }
        }
    }
}