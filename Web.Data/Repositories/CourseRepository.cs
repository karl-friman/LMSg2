﻿using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Data;

namespace Web.Data.Repositories
{
    class CourseRepository : IAsyncRepository<Course>
    {
        private readonly ApplicationDbContext db;
        public CourseRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Course>> GetAllWithCourseAndModule(bool includeAll)
        {
            if (includeAll)
            {
                List<Course> courseList = await db.Courses
                            .Include(d => d.Documents)
                            .Include(u => u.Users)
                            .Include(m => m.Modules)
                            .ThenInclude(a => a.Activities)
                            .ThenInclude(d => d.Documents)
                            .Include(m => m.Modules)
                            .ThenInclude(a => a.Activities)
                            .ThenInclude(at => at.ActivityType)
                            .OrderBy(x => x.Name)
                            .ToListAsync();
                return courseList;

            }
            else
            {
                List<Course> courseList = await db.Courses.ToListAsync();
                return courseList;
            }
        }
        public async Task<Course> GetOne(int? id, bool includeAll)
        {
            if (includeAll)
            {

                return await db.Courses
                            .Include(d => d.Documents)
                            .Include(u => u.Users)
                            .Include(m => m.Modules)
                            .ThenInclude(a => a.Activities)
                            .ThenInclude(d => d.Documents)
                            .Include(m => m.Modules)
                            .ThenInclude(a => a.Activities)
                            .ThenInclude(at => at.ActivityType)
                            .OrderBy(x => x.Name)
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                return await db.Courses
                            .FirstOrDefaultAsync(m => m.Id == id);
            }
        }
        //public async Task<List<Course>> GetAssignmentForUser(string userId){

        //    LMSUser user = await uow.LMSUserRepository.GetOne(userId, includeAll: false);
        //    IEnumerable<Activity> activities = course.Modules
        //                                        .SelectMany(a => a.Activities)
        //                                        .ToList()
        //                                        .Where(a => a.ActivityType.Name == "Assignment");

        //    List<Course> courseList = await db.Courses
        //        .Include(d => d.Documents)
        //        .Include(u => u.Users)
        //        .Include(m => m.Modules)
        //        .ThenInclude(a => a.Activities)
        //        .ThenInclude(d => d.Documents)
        //        .Include(m => m.Modules)
        //        .ThenInclude(a => a.Activities)
        //        .ThenInclude(at => at.ActivityType)
        //        .OrderBy(x => x.Name)
        //        .ToListAsync();
        //    return courseList;
        //}

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
        public async Task AddAsync(Course course)
        {
            await db.AddAsync(course);
        }
        public void Update(Course course)
        {
            db.Update(course);
        }
        public void Remove(Course course)
        {
            db.Remove(course);
        }
        public bool Any(int? Id)
        {
            return db.Courses.Any(m => m.Id == Id);
        }

        public async Task<Course> GetOne(string id, bool includeAll)
        {
            if (includeAll)
            {

                return await db.Courses
                             .Include(d => d.Documents)
                             .Include(m => m.Modules)
                             .ThenInclude(a => a.Activities)
                             .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
            else
            {
                return await db.Courses
                            .FirstOrDefaultAsync(m => m.Id.Equals(id));
            }
        }

        public bool Any(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LMSUser>> GetAllWithCourseAndModule()
        {
            throw new NotImplementedException();
        }
    }
}
