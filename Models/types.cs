using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class types
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HallCode { get; set; }//Код типа
        [StringLength(30)]
        public string Hall_Type { get; set; }//Название типа

    }
}