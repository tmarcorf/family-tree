using FamilyTree.Domain.Entities;
using FamilyTree.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyTree.API.Controllers
{
    [Route("api/person")]
    [FormatFilter]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            try
            {
                var personTree = await _service.FindById(id);

                if (personTree == null)
                {
                    return NotFound("Person not found.");
                }

                return Ok(personTree);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Impossible to recovery person: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> FindByNameId(string name)
        {
            try
            {
                var person = await _service.FindByName(name);

                if (person == null)
                {
                    return NotFound("Person not found.");
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Impossible to recovery person: {ex.Message}");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var persons = await _service.FindAll();

                if (persons == null)
                {
                    return NotFound("Persons not found.");
                }

                return Ok(persons);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Impossible to recovery persons: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            try
            {
                var personCreate = await _service.Create(person);

                if (personCreate == null)
                {
                    return BadRequest("Person not entered.");
                }

                return Ok(personCreate);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Failure while creating person: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Person person)
        {
            try
            {
                if (person == null)
                {
                    return NotFound("Person not updated.");
                }

                var result = await _service.Update(person);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Failure while updating person: {ex.Message}");
            }
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);

                if (!result)
                {
                    return BadRequest($"Id {id} not found");
                }

                return Ok($"Id {id} has been deleted.");
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Failure while deleting id {id}");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Failure while deleting person: {ex.Message}");
            }
        }
    }
}
