using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProspectosAPI.Models
{
    public class ProspectoContext:DbContext
    {
        public ProspectoContext(DbContextOptions<ProspectoContext> options):base(options)
        {


        }
        public DbSet<Prospectos> Prospectos { get; set; }
    }
}
