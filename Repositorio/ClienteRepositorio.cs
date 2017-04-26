using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;

namespace DAL
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public override void Atualizar(Cliente entity)
        {
            foreach (Cliente_Tag item in entity.cliente_tag)
            {
                if (item.Id >0)
                {
                    Context.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry(item).State = EntityState.Added;
                }                
            }

            foreach (Cliente_Transportadora item in entity.cliente_transportadora)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (Cliente_Vendedor item in entity.cliente_vendedor)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (Cliente_Parcela item in entity.cliente_parcela)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            base.Atualizar(entity);
        }
    }
}
