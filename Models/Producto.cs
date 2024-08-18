using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace appwebcine.Models
{
    [Table("t_producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [NotNull]
        public string? Nombre {get;set;}
        [NotNull]
        public Decimal? Precio {get;set;}
        [NotNull]
        public string? ImagenURL {get; set; }
        [NotNull] 
        public string? Descripcion {get; set; }

    }
}