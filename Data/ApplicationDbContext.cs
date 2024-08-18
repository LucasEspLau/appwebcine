using appwebcine.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace appwebcine.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Producto> DataProducto { get; set; }
    public DbSet<Contacto> DataContacto { get; set; }
    public DbSet<Proforma> DataProforma { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

}
