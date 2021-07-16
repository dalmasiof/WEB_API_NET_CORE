using System.Linq;
using SmartSchool.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext smartContext;

        public Repository(SmartContext smartContext)
        {
            this.smartContext = smartContext;

        }
        public void Add<T>(T entity) where T : class
        {
            this.smartContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.smartContext.Remove(entity);
        }

        public Aluno[] GetAllAlunos(bool incluiDisciplina)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos;

            if (incluiDisciplina)
            {
                query.Include(x => x.AlunosDisciplinas)
                .ThenInclude(Add => Add.Disciplina)
                .ThenInclude(Professor => Professor.Professor);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessor(bool incluiDisciplina)
        {
            throw new System.NotImplementedException();
        }

        public Aluno[] GetByDisciplinaIdAlunos(int Id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos;

            if(includeProfessor){
                query = query.Include(x=>x.AlunosDisciplinas)
                .ThenInclude(y=>y.Disciplina)
                .ThenInclude(z=>z.Professor);
            }

            query = query.AsNoTracking()
            .OrderBy(x=>x.Id)
            .Where(aluno=>aluno.AlunosDisciplinas.Any(ad=>ad.DisciplinaId == Id));


            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Professor[] GetByDisciplinaIdProfessor(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Aluno GetByIdAlunos(int Id)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos;

            
            query = query.AsNoTracking()
            .Where(x=>x.Id == Id);

            return query.FirstOrDefault();
        }

        public Professor GetByIdProfessor(int Id)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (this.smartContext.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            this.smartContext.Update(entity);
        }
    }
}