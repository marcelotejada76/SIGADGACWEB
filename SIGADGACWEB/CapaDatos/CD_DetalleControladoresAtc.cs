using CapaModelo;
using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DetalleControladoresAtc
    {
        public static CD_DetalleControladoresAtc _instancia = null;
        private CD_DetalleControladoresAtc()
        {

        }

        public static CD_DetalleControladoresAtc Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleControladoresAtc();
                }
                return _instancia;
            }
        }

        public List<tbDetalleControladoresAtc> DetalleControladoresAtc(string Lugar, string Dependencia, string Fechaelab, string Turno)
        {
            List<tbDetalleControladoresAtc> listarSolicitud = new List<tbDetalleControladoresAtc>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("select OPCLI1,OPCN02,OPCF07,OPCES5 " +
                    "FROM OPCA01   LEFT JOIN OPCAR9 ON OPCLI1= OPCLIC WHERE " +
                    "OPCLUG = '" + Lugar + "' AND OPCDEP='" + Dependencia + "' AND OPCF06 ='" + Fechaelab + "' AND OPCTUR='" + Turno + "' ");


                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();
                    decimal Valor = 0;
                    while (dr.Read())
                    {
                        // , ,,,,
                        tbDetalleControladoresAtc oSolicitud = new tbDetalleControladoresAtc();

                        oSolicitud.LICENCIACONTROLADOR = dr["OPCLI1"].ToString();
                        oSolicitud.NOMBRECONTROLADOR = dr["OPCN02"].ToString();
                        oSolicitud.VIGENCIACEMAC = dr["OPCF07"].ToString();
                        

                        string estado = dr["OPCES5"].ToString();
                        switch (estado)
                        {
                            case "AC":
                                oSolicitud.ESTADO = "ACTIVO";
                                break;

                            case "NO":
                                oSolicitud.ESTADO = "NO ACTIVO";
                                break;

                            case "EN":
                                oSolicitud.ESTADO = "ENTRENAMIENTO";
                                break;

                            default:
                                break;
                        }


                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listarSolicitud;
        }
        //llena detalle de la recaudacion
        

    }
}
