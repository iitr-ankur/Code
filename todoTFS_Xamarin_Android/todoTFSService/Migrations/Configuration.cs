namespace todoTFSService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using todoTFSService.DataObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<todoTFSService.Models.todoTFSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(todoTFSService.Models.todoTFSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Contact[] contacts = new Contact[] {
            //    new Contact{Name = "Ankur", PhoneNum = "AnkurNumber"}, 
            //    new Contact{Name = "Naveen", PhoneNum = "NaveenNum"}, 
            //    new Contact{Name = "L N", PhoneNum = "L N PhoneNum"}};
            //context.Contacts.AddOrUpdate(p => p.Name, contacts);
        }
    }
}
