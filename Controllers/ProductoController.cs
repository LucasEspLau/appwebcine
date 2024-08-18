using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appwebcine.Data;
using appwebcine.Service;
using Microsoft.AspNetCore.Mvc;
using Clase_6;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace appwebcine.Controllers
{
    public class ProductoController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductoService _productoService;
        public ProductoController(ApplicationDbContext context, ProductoService productoService){
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
    }
}