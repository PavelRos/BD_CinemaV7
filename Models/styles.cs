using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class styles
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Style_Code { get; set; }//Код жанра
        public string film_style { get; set; }//Жанр
    }
}