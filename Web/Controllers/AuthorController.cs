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
    public class AuthorController : Controller
    {
        public readonly IMapper mapper;
        public readonly APIClient apiClient;
        public AuthorController(IMapper mapper)
        {
            this.mapper = mapper;
            apiClient = new APIClient(44306);
        }

        // GET: LiteratureController
        public ActionResult Index()
        {
            var authors = apiClient.Fetch().GetAuthorsAsync(true).Result;

            return View(authors.AsEnumerable());
        }

        // GET: LiteratureController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var course = apiClient.Fetch().GetAuthorAsync(id, true).Result;

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
        public ActionResult Create(AuthorCreateModel model)
        {

            if (ModelState.IsValid)
            {
                var author = new Author()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Literatures = new List<LiteratureViewModel>()
                };

                foreach (var authorId in getSplitIds(model.LiteratureIds))
                {
                    author.Literatures.Add(new LiteratureViewModel(){Id = authorId});
                }

                var added = apiClient.Post().Author(author).Result;

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
            var model = apiClient.Fetch().GetAuthorAsync(id).Result;
            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: LiteratureController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int id, [Bind("Id,FirstName,LastName,DateOfBirth,Literatures")] Author author)
        {
            JsonPatchDocument patch = new JsonPatchDocument();

            var dbModel = apiClient.Fetch().GetAuthorAsync(id).Result;
            if (dbModel is null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!dbModel.FirstName.Equals(author.FirstName))
                {
                    patch.Replace("/FirstName", author.FirstName);
                }

                if (!dbModel.LastName.Equals(author.LastName))
                {
                    patch.Replace("/LastName", author.LastName);
                }

                if (!dbModel.DateOfBirth.Equals(author.DateOfBirth))
                {
                    patch.Replace("/DateOfBirth", author.DateOfBirth);
                }

                var patched = apiClient.Patch().Send("Author", id, patch).Result;
                if (!patched)
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(Index));
            }


            return View(author);
        }

        // GET: LiteratureController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = apiClient.Fetch().GetAuthorAsync(id).Result;
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
            var deleted = apiClient.Delete().Author(id).Result;
            if (!deleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
