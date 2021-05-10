using AutoMapper;
using Core.Repositories;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Core.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DevSite.Controllers
{
    public class FileUpload: Controller
    {
        private string Path2 = "C:Users/Elev/source/repos/LMSg2/Web/wwwroot/firstName_lastName";

        private IWebHostEnvironment environment;
        private string Path3 = "firstName_lastName";
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private string userId;

        // private readonly UserManager<IdentityUser> userManager;
        public FileUpload(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment environment)
    {
            this.environment = environment;
            this.uow = uow;
            this.mapper = mapper;
           
    }
 
    public IActionResult DownloadableFiles()
    {
        
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path2);
           
            //Copy File names to Model collection.
            List<FileName> files = new List<FileName>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileName { Name = Path.GetFileName(filePath) });
            }
           
            return View(files);
    }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.environment.WebRootPath, Path2) + "/" + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

    }
}
