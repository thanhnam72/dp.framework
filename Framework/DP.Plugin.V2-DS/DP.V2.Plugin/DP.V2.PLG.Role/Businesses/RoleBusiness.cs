using DP.V2.Core.Common.Mapper;
using DP.V2.Core.Data.DataModel;
using DP.V2.Core.Data.Interface;
using DP.V2.Core.WebApi.Dependencies;
using DP.V2.PLG.Role.Businesses.Dtos;
using DP.V2.PLG.Role.Businesses.Requests;
using DP.V2.PLG.Role.Businesses.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DP.V2.PLG.Role.Businesses
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IRepository<SysRole> _repoRole;
        private readonly IRepository<SysPermission> _repoPermis;
        public RoleBusiness(IRepository<SysRole> repoRole, IRepository<SysPermission> repoPermis)
        {
            _repoRole = repoRole;
            _repoPermis = repoPermis;
        }

        public Task<CreateOrUpdateRoleResponse> CreateOrUpdateRole(CreateOrUpdateRoleRequest request)
        {
            SysRole role;

            if (!request.Data.Id.HasValue) // insert
            {
                var roleEntity = AutoMapper.Map<RoleDto, SysRole>(request.Data);
                role = _repoRole.Insert(roleEntity);
            }
            else //update
            {
                role = _repoRole.FindById(request.Data.Id.Value);

                role.Name = request.Data.Name;
                role.Descriptions = request.Data.Descriptions;

                _repoRole.Update(role);

                _repoPermis.BulkRemove(x => x.RoleId.Equals(request.Data.Id.Value), false);
            }

            var permisDatas = new List<SysPermission>();

            foreach (var item in request.Data.PermissionData)
            {
                var permisEntity = AutoMapper.Map<PermissionDto, SysPermission>(item);
                permisEntity.RoleId = role.Id;
                permisDatas.Add(permisEntity);
            }

            _repoPermis.BulkInsert(permisDatas.ToArray());

            return Task.FromResult(new CreateOrUpdateRoleResponse {
                Data = true
            });
        }

        public Task<GetAllRoleResponse> GetAllRole()
        {
            var results = _repoRole.GetAll().Select(x => new RoleDto {
                Id = x.Id,
                Name = x.Name,
                Descriptions = x.Descriptions
            });

            return Task.FromResult(new GetAllRoleResponse()
            {
                Data = results
            });
        }

        public Task<GetFunctionByRoleResponse> GetFunctionByRole(GetRoleRequest request)
        {
            if (!request.Id.HasValue)
            {
                return Task.FromResult(new GetFunctionByRoleResponse()
                {
                    Errors = "Không có thông tin Id"
                });
            }

            IRepository<SysFunction> repoFunc = DependencyProvider.Resolve<IRepository<SysFunction>>();

            var results = _repoPermis.FindAll(x => x.RoleId.Equals(request.Id.Value)).AsEnumerable();

            var lstFunction = repoFunc.FindAll(x => results.Any(e => e.FnCd.Equals(x.FnCd))).AsEnumerable();

            return Task.FromResult(new GetFunctionByRoleResponse()
            {
                Data = AutoMapper.MapList<SysFunction, FunctionDto>(lstFunction)
            });
        }

        public Task<GetRoleResponse> GetRole(GetRoleRequest request)
        {
            if(!request.Id.HasValue)
            {
                return Task.FromResult(new GetRoleResponse()
                {
                    Errors = "Không có thông tin Id"
                });
            }

            var record = _repoRole.FindOne(x => x.Id.Equals(request.Id.Value));

            var role = new RoleDto
            {
                Id = record.Id,
                Name = record.Name,
                Descriptions = record.Descriptions
            };

            var permisData = _repoPermis.FindAll(x => x.RoleId.Equals(record.Id))
                                        .Select(x => AutoMapper.Map<SysPermission, PermissionDto>(x));

            role.PermissionData = permisData.ToList();

            return Task.FromResult(new GetRoleResponse()
            {
                Data = role
            });
        }

        public Task<RemoveRoleResponse> RemoveRole(RemoveRoleRequest request)
        {
            if(request.Ids.Count == 0)
            {
                return Task.FromResult(new RemoveRoleResponse()
                {
                    Data = false,
                    Errors = "Không tìm thấy thông tin để xóa"
                });
            }

            _repoRole.BulkRemove(x => request.Ids.Any(e => e.Equals(x.Id)));

            return Task.FromResult(new RemoveRoleResponse()
            {
                Data = true
            });
        }
    }
}
