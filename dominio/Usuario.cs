﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } 
        public string Email { get; set; } // para recuperar contraseña  


    }
}
