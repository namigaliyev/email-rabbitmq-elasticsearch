using System;

namespace Core.Http.Reponse.ElasticSearch
{
    public class IndexResponseDTO
    {
        public bool IsValid { get; set; }
        public string StatusMessage { get; set; }
        public Exception Exception { get; set; }
    }
}