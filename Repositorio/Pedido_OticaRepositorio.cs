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
    public class Pedido_OticaRepositorio : Repositorio<Pedido_Otica>, IPedido_OticaRepositorio
    {
        public override void Atualizar(Pedido_Otica entity)
        {
            /* Logica incluida para realizar o insert, update e exclusão
             * correta do item do pedido optica
             */
            List<ItemPedido_Otica> lstRemItemPedido_Otica = new List<ItemPedido_Otica>();

            foreach (ItemPedido_Otica item in entity.itempedido_otica)
            {
                if (item.state == EstadoEntidade.Deleted)
                {
                    Context.Set<ItemPedido_Otica>().Where(p => p.Id == item.Id).ToList().ForEach(del => Context.Set<ItemPedido_Otica>().Remove(del));
                    lstRemItemPedido_Otica.Add(item);
                }
                else
                {
                    Context.Entry(item).State = (EntityState)item.state;
                }
                
            }

            foreach (Pedido_Armacao item in entity.pedido_armacao)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (Pedido_Lente item in entity.pedido_lente)
            {
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (ItemPedido_Otica item in lstRemItemPedido_Otica)
            {
                entity.itempedido_otica.Remove(item);
            }
            
            base.Atualizar(entity);
        }
    }
}
