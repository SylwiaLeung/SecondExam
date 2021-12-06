using LearningMaterials.Data;
using LearningMaterials.Entities;
using LearningMaterials.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningMaterials
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MaterialsDbContext _context;

        public ReviewRepository(MaterialsDbContext context)
        {
            _context = context;
        }

        public async Task Create(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));

            await _context.Reviews.AddAsync(review);
        }

        public void Delete(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));

            _context.Remove(review);
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            var reviews = await _context
                .Reviews
                .Include(r => r.Material)
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetSingle(int id)
        {
            var review = await _context
                .Reviews
                .Include(r => r.Material)
                .FirstOrDefaultAsync(r => r.Id == id);

            return review;
        }

        public void Update(Review review)
        {
            _context.Update(review);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}