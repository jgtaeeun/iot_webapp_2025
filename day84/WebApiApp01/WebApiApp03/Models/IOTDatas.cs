using System.ComponentModel.DataAnnotations;

namespace WebApiApp03.Models
{
    public class IOTDatas
    {
        [Key]
        public int Id { get; set; } 
    
        public DateOnly Sensing_dt { get; set; }
        public string Loc_id { get; set; }
        public float Temp { get; set; }
        public float Humid {  get; set; }   
    }
}
