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
            //新增用户前需要检查userId是否存在
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var existUser = userDao.GetByUserId(request.UserId);
            if (existUser != null)
            {
                result.Message = "已存在该用户,请更换其它用户id!";
                return result;
            }

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

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> EditUser(EditUserRequest request, TRightsUser loginInfo)
        {
            //先要检查新的userId是否已经存在，不存在才能继续修改
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var user = userDao.GetByUserId(request.NewUserId);
            if (request.NewUserId != request.OriginalUserId && user != null)
            {
                result.Message = "已存在该用户,请更换其它用户id!";
                return result;
            }

            var item = new TRightsUser
            {
                Id = request.Id,
                UserId = request.NewUserId,
                UserName = request.NewUserName,
                EnableFlag = request.EnableFlag,
                IsChangePwd = request.IsChangePwd,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = DateTime.Now
            };
            var rs = userDao.Update(item);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
