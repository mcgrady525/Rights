using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rights.IService.Rights;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Rights.Service.Rights
{
    /// <summary>
    /// 菜单管理service
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RightsMenuService : IRightsMenuService
    {

    }
}
