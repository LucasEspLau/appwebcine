using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using appwebcine.Data;
using appwebcine.Models;
using appwebcine.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace appwebcine.Controllers
{
    public class PagoController : Controller
    {
            private readonly ILogger<CarritoController> _logger;
            private readonly ApplicationDbContext _context;

            private readonly UserManager<IdentityUser> _userManager;
            private readonly PagoService _pagoService;
            public PagoController(ILogger<CarritoController> logger, 
            ApplicationDbContext context, 
            UserManager<IdentityUser> userManager,
            PagoService pagoService){
                _logger = logger;
                _context = context;
                _userManager = userManager;
                _pagoService = pagoService;
            }

        public async Task<IActionResult> Index()
        {
            var pagos= await _pagoService.GetAllPagos();
            return pagos!= null? View(pagos) : Problem("La lista esta vacia");
        }
        public IActionResult Create(Decimal monto){
            Pago pago=new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal=monto;
            return View(pago);
        }
        [HttpPost]
        public IActionResult Pagar(Pago pago){
            pago.PaymentDate = DateTime.UtcNow;
            _context.Add(pago);
            var itemsCarrito = from o in _context.DataProforma select o;
            itemsCarrito = itemsCarrito.
                Include(p=> p.Producto).
                Where(s => s.UserID.Equals(pago.UserID) && s.Status.Equals("PENDIENTE"));

            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.pago = pago;
            pedido.Total=pago.MontoTotal;
            pedido.Status= "PENDIENTE";
            _context.Add(pedido);

            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach (var item in itemsCarrito.ToList())
            {
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.pedido=pedido;
                detallePedido.Producto=item.Producto;
                detallePedido.Cantidad=item.Cantidad;
                detallePedido.Precio=item.Precio;
                itemsPedido.Add(detallePedido);
            }
            _context.AddRange(itemsPedido);

            foreach(Proforma p in itemsCarrito.ToList()){
                p.Status="PROCESADO";
            }
            _context.UpdateRange(itemsCarrito);
            _context.SaveChanges();

            ViewData["Message"]= "El pago se ha registrado con el pedido nro "+pedido.ID;

            return View("Create");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}