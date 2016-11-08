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
    /// 用户管理
    /// </summary>
    public class UserController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取指定机构(包括所有子机构)下的所有用户，默认为0表示所有机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public ActionResult GetPagingUsers(GetPagingUsersRequest request, int page, int rows)
        {
            var result = string.Empty;
            if (request == null)
            {
                request = new GetPagingUsersRequest();
            }
            request.PageIndex = page;
            request.PageSize = rows;

            using (var factory = new ChannelFactory<IRightsUserService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetPagingUsers(request);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateFormat.DATETIME) + "}";
                }
            }

            return Content(result);
        }
    }
}