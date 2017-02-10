using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;


namespace BLL
{
    public class Produto_IbptBLL : IDisposable
    {
        IProduto_IbptRepositorio _Produto_IbptRepositorio;
        public Produto_IbptBLL()
        {
            try
            {
                _Produto_IbptRepositorio = new Produto_IbptRepositorio();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual List<Produto_Ibpt> getProduto_Ibpt(int Id = -1)
        {
            try
            {
                if (Id == -1)
                {
                    return _Produto_IbptRepositorio.GetTodos().ToList();
                }
                else
                {
                    return _Produto_IbptRepositorio.Get(p => p.Id == Id).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        public virtual List<Produto_Ibpt> getProduto_Ibpt(Expression<Func<Produto_Ibpt, string>> ordem, bool desc, int page, int pageSize, out int totalRecords)
        {
            try
            {
                totalRecords = _Produto_IbptRepositorio.getTotalRegistros();
                return _Produto_IbptRepositorio.GetTodos(ordem, desc, page, pageSize).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual List<Produto_Ibpt> getProduto_Ibpt(Expression<Func<Produto_Ibpt, bool>> predicate, Expression<Func<Produto_Ibpt, string>> ordem, bool desc, int page, int pageSize, out int totalRecords)
        {
            try
            {
                totalRecords = _Produto_IbptRepositorio.getTotalRegistros(predicate);
                return _Produto_IbptRepositorio.Get(predicate, ordem, desc, page, pageSize).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual void AdicionarProduto_Ibpt(Produto_Ibpt Produto_Ibpt)
        {
            try
            {
                Produto_Ibpt.inclusao = DateTime.Now;
                _Produto_IbptRepositorio.Adicionar(Produto_Ibpt);
                _Produto_IbptRepositorio.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual Produto_Ibpt Localizar(long? id)
        {
            try
            {
                return _Produto_IbptRepositorio.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public virtual void ExcluirProduto_Ibpt(Produto_Ibpt Produto_Ibpt)
        {
            try
            {
                _Produto_IbptRepositorio.Deletar(c => c == Produto_Ibpt);
                _Produto_IbptRepositorio.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual void AlterarProduto_Ibpt(Produto_Ibpt Produto_Ibpt)
        {
            try
            {
                Produto_Ibpt.alteracao = DateTime.Now;
                _Produto_IbptRepositorio.Atualizar(Produto_Ibpt);
                _Produto_IbptRepositorio.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            if (_Produto_IbptRepositorio != null)
            {
                _Produto_IbptRepositorio.Dispose();
            }

        }
    }
}
