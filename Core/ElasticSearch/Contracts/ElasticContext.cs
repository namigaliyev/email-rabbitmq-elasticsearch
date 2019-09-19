using System.Threading.Tasks;
using Core.ElasticSearch.Contracts.Interfaces;
using Core.Http.Reponse.ElasticSearch;
using Nest;

namespace Core.ElasticSearch.Contracts
{
    public class ElasticContext<T> : IElasticContext<T> where T : class
    {
        private readonly ElasticClient elasticClient;

        public ElasticContext(ElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        
        public async Task<IndexResponseDTO> CreateIndexAsync(string indexName, string aliasName)
        {
            CreateIndexDescriptor createIndexDescriptor = new CreateIndexDescriptor(indexName)
                    .Mappings(ms => ms
                    .Map<T>(m => m.AutoMap())
                     )
                    .Aliases(a => a.Alias(aliasName));
 
            ICreateIndexResponse response = await elasticClient.CreateIndexAsync(createIndexDescriptor);
 
            return new IndexResponseDTO()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public async Task<IndexResponseDTO> IndexAsync(string indexName, T model)
        {
            IIndexResponse response = await elasticClient.IndexAsync(model, i => i
                    .Index(indexName)
                    .Type<T>());

            return new IndexResponseDTO()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }
    }
}