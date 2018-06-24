using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoGemeenschap
{
    public class VideoContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //public DbSet<Verhuur> Verhuur { get; set; }
        //public DbSet<Klant> Klanten { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Film>()
        //        .HasOptional(a => a.Genres)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);
        //}
    }
}
