using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagement.Core.ViewModels;
using TaskManagement.Service;

namespace TaskManagment.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        public TasksController(TaskService taskService)
        {
            TaskService = taskService;
        }

        public TaskService TaskService { get; }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateTask(CreateUserTaskViewModel taskdata)
        {
            TaskService.CreateTask(taskdata);
            return Ok();
        }

        [Route("assign")]
        [HttpPost]
        public IHttpActionResult AssignTask(CreateUserTaskViewModel taskdata)
        {
            TaskService.AssignTask(taskdata.Id, taskdata.AssignedTo);
            return Ok();
        }

        [Route("getuserlist")]
        [HttpGet]
        public IHttpActionResult GetUserList()
        {
            var users = TaskService.GetUserList();
            return Ok(users);
        }
        [Route("getmytasks")]
        [HttpGet]
        public IHttpActionResult GetMyTasks()
        {
            var users = TaskService.GetMyTasks();
            return Ok(users);
        }
        [Route("delete/{taskId}")]
        [HttpPost]
        public IHttpActionResult DeleteTask(Guid taskId)
        {
            TaskService.DeleteTask(taskId);
            return Ok();
        }
        [Route("complete/{taskId}")]
        [HttpPost]
        public IHttpActionResult CompleteTask(Guid taskId)
        {
            try
            {
                TaskService.CompleteTask(taskId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
           
        }
        [Route("update")]
        [HttpPost]
        public IHttpActionResult UpdateTask(CreateUserTaskViewModel taskdata)
        {
            TaskService.UpdateTask(taskdata);
            return Ok();
        }



        [Route("gettask/{taskId}")]
        [HttpGet]
        public IHttpActionResult GetTaskById(Guid taskId)
        {
            try
            {
                var task = TaskService.GetTaskById(taskId);
                return Ok(task);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }

        }
    }
}
