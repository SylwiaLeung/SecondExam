using System.ComponentModel.DataAnnotations;

namespace LearningMaterials.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}