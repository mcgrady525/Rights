using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IDao.Rights;

namespace Rights.DaoFactory
{
    public class Factory
    {
        /// <summary>
        /// 依据dao名称创建实例
        /// </summary>
        /// <param name="daoName"></param>
        /// <returns></returns>
        private static object GetInstance(string daoName, string directoryName= "")
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["Rights.DaoAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                throw new Exception("还没有配置Rights.DaoAccess!");
            }

            StringBuilder sbClassName = new StringBuilder(configName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                sbClassName.Append("."+ directoryName);
            }
            sbClassName.Append("."+ daoName);

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);

            //创建指定类型的对象实例
            return assembly.CreateInstance(sbClassName.ToString());
        }

        /// <summary>
        /// 创建组织机构dao实例
        /// </summary>
        /// <returns></returns>
        public static IRightsOrganizationDao GetRightsOrganizationDao()
        {
            return GetInstance("RightsOrganizationDao", "Rights") as IRightsOrganizationDao;
        }

    }
}
