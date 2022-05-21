using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5,ErrorMessage = "Attenzione, lunghezza minima username non valida")]
        [MaxLength(30,ErrorMessage = "Attenzione, lunghezza massima non valida")]
        public string Username { get; set; }
    }
}
