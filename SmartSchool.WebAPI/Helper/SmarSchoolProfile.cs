using AutoMapper;
using SmartSchool.WebAPI.DTO;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helper
{
    public class SmarSchoolProfile : Profile
    {
        public SmarSchoolProfile()
        {
            CreateMap<Aluno,AlunoDTO>().ForMember(dest=>dest.Nome,
            opt=>opt.MapFrom(src=>$"{src.Nome} {src.SobreNome}")
            )
            .ForMember(dest=>dest.Idade,
            opt=>opt.MapFrom(src=>src.DataNasc.GetCurrentAge()));

            CreateMap<AlunoDTO,Aluno>();
            CreateMap<Aluno,AlunoRegistro>().ReverseMap();

            CreateMap<Professor,ProfessorDTO>().ReverseMap();

        }
    }
}