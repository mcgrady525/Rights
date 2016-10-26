using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IService.Rights;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Rights.Entity.Db;
using Rights.IDao.Rights;
using Rights.DaoFactory;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 组织机构service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsOrganizationService: IRightsOrganizationService
    {
        private static readonly IRightsOrganizationDao orgDao = Factory.GetRightsOrganizationDao();

        /// <summary>
        /// 插入机构
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public void Insert(TRightsOrganization item)
        {
            orgDao.Insert(item);
        }

        /// <summary>
        /// 更新机构
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public bool Update(TRightsOrganization item)
        {
            return orgDao.Update(item);
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return orgDao.Delete(id);
        }

        /// <summary>
        /// 依id查询机构
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TRightsOrganization GetById(int id)
        {
            return orgDao.GetById(id);
        }

        /// <summary>
        /// 获取所有机构
        /// </summary>
        /// <returns></returns>
        public List<TRightsOrganization> GetAll()
        {
            return orgDao.GetAll();
        }
    }
}
