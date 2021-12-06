using LearningMaterials.Entities;
using LearningMaterials.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials
{
    public class MaterialRepository : IMaterialRepository
    {
        public Task Create(Material obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Material obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Material>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Material> GetSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Material obj)
        {
            throw new System.NotImplementedException();
        }
    }
}