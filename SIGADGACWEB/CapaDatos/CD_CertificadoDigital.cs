using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_CertificadoDigital
    {
        public static CD_CertificadoDigital _instancia = null;
        private CD_CertificadoDigital()
        {

        }

        public static CD_CertificadoDigital Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_CertificadoDigital();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Metodo Certificado Digital
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns>tbCertificadoDigital</returns>
        public tbCertificadoDigital CertificadoDigitalPorUsuario(string codUsuario)
        {
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO, ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE  FROM CERAR4 WHERE CERASI = 'AC' AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.oCertificado = CertificadoDigitalPorUsuarioListar(codUsuario);
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return oCertificado;
        }

        /// <summary>
        /// Metodo Certificado Digital
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns>tbCertificadoDigital</returns>
        public tbCertificadoDigital CertificadoDigitalPorId(int idCodigo)
        {
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO, ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE  FROM CERAR4 WHERE CERASI = 'AC' AND CERC10 = " + idCodigo);
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.oCertificado = CertificadoDigitalPorUsuarioListar(oCertificado.CodigoUsuario);
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return oCertificado;
        }

        public tbCertificadoDigital CertificadoDigitalPorUsuarioUnRegistro(string codUsuario)
        {
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO, ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE  FROM CERAR4 WHERE CERC10 = '" + codUsuario.Trim().ToUpper() + "' FETCH FIRST 1 ROWS ONLY");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.oCertificado = CertificadoDigitalPorUsuarioListar(codUsuario);
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return oCertificado;
        }


        public List<tbCertificadoDigital> CertificadoDigitalPorUsuarioListar(string codUsuario)
        {
            List<tbCertificadoDigital> ListarCertificado = new List<tbCertificadoDigital>();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO FROM CERAR4 WHERE CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCertificadoDigital oCertificado = new tbCertificadoDigital();
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        ListarCertificado.Add(oCertificado);
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return ListarCertificado;
        }

        public tbCertificadoDigital CertificadoDigitalPorUsuarioDelega(string codUsuario, Int32 secuencial)
        {
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO, ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE FROM CERAR4 WHERE  CERC10 = '" + codUsuario.Trim().ToUpper() + "' AND CERSEU = " + secuencial);
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());

                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return oCertificado;
        }


        public List<tbCertificadoDigital> CertificadoDigitalPorUsuarioListarDelegar(string codUsuario)
        {
            List<tbCertificadoDigital> ListarCertificado = new List<tbCertificadoDigital>();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO, ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE  FROM CERAR4 WHERE CERASI = 'AC' AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCertificadoDigital oCertificado = new tbCertificadoDigital();
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                        oCertificado.oCertificado = CertificadoDigitalPorUsuarioListar(codUsuario);

                        ListarCertificado.Add(oCertificado);
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return ListarCertificado;
        }




        public bool VerificaExistaCertificadoDigital(string codUsuario)
        {
            bool estadoExiste = false;
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO FROM CERAR4 WHERE CERASI = 'AC' AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        estadoExiste = true;
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return estadoExiste;
        }

        public bool VerificaExistaCertificadoDigitalDelega(string codUsuario)
        {
            bool estadoExiste = false;
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO FROM CERAR4 WHERE CERASI = 'AC' AND CERPAT == '' AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        estadoExiste = true;
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return estadoExiste;
        }

        #region "Delegado"
        /// <summary>
        /// Metodo Certificado Digital
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns>tbCertificadoDigital</returns>
        public tbCertificadoDigital CertificadoDigitalPorUsuarioDelegado(string codUsuario)
        {
            tbCertificadoDigital oCertificado = new tbCertificadoDigital();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO  ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE FROM CERAR4 WHERE CERASI = 'AC' AND CEROI3 > 0 AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                        oCertificado.oCertificado = CertificadoDigitalPorUsuarioListar(codUsuario);
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return oCertificado;
        }


        public List<tbCertificadoDigital> CertificadoDigitalPorUsuarioListarDelegado(string codUsuario)
        {
            List<tbCertificadoDigital> ListarCertificado = new List<tbCertificadoDigital>();
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("SELECT ifnull(rtrim(ltrim(CERC10)), '') AS CODIGOUSUARIO, ifnull(CERSEU, 0) AS SECUENCIAL, ifnull(rtrim(ltrim(CERRUC)), '') AS RUC, ifnull(rtrim(ltrim(CERSER)), '') AS SERIE,");
                sb.Append(" ifnull(rtrim(ltrim(CERASI)), '') AS ASIGNADO, ifnull(rtrim(ltrim(CERF05)), '') AS FECHASUBIDA, ifnull(rtrim(ltrim(CERF06)), '') AS FECHAVENCIMIENTO,");
                sb.Append(" ifnull(rtrim(ltrim(CERCON)), '') AS CONTRASENA, ifnull(rtrim(ltrim(CERDOC)), '') AS PATHDOCUMENTO, ifnull(rtrim(ltrim(CERPAT)), '') AS PATHIMAGENFIRMA, ifnull(rtrim(ltrim(CERUS8)), '') AS USUARIOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF07)), '') AS FECHACREADO, ifnull(rtrim(ltrim(CERHO8)), '') AS HORACREADO, ifnull(rtrim(ltrim(CERUS9)), '') AS USUARIOMODIFICADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERF08)), '') AS FECHAMODIFICADO, ifnull(rtrim(ltrim(CERHO9)), '') AS HORAMODIFICADO, ifnull(rtrim(ltrim(CERDI8)), '') AS DISPOSITIVOCREADO,");
                sb.Append(" ifnull(rtrim(ltrim(CERDI9)), '') AS DISPOSITIVOMODIFICADO  ifnull(CEROI3, 0) AS OIDCERTIFICADOPADRE FROM CERAR4 WHERE CEROI3 > 0 AND CERC10 = '" + codUsuario.Trim().ToUpper() + "'");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tbCertificadoDigital oCertificado = new tbCertificadoDigital();
                        oCertificado.CodigoUsuario = dr["CodigoUsuario"].ToString();
                        oCertificado.Secuencial = Int32.Parse(dr["Secuencial"].ToString());
                        oCertificado.Ruc = dr["Ruc"].ToString();
                        oCertificado.Serie = dr["Serie"].ToString();
                        oCertificado.Asignado = dr["Asignado"].ToString();
                        oCertificado.FechaSubida = dr["FechaSubida"].ToString();
                        oCertificado.FechaVencimiento = dr["FechaVencimiento"].ToString();
                        oCertificado.Contrasena = dr["Contrasena"].ToString();
                        oCertificado.PathDocumento = dr["PathDocumento"].ToString();
                        oCertificado.PathImagenFirma = dr["PathImagenFirma"].ToString();
                        oCertificado.UsuarioCreado = dr["UsuarioCreado"].ToString();
                        oCertificado.FechaCreado = dr["FechaCreado"].ToString();
                        oCertificado.HoraCreado = dr["HoraCreado"].ToString();
                        oCertificado.DispositivoCreado = dr["DispositivoCreado"].ToString();
                        oCertificado.UsuarioModificado = dr["UsuarioModificado"].ToString();
                        oCertificado.FechaModificado = dr["FechaModificado"].ToString();
                        oCertificado.HoraModificado = dr["HoraModificado"].ToString();
                        oCertificado.DispositivoModificado = dr["DispositivoModificado"].ToString();
                        oCertificado.OidCertificadoPadre = Int32.Parse(dr["OIDCERTIFICADOPADRE"].ToString());
                        ListarCertificado.Add(oCertificado);
                    }
                    dr.Close();
                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return ListarCertificado;
        }
        #endregion


        /// <summary>
        /// Metodo nuevo Certificado Digital
        /// </summary>
        /// <param name="certificadoDigital"></param>
        /// <returns>True o False</returns>
        public bool CertificadoDigitalNuevo(tbCertificadoDigital certificadoDigital)
        {
            bool respuesta = false;
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            tbSistema osistema = new tbSistema();
            Int32 osecuencial = 0;
            try
            {
                osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                osecuencial = GeneraSecuencial();
                sb.Append("INSERT INTO CERAR4 (CERC10, CERSEU, CERRUC, CERSER, CERASI, CERF05, CERF06, CERCON, CERDOC, CERPAT, CERUS8, CERF07, CERHO8, CERDI8)");
                sb.Append(" VALUES(@CodigoUsuario, @Secuencial, @Ruc, @Serie, @Asignado, @FechaSubida, @FechaVencimiento, @Contrasena, @PathDocumento, @PathImagenFirma, @UsuarioCreado, @FechaCreado, @HoraCreado, @DispositivoCreado) ");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@CodigoUsuario"].Value = certificadoDigital.CodigoUsuario;
                    cmd.Parameters["@Secuencial"].Value = osecuencial;
                    cmd.Parameters["@Ruc"].Value = campoNull(certificadoDigital.Ruc);
                    cmd.Parameters["@Serie"].Value = campoNull(certificadoDigital.Serie);
                    cmd.Parameters["@Asignado"].Value = campoNull(certificadoDigital.Asignado);
                    cmd.Parameters["@FechaSubida"].Value = campoNull(certificadoDigital.FechaSubida);
                    cmd.Parameters["@FechaVencimiento"].Value = campoNull(certificadoDigital.FechaVencimiento);
                    cmd.Parameters["@Contrasena"].Value = campoNull(certificadoDigital.Contrasena);
                    cmd.Parameters["@PathDocumento"].Value = campoNull(certificadoDigital.PathDocumento);
                    cmd.Parameters["@PathImagenFirma"].Value = campoNull(certificadoDigital.PathImagenFirma);
                    cmd.Parameters["@UsuarioCreado"].Value = certificadoDigital.UsuarioCreado;
                    cmd.Parameters["@FechaCreado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraCreado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@DispositivoCreado"].Value = campoNull(certificadoDigital.DispositivoCreado);

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }


        /// <summary>
        /// Metodo nuevo Certificado Digital
        /// </summary>
        /// <param name="certificadoDigital"></param>
        /// <returns>True o False</returns>
        public bool CertificadoDigitalActualizar(tbCertificadoDigital certificadoDigital)
        {
            bool respuesta = false;
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();
            tbSistema osistema = new tbSistema();
            try
            {
                osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                sb.Append("UPDATE CERAR4 SET CERRUC = @Ruc, CERSER = @Serie, CERASI = @Asignado, CERF05 = @FechaSubida, CERF06 = @FechaVencimiento, ");
                sb.Append(" CERCON = @Contrasena, CERDOC = @PathDocumento, CERPAT = @PathImagenFirma, CERUS9 = @UsuarioModificado, ");
                sb.Append(" CERF08 = @FechaModificado, CERHO9 = @HoraModificado, CERDI9 = @DispositivoModificado, CEROI3 = @OidCertificadoPadre");
                sb.Append(" WHERE CERC10 = @CodigoUsuario AND CERSEU = @Secuencial");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Ruc"].Value = campoNull(certificadoDigital.Ruc);
                    cmd.Parameters["@Serie"].Value = campoNull(certificadoDigital.Serie);
                    cmd.Parameters["@Asignado"].Value = campoNull(certificadoDigital.Asignado);
                    cmd.Parameters["@FechaSubida"].Value = campoNull(certificadoDigital.FechaSubida);
                    cmd.Parameters["@FechaVencimiento"].Value = campoNull(certificadoDigital.FechaVencimiento);
                    cmd.Parameters["@Contrasena"].Value = campoNull(certificadoDigital.Contrasena);
                    cmd.Parameters["@PathDocumento"].Value = campoNull(certificadoDigital.PathDocumento);
                    cmd.Parameters["@PathImagenFirma"].Value = campoNull(certificadoDigital.PathImagenFirma);
                    cmd.Parameters["@UsuarioModificado"].Value = certificadoDigital.UsuarioModificado;
                    cmd.Parameters["@FechaModificado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraModificado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@DispositivoModificado"].Value = campoNull(certificadoDigital.DispositivoCreado);
                    cmd.Parameters["@OidCertificadoPadre"].Value = certificadoDigital.OidCertificadoPadre;
                    cmd.Parameters["@CodigoUsuario"].Value = certificadoDigital.CodigoUsuario;
                    cmd.Parameters["@Secuencial"].Value = certificadoDigital.Secuencial;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }

            }
            catch (iDB2Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }

        public bool CertificadoDigitalCambiaEstado(string estadoCertificado, string usuarioModificado, decimal secuencialId)
        {
            bool respuesta = false;
            string query = string.Empty;
            StringBuilder sb = new StringBuilder();

            try
            {
                var osistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                sb.Append("UPDATE CERAR4 SET CERASI = @Asignado, CERUS9 = @UsuarioModificado,");
                sb.Append(" CERF08 = @FechaModificado, CERHO9 = @HoraModificado");
                sb.Append(" WHERE CERC10 = @CodigoUsuario AND CERSEU = @Secuencial");
                query = sb.ToString();
                iDB2Command cmd;
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    cmd.DeriveParameters();
                    cmd.Parameters["@Asignado"].Value = campoNull(estadoCertificado);
                    cmd.Parameters["@UsuarioModificado"].Value = usuarioModificado;
                    cmd.Parameters["@FechaModificado"].Value = osistema.FechaSistema;
                    cmd.Parameters["@HoraModificado"].Value = osistema.HoraSistema;
                    cmd.Parameters["@CodigoUsuario"].Value = usuarioModificado;
                    cmd.Parameters["@Secuencial"].Value = secuencialId;

                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    cmd.Dispose();
                    oConexion.Close();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return respuesta;
        }

        /// <summary>
        /// Metodo retorna el secuencial de Certificado Digital
        /// </summary>
        /// <returns>Secuencial</returns>
        private Int32 GeneraSecuencial()
        {
            string query = "SELECT IFNULL(max(CERSEU), 0) + 1 AS Secuencial FROM CERAR4";
            iDB2Command cmd;
            Int32 secuencial = 0;
            try
            {
                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        secuencial = Int32.Parse(dr["Secuencial"].ToString());
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

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }

        private String fechaAS400(string ofecha)
        {
            string odate = string.Empty;
            try
            {
                if (ofecha.Trim().Length > 0)
                {
                    DateTime ofechaForato = Convert.ToDateTime(ofecha);
                    odate = ofechaForato.ToString("yyyyMMdd");
                }
            }
            catch
            {
                odate = ofecha;
            }
            return odate;
        }


    }
}
