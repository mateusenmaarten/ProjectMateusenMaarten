using Caliburn.Micro;
using Project_DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public partial class Person : BaseClass
    {
        public string Fullname
        {
            get { return $"{Firstname} {Lastname}"; }
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == Firstname && string.IsNullOrWhiteSpace(Firstname))
                {
                    return $"Voornaam is een verplicht in te vullen veld";
                }
                if (columnName == Lastname && string.IsNullOrWhiteSpace(Lastname))
                {
                    return $"Achternaam is een verplicht in te vullen veld";
                }
                if (columnName == Email && string.IsNullOrWhiteSpace(Email))
                {
                    return $"Email is een verplicht in te vullen veld";
                }
                return "";
            }

        }
    }
}
