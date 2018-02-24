using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Repository
{
    public interface ICurrentUserRepository
    {
        Guid GetCurrentUserId();
        bool IsAdmin();
        string GetCurrentUserName();
    }
}
