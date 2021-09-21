using System.Threading.Tasks;
using BlazorWASMProject.Models;

namespace BlazorWASMProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }
}