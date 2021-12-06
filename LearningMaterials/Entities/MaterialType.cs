using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Entities
{
    public class MaterialType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length of this field cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length of this field cannot exceed 100 characters")]
        public string Definition { get; set; }
    }
}