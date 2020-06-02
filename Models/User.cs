using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_CinemaV7.Models
{
    public class User
    {
        public int Id { get; set; }//Ключ
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }//Логин
        [Display(Name = "Имя")]
        [StringLength(40)]
        public string user_name { get; set; }//Имя
        [Display(Name = "Фамилия")]
        [StringLength(50)]
        public string user_surname { get; set; }//Фамилия
        [Display(Name = "Отчество")]
        [StringLength(50)]
        public string user_otc { get; set; }//Отчество
        //public int roleId { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }//Пароль
        [Display(Name = "Возраст")]
        [Range(0,100, ErrorMessage ="Недопустимый возраст")]
        public int Age { get; set; }//Возраст
      
        public int RoleId { get; set; }//Ключ роли
        public Role Role { get; set; }//Ссылка на роли
        public  ICollection<places> places { get; set; }//Коллекция мест
        public User()
        {
            places = new List<places>();
        }
    }
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }//Ключ
        [Display(Name="Название роли")]
        public string Name { get; set; }//Название роли
       
    }
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

    }

    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string user_name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string surname { get; set; }

        [Display(Name = "Отчество")]
        public string otc { get; set; }
        
        [Required]
        [Display(Name = "Возраст")]
        [Range(1, 150, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Подтвердить пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
    
    public class ChangePassModel
    {
        [Required]
        [Display(Name = "Текущий пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string Password_new { get; set; }
        [Required]
        [Display(Name = "Подтвердить пароль")]
        [DataType(DataType.Password)]
        [Compare("Password_new", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}