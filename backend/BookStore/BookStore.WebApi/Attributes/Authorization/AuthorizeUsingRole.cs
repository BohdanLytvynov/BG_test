using BookStore.DAL.Entities;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.WebApi.Attributes.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeUsingRole : Attribute, IAuthorizationFilter
    {
        public string RoleName { get; set; }

        public AuthorizeUsingRole(string roleName)
        {
            RoleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var http = context.HttpContext;

            if ((http.Items["User"] as User) is not null)
            {
                if (!http.Items["Role"].Equals(RoleName))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized", status = StatusCodes.Status403Forbidden });
                }
            }
            else
            { 
                context.Result = new JsonResult(new { message = "Unauthorized", status = StatusCodes.Status401Unauthorized });
            }

        }
    }
}
