using System.ComponentModel.DataAnnotations;

namespace AplicacionAPI_Desafio2_MM200149_PM190655.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaEvento { get; set; }

        [Required, MinLength(5), MaxLength(100)]
        public string Lugar { get; set; }

        //public ICollection<Participante> Participantes { get; set; }
        //public ICollection<Organizador> Organizadores { get; set; }
    }
}
