using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.API;
using Core.Entities.API.FormModels;
using Core.Entities.API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Web.Data.Data.Wrapper;

namespace Web.Controllers
{
    public class LiteratureController : Controller
    {
        public readonly IMapper mapper;
        public readonly APIClient apiClient;
        public LiteratureController(IMapper mapper)
        {
            this.mapper = mapper;
            apiClient = new APIClient(44306);
        }

        // GET: LiteratureController
        public ActionResult Index()
        {
            var literatures = apiClient.Fetch().GetLiteraturesAsync(true).Result;

            return View(literatures.AsEnumerable());
        }

        // GET: LiteratureController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var course = apiClient.Fetch().GetLiteratureAsync(id, true).Result;

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: LiteratureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LiteratureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LiteratureCreateModel model)
        {
            if (!model.LevelName.Equals("Beginner") && !model.LevelName.Equals("Intermediate") && !model.LevelName.Equals("Advanced"))
            {
                ModelState.AddModelError("LevelName", "LevelName has to be one of the following: Beginner, Intermediate or Advanced.");
            }

            if (ModelState.IsValid)
            {
                var literature = new Literature()
                {
                    Id = 0,
                    Title = model.Title,
                    Description = model.Description,
                    LevelName = model.LevelName,
                    PublishDate = model.PublishDate,
                    Authors = new List<AuthorViewModel>(),
                    Subjects = new List<SubjectViewModel>()
                };

                foreach (var authorId in getSplitIds(model.AuthorsIds))
                {
                    literature.Authors.Add(new AuthorViewModel(){Id = authorId});
                }

                foreach (var subjectId in getSplitIds(model.SubjectsIds))
                {
                    literature.Subjects.Add(new SubjectViewModel() { Id = subjectId });
                }

                var added = apiClient.Post().Literature(literature).Result;

                if (!added)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public List<int> getSplitIds(string idString)
        {
            List<int> ids = new List<int>();
            if (idString.Contains(","))
            {
                var splitAuthorIds = idString.Split(",");
                foreach (var id in splitAuthorIds)
                {
                    int outId;
                    if (Int32.TryParse(id.Trim(), out outId))
                    {
                        ids.Add(outId);
                    }
                }
            }
            else
            {
                int id;
                if (Int32.TryParse(idString.Trim(), out id))
                {
                    ids.Add(id);
                }
            }

            return ids;
        }

        // GET: LiteratureController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = apiClient.Fetch().GetLiteratureAsync(id).Result;
            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: LiteratureController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int id, [Bind("Id,Title,Description,PublishDate,Authors,Subjects,LevelName")] Literature literature)
        {
            if (!literature.LevelName.Equals("Beginner") && !literature.LevelName.Equals("Intermediate") && !literature.LevelName.Equals("Advanced"))
            {
                ModelState.AddModelError("LevelName", "LevelName has to be one of the following: Beginner, Intermediate or Advanced.");
            }

            JsonPatchDocument patch = new JsonPatchDocument();

            var dbModel = apiClient.Fetch().GetLiteratureAsync(id).Result;
            if (dbModel is null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!dbModel.Title.Equals(literature.Title))
                {
                    patch.Replace("/Title", literature.Title);
                }

                if (!dbModel.Description.Equals(literature.Description))
                {
                    patch.Replace("/Description", literature.Description);
                }

                if (!dbModel.PublishDate.Equals(literature.PublishDate))
                {
                    patch.Replace("/PublishDate", literature.PublishDate);
                }

                if (!dbModel.LevelName.Equals(literature.LevelName))
                {
                    patch.Replace("/LevelName", literature.LevelName);
                }

                var patched = apiClient.Patch().Send("Literatures", id, patch).Result;
                if (!patched)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(Index));
            }

            
            return View(literature);
        }

        // GET: LiteratureController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = apiClient.Fetch().GetLiteratureAsync(id).Result;
            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: LiteratureController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var deleted= apiClient.Delete().Literature(id).Result;
            if (!deleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
