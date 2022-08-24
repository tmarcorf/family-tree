using FamilyTree.Domain.Entities;
using FamilyTree.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            try
            {
                var person = await _service.FindById(id);

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

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                var persons = _service.FindAll();

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

                if (person == null)
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
    }
}
