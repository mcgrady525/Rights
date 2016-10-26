using Rights.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Rights.IService.Rights
{
    /// <summary>
    /// 组织机构service接口
    /// </summary>
    [ServiceContract(ConfigurationName = "RightsOrganizationService.IRightsOrganizationService")]
    public interface IRightsOrganizationService
    {
        /// <summary>
        /// 插入机构
        /// </summary>
        /// <param name="item">待插入的记录</param>
        [OperationContract]
        void Insert(TRightsOrganization item);

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        [OperationContract]
        bool Update(TRightsOrganization item);

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        [OperationContract]
        bool Delete(int id);

        /// <summary>
        /// 依id查询机构
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [OperationContract]
        TRightsOrganization GetById(int id);

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TRightsOrganization> GetAll();
    }
}
