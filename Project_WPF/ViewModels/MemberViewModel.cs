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
        

        private BindableCollection<Person> _persons;
        private BindableCollection<Category> _categories;

        

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
                MessageBox.Show($"{personToAdd.Fullname} is nu lid met ID : {personToAdd.Person_id}");
                EmptyTextFields();
                
            }
            else
            {
                MessageBox.Show(personToAdd.Error);
            }

            ReLoadMemberList();
        }

        public void EditExistingMember()
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show($"Selecteer een lid aub");
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
            

            ReLoadMemberList();

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

        

        


    }
}
