using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrdineProdotto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdProdotto")]
        public Prodotto Prodotto { get; set; }
        public int IdProdotto { get; set; }
        [ForeignKey("IdOrdine")]
        public Ordine Ordine { get; set; }
        public int IdOrdine { get; set; }
        public int Quantità { get; set; }
        public double Totale { get; set; }
    }
}
