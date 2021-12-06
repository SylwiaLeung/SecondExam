using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Entities
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length of this field cannot exceed 50 characters")]
        public string Title { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int MaterialTypeId { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;
    }
}
