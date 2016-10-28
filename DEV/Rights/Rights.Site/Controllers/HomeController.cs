﻿using Rights.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Rights.IService.Rights;
using Rights.Entity.Db;
using Tracy.Frameworks.Common.Extends;
using Rights.Entity.Rights;
using System.Text;
using Rights.Site.Filters;

namespace Rights.Site.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

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
        //    if (LoginInfo == null || LoginInfo.Id != request.Id)
        //    {
        //        msg = "未知错误,重置密码失败";
        //        return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (LoginInfo.UserPwd.Equals(request.NewPwd.To32bitMD5()))
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
        //            LoginInfo.UserPwd = request.NewPwd.To32bitMD5();
        //            LoginInfo.IsChangePwd = true;

        //            FormsAuthentication.SignOut();
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
        //            (
        //                2,
        //                LoginInfo.UserId,
        //                DateTime.Now,
        //                ticketOld.Expiration,
        //                false,
        //                LoginInfo.ToJson()
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
        //    if (!originalPwd.Equals(LoginInfo.UserPwd))
        //    {
        //        msg = "原密码不正确!";
        //        return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        //    }

        //    using (var factory = new ChannelFactory<IWebFxsCommonService>("*"))
        //    {
        //        var client = factory.CreateChannel();
        //        request.UserId = LoginInfo.Id;
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
        //        var result = client.GetUserMenu(LoginInfo.Id);
        //        return Content(result.Content);
        //    }
        //}

        /// <summary>
        /// 左侧导航菜单
        /// accordition+ tree
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult GetLeftMenuAccordion(int id)
        {
            var outPut = string.Empty;
            var leftMenus = new List<LeftMenu>();

            using (var factory = new ChannelFactory<IRightsAccountService>("*"))
            {
                var client = factory.CreateChannel();
                var result = client.GetAllChildrenMenu(LoginInfo.Id, id);
                if (result.ReturnCode == ReturnCodeType.Success)
                {
                    var childMenus = result.Content;
                    if (childMenus.HasValue())
                    {
                        foreach (var item in childMenus)
                        {
                            leftMenus.Add(new LeftMenu
                            {
                                id = item.Id,
                                text = item.Name,
                                iconCls = item.Icon
                            });
                        }
                    }
                    outPut = leftMenus.ToJson();
                }
            }

            return Content(outPut);
        }

        [HttpPost]
        public ContentResult GetLeftMenuTree(int id)
        {
            var result = string.Empty;
            StringBuilder sb = new StringBuilder();

            using (var factory = new ChannelFactory<IRightsAccountService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetAllChildrenMenu(LoginInfo.Id, id);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var childMenus = rs.Content;
                    if (childMenus.HasValue())
                    {
                        sb.Append(RecursionMenu(childMenus, id));
                        sb = sb.Remove(sb.Length - 2, 2);
                        result = sb.ToString();
                    }
                    else
                    {
                        result = "[]";
                    }
                }


            }

            return Content(result);
        }

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
        //        var result = client.GetMyInfo(LoginInfo.Id);
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


        #region Private method

        private string RecursionMenu(List<TRightsMenu> list, int menuParentId)
        {
            StringBuilder sb = new StringBuilder();
            var childMenus = list.Where(p => p.ParentId == menuParentId).ToList();
            if (childMenus.HasValue())
            {
                sb.Append("[");
                for (int i = 0; i < childMenus.Count; i++)
                {
                    var childMenuStr = RecursionMenu(list, childMenus[i].Id);
                    if (!childMenuStr.IsNullOrEmpty())
                    {
                        sb.Append("{\"id\":\"" + childMenus[i].Id.ToString() + "\",\"text\":\"" + childMenus[i].Name + "\",\"iconCls\":\"" + childMenus[i].Icon + "\",\"state\":\"closed\",\"children\":");
                        sb.Append(childMenuStr);
                    }
                    else
                    {
                        sb.Append("{\"id\":\"" + childMenus[i].Id.ToString() + "\",\"text\":\"" + childMenus[i].Name + "\",\"iconCls\":\"" + childMenus[i].Icon + "\",\"state\":\"open\",\"attributes\":{\"url\":\"" + childMenus[i].Url + "\"}},");
                    }

                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]},");
            }

            return sb.ToString();
        }

        #endregion

    }
}