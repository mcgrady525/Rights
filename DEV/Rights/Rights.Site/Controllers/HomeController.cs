using Rights.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Rights.Site.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : BaseController
    {
        ///// <summary>
        ///// 首页
        ///// </summary>
        ///// <returns></returns>
        ////[LoginAuthorization]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 首次登录,需修改密码
        ///// </summary>
        ///// <returns></returns>
        ////[LoginAuthorization]
        //public ActionResult FirstLogin()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult FirstLogin(FirstLoginRequest request)
        //{
        //    var flag = false;
        //    var msg = string.Empty;

        //    //只能修改当前登录用户的密码
        //    //新密码不能和原密码一样
        //    //修改成功需要重新生成cookie
        //    if (CurrentUserInfo == null || CurrentUserInfo.Id != request.Id)
        //    {
        //        msg = "未知错误,重置密码失败";
        //        return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (CurrentUserInfo.UserPwd.Equals(request.NewPwd.To32bitMD5()))
        //    {
        //        msg = "新密码不能和默认密码一样!";
        //        return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //    }

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var result = client.InitUserPwd(request);
        //        if (result.ReturnCode == ReturnCodeType.Success && result.Content == true)
        //        {
        //            //更新cookie
        //            FormsIdentity id = (FormsIdentity)HttpContext.User.Identity;
        //            FormsAuthenticationTicket ticketOld = id.Ticket;
        //            CurrentUserInfo.UserPwd = request.NewPwd.To32bitMD5();
        //            CurrentUserInfo.IsChangePwd = true;

        //            FormsAuthentication.SignOut();
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
        //            (
        //                2,
        //                CurrentUserInfo.UserId,
        //                DateTime.Now,
        //                ticketOld.Expiration,
        //                false,
        //                CurrentUserInfo.ToJson()
        //            );
        //            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
        //            if (ticket.Expiration != new DateTime(9999, 12, 31))
        //            {
        //                cookie.Expires = ticketOld.Expiration;
        //            }
        //            HttpContext.Response.Cookies.Add(cookie);

        //            flag = true;
        //            msg = "重置密码成功";
        //        }
        //        else
        //        {
        //            msg = "重置密码失败!";
        //            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <returns></returns>
        ////[LoginAuthorization]
        //public ActionResult ChangePwd()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult ChangePwd(ChangePwdRequest request)
        //{
        //    //更新密码
        //    //更新密码成功后清除cookie，然后登录的时候会重写cookie
        //    var flag = false;
        //    var msg = string.Empty;

        //    var originalPwd = request.OriginalPwd.To32bitMD5();
        //    var newPwd = request.NewPwd.To32bitMD5();
        //    if (!originalPwd.Equals(CurrentUserInfo.UserPwd))
        //    {
        //        msg = "原密码不正确!";
        //        return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //    }

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        request.UserId = CurrentUserInfo.Id;
        //        request.NewPwd = newPwd;
        //        var result = client.ChangePwd(request);
        //        if (result.ReturnCode == ReturnCodeType.Success && result.Content == true)
        //        {
        //            //修改成功要清除cookie然后到登录页面重写cookie
        //            FormsAuthentication.SignOut();
        //            flag = true;
        //            msg = "修改成功,正在跳转到登陆页面！";

        //        }
        //        else
        //        {
        //            msg = "修改失败!";
        //        }
        //    }

        //    return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 获取该用户所拥有的菜单权限
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetUserMenu()
        //{
        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var result = client.GetUserMenu(CurrentUserInfo.Id);
        //        return Content(result.Content);
        //    }
        //}

        ///// <summary>
        ///// 左侧导航菜单
        ///// accordition+ tree
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ContentResult GetLeftMenuAccordion(int id)
        //{
        //    var outPut = string.Empty;

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var result = client.GetLeftMenuAccordion(CurrentUserInfo.Id, id);
        //        if (result.ReturnCode == ReturnCodeType.Success)
        //        {
        //            outPut = result.Content;
        //        }
        //    }

        //    return Content(outPut);
        //}

        //[HttpPost]
        //public ContentResult GetLeftMenuTree(int id)
        //{
        //    var outPut = string.Empty;

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var result = client.GetLeftMenuTree(CurrentUserInfo.Id, id);
        //        if (result.ReturnCode == ReturnCodeType.Success)
        //        {
        //            outPut = result.Content;
        //        }
        //    }

        //    return Content(outPut);
        //}

        ///// <summary>
        ///// 获取该用户的信息并再次验证cookie
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetCurrentUser()
        //{
        //    var flag = false;
        //    var msg = "";

        //    //已登录并且cookie验证通过，所以直接从cookie中取就可以
        //    FormsIdentity id = (FormsIdentity)HttpContext.User.Identity;
        //    FormsAuthenticationTicket ticket = id.Ticket;
        //    msg = ticket.UserData;
        //    if (!msg.IsNullOrEmpty())
        //    {
        //        flag = true;
        //    }

        //    return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 我的信息
        ///// </summary>
        ///// <returns></returns>
        ////[LoginAuthorization]
        //public ActionResult GetMyInfo()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 我的信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult GetMyInfoPost()
        //{
        //    var flag = false;
        //    var msg = string.Empty;
        //    var data = new GetMyInfoResponse();

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        var result = client.GetMyInfo(CurrentUserInfo.Id);
        //        if (result.ReturnCode == ReturnCodeType.Success)
        //        {
        //            flag = true;
        //            data = result.Content;
        //        }
        //        else
        //        {
        //            msg = result.Message;
        //        }
        //    }

        //    return Json(new { success = flag, msg = msg, data = data }, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// 我的权限
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetMyAuthority()
        //{
        //    return Content("");
        //}
	}
}