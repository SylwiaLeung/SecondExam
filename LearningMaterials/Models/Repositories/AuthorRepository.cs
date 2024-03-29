using LearningMaterials.Data;
using LearningMaterials.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MaterialsDbContext _context;

        public AuthorRepository(MaterialsDbContext context)
        {
            _context = context;
        }

        public async Task Create(Author author)
        {
            if (author is null) throw new ArgumentNullException(nameof(author));

            await _context.Authors.AddAsync(author);
        }

        public void Delete(Author author)
        {
            if (author is null) throw new ArgumentNullException(nameof(author));

            _context.Remove(author);
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context
                .Authors
                .Include(a => a.Materials)
                .ThenInclude(m => m.MaterialType)
                .Include(a => a.Materials)
                .ThenInclude(m => m.Reviews)
                .ToListAsync();

            return authors;
        }

        public async Task<Author> GetSingle(int id)
        {
            var author = await _context
                .Authors
                .Include(a => a.Materials)
                .ThenInclude(m => m.MaterialType)
                .Include(a => a.Materials)
                .ThenInclude(m => m.Reviews)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Author author)
        {
            _context.Update(author);
        }
    }
}