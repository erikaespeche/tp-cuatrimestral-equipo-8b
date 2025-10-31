using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
    public class PacienteNegocio
    {
        public List<Paciente> Listar()
        {
            List<Paciente> listaPacientes = new List<Paciente>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comandoPaciente = new SqlCommand();
            SqlDataReader lectorPaciente;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CLINICA_DB; integrated security=true";
                conexion.Open();

                comandoPaciente.CommandType = System.Data.CommandType.Text;
                comandoPaciente.CommandText = @"SELECT IdPaciente, DniPaciente, Nombres, Apellidos, 
                                                FechaNacimiento, Sexo, GrupoSanguineo, Email, Telefono, Celular, Direccion, 
                                                Ciudad, Provincia, CodigoPostal FROM PACIENTES";
                comandoPaciente.Connection = conexion;

                lectorPaciente = comandoPaciente.ExecuteReader();
                while (lectorPaciente.Read())
                {
                    Paciente aux = new Paciente();
                    aux.IdPaciente = (int)lectorPaciente["IdPaciente"];
                    aux.DniPaciente = (int)lectorPaciente["DniPaciente"];
                    aux.Nombres = lectorPaciente["Nombres"].ToString();
                    aux.Apellidos = lectorPaciente["Apellidos"].ToString();
                    aux.FechaNacimiento = (DateTime)lectorPaciente["FechaNacimiento"];
                    aux.Sexo = char.Parse(lectorPaciente["Sexo"].ToString());
                    aux.GrupoSanguineo = lectorPaciente["GrupoSanguineo"].ToString();
                    aux.Email = lectorPaciente["Email"].ToString();
                    aux.Telefono = lectorPaciente["Telefono"].ToString();
                    aux.Celular = lectorPaciente["Celular"].ToString();
                    aux.Direccion = lectorPaciente["Direccion"].ToString();
                    aux.Ciudad = lectorPaciente["Ciudad"].ToString();
                    aux.Provincia = lectorPaciente["Provincia"].ToString();
                    aux.CodigoPostal = lectorPaciente["CodigoPostal"].ToString();

                    listaPacientes.Add(aux);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos: " + ex.Message);
            }

            return listaPacientes;
        }
    }
}
