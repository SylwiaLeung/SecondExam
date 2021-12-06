using LearningMaterials.Data;
using LearningMaterials.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public class MaterialTypeRepository : IMaterialTypeRepository
    {
        private readonly MaterialsDbContext _context;

        public MaterialTypeRepository(MaterialsDbContext context)
        {
            _context = context;
        }

        public Task Create(MaterialType materialType)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(MaterialType materialType)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MaterialType>> GetAll()
        {
            var materialTypes = await _context
                .MaterialTypes
                .ToListAsync();

            return materialTypes;
        }

        public async Task<MaterialType> GetSingle(int id)
        {
            var materialType = await _context
                .MaterialTypes
                .FirstOrDefaultAsync(mt => mt.Id == id);

            return materialType;
        }

        public void Update(MaterialType materialType)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}