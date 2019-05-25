using DP.V2.Core.Common.Base;
using DP.V2.Core.Common.Mapper;
using DP.V2.Core.Data.DataModel;
using DP.V2.Core.Data.Interface;
using DP.V2.PLG.Identity.Businesses.Dtos;
using DP.V2.PLG.Identity.Businesses.Requests;
using DP.V2.PLG.Identity.Businesses.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace DP.V2.PLG.Identity.Businesses
{
    public class IdentityBusiness : IIdentityBusiness
    {
        private readonly IRepository<SysUser> _repoUser;
        private readonly IRepository<SysRole> _repoRole;
        public IdentityBusiness(IRepository<SysUser> repoUser, IRepository<SysRole> repoRole)
        {
            _repoUser = repoUser;
            _repoRole = repoRole;
        }

        public Task<BaseResponse<string>> CreateOrUpdateUser(CreateOrUpdateUserRequest request)
        {
            string id = "";
            if (!request.Data.Id.HasValue)
            {
                var itemNew = AutoMapper.Map<UserDto, SysUser>(request.Data);
                id = _repoUser.Insert(itemNew).Id.ToString();
            }
            else
            {
                var item = _repoUser.FindById(request.Data.Id.Value);
                item.Email = request.Data.Email;
                item.Fullname = request.Data.Fullname;
                id = _repoUser.Update(item).Id.ToString();
            }

            return Task.FromResult(new BaseResponse<string>
            {
                Data = id
            });
        }

        public Task<GetAllResponse> GetAll()
        {
            var response = new GetAllResponse();

            response.Data = _repoUser.GetAll().Select(x => AutoMapper.Map<SysUser, UserDto>(x));

            return Task.FromResult(response);
        }

        public Task<GetUserResponse> GetUser(GetUserRequest request)
        {
            var user = _repoUser.FindById(request.Id);

            if(user == null)
            {
                return Task.FromResult(new GetUserResponse {
                    ErrorCode = -1,
                    Errors = "User not found"
                });
            }

            var userDto = AutoMapper.Map<SysUser, UserDto>(user);

            if(user.RoleId.HasValue)
            {
                userDto.RoleName = _repoRole.FindById(user.RoleId.Value).Name;
            }

            return Task.FromResult(new GetUserResponse
            {
                Data = userDto
            });
        }

        public Task<BaseResponse<bool>> RemoveUser(RemoveUserRequest request)
        {
            if (request.Id == null)
            {
                return Task.FromResult(new BaseResponse<bool>
                {
                    Data = false
                });
            }
            else
            {
                _repoUser.Remove(request.Id);

                return Task.FromResult(new BaseResponse<bool>
                {
                    Data = true
                });
            }
        }
    }
}
