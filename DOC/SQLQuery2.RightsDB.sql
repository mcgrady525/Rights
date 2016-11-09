USE RightsDB;
GO

--机构
SELECT * FROM dbo.t_rights_organization;

--用户
SELECT * FROM dbo.t_rights_user;

--用户-机构
SELECT * FROM dbo.t_rights_user_organization;

--INSERT INTO dbo.t_rights_user_organization
--        ( user_id, organization_id )
--VALUES  ( 1, -- user_id - int
--          2  -- organization_id - int
--          ),(3,2);


WITH cte_org(Id, N,ParentId) AS
(
	SELECT o1.id AS Id, o1.name AS N, o1.parent_id AS ParentId FROM dbo.t_rights_organization AS o1
	UNION ALL
	SELECT o1.id AS Id, o1.name AS N, o1.parent_id AS ParentId FROM dbo.t_rights_organization AS o1
	JOIN cte_org AS o2 ON o1.parent_id= o2.Id
)

SELECT DISTINCT * FROM cte_org AS c
WHERE c.ParentId= 4;

SELECT DISTINCT u.id, u.user_id AS UserId, u.user_name AS UserName,
u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag, u.created_time AS CreatedTime
FROM dbo.t_rights_user AS u
LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id= userOrg.user_id
LEFT JOIN cte_org AS c ON c.Id= userOrg.organization_id
WHERE c.ParentId= 4;


--查询用户列表(分页)
SELECT u.id, u.user_id AS UserId, u.user_name AS UserName,
u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag, u.created_time AS CreatedTime
FROM dbo.t_rights_user AS u
LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id= userOrg.user_id
WHERE userOrg.organization_id IN (4,6,7,24,25,26);
--WHERE userOrg.organization_id IN @OrgIds

--CTE,目的distinct
WITH cte_paging_user AS
(
    SELECT DISTINCT  u.id ,
            u.user_id AS UserId ,
            u.user_name AS UserName ,
            u.is_change_pwd AS IsChangePwd ,
            u.enable_flag AS EnableFlag ,
            u.created_time AS CreatedTime
    FROM    dbo.t_rights_user AS u
            LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id = userOrg.user_id
    WHERE   userOrg.organization_id IN @OrgIds
)

--分页
SELECT r.*
FROM    ( 
			SELECT ROW_NUMBER() OVER(ORDER BY cu.id) AS RowNum, cu.* FROM cte_paging_user AS cu
        ) AS r
WHERE   r.RowNum BETWEEN @Start AND @End;

--total
SELECT COUNT(DISTINCT u.id)
FROM    dbo.t_rights_user AS u
        LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id = userOrg.user_id
WHERE   userOrg.organization_id IN @OrgIds;


--获取所有用户
SELECT  r.*
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY u.created_time DESC ) AS RowNum ,
                    u.id ,
                    u.user_id AS UserId ,
                    u.user_name AS UserName ,
                    u.is_change_pwd AS IsChangePwd ,
                    u.enable_flag AS EnableFlag ,
                    u.created_time AS CreatedTime
          FROM      dbo.t_rights_user AS u
        ) AS r
WHERE   r.RowNum BETWEEN @Start AND @End;

--获取所有用户total
SELECT COUNT(DISTINCT u.id) FROM dbo.t_rights_user AS u;



--用户-机构
SELECT * FROM dbo.t_rights_user_organization;

--INSERT INTO dbo.t_rights_user_organization
--        ( user_id, organization_id )
--VALUES  
--( 17,2),
--( 16,2);


--角色
SELECT * FROM dbo.t_rights_role;

--用户-角色
SELECT * FROM dbo.t_rights_user_role;

--INSERT INTO dbo.t_rights_user_role
--        ( user_id, role_id )
--VALUES  
--( 17,1),
--( 16,1);

--菜单
SELECT * FROM dbo.t_rights_menu;

--按钮
SELECT * FROM dbo.t_rights_button;

--菜单-按钮
SELECT * FROM dbo.t_rights_menu_button;

--INSERT INTO dbo.t_rights_menu_button
--        ( menu_id, button_id )
--VALUES  
--( 6,2),
--( 6,3),
--( 6,4),
--( 6,7),
--( 6,8);

--角色-菜单-按钮
SELECT * FROM dbo.t_rights_role_menu_button;

--INSERT INTO dbo.t_rights_role_menu_button
--        ( role_id, menu_id, button_id )
--VALUES  
--( 1,6,2 ),
--( 1,6,3 ),
--( 1,6,4 ),
--( 1,6,7 ),
--( 1,6,8 );

--当前用户可以访问的菜单
SELECT menu.id, menu.name, menu.parent_id AS ParentId, menu.code, menu.url, menu.icon,menu.sort,
menu.created_by AS CreatedBy, menu.created_time AS CreatedTime,
menu.last_updated_by AS LastUpdatedBy, menu.last_updated_time AS LastUpdatedTime,* 
FROM dbo.t_rights_menu AS menu
LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON menu.id= roleMenuButton.menu_id
LEFT JOIN dbo.t_rights_user_role AS userRole ON roleMenuButton.role_id = userRole.role_id
LEFT JOIN dbo.t_rights_user AS u ON userRole.user_id = u.id
--WHERE u.id= @UserId
WHERE u.id= 4
ORDER BY menu.parent_id, menu.sort;

