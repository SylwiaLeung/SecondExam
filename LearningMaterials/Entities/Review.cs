using System.Collections.Generic;

namespace LearningMaterials.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public string WrittenReview { get; set; }
        public int Score { get; set; }

    }
}
