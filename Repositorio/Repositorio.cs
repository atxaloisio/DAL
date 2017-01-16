using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DAL
{
    public class Repositorio<T> : IRepositorio<T>, IDisposable where T : class
    {
        //Para alterar para o sqlcompact mudar aqui.
        protected MySQLEntities Context;

        protected Repositorio()
        {
            Context = new MySQLEntities();
        }

        public IQueryable<T> GetTodos()
        {
            return Context.Set<T>();
        }

        public IQueryable<T> GetTodos(Expression<Func<T, string>> ordem, bool desc, int page, int pageSize)
        {
            //int skipRows = (page - 1) * pageSize;

            if (desc)
            {
                return Context.Set<T>().OrderByDescending(ordem).Skip(page).Take(pageSize);
            }
            else
            {
                return Context.Set<T>().OrderBy(ordem).Skip(page).Take(pageSize);
            }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> ordem, bool desc, int page, int pageSize)
        {
            //int skipRows = (page - 1) * pageSize;

            if (desc)
            {
                return Context.Set<T>().Where(predicate).OrderByDescending(ordem).Skip(page).Take(pageSize);
            }
            else
            {
                return Context.Set<T>().Where(predicate).OrderBy(ordem).Skip(page).Take(pageSize);
            }
        }

        public virtual int getTotalRegistros(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).Count();
        }

        public virtual int getTotalRegistros()
        {
            return Context.Set<T>().Count();
        }

        public T Find(params object[] key)
        {
            return Context.Set<T>().Find(key);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public void Adicionar(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Atualizar(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Deletar(Func<T, bool> predicate)
        {
            Context.Set<T>().Where(predicate).ToList().ForEach(del => Context.Set<T>().Remove(del));
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
            else
            {
                GC.SuppressFinalize(this);
            }
        }




    }
}
