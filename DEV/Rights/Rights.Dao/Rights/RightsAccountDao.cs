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
        /// 当前用户可以访问的所有菜单
        /// </summary>
        private static List<TRightsMenu> UserMenus = new List<TRightsMenu>();

        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns>成功返回实体对象，失败返回null</returns>
        public TRightsUser CheckLogin(CheckLoginRequest request)
        {
            TRightsUser user = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                user = conn.Query<TRightsUser>(@"SELECT u.id, u.user_id AS UserId, u.password, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
                                        u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime
                                        FROM dbo.t_rights_user AS u
                                        WHERE u.user_id= @UserId AND u.password= @Password;", new { @UserId = request.loginId, @Password = request.loginPwd }).FirstOrDefault();
            }

            return user;
        }

        /// <summary>
        /// 获取指定父菜单下的所有子菜单
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="menuParentId">菜单parentId</param>
        /// <returns></returns>
        public List<TRightsMenu> GetAllChildrenMenu(int userId, int menuParentId)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsMenu, TRightsRoleMenuButton, TRightsUserRole, TRightsUser, TRightsMenu>(@"SELECT menu.id, menu.name, menu.parent_id AS ParentId, menu.code, menu.url, menu.icon,menu.sort,
                menu.created_by AS CreatedBy, menu.created_time AS CreatedTime,
                menu.last_updated_by AS LastUpdatedBy, menu.last_updated_time AS LastUpdatedTime,* 
                FROM dbo.t_rights_menu AS menu
                LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON menu.id= roleMenuButton.menu_id
                LEFT JOIN dbo.t_rights_user_role AS userRole ON roleMenuButton.role_id = userRole.role_id
                LEFT JOIN dbo.t_rights_user AS u ON userRole.user_id = u.id
                WHERE u.id= @UserId
                ORDER BY menu.parent_id, menu.sort;", (menu, roleMenuButton, userRole, user) =>
                {
                    return menu;
                }, new { @UserId = userId });

                UserMenus = query.ToList();
            }

            return GetAllChildrenMenuRecursion(menuParentId).ToList();
        }

        /// <summary>
        /// 递归获取所有子菜单
        /// </summary>
        /// <param name="menuParentId"></param>
        /// <returns></returns>
        public IEnumerable<TRightsMenu> GetAllChildrenMenuRecursion(int menuParentId)
        {
            var query = from item in UserMenus
                        where item.ParentId == menuParentId
                        select item;
            if (query != null && query.Count() > 0 && menuParentId == 0)
            {
                return query;
            }

            return query.ToList().Concat(query.ToList().SelectMany(p => GetAllChildrenMenuRecursion(p.Id)));
        }

    }
}
