using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using TaskManagement.Core.Entities;
using TaskManagement.Core.ViewModels;
using TaskManagement.Repository;
using TaskManagement.Service;

namespace TaskManagement.Tests
{
    [TestClass]
    public class TestTaskManagement
    {
        Guid userId = Guid.NewGuid();
        ICurrentUserRepository GetCurrentUserRepository()
        {
            Mock<ICurrentUserRepository> currentUserRepo = new Mock<ICurrentUserRepository>();
             currentUserRepo.Setup(x => x.GetCurrentUserId()).Returns(userId);
            currentUserRepo.Setup(x => x.IsAdmin()).Returns(false);

            return currentUserRepo.Object;
        }
        ICurrentUserRepository GetCurrentUserRepositoryForAdmin()
        {
            Mock<ICurrentUserRepository> currentUserRepo = new Mock<ICurrentUserRepository>();
            currentUserRepo.Setup(x => x.IsAdmin()).Returns(true);
            return currentUserRepo.Object;
        }
        [TestMethod]
        public void TestCreateTask()
        {
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();
           
            taskRepo.Setup(x => x.Create(new UserTask()));
            TaskService taskService = new TaskService(taskRepo.Object,GetCurrentUserRepository());
            var taskData = new CreateUserTaskViewModel();
            taskService.CreateTask(taskData);
        }

        [TestMethod]
        public void TestDeleteTask_with_Owner()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();
           
            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { CreatedByUser_Id = userId.ToString() });
            TaskService taskService = new TaskService(taskRepo.Object,GetCurrentUserRepository());

            taskService.DeleteTask(taskId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestDeleteTask_with_nonOwner()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { CreatedByUser_Id = Guid.NewGuid().ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.DeleteTask(taskId);
        }


        [TestMethod]
        public void TestAssignTask_byAssignedUser()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { AssignedToUser_Id = userId.ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.AssignTask(taskId,userId);
        }
        [TestMethod]
        public void TestAssignTask_byOwner()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { CreatedByUser_Id = userId.ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.AssignTask(taskId, userId);
        }
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestAssignTask_byOtherUser()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask(){ });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.AssignTask(taskId, userId);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAssignTask_CompletedTask()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { TaskStatusId = 3 });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.AssignTask(taskId, userId);
        }
        [TestMethod]
        public void TestCompleteTask_With_AssignedUser()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { AssignedToUser_Id = userId.ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.CompleteTask(taskId);
        }
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void TestCompleteTask_Without_AssignedUser()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { CreatedByUser_Id = Guid.NewGuid().ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.CompleteTask(taskId);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCompleteTask_CompletedTask()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { TaskStatusId = 3 });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepository());

            taskService.CompleteTask(taskId);
        }
        [TestMethod]
        public void TestDeleteTask_with_Admin()
        {
            var taskId = Guid.NewGuid();
            Mock<ITaskRepository> taskRepo = new Mock<ITaskRepository>();

            taskRepo.Setup(x => x.GetTask(taskId)).Returns(new UserTask() { CreatedByUser_Id = Guid.NewGuid().ToString() });
            TaskService taskService = new TaskService(taskRepo.Object, GetCurrentUserRepositoryForAdmin());

            taskService.DeleteTask(taskId);
        }
    }
}
