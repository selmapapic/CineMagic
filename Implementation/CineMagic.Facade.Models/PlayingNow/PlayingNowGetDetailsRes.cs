using CineMagic.Facade.Models.Projection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CineMagic.Facade.Models.PlayingNow
{
    public class PlayingNowGetDetailsRes
    {
        public IList<ProjectionGetDetailsRes> MondayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> TuesdayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> WednesdayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> ThursdayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> FridayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> SaturdayProjections { get; set; }
        public IList<ProjectionGetDetailsRes> SundayProjections { get; set; }
    }
}
