using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;
using Rights.Entity.Db;
using Rights.Entity.ViewModel;
using Rights.Common.Helper;
using Dapper;

namespace Rights.Dao.Rights
{
    /// <summary>
    /// 登陆相关
    /// </summary>
    public class RightsAccountDao : IRightsAccountDao
    {
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns>成功返回实体对象，失败返回null</returns>
        public TRightsUser CheckLogin(CheckLoginRequest request)
        {
            TRightsUser user = null;
            using (var conn= DapperHelper.CreateConnection())
            {
                user= conn.Query<TRightsUser>(@"SELECT u.id, u.user_id AS UserId, u.password, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                                        u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime
                                        FROM dbo.t_rights_user AS u
                                        WHERE u.user_id= @UserId AND u.password= @Password;", new { @UserId = request.loginId, @Password= request.loginPwd }).FirstOrDefault();
            }

            return user;
        }
    }
}
