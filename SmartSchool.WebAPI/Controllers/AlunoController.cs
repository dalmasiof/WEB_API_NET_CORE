using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    /// <sumary>
    ///sas
    /// </sumary>
    /// <returns></returns>
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        ///<sumary>
        ///ddddd
        ///</sumary>
        ///<returns></returns>
        public AlunoController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        ///<sumary>
        ///ddMetodo Retorna todos alunos
        ///</sumary>
        ///<returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var Alunos = repository.GetAllAlunos(false);
            return Ok(mapper.Map<IEnumerable<AlunoDTO>>(Alunos));
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
        public IActionResult Post(AlunoDTO aluno)
        {
            var alunoMOdel = mapper.Map<Aluno>(aluno);

            repository.Add(alunoMOdel);
            if (repository.SaveChanges())
            {
                return Created($"/api/aluno/{aluno.Id}", mapper.Map<AlunoDTO>(alunoMOdel));
            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }


        }

        [HttpPut]
        public IActionResult Put(AlunoDTO aluno)
        {
            Aluno alunoDB = repository.GetByIdAluno(aluno.Id).FirstOrDefault();

            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            alunoDB = mapper.Map(aluno, alunoDB);
            repository.Update(alunoDB);
            repository.SaveChanges();

            return Created($"/api/aluno/{aluno.Id}", mapper.Map<AlunoDTO>(aluno));
        }

        [HttpPatch]
        public IActionResult Patch(Aluno aluno)
        {
            var alunoDB = repository.GetByIdAluno(aluno.Id);

            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            mapper.Map(aluno, alunoDB);
            repository.Update(aluno);
            repository.SaveChanges();

            return Created($"/api/aluno/{aluno.Id}", mapper.Map<AlunoDTO>(aluno));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var alunoDB = repository.GetByIdAluno(Id).FirstOrDefault();
            if (alunoDB == null)
                return BadRequest("Aluno não encontrado");

            repository.Delete(alunoDB);
            if (repository.SaveChanges())
            {
                var alunosDTO = mapper.Map<Aluno[], AlunoDTO[]>(repository.GetAllAlunos(false));
                return Ok(alunosDTO);

            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }
        }


    }
}