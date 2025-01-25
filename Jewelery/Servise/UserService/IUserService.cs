using Jewelery.ViewModels.DTO.User;

namespace Jewelery.Servise.UserService
{
    public interface IUserService
    {
        public Task<List<UserListDTOVM>> GetAllUser(int lang);
        public void GetUserByIdForAdminManagment();

        public void GetAllAdmin();

    }
}
