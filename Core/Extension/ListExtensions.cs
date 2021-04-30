using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Extension
{
    public static class ListExtensions
    {
        public static async Task<IEnumerable<SelectListItem>> GetSelectListItems<T>(this IAsyncRepository<T> repo)
        {
            var repoList = repo.GetAll(includeAll: false).Result;
            switch (repoList)
            {
                case List<Module> modules:
                    return modules.Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    }).ToList();
                case List<Course> courses:
                    return courses.Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    }).ToList();
                case List<Activity> activities:
                    return activities.Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    }).ToList();
                case List<Document> documents:
                    return documents.Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                    }).ToList();
                case List<LMSUser> LMSUser:
                    return LMSUser.Select(item => new SelectListItem
                    {
                        Text = item.FullName,
                        Value = item.Id.ToString(),
                    }).ToList();
                default:
                    return null;
            }
        }


    }
}