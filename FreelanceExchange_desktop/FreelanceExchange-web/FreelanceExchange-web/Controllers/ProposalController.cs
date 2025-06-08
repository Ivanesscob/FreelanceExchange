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

            ViewBag.IsCustomer = DataClass.CurrentUser.Roles.First() == "customer";

            // Если пользователь - заказчик, показываем его задания
            if (ViewBag.IsCustomer)
            {
                var userTasks = DataClass.Tasks.Where(t => t.CreatorId == DataClass.CurrentUser.Id).ToList();
                ViewBag.Tasks = userTasks;
            }
            // Для исполнителя показываем его отклики
            else
            {
                var userResponses = DataClass.Tasks
                    .SelectMany(t => t.Responses)
                    .Where(r => r.FreelancerId == DataClass.CurrentUser.Id)
                    .ToList();
                ViewBag.Responses = userResponses;
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

        [HttpPost]
        public IActionResult Delete(FreelanceExchange_desktop.Data.Task task)
        {
            DatabaseCommands.DeleteTaskFromDb(task);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteResponse(int responseId)
        {
            var response = DataClass.Tasks
                .SelectMany(t => t.Responses)
                .FirstOrDefault(r => r.Id == responseId);

            if (response != null && !response.IsSelected)
            {
                DatabaseCommands.DeleteResponse(response);
            }

            return RedirectToAction("Index", "Home");
        }
    }
} 