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
using API.Data.Data;
using API.Data.Util;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiteraturesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;


        public LiteraturesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uow = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Literatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiteratureDto>>> GetLiterature(bool include = false)
        {
            var literatures = await uow.LiteratureRepository.getAllLiteratures(include);

            var literatureDtos = mapper.Map<IEnumerable<LiteratureDto>>(literatures);

            return Ok(literatureDtos);
        }

        // GET: api/Literatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiteratureDto>> GetLiterature(int id, bool include = false)
        {
            var literature = await uow.LiteratureRepository.getLiterature(id, include);

            if (literature == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<LiteratureDto>(literature));
        }

        // PUT: api/Literatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiterature(int id, LiteratureDto literatureDto)
        {
            if (!uow.LevelRepository.getAllLevels().Result.Any(level => level.Name.Equals(literatureDto.LevelName)))
            {
                return BadRequest("Can't use specified Level name, Available: Beginner, Intermediate, Advanced");
            }

            var literature = await uow.LiteratureRepository.getLiterature(id, false);
            if (id != literature.Id)
            {
                return BadRequest();
            }

            mapper.Map(literatureDto, literature);


            literature.Authors = uow.AuthorRepository
                .getAllAuthors(false)
                .Result
                .Where(a => literatureDto.Authors.Select(l => l.Id).Any(l => l == a.Id))
                .ToList();

            foreach (var literatureDtoAuthor in literatureDto.Authors)
            {
                if (literature.Authors.All(s => s.Id != literatureDtoAuthor.Id))
                {
                    literature.Authors.Add(new Author()
                    {
                        Id = literatureDtoAuthor.Id,
                        FirstName = literatureDtoAuthor.FirstName,
                        LastName = literatureDtoAuthor.LastName,
                        DateOfBirth = literatureDtoAuthor.DateOfBirth,
                    });
                }
            }


            literature.Subjects = uow.SubjectRepository
                .getAllSubjects(false)
                .Result
                .Where(s => literatureDto.Subjects.Select(l => l.Id).Any(l => l == s.Id))
                .ToList();


            foreach (var viewSubjects in literatureDto.Subjects)
            {
                if (!literature.Subjects.Any(s => s.Name.Equals(viewSubjects.Name)))
                {
                    literature.Subjects.Add(new Subject()
                    {
                        Name = viewSubjects.Name
                    });
                }
            }

            
            
            literature.Level = uow.LevelRepository.getLevelByName(literatureDto.LevelName).Result ?? uow.LevelRepository.getLevelByName("Beginner").Result;
            





            if (await uow.LiteratureRepository.SaveAsync())
            {
                return Ok(mapper.Map<LiteratureDto>(literature));
            }

            return StatusCode(500);
        }

        // POST: api/Literatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Literature>> PostLiterature(LiteratureDto literatureDto)
        {
            if (!uow.LevelRepository.getAllLevels().Result.Any(level => level.Name.Equals(literatureDto.LevelName)))
            {
                return BadRequest("Can't use specified Level name, Available: Beginner, Intermediate, Advanced");
            }

            var literature = mapper.Map<Literature>(literatureDto);
            await uow.AuthorRepository.AddAsync(literature);

            if (await uow.AuthorRepository.SaveAsync())
            {
                var model = mapper.Map<LiteratureDto>(literature);
                return CreatedAtAction("GetLiterature", new { id = model.Id }, model);
            }

            return StatusCode(500);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<LiteratureDto>> PatchLiterature(int id, JsonPatchDocument<LiteratureDto> jsonPatchDocument)
        {
            var patchValue = jsonPatchDocument.Operations.Select(patch => patch.value.ToString()).FirstOrDefault();
            if (string.IsNullOrEmpty(patchValue))
            {
                return BadRequest("Value is null");
            }

            var literature = await uow.LiteratureRepository.getLiterature(id, false);

            if (literature is null)
            {
                return NotFound();
            }

            var model = mapper.Map<LiteratureDto>(literature);

            jsonPatchDocument.ApplyTo(model, ModelState);

            if (!TryValidateModel(model))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(model, literature);

            if (await uow.LiteratureRepository.SaveAsync())
            {
                return Ok(mapper.Map<LiteratureDto>(literature));
            }

            return StatusCode(500);
        }

        // DELETE: api/Literatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLiterature(int id)
        {
            if (!await uow.LiteratureRepository.RemoveAsync(id)) return NotFound();

            return NoContent();
        }

    }
}
