using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace DAL
{
    public class CaixaRepositorio : Repositorio<Caixa>, ICaixaRepositorio
    {
        public virtual IQueryable<Caixa> GetCaixaJoin(StatusPedido status)
        {
            int st = (int)status;
            var qry = Context.Caixas.SqlQuery("select p1.* from caixa p1 left join pedido_otica p2 on (p2.Id_caixa = p1.Id) where (p2.status is null or p2.status = "+st.ToString()+")");
            //var qry = Context.Caixas.GroupJoin(Context.Pedido_Otica1, T1 => T1.Id, T2 => T2.Id_caixa, (C, T) => new { C, T }).SelectMany(n => n.T.DefaultIfEmpty(), (n, t2) => new { Tabela1 = n, Tabela2 = t2 }).Where(c => c.Tabela2.status == null || c.Tabela2.status >= st);
            return  qry.ToList<Caixa>().AsQueryable<Caixa>();
        }
    }
}
