using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IService.Rights;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Rights.Entity.Db;
using Rights.Entity.Common;
using Rights.IDao.Rights;
using Rights.DaoFactory;
using Tracy.Frameworks.Common.Extends;
using Rights.Entity.ViewModel;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 菜单管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsMenuService : IRightsMenuService
    {
        //注入dao
        private static readonly IRightsMenuDao menuDao = Factory.GetRightsMenuDao();

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public ServiceResult<List<TRightsMenu>> GetAll()
        {
            var result = new ServiceResult<List<TRightsMenu>>
            {
                ReturnCode = ReturnCodeType.Error,
                Content = new List<TRightsMenu>()
            };

            var rs = menuDao.GetAll();
            result.ReturnCode = ReturnCodeType.Success;
            result.Content = rs;

            return result;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ServiceResult<bool> AddMenu(AddMenuRequest request, TRightsUser loginInfo)
        {
            var result = new ServiceResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            var currentTime = DateTime.Now;
            var menu = new TRightsMenu
            {
                Name = request.Name,
                ParentId = request.ParentId,
                Code = request.Code,
                Url = request.Url,
                Icon = request.Icon,
                Sort = request.Sort,
                CreatedBy = loginInfo.Id,
                CreatedTime = currentTime,
                LastUpdatedBy = loginInfo.Id,
                LastUpdatedTime = currentTime
            };
            var rs = menuDao.Insert(menu);
            if (rs == true)
            {
                result.ReturnCode = ReturnCodeType.Success;
                result.Content = true;
            }

            return result;
        }
    }
}
