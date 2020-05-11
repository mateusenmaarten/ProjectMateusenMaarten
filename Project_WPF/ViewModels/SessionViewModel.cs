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
        private BindableCollection<Person> _dataGridPlayers = new BindableCollection<Person>();
        private string _txtGameToPlay;
        private string _txtDateToPlay;
        private DatePicker _dateToPlay = new DatePicker();
        private Person _selectedPlayer;
        private Boardgame _selectedBoardgame;
        private Person _selectedGridPlayer;
        private DateTime? _selectedDate;

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
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; NotifyOfPropertyChange(() => SelectedDate); }
        }
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
            int sessionId = 0;
            string foutmelding = Valideer("SelectedBoardgame");
            foutmelding += Valideer("DateToPlay");
            foutmelding += Valideer("SelectedPlayers");

            if (foutmelding == "")
            {
                string spelersInSessie = "";

                PlaySession playSession = new PlaySession();
                playSession.Boardgame_id = SelectedBoardgame.Boardgame_id;
                playSession.SessionDate = (DateTime)SelectedDate;
                //Playsession naar db --> meegeven : bg_id en Sessiondate
                //Opvragen : session_id

                int ok = DatabaseOperations.AddPlaySession(playSession, ref sessionId);
                if (ok > 0)
                {
                    MessageBox.Show($"Speelsessie toegevoegd met sessie ID : {sessionId}");
                }
                //Met dit session_id alle spelers toevoegen
                foreach (Person player in SelectedPlayers)
                {
                    int oke = DatabaseOperations.AddPlayer(player.Person_id, sessionId);
                    if (oke > 0)
                    {
                        spelersInSessie += player.Fullname + Environment.NewLine;
                    }
                    else
                    {
                        MessageBox.Show($"Toevoegen van {player.Fullname} niet gelukt");
                    }
                }

                MessageBox.Show($"De volgende spelers zijn toegevoegd aan speelsessie {sessionId}" + Environment.NewLine +
                                spelersInSessie);
            }
            else
            {
                MessageBox.Show(foutmelding);
            }
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
            if (columnName == nameof(SelectedDate) && !SelectedDate.HasValue)
            {
                return $"Selecteer een datum om te spelen" + Environment.NewLine;
            }
            if (columnName == "SelectedPlayers" && SelectedPlayers.Count <= 0)
            {
                return $"Selecteer minstens 1 speler om deel te nemen";
            }
            return "";
        }
    }
}
