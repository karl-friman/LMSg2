using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevSite.Services
{
    public interface IActivitySelectService
    {
        Task<IEnumerable<SelectListItem>> GetActivityAsync();
    }
}