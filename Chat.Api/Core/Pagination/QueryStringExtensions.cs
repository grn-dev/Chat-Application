using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Core.Pagination
{
    public static class QueryStringExtensions
    {
        public static string GetParameter(this QueryString receiver, string name)
        {
            var parameters = QueryHelpers.ParseQuery(receiver.ToString());
            return parameters.ContainsKey(name) ? parameters[name][0] : null;
        }
    }
}
