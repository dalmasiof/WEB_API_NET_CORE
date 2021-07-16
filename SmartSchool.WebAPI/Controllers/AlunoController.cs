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
        private readonly IRepository repository;

        public AlunoController( IRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var t = repository.GetAllAlunos(true);
            return Ok(t);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {

            var aluno = repository.GetByIdAluno(Id).FirstOrDefault();
            if (aluno == null)
                return BadRequest("Nenhum aluno encontrado com o Id: " + Id);

            return Ok(aluno);
        }

      
     

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            repository.Add(aluno);
            if(repository.SaveChanges()){
                 return Ok(repository.GetAllAlunos(false));

            }
            else{
                return BadRequest("Erro ao inserir valor");
            }
            

        }

        [HttpPut]
        public IActionResult Put(Aluno aluno)
        {
            var alunoDB = repository.GetAllAlunos(false).FirstOrDefault(x => x.Id == aluno.Id);
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            repository.Update(aluno);
            repository.SaveChanges();

            return Ok(repository.GetAllAlunos(false));
        }

        [HttpPatch]
        public IActionResult Patch(Aluno aluno)
        {
            var alunoDB = repository.GetAllAlunos(false).FirstOrDefault(x => x.Id == aluno.Id);
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            repository.Update(aluno);
            repository.SaveChanges();

            return Ok(repository.GetAllAlunos(false));
        }

        [HttpDelete]
        public IActionResult Delete(Aluno aluno)
        {
            var alunoDB =repository.GetByIdAluno(aluno.Id).FirstOrDefault();
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

             repository.Delete(aluno);
            if(repository.SaveChanges()){
            return Ok(repository.GetAllAlunos(false));

            }
            else{
                return BadRequest("Erro ao inserir valor");
            }
        }


    }
}