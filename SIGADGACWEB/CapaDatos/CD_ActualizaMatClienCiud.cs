using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ActualizaMatClienCiud
    {
        public static CD_ActualizaMatClienCiud _instancia = null;
        private CD_ActualizaMatClienCiud()
        {

        }

        public static CD_ActualizaMatClienCiud Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_ActualizaMatClienCiud();
                }
                return _instancia;
            }
        }

      

        public bool ActualizarDatosMatricula()
        {
            bool status = false;

            try
            {
                // Aeronave
                var listaAeronave = TablasRecuperadas.Aeronave();
                if (listaAeronave != null && listaAeronave.Count > 0)
                {
                    InsertarPower9.insertar_transaccionAeronave(listaAeronave);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZAMAT", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Matricula modificada
                var listaAeronaveMod = TablasRecuperadas.AeronaveModifica();
                if (listaAeronaveMod != null && listaAeronaveMod.Count > 0)
                {
                    InsertarPower9.insertar_transaccionAeronave(listaAeronaveMod);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZAMAT", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Compañia
                var listaCompania = TablasRecuperadas.Compañia();
                if (listaCompania != null && listaCompania.Count > 0)
                {
                    InsertarPower9.insertar_Compania(listaCompania);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZACIAAV", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Compañia modificada
                var listaCompaniaMod = TablasRecuperadas.CompañiaModificada();
                if (listaCompaniaMod != null && listaCompaniaMod.Count > 0)
                {
                    InsertarPower9.insertar_Compania(listaCompaniaMod);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZACIAAV", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Clientes Facturación
                var Clientefact = TablasRecuperadas.ClienteFacturas();
                if (Clientefact != null && Clientefact.Count > 0)
                {
                    InsertarPower9.insertar_ClienteFactura(Clientefact);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZAEMPRESA", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Cliente Modificado
                var ClientefactMod = TablasRecuperadas.ClienteFacturasModificado();
                if (ClientefactMod != null && ClientefactMod.Count > 0)
                {
                    InsertarPower9.insertar_ClienteFactura(ClientefactMod);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZAEMPRESA", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }

                // Ciudad
                var Ciudad = TablasRecuperadas.CiudadCreada();
                if (Ciudad != null && Ciudad.Count > 0)
                {
                    InsertarPower9.insertar_Ciudad(Ciudad);

                    using (iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion))
                    {
                        con.Open();
                        using (iDB2Command cm = new iDB2Command("PA_ACTUALIZACIAAV", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandTimeout = 0;
                            cm.ExecuteNonQuery();
                        }
                    }
                }
              

                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                throw ex;
            }

            return status;
        }
      

        private string campoNull(string campo)
        {
            if (String.IsNullOrEmpty(campo))
                campo = "";
            return campo;
        }
    }
}
