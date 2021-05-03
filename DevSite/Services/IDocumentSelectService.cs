
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSite.Services
{
    public interface IDocumentSelectService
    {
        Task<IEnumerable<SelectListItem>> GetDocumentAsync();
    }
}
