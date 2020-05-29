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

        //Constructor
        public SearchViewModel()
        {
            categoryList = DatabaseOperations.GetCategories();
            Categories = new BindableCollection<Category>(categoryList);
        }

        //Properties
        private BindableCollection<Category> _categories;
        private BindableCollection<Boardgame> _boardgameList;
        private Category _selectedCategory;
        private int _txtNumberOfPlayers;
        

        public BindableCollection<Boardgame> BoardgameList
        {
            get { return _boardgameList; }
            set { _boardgameList = value; NotifyOfPropertyChange(() => BoardgameList); }
        }
        public BindableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; NotifyOfPropertyChange(() => Categories); }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; NotifyOfPropertyChange(() => SelectedCategory); }
        }
        public int TxtNumberOfPlayers
        {
            get { return _txtNumberOfPlayers; }
            set { _txtNumberOfPlayers = value; NotifyOfPropertyChange(() => TxtNumberOfPlayers); }
        }
        
        //Methods
        public void Search()
        {
            try
            {
                BoardgameList = new BindableCollection<Boardgame>();
                foreach (Boardgame boardgame in DatabaseOperations.GetSearchedBoardgames(SelectedCategory.Category_id, TxtNumberOfPlayers))
                {
                    BoardgameList.Add(boardgame);
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
            }
            
            
        }
    }
}
