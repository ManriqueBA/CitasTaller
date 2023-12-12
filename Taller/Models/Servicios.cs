using System.ComponentModel.DataAnnotations;

namespace Taller.Models
{
    public class Servicios
    {
        [Key]
        public int ServicioID { get; set; }
        public string Descripcion { get; set; }
    }
}
