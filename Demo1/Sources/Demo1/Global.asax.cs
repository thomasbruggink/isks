using Business;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Web.Routing;

namespace Demo1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Logs";

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            //Configure serilog
            var configuration = new LoggerConfiguration()
                .Enrich.WithProperty("servicename", "DemoApp")
                .Enrich.WithProperty("servername", Environment.MachineName)
                .WriteTo.RollingFile($@"{basePath}\{{Date}}-service.log", LogEventLevel.Debug);

            //If configured log to elasticsearch
            configuration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration.Instance.ElasticSearchEndpoint))
            {
                AutoRegisterTemplate = true,
                BufferBaseFilename = $@"{basePath}\ElasticBuffer",
                MinimumLogEventLevel = LogEventLevel.Debug
            });

            Log.Logger = configuration.CreateLogger();
        }
    }
}