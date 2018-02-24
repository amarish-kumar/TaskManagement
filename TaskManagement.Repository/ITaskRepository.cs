using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.ViewModels;

namespace TaskManagement.Repository
{
    public interface ITaskRepository
    {
        void Create(UserTask task);
        void Update(UserTask task);
        void Delete(Guid taskid);
        List<UserTaskViewModel> GetList();
        List<UserTaskViewModel> GetByOwner(Guid UserId);
        UserTask GetTask(Guid taskId);
        List<UserInfo> GetUserList();
    }
}
