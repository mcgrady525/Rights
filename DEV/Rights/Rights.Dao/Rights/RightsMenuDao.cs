using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;
using Rights.Entity.Db;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Helpers;
using Dapper;
using Rights.Common.Helper;

namespace Rights.Dao.Rights
{
    /// <summary>
    /// 菜单管理dao
    /// </summary>
    public class RightsMenuDao : IRightsMenuDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsMenu item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"INSERT INTO dbo.t_rights_menu VALUES  ( @Name ,@ParentId ,@Code ,@Url ,@Icon ,@Sort ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
                if (effectedRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public bool Update(TRightsMenu item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"UPDATE dbo.t_rights_menu SET name=@Name, url= @Url, icon= @Icon, sort=@Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime
                    WHERE id= @Id;", item);
                if (effectedRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_menu WHERE id= @Id;", new { @Id = id });
                if (effectedRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id列表</param>
        /// <returns></returns>
        public bool BatchDelete(List<int> ids)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_menu WHERE id IN @Ids;", new { @Ids = ids });
                if (effectedRows > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TRightsMenu GetById(int id)
        {
            var result = new TRightsMenu();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsMenu>(@"SELECT menu.parent_id AS ParentId,
                    menu.created_by AS CreatedBy,
                    menu.created_time AS CreatedTime,
                    menu.last_updated_by AS LastUpdatedBy,
                    menu.last_updated_time AS LastUpdatedTime,
                    * FROM dbo.t_rights_menu AS menu
                    WHERE menu.id= @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsMenu> GetAll()
        {
            var result = new List<TRightsMenu>();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsMenu>(@"SELECT menu.parent_id AS ParentId,
                    menu.created_by AS CreatedBy,
                    menu.created_time AS CreatedTime,
                    menu.last_updated_by AS LastUpdatedBy,
                    menu.last_updated_time AS LastUpdatedTime,
                    * FROM dbo.t_rights_menu AS menu
                    ORDER BY menu.parent_id, menu.sort;").ToList();
            }

            return result;
        }
    }
}
