using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace TestsConfigurator.Models.API.Models
{
    public class Game
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int added { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int games_count { get; set; }
        public string image_background { get; set; }
        public List<Game> games { get; set; }
    }

    public class Genres
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<Genre> results { get; set; }
    }
}
