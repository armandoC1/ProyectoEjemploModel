using System.ComponentModel.DataAnnotations;

namespace ProyectoEjemploModel.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage ="Este campo solo acepta 100 caracteres")]
        [Display(Name ="Nombre del contacto")]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Este campo solo acepta 100 caracteres")]
        [Display(Name = "Apellido de contacto")]
        public string? Apellido { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Este campo solo acepta 50 caracteres")]
        [Display(Name ="Email del contacto")]
        [EmailAddress] 
        public string? Email { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Este campo solo acepta 40 caracteres")]
        [Display(Name = "Numero de telefono del cliente")]
        public string? Telefono { get; set; }
    }
}
