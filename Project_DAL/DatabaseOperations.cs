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

        public static Person GetPersonFromPersonID(int personID)
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.People
                .Where(x => x.Person_id == personID);
                
                return query.SingleOrDefault();
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

        public static List<int> GetBoardgameIDsFromPerson(int personID)
        {
            using (GameClubEntities gameClubEntities = new GameClubEntities())
            {
                List<int> boardgameIDs = new List<int>();
                var query = gameClubEntities.Owners;
                foreach (Owner owner in query)
                {
                    if (owner.Person_id == personID)
                    {
                        boardgameIDs.Add(owner.Boardgame_id);
                    }
                }
                return boardgameIDs;
            }
        }

        public static Boardgame GetBoardgameFromBoardgameID(int boardgameID)
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Boardgames
                    .Where(x => x.Boardgame_id == boardgameID);
                return query.SingleOrDefault();
            }
        }

        public static List<Boardgame> GetSearchedBoardgames(int categoryID, int numberOfPlayers)
        {
            using (GameClubEntities gameClubEntities = new GameClubEntities())
            {
                var query = gameClubEntities.Boardgames
                    .Include(y => y.Publisher)
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

        public static List<Publisher> GetPublishers()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Publishers;
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

        public static int AddCategoryToBoardgame(int bgID,List<int> catIDs)
        {
            Boardgame_Category bc;
            
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    foreach (int catID in catIDs)
                    {
                        bc = new Boardgame_Category();
                        bc.Boardgame_id = bgID;
                        bc.Category_id = catID;
                        gameClubEntities.Boardgame_Category.Add(bc);
                    }
                    
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int AddPerson(Person person)
        {
            
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.People.Add(person);
                    
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

        public static int DeletePerson(Person person)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.Entry(person).State = EntityState.Deleted;
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

        public static int AddOwnerToDB(Owner owner)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.Owners.Add(owner);
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static int AddDesignerToDB(Designer designer)
        {
            try
            {
                using (GameClubEntities gameClubEntities = new GameClubEntities())
                {
                    gameClubEntities.Designers.Add(designer);
                    return gameClubEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.FoutLoggen(ex);
                return 0;
            }
        }

        public static List<Owner> GetOwners()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Owners;
                return query.ToList();
            }
        }

        public static List<Designer> GetDesigners()
        {
            using (GameClubEntities entities = new GameClubEntities())
            {
                var query = entities.Designers;
                return query.ToList();
            }
        }
    }
}
