using DP.V2.PLG.Role.Businesses.Requests;
using DP.V2.PLG.Role.Businesses.Responses;
using System.Threading.Tasks;

namespace DP.V2.PLG.Role.Businesses
{
    public interface IRoleBusiness
    {
        Task<GetAllRoleResponse> GetAllRole();
        Task<GetRoleResponse> GetRole(GetRoleRequest request);
        Task<CreateOrUpdateRoleResponse> CreateOrUpdateRole(CreateOrUpdateRoleRequest request);
        Task<RemoveRoleResponse> RemoveRole(RemoveRoleRequest request);
        Task<GetFunctionByRoleResponse> GetFunctionByRole(GetRoleRequest request);
    }
}
