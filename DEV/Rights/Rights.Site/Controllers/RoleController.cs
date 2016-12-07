using Rights.Entity.Common;
using Rights.Entity.ViewModel;
using Rights.IService.Rights;
using Rights.Site.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tracy.Frameworks.Common.Consts;
using Tracy.Frameworks.Common.Extends;
using Rights.Common.Helper;

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
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
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
                    result = "{\"total\": " + rs.Content.TotalCount + ",\"rows\":" + rs.Content.Entities.ToJson(dateTimeFormat: DateTimeTypeConst.DATETIME) + "}";
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
                    msg = rs.Message.IsNullOrEmpty() ? "新增失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.EditRole(request, loginInfo);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "修改成功!";
                }
                else
                {
                    msg = rs.Message.IsNullOrEmpty() ? "修改失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.DeleteRole(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "删除成功!";
                }
                else
                {
                    msg = rs.Message.IsNullOrEmpty() ? "删除失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [LoginAuthorization]
        public ActionResult Authorize()
        {
            return View();
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Authorize(AuthorizeRoleRequest request)
        {
            var flag = false;
            var msg = string.Empty;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.AuthorizeRole(request);
                if (rs.ReturnCode == ReturnCodeType.Success && rs.Content == true)
                {
                    flag = true;
                    msg = "授权成功!";
                }
                else
                {
                    msg = rs.Message.IsNullOrEmpty() ? "授权失败!" : rs.Message;
                }
            }

            return Json(new { success = flag, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取该角色所拥有的菜单按钮权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult GetRoleMenuButton(int roleId)
        {
            var result = string.Empty;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetRoleMenuButton(roleId);
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    var roleMenuButtons = rs.Content;
                    if (roleMenuButtons.HasValue())
                    {
                        result = GetRoleMenuButtonStr(roleMenuButtons, roleId);
                    }
                }
            }

            return Content(result);
        }

        /// <summary>
        /// 获取该角色所拥有的菜单按钮权限json字符串
        /// 目前先固定成三级菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetRoleMenuButtonStr(List<GetRoleMenuButtonResponse> list, int roleId)
        {
            StringBuilder sb = new StringBuilder();
            var parentMenus = list.Where(p => p.MenuParentId == 0).ToList();
            sb.Append("[");
            if (parentMenus.HasValue())
            {
                for (int i = 0; i < parentMenus.Count; i++)//一级菜单
                {
                    sb.Append("{\"id\":\"" + parentMenus[i].MenuId.ToString() + "\",\"text\":\"" + parentMenus[i].MenuName + "\",\"children\":[");
                    var secondMenus = list.Where(p => p.MenuParentId == parentMenus[i].MenuId).ToList();
                    if (secondMenus.HasValue())
                    {
                        for (int j = 0; j < secondMenus.Count; j++)//二级菜单
                        {
                            sb.Append("{\"id\":\"" + secondMenus[j].MenuId.ToString() + "\",\"text\":\"" + secondMenus[j].MenuName + "\",\"children\":[");
                            var threeMenus = list.Where(p => p.MenuParentId == secondMenus[j].MenuId).ToList();
                            threeMenus = threeMenus.DistinctBy(p => p.MenuId).ToList();//distinct，因为一个menu可能有多个按钮
                            if (threeMenus.HasValue())
                            {
                                for (int k = 0; k < threeMenus.Count; k++)//三级菜单
                                {
                                    sb.Append("{\"id\":\"" + threeMenus[k].MenuId.ToString() + "\",\"text\":\"" + threeMenus[k].MenuName + "\",\"children\":[");
                                    var buttons = list.Where(p => p.MenuId == threeMenus[k].MenuId).ToList();
                                    if (buttons.HasValue())
                                    {
                                        for (int l = 0; l < buttons.Count; l++)//按钮
                                        {
                                            sb.Append("{\"id\":\"" + roleId + "\",\"text\":\"" + buttons[l].ButtonName + "\",\"checked\":" + buttons[l].Checked + ",\"attributes\":{\"menuid\":\"" + buttons[l].MenuId.ToString() + "\",\"buttonid\":\"" + buttons[l].ButtonId.ToString() + "\"}},");
                                        }
                                        sb.Remove(sb.Length - 1, 1);
                                        sb.Append("]},");
                                    }
                                    else
                                    {
                                        sb.Append("]},");
                                    }
                                }
                                sb.Remove(sb.Length - 1, 1);
                                sb.Append("]},");
                            }
                            else
                            {
                                sb.Append("]},");
                            }
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]},");
                    }
                    else
                    {
                        sb.Append("]},");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            else
            {
                sb.Append("]");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll()
        {
            var result = string.Empty;

            using (var factory = new ChannelFactory<IRightsRoleService>("*"))
            {
                var client = factory.CreateChannel();
                var rs = client.GetAllRole();
                if (rs.ReturnCode == ReturnCodeType.Success)
                {
                    result = rs.Content.Select(p => new
                    {
                        Id = p.Id,
                        RoleName = p.Name
                    }).ToJson();
                }
            }

            return Content(result);
        }

    }
}