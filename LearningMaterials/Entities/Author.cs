using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LearningMaterials.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string Description { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public int MaterialCount {
            get
            {
                return Materials.Count();
            }
        }
    }
}
