using Caliburn.Micro;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WPF.ViewModels
{
    public class SearchViewModel : Screen
    {
        List<Category> categoryList = new List<Category>();
        
        public SearchViewModel()
        {
            categoryList = DatabaseOperations.GetCategories();
            Categories = new BindableCollection<Category>(categoryList);
        }

        private BindableCollection<Category> _categories;
        private Category _selectedCategory;
        private int _txtNumberOfPlayers;
        private BindableCollection<Boardgame> _boardgameList;

        public BindableCollection<Boardgame> BoardgameList
        {
            get { return _boardgameList; }
            set { _boardgameList = value; NotifyOfPropertyChange(() => BoardgameList); }
        }

        public int TxtNumberOfPlayers
        {
            get { return _txtNumberOfPlayers; }
            set { _txtNumberOfPlayers = value; NotifyOfPropertyChange(() => TxtNumberOfPlayers); }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; NotifyOfPropertyChange(() => SelectedCategory); }
        }
        public BindableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; NotifyOfPropertyChange(() => Categories); }
        }

        public void Search()
        {
            BoardgameList = new BindableCollection<Boardgame>();
            foreach (Boardgame boardgame in DatabaseOperations.GetSearchedBoardgames(SelectedCategory.Category_id, TxtNumberOfPlayers))
            {
                BoardgameList.Add(boardgame);
            }
            
        }
    }
}
