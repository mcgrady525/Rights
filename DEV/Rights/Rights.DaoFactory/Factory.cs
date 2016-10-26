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
        private static object GetInstance(string daoName)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["Rights.Dao"];
            if (string.IsNullOrEmpty(configName))
            {
                throw new Exception("还没有配置Rights.Dao!");
            }
            string className = string.Format("{0}.{1}", configName, daoName);

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);

            //创建指定类型的对象实例
            return assembly.CreateInstance(className);
        }

        /// <summary>
        /// 创建组织机构dao实例
        /// </summary>
        /// <returns></returns>
        public static IRightsOrganizationDao GetRightsOrganizationDao()
        {
            return GetInstance("RightsOrganizationDao") as IRightsOrganizationDao;
        }

    }
}
