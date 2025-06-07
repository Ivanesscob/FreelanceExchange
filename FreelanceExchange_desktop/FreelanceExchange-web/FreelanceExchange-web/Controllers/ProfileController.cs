using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_desktop.Data;

namespace FreelanceExchange_web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(string firstName, string lastName, string bio, string skills)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                ModelState.AddModelError("", "Имя и фамилия обязательны для заполнения");
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult MyTasks()
        {
            return View();
        }
    }
} 