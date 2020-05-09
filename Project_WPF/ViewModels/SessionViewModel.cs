using Caliburn.Micro;
using Newtonsoft.Json.Converters;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public BindableCollection<Boardgame> Boardgames
        {
            get { return _boardgames; }
            set { _boardgames = value; }
        }

        private Person _selectedPlayer = null;

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

        public void AddPlayerToSession()
        {

        }
        public void RemovePlayerFromSession()
        {

        }
        public void ConfirmSession()
        {

        }
    }
}
