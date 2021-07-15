using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext smartContext;

        public AlunoController(SmartContext smartContext)
        {
            this.smartContext = smartContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(smartContext.Alunos);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {

            var aluno = smartContext.Alunos.Where(x => x.Id == Id).FirstOrDefault();
            if (aluno == null)
                return BadRequest("Nenhum aluno encontrado com o Id: " + Id);

            return Ok(aluno);
        }

        [HttpGet("{Name}")]
        public IActionResult GetByName(string Name)
        {

            var aluno = smartContext.Alunos.Where(x => x.Nome.Contains(Name)).FirstOrDefault();
            if (aluno == null)
                return BadRequest("Nenhum aluno encontrado com o nome: " + Name);

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            smartContext.Alunos.Add(aluno);
            smartContext.SaveChanges();

            return Ok(smartContext.Alunos);
        }

        [HttpPut]
        public IActionResult Put(Aluno aluno)
        {
            var alunoDB = smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == aluno.Id);
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            smartContext.Update(aluno);
            smartContext.SaveChanges();

            return Ok(smartContext.Alunos);
        }

        [HttpPatch]
        public IActionResult Patch(Aluno aluno)
        {
            var alunoDB = smartContext.Alunos.AsNoTracking().FirstOrDefault(x => x.Id == aluno.Id);
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            smartContext.Update(aluno);
            smartContext.SaveChanges();

            return Ok(smartContext.Alunos);
        }

        [HttpDelete]
        public IActionResult Delete(Aluno aluno)
        {
            var alunoDB = smartContext.Alunos.FirstOrDefault(x => x.Id == aluno.Id);
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            smartContext.Remove(alunoDB);
            smartContext.SaveChanges();

            return Ok(smartContext.Alunos);
        }


    }
}