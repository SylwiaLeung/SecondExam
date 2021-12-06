using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Models
{
    public class MaterialCreateDto
    {
        [Required]
        public int AuthorId { get; set; }

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
    }
}
