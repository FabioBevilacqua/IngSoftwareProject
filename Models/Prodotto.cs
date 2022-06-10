using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Prodotto
    {
        [Key]
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "Caratteri minimi 5")]
        [MaxLength(150, ErrorMessage = "Caratteri massimi 150")]
        [Required]
        public string Descrizione { get; set; }

        [Range(4,100, ErrorMessage = "Attenzione, quantita inserita non valida")]
        public int Quantita { get; set; }

        [Range(0.1, 1000, ErrorMessage = "Attenzione, il prezzo non può essere negativo o superare i 1000 euro")]
        public double Prezzo { get; set; }

        public DateTime? Deleted_At { get; set; }
    }
}