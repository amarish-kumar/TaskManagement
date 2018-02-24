using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure
{
    public class TasksDbContext : IdentityDbContext<ApplicationUser>
    {
        public static TasksDbContext Create()
        {
            return new TasksDbContext();
        }

        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Core.Entities.TaskStatus> Taskstatus { get; set; }
    }
}
