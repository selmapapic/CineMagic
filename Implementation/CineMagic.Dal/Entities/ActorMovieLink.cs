using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaRes.Dal.Entities
{
    public class ActorMovieLink
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
