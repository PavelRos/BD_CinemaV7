using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class sessions
    {
        [Key]
        public int Id { get; set; }// ID сеанса
        [StringLength(30)]
        [Display(Name = "Дата сеанса")]
        [DataType(DataType.Date)]
        public string date_of_session { get; set; }//Дата сеанса
        [StringLength(30)]
        [Display(Name = "Время сеанса")]
        [DataType(DataType.Time)]
        public string time_of_session { get; set; }//Время сеанса
        public int hallId { get; set; }//ID зала
        public hall hall { get; set; }//Ссылка на зал
        public int filmId { get; set; }//ID фильма
        public film film { get; set; }//Ссылка на фильм
        [Display(Name = "Цена билетов, руб")]
        [Range(0, 100000, ErrorMessage = "Недопустимая цена билетов")]
        public float price_of_tickets { get; set; }//Цена билетов
        public ICollection<places> Places { get; set; }//Коллекция мест
        public sessions()
        {
            Places = new List<places>();
        }
    }
}