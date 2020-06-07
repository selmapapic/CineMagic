using CineMagic.Facade.Models.Credits;
using CineMagic.Facade.Models.Crew;
using System.Collections.Generic;

namespace CineMagic.Facade.Models.Actor
{
    public class RootObjectActors
    {
        public List<ActorDb> Cast { get; set; }
        public List<CrewDb> Crew { get; set; }
    }
}
