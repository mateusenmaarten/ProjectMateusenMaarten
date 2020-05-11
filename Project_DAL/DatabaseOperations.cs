using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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

        public static List<Boardgame> GetBoardgames()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Boardgames;
                return query.ToList();
            }
        }

        public static List<Boardgame> GetSearchedBoardgames(int categoryID, int numberOfPlayers)
        {
            using (GameClubEntities gameClubEntities = new GameClubEntities())
            {
                var query = gameClubEntities.Boardgames
                    .Where(x => x.Boardgame_Category.Any(y => y.Category_id == categoryID))
                    .Where(z => z.MinNumberOfPlayers <= numberOfPlayers && z.MaxNumberOfPlayers >= numberOfPlayers);
                return query.ToList();
            }
        }
        public static List<Category> GetCategories()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Categories;
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

        public static int AddPlaySession(PlaySession session, ref int session_id)
        {
            session_id = 0;
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.PlaySessions.Add(session);
                    int changes = gameClubEntities.SaveChanges();
                    session_id = session.PlaySession_id;
                    return changes;
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
            
        }

        public static int AddPlayer(int personID, int sessionID)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    Player player = new Player();
                    player.Person_id = personID;
                    player.PlaySession_id = sessionID;
                    gameClubEntities.Players.Add(player);
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
