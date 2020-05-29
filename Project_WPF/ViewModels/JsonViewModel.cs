using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_WPF.ViewModels
{

    public class JsonViewModel : Screen
    {
        List<Boardgame> jsonGameList = new List<Boardgame>();

        private List<Boardgame> _gameTitles;
        private string _jsonStringText;

        public List<Boardgame> GameTitles
        {
            get { return _gameTitles; }
            set { _gameTitles = value; NotifyOfPropertyChange(() => GameTitles); }
        }

        public string JsonStringText
        {
            get { return _jsonStringText; }
            set
            {
                _jsonStringText = value;
                NotifyOfPropertyChange(() => JsonStringText);
            }
        }

        public void LoadJson()
        {
            WebClient client = new WebClient();
            string boardgames = client.DownloadString("https://bgg-json.azurewebsites.net/collection/mateusenmaarten");
            jsonGameList = JsonConvert.DeserializeObject<List<Boardgame>>(boardgames);

            GameTitles = jsonGameList;
            if (GameTitles.Count > 0)
            {
                JsonStringText = $"Er zijn {GameTitles.Count} spellen opgehaald uit deze JSON string";
            }

        }

        public void JsonToDatabase()
        {
            JsonStringText = "";
            if (GameTitles == null)
            {
                JsonStringText = $"Haal eerst de spellen op uit JSON";
            }
            else
            {
                foreach (Boardgame bg in jsonGameList)
                {
                    if (!DatabaseOperations.GetBoardgames().Contains(bg))
                    {
                        if (bg.IsGeldig())
                        {
                            DatabaseOperations.AddBoardgame(bg);
                            JsonStringText = "De spellen zijn toegevoegd aan de database";
                        }
                        else
                        {
                            JsonStringText = bg.Error;
                        }
                    }
                    else
                    {
                        JsonStringText += $"{bg.Titel} is al opgenomen in de database" + Environment.NewLine;
                    }

                }
            }
            
        }
    }
}
