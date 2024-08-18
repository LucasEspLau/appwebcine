using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appwebcine.Data;
using appwebcine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace appwebcine.Controllers
{
    public class CarritoController:Controller
    {
            private readonly ILogger<CarritoController> _logger;
            private readonly ApplicationDbContext _context;

            private readonly UserManager<IdentityUser> _userManager;
            public CarritoController(ILogger<CarritoController> logger, 
            ApplicationDbContext context, 
            UserManager<IdentityUser> userManager){
                _logger = logger;
                _context = context;
                _userManager = userManager;
            }

            public async Task<IActionResult> Add(int? id)
            {
                var userID= _userManager.GetUserName(User);
                if(userID == null){
                    _logger.LogInformation("No existe usuario");
                    ViewData["Message"] = "Por favor debe iniciar sesión antes de agregar un producto.";
                    return View("Index","Catalogo");
                }else{
                    var producto = await _context.DataProducto.FindAsync(id);
                    Util.SessionExtensions.Set<Producto>(HttpContext.Session,"MiUltimoProducto",producto);
                    Proforma proforma = new Proforma();
                    proforma.Producto = producto;
                    proforma.Precio= producto.Precio;
                    proforma.Cantidad=1;
                    proforma.UserID = userID;
                    _context.Add(proforma);
                    await _context.SaveChangesAsync();
                    ViewData["Message"] = "Se agrego al carrito";
                    _logger.LogInformation("Se agrego el producto al carrito");
                    return RedirectToAction("Index","Catalogo");
                }

            }
    }
}