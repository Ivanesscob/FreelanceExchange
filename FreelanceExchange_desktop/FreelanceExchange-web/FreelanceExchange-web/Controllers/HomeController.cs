using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FreelanceExchange_web.Models;
using FreelanceExchange_desktop.Data;

namespace FreelanceExchange_web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        DataClass.Tasks = DatabaseCommands.LoadTasksFromDb();
        DataClass.Responses = DatabaseCommands.LoadResponsesFromDb();
        DataClass.Users = DatabaseCommands.GetUsers();
        ViewBag.Tasks = DataClass.Tasks;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
