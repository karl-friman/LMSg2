//using AutoMapper;
//using Core.Repositories;
//using Core.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using System.Security.Claims;
//using Core.Entities;
//using Microsoft.AspNetCore.Identity;

//namespace Web.Controllers
//{
//    public class FileUpload: Controller
//    {
//        //private string Path2 = "C:Users/Elev/source/repos/LMSg2/Web/wwwroot/firstName_lastName";

//        //  private IWebHostEnvironment environment;
//        //   private string Path3 = "firstName_lastName";
//        private readonly IUnitOfWork uow;
//        private readonly IMapper mapper;
//        //private string userId;
//        private readonly UserManager<IdentityUser> userManager;


//        // private readonly UserManager<IdentityUser> userManager;
//        public FileUpload(IUnitOfWork uow, IMapper mapper, UserManager<IdentityUser> userManager)
//    {
//           // this.environment = environment;
//            this.uow = uow;
//            this.mapper = mapper;
//            this.userManager = userManager;
           
//    }
 
//    public async Task<IActionResult> Index()
//    {
//            // Skriv om UserDocumentViewModel så den bara har en filepath, en actrivity, en module, en kurs
//            // hitta id av den usern som är inloggad
//            // ta fram alla Document som är kopplade till den usern
//            // spara i vymodellen filepath, course, module, activity
//            // skicka en IEnumerable<UserDocumentViewModel>
//            // i vyn, sortera dokumenten utifrån om de tillhör course, module, activity eller "övrigt"

//            // userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            //   if (!String.IsNullOrEmpty(userId))
//            var userId = userManager.GetUserId(User);
//            var currentUser = await uow.LMSUserRepository.GetOne(userId, false);
//            var allDocuments = await uow.DocumentRepository.GetAll(false);
//            var userDocuments = allDocuments.Where(u => u.LMSUser == currentUser);

//            var allUsers = await uow.LMSUserRepository.GetAll(includeAll: true);
//            DocumentFileViewModel docsViewModel = null;
//            foreach (LMSUser u in allUsers)
//            {
//                if(u.Id.Equals(userId))
//                {
//                    docsViewModel = new DocumentFileViewModel
//                    {
//                        Course = u.Course,
//                        Modules = u.Course.Modules,
//                        Documents = u.Course.Documents,
//                        Activities = u.Course.Modules.SelectMany(a=>a.Activities).ToList(),

//                    };

//                }

//            }
//            //var filteredUsersById = allUsers.FindAll(user => user.Id == userId);
//            //var filteredUsers = allUsers.FindAll(z => z.UserType.ToString() == "Student");
//            //var id = filteredUsers.Select(a => a.Id);          
           
//            return View(docsViewModel);
//    }
 
  
//    }
//}
