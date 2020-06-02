using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryCode { get; set; }//Код страны
        [StringLength(50)]
        public string Country_Name { get; set; }//Название страны
        [NotMapped]
        public string Country_Name_Full
        {
            get { return Country_Name.Trim(); }
        }
    }
}