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
using Microsoft.AspNetCore.Authorization;
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
                User user = await _context.Users.Include(w => w.Role).FirstOrDefaultAsync(u => u.EmailAddress == model.Login && u.Password == model.Password);
                if (user == null)
                    user = await _context.Users.Include(w => w.Role).FirstOrDefaultAsync(u => u.UserName == model.Login && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Quizzes");
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
                User user = await _context.Users.Include(w => w.Role).FirstOrDefaultAsync(u => u.EmailAddress == model.EmailAddress);
                if (user != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким e-mail уже существует!");
                    return View(model);
                }
                user = await _context.Users.Include(w => w.Role).FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
                    return View(model);
                }
                // добавляем пользователя в бд

                Role role;
                if (_context.Users.Count() == 0)
                    role = await _context.Roles.FirstOrDefaultAsync(u => u.Name == "admin");
                else
                    role = await _context.Roles.FirstOrDefaultAsync(u => u.Name == "user");

                user = new User
                {
                    EmailAddress = model.EmailAddress,
                    Password = model.Password,
                    UserName = model.UserName,
                    Role = role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await Authenticate(user); // аутентификация

                return RedirectToAction("Index", "Quizzes");
            }
            else
            {
                ModelState.AddModelError("", "Вы пропустили какое-то поле или пароли не совпадают!");
            }
            return View(model);
        }

        public async Task<IActionResult> List()
        {
            List<User> Users = await _context.Users.Include(w => w.Role).ToListAsync();
            Users.ForEach(w => w.Password = "");
            return View(Users);
        }
        
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Admin(int? id)
        {
            var user = await _context.Users.Include(w => w.Role).FirstOrDefaultAsync(w => w.UserID == id);
            if (user == null) return NotFound();
            user.Role = await _context.Roles.FirstOrDefaultAsync(w => w.Name == "admin");
            _context.Update(user);
            _context.SaveChanges();
            return View();
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
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
