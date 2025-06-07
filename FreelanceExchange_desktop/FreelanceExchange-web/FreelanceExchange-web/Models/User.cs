using System.ComponentModel.DataAnnotations;

namespace FreelanceExchange_web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string? Bio { get; set; }

        public string? Skills { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
} 