using System.Threading.Tasks;
using HseAr.Data.DTO;

namespace HseAr.BusinessLayer.Auth
{
    public interface IAuthService
    {
        Task<object> Login(string email, string password);

        Task<object> Register(UserDto item);
    }
}