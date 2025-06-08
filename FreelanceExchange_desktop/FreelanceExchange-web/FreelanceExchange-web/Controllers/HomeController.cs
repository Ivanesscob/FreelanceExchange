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

    public IActionResult Index(string status = null, string searchQuery = null, string sortBy = "date")
    {
        DataClass.Tasks = DatabaseCommands.LoadTasksFromDb();
        DataClass.Responses = DatabaseCommands.LoadResponsesFromDb();
        DataClass.Users = DatabaseCommands.GetUsers();

        // Фильтрация по статусу, поиску и сортировка
        var filteredTasks = DataClass.Tasks;
        
        // Применяем поиск по названию, если указан
        if (!string.IsNullOrEmpty(searchQuery))
        {
            filteredTasks = new System.Collections.ObjectModel.ObservableCollection<FreelanceExchange_desktop.Data.Task>(
                filteredTasks.Where(t => t.Title.ToLower().Contains(searchQuery.ToLower()))
            );
        }

        // Применяем фильтр по статусу
        if (!string.IsNullOrEmpty(status))
        {
            filteredTasks = new System.Collections.ObjectModel.ObservableCollection<FreelanceExchange_desktop.Data.Task>(
                filteredTasks.Where(t => t.GetStatusText().ToLower() == status.ToLower())
            );
        }

        // Сортировка
        filteredTasks = new System.Collections.ObjectModel.ObservableCollection<FreelanceExchange_desktop.Data.Task>(
            sortBy switch
            {
                "name" => filteredTasks.OrderBy(t => t.Title).ToList(),
                "name_desc" => filteredTasks.OrderByDescending(t => t.Title).ToList(),
                "date" => filteredTasks.OrderByDescending(t => t.CreatedAt).ToList(),
                "date_asc" => filteredTasks.OrderBy(t => t.CreatedAt).ToList(),
                _ => filteredTasks.OrderByDescending(t => t.CreatedAt).ToList()
            }
        );

        ViewBag.Tasks = filteredTasks;
        ViewBag.CurrentStatus = status;
        ViewBag.SearchQuery = searchQuery;
        ViewBag.SortBy = sortBy;
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
