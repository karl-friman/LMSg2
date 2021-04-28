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

                var fake = new Faker();

                //Create Courses
                var courses = courseCreator(5, fake);
                await context.AddRangeAsync(courses);

                //Create Modules
                var modules = modulesCreator(5, fake, courses);
                await context.AddRangeAsync(modules);
                //Save to Db
                await context.SaveChangesAsync();

                var activityTypes = new List<ActivityType>();
                activityTypes.Add(new ActivityType() { Name = "Assignement" });
                activityTypes.Add(new ActivityType() { Name = "Lecture" });
                activityTypes.Add(new ActivityType() { Name = "E-Learning" });
                activityTypes.Add(new ActivityType() { Name = "Excursion" });
                activityTypes.Add(new ActivityType() { Name = "Internship" });
                activityTypes.Add(new ActivityType() { Name = "Teamwork" });

                var activities = activitiesCreator(fake, activityTypes, modules);
                await context.AddRangeAsync(activities);
                //Save to Db
                await context.SaveChangesAsync();


                //await context.SaveChangesAsync();
                //15-18 students for each course auto generated with bogus + bogus avatar
                //7 admins auto generated with bogus + avatar 
                //1 admin spawned with email: admin@admin.com and pw: admin
                //fake.PickRandom<UserType>();

                //Create Users
                var users = usersCreator(amountOfStudents: 50, amountOfAdmins: 6, fake, courses);

                var admin = new LMSUser
                {
                    Email = "admin@admin.com",
                    FirstName = "Main System",
                    LastName = "Administrator",
                    PhoneNumber = fake.Phone.PhoneNumberFormat(),
                    PasswordHash = "asdfasdf123!A",
                    UserType = UserType.Admin
                };
                users.Add(admin);
                await context.AddRangeAsync(users);
                //Save to Db
                await context.SaveChangesAsync();

                var documents = documentsCreator(lmsusers: users, fake: fake, activities: activities, courses: courses, modules: modules);
                await context.AddRangeAsync(documents);

                //Save to Db
                await context.SaveChangesAsync();

            }
        }

        private static List<LMSUser> usersCreator(int amountOfStudents, int amountOfAdmins, Faker fake, List<Course> courses)
        {
            List<LMSUser> users = new List<LMSUser>();
            foreach (Course course in courses)
            {
                for (int i = 0; i < amountOfAdmins; i++)
                {
                    Random random = new Random();
                    int rand = random.Next(0, 40) - 68;
                    var user = new LMSUser
                    {
                        Email = fake.Internet.Email(),
                        FirstName = fake.Name.FirstName(),
                        LastName = fake.Name.LastName(),
                        Avatar = fake.Internet.Avatar(),
                        PhoneNumber = fake.Phone.PhoneNumber(),
                        UserType = UserType.Admin,
                        DateOfBirth = DateTime.Now.AddYears(rand)
                    };
                    users.Add(user);
                }
                for (int i = 0; i < amountOfStudents; i++)
                {
                    Random random = new Random();
                    int rand = random.Next(0,20) - 38;
                    var user = new LMSUser
                    {
                        Email = fake.Internet.Email(),
                        FirstName = fake.Name.FirstName(),
                        LastName = fake.Name.LastName(),
                        Avatar = fake.Internet.Avatar(),
                        PhoneNumber = fake.Phone.PhoneNumber(),
                        UserType = UserType.Student,
                        DateOfBirth = DateTime.Now.AddYears(rand),
                        CourseId = course.Id
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        private static List<Activity> activitiesCreator(Faker fake, List<ActivityType> activityTypes, List<Module> modules)
        {
            List<Activity> activities = new List<Activity>();
            foreach (Module module in modules)
            {
                for (int i = 0; i < fake.Random.Int(1, 8); i++)
                {
                    var activity = new Activity
                    {
                        ActivityType = fake.Random.ListItem(activityTypes),
                        Module = module,
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Lorem.Paragraphs(1),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        EndDate = DateTime.Now.AddMonths(4),
                    };
                    activities.Add(activity);
                }
            }
            return activities;
        }

        private static List<Document> documentsCreator(List<LMSUser> lmsusers,Faker fake, List<Activity> activities, List<Course> courses, List<Module> modules)
        {
            List<Document> documents = new List<Document>();

            foreach (Course course in courses)
            {
                for (int i = 0; i < fake.Random.Int(1, 2); i++)
                {
                    var document = new Document
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Lorem.Paragraphs(1),
                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),
                        CourseId = course.Id,     
                        LMSUser = fake.Random.ListItem(lmsusers)
                    };
                    documents.Add(document);
                }
            }

            foreach (Module module in modules)
            {
                for (int i = 0; i < fake.Random.Int(1, 2); i++)
                {
                    var document = new Document
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Lorem.Paragraphs(1),
                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),
                        ModuleId = module.Id,
                        LMSUser = fake.Random.ListItem(lmsusers)
                    };
                    documents.Add(document);
                }
            }

            foreach (Activity activity in activities)
            {
                for (int i = 0; i < fake.Random.Int(1, 3); i++)
                {
                    var document = new Document
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Lorem.Paragraphs(1),
                        TimeStamp = DateTime.Now,
                        FilePath = fake.System.FilePath(),
                        ActivityId = activity.Id,
                        LMSUser = fake.Random.ListItem(lmsusers)
                    };
                    documents.Add(document);
                }
            }
            return documents;
        }

        private static List<Module> modulesCreator(int amount, Faker fake, List<Course> courses)
        {
            List<Module> modules = new List<Module>();
            foreach (Course course in courses)
            {
                for (int i = 0; i < fake.Random.Int(2, amount); i++)
                {
                    //Modules cannot finish after courses have finished.
                    var startDate = course.StartDate.AddMonths(fake.Random.Int(0, 5));
                    var monthsToAdd = fake.Random.Int(1, 5);
                    var moduleEndDate = course.StartDate.AddMonths(monthsToAdd);
                    if (moduleEndDate > course.EndDate)
                    {
                        moduleEndDate = course.EndDate;
                    }

                    var module = new Module
                    {
                        Course = course,
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Lorem.Paragraphs(1),
                        StartDate = startDate,
                        EndDate = moduleEndDate,
                    };
                    modules.Add(module);
                }
            }
            return modules;
        }

        private static List<Course> courseCreator(int amount, Faker fake)
        {
            List<Course> courses = new List<Course>();
            for (int i = 0; i < amount; i++)
            {
                var courseLength = fake.Random.Int(1, 6);
                var courseEndDate = DateTime.Now.AddMonths(courseLength);
                var course = new Course()
                {
                    Name = fake.Company.CatchPhrase(),
                    Description = fake.Lorem.Paragraphs(1),
                    StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                    EndDate = courseEndDate,
                };

                courses.Add(course);
            }
            return courses;
        }
    }
}