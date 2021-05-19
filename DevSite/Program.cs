
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.API;
using Core.Entities.API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Web.Data.Data;
using Web.Data.Data.Wrapper;

namespace DevSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
         //context.Database.EnsureDeleted();
         //context.Database.Migrate();
                var config = services.GetRequiredService<IConfiguration>();
                //dotnet user-secrets set "adminPW""LMS-Group2"
                var adminPW = config["adminPW"];
                var studentPW = config["studentPW"];
                try
                {
                    //queryAPI().Wait(); // test API
                    SeedData.InitAsync(services, adminPW, studentPW).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.Message, "Seed Fail");
                    throw;
                }
            }

            host.Run();
        }

        // Test API functionality
        public static async Task queryAPI()
        {
            APIClient api = new APIClient(44306);
            var postSuccess = await FetchAndPostTest(api);
            var patchSuccess = await FetchAndPatchTest(api);
            var deleteSuccess = await FetchAndDeleteTest(api);
        }

        private static async Task<bool> FetchAndPostTest(APIClient api)
        {
            var authors = await api.Fetch().GetAuthorsAsync(true);
            var added = await api.Post().Author(new Author()
            {
                Id = 0,
                FirstName = "korv",
                LastName = "potatis",
                DateOfBirth = DateTime.Now.AddDays(-3400),
                Literatures = new List<LiteratureViewModel>()
            });
            authors = await api.Fetch().GetAuthorsAsync(true);

            var subjects = await api.Fetch().GetSubjectsAsync(true);
            var subjectAdded = await api.Post().Subject(new Subject()
            {
                Id = 0,
                Literatures = new List<LiteratureViewModel>()
                {
                    new LiteratureViewModel()
                    {
                        Id = 4
                    }
                },
                Name = "Coolio"
            });
            subjects = await api.Fetch().GetSubjectsAsync(true);

            var literatures = await api.Fetch().GetLiteraturesAsync(true);
            var literatureAdded = await api.Post().Literature(new Literature()
            {
                Authors = new List<AuthorViewModel>()
                {
                    new AuthorViewModel()
                    {
                        Id = 4
                    }
                },
                Description = "cool description",
                Id = 0,
                LevelName = "Beginner",
                PublishDate = DateTime.Now.AddDays(-300),
                Subjects = new List<SubjectViewModel>()
                {
                    new SubjectViewModel()
                    {
                        Id = 4
                    }
                },
                Title = "Potatisarnas fight"
            });
            literatures = await api.Fetch().GetLiteraturesAsync(true);

            return added && literatureAdded && subjectAdded;
        }

        private static async Task<bool> FetchAndDeleteTest(APIClient api)
        {
            var deleteAuthor = await api.Delete().Author(1);
            var deleteLiterature = await api.Delete().Literature(1);
            var deleteSubject = await api.Delete().Subject(1);
            return deleteAuthor && deleteLiterature && deleteSubject;
        }

        private static async Task<bool> FetchAndPatchTest(APIClient api)
        {
            var authorPatch = new JsonPatchDocument().Replace("/firstName", "patchad");

            var author = await api.Fetch().GetAuthorAsync(4);

            var patchedAuthor = await api.Patch().Send("Author", 4, authorPatch);

            author = await api.Fetch().GetAuthorAsync(4);

            // ----------------------------- \\

            var literaturePatch = new JsonPatchDocument().Replace("/title", "patchad");

            var literature = await api.Fetch().GetLiteratureAsync(4);

            var patchedliterature = await api.Patch().Send("Literatures", 4, literaturePatch);

            literature = await api.Fetch().GetLiteratureAsync(4);

            // ----------------------------- \\

            var subjectPatch = new JsonPatchDocument().Replace("/name", "patchad");

            var subject = await api.Fetch().GetSubjectAsync(4);

            var patchedsubject = await api.Patch().Send("Subjects", 4, subjectPatch);

            subject = await api.Fetch().GetSubjectAsync(4);

            return patchedAuthor && patchedliterature && patchedsubject;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
