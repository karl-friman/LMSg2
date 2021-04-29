using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Core.Entities;
using API.Core.Repositories;
using API.Core.ViewModel;
using API.Data.Data;
using API.Data.Util;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uow = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors(bool include = false)
        {
            var authors = await uow.AuthorRepository.getAllAuthors(include);

            var authorDtos = mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(authorDtos);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id, bool include = false)
        {
            var author = await uow.AuthorRepository.getAuthor(id, include);


            if (author == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AuthorDto>(author));
        }

        // PUT: api/Author/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDto authorDto)
        {
            var author = await uow.AuthorRepository.getAuthor(id, true);

            if (id != author.Id)
            {
                return BadRequest();
            }

            mapper.Map(authorDto, author);

            List<LiteratureViewModel> literatureDtos = authorDto.Literatures.ToList();

            if (uow.LevelRepository.getAllLevels().Result.Any(level => !literatureDtos.Any(l => level.Name.Equals(l.LevelName))))
            {
                return BadRequest("Literature Has to use a valid Level Name, Available: Beginner, Intermediate, Advanced");
            }


            author.Literatures = literatureDtos.Select(l => Literature(l, author)).ToList();

            if  (await uow.AuthorRepository.SaveAsync())
            {
                return Ok(mapper.Map<AuthorDto>(author));
            }

            return StatusCode(500);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<AuthorDto>> PatchAuthor(int id, JsonPatchDocument<AuthorDto> jsonPatchDocument)
        {
            var patchValue = jsonPatchDocument.Operations.Select(patch => patch.value.ToString()).FirstOrDefault();
            var pathString = jsonPatchDocument.Operations.Select(patch => patch.path.ToString()).FirstOrDefault();

            if (string.IsNullOrEmpty(patchValue)) return BadRequest("Value is null");
            if (string.IsNullOrEmpty(pathString)) return BadRequest("Path is null");
            if (pathString.ToLower().Contains("literatures")) return BadRequest("Can't modify literatures from author");


            var author = await uow.AuthorRepository.getAuthor(id, false);

            if(author is null)
            {
                return NotFound();
            }

            var model = mapper.Map<AuthorDto>(author);

            jsonPatchDocument.ApplyTo(model, ModelState);

            if(!TryValidateModel(model))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(model, author);

            if(await uow.AuthorRepository.SaveAsync())
            {
                return Ok(mapper.Map<AuthorDto>(author));
            }

            return StatusCode(500);
        }

        // POST: api/Author
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDto authorDto)
        {
            var author = mapper.Map<Author>(authorDto);
            await uow.AuthorRepository.AddAsync(author);
            await uow.CompleteAsync();

            return CreatedAtAction("GetAuthor", new { id = authorDto.Id }, authorDto);
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            
            if (!await uow.AuthorRepository.RemoveAsync(id)) return NotFound();

            return NoContent();
        }


        private Literature Literature(LiteratureViewModel l, Author author)
        {
            /**
                 * kollar på alla användare och om någon av användarna matchar, view models användare så lägger den till i listan.
                 */
            ICollection<Author> newAuthor = uow.AuthorRepository.getAllAuthors(false).Result
                .Where(a => MapperUtil<string>.IdExistInList(a.Id, l.Authors)).ToList();

            /**
                 * om egna author'n inte finns i listan så lägger vi till den
                 */
            if (newAuthor.All(a => a.Id != author.Id)) newAuthor.Add(author);


            /**
                 * kollar på alla subjects och om någon av subject matchar, view models subjects så lägger den till i listan.
                 */
            ICollection<Subject> subjects = uow.SubjectRepository.getAllSubjects(false).Result
                .Where(s => MapperUtil<string>.IdExistInList(s.Id, l.Subjects)).ToList();


            /**
                 * loopar view modelns subjects
                 * Om view modelns subject inte finns i listan av Subjects
                 * så lägger vi till nya subjects utifrån view modelns subjects (den lägger till i databasen)
                 */
            foreach (var viewSubjects in l.Subjects)
            {
                if (!subjects.Any(s => s.Name.Equals(viewSubjects)))
                {
                    subjects.Add(new Subject()
                    {
                        Name = viewSubjects
                    });
                }
            }

            // returnera en omvanlad literature från viewmodel
            return new Literature()
            {
                Id = l.Id,
                Title = l.Title,
                PublishDate = l.PublishDate,
                Description = l.Description,
                Level = uow.LevelRepository.getLevelByName(l.LevelName).Result ??
                        uow.LevelRepository.getLevelByName("Beginner").Result,
                Authors = newAuthor,
                Subjects = subjects
            };
        }

    }
}
