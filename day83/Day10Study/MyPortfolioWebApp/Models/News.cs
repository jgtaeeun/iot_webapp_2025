using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class News
    {
        [Key]  //pk

        [DisplayName("번호")]
        public int Id { get; set; }



        [DisplayName("작성자")]
        [BindNever]
        public string? Writer { get; set; }


        [DisplayName("뉴스제목")]
        [Required(ErrorMessage ="{0}은 필수입니다.")] //not null ,{0}은 DisplayName으로 설정된 값을 자동으로 대체합니다.
        public string Title { get; set; }



        [DisplayName("뉴스내용")]
        [Required(ErrorMessage = "{0}은 필수입니다.")] //not null
        public string Description { get; set; }


        [DisplayName("작성일")]
        [BindNever]
        [DisplayFormat(DataFormatString ="{0:yyyy년 MM월 dd일}", ApplyFormatInEditMode = false)]
        public DateTime PostdDate { get; set; }



        [DisplayName("조회수")]
        [BindNever]
        public int ReadCount { get; set; }

       
    }
}
