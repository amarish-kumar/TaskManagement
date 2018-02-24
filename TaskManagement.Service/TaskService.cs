using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Common;
using TaskManagement.Core.Entities;
using TaskManagement.Core.ViewModels;
using TaskManagement.Repository;

namespace TaskManagement.Service
{
    public class TaskService
    {
        public TaskService(ITaskRepository taskRepository,ICurrentUserRepository currentUserRepository)
        {
            this.taskRepository = taskRepository;
            this.currentUserRepository = currentUserRepository;
        }

        public ITaskRepository taskRepository { get; private set; }
        public ICurrentUserRepository currentUserRepository { get; private set; }

        public  void CreateTask(CreateUserTaskViewModel taskData)
        {
            var userTask = new UserTask()
            {
                CreatedByUser_Id = currentUserRepository.GetCurrentUserId().ToString(),
                DateCreated = DateTime.Now,
                TaskId = Guid.NewGuid(),
                TaskStatusId = (int)UserTaskStatus.New,
                TaskDesc = taskData.Desc,
                TaskTitle = taskData.Title,
                DueDate = taskData.DueDate,
                AssignedToUser_Id = taskData.AssignedTo.ToString(),
                DateUpdated = (DateTime?)null
            };
            taskRepository.Create(userTask);
        }

        public List<UserTaskViewModel> GetMyTasks()
        {
            if (currentUserRepository.IsAdmin())
                return taskRepository.GetList();
            else
                return taskRepository.GetByOwner(currentUserRepository.GetCurrentUserId());
        }

        public void UpdateTask(CreateUserTaskViewModel taskdata)
        {
            var task = taskRepository.GetTask(taskdata.Id);
            var currentUserId = currentUserRepository.GetCurrentUserId().ToString();
            if (task.TaskStatusId == (int)UserTaskStatus.Completed)
                throw new InvalidOperationException();
            if (task.CreatedByUser_Id == currentUserId)
            {
                task.AssignedToUser_Id = taskdata.AssignedTo.ToString();
                task.TaskDesc = taskdata.Desc;
                task.TaskTitle = taskdata.Title;
                task.DueDate = taskdata.DueDate;

                task.DateUpdated = DateTime.Now;
                task.UpdatedBy = new Guid(currentUserId);

                taskRepository.Update(task);
            }
            else
                throw new UnauthorizedAccessException();
        }

        public CreateUserTaskViewModel GetTaskById(Guid taskId)
        {
            var task = taskRepository.GetTask(taskId);
            var currentUserId = currentUserRepository.GetCurrentUserId().ToString();
            if (task.CreatedByUser_Id == currentUserId
                || task.AssignedToUser_Id == currentUserId
                || currentUserRepository.IsAdmin())
            {
                return new CreateUserTaskViewModel()
                {
                    Id = task.TaskId,
                     AssignedTo = new Guid(task.AssignedToUser_Id),
                    CreatedBy = new Guid(task.CreatedByUser_Id),
                    Desc = task.TaskDesc,
                     DueDate = task.DueDate,
                     Title = task.TaskTitle,
                     Status = task.TaskStatus.TaskStatusName
                };
            }
            else
                throw new UnauthorizedAccessException();
        }

        public List<UserInfo> GetUserList()
        {
            return taskRepository.GetUserList();
        }

        public void DeleteTask(Guid taskId)
        {
            var task = taskRepository.GetTask(taskId);
            var currentUserId = currentUserRepository.GetCurrentUserId().ToString();
            if (task.CreatedByUser_Id == currentUserId || currentUserRepository.IsAdmin())
            {
                taskRepository.Delete(taskId);
            }
            else
                throw new UnauthorizedAccessException();
            
        }

        public void CompleteTask(Guid taskId)
        {
            var task = taskRepository.GetTask(taskId);
            var currentUserId = currentUserRepository.GetCurrentUserId().ToString();
            if (task.TaskStatusId == (int)UserTaskStatus.Completed)
                throw new InvalidOperationException();
            if (task.AssignedToUser_Id == currentUserId)
            {
                task.TaskStatusId = (int)UserTaskStatus.Completed;
                taskRepository.Update(task);
            }
            else
                throw new UnauthorizedAccessException();
        }

        public void AssignTask(Guid taskId, Guid userId)
        {
            var task = taskRepository.GetTask(taskId);
            var currentUserId = currentUserRepository.GetCurrentUserId().ToString();
            if (task.TaskStatusId == (int)UserTaskStatus.Completed)
                throw new InvalidOperationException();
            if (task.AssignedToUser_Id == currentUserId || task.CreatedByUser_Id == currentUserId)
            {
                task.AssignedToUser_Id = userId.ToString();
                taskRepository.Update(task);
            }
            else
                throw new UnauthorizedAccessException();
        }
    }
}
