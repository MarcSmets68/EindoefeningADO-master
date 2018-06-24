using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VideoGemeenschap
{
    [Table("Verhuur")]
    public class Verhuur
    {
        
        public int KlantNr { get; set; }
        public int BandNr { get; set; }
        public DateTime Verhuurdatum { get; set; }

        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
    }
}
