using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class HistoriaClinicaNegocio
{
    public List<HistoriaClinica> ListarPorPaciente(int idPaciente)
    {
        List<HistoriaClinica> listaHC = new List<HistoriaClinica>();
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta(@"
                SELECT hc.IdHistoriaClinica, hc.IdPaciente, hc.IdMedico, hc.FechaConsulta, hc.Observaciones, 
                       hc.Diagnostico, hc.Tratamientos, hc.ProximosPasos, hc.ArchivosAdjuntos,
                       hc.GrupoFactorSanguineo, hc.Peso, hc.Altura, hc.Alergias, hc.EnfermedadesCronicas, hc.Patologias,
                       m.Nombre AS NombreMedico
                FROM HistoriaClinica hc
                INNER JOIN Medico m ON hc.IdMedico = m.IdMedico
                WHERE hc.IdPaciente = @idPaciente
                ORDER BY hc.FechaConsulta DESC
            ");

            datos.setearParametro("@idPaciente", idPaciente);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                HistoriaClinica hc = new HistoriaClinica();
                hc.IdHistoriaClinica = (int)datos.Lector["IdHistoriaClinica"];
                hc.IdPaciente = (int)datos.Lector["IdPaciente"];
                hc.IdMedico = (int)datos.Lector["IdMedico"];
                hc.FechaConsulta = (DateTime)datos.Lector["FechaConsulta"];
                hc.Observaciones = datos.Lector["Observaciones"].ToString();
                hc.Diagnostico = datos.Lector["Diagnostico"].ToString();
                hc.Tratamientos = datos.Lector["Tratamientos"].ToString();
                hc.ProximosPasos = datos.Lector["ProximosPasos"].ToString();
                hc.ArchivosAdjuntos = datos.Lector["ArchivosAdjuntos"] != DBNull.Value
                    ? datos.Lector["ArchivosAdjuntos"].ToString()
                    : null;

                // NUEVOS CAMPOS
                hc.GrupoFactorSanguineo = datos.Lector["GrupoFactorSanguineo"].ToString();
                hc.Peso = datos.Lector["Peso"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Peso"]) : 0;
                hc.Altura = datos.Lector["Altura"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Altura"]) : 0;
                hc.Alergias = datos.Lector["Alergias"].ToString();
                hc.EnfermedadesCronicas = datos.Lector["EnfermedadesCronicas"].ToString();
                hc.Patologias = datos.Lector["Patologias"].ToString();

                // Datos del médico
                hc.NombreMedico = datos.Lector["NombreMedico"].ToString();

                listaHC.Add(hc);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al listar la historia clínica: " + ex.Message);
        }
        finally
        {
            datos.cerrarConexion();
        }

        return listaHC;
    }

    public void Guardar(HistoriaClinica hc)
    {
        AccesoDatos datos = new AccesoDatos();

        try
        {
            datos.setearConsulta(@"
            INSERT INTO HistoriaClinica
            (IdPaciente, IdMedico, FechaConsulta, Observaciones, Diagnostico, Tratamientos, ProximosPasos, ArchivosAdjuntos,
             GrupoFactorSanguineo, Peso, Altura, Alergias, EnfermedadesCronicas, Patologias)
            VALUES
            (@IdPaciente, @IdMedico, @FechaConsulta, @Observaciones, @Diagnostico, @Tratamientos, @ProximosPasos, @ArchivosAdjuntos,
             @GrupoFactorSanguineo, @Peso, @Altura, @Alergias, @EnfermedadesCronicas, @Patologias)
        ");

            datos.setearParametro("@IdPaciente", hc.IdPaciente);
            datos.setearParametro("@IdMedico", hc.IdMedico);
            datos.setearParametro("@FechaConsulta", hc.FechaConsulta);

            datos.setearParametro("@Observaciones", string.IsNullOrEmpty(hc.Observaciones) ? DBNull.Value : (object)hc.Observaciones);
            datos.setearParametro("@Diagnostico", string.IsNullOrEmpty(hc.Diagnostico) ? DBNull.Value : (object)hc.Diagnostico);
            datos.setearParametro("@Tratamientos", string.IsNullOrEmpty(hc.Tratamientos) ? DBNull.Value : (object)hc.Tratamientos);
            datos.setearParametro("@ProximosPasos", string.IsNullOrEmpty(hc.ProximosPasos) ? DBNull.Value : (object)hc.ProximosPasos);
            datos.setearParametro("@ArchivosAdjuntos", string.IsNullOrEmpty(hc.ArchivosAdjuntos) ? DBNull.Value : (object)hc.ArchivosAdjuntos);

            datos.setearParametro("@GrupoFactorSanguineo", string.IsNullOrEmpty(hc.GrupoFactorSanguineo) ? DBNull.Value : (object)hc.GrupoFactorSanguineo);
            datos.setearParametro("@Peso", hc.Peso);
            datos.setearParametro("@Altura", hc.Altura);
            datos.setearParametro("@Alergias", string.IsNullOrEmpty(hc.Alergias) ? DBNull.Value : (object)hc.Alergias);
            datos.setearParametro("@EnfermedadesCronicas", string.IsNullOrEmpty(hc.EnfermedadesCronicas) ? DBNull.Value : (object)hc.EnfermedadesCronicas);
            datos.setearParametro("@Patologias", string.IsNullOrEmpty(hc.Patologias) ? DBNull.Value : (object)hc.Patologias);

            datos.ejecutarAccion();
        }
        catch (Exception ex)
        {
            throw new Exception("Error al guardar la historia clínica: " + ex.Message);
        }
        finally
        {
            datos.cerrarConexion();
        }
    }

}
