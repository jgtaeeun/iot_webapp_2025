using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class News
    {
        [Key]  //pk
        public int Id { get; set; }
        public string Writer { get; set; }
        [Required] //not null
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PostdDate { get; set; }

        public int ReadCount { get; set; }

       
    }
}
