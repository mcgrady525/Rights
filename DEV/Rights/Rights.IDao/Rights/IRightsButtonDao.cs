using Rights.Entity.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rights.IDao.Rights
{
    /// <summary>
    /// 按钮管理dao接口
    /// </summary>
    public interface IRightsButtonDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsButton item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsButton item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        bool BatchDelete(List<int> ids);

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        TRightsButton GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsButton> GetAll();
    }
}
