using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.AuthService
{
    public interface IAuthService
    {
        Task<object> Login(string email, string password);

        Task<object> Register(string email, string password, string name);
    }
}