using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appwebcine.Data;
using appwebcine.Models;
using Microsoft.EntityFrameworkCore;

namespace appwebcine.Service
{
    public class PagoService
    {
        private readonly ILogger<PagoService> _logger;
        private readonly ApplicationDbContext _context;
        public PagoService(ILogger<PagoService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<List<Pago>?> GetAllPagos(){
            if(_context.DataPago ==null)
                return null;
            
            return await _context.DataPago.ToListAsync();

        }
    }
}