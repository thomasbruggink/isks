using System.Text;
using Newtonsoft.Json;

namespace Engine.Tests.Helpers
{
    public class EventHelper
    {
        public static byte[] CreateEvent(string key, object content)
        {
            var message = new
            {
                key = key,
                content = content
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            return data;
        }
    }
}
