using AutoMapper;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data
{
    public class BackDbContext : DbContext
    {
        public BackDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Recibo> Recibos { get; set; }

    }
}
