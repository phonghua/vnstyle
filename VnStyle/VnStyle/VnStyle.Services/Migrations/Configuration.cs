using VnStyle.Services.Data.Domain.Memberships;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VnStyle.Services.Data.VnStyleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VnStyle.Services.Data.VnStyleContext context)
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


            context.AspNetClients.AddOrUpdate(p => new { p.Id }, new AspNetClient
            {
                Id = "fe3429036f404047865a48a5f8739c94",
                Active = true,
                AllowedOrigin = "*",
                ApplicationType = EAspNetApplicationTypes.JavaScript,
                Name = "Js",
                Secret = "67b4b438cc37427792a2b1521f10cba4",
                RefreshTokenLifeTime = 1
            });

            context.AspNetRoles.AddOrUpdate(p=> new {p.Name}, new AspNetRole {Name = "admin"});

            context.SaveChanges();
        }
    }
}
