using Microsoft.AspNetCore.Mvc;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetActionResult(){
            return Ok("Teste Professor");
        }
        public ProfessorController()
        {
            
        }
    }
}