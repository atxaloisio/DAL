﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario GetUsuarioPorLoginSenha(Usuario usuario);
    }
}
