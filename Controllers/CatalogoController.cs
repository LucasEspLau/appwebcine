using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appwebcine.Data;
using appwebcine.Service;
using Microsoft.AspNetCore.Mvc;
using Clase_6;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace appwebcine.Controllers
{
    public class CatalogoController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductoService _productoService;
        public CatalogoController(ApplicationDbContext context, ProductoService productoService){
            _context = context;
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.GetAllProductos();
            
            return productos != null ?
                        View(productos) :
                        Problem("La lista esta vacia");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();
            
            var producto = await _context.DataProducto
                    .FirstOrDefaultAsync(m => m.Id == id);
            
            if(producto == null)
                return NotFound();

            return View(producto);
        }
    }
}