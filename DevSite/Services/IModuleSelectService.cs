using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevSite.Services
{
    public interface IModuleSelectService
    {
        Task<IEnumerable<SelectListItem>> GetModuleAsync();
    }
}