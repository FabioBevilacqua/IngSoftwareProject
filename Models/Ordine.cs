using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ordine
    {
        [Key]
        public int Id { get; set; }

        public int IdUtente { get; set; }

        [ForeignKey("IdUtente")]

        public Utente Utente { get; set; }

        public List<OrdineProdotto> OrdineProdotto { get; set; }

        public double Totale { get; set; }

        [Required]
        public DateTime Created_At { get; set; }

        public DateTime? Deleted_At { get; set; }
    }
}
