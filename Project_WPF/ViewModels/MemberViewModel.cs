using Caliburn.Micro;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_WPF.ViewModels
{
   

    public class MemberViewModel : Screen
    {
        public BindableCollection<Person> Persons { get; private set; }

        public MemberViewModel()
        {
            List<Person> PersonList = DatabaseOperations.GetPeople();
            Persons = new BindableCollection<Person>(PersonList);
        }

    }
}
