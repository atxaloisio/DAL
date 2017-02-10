﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAL
{
    public interface IRepositorio<T> where T:class
    {
        IQueryable<T> GetTodos();
        IQueryable<T> GetTodos(Expression<Func<T, string>> ordem, bool desc, int page, int pageSize);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate,Expression<Func<T, string>> ordem, bool desc, int page, int pageSize);        
        int getTotalRegistros(Expression<Func<T, bool>> predicate);
        int getTotalRegistros();
        T Find(params object[] key);
        T First(Expression<Func<T, bool>> predicate);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(Func<T, bool> predicate);
        void Commit();
        void Dispose();
    }
}