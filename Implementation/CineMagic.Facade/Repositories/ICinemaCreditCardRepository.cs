using CineMagic.Facade.Models.CinemaCreditCard;
using System.Threading.Tasks;

namespace CineMagic.Facade.Repositories
{
    public interface ICinemaCreditCardRepository
    {
        Task<CinemaCreditCardGetDetailsRes> GetCreditCard(CinemaCreditCardGetDetailsReq req);
    }
}
