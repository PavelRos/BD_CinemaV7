using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class ages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age_Code { get; set; }//Код рейтинга
        [StringLength(50)]
        public string Age_Rating { get; set; }//Возрастной рейтинг
        [NotMapped]
        public string Age_Rating_Full
        {
            get { return Age_Rating.Trim(); }
        }
    }
}