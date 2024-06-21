using GraduationProject.DTO;
using GraduationProject.DTO.DTOAccount;
using GraduationProject.Models;
using System.Threading.Tasks;

namespace GraduationProject.Services.IAuthServices
{
    public interface IAuthService
    {

        Task<AuthModel> RegisterAsync(RegisterUser model);
     
           Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
