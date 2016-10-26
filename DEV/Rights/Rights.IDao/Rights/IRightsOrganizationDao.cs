using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.Entity.Db;

namespace Rights.IDao.Rights
{
    /// <summary>
    /// 组织机构数据访问Dao接口
    /// </summary>
    public interface IRightsOrganizationDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        void Insert(TRightsOrganization item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsOrganization item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TRightsOrganization GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsOrganization> GetAll();



    }
}
