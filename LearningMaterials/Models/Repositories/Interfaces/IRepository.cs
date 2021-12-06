using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(int id);
        Task Create(T obj);
        Task Update(T obj);
        Task Delete(T obj);
        Task PartialUpdate(T obj);
        Task Save();
    }
}