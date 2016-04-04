using System.Collections.Generic;
using System.Net.Http;

namespace Service.Tests.Helpers
{
    /// <summary>
    /// A class used to store responses from WebAPIs during a scenario.
    /// </summary>
    public class ApiResultTable
    {
        private static ApiResultTable _instance;

        public static ApiResultTable Instance => _instance ?? (_instance = new ApiResultTable());

        private readonly Dictionary<string, HttpResponseMessage> _nameResult;

        private ApiResultTable()
        {
            _nameResult = new Dictionary<string, HttpResponseMessage>();
        }

        /// <summary>
        /// Adds a result to the internal dictionary.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The dictionary value</param>
        public void AddResult(string name, HttpResponseMessage result)
        {
            _nameResult.Add(name, result);
        }

        /// <summary>
        /// Retrieves the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <returns>The HttpResponseMessage that belongs to the specified key.</returns>
        public HttpResponseMessage GetResultByName(string name)
        {
            return _nameResult[name];
        }

        /// <summary>
        /// Updates the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The result to store</param>
        public void UpdateResultByName(string name, HttpResponseMessage result)
        {
            _nameResult[name] = result;
        }

        /// <summary>
        /// Destroys the current instance.
        /// </summary>
        public static void Reset()
        {
            _instance = null;
        }
    }
}