using System;
using System.Data.SqlClient;
using dominio;

public class HistoriaClinicaNegocio
{
    public void Guardar(HistoriaClinica hc)
    {
        using (SqlConnection conexion = new SqlConnection("tu_cadena"))
        {
            conexion.Open();

            SqlCommand cmd = new SqlCommand(@"
                INSERT INTO HistoriaClinica
                (IdPaciente, IdMedico, FechaConsulta, Observaciones, Diagnostico, Tratamientos, ProximosPasos, ArchivosAdjuntos)
                VALUES
                (@IdPaciente, @IdMedico, @FechaConsulta, @Observaciones, @Diagnostico, @Tratamientos, @ProximosPasos, @ArchivosAdjuntos)
            ", conexion);

            cmd.Parameters.AddWithValue("@IdPaciente", hc.IdPaciente);
            cmd.Parameters.AddWithValue("@IdMedico", hc.IdMedico);
            cmd.Parameters.AddWithValue("@FechaConsulta", hc.FechaConsulta);
            cmd.Parameters.AddWithValue("@Observaciones", hc.Observaciones);
            cmd.Parameters.AddWithValue("@Diagnostico", hc.Diagnostico);
            cmd.Parameters.AddWithValue("@Tratamientos", hc.Tratamientos);
            cmd.Parameters.AddWithValue("@ProximosPasos", hc.ProximosPasos);
            cmd.Parameters.AddWithValue("@ArchivosAdjuntos", (object)hc.ArchivosAdjuntos ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }
    }
}
