using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Models
{
    public class AuthorCreateDto
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string Description { get; set; }
    }
}
