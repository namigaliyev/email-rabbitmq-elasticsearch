using System.Threading.Tasks;
using Core.Http.Reponse.ElasticSearch;
using Service.Interfaces;
using Core.AppSettings;
using Microsoft.Extensions.Options;
using System;
using Nest;
using Core.ElasticSearch.Common;
using Core.ElasticSearch.Contracts;

namespace Service
{
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
    {
        private readonly IOptions<ElasticSearchSettings> elasticSearchSettings;
        private ElasticClient elasticClient;
        private ElasticContext<T> elasticContext;
        private string indexName;
        private string aliasName;
        public ElasticSearchService(IOptions<ElasticSearchSettings> elasticSearchSettings)
        {
            this.elasticSearchSettings = elasticSearchSettings;

            indexName = string.Format("{0}_{1}", elasticSearchSettings.Value.IndexName,
                                         DateTime.Now.ToString("yyyyMMddHHss"));

            aliasName = elasticSearchSettings.Value.IndexName;
            
            elasticClient = new ElasticClient(ElasticHelper.Instance.GetConnectionSettings(elasticSearchSettings.Value.URI));

            elasticContext = new ElasticContext<T>(elasticClient);
        }

        public async Task<IndexResponseDTO> CreateAsync()
        {
            return await elasticContext.CreateIndexAsync(indexName, aliasName);
        }

        public async Task<IndexResponseDTO> IndexAsync(T model)
        {
            return await elasticContext.IndexAsync(indexName, model);
        }
    }
}