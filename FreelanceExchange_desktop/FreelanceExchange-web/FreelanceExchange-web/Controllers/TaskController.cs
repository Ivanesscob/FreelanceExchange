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
        public IActionResult Create(string title, string description, decimal budget)
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

            FreelanceExchange_desktop.Data.Task newTask = new FreelanceExchange_desktop.Data.Task();
            newTask.CreatedAt = DateTime.Now;
            newTask.CreatorId = DataClass.CurrentUser.Id;
            newTask.StatusId = 1;
            newTask.Title = title;
            newTask.Description = description;
            newTask.Budget = budget;
            DatabaseCommands.AddTaskToDatabase(newTask);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            var task = DataClass.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            // Проверяем, является ли текущий пользователь заказчиком этого задания
            bool isTaskOwner = DataClass.CurrentUser != null && 
                              DataClass.CurrentUser.Id == task.CreatorId;

            // Если пользователь не авторизован или не является заказчиком, очищаем список откликов
            if (!isTaskOwner)
            {
                task.Responses = new List<Response>();
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

        [HttpPost]
        public IActionResult AcceptResponse(int responseId, int taskId)
        {
            var task = DataClass.Tasks.FirstOrDefault(t => t.Id == taskId);
            var response = task?.Responses.FirstOrDefault(r => r.Id == responseId);

            DatabaseCommands.SelectResponseForTask(task.Responses, response, task);

            return RedirectToAction("Index", "Home");
        }
    }
}