using ERP.Infrastructure.Entities;

namespace ERP.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(User user);

        Task<int> UpdateUser(User user);

        Task<int> DeleteUser(User user);

        Task<User> GetUser(Guid id);

        Task<User> GetUserByEmail(string id);
    }
}
