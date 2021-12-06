using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSingle(int id);
        Task Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        Task SaveAsync();
    }
}