using AutoMapper;
using Core.Repositories;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Core.Entities;

namespace DevSite.Controllers
{
    public class FileUpload: Controller
    {
        //private string Path2 = "C:Users/Elev/source/repos/LMSg2/Web/wwwroot/firstName_lastName";

        //  private IWebHostEnvironment environment;
        //   private string Path3 = "firstName_lastName";
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private string userId;

        // private readonly UserManager<IdentityUser> userManager;
        public FileUpload(IUnitOfWork uow, IMapper mapper)
    {
           // this.environment = environment;
            this.uow = uow;
            this.mapper = mapper;
           
    }
 
    public async Task<IActionResult> Index()
    {
            // Skriv om UserDocumentViewModel så den bara har en filepath, en actrivity, en module, en kurs
            // hitta id av den usern som är inloggad
            // ta fram alla Document som är kopplade till den usern
            // spara i vymodellen filepath, course, module, activity
            // skicka en IEnumerable<UserDocumentViewModel>
            // i vyn, sortera dokumenten utifrån om de tillhör course, module, activity eller "övrigt"

            ////Fetch all files in the Folder (Directory).
            //string[] filePaths = Directory.GetFiles(Path.Combine(this.environment.WebRootPath, Path2));
            ////var filename = ""
            //    // $"upload/{filename}"
            ////Copy File names to Model collection.
            //List<FileName> files = new List<FileName>();
            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new FileName { Name = Path.GetFileName(filePath) });
            //}


            //            [11:45] Karl Friman(Gäst)


            //var allUsers = await uow.LMSUserRepository.GetAll(includeAll: false);
            //            var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Student");

            //            var model = mapper.Map<IEnumerable<LMSUserViewModel>>(filteredUsers);
            //            return View(model);
            //​[11:45] Karl Friman(Gäst)


            //public async Task<IActionResult> AdminStudents()
            //            {​​​​​
            //var allUsers = await uow.LMSUserRepository.GetAll(includeAll: false);
            //                var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Student");

            //                var model = mapper.Map<IEnumerable<LMSUserViewModel>>(filteredUsers);
            //                return View(model);
            //            }​​​​​
            //   var userId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            // var userId = HttpContextAccessor.
            
        userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          //   if (!String.IsNullOrEmpty(userId))
          
            var allUsers = await uow.LMSUserRepository.GetAll(includeAll: false);
            DocumentFileViewModel docsViewModel = null;

            foreach (LMSUser u in allUsers)
            {
                if(u.Id.Equals(userId))
                {
                    docsViewModel = new DocumentFileViewModel
                    {
                        Course = u.Course,
                        Documents = u.Course.Documents,
                        Modules = u.Course.Modules,
                        FilePath = u.Course.Document.FilePath
                    };

                }

            }


            var filteredUsersById = allUsers.FindAll(user => user.Id == userId);

            

            var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Student");
            var id = filteredUsers.Select(a => a.Id);


            var model10 = mapper.Map<IEnumerable<LMSUserViewModel>>(filteredUsers);
            var courseList = await uow.CourseRepository.GetAll(includeAll: true);
            var model = mapper.Map<IEnumerable<CourseViewModel>>(courseList);

            var moduleList = await uow.ModuleRepository.GetAll(includeAll: true);
            var model1 = mapper.Map<IEnumerable<ModuleViewModel>>(moduleList);


            var activitiesList = await uow.ActivityRepository.GetAll(includeAll: true);
            var model2 = mapper.Map<IEnumerable<ActivityViewModel>>(activitiesList);


            var usersList = await uow.LMSUserRepository.GetAll(includeAll: true);
            var model3 = mapper.Map<IEnumerable<LMSUserViewModel>>(usersList);

          
           
            return View(docsViewModel);
    }
 
    //public FileResult DownloadFile(string fileName)
    //{
    //    //Build the File Path.
    //    string path = Path.Combine(this.environment.WebRootPath, Path2) + "/"+ fileName;
 
    //    //Read the File data into Byte Array.
    //    byte[] bytes = System.IO.File.ReadAllBytes(path);
 
    //    //Send the File to Download.
    //    return File(bytes, "application/octet-stream", fileName);
    //}












    }
}
