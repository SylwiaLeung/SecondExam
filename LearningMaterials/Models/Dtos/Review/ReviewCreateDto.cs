using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Models
{
    public class ReviewCreateDto
    {
        [Required]
        public int MaterialId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string WrittenReview { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Please insert a numerical value from 0 to 10")]
        public int Score { get; set; }
    }
}
