﻿using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class ContactModel
    {
        
        public string? Name { get; set; }

        public string? Email { get; set; }

     
        public string? Subject { get; set; }

      
        public string? Message { get; set; }    
    }
}
