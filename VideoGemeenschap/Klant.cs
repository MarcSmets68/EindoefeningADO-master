using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VideoGemeenschap
{
    [Table("Klanten")]
    public class Klant
    {
        [Key]
        public int KlantNr { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Straat_Nr { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }

    }
}
