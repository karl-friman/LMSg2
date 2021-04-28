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

                //if (await context.Courses.AnyAsync()) return;

                var userManager = services.GetRequiredService<UserManager<LMSUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var fake = new Faker("sv");

                var courses = new List<Course>();
                var modules = new List<Module>();
                var documents = new List<Document>();
                var activities = new List<Activity>();
                var activityTypes = new List<ActivityType>();

                for (int i = 0; i < 8; i++)
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
                    for (int i = 0; i < fake.Random.Int(2, 5); i++)
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
                 }
                await context.AddRangeAsync(modules);          
                foreach (Course course in courses)
                    {
                        for (int i = 0; i < fake.Random.Int(1, 10); i++)
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
                    }
                await context.AddRangeAsync(documents);

                //fler aktiviteter ibland 2 ibland 8
                foreach (Module module in modules)
                {
                    for (int i = 0; i < fake.Random.Int(1, 10); i++)
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
                }
                   await context.AddRangeAsync(activities);

                foreach (Module module in modules)
                {
                    for (int i = 0; i < fake.Random.Int(1, 10); i++)
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
                }
                await context.AddRangeAsync(documents);

                foreach (Activity activity in activities)
                {
                    for (int i = 0; i < fake.Random.Int(1, 10); i++)
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
                }
                await context.AddRangeAsync(documents);

                activityTypes.Add(new ActivityType() { Name = "Assignement" });
                activityTypes.Add(new ActivityType() { Name = "Lecture" });
                activityTypes.Add(new ActivityType() { Name = "E-Learning" });

                for (int i = 0; i < 20; i++)
                {
                    var activity = new Activity()
                    {
                        ActivityType = fake.Random.ListItem(activityTypes),
                        Module = fake.Random.ListItem(modules),
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Hacker.Verb(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),
                    };

                    activities.Add(activity);
                }
                await context.AddRangeAsync(activities);


                //   await context.SaveChangesAsync();
                //15-18 students for each course auto generated with bogus + bogus avatar
                //7 admins auto generated with bogus + avatar 
                //1 admin spawned with email: admin@admin.com and pw: admin
                //fake.PickRandom<UserType>();


                //var roleNames = new[] { "Admin", "Student" };

                //foreach (var roleName in roleNames)
                //{
                //    if (await roleManager.RoleExistsAsync(roleName)) continue;

                //    var role = new IdentityRole { Name = roleName };
                //    var result = await roleManager.CreateAsync(role);

                //    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                //}

                //for (int i = 0; i < fake.Random.Int(7); i++)
                //{

                //    var adminEmail = fake.Internet.Email();

                //    var foundAdmin = await userManager.FindByEmailAsync(adminEmail);

                //    if (foundAdmin != null) return;


                //    var admin = new LMSUser
                //    {

                //        UserName = fake.Internet.UserName(),
                //        Email = fake.Internet.Email(),
                //        FirstName = fake.Name.FirstName(),
                //        LastName = fake.Name.LastName(),
                //        Avatar = fake.Internet.Avatar(),
                //        PhoneNumber = fake.Phone.PhoneNumberFormat(),
                       
                //    };

                   // var adminpw = "asdfasdf123!A";

                   // var addAdminResult = await userManager.CreateAsync(admin, adminpw);

                   // if (!addAdminResult.Succeeded) throw new Exception(string.Join("\n", addAdminResult.Errors));

                    //var adminUser = await userManager.FindByEmailAsync(adminEmail);

                    //foreach (var role in roleNames)
                    //{
                    //    if (await userManager.IsInRoleAsync(adminUser, role)) continue;

                    //    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, role);

                    //    if (!addToRoleResult.Succeeded) throw new Exception(string.Join("\n", addToRoleResult.Errors));
                    //}


                //}
                await context.SaveChangesAsync();
            }
        }
    }
}