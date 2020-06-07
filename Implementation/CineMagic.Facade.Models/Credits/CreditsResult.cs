using CineMagic.Facade.Models.Actor;
using CineMagic.Facade.Models.Crew;
using System.Collections.Generic;

namespace CineMagic.Facade.Models.Credits
{
    public class CreditsResult
    {
        public int Id { get; set; }
        public List<ActorDb> cast { get; set; }
        public List<CrewDb> crew { get; set; }
    }
}
