using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.Entity.Db;
using Tracy.Frameworks.Common.Result;
using Rights.Entity.ViewModel;

namespace Rights.IDao.Rights
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IRightsUserDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        bool Insert(TRightsUser item);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        bool Update(TRightsUser item);

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
        TRightsUser GetById(int id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TRightsUser> GetAll();

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagingResult<GetPagingUsersResponse> GetPagingUsers(GetPagingUsersRequest request);
    }
}
