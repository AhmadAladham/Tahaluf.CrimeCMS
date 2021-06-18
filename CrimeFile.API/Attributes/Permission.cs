using CrimeFile.API.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CrimeFile.API.Attributes
{
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string Permission;

        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaim = SecurityUtility.CheckPermissions(context.HttpContext.User.Claims, Permission);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
