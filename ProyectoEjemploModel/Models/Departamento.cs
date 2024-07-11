using Microsoft.CodeAnalysis.Host;
using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploModel.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Este  campo solo acepta 100 caracteres")]
        [Display(Name="Nombre del dapartamento")]
        public string Nombre { get; set; } = string.Empty;
    }
}
