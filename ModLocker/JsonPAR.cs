using Newtonsoft.Json;
using System.Collections.Generic;


namespace ModLocker
{
    public class Mod
    {
        [JsonProperty("Enabled")]
        public bool Enabled;

        [JsonProperty("LastUpdate")]
        public string LastUpdate;

        [JsonProperty("Path")]
        public string Path;

        [JsonProperty("Priority")]
        public int Priority;

        [JsonProperty("PublishID")]
        public int PublishID;

        [JsonProperty("Title")]
        public string Title;

        [JsonProperty("WorkshopID")]
        public int WorkshopID;
    }

    public class Root
    {
        [JsonProperty("Mods")]
        public List<Mod> Mods;

        [JsonProperty("Unsub")]
        public List<object> Unsub;
    }


}
