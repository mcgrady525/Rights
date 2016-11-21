using Rights.Entity.Common;
using Rights.IService.Rights;
using Rights.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Extends;
using Tracy.Frameworks.Common.Const;

namespace Rights.Site.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : BaseController
    {
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有菜单，以树形展示
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            var result = string.Empty;
            StringBuilder sb = new StringBuilder();

            //using (var factory = new ChannelFactory<IRightsMenuService>("*"))
            //{
            //    var client = factory.CreateChannel();
            //    var rs = client.GetAll();
            //    if (rs.ReturnCode == ReturnCodeType.Success)
            //    {
            //        var orgs = rs.Content;
            //        if (orgs.HasValue())
            //        {
            //            sb.Append(RecursionOrg(orgs, 0));
            //            sb = sb.Remove(sb.Length - 2, 2);
            //            result = sb.ToString();
            //        }
            //        else
            //        {
            //            result = "[]";
            //        }
            //    }
            //}

            return Content(result);
        }


    }
}