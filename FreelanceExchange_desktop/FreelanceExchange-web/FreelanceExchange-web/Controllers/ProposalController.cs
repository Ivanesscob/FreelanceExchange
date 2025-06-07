using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_desktop.Data;
using System.Linq;

namespace FreelanceExchange_web.Controllers
{
    public class ProposalController : Controller
    {
        public IActionResult Index()
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Если пользователь - заказчик, показываем его задания
            if (DataClass.CurrentUser.Roles.First() == "customer")
            {
                var userTasks = DataClass.Tasks.Where(t => t.CreatorId == DataClass.CurrentUser.Id).ToList();
                ViewBag.Tasks = userTasks;
            }
            // Для исполнителя пока пустая страница
            else
            {
                ViewBag.Tasks = new List<FreelanceExchange_desktop.Data.Task>();
            }

            return View();
        }

        public IActionResult Create(int taskId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int taskId, string message, decimal price)
        {
            if (string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("", "Введите сообщение");
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
} 