using System.Threading.Tasks;
using Core.Http.Reponse.ElasticSearch;

namespace Core.ElasticSearch.Contracts.Interfaces
{
    public interface IElasticContext<T> where T : class
    {   
        Task<IndexResponseDTO> CreateIndexAsync(string indexName, string aliasName);
        Task<IndexResponseDTO> IndexAsync(string indexName, T model);
    }
}