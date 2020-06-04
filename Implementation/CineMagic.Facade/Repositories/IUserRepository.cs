using System;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface IUserRepository
    {
        Task<Boolean> DoesUserExists();

    }
}
