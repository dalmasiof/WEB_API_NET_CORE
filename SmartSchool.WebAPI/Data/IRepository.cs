
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{


    public interface IRepository
    {
        
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();


        Aluno[] GetAllAlunos(bool incluiDisciplina);
        Aluno[] GetByDisciplinaIdAlunos(int Id, bool includeProfessor);
        Aluno[] GetByIdAluno(int IdAluno, bool incluiDisciplina = false);
        Professor[] GetAllProfessor(bool incluiDisciplina);
        Professor[] GetByDisciplinaIdProfessor(int Id,bool includeAluno);
        Professor GetByIdProfessor(int Id);


    }
}