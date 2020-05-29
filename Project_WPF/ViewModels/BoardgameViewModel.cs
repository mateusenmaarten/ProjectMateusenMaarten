using Caliburn.Micro;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project_WPF.ViewModels
{
    public class BoardgameViewModel : Screen
    {
        
        List<Publisher> PublisherList = new List<Publisher>();
        List<Category> CategoryList = new List<Category>();

        //PROPERTIES
        private BindableCollection<Publisher> _publishers = new BindableCollection<Publisher>();
        private BindableCollection<Category> _categories;
        private Publisher _selectedPublisher;
        private Category _selectedCategory;
        private Category _selectedGameCategory;
        private BindableCollection<Category> _selectedGameCategories = new BindableCollection<Category>();
        private BindableCollection<Category> _datagridCategories = new BindableCollection<Category>();
        private List<int> _selectedCategoryIDs = new List<int>();

        private string _txtTitle;
        private string _txtMinPlayers;
        private string _txtMaxPlayers;
        private string _txtMinPlaytime;
        private string _txtMaxPlaytime;
        private string _txtMinAge;
        private bool _smallParts;

        public BindableCollection<Category> DataGridCategories
        {
            get { return _datagridCategories; }
            set { _datagridCategories  = value; NotifyOfPropertyChange(() => DataGridCategories); }
        }
        public BindableCollection<Category> SelectedGameCategories
        {
            get { return _selectedGameCategories; }
            set { _selectedGameCategories = value; NotifyOfPropertyChange(() => SelectedGameCategories); }
        }
        public BindableCollection<Publisher> Publishers
        {
            get { return _publishers; }
            set { _publishers = value; NotifyOfPropertyChange(() => Publishers); }
        }
        public BindableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; NotifyOfPropertyChange(() => Categories); }
        }

        public List<int> SelectedCategoryIDs
        {
            get { return _selectedCategoryIDs; }
            set { _selectedCategoryIDs = value; }
        }

        public Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set { _selectedPublisher = value; NotifyOfPropertyChange(() => SelectedPublisher); }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; NotifyOfPropertyChange(() => SelectedCategory); }
        }
        public Category SelectedGameCategory
        {
            get { return _selectedGameCategory; }
            set { _selectedGameCategory = value; }
        }

        public bool SmallParts
        {
            get { return _smallParts; }
            set { _smallParts = value; NotifyOfPropertyChange(()=> SmallParts); }
        }
        public string TxtMinAge
        {
            get { return _txtMinAge; }
            set { _txtMinAge = value; NotifyOfPropertyChange(() => TxtMinAge); }
        }
        public string TxtMaxPlaytime
        {
            get { return _txtMaxPlaytime; }
            set { _txtMaxPlaytime = value; NotifyOfPropertyChange(() => TxtMaxPlaytime); }
        }
        public string TxtMinPlaytime
        {
            get { return _txtMinPlaytime; }
            set { _txtMinPlaytime = value; NotifyOfPropertyChange(() => TxtMinPlaytime); }
        }
        public string TxtMaxPlayers
        {
            get { return _txtMaxPlayers; }
            set { _txtMaxPlayers = value; NotifyOfPropertyChange(() => TxtMaxPlayers); }
        }
        public string TxtMinPlayers
        {
            get { return _txtMinPlayers; }
            set { _txtMinPlayers = value; NotifyOfPropertyChange(() => TxtMinPlayers); }
        }
        public string TxtTitle
        {
            get { return _txtTitle; }
            set { _txtTitle = value; NotifyOfPropertyChange(() => TxtTitle); }
        }

        //CONSTRUCTOR
        public BoardgameViewModel()
        {
            PublisherList = DatabaseOperations.GetPublishers();
            Publishers = new BindableCollection<Publisher>(PublisherList);

            CategoryList = DatabaseOperations.GetCategories();
            Categories = new BindableCollection<Category>(CategoryList);

        }

        //METHODS
        public void AddCategoryToGame()
        {
            string foutmeldingen = Valideer("SelectedCategory");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {

                if (!SelectedGameCategories.Contains(SelectedCategory))
                {
                    SelectedGameCategories.Add(SelectedCategory);
                    
                }
                else
                {
                    MessageBox.Show($"Deze categorie is al toegevoegd");
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }
        public void RemoveCategoryFromGame()
        {
            if (SelectedGameCategory == null)
            {
                MessageBox.Show($"Selecteer een categorie om te verwijderen");
            }
            else
            {
                Category cat = SelectedGameCategory as Category;
                SelectedGameCategories.Remove(cat);
                
            }
        }
        public void AddGame()
        {
            string foutmeldingen = Valideer("TxtMinPlayers");
            foutmeldingen += Valideer("TxtMaxPlayers");
            foutmeldingen += Valideer("TxtMinAge");
            foutmeldingen += Valideer("TxtMinPlaytime");
            foutmeldingen += Valideer("TxtMaxPlaytime");
            foutmeldingen += Valideer("SelectedPublisher");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Boardgame boardgameToAdd = new Boardgame();
                boardgameToAdd.Titel = TxtTitle;
                boardgameToAdd.MinNumberOfPlayers = int.Parse(TxtMinPlayers);
                boardgameToAdd.MaxNumberOfPlayers = int.Parse(TxtMaxPlayers);
                boardgameToAdd.MinPlayingTime = int.Parse(TxtMinPlaytime);
                boardgameToAdd.MaxPlayingTime = int.Parse(TxtMaxPlaytime);
                boardgameToAdd.MinAgeToPlay = int.Parse(TxtMinAge);
                boardgameToAdd.ContainsSmallParts = SmallParts;
                boardgameToAdd.Publisher_id = SelectedPublisher.Publisher_id;

                if (boardgameToAdd.IsGeldig())
                {
                    if (!DatabaseOperations.GetBoardgames().Contains(boardgameToAdd))
                    {
                        if (SelectedGameCategories.Count != 0)
                        {
                            DatabaseOperations.AddBoardgame(boardgameToAdd);
                            MessageBox.Show($"{boardgameToAdd.Titel} is toegevoegd aan de database");
                            Wissen();
                            //Nadat het spel succesvol is toegevoegd wordt het gelinkt aan de juiste categorie
                            //(in db : Boardgame_Category)
                            foreach (Category cat in SelectedGameCategories)
                            {
                                SelectedCategoryIDs.Add(cat.Category_id);
                            }
                            DatabaseOperations.AddCategoryToBoardgame(boardgameToAdd.Boardgame_id, SelectedCategoryIDs);
                        }
                        else
                        {
                            MessageBox.Show($"Selecteer minstens 1 categorie");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Dit spel bestaat al in de database");
                    }
                }
                else
                {
                    MessageBox.Show(boardgameToAdd.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        public void Wissen()
        {
            TxtTitle = "";
            TxtMinPlaytime = "";
            TxtMaxPlaytime = "";
            TxtMinPlayers = "";
            TxtMaxPlayers = "";
            TxtMinAge = "";
            SelectedPublisher = null;
            SelectedGameCategories = new BindableCollection<Category>();
        }
        private string Valideer(string columnName)
        {
            
            if (columnName == "TxtMinPlayers" && !int.TryParse(TxtMinPlayers, out int txtMinPlayer))
            {
                return $"Minimum aantal spelers moet een numerieke waarde zijn" + Environment.NewLine;
            }
            if (columnName == "TxtMaxPlayers" && !int.TryParse(TxtMaxPlayers, out int txtMaxPlayer))
            {
                return $"Maximum aantal spelers moet een numerieke waarde zijn" + Environment.NewLine;
            }
            if (columnName == "TxtMinAge" && !int.TryParse(TxtMinAge, out int txtMinAge))
            {
                return $"Minimum leeftijd moet een numerieke waarde zijn" + Environment.NewLine;
            }
            if (columnName == "TxtMinPlaytime" && !int.TryParse(TxtMinPlaytime, out int txtMinPlaytime))
            {
                return $"Minimum speelduur moet een numerieke waarde zijn" + Environment.NewLine;
            }
            if (columnName == "TxtMaxPlaytime" && !int.TryParse(TxtMaxPlaytime, out int txtMaxPlaytime))
            {
                return $"Maximum speelduur moet een numerieke waarde zijn" + Environment.NewLine;
            }
            if (columnName == "SelectedPublisher" && SelectedPublisher == null)
            {
                return $"Selecteer een publisher" + Environment.NewLine;
            }
            if (columnName == "SelectedCategory" && SelectedCategory == null)
            {
                return $"Selecteer een categie" + Environment.NewLine;
            }
            return "";
        }
    }
}
