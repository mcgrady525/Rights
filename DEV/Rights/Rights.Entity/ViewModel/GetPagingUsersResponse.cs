using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rights.Entity.ViewModel
{
    /// <summary>
    /// 查询用户列表(分页)返回结果
    /// </summary>
    [Serializable]
    public class GetPagingUsersResponse
    {
        /// <summary>
        /// 登录id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 拥用角色，多个角色以','分隔
        /// </summary>
        public string RolesName { get; set; }

        public string RolesId { get; set; }

        /// <summary>
        /// 所属机构，多个机构以','分隔
        /// </summary>
        public string OrgsName { get; set; }

        public string OrgsId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnableFlag { get; set; }

        /// <summary>
        /// 是否改密
        /// </summary>
        public bool IfChangePwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }
}
