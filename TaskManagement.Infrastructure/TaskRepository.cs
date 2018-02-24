using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.ViewModels;
using TaskManagement.Repository;

namespace TaskManagement.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        TasksDbContext TasksDbContext = new TasksDbContext();
        public void Create(UserTask task)
        {
            TasksDbContext.Tasks.Add(task);
            TasksDbContext.SaveChanges();
        }

        public void Delete(Guid taskid)
        {
            var task = TasksDbContext.Tasks.Find(taskid);
            TasksDbContext.Tasks.Remove(task);
            TasksDbContext.SaveChanges();
        }

        public List<UserTaskViewModel> GetByOwner(Guid UserId)
        {
            var tasks = from task in TasksDbContext.Tasks
                        where task.CreatedByUser_Id == UserId.ToString() || task.AssignedToUser_Id == UserId.ToString()
                        select new UserTaskViewModel()
                        {
                            Id = task.TaskId,
                            DateCreated = task.DateCreated,
                            Desc = task.TaskDesc,
                            Title = task.TaskTitle,
                            DueDate = task.DueDate,
                            CreatedBy = task.CreatedByUser_Id,
                            AssignedTo = task.AssignedToUser_Id,
                            AssignedToUserName = task.AssignedToUser.UserName,
                            CreatedByUserName = task.CreatedByUser.UserName,
                            Status = task.TaskStatus.TaskStatusName
                        };
            return tasks.ToList<UserTaskViewModel>();
        }

        public List<UserTaskViewModel> GetList()
        {
            var tasks = from task in TasksDbContext.Tasks
                        select new UserTaskViewModel()
                        {
                            Id = task.TaskId,
                            DateCreated = task.DateCreated,
                            Desc = task.TaskDesc,
                            Title = task.TaskTitle,
                            DueDate = task.DueDate,
                            CreatedBy = task.CreatedByUser_Id,
                            AssignedTo = task.AssignedToUser_Id,
                            AssignedToUserName = task.AssignedToUser.UserName,
                            CreatedByUserName = task.CreatedByUser.UserName,
                            Status = task.TaskStatus.TaskStatusName
                        };
            return tasks.ToList<UserTaskViewModel>();
        }

        public UserTask GetTask(Guid taskId)
        {
            var task = TasksDbContext.Tasks.Find(taskId);
            return task;
        }

        public List<UserInfo> GetUserList()
        {
            var supportRole = TasksDbContext.Roles.First(r => r.Name == "Support");
            var users = TasksDbContext.Users
                        .Where(user => user.Roles.Select(role => role.RoleId).Contains(supportRole.Id))
                        .Select(user => new UserInfo() { UserId = user.Id , UserName = user.UserName});
            return users.ToList<UserInfo>();
        }

        public void Update(UserTask task)
        {
            TasksDbContext.SaveChanges();
        }
    }
}
