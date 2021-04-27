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
        public async Task<ActionResult<IEnumerable<LiteratureDto>>> GetLiterature()
        {
            var literatures = await uow.LiteratureRepository.getAllLiteratures();

            return Ok(mapper.Map<IEnumerable<LiteratureDto>>(literatures));
        }

        // GET: api/Literatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiteratureDto>> GetLiterature(int id)
        {
            var literature = await uow.LiteratureRepository.getLiterature(id);

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

            var literature = await uow.LiteratureRepository.getLiterature(id);
            if (id != literature.Id)
            {
                return BadRequest();
            }

            mapper.Map(literatureDto, literature);

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

            var literature = mapper.Map<Author>(literatureDto);
            await uow.AuthorRepository.AddAsync(literature);
            await uow.CompleteAsync();

            return CreatedAtAction("GetLiterature", new { id = literature.Id }, literature);
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
