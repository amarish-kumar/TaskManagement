namespace TaskManagement.Infrastructure.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManagement.Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManagement.Infrastructure.TasksDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManagement.Infrastructure.TasksDbContext context)
        {
            //Task Status Default Data
            context.Taskstatus.AddOrUpdate(new Core.Entities.TaskStatus() { TaskStatusId = 1,TaskStatusName="New" });
            context.Taskstatus.AddOrUpdate(new Core.Entities.TaskStatus() { TaskStatusId = 2, TaskStatusName = "In Progress" });
            context.Taskstatus.AddOrUpdate(new Core.Entities.TaskStatus() { TaskStatusId = 3, TaskStatusName = "Completed" });

            //Roles Default Data
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Id = "c397da05-4719-4b96-b476-04f510fde973", Name="Admin" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Id = "468795ec-e234-42d8-96fb-c6958f97c508", Name = "Support" });

            //Users Default Data
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            CreateUser(userManager, "admin", "P@ssw0rd", "Admin");
            CreateUser(userManager, "support1", "P@ssw0rd", "Support");
            CreateUser(userManager, "support2", "P@ssw0rd", "Support");
            CreateUser(userManager, "support3", "P@ssw0rd", "Support");

        }
        private void  CreateUser(UserManager<ApplicationUser> userManager,string userName,string password,string role)
        {
            if (userManager.FindByName(userName)==null)
            {
                var userId = Guid.NewGuid().ToString();
                var userToInsert = new ApplicationUser { Id = userId, UserName = userName };
                var result = userManager.Create(userToInsert, password);
                if (result.Succeeded)
                    userManager.AddToRole(userId, role);
            }
        }
    }
}
