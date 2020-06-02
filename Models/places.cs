using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class places
    {
        [Key]
        public int Id { get; set; }//ID места
        public int sessionsId { get; set; }//ID сеанса
        public sessions sessions { get; set; }//Ссылка на сеанс
        [Display(Name = "Номер ряда")]
        public int number_of_row { get; set; }//Номер ряда
        [Display(Name = "Номер места")]
        public int number_of_seat_in_a_row { get; set; }//Номер места
        [StringLength(30)]
        [Display(Name = "Статус места")]
        public string status { get; set; }//Статус мест
        [Display(Name = "Дата операции")]
        public DateTime? date_of_operation { get; set; }//Дата операции
        public int?  UserId { get; set; }//Пользователь
        public User User { get; set; }//Ссылка на таблицу пользователей
       
    }
}