using System.Linq;
using SmartSchool.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartSchool.WebAPI.Helper;

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
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Aluno[] GetByIdAluno(int IdAluno, bool incluiDisciplina = false)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos.Where(x => x.Id == IdAluno);

            if (incluiDisciplina)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }
        public Professor[] GetAllProfessor(bool incluiDisciplina)
        {
            IQueryable<Professor> query = this.smartContext.Professor;

            if (incluiDisciplina)
            {
                query = query.Include(x => x.Disciplinas)
                .ThenInclude(Add => Add.AlunosDisciplinas)
                .ThenInclude(Aluno => Aluno.Aluno);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Aluno[] GetByDisciplinaIdAlunos(int Id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos;

            if (includeProfessor)
            {
                query = query.Include(x => x.AlunosDisciplinas)
                .ThenInclude(y => y.Disciplina)
                .ThenInclude(z => z.Professor);
            }

            query = query.AsNoTracking()
            .OrderBy(x => x.Id)
            .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == Id));


            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Professor[] GetByDisciplinaIdProfessor(int Id, bool includeAluno)
        {
            IQueryable<Professor> query = this.smartContext.Professor;

            if (includeAluno)
            {
                query = query.Include(x => x.Disciplinas)
                .ThenInclude(y => y.AlunosDisciplinas)
                .ThenInclude(z => z.Aluno);
            }

            query = query.AsNoTracking()
            .OrderBy(x => x.Id)
            .Where(profe => profe.Disciplinas.Any(ad => ad.Id == Id));


            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.ToArray();
        }

        public Professor GetByIdProfessor(int Id, bool includeAluno)
        {

            IQueryable<Professor> query =
            this.smartContext.Professor.Where(x => x.Id == Id);


            if (includeAluno)
            {
                query = query.Include(x => x.Disciplinas)
                .ThenInclude(y => y.AlunosDisciplinas)
                .ThenInclude(z => z.Aluno);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);
            return query.FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (this.smartContext.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            this.smartContext.Update(entity);
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(
            PageParams pageParams,bool incluiDisciplina)
        {
            IQueryable<Aluno> query = this.smartContext.Alunos;

            if (incluiDisciplina)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                                .ThenInclude(ad => ad.Disciplina)
                                .ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()) ||
                                             aluno.SobreNome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()));

            if (pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);
            
            if (pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

            //return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query,pageParams.pageNumber,pageParams.PageSize);
        }
    }
}