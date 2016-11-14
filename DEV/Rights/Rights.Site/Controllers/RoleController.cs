using Rights.Entity.Common;
using Rights.Entity.ViewModel;
using Rights.IService.Rights;
using Rights.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Const;
using Tracy.Frameworks.Common.Extends;

namespace Rights.Site.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 角色列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingRoles(GetPagingRolesRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingRolesRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetPagingRoles(request);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateFormat.DATETIME) + "}";
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 获取角色下的用户列表(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPagingRoleUsers(GetPagingRoleUsersRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingRoleUsersRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetPagingRoleUsers(request);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateFormat.DATETIME) + "}";
                }
            }

            return Content(result);        
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AddRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            if (request == null)
            {
                request = new AddRoleRequest();
            }

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.AddRole(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "新增成功!";
                }
                else
                {
                    msg = rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

	}
}