using System.Threading.Tasks;

namespace LearningMaterials.Controllers
{
    public interface IAccountRepository
    {
        Task RegisterUser(RegisterUserDto dto);
        Task<string> GenerateJwt(LoginDto dto);
    }
}