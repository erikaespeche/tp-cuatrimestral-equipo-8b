using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace dominio
{
    public class Validador
    {

        public class ResultadoValidacion
        {
            public bool EsValido { get; set; }
            public string Mensaje { get; set; }
            public string CssClass => EsValido ? "is-valid" : "is-invalid";

            public ResultadoValidacion(bool ok, string msg)
            {
                EsValido = ok;
                Mensaje = msg;
            }
        }





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


        public static (bool esValido, string mensaje) ValidarContrasena(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                return (false, "La contraseña es obligatoria.");

            // Mínimo 6 caracteres
            if (contrasena.Length < 6)
                return (false, "La contraseña debe tener al menos 6 caracteres.");

            // Debe contener al menos una mayúscula
            if (!Regex.IsMatch(contrasena, @"[A-Z]"))
                return (false, "La contraseña debe incluir al menos una letra mayúscula.");

            // Debe contener al menos un número
            if (!Regex.IsMatch(contrasena, @"\d"))
                return (false, "La contraseña debe incluir al menos un número.");

            // Debe contener al menos un carácter especial
            if (!Regex.IsMatch(contrasena, @"[!@#$%^&*()_+\-=\[\]{};':"".,<>\/?\\|]"))
                return (false, "La contraseña debe incluir al menos un carácter especial.");

            return (true, "");
        }

        public static (bool esValido, string mensaje) ValidarConfirmacionContrasena(string contrasena, string confirm)
        {
            if (string.IsNullOrWhiteSpace(confirm))
                return (false, "Debe confirmar la contraseña.");

            if (contrasena != confirm)
                return (false, "Las contraseñas no coinciden.");

            return (true, "");
        }



        // Regex para validar
        Dictionary<string, Regex> regex = new Dictionary<string, Regex>()
        {
          { "nombre", new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$") },
          { "apellido", new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$") },
          { "dni", new Regex(@"^[0-9]{7,8}$") },
          { "mail", new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$") },
          { "cel", new Regex(@"^[0-9]{10,13}$") },
          { "tel", new Regex(@"^[0-9]{7,10}$") },
          { "dir", new Regex(@"^(?=.*[A-Za-zÁÉÍÓÚáéíóúÑñ])(?=.*\d)[A-Za-z0-9ÁÉÍÓÚáéíóúÑñ .,-]+$") },
          { "ciudad", new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$") },
          { "prov", new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$") },
          { "cp", new Regex(@"^(\d{4}|[A-Za-z]\d{4}[A-Za-z]{3})$") }
        };


        Dictionary<string, string> mensajes = new Dictionary<string, string>()
{
         { "nombre", "Ingrese un nombre válido (solo letras)." },
         { "apellido", "Ingrese un apellido válido (solo letras)." },
         { "dni", "Ingrese un DNI válido (7 a 8 dígitos numéricos)." },
         { "mail", "Formato de mail inválido." },
         { "cel", "Ingrese un celular válido (10 a 13 dígitos)." },
         { "tel", "Ingrese un teléfono válido (7 a 10 dígitos)." },
         { "dir", "Ingrese una dirección válida (calle + numeración)." },
         { "ciudad", "Ingrese una ciudad válida (solo letras)." },
         { "prov", "Ingrese una provincia válida (solo letras)." },
         { "cp", "Formato inválido. Ej: 1000 o C1000ABC." }
        };



        public List<string> ValidarUsuario(
          string nombre,
          string apellido,
          string dni,
          string mail,
          string celular,
          string telefono,
          string direccion,
          string ciudad,
          string provincia,
          string cp)
        {
            List<string> errores = new List<string>();

            if (!regex["nombre"].IsMatch(nombre))
                errores.Add(mensajes["nombre"]);

            if (!regex["apellido"].IsMatch(apellido))
                errores.Add(mensajes["apellido"]);

            if (!regex["dni"].IsMatch(dni))
                errores.Add(mensajes["dni"]);

            if (!regex["mail"].IsMatch(mail))
                errores.Add(mensajes["mail"]);

            if (!string.IsNullOrEmpty(celular) && !regex["cel"].IsMatch(celular))
                errores.Add(mensajes["cel"]);

            if (!string.IsNullOrEmpty(telefono) && !regex["tel"].IsMatch(telefono))
                errores.Add(mensajes["tel"]);

            if (!regex["dir"].IsMatch(direccion))
                errores.Add(mensajes["dir"]);

            if (!regex["ciudad"].IsMatch(ciudad))
                errores.Add(mensajes["ciudad"]);

            if (!regex["prov"].IsMatch(provincia))
                errores.Add(mensajes["prov"]);

            if (!regex["cp"].IsMatch(cp))
                errores.Add(mensajes["cp"]);

            return errores;
        }









    }
}
