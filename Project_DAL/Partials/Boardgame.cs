using Newtonsoft.Json;
using Project_DAL.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DAL
{
    public partial class Boardgame : BaseClass
    {

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "JsonTitel" && string.IsNullOrWhiteSpace(JsonTitel))
                {
                    return $"Geen titel meegekregen" + Environment.NewLine;
                }
                if (columnName == "JsonMinPlayers" && JsonMinPlayers <= 0)
                {
                    return $"Minimum aantal spelers moet groter zijn dan 0" + Environment.NewLine;
                }
                if (columnName == "JsonMaxPlayers" && JsonMaxPlayers <= 0)
                {
                    return $"Maximum aantal spelers moet een positief getal zijn" + Environment.NewLine;
                }
                if (columnName == "JsonMinPlayingTime" && JsonMinPlayingTime <= 0)
                {
                    return $"De minimum speelduur moet een positief getal zijn" + Environment.NewLine;
                }
                return "";
            }
        }

        [JsonProperty("name")]
        public string JsonTitel { get => Titel; set => Titel = value; }
        [JsonProperty("minPlayers")]
        public int? JsonMinPlayers { get => MinNumberOfPlayers; set => MinNumberOfPlayers = value; }
        [JsonProperty("maxPlayers")]
        public int? JsonMaxPlayers { get => MaxNumberOfPlayers; set => MaxNumberOfPlayers = value; }
        [JsonProperty("playingTime")]
        public int? JsonMinPlayingTime { get => MinPlayingTime; set => MinPlayingTime = value; }

    }
}
