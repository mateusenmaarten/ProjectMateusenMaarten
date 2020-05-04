using Caliburn.Micro;
using Project_DAL;
using Project_DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WPF.ViewModels
{
    public class MemberViewModel : Screen
    {
        List<Person> PersonList = new List<Person>();

        private Person _selectedPerson = null;
        private string _txtFirstname;
        private string _txtLastname;
        private string _txtEmail;

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
            //Combobox leegmaken zodat er niemand is geselecteerd
            SelectedPerson = null;
        }

        public void EditExistingMember(Person selectedPerson)
        {
            //Er moet iemand geselecteerd zijn in de combobox, zo niet foutmelding "selecteer lid"
            //De waarden van deze persoon worden ingevuld in de textboxen
        }

        public void CommitMember()
        {
            Person personToAdd = new Person();
            personToAdd.Firstname = TxtFirstname;
            personToAdd.Lastname = TxtLastname;
            personToAdd.Email = TxtEmail;

            if (personToAdd.IsGeldig())
            {
                DatabaseOperations.AddPerson(personToAdd);
            }

            ReLoadMemberList();
            
        }

        
    }
}
