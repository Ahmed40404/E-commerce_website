using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,StringLength(50)]
        public string Name { get; set; }
        [Required,StringLength(80)]
        public string Email { get; set; }
        [Required,StringLength(50)]
        public string Password { get; set; }
        [Required,StringLength(20)]
        public string Role { get; set; }  // User and Admin
    }
}
