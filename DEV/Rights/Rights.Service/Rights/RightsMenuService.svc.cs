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
    }
}
