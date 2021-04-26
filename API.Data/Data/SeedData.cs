using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Data.Extensions;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Data.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            await using (var db = new DbContextAPI(services.GetRequiredService<DbContextOptions<DbContextAPI>>()))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.MigrateAsync();

                if (!await db.Authors.AnyAsync())
                {
                    await db.AddRangeAsync(getLevels());
                    await db.AddRangeAsync(getAuthors(10));
                    await db.AddRangeAsync(getsSubjects(10));
                    await db.SaveChangesAsync();

                    await db.AddRangeAsync(GenerateLiterature(db, 10));
                    await db.SaveChangesAsync();
                }

            }
        }

        private static string[] levelNames = { "Nybörjare", "Medel", "Avancerad" };
        private static List<Level> getLevels()
        {
            List<Level> levels = new List<Level>();
            foreach (var levelName in levelNames)
            {
                levels.Add(new Level()
                {
                    Name = levelName
                });
            }
            return levels;
        }

        private static List<Author> getAuthors(int amount)
        {
            Faker faker = new Faker("sv");

            List<Author> authors = new List<Author>();
            for (int i = 0; i < amount; i++)
            {
                authors.Add(new Author()
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    DateOfBirth = faker.Date.Past(100)
                });

            }

            return authors;
        }

        private static List<Subject> getsSubjects(int amount)
        {
            Faker faker = new Faker("sv");
            List<Subject> subjects = new List<Subject>();
            for (int i = 0; i < amount; i++)
            {
                subjects.Add(new Subject()
                {
                    Name = faker.Company.CompanyName()
                });
            }

            return subjects;
        }

        private static List<Literature> GenerateLiterature(DbContextAPI db, int amount)
        {
            Faker faker = new Faker("sv");


            List<Author> authors = db.Authors.ToList();
            List<Subject> subjects = db.Subject.ToList();


            List<Literature> literatureList = new List<Literature>();
            for (int i = 0; i < amount; i++)
            {
                authors.Shuffle();
                subjects.Shuffle();

                List<Level> levels = db.Levels.ToList();

                literatureList.Add(new Literature()
                {
                    Title = faker.Lorem.Sentence(),
                    PublishDate = faker.Date.Past(10),
                    Description = faker.Lorem.Paragraph(),
                    Authors = authors.Take(faker.Random.Int(1, 3)).ToList(),
                    Subjects = subjects.Take(faker.Random.Int(1, 3)).ToList(),
                    Level = levels[faker.Random.Int(0, levels.Count - 1)]
                });
            }

            return literatureList;
        }

    }
}
