using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "{0}은 필수입니다.")]
        [EmailAddress]
        
        public string Email { get; set; }


        [Required(ErrorMessage = "{0}은 필수입니다.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }


        [Required(ErrorMessage = "{0}은 필수입니다.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage ="Password and confirmation password don't match.")]
        public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }

        public string? City {  get; set; }
        public string? Mobile {  get; set; }
        public string? Hobby { get; set; }  

    }
}
