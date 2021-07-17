using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }


        [HttpGet]
        public IActionResult Get()
        {
            var Profs = repository.GetAllProfessor(false);

            return Ok(mapper.Map<IEnumerable<Professor>>(Profs));
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {

            var Professor = repository.GetByIdProfessor(Id,false);
            if (Professor == null)
                return BadRequest("Nenhum Professor encontrado com o Id: " + Id);

            return Ok(mapper.Map<ProfessorDTO>(Professor));
        }



        [HttpPost]
        public IActionResult Post(ProfessorDTO ProfessorDTO)
        {
            var ProfessorBD = mapper.Map<Professor>(ProfessorDTO);
            repository.Add(ProfessorBD);
            if (repository.SaveChanges())
            {
                return Created($"/api/professor/{ProfessorBD.Id}", mapper.Map<ProfessorDTO>(ProfessorBD));

            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }
        }

        [HttpPut]
        public IActionResult Put(ProfessorDTO Professor)
        {
            var ProfessorDB = repository.GetByIdProfessor(Professor.Id,false);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            ProfessorDB = mapper.Map<Professor>(Professor);
            repository.Update(ProfessorDB);
            if (repository.SaveChanges())
            {
                return Created($"/api/professor/{ProfessorDB.Id}", mapper.Map<ProfessorDTO>(ProfessorDB));

            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }
        }

        [HttpPatch]
        public IActionResult Patch(ProfessorDTO Professor)
        {
            var ProfessorDB = repository.GetByIdProfessor(Professor.Id,false);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");

            ProfessorDB = mapper.Map<Professor>(Professor);
            repository.Update(ProfessorDB);
            if (repository.SaveChanges())
            {
                return Created($"/api/professor/{ProfessorDB.Id}", mapper.Map<ProfessorDTO>(ProfessorDB));

            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ProfessorDB = repository.GetByIdProfessor(Id,false);
            if (ProfessorDB == null)
                return BadRequest("Professor não encontrado");


            repository.Delete(ProfessorDB);
           if (repository.SaveChanges())
            {
                var ProfessorDto = mapper.Map<Professor[],ProfessorDTO[]>(repository.GetAllProfessor(false));
                return Ok(ProfessorDto);
            }
            else
            {
                return BadRequest("Erro ao inserir valor");
            }
        }
    }
}