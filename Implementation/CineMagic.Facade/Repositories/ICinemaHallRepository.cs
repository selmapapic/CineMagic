using CineMagic.Facade.Models.CinemaHall;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface ICinemaHallRepository
    {
        Task<CinemaHallGetDetailsRes> GetCinemaHallDetails(CinemaHallGetDetailsReq req);
    }
}
