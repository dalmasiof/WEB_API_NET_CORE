using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id=1,
                Nome="Dalmasio",
                SobreNome="Fernandes",
                Telefone="123456"                
            },
            new Aluno(){
                Id=2,
                Nome="João",
                SobreNome="Calado",
                Telefone="3554534"                
            },
            new Aluno(){
                Id=3,
                Nome="William",
                SobreNome="Mattos",
                Telefone="32231"                
            },
            new Aluno(){
                Id=4,
                Nome="Lucas",
                SobreNome="Stange",
                Telefone="5353324"                
            },
        };

        public AlunoController()
        {

        }
        

        [HttpGet]
        public IActionResult Get(){
            return Ok(Alunos);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id){

            var aluno = Alunos.Where(x=>x.Id==Id).FirstOrDefault();
            if(aluno == null)   
                return BadRequest("Nenhum aluno encontrado com o Id: "+Id);
                
            return Ok(aluno);
        }

        [HttpGet("{Name}")]
        public IActionResult GetByName(string Name){

            var aluno = Alunos.Where(x=>x.Nome.Contains(Name)).FirstOrDefault();
            if(aluno == null)   
                return BadRequest("Nenhum aluno encontrado com o nome: "+Name);
                
            return Ok(aluno);
        }
    }
}