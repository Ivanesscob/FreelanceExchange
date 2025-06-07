using Microsoft.AspNetCore.Mvc;

namespace FreelanceExchange_web.Controllers
{
    public class ProposalController : Controller
    {
        public IActionResult Index()
        {
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