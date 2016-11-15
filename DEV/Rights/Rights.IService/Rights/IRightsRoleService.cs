using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Rights.Entity.Common;
using Rights.Entity.Db;
using Rights.Entity.ViewModel;
using Tracy.Frameworks.Common.Result;

namespace Rights.IService.Rights
{
    [ServiceContract(ConfigurationName = "RightsRoleService.IRightsRoleService")]
    public interface IRightsRoleService
    {
        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingRolesResponse>> GetPagingRoles(GetPagingRolesRequest request);

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<PagingResult<GetPagingRoleUsersResponse>> GetPagingRoleUsers(GetPagingRoleUsersRequest request);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddRole(AddRoleRequest request, TRightsUser loginInfo);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> EditRole(EditRoleRequest request, TRightsUser loginInfo);

    }
}
