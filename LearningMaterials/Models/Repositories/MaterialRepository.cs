using LearningMaterials.Data;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly MaterialsDbContext _context;

        public MaterialRepository(MaterialsDbContext context)
        {
            _context = context;
        }

        public async Task Create(Material material)
        {
            if (material is null) throw new ArgumentNullException(nameof(material));

            await _context.Materials.AddAsync(material);
        }

        public void Delete(Material materialType)
        {
            if (materialType is null) throw new ArgumentNullException(nameof(materialType));

            _context.Remove(materialType);
        }

        public async Task<IEnumerable<Material>> GetAll()
        {
            var materials = await _context
                .Materials
                .Include(m => m.Author)
                .Include(m => m.MaterialType)
                .ToListAsync();

            return materials;
        }

        public async Task<Material> GetSingle(int id)
        {
            var material = await _context
                .Materials
                .Include(m => m.Author)
                .Include(m => m.MaterialType)
                .FirstOrDefaultAsync(m => m.Id == id);

            return material;
        }

        public void Update(Material material)
        {
            _context.Update(material);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}