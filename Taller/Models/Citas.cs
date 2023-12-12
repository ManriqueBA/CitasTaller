using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Citas
    {
        [Key]
        public int CitaID { get; set; }

        [Required(ErrorMessage = "Se requiere un mecánico para la cita.")]
        public int MecanicoID { get; set; }

        [Required(ErrorMessage = "Se requiere un horario para la cita.")]
        public TimeSpan HoraCita { get; set; }

        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "El estado de la cita es obligatorio.")]
        public string EstadoCita { get; set; }

        [Required(ErrorMessage = "Se requiere un servicio para la cita")]
        public int ServicioID { get; set; }
    }
}
