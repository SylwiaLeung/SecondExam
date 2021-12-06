using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Models
{
    public class MaterialReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string MaterialTypeName { get; set; }
        //public IEnumerable<ReviewReadDto> Reviews { get; set; }
        public string AuthorName { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
    }
}
