using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AplicacionAPI_Desafio2_MM200149_PM190655.Models
{
    public class Organizador
    {
        [Key]
        public int OrganizadorId { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Nombre { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Cargo { get; set; }


        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        //public Evento Evento { get; set; }
    }
}
