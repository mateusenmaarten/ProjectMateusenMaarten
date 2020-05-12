using Caliburn.Micro;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WPF.ViewModels
{
    public class BoardgameViewModel : Screen
    {
        List<Person> PersonList = new List<Person>();
        List<Publisher> PublisherList = new List<Publisher>();
        List<Category> CategoryList = new List<Category>();
        private BindableCollection<Person> _persons;
        private BindableCollection<Publisher> _publishers;
        private BindableCollection<Category> _categories;
        private Person _selectedPerson;
        private Publisher _selectedPublisher;

        public Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set { _selectedPublisher = value; NotifyOfPropertyChange(() => SelectedPublisher); }
        }
        public BindableCollection<Publisher> Publishers
        {
            get { return _publishers; }
            set { _publishers = value; NotifyOfPropertyChange(() => Publishers); }
        }
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { _selectedPerson = value; NotifyOfPropertyChange(() => SelectedPerson); }
        }
        public BindableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; NotifyOfPropertyChange(() => Persons); }
        }
        public BindableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; NotifyOfPropertyChange(() => Categories); }
        }
        public BoardgameViewModel()
        {
            PersonList = DatabaseOperations.GetPeople();
            Persons = new BindableCollection<Person>(PersonList);

            PublisherList = DatabaseOperations.GetPublishers();
            Publishers = new BindableCollection<Publisher>(PublisherList);

            CategoryList = DatabaseOperations.GetCategories();
            Categories = new BindableCollection<Category>(CategoryList);
        }



        //private string Valideer(string columnName)
        //{
        //    if (columnName == "SelectedPerson" && SelectedPerson == null)
        //    {
        //        return $"Selecteer een lid om eigenaar te maken" + Environment.NewLine;
        //    }
        //    if (columnName == "SelectedCategory" && SelectedCategory == null)
        //    {
        //        return $"Selecteer een categorie" + Environment.NewLine;
        //    }
        //    if (columnName == "TxtMinPlayers" && !int.TryParse(TxtMinPlayers, out int txtMinPlayer))
        //    {
        //        return $"Minimum aantal spelers moet een numerieke waarde zijn" + Environment.NewLine;
        //    }
        //    if (columnName == "TxtMaxPlayers" && !int.TryParse(TxtMaxPlayers, out int txtMaxPlayer))
        //    {
        //        return $"Maximum aantal spelers moet een numerieke waarde zijn" + Environment.NewLine;
        //    }
        //    if (columnName == "TxtMinAge" && !int.TryParse(TxtMinAge, out int txtMinAge))
        //    {
        //        return $"Minimum leeftijd moet een numerieke waarde zijn" + Environment.NewLine;
        //    }
        //    //if (columnName == "TxtMaxAge" && !int.TryParse(TxtMaxAge, out int txtMaxAge))
        //    //{
        //    //    return $"Maximum leeftijd moet een numerieke waarde zijn" + Environment.NewLine;
        //    //}
        //    if (columnName == "TxtMinPlaytime" && !int.TryParse(TxtMinPlaytime, out int txtMinPlaytime))
        //    {
        //        return $"Minimum speelduur moet een numerieke waarde zijn" + Environment.NewLine;
        //    }
        //    if (columnName == "TxtMaxPlaytime" && !int.TryParse(TxtMaxPlaytime, out int txtMaxPlaytime))
        //    {
        //        return $"Maximum speelduur moet een numerieke waarde zijn" + Environment.NewLine;
        //    }
        //    return "";
        //}
    }
}
