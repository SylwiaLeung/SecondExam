using LearningMaterials.Data;
using LearningMaterials.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LearningMaterials.Models
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MaterialsDbContext _context;

        public AuthorRepository(MaterialsDbContext context)
        {
            _context = context;
        }

        public void Create(Author obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Author obj)
        {
            throw new System.NotImplementedException();
        }

        public async IEnumerable<Author> GetAll()
        {
            var authors = await _context
                .Authors
                .Include(a => a.Materials)
                .ToListAsync();

            return authors;
        }

        public Author GetSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public void PartialUpdate(Author obj)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Author obj)
        {
            throw new System.NotImplementedException();
        }
    }
}