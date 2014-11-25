namespace testcodefirstService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("testcodefirst.TodoItems", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("testcodefirst.TodoItems", "Status");
        }
    }
}
