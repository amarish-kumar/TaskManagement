namespace TaskManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDateAllowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserTasks", "DateUpdated", c => c.DateTime());
            AlterColumn("dbo.UserTasks", "UpdatedBy", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserTasks", "UpdatedBy", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserTasks", "DateUpdated", c => c.DateTime(nullable: false));
        }
    }
}
