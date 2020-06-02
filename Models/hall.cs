using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class hall
    {
        [Key]
        public int Id { get; set; }//ID зала
        [StringLength(15)]
        [Display(Name = "Название зала")]
        public string hall_name { get; set; }//Название зала
        [StringLength(30)]
        [Display(Name = "Тип зала")]
        public string type { get; set; }//Тип зала
        [Display(Name = "Количество рядов")]
        [Range(0, 30, ErrorMessage = "Недопустимое количество рядов")]
        public int number_of_rows { get; set; }//Количество рядов
        [Display(Name = "Количество мест в ряду")]
        [Range(0, 30, ErrorMessage = "Недопустимое количество мест в ряду")]
        public int number_of_seats_in_a_row { get; set; }//Количество мест в ряду
        public types theHall_Type { get; set; }//Ссылка на таблицу типов зала

        public ICollection<sessions> sessions { get; set; }//Коллекция сеансов
        public hall()
        {
            sessions = new List<sessions>();
        }
    }
}