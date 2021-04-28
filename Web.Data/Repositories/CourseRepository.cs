using Core.Entities;
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
    class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext db;
        public CourseRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Course>> GetAllCourses(bool includeAll)
        {
            if (includeAll)
            {
                List<Course> courseList = await db.Courses
                                .Include(d => d.Documents)
                                .Include(m => m.Modules)
                                .ThenInclude(a => a.Activities)
                                .ToListAsync();
                return courseList;

            }
            else
            {
                List<Course> courseList = await db.Courses.ToListAsync();
                return courseList;
            }
        }
        public List<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }
    }
}
