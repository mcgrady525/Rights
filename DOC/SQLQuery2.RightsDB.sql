USE RightsDB;
GO

--机构
SELECT * FROM dbo.t_rights_organization;

--用户
SELECT * FROM dbo.t_rights_user;

--登陆检查
--SELECT u.id, u.user_id AS UserId, u.password, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
--u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime
--FROM dbo.t_rights_user AS u
--WHERE u.user_id= @UserId AND u.password= @Password;

--用户-机构
--SELECT * FROM dbo.t_rights_user_organization;

--角色
SELECT * FROM dbo.t_rights_role;

--用户-角色
SELECT * FROM dbo.t_rights_user_role;

--菜单
SELECT * FROM dbo.t_rights_menu;

--按钮
SELECT * FROM dbo.t_rights_button;

--菜单-按钮
SELECT * FROM dbo.t_rights_menu_button;

--角色-菜单-按钮
SELECT * FROM dbo.t_rights_role_menu_button;

--当前用户可以访问的菜单
SELECT menu.id, menu.name, menu.parent_id AS ParentId, menu.code, menu.url, menu.icon,menu.sort,
menu.created_by AS CreatedBy, menu.created_time AS CreatedTime,
menu.last_updated_by AS LastUpdatedBy, menu.last_updated_time AS LastUpdatedTime 
FROM dbo.t_rights_menu AS menu
LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON menu.id= roleMenuButton.menu_id
LEFT JOIN dbo.t_rights_user_role AS userRole ON roleMenuButton.role_id = userRole.role_id
LEFT JOIN dbo.t_rights_user AS u ON userRole.user_id = u.id
--WHERE u.id= @UserId AND menu.parent_id= @MenuParentId
WHERE u.id= 4 AND menu.parent_id= 0
ORDER BY menu.parent_id, menu.sort;


