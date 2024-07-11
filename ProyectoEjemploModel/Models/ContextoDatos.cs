using Microsoft.EntityFrameworkCore;

namespace ProyectoEjemploModel.Models
{
    public class ContextoDatos : DbContext
    {
        public ContextoDatos(DbContextOptions opciones) : base(opciones) { 

        }

        public DbSet<Departamento> Departamentos { get; set; }
    }
}
