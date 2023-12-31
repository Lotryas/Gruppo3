﻿using System.Diagnostics;
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

    public IActionResult Elenco(string search)
    {
        if (HttpContext.Items["AuthUser"] is not null)
        {
            Utente? u = (Utente?)HttpContext.Items["AuthUser"];
            List<long> prestiti = DAOUtente_Libro.GetInstance().GetPrestati(u.Id);
            ViewBag.Prestiti = prestiti;
        }
        if (search != null)
        {
            List<Entity> libri= DAOLibro.GetInstance().FindTitolo(search);
            return View(libri);
        }
        else
        {
            List<Entity> libri = DAOLibro.GetInstance().ReadAll();
            return View(libri);
        }
        
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
