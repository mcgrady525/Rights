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
using Rights.Entity.Db;
using Tracy.Frameworks.Common.Extends;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 用户管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsUserService : IRightsUserService
    {
        private static readonly IRightsUserDao userDao = Factory.GetRightsUserDao();

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns>分页结果集</returns>
        public ServiceResult<PagingResult<GetPagingUsersResponse>> GetPagingUsers(GetPagingUsersRequest request)
        {
            var result = new ServiceResult<PagingResult<GetPagingUsersResponse>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new PagingResult<GetPagingUsersResponse>()
            };

            var rs = userDao.GetPagingUsers(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddUser(AddUserRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var item = new TRightsUser
            {
                UserId = request.UserId,
                Password = "123456".To32bitMD5(),//默认密码为123456
                UserName = request.UserName,
                IsChangePwd = request.IsChangePwd,
                EnableFlag = request.EnableFlag,
                CreatedBy = loginInfo.Id,
                CreatedTime = DateTime.Now
            };
            var rs = userDao.Insert(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
