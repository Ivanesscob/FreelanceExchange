using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_desktop.Data;

namespace FreelanceExchange_web.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Проверяем роль пользователя
            if (DataClass.CurrentUser.Roles.First() != "customer")
            {
                return RedirectToAction("List");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string description, decimal budget, DateTime deadline, string category)
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Проверяем роль пользователя
            if (DataClass.CurrentUser.Roles.First() != "customer")
            {
                return RedirectToAction("List");
            }

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                ModelState.AddModelError("", "Заполните все обязательные поля");
                return View();
            }

            // TODO: Добавить создание задания в базу данных

            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            var task = DataClass.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Task = task;
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult MyTasks()
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
} 