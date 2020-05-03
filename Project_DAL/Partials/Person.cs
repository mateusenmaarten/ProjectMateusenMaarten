using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public partial class Person
    {
        public string Fullname
        {
            get { return $"{Firstname} {Lastname}"; }
        }
    }
}
