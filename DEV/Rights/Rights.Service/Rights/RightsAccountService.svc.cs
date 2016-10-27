using Rights.Entity.Common;
using Rights.Entity.Db;
using Rights.Entity.ViewModel;
using Rights.IService.Rights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;
using Rights.DaoFactory;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 登陆相关的服务
    /// </summary>
    public class RightsAccountService : IRightsAccountService
    {
        private static readonly IRightsAccountDao accountDao = Factory.GetRightsAccountDao();

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServiceResult<TRightsUser> CheckLogin(CheckLoginRequest request)
        {
            var result = new ServiceResult<TRightsUser> 
            {
                ReturnCode= ReturnCodeType.Error
            };

            var user = accountDao.CheckLogin(request);
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = user;

            return result;
        }

    }
}
