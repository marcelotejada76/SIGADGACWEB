using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class TablasRecuperadas
    {
        //actualiza aeronave del p9
        public static void ActualizaAeronaveP9(List<DatosGlobales> listaMensaje)

        {
            foreach (DatosGlobales mensaje in listaMensaje)
            {
                string query = "update AERONAVECOMPANIA SET PESOMAXESTRUCTURAL = " + mensaje.PESOMAXIMO + ", ANIOFABRICACION=" + mensaje.ANIOFABRICACION + "  " +
                    " , NUMPASAJEROS=" + mensaje.NUMPASAJEROS + " , PESOVACIOKG=" + mensaje.PESOVACIOKG + " , USUARIOMODIFICA='" + mensaje.USUARIOMODIFICA + "'" +
                    "  where oid=" + mensaje.OIDAERONAVE + "";
                OdbcCommand cmd;
                try
                {
                    using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                    {
                        cmd = new OdbcCommand(query, oConexion);
                        oConexion.Open();
                        OdbcDataReader dr = cmd.ExecuteReader();

                        dr.Close();
                        oConexion.Close();
                    }

                }
                catch (Exception ex)
                {

                }
            }
            //return sec;

        }


        //aeronave
        public static List<DatosGlobales> Aeronave()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstAeronave = new List<DatosGlobales>();
            string query = "Select AE.OID AS OIDCIA,AE.MATRICULA,AE.MTOW,AE.REGION,AE.APROBADO,AE.NUMEROSERIE,AE.DESTINADOAUSO,AE.HORASTOTALT,AE.ESTADO,AE.HORASTOTALAR," +
                "AE.HORASTOTALR,AE.HORASTOTALAT,AE.CICLOSTOTAL,AE.ANIOFABRICACION,AE.CICLOSTOTALA,AE.PESOMAXATERRIZAJE,AE.ESPECIFICACION,AE.PESOMAXESTRUCTURAL," +
                "AE.CONDVUELO,AE.NUMPASAJEROS,AE.NUMPERSEXTRAS,AE.PESOVACIOKG,AE.AUTONOMIA,AE.CATEGORIA,AE.NUMTRIPULANTES,AE.NIBRAS,AE.CAPGALCOMBUSTIBLE,AE.GLIDESLOPE," +
                "AE.PILOTOAUTOMATICO,AE.VHF,AE.HF,AE.ADF,AE.VOR,AE.BCN,AE.RADAR,AE.DME,AE.ESELVA,AE.ELT,AE.BOTIQUIN,AE.EMAR,AE.EXTINGUIDOR,AE.EOXIGENO," +
                "AE.SENIALES,AE.FECHAINGPAIS,AE.FECHAULTINSPECCION,AE.ULTIMOPAIS,AE.LUGARINSPECCION,AE.GALONES_HORA,AE.EXCEPCIONES,AE.FECHAULTPB,AE.FECHAFINPB,AE.BASEOPERACION," +
                "AE.SISALTIMETRICO,AE.TRANSPONDER,AE.BRUJULA,AE.PITOT,AE.TIEMPOTOTAL,AE.REG,AE.NUMEROREGISTRO,AE.FECHAREGISTRO,AE.CATEGORIAAERONAVE," +
                "AE.FECHAEXPEDICION,AE.TIPOMATRICULA,AE.ESTADOMATRICULA,AE.LIMITANTEOPERA,AE.FECHACANCELACION,AE.REMRELOJ,AE.NACIONALIDAD_MATRICULA,AE.ULTPROPIETARIO," +
                "AE.DESCANCELACION,AE.INSCRIPMATRIEXT,AE.ARRENDADO,AE.FECHAVALIDO,AE.OBSERVACION,AE.GRAVAMEN,AE.AVALUO,AE.USUARIOCREA,AE.FECHACREA,AE.USUARIOMODIFICA,AE.FECHAMODIFICA," +
                "MODELO.MODELO,CIA.CODIGO_OACI,CIA.OID,MARCA.NOMBRE from AERONAVECOMPANIA AS AE LEFT JOIN COMPANIA AS CIA ON CIA.OID = OIDCOMPANIA LEFT JOIN AERONAVE AS MODELO ON MODELO.OID = OIDAERONAVE " +
                "LEFT JOIN MARCA AS MARCA ON MARCA.OID = MODELO.OIDMARCA  WHERE  (AE.ESTADO IS NULL OR AE.ESTADO <> 5) and CAST(ae.FECHACREA AS date) = '" + fecha + "'";
            // " WHERE CIA.CODIGO_OACI='EEA' ";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    int cont = 1;
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        try
                        {
                            DatosGlobales oAeronave = new DatosGlobales();
                            string Caracter = "";
                            Caracter = ValidaCampos.ValidaBlancos(dr["MATRICULA"]);
                            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "-", "%" };
                            foreach (var c in charsToRemove)
                            {
                                Caracter = Caracter.Replace(c, string.Empty);
                            }

                            oAeronave.MATRICULA = Caracter;
                            try
                            {
                                int numcar = oAeronave.MATRICULA.Length;
                                if (numcar == 9)
                                {
                                    numcar = numcar - 3;
                                    oAeronave.MATRICULAINDRA = Caracter.Substring(0, numcar);
                                }
                                else
                                {
                                    if (numcar == 8)
                                    {
                                        numcar = numcar - 2;
                                        oAeronave.MATRICULAINDRA = Caracter.Substring(0, numcar);
                                    }
                                    else
                                    {
                                        oAeronave.MATRICULAINDRA = oAeronave.MATRICULA;
                                    }
                                }

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            oAeronave.OIDAERONAVE = ValidaCampos.ValidaEntero(dr["OIDCIA"]);
                            oAeronave.OIDCIA = ValidaCampos.ValidaEntero(dr["OID"]);
                            oAeronave.REGION = ValidaCampos.ValidaEntero(dr["REGION"]);
                            oAeronave.APROBADO = ValidaCampos.ValidaEntero(dr["APROBADO"]);
                            oAeronave.NUMEROSERIE = ValidaCampos.ValidaBlancos(dr["NUMEROSERIE"]);

                            oAeronave.DESTINADOAUSO = ValidaCampos.ValidaEntero(dr["DESTINADOAUSO"]);
                            oAeronave.HORASTOTALT = ValidaCampos.ValidaEntero(dr["HORASTOTALT"]);
                            oAeronave.ESTADO = ValidaCampos.ValidaEntero(dr["ESTADO"]);
                            oAeronave.HORASTOTALAR = ValidaCampos.ValidaEntero(dr["HORASTOTALAR"]);
                            oAeronave.HORASTOTALR = ValidaCampos.ValidaEntero(dr["HORASTOTALR"]);
                            oAeronave.HORASTOTALAT = ValidaCampos.ValidaEntero(dr["HORASTOTALAT"]);
                            oAeronave.CICLOSTOTAL = ValidaCampos.ValidaEntero(dr["CICLOSTOTAL"]);
                            oAeronave.ANIOFABRICACION = ValidaCampos.ValidaBlancos(dr["ANIOFABRICACION"]);

                            oAeronave.CICLOSTOTALA = ValidaCampos.ValidaEntero(dr["CICLOSTOTALA"]);
                            oAeronave.PESOMAXATERRIZAJE = ValidaCampos.ValidaEntero(dr["PESOMAXATERRIZAJE"]);
                            oAeronave.ESPECIFICACION = ValidaCampos.ValidaBlancos(dr["ESPECIFICACION"]);

                            oAeronave.PESOMAXESTRUCTURAL = ValidaCampos.ValidaDecimal(dr["PESOMAXESTRUCTURAL"]);
                            oAeronave.CONDVUELO = ValidaCampos.ValidaBlancos(dr["CONDVUELO"]);

                            oAeronave.NUMPASAJEROS = ValidaCampos.ValidaEntero(dr["NUMPASAJEROS"]);
                            oAeronave.NUMPERSEXTRAS = ValidaCampos.ValidaEntero(dr["NUMPERSEXTRAS"]);
                            oAeronave.PESOVACIOKG = ValidaCampos.ValidaEntero(dr["PESOVACIOKG"]);
                            oAeronave.AUTONOMIA = ValidaCampos.ValidaBlancos(dr["AUTONOMIA"]);

                            oAeronave.CATEGORIA = ValidaCampos.ValidaEntero(dr["CATEGORIA"]);
                            oAeronave.NUMTRIPULANTES = ValidaCampos.ValidaEntero(dr["NUMTRIPULANTES"]);
                            oAeronave.NIBRAS = ValidaCampos.ValidaEntero(dr["NIBRAS"]);
                            oAeronave.CAPGALCOMBUSTIBLE = ValidaCampos.ValidaEntero(dr["CAPGALCOMBUSTIBLE"]);
                            try
                            {
                                oAeronave.GLIDESLOPE = ValidaCampos.ValidaCaracter(dr["GLIDESLOPE"]);
                                oAeronave.PILOTOAUTOMATICO = ValidaCampos.ValidaCaracter(dr["PILOTOAUTOMATICO"]);
                                oAeronave.VHF = ValidaCampos.ValidaCaracter(dr["VHF"]);
                                oAeronave.HF = ValidaCampos.ValidaCaracter(dr["HF"]);
                                oAeronave.ADF = ValidaCampos.ValidaCaracter(dr["ADF"]);
                                oAeronave.VOR = ValidaCampos.ValidaCaracter(dr["VOR"]);
                                oAeronave.BCN = ValidaCampos.ValidaCaracter(dr["BCN"]);
                                oAeronave.RADAR = ValidaCampos.ValidaCaracter(dr["RADAR"]);
                                oAeronave.DME = ValidaCampos.ValidaCaracter(dr["DME"]);
                                oAeronave.ESELVA = ValidaCampos.ValidaCaracter(dr["ESELVA"]);
                                oAeronave.ELT = ValidaCampos.ValidaCaracter(dr["ELT"]);
                                oAeronave.BOTIQUIN = ValidaCampos.ValidaCaracter(dr["BOTIQUIN"]);
                                oAeronave.EMAR = ValidaCampos.ValidaCaracter(dr["EMAR"]);
                                oAeronave.EXTINGUIDOR = ValidaCampos.ValidaCaracter(dr["EXTINGUIDOR"]);
                                oAeronave.EOXIGENO = ValidaCampos.ValidaCaracter(dr["EOXIGENO"]);
                                oAeronave.SENIALES = ValidaCampos.ValidaCaracter(dr["SENIALES"]);
                            }
                            catch (Exception EX)
                            {

                                Console.WriteLine("matricula: " + oAeronave.MATRICULA);
                            }

                            oAeronave.FECHAINGPAIS = ValidaCampos.ValidaFecha(dr["FECHAINGPAIS"]);
                            oAeronave.FECHAULTINSPECCION = ValidaCampos.ValidaFecha(dr["FECHAULTINSPECCION"]);
                            oAeronave.ULTIMOPAIS = ValidaCampos.ValidaBlancos(dr["ULTIMOPAIS"]);
                            oAeronave.LUGARINSPECCION = ValidaCampos.ValidaBlancos(dr["LUGARINSPECCION"]);

                            oAeronave.GALONES_HORA = ValidaCampos.ValidaDecimal(dr["GALONES_HORA"]);
                            oAeronave.ULTIMOPAIS = ValidaCampos.ValidaBlancos(dr["ULTIMOPAIS"]);

                            string cadena = ValidaCampos.ValidaBlancos(dr["EXCEPCIONES"]);
                            int car = cadena.Length;
                            if (car > 100)
                            {
                                cadena = cadena.Substring(0, 99);
                                oAeronave.EXCEPCIONES = cadena;
                            }
                            else
                            {
                                oAeronave.EXCEPCIONES = cadena;
                            }
                            oAeronave.FECHAULTPB = ValidaCampos.ValidaFecha(dr["FECHAULTPB"]);
                            oAeronave.FECHAFINPB = ValidaCampos.ValidaFecha(dr["FECHAFINPB"]);
                            oAeronave.BASEOPERACION = ValidaCampos.ValidaBlancos(dr["BASEOPERACION"]);

                            oAeronave.SISALTIMETRICO = ValidaCampos.ValidaFecha(dr["SISALTIMETRICO"]);
                            oAeronave.TRANSPONDER = ValidaCampos.ValidaFecha(dr["TRANSPONDER"]);
                            oAeronave.BRUJULA = ValidaCampos.ValidaFecha(dr["BRUJULA"]);
                            oAeronave.PITOT = ValidaCampos.ValidaFecha(dr["PITOT"]);
                            oAeronave.TIEMPOTOTAL = ValidaCampos.ValidaEntero(dr["TIEMPOTOTAL"]);
                            oAeronave.REG = ValidaCampos.ValidaEntero(dr["REG"]);
                            oAeronave.NUMEROREGISTRO = ValidaCampos.ValidaBlancos(dr["NUMEROREGISTRO"]);

                            oAeronave.FECHAREGISTRO = ValidaCampos.ValidaFecha(dr["FECHAREGISTRO"]);
                            oAeronave.CATEGORIAAERONAVE = ValidaCampos.ValidaBlancos(dr["CATEGORIAAERONAVE"]);

                            oAeronave.FECHAEXPEDICION = ValidaCampos.ValidaFecha(dr["FECHAEXPEDICION"]);
                            oAeronave.TIPOMATRICULA = ValidaCampos.ValidaCaracter(dr["TIPOMATRICULA"]);
                            oAeronave.ESTADOMATRICULA = ValidaCampos.ValidaCaracter(dr["ESTADOMATRICULA"]);
                            oAeronave.MTOW = ValidaCampos.ValidaDecimal(dr["MTOW"]);
                            oAeronave.LIMITANTEOPERA = ValidaCampos.ValidaEntero(dr["LIMITANTEOPERA"]);
                            oAeronave.FECHACANCELACION = ValidaCampos.ValidaFecha(dr["FECHACANCELACION"]);
                            oAeronave.REMRELOJ = ValidaCampos.ValidaDecimal(dr["REMRELOJ"]);
                            oAeronave.NACIONALIDAD_MATRICULA = ValidaCampos.ValidaCaracter(dr["NACIONALIDAD_MATRICULA"]);
                            oAeronave.ULTPROPIETARIO = ValidaCampos.ValidaBlancos(dr["ULTPROPIETARIO"]);

                            try
                            {
                                cadena = ValidaCampos.ValidaBlancos(dr["DESCANCELACION"]);
                                // cadena = dr["DESCANCELACION"].ToString();

                                car = cadena.Length;
                                if (car == 217)
                                {
                                    oAeronave.DESCANCELACION = "";
                                }
                                else
                                {
                                    if (car > 100)
                                    {
                                        cadena = cadena.Substring(0, 99);
                                        oAeronave.DESCANCELACION = cadena;
                                    }
                                    else
                                    {
                                        oAeronave.DESCANCELACION = cadena;
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                oAeronave.DESCANCELACION = "";
                            }

                            cadena = ValidaCampos.ValidaBlancos(dr["INSCRIPMATRIEXT"]);
                            car = cadena.Length;
                            if (car > 100)
                            {
                                cadena = cadena.Substring(0, 99);
                                oAeronave.INSCRIPMATRIEXT = cadena;
                            }
                            else
                            {
                                oAeronave.INSCRIPMATRIEXT = cadena;
                            }
                            oAeronave.ARRENDADO = ValidaCampos.ValidaBlancos(dr["ARRENDADO"]);
                            oAeronave.FECHAVALIDO = ValidaCampos.ValidaFecha(dr["FECHAVALIDO"]);
                            oAeronave.GRAVAMEN = ValidaCampos.ValidaBlancos(dr["GRAVAMEN"]);
                            cadena = ValidaCampos.ValidaBlancos(dr["OBSERVACION"]);
                            car = cadena.Length;
                            if (car > 100)
                            {
                                cadena = cadena.Substring(0, 99);
                                oAeronave.OBSERVACION = cadena;
                            }
                            else
                            {
                                oAeronave.OBSERVACION = cadena;
                            }
                            oAeronave.AVALUO = ValidaCampos.ValidaDecimal(dr["AVALUO"]);
                            //REFERENCIAS
                            cadena = ValidaCampos.ValidaBlancos(dr["MODELO"]);
                            car = cadena.Length;
                            if (car > 30)
                            {
                                cadena = cadena.Substring(0, 29);
                                oAeronave.MODELO = cadena;
                            }
                            else
                            {
                                oAeronave.MODELO = cadena;
                            }
                            oAeronave.CODIGOOACI = ValidaCampos.ValidaBlancos(dr["CODIGO_OACI"]);
                            oAeronave.USUARIOCREA = ValidaCampos.ValidaBlancos(dr["USUARIOCREA"]);
                            oAeronave.FECHACREA = ValidaCampos.ValidaFechaCreacion(dr["FECHACREA"]);
                            oAeronave.USUARIOMODIFICA = ValidaCampos.ValidaBlancos(dr["USUARIOMODIFICA"]);
                            oAeronave.FECHAMODIFICA = ValidaCampos.ValidaFechaCreacion(dr["FECHAMODIFICA"]);
                            oAeronave.Nombre = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                            Console.WriteLine(oAeronave.MATRICULA + "contador. ." + cont);
                            cont++;
                            lstAeronave.Add(oAeronave);
                        }
                        catch (Exception ex)
                        {


                        }
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstAeronave;
            }
            catch (Exception ex)
            {

            }
            return lstAeronave;
        }
        public static List<DatosGlobales> AeronaveModifica()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstAeronave = new List<DatosGlobales>();
            string query = "Select AE.OID AS OIDCIA,AE.MATRICULA,AE.MTOW,AE.REGION,AE.APROBADO,AE.NUMEROSERIE,AE.DESTINADOAUSO,AE.HORASTOTALT,AE.ESTADO,AE.HORASTOTALAR," +
                "AE.HORASTOTALR,AE.HORASTOTALAT,AE.CICLOSTOTAL,AE.ANIOFABRICACION,AE.CICLOSTOTALA,AE.PESOMAXATERRIZAJE,AE.ESPECIFICACION,AE.PESOMAXESTRUCTURAL," +
                "AE.CONDVUELO,AE.NUMPASAJEROS,AE.NUMPERSEXTRAS,AE.PESOVACIOKG,AE.AUTONOMIA,AE.CATEGORIA,AE.NUMTRIPULANTES,AE.NIBRAS,AE.CAPGALCOMBUSTIBLE,AE.GLIDESLOPE," +
                "AE.PILOTOAUTOMATICO,AE.VHF,AE.HF,AE.ADF,AE.VOR,AE.BCN,AE.RADAR,AE.DME,AE.ESELVA,AE.ELT,AE.BOTIQUIN,AE.EMAR,AE.EXTINGUIDOR,AE.EOXIGENO," +
                "AE.SENIALES,AE.FECHAINGPAIS,AE.FECHAULTINSPECCION,AE.ULTIMOPAIS,AE.LUGARINSPECCION,AE.GALONES_HORA,AE.EXCEPCIONES,AE.FECHAULTPB,AE.FECHAFINPB,AE.BASEOPERACION," +
                "AE.SISALTIMETRICO,AE.TRANSPONDER,AE.BRUJULA,AE.PITOT,AE.TIEMPOTOTAL,AE.REG,AE.NUMEROREGISTRO,AE.FECHAREGISTRO,AE.CATEGORIAAERONAVE," +
                "AE.FECHAEXPEDICION,AE.TIPOMATRICULA,AE.ESTADOMATRICULA,AE.LIMITANTEOPERA,AE.FECHACANCELACION,AE.REMRELOJ,AE.NACIONALIDAD_MATRICULA,AE.ULTPROPIETARIO," +
                "AE.DESCANCELACION,AE.INSCRIPMATRIEXT,AE.ARRENDADO,AE.FECHAVALIDO,AE.OBSERVACION,AE.GRAVAMEN,AE.AVALUO,AE.USUARIOCREA,AE.FECHACREA,AE.USUARIOMODIFICA,AE.FECHAMODIFICA," +
                "MODELO.MODELO,CIA.CODIGO_OACI,CIA.OID,MARCA.NOMBRE from AERONAVECOMPANIA AS AE LEFT JOIN COMPANIA AS CIA ON CIA.OID = OIDCOMPANIA LEFT JOIN AERONAVE AS MODELO ON MODELO.OID = OIDAERONAVE " +
                "LEFT JOIN MARCA AS MARCA ON MARCA.OID = MODELO.OIDMARCA  WHERE  (AE.ESTADO IS NULL OR AE.ESTADO <> 5) and CAST(ae.FECHAMODIFICA AS date) = '" + fecha + "'";
            // " WHERE CIA.CODIGO_OACI='EEA' ";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    int cont = 1;
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        try
                        {
                            DatosGlobales oAeronave = new DatosGlobales();
                            string Caracter = "";
                            Caracter = ValidaCampos.ValidaBlancos(dr["MATRICULA"]);
                            string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "-", "%" };
                            foreach (var c in charsToRemove)
                            {
                                Caracter = Caracter.Replace(c, string.Empty);
                            }

                            oAeronave.MATRICULA = Caracter;
                            try
                            {
                                int numcar = oAeronave.MATRICULA.Length;
                                if (numcar == 9)
                                {
                                    numcar = numcar - 3;
                                    oAeronave.MATRICULAINDRA = Caracter.Substring(0, numcar);
                                }
                                else
                                {
                                    if (numcar == 8)
                                    {
                                        numcar = numcar - 2;
                                        oAeronave.MATRICULAINDRA = Caracter.Substring(0, numcar);
                                    }
                                    else
                                    {
                                        oAeronave.MATRICULAINDRA = oAeronave.MATRICULA;
                                    }
                                }

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            oAeronave.OIDAERONAVE = ValidaCampos.ValidaEntero(dr["OIDCIA"]);
                            try
                            {
                                oAeronave.OIDCIA = ValidaCampos.ValidaEntero(dr["OIDCIA"]);
                            }
                            catch (Exception ex)
                            {

                            }
                            
                            oAeronave.REGION = ValidaCampos.ValidaEntero(dr["REGION"]);
                            oAeronave.APROBADO = ValidaCampos.ValidaEntero(dr["APROBADO"]);
                            oAeronave.NUMEROSERIE = ValidaCampos.ValidaBlancos(dr["NUMEROSERIE"]);

                            oAeronave.DESTINADOAUSO = ValidaCampos.ValidaEntero(dr["DESTINADOAUSO"]);
                            oAeronave.HORASTOTALT = ValidaCampos.ValidaEntero(dr["HORASTOTALT"]);
                            oAeronave.ESTADO = ValidaCampos.ValidaEntero(dr["ESTADO"]);
                            oAeronave.HORASTOTALAR = ValidaCampos.ValidaEntero(dr["HORASTOTALAR"]);
                            oAeronave.HORASTOTALR = ValidaCampos.ValidaEntero(dr["HORASTOTALR"]);
                            oAeronave.HORASTOTALAT = ValidaCampos.ValidaEntero(dr["HORASTOTALAT"]);
                            oAeronave.CICLOSTOTAL = ValidaCampos.ValidaEntero(dr["CICLOSTOTAL"]);
                            oAeronave.ANIOFABRICACION = ValidaCampos.ValidaBlancos(dr["ANIOFABRICACION"]);

                            oAeronave.CICLOSTOTALA = ValidaCampos.ValidaEntero(dr["CICLOSTOTALA"]);
                            oAeronave.PESOMAXATERRIZAJE = ValidaCampos.ValidaEntero(dr["PESOMAXATERRIZAJE"]);
                            oAeronave.ESPECIFICACION = ValidaCampos.ValidaBlancos(dr["ESPECIFICACION"]);

                            oAeronave.PESOMAXESTRUCTURAL = ValidaCampos.ValidaDecimal(dr["PESOMAXESTRUCTURAL"]);
                            oAeronave.CONDVUELO = ValidaCampos.ValidaBlancos(dr["CONDVUELO"]);

                            oAeronave.NUMPASAJEROS = ValidaCampos.ValidaEntero(dr["NUMPASAJEROS"]);
                            oAeronave.NUMPERSEXTRAS = ValidaCampos.ValidaEntero(dr["NUMPERSEXTRAS"]);
                            oAeronave.PESOVACIOKG = ValidaCampos.ValidaEntero(dr["PESOVACIOKG"]);
                            oAeronave.AUTONOMIA = ValidaCampos.ValidaBlancos(dr["AUTONOMIA"]);

                            oAeronave.CATEGORIA = ValidaCampos.ValidaEntero(dr["CATEGORIA"]);
                            oAeronave.NUMTRIPULANTES = ValidaCampos.ValidaEntero(dr["NUMTRIPULANTES"]);
                            oAeronave.NIBRAS = ValidaCampos.ValidaEntero(dr["NIBRAS"]);
                            oAeronave.CAPGALCOMBUSTIBLE = ValidaCampos.ValidaEntero(dr["CAPGALCOMBUSTIBLE"]);
                            try
                            {
                                oAeronave.GLIDESLOPE = ValidaCampos.ValidaCaracter(dr["GLIDESLOPE"]);
                                oAeronave.PILOTOAUTOMATICO = ValidaCampos.ValidaCaracter(dr["PILOTOAUTOMATICO"]);
                                oAeronave.VHF = ValidaCampos.ValidaCaracter(dr["VHF"]);
                                oAeronave.HF = ValidaCampos.ValidaCaracter(dr["HF"]);
                                oAeronave.ADF = ValidaCampos.ValidaCaracter(dr["ADF"]);
                                oAeronave.VOR = ValidaCampos.ValidaCaracter(dr["VOR"]);
                                oAeronave.BCN = ValidaCampos.ValidaCaracter(dr["BCN"]);
                                oAeronave.RADAR = ValidaCampos.ValidaCaracter(dr["RADAR"]);
                                oAeronave.DME = ValidaCampos.ValidaCaracter(dr["DME"]);
                                oAeronave.ESELVA = ValidaCampos.ValidaCaracter(dr["ESELVA"]);
                                oAeronave.ELT = ValidaCampos.ValidaCaracter(dr["ELT"]);
                                oAeronave.BOTIQUIN = ValidaCampos.ValidaCaracter(dr["BOTIQUIN"]);
                                oAeronave.EMAR = ValidaCampos.ValidaCaracter(dr["EMAR"]);
                                oAeronave.EXTINGUIDOR = ValidaCampos.ValidaCaracter(dr["EXTINGUIDOR"]);
                                oAeronave.EOXIGENO = ValidaCampos.ValidaCaracter(dr["EOXIGENO"]);
                                oAeronave.SENIALES = ValidaCampos.ValidaCaracter(dr["SENIALES"]);
                            }
                            catch (Exception EX)
                            {

                                Console.WriteLine("matricula: " + oAeronave.MATRICULA);
                            }

                            oAeronave.FECHAINGPAIS = ValidaCampos.ValidaFecha(dr["FECHAINGPAIS"]);
                            oAeronave.FECHAULTINSPECCION = ValidaCampos.ValidaFecha(dr["FECHAULTINSPECCION"]);
                            oAeronave.ULTIMOPAIS = ValidaCampos.ValidaBlancos(dr["ULTIMOPAIS"]);
                            oAeronave.LUGARINSPECCION = ValidaCampos.ValidaBlancos(dr["LUGARINSPECCION"]);

                            oAeronave.GALONES_HORA = ValidaCampos.ValidaDecimal(dr["GALONES_HORA"]);
                            oAeronave.ULTIMOPAIS = ValidaCampos.ValidaBlancos(dr["ULTIMOPAIS"]);

                            string cadena = ValidaCampos.ValidaBlancos(dr["EXCEPCIONES"]);
                            int car = cadena.Length;
                            if (car > 100)
                            {
                                cadena = cadena.Substring(0, 99);
                                oAeronave.EXCEPCIONES = cadena;
                            }
                            else
                            {
                                oAeronave.EXCEPCIONES = cadena;
                            }
                            oAeronave.FECHAULTPB = ValidaCampos.ValidaFecha(dr["FECHAULTPB"]);
                            oAeronave.FECHAFINPB = ValidaCampos.ValidaFecha(dr["FECHAFINPB"]);
                            oAeronave.BASEOPERACION = ValidaCampos.ValidaBlancos(dr["BASEOPERACION"]);

                            oAeronave.SISALTIMETRICO = ValidaCampos.ValidaFecha(dr["SISALTIMETRICO"]);
                            oAeronave.TRANSPONDER = ValidaCampos.ValidaFecha(dr["TRANSPONDER"]);
                            oAeronave.BRUJULA = ValidaCampos.ValidaFecha(dr["BRUJULA"]);
                            oAeronave.PITOT = ValidaCampos.ValidaFecha(dr["PITOT"]);
                            oAeronave.TIEMPOTOTAL = ValidaCampos.ValidaEntero(dr["TIEMPOTOTAL"]);
                            oAeronave.REG = ValidaCampos.ValidaEntero(dr["REG"]);
                            oAeronave.NUMEROREGISTRO = ValidaCampos.ValidaBlancos(dr["NUMEROREGISTRO"]);

                            oAeronave.FECHAREGISTRO = ValidaCampos.ValidaFecha(dr["FECHAREGISTRO"]);
                            oAeronave.CATEGORIAAERONAVE = ValidaCampos.ValidaBlancos(dr["CATEGORIAAERONAVE"]);

                            oAeronave.FECHAEXPEDICION = ValidaCampos.ValidaFecha(dr["FECHAEXPEDICION"]);
                            oAeronave.TIPOMATRICULA = ValidaCampos.ValidaCaracter(dr["TIPOMATRICULA"]);
                            oAeronave.ESTADOMATRICULA = ValidaCampos.ValidaCaracter(dr["ESTADOMATRICULA"]);
                            oAeronave.MTOW = ValidaCampos.ValidaDecimal(dr["MTOW"]);
                            oAeronave.LIMITANTEOPERA = ValidaCampos.ValidaEntero(dr["LIMITANTEOPERA"]);
                            oAeronave.FECHACANCELACION = ValidaCampos.ValidaFecha(dr["FECHACANCELACION"]);
                            
                            oAeronave.REMRELOJ = ValidaCampos.ValidaDecimal(dr["REMRELOJ"]);
                            oAeronave.NACIONALIDAD_MATRICULA = ValidaCampos.ValidaCaracter(dr["NACIONALIDAD_MATRICULA"]);
                            oAeronave.ULTPROPIETARIO = ValidaCampos.ValidaBlancos(dr["ULTPROPIETARIO"]);

                            try
                            {
                                cadena = ValidaCampos.ValidaBlancos(dr["DESCANCELACION"]);
                                // cadena = dr["DESCANCELACION"].ToString();

                                car = cadena.Length;
                                if (car == 217)
                                {
                                    oAeronave.DESCANCELACION = "";
                                }
                                else
                                {
                                    if (car > 100)
                                    {
                                        cadena = cadena.Substring(0, 99);
                                        oAeronave.DESCANCELACION = cadena;
                                    }
                                    else
                                    {
                                        oAeronave.DESCANCELACION = cadena;
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                oAeronave.DESCANCELACION = "";
                            }
                            try
                            {
                                cadena = ValidaCampos.ValidaBlancos(dr["INSCRIPMATRIEXT"]);
                                car = cadena.Length;
                                if (car > 100)
                                {
                                    cadena = cadena.Substring(0, 99);
                                    oAeronave.INSCRIPMATRIEXT = cadena;
                                }
                                else
                                {
                                    oAeronave.INSCRIPMATRIEXT = cadena;
                                }
                            }
                            catch (Exception)
                            {

                                oAeronave.INSCRIPMATRIEXT = "";
                            }
                            try
                            {
                                oAeronave.ARRENDADO = ValidaCampos.ValidaBlancos(dr["ARRENDADO"]);
                            }
                            catch (Exception)
                            {

                                oAeronave.ARRENDADO = "";
                            }
                            try
                            {
                                oAeronave.FECHAVALIDO = ValidaCampos.ValidaFecha(dr["FECHAVALIDO"]);
                            }
                            catch (Exception EX)
                            {
                                oAeronave.FECHAVALIDO =Convert.ToString(dr["FECHAVALIDO"]).Trim();

                            }
                            
                            oAeronave.GRAVAMEN = ValidaCampos.ValidaBlancos(dr["GRAVAMEN"]);
                            cadena = ValidaCampos.ValidaBlancos(dr["OBSERVACION"]);
                            car = cadena.Length;
                            if (car > 100)
                            {
                                cadena = cadena.Substring(0, 99);
                                oAeronave.OBSERVACION = cadena;
                            }
                            else
                            {
                                oAeronave.OBSERVACION = cadena;
                            }
                            oAeronave.AVALUO = ValidaCampos.ValidaDecimal(dr["AVALUO"]);
                            //REFERENCIAS
                            cadena = ValidaCampos.ValidaBlancos(dr["MODELO"]);
                            car = cadena.Length;
                            if (car > 30)
                            {
                                cadena = cadena.Substring(0, 29);
                                oAeronave.MODELO = cadena;
                            }
                            else
                            {
                                oAeronave.MODELO = cadena;
                            }
                            oAeronave.CODIGOOACI = ValidaCampos.ValidaBlancos(dr["CODIGO_OACI"]);
                            oAeronave.USUARIOCREA = ValidaCampos.ValidaBlancos(dr["USUARIOCREA"]);
                            oAeronave.FECHACREA = ValidaCampos.ValidaFechaCreacion(dr["FECHACREA"]);
                            oAeronave.USUARIOMODIFICA = ValidaCampos.ValidaBlancos(dr["USUARIOMODIFICA"]);
                            oAeronave.FECHAMODIFICA = ValidaCampos.ValidaFechaCreacion(dr["FECHAMODIFICA"]);
                            oAeronave.Nombre = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                            Console.WriteLine(oAeronave.MATRICULA + "contador. ." + cont);
                            cont++;
                            lstAeronave.Add(oAeronave);
                        }
                        catch (Exception ex)
                        {


                        }
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstAeronave;
            }
            catch (Exception ex)
            {

            }
            return lstAeronave;
        }
        public static List<DatosGlobales> Compañia()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstUceo = new List<DatosGlobales>();

            string query = "SELECT * FROM COMPANIA WHERE  CAST(FECHACREA AS date) = '" + fecha + "'";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DatosGlobales oUceo = new DatosGlobales();

                        string NOMBRE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                        bool resultado = false;
                        resultado = NOMBRE.Contains("ANTERIOR");

                        if (resultado == false)
                        {

                            oUceo.OID = ValidaCampos.ValidaEntero(dr["OID"]);
                            oUceo.OIDPAIS = ValidaCampos.ValidaEntero(dr["OIDPAIS"]);
                            oUceo.OIDCIUDAD = ValidaCampos.ValidaEntero(dr["OIDCIUDAD"]);
                            oUceo.SIGLAS_IATA = ValidaCampos.ValidaBlancos(dr["SIGLAS_IATA"]);
                            oUceo.CODIGO_OACI = ValidaCampos.ValidaBlancos(dr["CODIGO_OACI"]);

                            oUceo.NOMBRE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);

                            if (oUceo.NOMBRE.ToString().Trim().Length > 99)
                            {
                                oUceo.NOMBRE = oUceo.NOMBRE.ToString().Substring(0, 99);
                            }
                            oUceo.DIRECCION = ValidaCampos.ValidaBlancos(dr["DIRECCION"]);
                            if (oUceo.DIRECCION.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECCION = oUceo.DIRECCION.ToString().Substring(0, 99);
                            }
                            oUceo.REPRESENTANTELEGAL = ValidaCampos.ValidaBlancos(dr["REPRESENTANTELEGAL"]);
                            if (oUceo.REPRESENTANTELEGAL.ToString().Trim().Length > 99)
                            {
                                oUceo.REPRESENTANTELEGAL = oUceo.REPRESENTANTELEGAL.ToString().Substring(0, 99);
                            }
                            oUceo.DIRECCREPRELEGAL = ValidaCampos.ValidaBlancos(dr["DIRECCREPRELEGAL"]);
                            if (oUceo.DIRECCREPRELEGAL.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECCREPRELEGAL = oUceo.DIRECCREPRELEGAL.ToString().Substring(0, 99);
                            }
                            oUceo.TELEFREPRELEGAL = ValidaCampos.ValidaBlancos(dr["TELEFREPRELEGAL"]);
                            oUceo.OIDCIUDADREPLEGAL = ValidaCampos.ValidaEntero(dr["OIDCIUDADREPLEGAL"]);
                            oUceo.REPRESENTANTETECNI = ValidaCampos.ValidaBlancos(dr["REPRESENTANTETECNI"]);
                            oUceo.TELEFONO = ValidaCampos.ValidaBlancos(dr["TELEFONO"]);
                            if (oUceo.TELEFONO.ToString().Trim().Length > 20)
                            {
                                oUceo.TELEFONO = oUceo.TELEFONO.ToString().Substring(0, 19);
                            }
                            oUceo.FAX = ValidaCampos.ValidaBlancos(dr["FAX"]);

                            if (oUceo.FAX.ToString().Trim().Length > 20)
                            {
                                oUceo.FAX = oUceo.FAX.ToString().Substring(0, 19);
                            }
                            oUceo.EMAIL = ValidaCampos.ValidaBlancos(dr["EMAIL"]);
                            oUceo.CUPO = ValidaCampos.ValidaEntero(dr["CUPO"]);
                            oUceo.CARGOREPRESENLEGAL = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENLEGAL"]);
                            oUceo.LISTAPRECIOS = ValidaCampos.ValidaEntero(dr["LISTAPRECIOS"]);
                            oUceo.VIGENCIACOA = ValidaCampos.ValidaBlancos(dr["VIGENCIACOA"]);
                            oUceo.CARGOREPRESENTECNI = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENTECNI"]);
                            oUceo.DIASCREDITO = ValidaCampos.ValidaEntero(dr["DIASCREDITO"]);
                            oUceo.REPRESENTANTEPERMI = ValidaCampos.ValidaBlancos(dr["REPRESENTANTEPERMI"]);
                            oUceo.OIDCTACONT = ValidaCampos.ValidaEntero(dr["OIDCTACONT"]);
                            oUceo.CUENTACONTABLE = ValidaCampos.ValidaBlancos(dr["CUENTACONTABLE"]);
                            oUceo.CARGOREPRESENPERMI = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENPERMI"]);
                            oUceo.COPIAS = ValidaCampos.ValidaEntero(dr["COPIAS"]);
                            oUceo.NUMEROPAGOS = ValidaCampos.ValidaEntero(dr["NUMEROPAGOS"]);
                            oUceo.IVA = ValidaCampos.ValidaDecimal(dr["IVA"]);
                            oUceo.RETENCIONIVA = ValidaCampos.ValidaDecimal(dr["RETENCIONIVA"]);
                            oUceo.RETENCIONFUENTE = ValidaCampos.ValidaDecimal(dr["RETENCIONFUENTE"]);
                            oUceo.NACIONAL_EXTRANJER = ValidaCampos.ValidaBlancos(dr["NACIONAL_EXTRANJER"]);
                            oUceo.TIPOOBJETO = ValidaCampos.ValidaBlancos(dr["TIPOOBJETO"]);
                            oUceo.TIPOCOMPANIA = ValidaCampos.ValidaBlancos(dr["TIPOCOMPANIA"]);
                            oUceo.USUARIOCREA = ValidaCampos.ValidaBlancos(dr["USUARIOCREA"]);
                            oUceo.FECHACREA = ValidaCampos.ValidaFechaCreacion(dr["FECHACREA"]);
                            oUceo.USUARIOMODIFICA = ValidaCampos.ValidaBlancos(dr["USUARIOMODIFICA"]);
                            oUceo.FECHAMODIFICA = ValidaCampos.ValidaFechaCreacion(dr["FECHAMODIFICA"]);
                            oUceo.OIDZONA = ValidaCampos.ValidaEntero(dr["OIDZONA"]);
                            oUceo.PROPIETARIO = ValidaCampos.ValidaBlancos(dr["PROPIETARIO"]);
                            oUceo.OIDCTACONTANT = ValidaCampos.ValidaEntero(dr["OIDCTACONTANT"]);
                            oUceo.EMAILREPRELEGAL = ValidaCampos.ValidaBlancos(dr["EMAILREPRELEGAL"]);
                            oUceo.FAXREPRELEGAL = ValidaCampos.ValidaBlancos(dr["FAXREPRELEGAL"]);
                            oUceo.DIRECREPRETECNI = ValidaCampos.ValidaBlancos(dr["DIRECREPRETECNI"]);
                            if (oUceo.DIRECREPRETECNI.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECREPRETECNI = oUceo.DIRECREPRETECNI.ToString().Substring(0, 99);
                            }
                            oUceo.TELFREPRETECNI = ValidaCampos.ValidaBlancos(dr["TELFREPRETECNI"]);
                            oUceo.FAXREPRETECNI = ValidaCampos.ValidaBlancos(dr["FAXREPRETECNI"]);
                            oUceo.OIDCIUDADREPTECNI = ValidaCampos.ValidaEntero(dr["OIDCIUDADREPTECNI"]);
                            oUceo.RUC = ValidaCampos.ValidaBlancos(dr["RUC"]);
                            oUceo.AFTN = ValidaCampos.ValidaBlancos(dr["AFTN"]);
                            oUceo.NOMBRECOMERCIAL = ValidaCampos.ValidaBlancos(dr["NOMBRECOMERCIAL"]);
                            oUceo.OBSERVACION = ValidaCampos.ValidaBlancos(dr["OBSERVACION"]);
                            if (oUceo.OBSERVACION.ToString().Trim().Length > 99)
                            {
                                oUceo.OBSERVACION = oUceo.OBSERVACION.ToString().Substring(0, 99);
                            }
                            else
                            {
                                //oUceo.OBSERVACION = dr["OBSERVACION"].ToString();
                            }
                            // oUceo.CODEMPRESA = dr["CODEMPRESA"].ToString();
                            oUceo.CODEMPRESA = ValidaCampos.ValidaBlancos(dr["CODEMPRESA"]);
                            lstUceo.Add(oUceo);
                        }
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstUceo;
            }
            catch (Exception ex)
            {


            }
            return lstUceo;
        }
        //compañia modificada
        public static List<DatosGlobales> CompañiaModificada()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstUceo = new List<DatosGlobales>();

            string query = "SELECT * FROM COMPANIA WHERE  CAST(FECHAMODIFICA AS date) = '" + fecha + "'";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DatosGlobales oUceo = new DatosGlobales();

                        string NOMBRE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                        bool resultado = false;
                        resultado = NOMBRE.Contains("ANTERIOR");

                        if (resultado == false)
                        {

                            oUceo.OID = ValidaCampos.ValidaEntero(dr["OID"]);
                            oUceo.OIDPAIS = ValidaCampos.ValidaEntero(dr["OIDPAIS"]);
                            oUceo.OIDCIUDAD = ValidaCampos.ValidaEntero(dr["OIDCIUDAD"]);
                            oUceo.SIGLAS_IATA = ValidaCampos.ValidaBlancos(dr["SIGLAS_IATA"]);
                            oUceo.CODIGO_OACI = ValidaCampos.ValidaBlancos(dr["CODIGO_OACI"]);

                            oUceo.NOMBRE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);

                            if (oUceo.NOMBRE.ToString().Trim().Length > 99)
                            {
                                oUceo.NOMBRE = oUceo.NOMBRE.ToString().Substring(0, 99);
                            }
                            oUceo.DIRECCION = ValidaCampos.ValidaBlancos(dr["DIRECCION"]);
                            if (oUceo.DIRECCION.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECCION = oUceo.DIRECCION.ToString().Substring(0, 99);
                            }
                            oUceo.REPRESENTANTELEGAL = ValidaCampos.ValidaBlancos(dr["REPRESENTANTELEGAL"]);
                            if (oUceo.REPRESENTANTELEGAL.ToString().Trim().Length > 99)
                            {
                                oUceo.REPRESENTANTELEGAL = oUceo.REPRESENTANTELEGAL.ToString().Substring(0, 99);
                            }
                            oUceo.DIRECCREPRELEGAL = ValidaCampos.ValidaBlancos(dr["DIRECCREPRELEGAL"]);
                            if (oUceo.DIRECCREPRELEGAL.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECCREPRELEGAL = oUceo.DIRECCREPRELEGAL.ToString().Substring(0, 99);
                            }
                            oUceo.TELEFREPRELEGAL = ValidaCampos.ValidaBlancos(dr["TELEFREPRELEGAL"]);
                            oUceo.OIDCIUDADREPLEGAL = ValidaCampos.ValidaEntero(dr["OIDCIUDADREPLEGAL"]);
                            oUceo.REPRESENTANTETECNI = ValidaCampos.ValidaBlancos(dr["REPRESENTANTETECNI"]);
                            oUceo.TELEFONO = ValidaCampos.ValidaBlancos(dr["TELEFONO"]);
                            if (oUceo.TELEFONO.ToString().Trim().Length > 20)
                            {
                                oUceo.TELEFONO = oUceo.TELEFONO.ToString().Substring(0, 19);
                            }
                            oUceo.FAX = ValidaCampos.ValidaBlancos(dr["FAX"]);

                            if (oUceo.FAX.ToString().Trim().Length > 20)
                            {
                                oUceo.FAX = oUceo.FAX.ToString().Substring(0, 19);
                            }
                            oUceo.EMAIL = ValidaCampos.ValidaBlancos(dr["EMAIL"]);
                            oUceo.CUPO = ValidaCampos.ValidaEntero(dr["CUPO"]);
                            oUceo.CARGOREPRESENLEGAL = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENLEGAL"]);
                            oUceo.LISTAPRECIOS = ValidaCampos.ValidaEntero(dr["LISTAPRECIOS"]);
                            oUceo.VIGENCIACOA = ValidaCampos.ValidaBlancos(dr["VIGENCIACOA"]);
                            oUceo.CARGOREPRESENTECNI = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENTECNI"]);
                            oUceo.DIASCREDITO = ValidaCampos.ValidaEntero(dr["DIASCREDITO"]);
                            oUceo.REPRESENTANTEPERMI = ValidaCampos.ValidaBlancos(dr["REPRESENTANTEPERMI"]);
                            oUceo.OIDCTACONT = ValidaCampos.ValidaEntero(dr["OIDCTACONT"]);
                            oUceo.CUENTACONTABLE = ValidaCampos.ValidaBlancos(dr["CUENTACONTABLE"]);
                            oUceo.CARGOREPRESENPERMI = ValidaCampos.ValidaBlancos(dr["CARGOREPRESENPERMI"]);
                            oUceo.COPIAS = ValidaCampos.ValidaEntero(dr["COPIAS"]);
                            oUceo.NUMEROPAGOS = ValidaCampos.ValidaEntero(dr["NUMEROPAGOS"]);
                            oUceo.IVA = ValidaCampos.ValidaDecimal(dr["IVA"]);
                            oUceo.RETENCIONIVA = ValidaCampos.ValidaDecimal(dr["RETENCIONIVA"]);
                            oUceo.RETENCIONFUENTE = ValidaCampos.ValidaDecimal(dr["RETENCIONFUENTE"]);
                            oUceo.NACIONAL_EXTRANJER = ValidaCampos.ValidaBlancos(dr["NACIONAL_EXTRANJER"]);
                            oUceo.TIPOOBJETO = ValidaCampos.ValidaBlancos(dr["TIPOOBJETO"]);
                            oUceo.TIPOCOMPANIA = ValidaCampos.ValidaBlancos(dr["TIPOCOMPANIA"]);
                            oUceo.USUARIOCREA = ValidaCampos.ValidaBlancos(dr["USUARIOCREA"]);
                            oUceo.FECHACREA = ValidaCampos.ValidaFechaCreacion(dr["FECHACREA"]);
                            oUceo.USUARIOMODIFICA = ValidaCampos.ValidaBlancos(dr["USUARIOMODIFICA"]);
                            oUceo.FECHAMODIFICA = ValidaCampos.ValidaFechaCreacion(dr["FECHAMODIFICA"]);
                            oUceo.OIDZONA = ValidaCampos.ValidaEntero(dr["OIDZONA"]);
                            oUceo.PROPIETARIO = ValidaCampos.ValidaBlancos(dr["PROPIETARIO"]);
                            oUceo.OIDCTACONTANT = ValidaCampos.ValidaEntero(dr["OIDCTACONTANT"]);
                            oUceo.EMAILREPRELEGAL = ValidaCampos.ValidaBlancos(dr["EMAILREPRELEGAL"]);
                            oUceo.FAXREPRELEGAL = ValidaCampos.ValidaBlancos(dr["FAXREPRELEGAL"]);
                            oUceo.DIRECREPRETECNI = ValidaCampos.ValidaBlancos(dr["DIRECREPRETECNI"]);
                            if (oUceo.DIRECREPRETECNI.ToString().Trim().Length > 99)
                            {
                                oUceo.DIRECREPRETECNI = oUceo.DIRECREPRETECNI.ToString().Substring(0, 99);
                            }
                            oUceo.TELFREPRETECNI = ValidaCampos.ValidaBlancos(dr["TELFREPRETECNI"]);
                            oUceo.FAXREPRETECNI = ValidaCampos.ValidaBlancos(dr["FAXREPRETECNI"]);
                            oUceo.OIDCIUDADREPTECNI = ValidaCampos.ValidaEntero(dr["OIDCIUDADREPTECNI"]);
                            oUceo.RUC = ValidaCampos.ValidaBlancos(dr["RUC"]);
                            oUceo.AFTN = ValidaCampos.ValidaBlancos(dr["AFTN"]);
                            oUceo.NOMBRECOMERCIAL = ValidaCampos.ValidaBlancos(dr["NOMBRECOMERCIAL"]);
                            oUceo.OBSERVACION = ValidaCampos.ValidaBlancos(dr["OBSERVACION"]);
                            if (oUceo.OBSERVACION.ToString().Trim().Length > 99)
                            {
                                oUceo.OBSERVACION = oUceo.OBSERVACION.ToString().Substring(0, 99);
                            }
                            else
                            {
                                //oUceo.OBSERVACION = dr["OBSERVACION"].ToString();
                            }
                            // oUceo.CODEMPRESA = dr["CODEMPRESA"].ToString();
                            oUceo.CODEMPRESA = ValidaCampos.ValidaBlancos(dr["CODEMPRESA"]);
                            lstUceo.Add(oUceo);
                        }
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstUceo;
            }
            catch (Exception ex)
            {


            }
            return lstUceo;
        }
        //clientes
        //cliente facturacion
        public static List<DatosGlobales> ClienteFacturas()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstUceo = new List<DatosGlobales>();
            string query = "SELECT *FROM CLIENTEFR3  WHERE NOMBRE not like ('%(ANTERIOR)%')  and (CAST(FECHACREA AS date) = '" + fecha + "' OR CAST(FECHAMODIFICA AS date) = '" + fecha + "')";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd.CommandTimeout = 3600;
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DatosGlobales oUceo = new DatosGlobales();
                        string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "-", "%" };

                        oUceo.CEDULA_RUC = ValidaCampos.ValidaBlancos(dr["CLIENTEID"]);

                        oUceo.NOMBRECLIENTE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                        if (oUceo.NOMBRECLIENTE.ToString().Trim().Length > 99)
                        {

                            oUceo.NOMBRECLIENTE = oUceo.NOMBRECLIENTE.Substring(0, 99);
                        }

                        foreach (var c in charsToRemove)
                        {
                            oUceo.NOMBRECLIENTE = oUceo.NOMBRECLIENTE.Replace(c, string.Empty);
                        }



                        oUceo.DIRECCION = ValidaCampos.ValidaBlancos(dr["DIRECCION"]);
                        if (oUceo.DIRECCION.ToString().Trim().Length > 99)
                        {

                            oUceo.DIRECCION = oUceo.DIRECCION.Substring(0, 99);
                        }


                        foreach (var c in charsToRemove)
                        {
                            oUceo.DIRECCION = oUceo.DIRECCION.Replace(c, string.Empty);
                        }


                        oUceo.TELEFONO = ValidaCampos.ValidaBlancos(dr["TELEFONO"]);
                        if (oUceo.TELEFONO.ToString().Trim().Length > 19)
                        {

                            oUceo.TELEFONO = oUceo.TELEFONO.Substring(0, 19);
                        }
                        oUceo.TABLA = ValidaCampos.ValidaBlancos(dr["TABLA"]);
                        oUceo.NIVEL = ValidaCampos.ValidaBlancos(dr["NIVEL"]);

                        lstUceo.Add(oUceo);
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstUceo;
            }
            catch (Exception ex)
            {

            }
            return lstUceo;
        }
        //cliente modificado
        public static List<DatosGlobales> ClienteFacturasModificado()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstUceo = new List<DatosGlobales>();
            string query = "SELECT *FROM CLIENTEFR3  WHERE NOMBRE not like ('%(ANTERIOR)%')  and CAST(FECHAMODIFICA AS date) = '" + fecha + "'";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd.CommandTimeout = 3600;
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DatosGlobales oUceo = new DatosGlobales();
                        string[] charsToRemove = new string[] { "@", ",", ".", ";", "'", "-", "%" };

                        oUceo.CEDULA_RUC = ValidaCampos.ValidaBlancos(dr["CLIENTEID"]);

                        oUceo.NOMBRECLIENTE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                        if (oUceo.NOMBRECLIENTE.ToString().Trim().Length > 99)
                        {

                            oUceo.NOMBRECLIENTE = oUceo.NOMBRECLIENTE.Substring(0, 99);
                        }

                        foreach (var c in charsToRemove)
                        {
                            oUceo.NOMBRECLIENTE = oUceo.NOMBRECLIENTE.Replace(c, string.Empty);
                        }



                        oUceo.DIRECCION = ValidaCampos.ValidaBlancos(dr["DIRECCION"]);
                        if (oUceo.DIRECCION.ToString().Trim().Length > 99)
                        {

                            oUceo.DIRECCION = oUceo.DIRECCION.Substring(0, 99);
                        }


                        foreach (var c in charsToRemove)
                        {
                            oUceo.DIRECCION = oUceo.DIRECCION.Replace(c, string.Empty);
                        }


                        oUceo.TELEFONO = ValidaCampos.ValidaBlancos(dr["TELEFONO"]);
                        if (oUceo.TELEFONO.ToString().Trim().Length > 19)
                        {

                            oUceo.TELEFONO = oUceo.TELEFONO.Substring(0, 19);
                        }
                        oUceo.TABLA = ValidaCampos.ValidaBlancos(dr["TABLA"]);
                        oUceo.NIVEL = ValidaCampos.ValidaBlancos(dr["NIVEL"]);

                        lstUceo.Add(oUceo);
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstUceo;
            }
            catch (Exception ex)
            {

            }
            return lstUceo;
        }

        //ciudad creada
        public static List<DatosGlobales> CiudadCreada()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd").ToUpper();
            List<DatosGlobales> lstUceo = new List<DatosGlobales>();
            string query = "SELECT *FROM CIUDAD  WHERE  (CAST(FECHACREA AS date) = '" + fecha + "' OR CAST(FECHAMODIFICA AS date) = '" + fecha + "')";
            OdbcCommand cmd;
            try
            {
                using (OdbcConnection oConexion = new OdbcConnection(ConexionP550.CadenaConexion))
                {
                    cmd = new OdbcCommand(query, oConexion);
                    //cmd.CommandTimeout = 3600;
                    oConexion.Open();
                    OdbcDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DatosGlobales oUceo = new DatosGlobales();
                        oUceo.OID = ValidaCampos.ValidaEntero(dr["OID"]);
                        oUceo.OIDREGION = ValidaCampos.ValidaEntero(dr["OIDREGION"]);
                        oUceo.OIDPROVINCIA = ValidaCampos.ValidaEntero(dr["OIDPROVINCIA"]);
                        oUceo.CODIGO = ValidaCampos.ValidaBlancos(dr["CODIGO"]);
                        oUceo.NOMBRE = ValidaCampos.ValidaBlancos(dr["NOMBRE"]);
                        oUceo.USUARIOCREA = ValidaCampos.ValidaBlancos(dr["USUARIOCREA"]);
                        oUceo.FECHACREA = ValidaCampos.ValidaFechaCreacion(dr["FECHACREA"]);
                        oUceo.USUARIOMODIFICA = ValidaCampos.ValidaBlancos(dr["USUARIOMODIFICA"]);
                        oUceo.FECHAMODIFICA = ValidaCampos.ValidaFechaCreacion(dr["FECHAMODIFICA"]);
                        oUceo.CODIGOIATA = ValidaCampos.ValidaBlancos(dr["CODIGOIATA"]);
                        oUceo.CODIGOOACI = ValidaCampos.ValidaBlancos(dr["CODIGOOACI"]);

                        lstUceo.Add(oUceo);
                    }
                    dr.Close();
                    oConexion.Close();
                }
                return lstUceo;
            }
            catch (Exception ex)
            {

            }
            return lstUceo;
        }
       
    }
}
