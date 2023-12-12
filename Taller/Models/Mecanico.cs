using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taller.Models
{
    public class Mecanico
    {
        [Key]
        public int MecanicoID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El nombre de usuario del mecánico es obligatorio.")]
        public string UsuarioMecanico { get; set; }

        [Required(ErrorMessage = "La contraseña del mecánico es obligatoria.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string ContrasenaMecanico { get; set; }

        
    }
}
