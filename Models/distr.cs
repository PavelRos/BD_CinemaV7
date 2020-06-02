using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class distr
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dis_Code { get; set; }//Код дистрибьютора
        public string distributor { get; set; }//Дистрибьютор
    }
}