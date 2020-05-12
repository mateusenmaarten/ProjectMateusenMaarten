using Caliburn.Micro;
using Project_DAL;
using Project_DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_WPF.ViewModels
{
    public class MemberViewModel : Screen
    {
        WindowManager wm = new WindowManager();

        List<Person> PersonList = new List<Person>();
        List<Category> CategoryList = new List<Category>();

        private Person _selectedPerson = null;
        private Category _selectedCategory = null;

        private string _txtFirstname;
        private string _txtLastname;
        private string _txtEmail;
        private string _foutmeldingsLabel;

        private BindableCollection<Person> _persons;
        private BindableCollection<Category> _categories;

        private string _txtTitle;
        private string _txtPublisher;
        private string _txtDesigner;
        private string _txtMinPlayers;
        private string _txtMaxPlayers;
        private string _txtMinPlaytime;
        private string _txtMaxPlaytime;
        private string _txtMinAge;
        private bool _smallParts;

        //Boardgame
        public bool SmallParts
        {
            get { return _smallParts; }
            set { _smallParts = value; }
        }
        public string TxtMinAge
        {
            get { return _txtMinAge; }
            set { _txtMinAge = value; }
        }
        public string TxtMaxPlaytime
        {
            get { return _txtMaxPlaytime; }
            set { _txtMaxPlaytime = value; }
        }
        public string TxtMinPlaytime
        {
            get { return _txtMinPlaytime; }
            set { _txtMinPlaytime = value; }
        }
        public string TxtMaxPlayers
        {
            get { return _txtMaxPlayers; }
            set { _txtMaxPlayers = value; }
        }
        public string  TxtMinPlayers
        {
            get { return _txtMinPlayers; }
            set { _txtMinPlayers = value; }
        }
        public string TxtDesigner
        {
            get { return _txtDesigner; }
            set { _txtDesigner = value; }
        }
        public string TxtPublisher
        {
            get { return _txtPublisher; }
            set { _txtPublisher = value; }
        }
        public string TxtTitle
        {
            get { return _txtTitle; }
            set { _txtTitle = value; }
        }

        //Categorie
        public BindableCollection<Category> Categories
        {
            get { return _categories; }
            private set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }

        //Person
        public BindableCollection<Person> Persons 
        {
            get 
            {
                return _persons;
            }
            private set 
            {
                _persons = value;
                NotifyOfPropertyChange(() => Persons);
            }
        }
        public Person SelectedPerson
        {
            get
            {
                return this._selectedPerson;
            }
            set
            {
                this._selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }
        public string TxtFirstname
        {
            get
            {
                return _txtFirstname;
            }
            set
            {
                _txtFirstname = value;
                NotifyOfPropertyChange(() => TxtFirstname);
            }
        }
        public string TxtLastname
        {
            get
            {
                return _txtLastname;
            }
            set
            {
                _txtLastname = value;
                NotifyOfPropertyChange(() => TxtLastname);
            }
        }
        public string TxtEmail
        {
            get
            {
                return _txtEmail;
            }
            set
            {
                _txtEmail = value;
                NotifyOfPropertyChange(() => TxtEmail);
            }
        }
        public string FoutMeldingsLabel 
        {
            get { return _foutmeldingsLabel; }
            set 
            { 
                _foutmeldingsLabel = value; 
                NotifyOfPropertyChange(() => FoutMeldingsLabel); 
            }
        }
        

        //Constructor
        public MemberViewModel()
        {
            ReLoadMemberList();

            //Vul de lijst van Categorie al in
            CategoryList = DatabaseOperations.GetCategories();
            Categories = new BindableCollection<Category>(CategoryList);
        }

        private void ReLoadMemberList()
        {
            //Vul de lijst van personen in
            PersonList = DatabaseOperations.GetPeople();
            Persons = new BindableCollection<Person>(PersonList);
            
        }

        public void AddNewMember()
        {
            Person personToAdd = new Person();
            personToAdd.Firstname = TxtFirstname;
            personToAdd.Lastname = TxtLastname;
            personToAdd.Email = TxtEmail;

            if (personToAdd.IsGeldig())
            {
                int personID = 0;
                DatabaseOperations.AddPerson(personToAdd, ref personID);
                FoutMeldingsLabel = $"{personToAdd.Fullname} is nu lid met ID : {personToAdd.Person_id}";
                EmptyTextFields();
                
            }
            else
            {
                FoutMeldingsLabel = personToAdd.Error;
            }

            ReLoadMemberList();
        }

        public void EditExistingMember()
        {
            if (SelectedPerson == null)
            {
                FoutMeldingsLabel = $"Selecteer een lid aub";
            }
            
        }

        public void CommitMember() 
        {
            
                if (SelectedPerson is Person personToEdit)
                {
                    if (personToEdit.IsGeldig())
                    {
                        int ok = DatabaseOperations.EditPerson(personToEdit);
                        if (ok > 0)
                        {
                            FoutMeldingsLabel = $"Wijziging gelukt!";
                        }
                        else
                        {
                            FoutMeldingsLabel = $"Er is iets fout gegaan";
                        }
                    }
                    else
                    {
                        FoutMeldingsLabel = personToEdit.Error;
                    }
                }
            

            ReLoadMemberList();

        }

        public void BackButton()
        {
            EmptyTextFields();
            FoutMeldingsLabel = "";
            SelectedPerson = null;
        }

        public void EmptyTextFields()
        {
            TxtEmail = "";
            TxtFirstname = "";
            TxtLastname = "";
        }

        public void AddGame()
        {
            wm.ShowWindow(new BoardgameViewModel(), null,null);

            //string foutmeldingen = Valideer("SelectedPerson");
            //foutmeldingen += Valideer("SelectedCategory");
            //foutmeldingen += Valideer("TxtMinPlayers");
            //foutmeldingen += Valideer("TxtMaxPlayers");
            //foutmeldingen += Valideer("TxtMinAge");
            //foutmeldingen += Valideer("TxtMinPlaytime");
            //foutmeldingen += Valideer("TxtMaxPlaytime");

            //if (string.IsNullOrWhiteSpace(foutmeldingen))
            //{
            //    Boardgame boardgameToAdd = new Boardgame();
            //    //Publisher??
            //    boardgameToAdd.Titel = TxtTitle;
            //    boardgameToAdd.MinNumberOfPlayers = int.Parse(TxtMinPlayers);
            //    //haal person id 

            //    //(samen met boardgame id nieuwe Owner maken) van opgehaalde person id --> er is nog geen boardgame id?

            //    //haal categorie id 

            //    //(samen met boardgame id niewe Boardgame_category maken) van opgehaalde categorie id --> er is nog geen boardgame id?  
            //}
            //else
            //{
            //    MessageBox.Show(foutmeldingen);
            //}



            ////datagrid toont alle spellen van deze owner_id
        }

        


    }
}
