﻿using Caliburn.Micro;
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

        public override string ToString()
        {
            return Fullname;
        }

        public int PersonID { get; set; }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Firstname" && string.IsNullOrWhiteSpace(Firstname))
                {
                    return $"Voornaam invullen aub" + Environment.NewLine;
                }
                if (columnName == "Lastname" && string.IsNullOrWhiteSpace(Lastname))
                {
                    return $"Achternaam invullen aub" + Environment.NewLine;
                }
                //if (columnName == "Email" && string.IsNullOrWhiteSpace(Email))
                //{
                //    return $"Email is een verplicht in te vullen veld";
                //}
                return "";
            }

        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Fullname == person.Fullname;
        }

        public override int GetHashCode()
        {
            return 558414575 + EqualityComparer<string>.Default.GetHashCode(Fullname);
        }
    }
}
