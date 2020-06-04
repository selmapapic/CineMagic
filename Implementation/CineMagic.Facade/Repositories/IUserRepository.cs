using CineMagic.Facade.Models.Reservation;
using CineMagic.Facade.Models.User;
using System;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IUserRepository
    {
        Task<Boolean> DoesUserExists();
        Task<UserGetDetailsRes> GetCurrentUser();
        Task CreateReservationAsync(CheckReservationModel model);

    }
}
