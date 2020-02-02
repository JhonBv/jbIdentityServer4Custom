using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services
{
    public interface ICompleteClientSetUpService
    {
        /// <summary>
        /// JB. Complete Client Registration by addint Scope, Claims, Grants (grants type)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ClientCompleteSetupResponseModel> CompleteClientSetUp(ClientCompleteSetupModel model);
    }
}
