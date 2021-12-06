using System;
using System.Collections.Generic;

namespace LearningMaterials.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MaterialTypeId { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