--查询用户
SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag, u.created_by AS CreatedBy,
u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
FROM dbo.t_rights_user AS u
WHERE u.id= @Id;

UPDATE dbo.t_rights_user SET is_change_pwd= 1, password= @Password, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;

UPDATE dbo.t_rights_user SET password= 'e10adc3949ba59abbe56e057f20f883e', is_change_pwd= 0 WHERE id= 4;


--我的信息
--帐户信息，角色和所属机构
SELECT u.user_id AS UserId, u.user_name AS UserName, u.created_time AS CreatedTime,* 
FROM dbo.t_rights_user AS u
LEFT JOIN dbo.t_rights_user_organization AS userOrg ON u.id= userOrg.user_id
LEFT JOIN dbo.t_rights_organization AS org ON userOrg.organization_id= org.id
LEFT JOIN dbo.t_rights_user_role AS userRole ON u.id= userRole.user_id
LEFT JOIN dbo.t_rights_role AS r ON userRole.role_id= r.id
WHERE u.id= @Id;

--获取当前用户当前页面可以访问的按钮列表
SELECT * FROM dbo.t_rights_button AS button
LEFT JOIN dbo.t_rights_role_menu_button AS roleMenuButton ON button.id= roleMenuButton.button_id
LEFT JOIN dbo.t_rights_menu AS menu ON roleMenuButton.menu_id= menu.id
LEFT JOIN dbo.t_rights_user_role AS userRole ON userRole.role_id= roleMenuButton.role_id
LEFT JOIN dbo.t_rights_user AS u ON u.id= userRole.user_id
--WHERE u.id= @UserId AND menu.code= @MenuCode;
WHERE u.id= 4 AND menu.code= 'organization';

--获取指定机构的所有子机构
SELECT org.parent_id AS ParentId,org.organization_type AS OrganizationType, org.enable_flag AS EnableFlag,
org.created_by AS CreatedBy, org.created_time AS CreatedTime, org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime,* 
FROM dbo.t_rights_organization AS org
WHERE org.enable_flag= 1 AND org.parent_id= @ParentId;

--修改机构
UPDATE dbo.t_rights_organization SET name= @OrgName, parent_id= @ParentId, sort= @Sort, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;

SELECT org.parent_id AS ParentId,org.organization_type AS OrganizationType, org.enable_flag AS EnableFlag,
org.created_by AS CreatedBy, org.created_time AS CreatedTime, org.last_updated_by AS LastUpdatedBy, org.last_updated_time AS LastUpdatedTime,* 
FROM dbo.t_rights_organization AS org
WHERE org.enable_flag= 1 AND org.parent_id= @ParentId
ORDER BY org.code, org.sort;

--新增用户
INSERT INTO dbo.t_rights_user VALUES ( @UserId ,@Password ,@UserName ,@IsChangePwd ,@EnableFlag ,@CreatedBy ,@CreatedTime ,@LastUpdatedBy ,@LastUpdatedTime);

--修改用户
UPDATE dbo.t_rights_user SET user_id= @UserId, user_name= @UserName, enable_flag= @EnableFlag, is_change_pwd= @IsChangePwd, last_updated_by= @LastUpdatedBy, last_updated_time= @LastUpdatedTime WHERE id= @Id;

--删除用户
DELETE FROM dbo.t_rights_user WHERE id= @Id;

--查询用户
SELECT u.user_id AS UserId, u.user_name AS UserName, u.is_change_pwd AS IsChangePwd, u.enable_flag AS EnableFlag,
u.created_by AS CreatedBy, u.created_time AS CreatedTime, u.last_updated_by AS LastUpdatedBy, u.last_updated_time AS LastUpdatedTime,* 
FROM dbo.t_rights_user AS u WHERE u.id= @Id;


SELECT  u.user_id AS UserId ,
        u.user_name AS UserName ,
        u.is_change_pwd AS IsChangePwd ,
        u.enable_flag AS EnableFlag ,
        u.created_by AS CreatedBy ,
        u.created_time AS CreatedTime ,
        u.last_updated_by AS LastUpdatedBy ,
        u.last_updated_time AS LastUpdatedTime ,
        *
FROM    dbo.t_rights_user AS u
ORDER BY u.id;


--依据userId获取用户
SELECT TOP 1 * FROM dbo.t_rights_user AS u WHERE u.user_id= @UserId;

--删除用户
DELETE FROM dbo.t_rights_user WHERE id IN @Ids;

--解除用户-机构
DELETE FROM dbo.t_rights_user_organization WHERE user_id IN @Ids;

--解除用户-角色
DELETE FROM dbo.t_rights_user_role WHERE user_id IN @Ids;







