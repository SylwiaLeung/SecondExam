using System.Collections.Generic;

namespace LearningMaterials.Models
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetSingle(int id);
        void Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        void PartialUpdate(T obj);
        void Save();
    }
}