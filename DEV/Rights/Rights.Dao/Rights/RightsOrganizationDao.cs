using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.Entity.Db;
using Rights.IDao.Rights;
using Rights.Common.Helper;
using Dapper;
using Tracy.Frameworks.Common.Extends;

namespace Rights.Dao.Rights
{
    /// <summary>
    /// 组织机构数据访问Dao
    /// </summary>
    public class RightsOrganizationDao : IRightsOrganizationDao
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="item">待插入的记录</param>
        public void Insert(TRightsOrganization item)
        {
            using (var conn= DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"INSERT INTO dbo.t_rights_organization VALUES  ( @OrgName ,@ParentId ,@Code ,@OrganizationType ,@Sort ,@EnableFlag ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);", 
                                new 
                                {
                                    @OrgName= item.Name,
                                    @ParentId= item.ParentId,
                                    @Code= item.Code,
                                    @OrganizationType= item.OrganizationType,
                                    @Sort= item.Sort,
                                    @EnableFlag= item.EnableFlag,
                                    @CreatedBy= item.CreatedBy,
                                    @CreatedTime= item.CreatedTime,
                                    @LastUpdatedBy= item.LastUpdatedBy,
                                    @LastUpdatedTime= item.LastUpdatedTime
                                });
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item">待更新的记录</param>
        /// <returns></returns>
        public bool Update(TRightsOrganization item)
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"UPDATE dbo.t_rights_organization SET name= @OrgName, parent_id= @ParentId, sort= @Sort WHERE id= @Id;",
                                             new { @Id = item.Id, @OrgName = item.Name, @ParentId = item.ParentId, @Sort = item.Sort });
                if (effectRows > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除记录的id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var result = false;
            using (var conn = DapperHelper.CreateConnection())
            {
                var effectRows = conn.Execute(@"DELETE FROM dbo.t_rights_organization WHERE id= @Id;", new { @Id = id });
                if (effectRows > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 依id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public TRightsOrganization GetById(int id)
        {
            TRightsOrganization result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsOrganization>(@"SELECT org.id AS Id, org.name AS NAME, org.parent_id AS ParentId, org.code AS Code, org.organization_type AS OrganizationType,
                                                            org.sort AS Sort, org.enable_flag AS EnableFlag, org.created_by AS CreatedBy, org.created_time AS CreatedTime,
                                                            org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime 
                                                            FROM dbo.t_rights_organization AS org
                                                            WHERE org.id= @Id", new { @Id = id });
                result = query.FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TRightsOrganization> GetAll()
        {
            List<TRightsOrganization> result = null;
            using (var conn = DapperHelper.CreateConnection())
            {
                var query = conn.Query<TRightsOrganization>(@"SELECT org.id AS Id, org.name AS NAME, org.parent_id AS ParentId, org.code AS Code, org.organization_type AS OrganizationType,
                                                            org.sort AS Sort, org.enable_flag AS EnableFlag, org.created_by AS CreatedBy, org.created_time AS CreatedTime,
                                                            org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime 
                                                            FROM dbo.t_rights_organization AS org
                                                            ORDER BY org.code, org.sort;");
                result = query.ToList();
            }

            return result;
        }
    }
}
