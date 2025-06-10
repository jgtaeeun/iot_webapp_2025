﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiApp01.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string  Title { get; set; }

        [Required]
        [Column(TypeName = "CHAR(8)")]
        public string TodoDate { get; set; }

       
        public Boolean IsComplete { get; set; }
    }
}
