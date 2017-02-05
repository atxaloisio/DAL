using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public Usuario GetUsuarioPorLoginSenha(Usuario usuario)
        {
            return Context.Set<Usuario>().Where(p => p.email == usuario.email && p.password == usuario.password).FirstOrDefault();
        }
    }
}
