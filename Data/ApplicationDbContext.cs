using appwebcine.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace appwebcine.Data;

public class ApplicationDbContext : IdentityDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Producto> DataProducto { get; set; }
    public DbSet<Contacto> DataContacto { get; set; }
    public DbSet<Proforma> DataProforma { get; set; }
    
    public DbSet<Pago> DataPago { get; set; }
    public DbSet<Pedido> DataPedido { get; set; }
    public DbSet<DetallePedido> DataDetallePedido { get; set; }

}
