using Microsoft.AspNetCore.Mvc;
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
        public string Writer { get; set; }



        [Required] //not null
        [DisplayName("뉴스제목")]
        public string Title { get; set; }



        [DisplayName("뉴스내용")]
        public string Description { get; set; }


        [DisplayName("작성일")]
        [DisplayFormat(DataFormatString ="{0:yyyy년 MM월 dd일}", ApplyFormatInEditMode = false)]
        public DateTime PostdDate { get; set; }



        [DisplayName("조회수")]
        public int ReadCount { get; set; }

       
    }
}
