using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoGemeenschap;

namespace VideoGemeenschap
{
    public class FilmDBManager
    {

        public ObservableCollection<Film> GetFilms()
        {
            ObservableCollection<Film> filmlijst = new ObservableCollection<Film>();
            using (var entities = new VideoContext())
            {
                  var query = from f in entities.Films.Include("Genres")
                              orderby f.Titel
                              select f;
              //  var query = entities.Films;


                foreach (var f in query)
                {
                    filmlijst.Add(f);
                }
            }
            return filmlijst;
        }

        public ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> genreLijst = new ObservableCollection<Genre>();
            using (var context = new VideoContext())
            {
                var query = from g in context.Genres
                            orderby g.GenreNaam
                            select g;
                foreach (var g in query)
                {
                    genreLijst.Add(g);
                }
            }
            return genreLijst;
        }
        public void DeleteFilm(List<Film> filmsToDelete)
        {
            using (var context = new VideoContext())
            {
                // loop trough each film in list of entities to delete
                foreach (Film film in filmsToDelete)
                {
                    try
                    {
                        // search film in entities on correct BandNr
                        var filmToDelete = context.Films.Find(film.BandNr);
                        if (filmToDelete != null)
                        {
                            // search film in database ?? 
                            Film gevondenFilm = context.Films.Where(f => f.BandNr == film.BandNr).First();
                            // remove film from database
                            context.Films.Remove(gevondenFilm);
                            // save all changes
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("film niet verwijderd");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }

        }
        public void InsertFilm(List<Film> filmsToInsert)
        {
            using (var context = new VideoContext())
            {
                foreach (var film in filmsToInsert)
                {
                    context.Films.Add(film);
                }
                context.SaveChanges();
            }

        }

        public void UpdateFilm(List<Film> filmsToUpdate)
        {
            using (var context = new VideoContext())
            {
                foreach (Film f in filmsToUpdate)
                {
                    try
                    {
                        var Film = context.Films.Find(f.BandNr);
                        Film.GenreNr = f.GenreNr;
                        Film.Titel = f.Titel;
                        Film.InVoorraad = f.InVoorraad;
                        Film.UitVoorraad = f.UitVoorraad;
                        Film.TotaalVerhuurd = f.TotaalVerhuurd;
                        Film.Prijs = f.Prijs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                context.SaveChanges();
            }
        }

    }
}
