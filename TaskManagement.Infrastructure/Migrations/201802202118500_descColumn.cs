namespace TaskManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTasks", "TaskDesc", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTasks", "TaskDesc");
        }
    }
}
