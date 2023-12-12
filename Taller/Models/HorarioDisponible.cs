using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class HorarioDisponible
    {
        [Key]
        public int HorarioID { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de finalización es obligatoria.")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El tipo de servicio es obligatorio.")]
        public string TipoServicio { get; set; }

        [Required(ErrorMessage = "El estado de disponibilidad es obligatorio.")]
        public bool Disponible { get; set; }
    }
}
