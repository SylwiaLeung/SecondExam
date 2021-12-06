using System.Collections.Generic;

namespace LearningMaterials.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public int MaterialCount { get; set; }
    }
}
