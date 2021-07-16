using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repository;
        public ProfessorController(IRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAllProfessor(true));
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {

            var Professor = repository.GetByIdProfessor(Id);
            if (Professor == null)
                return BadRequest("Nenhum Professor encontrado com o Id: " + Id);

            return Ok(Professor);
        }



        [HttpPost]
        public IActionResult Post(Professor Professor)
        {
            repository.Add(Professor);
            repository.SaveChanges();

            return Ok(repository.GetAllProfessor(false));
        }

        [HttpPut]
        public IActionResult Put(Professor Professor)
        {
            var ProfessorDB = repository.GetByIdProfessor(Professor.Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            repository.Update(Professor);
            repository.SaveChanges();

            return Ok(repository.GetAllProfessor(false));
        }

        [HttpPatch]
        public IActionResult Patch(Professor Professor)
        {
            var ProfessorDB = repository.GetByIdProfessor(Professor.Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            repository.Update(Professor);
            repository.SaveChanges();

            return Ok(repository.GetAllProfessor(false));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
             var ProfessorDB = repository.GetByIdProfessor(Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");


            repository.Delete(ProfessorDB);
            repository.SaveChanges();

            return Ok(repository.GetAllProfessor(false));
        }
    }
}