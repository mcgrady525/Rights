using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;
using Rights.Entity.Db;
using Rights.Common.Helper;
using Dapper;

namespace Rights.Dao.Rights
{
    /// <summary>
    /// 按钮管理dao
    /// </summary>
    public class RightsButtonDao : IRightsButtonDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsButton item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"INSERT INTO dbo.t_rights_button VALUES (@Name ,@Code ,@Icon ,@Sort ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
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
        public bool Update(TRightsButton item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectedRows = conn.Execute(@"UPDATE dbo.t_rights_button SET name=@Name, icon= @Icon, sort=@Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;", item);
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_button WHERE id= @Id;", new { @Id = id });
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
                var effectedRows = conn.Execute(@"DELETE FROM dbo.t_rights_button WHERE id IN @Ids;", new { @Ids = ids });
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
        public TRightsButton GetById(int id)
        {
            var result = new TRightsButton();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT  btn.created_by AS CreatedBy ,
                                                            btn.created_time AS CreatedTime ,
                                                            btn.last_updated_by AS LastUpdatedBy ,
                                                            btn.last_updated_time AS LastUpdatedTime ,
                                                            *
                                                    FROM    dbo.t_rights_button AS btn
                                                    WHERE   btn.id = @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsButton> GetAll()
        {
            var result = new List<TRightsButton>();
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsButton>(@"SELECT  btn.created_by AS CreatedBy ,
                                                            btn.created_time AS CreatedTime ,
                                                            btn.last_updated_by AS LastUpdatedBy ,
                                                            btn.last_updated_time AS LastUpdatedTime ,
                                                            *
                                                    FROM    dbo.t_rights_button AS btn
                                                    ORDER BY btn.sort;").ToList();
            }

            return result;
        }
    }
}
