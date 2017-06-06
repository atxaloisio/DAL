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

            foreach (Pedido_Otica_InfoAdic item in entity.pedido_otica_infoadic)
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

            
            List<Pedido_Otica_Parcelas> remParcelas = new List<Pedido_Otica_Parcelas>();

            foreach (Pedido_Otica_Parcelas item in entity.pedido_otica_parcelas)
            {
                if (item.state == EstadoEntidade.Deleted)
                {                   
                    remParcelas.Add(item);
                }
                else
                {
                    if (item.Id > 0)
                    {
                        Context.Entry(item).State = EntityState.Modified;
                    }
                    else
                    {
                        Context.Entry(item).State = EntityState.Added;                        
                        item.Id_pedido_otica = entity.Id;
                    }
                    
                }
            }

            foreach (Pedido_Otica_Parcelas item in remParcelas)
            {
                Context.Entry(item).State = EntityState.Deleted;
                entity.pedido_otica_parcelas.Remove(item);                
            }

            
            foreach (ItemPedido_Otica item in lstRemItemPedido_Otica)
            {
                
                entity.itempedido_otica.Remove(item);
            }
            
            base.Atualizar(entity);
        }
    }
}
