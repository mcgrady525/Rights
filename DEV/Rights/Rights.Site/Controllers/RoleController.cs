using Rights.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //public ActionResult GetPagingRoles(GetPagingRolesRequest request, int page, int rows)
        //{
        //    var result = string.Empty;
        //    if (request == null)
        //    {
        //        request = new GetPagingRolesRequest();
        //    }
        //    request.PageIndex = page;
        //    request.PageSize = rows;

        //    using (var factory = new ChannelFactory<IRightsUserService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var rs = client.GetPagingUsers(request);
        //        if (rs.ReturnCode == ReturnCodeType.Success)
        //        {
        //            result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateFormat.DATETIME) + "}";
        //        }
        //    }

        //    return Content(result);
        //}
	}
}