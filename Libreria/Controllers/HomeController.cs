using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Libreria.Models;
using Utility;

namespace Libreria.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Elenco()
    {
        if (HttpContext.Items["AuthUser"] is not null)
        {
            ViewData["User"] = HttpContext.Items["AuthUser"];
        }
        List<Entity> libri = DAOLibro.GetInstance().ReadAll();
        return View(libri);
    }
    public IActionResult Privacy()
    {
        if (HttpContext.Items["AuthUser"] is not null)
        {
            ViewData["User"] = HttpContext.Items["AuthUser"];
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
