using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OpenMissingSteamWorkshopMods
{
    internal class StellarisWorkshopDescriptor
    {

        [JsonInclude]
        public string remote_file_id = "";

        //dont care about
        //[JsonInclude]
        //public string version = "";
        //[JsonInclude]
        //public string name = "";
        //[JsonInclude]
        //public string supported_version = "";
        //[JsonInclude]
        //public string[] tags = new string[0];
    }

    internal class StellarisJson
    {
        [JsonInclude]
        public string game = "";
        [JsonInclude]
        public string name = "";
        [JsonInclude]
        public List<StellarisJsonMod> mods = new ();
    }

    internal class StellarisJsonMod
    {
        [JsonInclude]
        public string displayName = "";
        [JsonInclude]
        public string steamId = "";

        public string GetLink()
        {
            return $"https://steamcommunity.com/sharedfiles/filedetails/?id={steamId}";
        }

        public string GetLinkFileLine()
        {
            return $"{displayName}  :  {GetLink()}";
        }

        public string GetLinkConsole()
        {
            return $"{displayName}  :  \e]8;;{GetLink()}\e\\{steamId}\e]8;;\e\\";
        }

        //dont care about
        //[JsonInclude]
        //public bool enabled = false;
        //[JsonInclude]
        //public string position = "";
    }
}
