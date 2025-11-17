using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dominio
{
    public class Validador
    {
        public static (bool esValido, string mensaje) ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return (false, "El DNI es obligatorio.");
            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
                return (false, "El DNI debe tener 7 u 8 dígitos.");
            return (true, "");
        }

        public static (bool esValido, string mensaje) ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return (false, "El nombre es obligatorio.");
            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(\s[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)*$"))
                return (false, "El nombre solo puede contener letras y espacios.");
            return (true, "");
        }

        public static (bool esValido, string mensaje) ValidarApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                return (false, "El apellido es obligatorio.");
            if (!Regex.IsMatch(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(\s[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)*$"))
                return (false, "El apellido solo puede contener letras y espacios.");
            return (true, "");
        }

        public static (bool esValido, string mensaje) ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "El email es obligatorio.");
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return (false, "Formato de email inválido.");
            return (true, "");
        }

        public static (bool esValido, string mensaje) ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return (false, "El teléfono es obligatorio.");
            if (!Regex.IsMatch(telefono, @"^\d{8,15}$"))
                return (false, "El teléfono debe tener entre 8 y 15 dígitos.");
            return (true, "");
        }

    }
}
