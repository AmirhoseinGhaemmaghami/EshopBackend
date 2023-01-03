using System.ComponentModel.DataAnnotations;

namespace EshopBackend.Shared.Dtos.Account
{
    public class RegisterInputDto
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }
    }
}
