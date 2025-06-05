using Microsoft.AspNetCore.Identity;

namespace MyPortfolioWebApp.Models
{
    public class CustomUser : IdentityUser
    {
        //회원가입시 추가로 받고 싶은 정보
        public string? City { get; set; }
        public string? Hobby {  get; set; }
        public string? Mobile { get; set; } 

    }
}
