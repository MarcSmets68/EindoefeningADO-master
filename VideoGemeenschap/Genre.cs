using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGemeenschap
{
    [Table("Genres")]
    public class Genre
    {
       
        [Key]
        public virtual int GenreNr { get; set; }
        [Column("Genre")]
        public string GenreNaam { get; set; }

        public virtual ICollection<Film> Films { get; set; }
       
    }
    
}
