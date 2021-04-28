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
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;


        public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            uow = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubject(bool include = false)
        {
            var subjects = await uow.SubjectRepository.getAllSubjects(include);

            var subjectDtos = mapper.Map<IEnumerable<SubjectDto>>(subjects);

            return Ok(subjectDtos);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(int id, bool include = false)
        {
            var subject = await uow.SubjectRepository.getSubjects(id, include);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SubjectDto>(subject));
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, SubjectDto subjectDto)
        {
            var subject = await uow.SubjectRepository.getSubjects(id, false);
            if (id != subject.Id)
            {
                return BadRequest();
            }

            mapper.Map(subjectDto, subject);

            if (await uow.SubjectRepository.SaveAsync())
            {
                return Ok(mapper.Map<SubjectDto>(subject));
            }

            return StatusCode(500);

        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> PostSubject(SubjectDto subjectDto)
        {
            var subject = mapper.Map<Subject>(subjectDto);
            await uow.SubjectRepository.AddAsync(subject);
            await uow.CompleteAsync();

            return CreatedAtAction("GetSubject", new { id = subjectDto.Id }, subjectDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SubjectDto>> PatchSubject(int id, JsonPatchDocument<SubjectDto> jsonPatchDocument)
        {
            var subject = await uow.SubjectRepository.getSubjects(id, false);

            if (subject is null)
            {
                return NotFound();
            }

            var model = mapper.Map<SubjectDto>(subject);

            jsonPatchDocument.ApplyTo(model, ModelState);

            if (!TryValidateModel(model))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(model, subject);

            if (await uow.SubjectRepository.SaveAsync())
            {
                return Ok(mapper.Map<SubjectDto>(subject));
            }
            else
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (!await uow.SubjectRepository.RemoveAsync(id)) return NotFound();

            return NoContent();
        }

    }
}
