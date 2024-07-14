using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appwebcine.Models;

namespace appwebcine.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["Message"] ="Enviando mensajes desde el controller al a vista";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]

    public IActionResult Formulario()
    {        
        ViewData["msj"] ="Bienvenido a registrar producto";

        return View();
    }
    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            // Lógica para guardar el producto en la base de datos
            ViewData["msj"] = "Producto registrado exitosamente.";
            return RedirectToAction("Crear");
        }
        return View(producto);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
