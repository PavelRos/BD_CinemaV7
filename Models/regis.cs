using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class regis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reg_Code { get; set; }//Код режиссёра
        [StringLength(50)]
        public string regisseur { get; set; }//Режиссёр
    }
}