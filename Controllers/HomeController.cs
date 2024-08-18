using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appwebcine.Models;
using Clase_6;
using Microsoft.Extensions.ML;

namespace appwebcine.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PredictionEnginePool<MLModel1.ModelInput,MLModel1.ModelOutput> _predictionEnginePool;

    public HomeController(ILogger<HomeController> logger,PredictionEnginePool<MLModel1.ModelInput,MLModel1.ModelOutput> predictionEnginePool)
    {
        _logger = logger;
        _predictionEnginePool = predictionEnginePool;
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
    public  IActionResult Contacto()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(Contacto objContacto)
    {
        

        MLModel1.ModelInput modelInput = new MLModel1.ModelInput()
        {
            SENTIMIENTO_TEXT = objContacto.Mensaje
        };
        MLModel1.ModelOutput prediction=_predictionEnginePool.Predict(modelInput);

        TempData["MessageCONTACTO"] ="";
        if(prediction.PredictedLabel==1)
        {
            TempData["MessageCONTACTO"] = "El mensaje fue Positivo";
        }else{
            TempData["MessageCONTACTO"] = "El mensaje fue Negativo";
        }
        TempData["Sentimiento"] =""+ prediction.PredictedLabel;
        //Mensaje resumen 1 1.2826674 -1.0390763

        return View("~/Views/Home/Contacto.cshtml");
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            // Lógica para guardar el producto en la base de datos
            ViewData["msj"] = "Producto registrado exitosamente. "+producto.Nombre;
            return View("Formulario",producto);
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
