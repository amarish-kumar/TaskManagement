namespace TaskManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtaskuserrelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "AssignedToUser_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserTasks", "CreatedByUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserTasks", "AssignedToUser_Id");
            CreateIndex("dbo.UserTasks", "CreatedByUser_Id");
            AddForeignKey("dbo.UserTasks", "AssignedToUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.UserTasks", "CreatedByUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            DropColumn("dbo.UserTasks", "AssignedTo");
            DropColumn("dbo.UserTasks", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserTasks", "CreatedBy", c => c.Guid(nullable: false));
            AddColumn("dbo.UserTasks", "AssignedTo", c => c.Guid(nullable: false));
            DropForeignKey("dbo.UserTasks", "CreatedByUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTasks", "AssignedToUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserTasks", new[] { "CreatedByUser_Id" });
            DropIndex("dbo.UserTasks", new[] { "AssignedToUser_Id" });
            DropColumn("dbo.UserTasks", "CreatedByUser_Id");
            DropColumn("dbo.UserTasks", "AssignedToUser_Id");
        }
    }
}
