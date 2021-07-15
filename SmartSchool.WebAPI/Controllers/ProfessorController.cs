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
        private readonly SmartContext smartContext;

        public ProfessorController(SmartContext smartContext)
        {
            this.smartContext = smartContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(smartContext.Professor);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {

            var Professor = smartContext.Professor.Where(x => x.Id == Id).FirstOrDefault();
            if (Professor == null)
                return BadRequest("Nenhum Professor encontrado com o Id: " + Id);

            return Ok(Professor);
        }

        [HttpGet("{Name}")]
        public IActionResult GetByName(string Name)
        {

            var Professor = smartContext.Professor.Where(x => x.Nome.Contains(Name)).FirstOrDefault();
            if (Professor == null)
                return BadRequest("Nenhum Professor encontrado com o nome: " + Name);

            return Ok(Professor);
        }

        [HttpPost]
        public IActionResult Post(Professor Professor)
        {
            smartContext.Professor.Add(Professor);
            smartContext.SaveChanges();

            return Ok(smartContext.Professor);
        }

        [HttpPut]
        public IActionResult Put(Professor Professor)
        {
            var ProfessorDB = smartContext.Professor.AsNoTracking().FirstOrDefault(x => x.Id == Professor.Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            smartContext.Update(Professor);
            smartContext.SaveChanges();

            return Ok(smartContext.Professor);
        }

        [HttpPatch]
        public IActionResult Patch(Professor Professor)
        {
            var ProfessorDB = smartContext.Professor.AsNoTracking().FirstOrDefault(x => x.Id == Professor.Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            smartContext.Update(Professor);
            smartContext.SaveChanges();

            return Ok(smartContext.Professor);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ProfessorDB = smartContext.Professor.FirstOrDefault(x => x.Id == Id);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            smartContext.Remove(ProfessorDB);
            smartContext.SaveChanges();

            return Ok(smartContext.Professor);
        }
    }
}