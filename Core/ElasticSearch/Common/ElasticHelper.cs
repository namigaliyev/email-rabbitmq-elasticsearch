using System;
using Nest;

namespace Core.ElasticSearch.Common
{
    public class ElasticHelper
    {
        private static readonly Lazy<ElasticHelper> instance = new Lazy<ElasticHelper>(() => new ElasticHelper());
 
        private ElasticHelper()
        {
 
        }
 
        public static ElasticHelper Instance
        {
            get
            {
                return instance.Value;
            }
        }
 
        public ConnectionSettings GetConnectionSettings(string URI)
        {
            var connectionSettings = new ConnectionSettings(new Uri(URI));
 
            return connectionSettings;
        }
    }
}