using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AccountAPI2.Models
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [BindNever]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Email address is invalid")]
        [Required]
        public string Email { get; set; }
        
        [AllowedValues("ADMIN", "USER", ErrorMessage = "Only admin or user role allowed")]
        [Required]
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Today;
        public DateTime UodatedAr { get; set; } = DateTime.Today;
    }
}
