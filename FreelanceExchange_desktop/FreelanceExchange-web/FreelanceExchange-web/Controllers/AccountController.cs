using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_web.Models;
using FreelanceExchange_desktop.Data;
using Mysqlx.Notice;

namespace FreelanceExchange_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly Dictionary<string, string> _roles = new Dictionary<string, string>
        {
            { UserRole.Customer, "Заказчик" },
            { UserRole.Freelancer, "Исполнитель" }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Введите логин и пароль");
                return View();
            }
            else
            {
                var matchedUser = DataClass.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (matchedUser != null)
                {
                    DataClass.CurrentUser = matchedUser;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View();
                }
            }
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _roles;
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password, string confirmPassword, 
            string firstName, string lastName, string username, DateTime birthDate, string roleName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || 
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(firstName) || 
                string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("", "Все поля обязательны для заполнения");
                ViewBag.Roles = _roles;
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают");
                ViewBag.Roles = _roles;
                return View();
            }

            if (birthDate == default(DateTime))
            {
                ModelState.AddModelError("", "Выберите дату рождения");
                ViewBag.Roles = _roles;
                return View();
            }

            // Проверка возраста (должно быть не менее 18 лет)
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            if (age < 18)
            {
                ModelState.AddModelError("", "Вам должно быть не менее 18 лет");
                ViewBag.Roles = _roles;
                return View();
            }

            if (roleName != UserRole.Customer && roleName != UserRole.Freelancer)
            {
                ModelState.AddModelError("", "Выберите корректную роль");
                ViewBag.Roles = _roles;
                return View();
            }

            // Проверка формата username
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$"))
            {
                ModelState.AddModelError("", "Имя пользователя должно содержать от 3 до 20 символов (буквы, цифры и знак подчеркивания)");
                ViewBag.Roles = _roles;
                return View();
            }

            // Создаем нового пользователя
            var newUser = new FreelanceExchange_desktop.Data.User
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                BirthDate = birthDate,
            };
            newUser.Roles.Add(roleName);

            DatabaseCommands.AddUser(newUser, roleName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            DataClass.CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }
    }
} 