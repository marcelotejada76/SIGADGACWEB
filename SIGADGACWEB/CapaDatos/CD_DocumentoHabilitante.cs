using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DocumentoHabilitante
    {
        public static CD_DocumentoHabilitante _instancia = null;
        private CD_DocumentoHabilitante()
        {

        }

        public static CD_DocumentoHabilitante Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DocumentoHabilitante();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo nuevo registro en la tabla de Dct. Habilitante
        /// </summary>
        /// <param name="documentoHabilitante"></param>
        /// <returns>True o False</returns>
        public bool DocumentoHabilitanteNuevo(tbDocumentoHabilitante documentoHabilitante)
        {
            bool respuesta = false;
            StringBuilder sb = new StringBuilder();
            string queryInsertar = "";
            iDB2Command cmd;
            try
            {
                
                sb.Append("INSERT INTO DOCARC (DOCANI, DOCNUM, DOCSEC, DOCTIP, DOCDIR, DOCPAT, DOCNOM, DOCTI1, DOCEST, DOCUSU, DOCFEC, DOCHOR, DOCDIS) ");
                sb.Append("VALUES (@AnioSolicitud, @NumeroSolicitud, @SecuencialDocHblt, @TipoDocumento, @DireccionIP, @PathArchivo1, @NombreArchivo1, @TipoArchivo, @EstadoDocumento, )");
                sb.Append("@UsuarioCreado, @FechaCreado, @HoraCreado, @DispositivoCreado");

                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(queryInsertar, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@AnioSolicitud"].Value = documentoHabilitante.AnioSolicitud;
                    cmd.Parameters["@NumeroSolicitud"].Value = documentoHabilitante.NumeroSolicitud;
                    cmd.Parameters["@SecuencialDocHblt"].Value = DocumentoHabilitanteSecuencial();
                    cmd.Parameters["@TipoDocumento"].Value = documentoHabilitante.TipoDocumento;
                    cmd.Parameters["@DireccionIP"].Value = documentoHabilitante.DireccionIP;
                    cmd.Parameters["@PathArchivo1"].Value = documentoHabilitante.PathArchivo1;
                    cmd.Parameters["@NombreArchivo1"].Value = documentoHabilitante.NombreArchivo1;
                    cmd.Parameters["@TipoArchivo"].Value = documentoHabilitante.TipoArchivo;
                    cmd.Parameters["@EstadoDocumento"].Value = documentoHabilitante.EstadoDocumento;
                    cmd.Parameters["@UsuarioCreado"].Value = documentoHabilitante.UsuarioCreado;
                    cmd.Parameters["@FechaCreado"].Value = documentoHabilitante.FechaCreado;
                    cmd.Parameters["@HoraCreado"].Value = documentoHabilitante.HoraCreado;
                    cmd.Parameters["@DispositivoCreado"].Value = documentoHabilitante.DispositivoCreado;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return respuesta;
        }

        /// <summary>
        /// Metodo obtiene el numero de secuencial de la tabla Dct. Habilitante
        /// </summary>
        /// <returns></returns>
        private decimal DocumentoHabilitanteSecuencial()
        {
            string query = "SELECT IFNULL(max(DOCSEC), 0) + 1 AS Secuencial FROM DOCARC";
            iDB2Command cmd;
            decimal secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = decimal.Parse(dr["Secuencial"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                secuencial = 0;
            }
            return secuencial;
        }
    }
}
