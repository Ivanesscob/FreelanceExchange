using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_desktop.Data;

namespace FreelanceExchange_web.Controllers
{
    public class ResponseController : Controller
    {
        public IActionResult Create(int taskId)
        {
            // Проверяем, авторизован ли пользователь
            if (DataClass.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Проверяем роль пользователя
            if (DataClass.CurrentUser.Roles.First() != "freelancer")
            {
                return RedirectToAction("Index", "Home");
            }

            var task = DataClass.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Task = task;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int taskId, string message, decimal proposedPrice)
        {
            Response response = new Response
            {
                TaskId = taskId,
                FreelancerId = DataClass.CurrentUser.Id,
                Message = message,
                ProposedPrice = proposedPrice,
                CreatedAt = DateTime.Now,
                IsSelected = false
            };
            try
            {
                DatabaseCommands.InsertResponse(response);
            }
            catch
            {
            }
            return RedirectToAction("Index", "Home");
        }
    }
} 