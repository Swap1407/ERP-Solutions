using ERP.Business.Services.Interfaces;
using ERP.Infrastructure.Entities;
using ERP.Infrastructure.Interfaces;

namespace ERP.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> DeleteUser(User user)
        {
            _unitOfWork.Users.Delete(user);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _unitOfWork.Users.GetByIDAsync(id);
        }

        public async Task<int> RegisterUser(User user)
        {
            _unitOfWork.Users.Insert(user);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateUser(User user)
        {
            _unitOfWork.Users.Update(user);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var users = await Task.Run(() => _unitOfWork.Users.Get(user => user.Email == email));
            return users.FirstOrDefault();
        }
    }
}
