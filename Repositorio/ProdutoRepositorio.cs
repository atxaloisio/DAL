using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;

namespace DAL
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {
        public override void Atualizar(Produto entity)
        {
            foreach (Movimento item in entity.movimentacoes)
            {
                if (item.Id > 0)
                {
                    Context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry(item).State = EntityState.Added;
                }
            }

            base.Atualizar(entity);
        }
    }
}
