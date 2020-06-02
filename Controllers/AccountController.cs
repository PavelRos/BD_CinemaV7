using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BD_CinemaV7.Models;
using BD_CinemaV7.Context;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Net;


namespace BD_CinemaV7.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private MsSqlContext db = new MsSqlContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                if (ModelState.IsValid)
                {

                    // поиск пользователя в бд
                    User user = null;

                    byte[] bytes = Encoding.Unicode.GetBytes(model.Password);
                    MD5CryptoServiceProvider CSP =
                        new MD5CryptoServiceProvider();

                    byte[] byteHash = CSP.ComputeHash(bytes);
                    model.Password = string.Empty;
                    foreach (byte b in byteHash)
                        model.Password += string.Format("{0:x2}", b);
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Details");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }
                else
                {
                    return RedirectToAction("Details");///////////////// 
                }

            }

            return View(model);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassModel model)
        {
            if (ModelState.IsValid)
            {
                User user;
                user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    if (user.Email == User.Identity.Name)
                    {
                        using (MsSqlContext db = new MsSqlContext())
                        {
                            byte[] bytes = Encoding.Unicode.GetBytes(model.Password);
                            MD5CryptoServiceProvider CSP =
                                new MD5CryptoServiceProvider();

                            byte[] byteHash = CSP.ComputeHash(bytes);
                            model.Password = string.Empty;
                            foreach (byte b in byteHash)
                                model.Password += string.Format("{0:x2}", b);
                            if (model.Password == user.Password)
                            {
                                byte[] bytesnew = Encoding.Unicode.GetBytes(model.Password_new);
                                MD5CryptoServiceProvider CSPnew =
                                    new MD5CryptoServiceProvider();

                                byte[] byteHashnew = CSPnew.ComputeHash(bytesnew);
                                model.Password_new = string.Empty;
                                foreach (byte b in byteHashnew)
                                    model.Password_new += string.Format("{0:x2}", b);

                                byte[] bytesconf = Encoding.Unicode.GetBytes(model.ConfirmPassword);
                                MD5CryptoServiceProvider CSPconf =
                                    new MD5CryptoServiceProvider();

                                byte[] byteHashconf = CSP.ComputeHash(bytesconf);
                                model.ConfirmPassword = string.Empty;
                                foreach (byte b in byteHashconf)
                                    model.ConfirmPassword += string.Format("{0:x2}", b);
                                if (model.ConfirmPassword == model.Password_new)
                                {
                                    user.Password = model.ConfirmPassword;
                                    db.Entry(user).State = EntityState.Modified;
                                    db.SaveChanges();
                                    return RedirectToAction("Details");
                                }

                            }
                            else
                            {
                                ModelState.AddModelError("Password", "Неверный пароль");
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Details");
                    }
                    

                    }
                }
            else
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                if (ModelState.IsValid)
                {

                    User user = null;
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                    if (user == null)
                    {
                        // создаем нового пользователя
                        byte[] bytes = Encoding.Unicode.GetBytes(model.Password);
                        MD5CryptoServiceProvider CSP =
                            new MD5CryptoServiceProvider();
                        byte[] byteHash = CSP.ComputeHash(bytes);
                        model.Password = string.Empty;
                        foreach (byte b in byteHash)
                            model.Password += string.Format("{0:x2}", b);
                        Role role = db.Roles.FirstOrDefault(c => c.Id == 1);
                        model.RoleId = 1;
                        db.Users.Add(new User { Email = model.Name, user_name = model.user_name, user_surname = model.surname, user_otc = model.otc, Password = model.Password, Age = model.Age, RoleId = model.RoleId });

                        db.SaveChanges();
                        user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                        if (user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Name, true);
                            return RedirectToAction("Details");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }
               

                return View(model);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            var user1 = db.Users.FirstOrDefault(c => c.Email == User.Identity.Name);
            id = user1.Id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Email,Password, user_surname, user_name,user_otc, Age")] User user)
        {
            if (User.Identity.IsAuthenticated==true)
            {
                if (ModelState.IsValid)
                {
                    using(MsSqlContext dd=new MsSqlContext())
                    {
                        var users = dd.Users.FirstOrDefault(c => c.Email == User.Identity.Name);
                        user.Password = users.Password;
                    }
                    user.Email = User.Identity.Name;
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details");
                }
            }
            else
            {
                return RedirectToAction("Login");

            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "halls");
        }
    }
}