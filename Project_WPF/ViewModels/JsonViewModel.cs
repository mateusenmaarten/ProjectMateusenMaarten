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

    /*
     Tijd over : tekstvak om url van boardgamegeek in te copy pasten waardoor de spellen worden geplaatst in db.
     Eerst een persoon selecteren uit een lijst zodat de boardgame id's ineens bij de juiste owner terecht komen.
     */
    public class JsonViewModel : Screen
    {
        List<Boardgame> jsonGameList = new List<Boardgame>();

        private List<Boardgame> _gameTitles;

        public List<Boardgame> GameTitles
        {
            get { return _gameTitles; }
            set { _gameTitles = value; NotifyOfPropertyChange(() => GameTitles); }
        }

        private string _jsonStringText;

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

        //Nog ervoor zorgen dat de spellen niet dubbel kunnen worden toegevoegd (ze krijgen gewoon een ander id dan)
        //Eerst lijst van (Boardgames) titels ophalen uit db en enkel spel toevoegen als de tital nog niet in deze lijst zit? 
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
