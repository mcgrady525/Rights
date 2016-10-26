USE RightsDB;
GO


--权限管理系统
SELECT * FROM dbo.t_rights_organization;

SELECT * FROM dbo.t_rights_user;

SELECT * FROM dbo.t_rights_user_organization;



--测试数据
--组织机构

--INSERT INTO dbo.t_rights_user
--        ( user_id ,
--          password ,
--          user_name ,
--          is_change_pwd ,
--          enable_flag ,
--          created_by ,
--          created_time ,
--          last_updated_by ,
--          last_updated_time
--        )
--VALUES  
--( N'admin' ,N'123456' ,N'系统管理员' ,0 ,1 ,1 ,'2016-10-26 09:42:36' ,NULL ,NULL),
--( N'luning' ,N'123456' ,N'鲁宁' ,0 ,1 ,1 ,'2016-10-26 09:42:36' ,NULL ,NULL),
--( N'mcgrady' ,N'123456' ,N'麦迪' ,0 ,1 ,1 ,'2016-10-26 09:42:36' ,NULL ,NULL);

--
--INSERT INTO dbo.t_rights_user_organization
--        ( user_id, organization_id )
--VALUES  
--( 1,1),
--( 2,4),
--( 3,5);
