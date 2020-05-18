using Caliburn.Micro;
using Project_DAL;
using Project_DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_WPF.ViewModels
{
    public class MemberViewModel : Screen
    {
        

        List<Person> PersonList = new List<Person>();
        List<Boardgame> GameList = new List<Boardgame>();
        List<Owner> OwnerList = new List<Owner>();

        private Person _selectedPerson = null;
        private Boardgame _selectedGame = null;
        private Owner _selectedOwner = null;

        public Owner SelectedOwner
        {
            get { return _selectedOwner; }
            set { _selectedOwner = value; NotifyOfPropertyChange(() => SelectedOwner); }
        }


        private string _txtFirstname;
        private string _txtLastname;
        private string _txtEmail;
        

        private BindableCollection<Person> _persons;
        private BindableCollection<Boardgame> _boardgames;
        private BindableCollection<Owner> _owners;
        private BindableCollection<Person> _personsWithBoardgames = new BindableCollection<Person>();

        public BindableCollection<Person> PersonsWithBoardgames
        {
            get { return _personsWithBoardgames; }
            set { _personsWithBoardgames = value; NotifyOfPropertyChange(() => PersonsWithBoardgames); }
        }


        public BindableCollection<Owner>  Owners
        {
            get { return _owners; }
            set { _owners = value; NotifyOfPropertyChange(() => Owners); }
        }


        //Categorie
        public BindableCollection<Boardgame> Games
        {
            get { return _boardgames; }
            private set
            {
                _boardgames = value;
                NotifyOfPropertyChange(() => Games);
            }
        }
        public Boardgame SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                NotifyOfPropertyChange(() => SelectedGame);
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
        
        

        //Constructor
        public MemberViewModel()
        {
            ReloadLists();

            //Vul de lijst van Categorie al in
            GameList = DatabaseOperations.GetBoardgames();
            Games = new BindableCollection<Boardgame>(GameList);
        }

        private void ReloadLists()
        {
            //Vul de lijst van personen in
            PersonList = DatabaseOperations.GetPeople();
            Persons = new BindableCollection<Person>(PersonList);

            OwnerList = DatabaseOperations.GetOwners();
            Owners = new BindableCollection<Owner>(OwnerList);

            PersonsWithBoardgames = new BindableCollection<Person>();
            //Ophalen van Owners => zijn personID's die voorkomen in Owners (geen dubbele!)
            PersonsWithBoardgames = GetPersonsWithBoardgames(GetPersonIDsFromOwner(Owners));
        }

        public void AddNewPerson()
        {
            Person personToAdd = new Person();
            personToAdd.Firstname = TxtFirstname;
            personToAdd.Lastname = TxtLastname;
            personToAdd.Email = TxtEmail;

            if (personToAdd.IsGeldig())
            {
                List<Person> personsInDb = new List<Person>();
                personsInDb = DatabaseOperations.GetPeople();

                if (!personsInDb.Contains(personToAdd))
                {
                    DatabaseOperations.AddPerson(personToAdd);
                    MessageBox.Show($"{personToAdd.Fullname} is nu lid met persoon ID : {personToAdd.Person_id}");
                    EmptyTextFields();
                }
                else
                {
                    MessageBox.Show($"{personToAdd.Fullname} is al toegevoegd aan de database");
                }
               
                
            }
            else
            {
                MessageBox.Show(personToAdd.Error);
            }

            ReloadLists();
        }

        public void EditExistingPerson()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show($"Selecteer een persoon aub");
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
                        MessageBox.Show($"Wijziging gelukt!");
                        }
                        else
                        {
                        MessageBox.Show($"Er is iets fout gegaan");
                        }
                    }
                    else
                    {
                        MessageBox.Show( personToEdit.Error);
                    }
                }
            

            ReloadLists();

        }

        public void BackButton()
        {
            EmptyTextFields();
            
            SelectedPerson = null;
        }

        public void EmptyTextFields()
        {
            TxtEmail = "";
            TxtFirstname = "";
            TxtLastname = "";
        }

        public void AddOwner()
        {
            
            if (SelectedPerson != null && SelectedGame != null)
            {
                Owner o = new Owner();
                o.Boardgame_id = SelectedGame.Boardgame_id;
                o.Person_id = SelectedPerson.Person_id;
                List<Owner> ownersInDb = new List<Owner>();
                ownersInDb = DatabaseOperations.GetOwners();
                if (!ownersInDb.Contains(o))
                {
                    DatabaseOperations.AddOwnerToDB(o);
                    MessageBox.Show($"{SelectedPerson.Fullname} is nu eigenaar van {SelectedGame.Titel}");

                }
                else
                {
                    MessageBox.Show($"{SelectedPerson.Fullname} is al eigenaar van {SelectedGame.Titel}");
                }       
            }
            else
            {
                MessageBox.Show($"Selecteer een persoon en een spel aub");
            }
            SelectedGame = null;
            SelectedPerson = null;
            ReloadLists();
        }

        public void AddDesigner()
        {
            if (SelectedPerson != null && SelectedGame != null)
            {
               
                Designer d = new Designer();
                d.Boardgame_id = SelectedGame.Boardgame_id;
                d.Person_id = SelectedPerson.Person_id;

                List<Designer> designersInDb = new List<Designer>();
                designersInDb = DatabaseOperations.GetDesigners();

                if (!designersInDb.Contains(d))
                {
                    DatabaseOperations.AddDesignerToDB(d);
                    MessageBox.Show($"{SelectedPerson.Fullname} is nu toegevoegd aan de database als designer van {SelectedGame.Titel}");
                }
                else
                {
                    MessageBox.Show($"{SelectedPerson.Fullname} is reeds toegevoegd als designer van {SelectedGame.Titel}");
                }
               
            }
            else
            {
                MessageBox.Show($"Selecteer een persoon en een spel aub");
            }
            SelectedGame = null;
            SelectedPerson = null;
            ReloadLists();
        }

        public List<int> GetPersonIDsFromOwner(BindableCollection<Owner> owners)
        {
            //haal op : alle personId's van de OwnersList
            //Zorg dat elk personID maar 1 keer voorkomt
            List<int> ownerPersonIds = new List<int>();
            foreach (Owner owner in owners)
            {
                int personID = owner.Person_id;
                if (!ownerPersonIds.Contains(personID))
                {
                    ownerPersonIds.Add(personID);
                }
            }
            return ownerPersonIds;
        }
        
        public BindableCollection<Person> GetPersonsWithBoardgames(List<int> personIDs)
        {
            //Voor elk id in deze lijst halen we de persoon uit de db
            foreach (int personid in personIDs)
            {
                PersonsWithBoardgames.Add(DatabaseOperations.GetPersonFromPersonID(personid));
            }
            //de lijst van deze personen linken aan de combobox
            return PersonsWithBoardgames;
        }
        
        
        //van deze geselcteerde persoon/owner geven we alle boardgameIDs die bij deze personID horen
        //van deze boardgameID's tonen we de titels in het datagrid

    }
}
