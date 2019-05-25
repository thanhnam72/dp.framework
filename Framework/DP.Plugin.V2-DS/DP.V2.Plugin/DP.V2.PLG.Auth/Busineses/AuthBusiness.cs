using DP.V2.Core.Common.Base;
using DP.V2.Core.Common.Ultilities;
using DP.V2.Core.Data.DataModel;
using DP.V2.Core.Data.Interface;
using DP.V2.PLG.Auth.Busineses.Dtos;
using DP.V2.PLG.Auth.Busineses.Requests;
using System;
using System.Threading.Tasks;

namespace DP.V2.PLG.Auth.Busineses
{
    public class AuthBusiness : IAuthBusiness
    {
        private IRepository<SysUser> _repoUser;
        public AuthBusiness(IRepository<SysUser> repoUser)
        {
            _repoUser = repoUser;
        }
        public Task<BaseResponse<TokenDto>> GetToken(GetTokenRequest request)
        {
            string hashPassword = PasswordSecurityHelper.GetHashedPassword(request.Password);
            SysUser user = _repoUser.FindOne(x => x.Username.Equals(request.Username) && x.Password.Equals(hashPassword));

            if (user == null)
            {
                return Task.FromResult(new BaseResponse<TokenDto>
                {
                    Data = null,
                    Errors = "Tài khoản hay mật khẩu không hợp lệ",
                    ErrorCode = -1
                });
            }

            user.Token = TokenSecurityHelper.GenerateToken(request.Username, request.Password, "120.0.0.1", "", DateTime.Now.Ticks);
            user.TokenExp = DateTime.Now.AddMinutes(30);
            user.LastLogin = DateTime.Now;

            _repoUser.Update(user);

            return Task.FromResult(new BaseResponse<TokenDto>
            {
                Data = new TokenDto
                {
                    Token = user.Token,
                    Username = user.Username,
                    Expired = user.TokenExp.Value,
                    Id = user.Id.ToString()
                }
            });
        }
    }
}
