using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaskManagement.Repository;

namespace TaskManagement.Infrastructure
{
    public class CurrentUserRepository : ICurrentUserRepository
    {
        public CurrentUserRepository()
        {

        }
        public Guid GetCurrentUserId()
        {
            var claimIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return new Guid(userId);
        }

        public string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public bool IsAdmin()
        {
            var claimIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var isadmin = claimIdentity.Claims.Any(c=>c.Type == (ClaimTypes.Role) && c.Value == "Admin");
            return isadmin;
        }
    }
}
