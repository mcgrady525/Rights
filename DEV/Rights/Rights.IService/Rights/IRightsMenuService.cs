using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Rights.Entity.Common;
using Rights.Entity.Db;
using Rights.Entity.ViewModel;

namespace Rights.IService.Rights
{
    /// <summary>
    /// 菜单管理service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsMenuService.IRightsMenuService")]
    public interface IRightsMenuService
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<List<TRightsMenu>> GetAll();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceResult<bool> AddMenu(AddMenuRequest request, TRightsUser loginInfo);

    }
}
