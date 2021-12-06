using LearningMaterials.Entities;
using System.Collections.Generic;

namespace LearningMaterials.Models
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaterialCount { get; }
        public IEnumerable<MaterialReadDto> Materials { get; set; }
    }
}