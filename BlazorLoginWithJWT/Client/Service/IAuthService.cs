using BlazorLoginWithJWT.Shared;
using System.Threading.Tasks;

namespace BlazorLoginWithJWT.Client.Service
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(UserInfo userInfo);

        Task LogoutAsync();
    }
}