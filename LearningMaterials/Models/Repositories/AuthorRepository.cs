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

        public Task Delete(Author obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context
                .Authors
                .Include(a => a.Materials)
                .ToListAsync();

            return authors;
        }

        public async Task<Author> GetSingle(int id)
        {
            var author = await _context
                .Authors
                .Include(a => a.Materials)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public Task PartialUpdate(Author obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Author obj)
        {
            throw new System.NotImplementedException();
        }
    }
}