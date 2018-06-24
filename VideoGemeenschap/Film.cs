using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VideoGemeenschap
{
    [Table("Films")]
    public class Film
    {
     
        [Key]
        public Int32 BandNr { get; set; }
        public String Titel { get; set; }

        //[ForeignKey("GenreNr")]
        public virtual Genre Genres { get; set; }
        public int GenreNr { get; set; }

        public Int32 InVoorraad { get; set; }
        public Int32 UitVoorraad { get; set; }
        public decimal Prijs { get; set; }
        public Int32 TotaalVerhuurd { get; set; }
        [NotMapped]
        public bool Changed { get; set; }


    }
}
