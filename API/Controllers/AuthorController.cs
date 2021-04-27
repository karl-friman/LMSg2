﻿using System;
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

            var authorsDto = authors.Select(a => new AuthorDto()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                DateOfBirth = a.DateOfBirth,
                Literatures = include ? a.Literatures.Where(l => !string.IsNullOrEmpty(l.Title)).Select(l => l.Title).ToList() : null
            });

            //var authorsDto = mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(authorsDto);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await uow.AuthorRepository.getAuthor(id);

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
            var author = await uow.AuthorRepository.getAuthor(id);
            if (id != author.Id)
            {
                return BadRequest();
            }

            mapper.Map(authorDto, author);

            if  (await uow.AuthorRepository.SaveAsync())
            {
                return Ok(mapper.Map<Author>(author));
            }

            return StatusCode(500);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<AuthorDto>> PatchAuthor(int id, JsonPatchDocument<AuthorDto> jsonPatchDocument)
        {
            var author = await uow.AuthorRepository.getAuthor(id);

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
            else
            {
                return StatusCode(500);
            }
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
    }
}
