using Caliburn.Micro;
using Newtonsoft.Json.Converters;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project_WPF.ViewModels
{
    public class SessionViewModel : Screen
    {
        public SessionViewModel()
        {
            PlayerList = DatabaseOperations.GetPeople();
            Players = new BindableCollection<Person>(PlayerList);

            BoardGameList = DatabaseOperations.GetBoardgames();
            Boardgames = new BindableCollection<Boardgame>(BoardGameList);
        }

        List<Person> PlayerList = new List<Person>();
        List<Boardgame> BoardGameList = new List<Boardgame>();

        private BindableCollection<Person> _players;
        private BindableCollection<Boardgame> _boardgames;
        private BindableCollection<Person> _selectedPlayers = new BindableCollection<Person>();
        private string _txtGameToPlay;
        private string _txtDateToPlay;
        private DatePicker _dateToPlay;

        public DatePicker DateToPlay
        {
            get { return _dateToPlay; }
            set { _dateToPlay = value; NotifyOfPropertyChange(() => DateToPlay); }
        }
        public string TxtDateToPlay
        {
            get { return _txtDateToPlay; }
            set { _txtDateToPlay = value; NotifyOfPropertyChange(() => TxtDateToPlay); }
        }
        public string TxtGameToPlay
        {
            get { return _txtGameToPlay; }
            set { _txtGameToPlay = value; NotifyOfPropertyChange(() => TxtGameToPlay); }
        }

        public BindableCollection<Person> SelectedPlayers
        {
            get { return _selectedPlayers; }
            set 
            { 
                _selectedPlayers = value;
                NotifyOfPropertyChange(() => SelectedPlayers);
            }
        }

        private Person _selectedPlayer;
        private Boardgame _selectedBoardgame;
        private BindableCollection<Person> _dataGridPlayers = new BindableCollection<Person>();
        private Person _selectedGridPlayer;

        public Person SelectedGridPlayer
        {
            get { return _selectedGridPlayer; }
            set { _selectedGridPlayer = value; NotifyOfPropertyChange(() => SelectedGridPlayer); }
        }

        public BindableCollection<Person> DataGridPlayers
        {
            get { return _dataGridPlayers; }
            set { _dataGridPlayers = value; NotifyOfPropertyChange(() => DataGridPlayers); }
        }


        public Boardgame SelectedBoardgame
        {
            get { return _selectedBoardgame; }
            set 
            {
                _selectedBoardgame = value;
                NotifyOfPropertyChange(() => SelectedBoardgame);
            }
        }
        public Person SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                _selectedPlayer = value;
                NotifyOfPropertyChange(() => SelectedPlayer);
            }
        }

        public BindableCollection<Boardgame> Boardgames
        {
            get { return _boardgames; }
            set 
            { 
                _boardgames = value;
                NotifyOfPropertyChange(() => Boardgames);
            }
        }
        public BindableCollection<Person> Players
        {
            get
            {
                return _players;
            }
            private set
            {
                _players = value;
                NotifyOfPropertyChange(() => Players);
            }
        }

        

        public void AddPlayerToSession()
        {
            string foutmeldingen = Valideer("SelectedPlayer");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                
                if (!SelectedPlayers.Contains(SelectedPlayer))
                {
                    SelectedPlayers.Add(SelectedPlayer);
                }
                else
                {
                    MessageBox.Show($"Deze speler is al toegevoegd");
                }
                
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }
        public void RemovePlayerFromSession()
        {
            if (SelectedGridPlayer == null)
            {
                MessageBox.Show($"Selecteer een speler om te verwijderen");
            }
            else
            {
                Person person = SelectedGridPlayer as Person;
                SelectedPlayers.Remove(person);
                
            }

        }
        public void ConfirmSession()
        {

        }

        public string Valideer(string columnName)
        {
            if (columnName == "SelectedPlayer" && SelectedPlayer == null)
            {
                return $"Selecteer een speler om deel te nemen aan de sessie" + Environment.NewLine;
            }
            if (columnName == "SelectedBoardgame" && SelectedBoardgame == null)
            {
                return $"Selecteer een bordspel om te spelen" + Environment.NewLine;
            }
            return "";
        }
    }
}
