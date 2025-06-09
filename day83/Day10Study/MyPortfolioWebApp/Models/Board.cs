using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models;

public partial class Board
{
    [Key]
    [DisplayName("번호")]
    public int Id { get; set; }

    public string? Email { get; set; }

    [DisplayName("작성자")]
    public string? Writer { get; set; }

    [Required]
    [DisplayName("게시글 제목")]
    public string? Title { get; set; } 

    [Required]
    public string? Contents { get; set; }

    [DisplayName("작성일")]
    [DisplayFormat(DataFormatString = "{0:yyyy년 MM월 dd일}", ApplyFormatInEditMode = false)]
    public DateTime? PostDate { get; set; }

    [DisplayName("조회수")]
    public int? ReadCount { get; set; }
}
