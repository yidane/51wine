select * from wx_link_module

INSERT INTO  [wx_link_module]
           ([lName]
           ,[moduleName]
           ,[moduleCode]
           ,[moduleType]
           ,[urlRule]
           ,[urlNeedReplace]
           ,[tableName]
           ,[isGlobal]
           ,[isMore]
           ,[sortId]
           ,[remark]
           ,[idColumn]
           ,[nameColumn])
     VALUES
           ('砸金蛋'
           ,'砸金蛋'
           ,'zjd'
           ,1
           ,'{yuming}/weixin/zjd/index.aspx?wid={wid}&aid={id}'
           ,1
           ,'wx_zjdActionInfo'
           ,1
           ,1
           ,5
           ,'营销工具'
           ,'id'
           ,'actName')