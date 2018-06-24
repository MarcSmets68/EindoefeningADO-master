using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VideoGemeenschap;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Videotheek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public VideoContext manager = new VideoContext();
        public List<Film> teVerwijderenFilms = new List<Film>();
        public List<Film> toeTeVoegenFilms = new List<Film>();
        ObservableCollection<Film> filmsOb = new ObservableCollection<Film>();
        ObservableCollection<Genre> genresOb = new ObservableCollection<Genre>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource filmViewSource = ((CollectionViewSource)(this.FindResource("filmViewSource")));
            CollectionViewSource genreViewSource = ((CollectionViewSource)(this.FindResource("genreViewSource")));
            var manager = new VideoContext();
            //  filmsOb = this.manager.GetFilms();

            FilmDBManager mgr = new FilmDBManager();
            filmsOb = mgr.GetFilms();

            genresOb = mgr.GetGenres();
            filmViewSource.Source = filmsOb;
            genreViewSource.Source = genresOb;
            filmsOb.CollectionChanged += this.OnCollectionChanged;
        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Film oudeFilms in e.OldItems)
                {
                    teVerwijderenFilms.Add(oudeFilms);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Film nieuweFilms in e.NewItems)
                {
                    toeTeVoegenFilms.Add(nieuweFilms);
                }
            }
        }
        private void buttonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonToevoegen.Content.ToString() == "Toevoegen")
            {
                // Button TOEVOEGEN
                SchermToevoegen();
                listBoxFilms.IsEnabled = false;
                filmsOb.Add(new Film());
                listBoxFilms.SelectedIndex = listBoxFilms.Items.Count - 1;
                textBoxTitel.Text = string.Empty;
                textBoxInVoorraad.Text = string.Empty;
                textBoxUitVoorraad.Text = string.Empty;
                textBoxPrijs.Text = string.Empty;
                textBoxTotaalVerhuurd.Text = string.Empty;
                comboBoxGenre.SelectedItem = null;


            }
            else
            {
                //Button BEVESTIGEN
                if (FilmsHasErrors())
                    MessageBox.Show("Niet alle velden zijn correct ingevuld");
                else
                {

                    StartScherm();


                }
            }
        }
        private void buttonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonVerwijderen.Content.ToString() == "Verwijderen")
            {
                // Button VERWIJDEREN
                if (MessageBox.Show("Ben je zeker dat je deze film wil verwijderen?",
                    "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    try
                    {
                        filmsOb.Remove((Film)listBoxFilms.SelectedItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                // Button ANNULEREN
                StartScherm();
            }
        }

        public void StartScherm()
        {
            textBoxBandNr.IsEnabled = false;
            textBoxBandNr.IsReadOnly = true;
            buttonToevoegen.Content = "Toevoegen";
            buttonVerwijderen.Content = "Verwijderen";
            listBoxFilms.IsEnabled = true;
            textBoxTitel.IsReadOnly = true;
            comboBoxGenre.IsEnabled = false;
            textBoxInVoorraad.IsReadOnly = true;
            textBoxUitVoorraad.IsReadOnly = true;
            textBoxPrijs.IsReadOnly = true;
            textBoxTotaalVerhuurd.IsReadOnly = true;

            buttonOpslaan.IsEnabled = true;
            buttonVerhuur.IsEnabled = true;

        }
        public void SchermToevoegen()
        {
            // Verander in Bevestigen
            buttonToevoegen.Content = "Bevestigen";
            // Verwijder wordt Annuleren
            buttonVerwijderen.Content = "Annuleren";
            // andere buttons worden gedisabled, alsook de listbox
            buttonOpslaan.IsEnabled = false;
            buttonVerhuur.IsEnabled = false;

            // alle controls van de gegevens worden actief, behalve het Bandnr(=autonummering)
            textBoxTitel.IsEnabled = true;
            textBoxTitel.IsReadOnly = false;
            comboBoxGenre.IsEnabled = true;
            comboBoxGenre.SelectedValue = null;
            textBoxInVoorraad.IsEnabled = true;
            textBoxInVoorraad.IsReadOnly = false;
            textBoxUitVoorraad.IsEnabled = true;
            textBoxUitVoorraad.IsReadOnly = false;
            textBoxPrijs.IsEnabled = true;
            textBoxPrijs.IsReadOnly = false;
            textBoxTotaalVerhuurd.IsEnabled = true;
            textBoxTotaalVerhuurd.IsReadOnly = false;
        }

        private bool FilmsHasErrors()
        {
            bool foutGevonden = false;
            if (Validation.GetHasError(textBoxTitel)) foutGevonden = true;
            if (Validation.GetHasError(textBoxInVoorraad)) foutGevonden = true;
            if (Validation.GetHasError(textBoxUitVoorraad)) foutGevonden = true;
            if (Validation.GetHasError(textBoxPrijs)) foutGevonden = true;
            if (Validation.GetHasError(textBoxTotaalVerhuurd)) foutGevonden = true;
            return foutGevonden;
        }
        public List<Film> gewijzigdeFilms = new List<Film>();
        private void buttonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                try
                {
                    var manager = new FilmDBManager();
                    manager.DeleteFilm(teVerwijderenFilms);
                    manager.InsertFilm(toeTeVoegenFilms);

                    foreach (Film film in filmsOb)
                    {
                        if ((film.Changed == true) && (film.BandNr != 0))
                        {
                            gewijzigdeFilms.Add(film);
                        }

                        film.Changed = false;
                    }
                    manager.UpdateFilm(gewijzigdeFilms);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            teVerwijderenFilms.Clear();
            toeTeVoegenFilms.Clear();
            gewijzigdeFilms.Clear();
        }

        private void buttonVerhuur_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(textBoxInVoorraad.Text) > 0)
            {
                textBoxInVoorraad.Text = ((Convert.ToInt32(textBoxInVoorraad.Text)) - 1).ToString();
                textBoxUitVoorraad.Text = ((Convert.ToInt32(textBoxUitVoorraad.Text)) + 1).ToString();
                textBoxTotaalVerhuurd.Text = ((Convert.ToInt32(textBoxTotaalVerhuurd.Text)) + 1).ToString();
            }
            else
            {
                MessageBox.Show("Alle films zijn verhuurd.");
            }
        }

        private void listBoxFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
