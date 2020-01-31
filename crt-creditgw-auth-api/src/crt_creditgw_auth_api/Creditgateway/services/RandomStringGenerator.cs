using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.Creditgateway.services
{
    public static class RandomStringGenerator
    {
        /// <summary>
        /// JB. Randomly generates a string
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GeneratedString()
        {
            string result ="";
            await Task.Run (() => {
               result = Guid.NewGuid().ToString("N");
            });
            return result;
        }

    }
}
