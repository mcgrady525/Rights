using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;
using Rights.Entity.Db;
using Rights.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Result;
using Rights.Entity.ViewModel;

namespace Rights.Dao.Rights
{
    /// <summary>
    /// 用户
    /// </summary>
    public class RightsUserDao : IRightsUserDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public bool Insert(TRightsUser item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_rights_user VALUES ( @UserId ,@Password ,@UserName ,@IsChangePwd ,@EnableFlag ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", item);
                if (effectRows > 0)
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
        public bool Update(TRightsUser item)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"UPDATE dbo.t_rights_user SET user_id= @UserId, user_name= @UserName, enable_flag= @EnableFlag, is_change_pwd= @IsChangePwd, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;", item);
                if (effectRows > 0)
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_user WHERE id= @Id;", new { @Id = id });
                if (effectRows > 0)
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
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_user WHERE id IN @Ids;", new { @Ids = ids });
                if (effectRows > 0)
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
        public TRightsUser GetById(int id)
        {
            TRightsUser result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                    u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_user AS u WHERE u.id= @Id;", new { @Id = id }).FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsUser> GetAll()
        {
            List<TRightsUser> result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                result = conn.Query<TRightsUser>(@"SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                    u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
                    FROM dbo.t_rights_user AS u;").ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagingResult<GetPagingUsersResponse> GetPagingUsers(GetPagingUsersRequest request)
        {
            PagingResult<GetPagingUsersResponse> result = null;
            using (var conn= DapperHelper.CreateConnection())
            {
                var query = conn.Query<GetPagingUsersResponse>(@"", new { });


            }


            return result;
        }
    }
}
