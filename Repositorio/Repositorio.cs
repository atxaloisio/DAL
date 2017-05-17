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
            //Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);            
        }

        public IQueryable<T> GetTodos()
        {            
            return Context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetTodos(Expression<Func<T, string>> ordem, bool desc, int page, int pageSize)
        {
            //int skipRows = (page - 1) * pageSize;

            if (desc)
            {
                return Context.Set<T>().AsNoTracking().OrderByDescending(ordem).Skip(page).Take(pageSize);
            }
            else
            {
                return Context.Set<T>().AsNoTracking().OrderBy(ordem).Skip(page).Take(pageSize);
            }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetNT(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> ordem, bool desc, int page, int pageSize)
        {
            //int skipRows = (page - 1) * pageSize;

            if (desc)
            {
                return Context.Set<T>().Where(predicate).AsNoTracking().OrderByDescending(ordem).Skip(page).Take(pageSize);
            }
            else
            {
                return Context.Set<T>().Where(predicate).AsNoTracking().OrderBy(ordem).Skip(page).Take(pageSize);
            }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool desc, int page, int pageSize,params Expression<Func<T, string>>[] ordem)
        {
            IQueryable<T> Qry;

            if (desc)
            {
                var Query = Context.Set<T>().Where(predicate).AsNoTracking().OrderByDescending(ordem.First());
                
                if (ordem.Length > 1)
                {
                    for (int i = 1; i < ordem.Length; i++)
                    {
                        Query.ThenBy(ordem[i]);
                    }                    
                }
                Query.Skip(page).Take(pageSize);

                Qry = Query;
            }
            else
            {
                var Query = Context.Set<T>().Where(predicate).AsNoTracking().OrderBy(ordem.First());
                if (ordem.Length > 1)
                {
                    for (int i = 1; i < ordem.Length; i++)
                    {
                        Query.ThenBy(ordem[i]);
                    }
                }
                Query.Skip(page).Take(pageSize);

                Qry = Query;
            }

            return Qry;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, bool desc,params Expression<Func<T, string>>[] ordem)
        {
            //int skipRows = (page - 1) * pageSize;
            IQueryable<T> Qry; 

            if (desc)
            {
                var Query = Context.Set<T>().Where(predicate).AsNoTracking().OrderByDescending(ordem.First());
                if (ordem.Length > 1)
                {
                    for (int i = 1; i < ordem.Length; i++)
                    {
                        Query.ThenBy(ordem[i]);
                    }
                }
                Qry = Query;

            }
            else
            {
                var Query = Context.Set<T>().Where(predicate).AsNoTracking().OrderBy(ordem.First());
                if (ordem.Length > 1)
                {
                    for (int i = 1; i < ordem.Length; i++)
                    {
                        Query.ThenBy(ordem[i]);
                    }
                }
                Qry = Query;
            }

            return Qry;
        }

        public virtual int getTotalRegistros(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsNoTracking().Count();
        }

        public virtual int getTotalRegistros()
        {            
            return Context.Set<T>().AsNoTracking().Count();
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
            //Context.Database.Log
            //ContextRemote.Set<T>().Add(entity);
        }

        public virtual void Atualizar(T entity)
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
