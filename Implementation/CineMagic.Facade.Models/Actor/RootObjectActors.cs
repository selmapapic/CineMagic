using System.Collections.Generic;

namespace CineMagic.Facade.Models.Actor
{
    public class RootObjectActors
    {
        public int Page { get; set; }
        public List<ActorDb> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
