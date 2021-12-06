using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string WrittenReview { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Please insert a numerical value from 0 to 10")]
        public int Score { get; set; }
    }
}
