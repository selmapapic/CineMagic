using CineMagic.Facade.Models.Projection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Models.PlayingNow
{
    public class PlayingNowGetDetailsRes
    {
        public IList<ProjectionRes> MondayProjections { get; set; }
        public IList<ProjectionRes> TuesdayProjections { get; set; }
        public IList<ProjectionRes> WednesdayProjections { get; set; }
        public IList<ProjectionRes> ThursdayProjections { get; set; }
        public IList<ProjectionRes> FridayProjections { get; set; }
        public IList<ProjectionRes> SaturdayProjections { get; set; }
        public IList<ProjectionRes> SundayProjections { get; set; }
    }
}
