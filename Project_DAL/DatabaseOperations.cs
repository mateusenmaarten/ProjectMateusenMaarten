using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

        public static int AddBoardgame(Boardgame bg)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.Boardgames.Add(bg);
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
            
        }
        public static int AddPerson(Person person, ref int personID)
        {
            personID = 0;
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.People.Add(person);
                    personID = person.Person_id;
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int EditPerson(Person person)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.Entry(person).State = EntityState.Modified;
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }
    }
}
