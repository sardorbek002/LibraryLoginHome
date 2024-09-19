using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using UnauthorizedResult = Microsoft.AspNetCore.Mvc.UnauthorizedResult;

namespace CrudforMedicshop.infrastructure.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthefrationAttributeFilter : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        private readonly string _key;
        private readonly string _value;

        public AuthefrationAttributeFilter(string value, string key = "permission")
        {
            _key = key;
            _value = value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var permissionclaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(_key, StringComparison.OrdinalIgnoreCase));
            if (permissionclaim == null || !permissionclaim.Value.Equals(_value, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ForbidResult();
                return;
            }
        }

    }
}
