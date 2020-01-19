namespace BlogProject.Migrations
{
    using BlogProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogProject.Models.ApplicationDbContext context)
        {
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin")) //Checks database for role named Admin, if none found creates one.
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            #endregion
            #region User Creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "andystache@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "andystache@mailinator.com",
                    Email = "andystache@mailinator.com",
                    FirstName = "Andrew",
                    LastName = "Russell",
                    DisplayName = "The Dev",
                }, "Abc&123!") ; //Password
            }

            if (!context.Users.Any(u => u.Email == "JasonTwichell@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JasonTwichell@Mailinator.com",
                    Email = "JasonTwichell@Mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Teach",
                }, "Abc&123!"); //Password
            }
            #endregion
            #region Assign roles to User
            var adminId = userManager.FindByEmail("andystache@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var modId = userManager.FindByEmail("JasonTwichell@Mailinator.com").Id;
            userManager.AddToRole(modId, "Moderator");
            #endregion
        }
    }
}
