using CineMagic.Facade.Models.CinemaCreditCard;
using Microsoft.AspNetCore.Identity;

namespace CineMagic.Facade.Models.User
{
    public class UserGetDetailsRes
    {
        public IdentityUser User { get; set; }
        public CinemaCreditCardGetDetailsRes CinemaCreditCard { get; set; }
    }
}
