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
        List<Person> PersonList = new List<Person>();

        private Person _selectedPerson = null;
        private string _txtFirstname;
        private string _txtLastname;
        private string _txtEmail;
        private string _foutmeldingsLabel;
        private BindableCollection<Person> _persons;
        
        public BindableCollection<Person> Persons 
        {
            get 
            {
                return _persons;
            }
            private set 
            {
                _persons = value;
                NotifyOfPropertyChange(()=>Persons);
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

        public MemberViewModel()
        {
            ReLoadMemberList();
        }

        private void ReLoadMemberList()
        {
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

    }
}
