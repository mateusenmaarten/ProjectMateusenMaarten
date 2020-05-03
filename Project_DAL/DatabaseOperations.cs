using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public static class DatabaseOperations
    {
        public static List<Person> GetPeople()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.People;
                return query.ToList();
            }
        }
    }
}
