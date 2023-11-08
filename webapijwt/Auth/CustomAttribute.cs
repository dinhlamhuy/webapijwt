
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace webapijwt.Auth
{
    public class CustomAttribute : AuthorizeAttribute
    {

        private readonly string[] allowedroles;
        public CustomAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool authorize = false;
            var authToken = actionContext.Request.Headers.Authorization?.Parameter;
            if (authToken != null)
            {
                List<string> userRoles = JwtManager.GetPrincipal(authToken).FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
                
                authorize = this.allowedroles.Any(x => userRoles.Any(y => y == x));
            }
            return authorize;
        }
    }

}