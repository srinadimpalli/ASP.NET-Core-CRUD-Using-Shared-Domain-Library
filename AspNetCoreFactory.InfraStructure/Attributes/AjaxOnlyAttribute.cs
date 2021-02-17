using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFactory.InfraStructure.Attributes
{
    // Allows only Ajax calls to be processed
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var headerValue = routeContext.HttpContext.Request?.Headers["X-Requested-With"];
            return headerValue.HasValue && headerValue.Value == "XMLHttpRequest";
        }
    }
}
