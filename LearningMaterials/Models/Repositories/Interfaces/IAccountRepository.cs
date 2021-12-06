using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public interface IAccountRepository
    {
        Task RegisterUser(RegisterUserDto dto);
        Task<string> GenerateJwt(LoginDto dto);
    }
}