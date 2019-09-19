using System.Threading.Tasks;
using Core.Http.Reponse.ElasticSearch;

namespace Service.Interfaces
{
    public interface IElasticSearchService<T> where T : class
    {
        Task<IndexResponseDTO> CreateAsync();
        Task<IndexResponseDTO> IndexAsync(T model);
    }   
}