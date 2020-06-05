using CineMagic.Facade.Models.Seat;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IAvailableSeatsRepository
    {
        Task<AvailableSeatGetDetailsRes> GetAvailableSeatDetails(AvailableSeatGetDetailsReq req);
    }
}
