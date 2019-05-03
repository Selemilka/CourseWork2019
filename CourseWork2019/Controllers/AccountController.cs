using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork2019.Data;
using CourseWork2019.Models;
using CourseWork2019.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseWork2019.Controllers
{
    public class AccountController : Controller
    {
        private QuizContext _context;

        public AccountController(QuizContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == model.Login && u.Password == model.Password);
                if (user == null)
                    user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(user.EmailAddress); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == model.EmailAddress);
                if (user != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким e-mail уже существует!");
                    return View(model);
                }
                user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
                    return View(model);
                }
                // добавляем пользователя в бд
                _context.Users.Add(new User
                {
                    EmailAddress = model.EmailAddress,
                    Password = model.Password,
                    UserName = model.UserName
                });
                await _context.SaveChangesAsync();

                await Authenticate(model.EmailAddress); // аутентификация

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Вы пропустили какое-то поле или пароли не совпадают!");
            }
            return View(model);
        }


        private async Task Authenticate(string emailAddress)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, emailAddress)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}