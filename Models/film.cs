using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BD_CinemaV7.Models
{
    public class film
    {
        [Key]
        public int Id { get; set; }//ID фильма
        [StringLength(30)]
        [Display(Name = "Страна")]
        public string Country { get; set; }//Страна
        [Display(Name = "Бюджет фильма")]
        public int budget { get; set; }//Бюджет фильма
        [Display(Name = "Жанр фильма")]
        public string style { get; set; }//Жанр фильма
        
        [Display(Name = "Дата выхода")]
        [DataType(DataType.Date)]
        public string release_date { get; set; }//Дата выхода
        [StringLength(30)]
        [Display(Name = "Возрастной рейтинг")]
        public string age_rating { get; set; }//Возрастной рейтинг
        [Display(Name = "Длительность в минутах")]
        [Range(0, 300, ErrorMessage = "Недопустимая длительность фильма")]
        public int duration { get; set; }//Длительность в минутах
        [Display(Name = "Дистрибьютор")]
        public string distributor { get; set; }//Дистрибьютор
        [Display(Name = "Режиссёр")]
        [StringLength(40)]
        public string regisseur { get; set; }//Режиссёр
        [StringLength(40)]
        [Display(Name = "Название фильма")]
        public string film_name { get; set; }//Название фильма
        public ICollection<sessions> Sessions { get; set; }//Коллекция сеансов
        public film()
        {
            Sessions = new List<sessions>();
        }
        public country theCountry { get; set; }//Ссылка на таблицу стран
        public ages the_rating { get; set; }//Ссылка на таблицу возрастных рейтингов
        public regis theRegis { get; set; }//Ссылка на таблицу режиссёров
        public styles theStyle { get; set; }//Ссылка на таблицу жанров
        public distr theDistr { get; set; }//Ссылка на таблицу дистрибьютеров
    }
}