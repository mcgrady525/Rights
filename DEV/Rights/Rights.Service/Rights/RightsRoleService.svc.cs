using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Rights.IService.Rights;
using Rights.Entity.Common;
using Tracy.Frameworks.Common.Result;
using Rights.Entity.ViewModel;
using Rights.IDao.Rights;
using Rights.DaoFactory;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 角色管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsRoleService : IRightsRoleService
    {
        private static readonly IRightsRoleDao roleDao = Factory.GetRightsRoleDao();

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingRolesResponse>> GetPagingRoles(GetPagingRolesRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingRolesResponse>> 
            {
                ReturnCode= ReturnCodeType.Error,
                Content= new PagingResult<GetPagingRolesResponse>()
            };

            var rs = roleDao.GetPagingRoles(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<PagingResult<GetPagingRoleUsersResponse>> GetPagingRoleUsers(GetPagingRoleUsersRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingRoleUsersResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingRoleUsersResponse>()
            };

            var rs = roleDao.GetPagingRoleUsers(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

    }
}
