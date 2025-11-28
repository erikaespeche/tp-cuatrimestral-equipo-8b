using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class MedicoEspecialidadNegocio
    {
        public void Agregar(MedicoEspecialidad medicoEsp)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO MEDICO_ESPECIALIDAD 
                                       (IdMedico, IdEspecialidad)
                                       VALUES (@IdMedico, @IdEspecialidad)");

                datos.setearParametro("@IdMedico", medicoEsp.IdMedico);
                datos.setearParametro("@IdEspecialidad", medicoEsp.IdEspecialidad);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Especialidad> ListarPorMedico(int idMedico)
        {
            List<Especialidad> lista = new List<Especialidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT E.IdEspecialidad, E.Nombre, E.Descripcion
                    FROM MEDICO_ESPECIALIDAD ME
                    INNER JOIN ESPECIALIDAD E ON E.IdEspecialidad = ME.IdEspecialidad
                    WHERE ME.IdMedico = @IdMedico
                ");

                datos.setearParametro("@IdMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad esp = new Especialidad();
                    esp.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    esp.Nombre = datos.Lector["Nombre"].ToString();
                    esp.Descripcion = datos.Lector["Descripcion"].ToString();

                    lista.Add(esp);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }
}

