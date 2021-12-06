using System.Collections.Generic;
using System.Linq;

namespace LearningMaterials.Models
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaterialCount { 
            get 
            {
                return Materials.Count();
            }
        }
        public IEnumerable<MaterialReadDto> Materials { get; set; }
    }
}