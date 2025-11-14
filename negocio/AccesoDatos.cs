using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CLINICA_DB; integrated security=true";
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public int obtenerId()
        {
            comando.Connection = conexion;
            conexion.Open();
            int id = Convert.ToInt32(comando.ExecuteScalar());
            conexion.Close();
            return id;
        }

        public void cerrarConexion()
        {
            if (lector != null && !lector.IsClosed)
                lector.Close();
            if (conexion.State == ConnectionState.Open)
                conexion.Close();

            comando.Parameters.Clear();
        }
    }
}
