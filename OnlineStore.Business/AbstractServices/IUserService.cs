using OnlineStore.Business.Dtos;
using OnlineStore.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Business.AbstractServices
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(UserAddDto userAddDto);

        Task<UserInfoDto> LoginUser(UserLoginDto userLoginDto);

        Task<List<UserListDto>> GetAllUsers();
        UserDetailDto GetUserDetail(int id);
    }
}
