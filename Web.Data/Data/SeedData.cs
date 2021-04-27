using Bogus;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Data.Data;


namespace Web.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(IServiceProvider services)

        {
            using (var context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                //  if (await contex.AnyAsync()) return;

                //  var userManager = services.GetRequiredService<UserManager<LMSUser>>();
                //   var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var fake = new Faker("sv");

                var courses = new List<Course>();
                var modules = new List<Module>();
                var documents = new List<Document>();
                var activities = new List<Activity>();
                var activityTypes = new List<ActivityType>();

                for (int i = 0; i < 20; i++)
                {
                    var course = new Course()
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Hacker.Verb(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),
                    };

                    courses.Add(course);
                }

                await context.AddRangeAsync(courses);

                foreach (Course course in courses)
                {
                    var module = new Module
                    {
                        Course = course,

                        Name = fake.Company.CatchPhrase(),

                        Description = fake.Hacker.Verb(),

                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),

                    };
                    modules.Add(module);

                }

                await context.AddRangeAsync(modules);

                foreach (Course course in courses)
                {
                    var document = new Document

                    {

                        Name = fake.Company.CatchPhrase(),

                        Description = fake.Hacker.Verb(),

                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),


                    };
                    documents.Add(document);

                }

                await context.AddRangeAsync(documents);

                ///

                foreach (Module module in modules)
                {
                    var activity = new Activity

                    {

                        Module = module,

                        Name = fake.Company.CatchPhrase(),

                        Description = fake.Hacker.Verb(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),


                    };
                    activities.Add(activity);
                }


                foreach (Module module in modules)
                {
                    var document = new Document

                    {

                        Name = fake.Company.CatchPhrase(),

                        Description = fake.Hacker.Verb(),

                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),


                    };
                    documents.Add(document);
                }
                await context.AddRangeAsync(documents);

                activityTypes.Add(new ActivityType() { Name = "Assignement"});
                activityTypes.Add(new ActivityType() { Name = "Lecture" });
                activityTypes.Add(new ActivityType() { Name = "E-Learning" });


                await context.AddRangeAsync(activityTypes);

                foreach (Activity activity in activities)
                {
                    var document = new Document

                    {

                        Name = fake.Company.CatchPhrase(),

                        Description = fake.Hacker.Verb(),

                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),


                    };
                    documents.Add(document);
                }
                await context.AddRangeAsync(documents);


                //////??????

                //foreach (ActivityType activityType in activityTypes)
                //{
                //    var activity = new Activity
                //    {
                //        ActivityType = activityType,
                //        Name = fake.Company.CatchPhrase(),
                //        Description = fake.Hacker.Verb(),
                //        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                //        EndDate = DateTime.Now.AddMonths(4),
                //    };
                //    activities.Add(activity);
                //}
                //await context.AddRangeAsync(activities);



                for (int i = 0; i < 20; i++)
                {
                    var activity = new Activity()
                    {
                        //ActivityType = new ActivityType() { Name = "E-Learning" },
                        Module = fake.Random.ListItem(modules),
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Hacker.Verb(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),
                    };

                    activities.Add(activity);
                }
                await context.AddRangeAsync(activities);


                await context.SaveChangesAsync();










                //foreach (Module module in modules)
                //{
                //    var activity = new Activity

                //    {

                //        Module = module,

                //        Name = fake.Company.CatchPhrase(),

                //        Description = fake.Hacker.Verb(),
                //        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                //        EndDate = DateTime.Now.AddMonths(4),


                //    };
                //    activities.Add(activity);
                //}

                //await context.AddRangeAsync(activities);





                //foreach (Activity activity in activities)
                //{
                //    var document = new Document

                //    {

                //        Name = fake.Company.CatchPhrase(),

                //        Description = fake.Hacker.Verb(),

                //        TimeStamp = DateTime.Now,
                //        FilePath = fake.System.FilePath(),


                //    };
                //    documents.Add(document);
                //}
                //await context.AddRangeAsync(documents);












                //foreach (Module module in modules)
                //{
                //    var document = new Document

                //    {

                //        Name = fake.Company.CatchPhrase(),

                //        Description = fake.Hacker.Verb(),

                //        TimeStamp = DateTime.Now,
                //        FilePath = fake.System.FilePath(),


                //    };
                //    documents.Add(document);
                //}

                //await context.AddRangeAsync(documents);




                //var roleNames = new[] { "Admin", "Member" };

                //foreach (var roleName in roleNames)
                //{
                //    if (await roleManager.RoleExistsAsync(roleName)) continue;

                //    var role = new IdentityRole { Name = roleName };
                //    var result = await roleManager.CreateAsync(role);

                //    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                //}

                //var adminEmail = "admin@lms.se";

                //var foundAdmin = await userManager.FindByEmailAsync(adminEmail);

                //if (foundAdmin != null) return;

                //var admin = new LMSUser
                //{
                //    UserName = "Admin",
                //    Email = adminEmail,
                //};

                //var addAdminResult = await userManager.CreateAsync(admin, adminPW);

                //if (!addAdminResult.Succeeded) throw new Exception(string.Join("\n", addAdminResult.Errors));

                //var adminUser = await userManager.FindByEmailAsync(adminEmail);

                //foreach (var role in roleNames)
                //{
                //    if (await userManager.IsInRoleAsync(adminUser, role)) continue;

                //    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, role);

                //    if (!addToRoleResult.Succeeded) throw new Exception(string.Join("\n", addToRoleResult.Errors));
                //}


            }
        }
    }
}