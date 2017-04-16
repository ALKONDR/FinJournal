using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    public class BaseController : Controller
    {
        protected const string SUB = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        /// <summary>
        /// returns claim value with given key
        /// </summary>
        /// <param name="type">key</param>
        /// <returns>claim value</returns>
        protected string GetClaimByName(string type)
        {
            ClaimsIdentity claims = User.Identity as ClaimsIdentity;
            var claim = claims.FindFirst(c => c.Type == type);

            return claim == null ? "" : claim.Value;
        }    
    }
}