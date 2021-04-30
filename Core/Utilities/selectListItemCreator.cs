using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class selectListItemCreator<T>
    {
        private readonly IAsyncRepository<T> asyncRepository;
        public selectListItemCreator(IAsyncRepository<T> asyncRepository)
        {
            this.asyncRepository = asyncRepository;
        }

        //1. Hur göra denna generisk
        //2. Hur modifiera modellen (utan att göra det
        //i entiteten) men utan viewmodel?
        public async Task<IEnumerable<SelectListItem>> GetSelectListItems()
        {
            List<Course> dbList = await asyncRepository.GetAll(includeAll: true);
            if (GetType().Equals(typeof(Course)))
            {
                dbList = await asyncRepository.GetAll(includeAll: true);
            }

            //List<Course> dbList = null;
            //switch (GetType())
            //{
            //    case typeof(Course):
            //        dbList = await uow.CourseRepository.GetAll(includeAll: true);
            //        break;
            //    default:
            //        break;
            //}
            //if (GetType().Equals(typeof(Course)))
            //{
            //    dbList = await uow.CourseRepository.GetAll(includeAll: true);
            //}
            //var dbList = await uow.CourseRepository.GetAll(includeAll: true);
            var selectListItems = new List<SelectListItem>();
            foreach (var type in dbList)
            {
                var newType = (new SelectListItem
                {
                    Text = type.Name,
                    Value = type.Id.ToString(),
                });
                selectListItems.Add(newType);
            }
            return selectListItems;
        }
    }
}
