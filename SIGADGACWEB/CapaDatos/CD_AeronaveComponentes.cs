using CapaModelo;
using IBM.Data.DB2.iSeries;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_AeronaveComponentes
    {
        public static CD_AeronaveComponentes _instancia = null;
        private CD_AeronaveComponentes()
        {

        }

        public static CD_AeronaveComponentes Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_AeronaveComponentes();
                }
                return _instancia;
            }
        }

        public List<tbAeronavesComponentes> DetalleDocumentos()
        {
            List<tbAeronavesComponentes> listarSolicitud = new List<tbAeronavesComponentes>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            string Codigo = "HC";
            try
            {
                sbSol.Append("SELECT * FROM AERAR1 WHERE SUBSTRING(AERMAT,1,2)='"+Codigo+"'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMAT"].ToString().Trim();
                        oSolicitud.CODIFOOACI = dr["AERCO1"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERFAB"].ToString().Trim();
                        oSolicitud.MODELO = dr["AERMOD"].ToString().Trim();
                        
                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

        //busqueda por maricula
        public List<tbAeronavesComponentes> DetalleDocumentosMatricula(string Matricula)
        {
            List<tbAeronavesComponentes> listarSolicitud = new List<tbAeronavesComponentes>();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            string Codigo = "HC";
            try
            {
                sbSol.Append("SELECT * FROM AERAR1 WHERE AERMAT='" + Matricula + "'");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMAT"].ToString().Trim();
                        oSolicitud.CODIFOOACI = dr["AERCO1"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERFAB"].ToString().Trim();
                        oSolicitud.MODELO = dr["AERMOD"].ToString().Trim();

                        listarSolicitud.Add(oSolicitud);
                    }
                    dr.Close();
                    oConexion.Close();
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

        public tbAeronavesComponentes DetalleDocumentosClave(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbAeronavesComponentes listarSolicitud = new tbAeronavesComponentes();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT  * FROM AERAR9 LEFT JOIN CIAARC01 ON CIACOD=AERC06  LEFT JOIN OPCAR1 ON OPCCO4=AERBA3 WHERE AERMA8 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMA8"].ToString().Trim();
                        oSolicitud.EXPLOTADOR = dr["AERC06"].ToString().Trim()+"/"+ dr["CIANOM"].ToString().Trim();
                        oSolicitud.PROPIETARIO = dr["AERPRO"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERAE1"].ToString().Trim();
                        oSolicitud.MODELO = dr["AERAE2"].ToString().Trim();
                        oSolicitud.SERIE = dr["AERAE3"].ToString().Trim();
                        oSolicitud.AÑOFAB = dr["AERANO"].ToString().Trim();

                        oSolicitud.PESOVACIO = decimal.Parse(dr["AERP05"].ToString());
                        oSolicitud.PMP = decimal.Parse(dr["AERP06"].ToString());
                        oSolicitud.TECHO = decimal.Parse(dr["AERTEC"].ToString());
                        oSolicitud.NUMEROPAX = Int16.Parse(dr["AERN11"].ToString());

                        oSolicitud.PESOVACIODESIGNACION = dr["AERP07"].ToString();
                        oSolicitud.PMPDESIGNACION = dr["AERP08"].ToString();
                        oSolicitud.TECHODESIGNACION = dr["AERTE1"].ToString();

                        oSolicitud.MOTOR1MARCA = dr["AERMOT"].ToString().Trim();
                        oSolicitud.MOTOR2MARCA = dr["AERMO6"].ToString().Trim();
                        oSolicitud.MOTOR1MODELO = dr["AERMO5"].ToString().Trim();
                        oSolicitud.MOTOR2MODELO = dr["AERMO7"].ToString().Trim();
                        oSolicitud.HELICE1MARCA = dr["AERHEL"].ToString().Trim();
                        oSolicitud.HELICE2MARCA = dr["AERHE2"].ToString().Trim();
                        oSolicitud.HELICE1MODELO = dr["AERHE1"].ToString().Trim();
                        oSolicitud.HELICE2MODELO = dr["AERHE3"].ToString().Trim();

                        oSolicitud.ELTMARCA = dr["AEREL3"].ToString().Trim();
                        oSolicitud.ELTMODELO = dr["AEREL4"].ToString().Trim();
                        oSolicitud.ELTSERIE = dr["AEREL5"].ToString().Trim();
                        oSolicitud.ELTCODIGOHEX = dr["AEREL6"].ToString().Trim();

                        oSolicitud.ELTPORTATILMARCA = dr["AEREL8"].ToString().Trim();
                        oSolicitud.ELTPORTATILMODELO = dr["AEREL9"].ToString().Trim();
                        oSolicitud.ELTPORTATILCODIGOHEX = dr["AERE05"].ToString().Trim();

                        oSolicitud.CODIGOMODOSS = dr["AEREL7"].ToString().Trim();
                        oSolicitud.TIPOAPROBACION = dr["AERTI8"].ToString().Trim();

                        //oSolicitud.CONDICION = dr["AERC05"].ToString();
                        string Condicion = dr["AERC05"].ToString();
                        switch (Condicion)
                        {
                            case "0":
                                oSolicitud.CONDICION = "OPERABLE";
                                break;

                            case "1":
                                oSolicitud.CONDICION = "MANTENIMIENTO";
                                break;
                            case "2":
                                oSolicitud.CONDICION = "ACCIDENTADO";
                                break;
                            case "3":
                                oSolicitud.CONDICION = "CANCELADO";
                                break;
                            case "4":
                                oSolicitud.CONDICION = "SALIO DEL PAIS";
                                break;
                            case "5":
                                oSolicitud.CONDICION = "INACTIVO";
                                break;

                            default:
                                break;
                        }

                        string Region = dr["AERR08"].ToString();
                        switch (Region)
                        {
                            case "0":
                                oSolicitud.REGION = "COSTA";
                                break;

                            case "1":
                                oSolicitud.REGION = "SIERRA";
                                break;
                            
                            default:
                                break;
                        }

                        oSolicitud.BASEOPERACION = dr["OPCDES"].ToString().Trim();
                        oSolicitud.FECHAMONITOREORVSM = dr["AERMON"].ToString().Trim();
                        oSolicitud.ERRORASE = dr["AERMO8"].ToString().Trim();
                        oSolicitud.OBSERVACIONES = dr["AEROB7"].ToString().Trim();



                        //LLENA DETALE DE CERTIFICADOS AERONAVEGABILIDAD
                        oSolicitud.oDetalleCertAeronavegabilidad= CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAero(oSolicitud.AERONAVE);
                        //LLENA DETALE DE CERTIFICADOS DE RADIO
                        oSolicitud.oDetalleCertRadio = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRadio(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS DE RUIDO
                        oSolicitud.oDetalleCerHomolRuido = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRuido(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS PBN RNP10
                        oSolicitud.oDetalleCertPbnRnp10 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNP10(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV5
                        oSolicitud.oDetalleCertRnav5 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV5(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV2
                        oSolicitud.oDetalleCertRnav2 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV2(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS APPROACH
                        oSolicitud.oDetalleCertRnavApproach = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAPPROACH(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RVSM
                        oSolicitud.oDetalleCertRvsm = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRVSM(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS ETOPS
                        oSolicitud.oDetalleCertEtops = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoETOPS(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS CAT II/III
                        oSolicitud.oDetalleCategorias = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCategorias(oSolicitud.AERONAVE);

                        //LLENA DETALE DE ACCIDENTE
                        oSolicitud.oDetalleAccidenteAeronave = CD_DetalleCertAeronav.Instancia.DetalleDocumentosAccidente(oSolicitud.AERONAVE);


                        listarSolicitud = oSolicitud;
                    }

                    dr.Close();
                    oConexion.Close();

                }

                return listarSolicitud;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }
        public tbAeronavesComponentes ImprimeDocumentos(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbAeronavesComponentes listarSolicitud = new tbAeronavesComponentes();
            try
            {
                Directory.CreateDirectory(@"C:\Temp");

                string file = @"C:\Temp\test_" + Matricula+ ".pdf";

                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, fs);

                    doc.Open();
                    doc.Add(new Paragraph("PDF OK"));
                    doc.Close();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"C:\Temp\error.txt", ex.ToString());
            }
            return listarSolicitud;
        }
        //IMPRIME PDF
        public tbAeronavesComponentes ImprimeDocumento(string Matricula)
        {
            // string fECHA = DateTime.Now.ToString("yyyyMMdd");
            tbAeronavesComponentes listarSolicitud = new tbAeronavesComponentes();
            StringBuilder sbSol = new StringBuilder();
            string query = string.Empty;
            try
            {
                sbSol.Append("SELECT  * FROM AERAR9 LEFT JOIN CIAARC01 ON CIACOD=AERC06  LEFT JOIN OPCAR1 ON OPCCO4=AERBA3 WHERE AERMA8 = '" + Matricula + "' ");

                query = sbSol.ToString();
                iDB2Command cmd;


                using (iDB2Connection oConexion = new iDB2Connection(ConexionDB2.CadenaConexion))
                {
                    cmd = new iDB2Command(query, oConexion);
                    oConexion.Open();
                    iDB2DataReader dr = cmd.ExecuteReader();



                    while (dr.Read())
                    {
                        tbAeronavesComponentes oSolicitud = new tbAeronavesComponentes();

                        oSolicitud.AERONAVE = dr["AERMA8"].ToString().Trim();
                        oSolicitud.EXPLOTADOR = dr["AERC06"].ToString().Trim() + "/" + dr["CIANOM"].ToString().Trim();
                        oSolicitud.PROPIETARIO = dr["AERPRO"].ToString().Trim();
                        oSolicitud.MARCA = dr["AERAE1"].ToString().Trim();
                        oSolicitud.MODELO = dr["AERAE2"].ToString().Trim();
                        oSolicitud.SERIE = dr["AERAE3"].ToString().Trim();
                        oSolicitud.AÑOFAB = dr["AERANO"].ToString().Trim();

                        oSolicitud.PESOVACIO = decimal.Parse(dr["AERP05"].ToString());
                        oSolicitud.PMP = decimal.Parse(dr["AERP06"].ToString());
                        oSolicitud.TECHO = decimal.Parse(dr["AERTEC"].ToString());
                        oSolicitud.NUMEROPAX = Int16.Parse(dr["AERN11"].ToString());

                        oSolicitud.PESOVACIODESIGNACION = dr["AERP07"].ToString().Trim();
                        oSolicitud.PMPDESIGNACION = dr["AERP08"].ToString().Trim();
                        oSolicitud.TECHODESIGNACION = dr["AERTE1"].ToString().Trim();

                        oSolicitud.MOTOR1MARCA = dr["AERMOT"].ToString().Trim();
                        oSolicitud.MOTOR2MARCA = dr["AERMO6"].ToString().Trim();
                        oSolicitud.MOTOR1MODELO = dr["AERMO5"].ToString().Trim();
                        oSolicitud.MOTOR2MODELO = dr["AERMO7"].ToString().Trim();
                        oSolicitud.HELICE1MARCA = dr["AERHEL"].ToString().Trim();
                        oSolicitud.HELICE2MARCA = dr["AERHE2"].ToString().Trim();
                        oSolicitud.HELICE1MODELO = dr["AERHE1"].ToString().Trim();
                        oSolicitud.HELICE2MODELO = dr["AERHE3"].ToString().Trim();

                        oSolicitud.ELTMARCA = dr["AEREL3"].ToString().Trim();
                        oSolicitud.ELTMODELO = dr["AEREL4"].ToString().Trim();
                        oSolicitud.ELTSERIE = dr["AEREL5"].ToString().Trim();
                        oSolicitud.ELTCODIGOHEX = dr["AEREL6"].ToString().Trim();

                        oSolicitud.ELTPORTATILMARCA = dr["AEREL8"].ToString().Trim();
                        oSolicitud.ELTPORTATILMODELO = dr["AEREL9"].ToString().Trim();
                        oSolicitud.ELTPORTATILCODIGOHEX = dr["AERE05"].ToString().Trim();

                        oSolicitud.CODIGOMODOSS = dr["AEREL7"].ToString().Trim();
                        oSolicitud.TIPOAPROBACION = dr["AERTI8"].ToString().Trim();

                        //oSolicitud.CONDICION = dr["AERC05"].ToString();
                        string Condicion = dr["AERC05"].ToString();
                        switch (Condicion)
                        {
                            case "0":
                                oSolicitud.CONDICION = "OPERABLE";
                                break;

                            case "1":
                                oSolicitud.CONDICION = "MANTENIMIENTO";
                                break;
                            case "2":
                                oSolicitud.CONDICION = "ACCIDENTADO";
                                break;
                            case "3":
                                oSolicitud.CONDICION = "CANCELADO";
                                break;
                            case "4":
                                oSolicitud.CONDICION = "SALIO DEL PAIS";
                                break;
                            case "5":
                                oSolicitud.CONDICION = "INACTIVO";
                                break;

                            default:
                                break;
                        }

                        string Region = dr["AERR08"].ToString();
                        switch (Region)
                        {
                            case "0":
                                oSolicitud.REGION = "COSTA";
                                break;

                            case "1":
                                oSolicitud.REGION = "SIERRA";
                                break;

                            default:
                                break;
                        }

                        oSolicitud.BASEOPERACION = dr["OPCDES"].ToString().Trim();
                        oSolicitud.FECHAMONITOREORVSM = dr["AERMON"].ToString().Trim();
                        oSolicitud.ERRORASE = dr["AERMO8"].ToString().Trim();
                        oSolicitud.OBSERVACIONES = dr["AEROB7"].ToString().Trim();



                        //LLENA DETALE DE CERTIFICADOS AERONAVEGABILIDAD
                     var ListaCertAero=   oSolicitud.oDetalleCertAeronavegabilidad = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAero(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS DE RADIO
                        oSolicitud.oDetalleCertRadio = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRadio(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS DE RUIDO
                        oSolicitud.oDetalleCerHomolRuido = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRuido(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS PBN RNP10
                        oSolicitud.oDetalleCertPbnRnp10 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNP10(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV5
                        oSolicitud.oDetalleCertRnav5 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV5(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RNAV2
                        oSolicitud.oDetalleCertRnav2 = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRNAV2(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS APPROACH
                        oSolicitud.oDetalleCertRnavApproach = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoAPPROACH(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS RVSM
                        oSolicitud.oDetalleCertRvsm = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoRVSM(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS ETOPS
                        oSolicitud.oDetalleCertEtops = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCertificadoETOPS(oSolicitud.AERONAVE);

                        //LLENA DETALE DE CERTIFICADOS CAT II/III
                        oSolicitud.oDetalleCategorias = CD_DetalleCertAeronav.Instancia.DetalleDocumentosCategorias(oSolicitud.AERONAVE);

                        //LLENA DETALE DE ACCIDENTE
                        oSolicitud.oDetalleAccidenteAeronave = CD_DetalleCertAeronav.Instancia.DetalleDocumentosAccidente(oSolicitud.AERONAVE);

                        //crea pdf
                        
                        string path = @"\\172.20.19.55\Aeronaves\";
                        //string path = @"\\172.20.19.55\Aeronaves\aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf";
                        //string path = (@"C:\Aeronaves\");

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string patharchivo = (@"\\172.20.19.55\Aeronaves\" + "aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf");

                        if (File.Exists(patharchivo))
                        {
                            File.Delete(patharchivo);
                        }


                       
                        //string patharchivo = (@"C:\Aeronaves\" + "aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf");

                        //string nombrearchivo = (model.LUGAR.Trim() + "fecha" + model.FECHAELABORACION.Trim() + "dependencia" + model.DEPENDENCIA.Trim() +
                        //    "turno" + model.TURNO.Trim() + ".pdf");

                        string nombrearchivo = ("aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf");

                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER);
                        // Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                        // Indicamos donde vamos a guardar 
                        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"\\172.20.19.55\Aeronaves\" + "aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf", FileMode.Create));

                        // PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Aeronaves\" + "aeronave_" + oSolicitud.AERONAVE.Trim() + ".pdf", FileMode.Create));


                        using (FileStream fs = new FileStream(patharchivo, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (doc)
                            {
                                PdfWriter writer = PdfWriter.GetInstance(doc, fs);


                                // Abrimos el archivo
                                doc.Open();

                                // Colores tipo institucional (puedes ajustar según manual DGAC)
                                BaseColor azulDGAC1 = new BaseColor(0, 70, 127);     // Azul principal
                                BaseColor azulClaro1 = new BaseColor(100, 150, 200); // Opcional

                                BaseColor azulDGAC = new BaseColor(56, 141, 186);
                                BaseColor azulClaro = new BaseColor(180, 210, 230);


                                iTextSharp.text.Font ValoresTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.MAGENTA);
                                iTextSharp.text.Font Valores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                iTextSharp.text.Font _standardFontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                iTextSharp.text.Font Titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                                iTextSharp.text.Font TitulosValor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                iTextSharp.text.Font Dgac = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                                iTextSharp.text.Font Dgac1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                                iTextSharp.text.Font TituloAto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                iTextSharp.text.Font Texto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                                iTextSharp.text.Font Total = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                                iTextSharp.text.Font TotalCabecera = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.RED);
                                iTextSharp.text.Font Cabecera = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLUE);
                                // Creamos la imagen y le ajustamos el tamaño

                                Font titulo = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.WHITE);
                                Font tituloBlanco = new Font(Titulos.BaseFont, 10, Font.BOLD, BaseColor.WHITE);


                                //doc.Add(tblCodigo);

                                #region Titulo


                                string aeronaveTexto = string.Empty;
                                var tablaGeneral = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1 };
                                var tablaprincipal = new PdfPTable(new float[] { 10f, 89f }) { WidthPercentage = 100f, HorizontalAlignment = 1 };
                                var tblimagen = new PdfPTable(new float[] { 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1 };
                                var tblTitulo = new PdfPTable(new float[] { 50f, 20f }) { WidthPercentage = 100f, HorizontalAlignment = 1 };
                                tablaprincipal.DefaultCell.Border = 0;
                                tblimagen.DefaultCell.Border = 0;

                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };

                                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"D:/proyectos c#/Informe Transito Aereo/Informe Transito Aereo/Anexos/DGAC.jpg");
                                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"\\172.20.19.55\Aeronaves\LogoDacNuevo.jpg");
                                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"D:/proyectos c#/Informe Transito Aereo/Informe Transito Aereo/Anexos/LogoDacNuevo.jpg");

                                imagen.BorderWidth = 0;
                                imagen.Alignment = Element.ALIGN_LEFT;
                                //float percentage = 0.0f;
                                //percentage = 150 / imagen.Width;
                                //imagen.ScalePercent(percentage * 100);
                                //celAeron.BorderWidth = 0;
                                imagen.ScaleAbsoluteWidth(20);
                                imagen.ScaleAbsoluteHeight(40);
                                imagen.ScaleAbsolute(40, 34);
                                imagen.ScalePercent(50);
                                tblimagen.AddCell(imagen);


                                var celAeron = new PdfPCell(new Phrase("DIRECCIÓN GENERAL DE", Dgac));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BorderWidth = 0;

                                tblTitulo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSTRUCCIÓN TÉCNICA ", Dgac1));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BorderWidth = 0;

                                tblTitulo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("AVIACION CIVIL", Dgac));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BorderWidth = 0;

                                tblTitulo.AddCell(celAeron);

                                //celAeron = new PdfPCell(new Phrase("ITS", Dgac1));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BorderWidth = 0;

                                //tblTitulo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INFORME TÉCNICO DE AERONAVE", Dgac));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BorderWidth = 0;

                                tblTitulo.AddCell(celAeron);

                                var celAeron1 = new PdfPCell(new Phrase("", Dgac1));
                                celAeron1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron1.BorderWidth = 0;
                                tblTitulo.AddCell(celAeron1);

                                //celAeron = new PdfPCell(new Phrase("TRÁNSITO AÉREO ITS", Dgac));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BorderWidth = 0;

                                //tblTitulo.AddCell(celAeron);

                                //celAeron1 = new PdfPCell(new Phrase("PTA-RG-07", Dgac1));
                                //celAeron1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron1.BorderWidth = 0;
                                //tblTitulo.AddCell(celAeron1);


                                tablaprincipal.AddCell(tblimagen);
                                tablaprincipal.AddCell(tblTitulo);
                                tablaprincipal.SpacingAfter = 2f;
                                // tablaGeneral.AddCell(tablaprincipal);
                                doc.Add(tablaprincipal);
                                // doc.Add(tablaGeneral);

                                #endregion



                                //datos cabecera
                                #region tablacabecera


                                aeronaveTexto = string.Empty;
                                //TITULO CABECERA
                                BaseColor azulLinea = new BaseColor(100, 150, 200);

                                Font titulo1 = new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.WHITE);
                                var tblTituloCabecera = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 12f,
                                    SpacingAfter = 6f
                                };


                                // Fuente más visible
                                Font tituloBlanco1 = new Font(Titulos.BaseFont, 12, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("INFORMACIÓN GENERAL DE LA AERONAVE", titulo1));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC1;
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 6f;
                                celAeron.PaddingBottom = 6f;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 8f;
                                celAeron.PaddingBottom = 8f;

                                tblTituloCabecera.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                //PdfPCell linea = new PdfPCell(new Phrase(""))
                                //{
                                //    BackgroundColor = azulClaro,
                                //    FixedHeight = 2f,
                                //    Border = Rectangle.NO_BORDER
                                //};
                                //tblTituloCabecera.AddCell(linea);

                                // Línea decorativa fina
                                PdfPCell linea = new PdfPCell()
                                {
                                    BackgroundColor = azulLinea,
                                    FixedHeight = 2f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCabecera.AddCell(linea);


                                doc.Add(tblTituloCabecera);


                                //var tblTituloCabecera = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("INFORMACIÓN GENERAL DE LA AERONAVE", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCabecera.AddCell(celAeron);
                                //doc.Add(tblTituloCabecera);


                                var tblItsCabecera = new PdfPTable(new float[] { 30f, 35f, 35f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("AERONAVE", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblItsCabecera.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("EXPLOTADOR", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblItsCabecera.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("PROPIETARIO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblItsCabecera.AddCell(celAeron);



                                // doc.Add(tblItsCabecera);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.AERONAVE.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblItsCabecera.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.EXPLOTADOR.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblItsCabecera.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.PROPIETARIO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblItsCabecera.AddCell(celAeron);

                                // tablaGeneral.AddCell(tblItsCabecera);
                                doc.Add(tblItsCabecera);
                                //doc.Add(tablaGeneral);

                                //MARCA
                                var tblMarca = new PdfPTable(new float[] { 30f, 30f, 20f, 20f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("SERIE", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("AÑO FAB", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMarca.AddCell(celAeron);



                                // doc.Add(tblItsCabecera);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.SERIE.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMarca.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.AÑOFAB.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMarca.AddCell(celAeron);

                                // tablaGeneral.AddCell(tblItsCabecera);
                                doc.Add(tblMarca);
                                //doc.Add(tablaGeneral);
                                #endregion

                                #region PESOS Y PERFORMANCE

                                //TITULO CABECERA

                                var tblTitulopeso = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 8f,
                                    SpacingAfter = 5f
                                };

                                // Fuente más equilibrada
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("PESOS Y PERFORMANCE", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estilo limpio
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTitulopeso.AddCell(celAeron);

                                // Línea decorativa más sutil
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1.5f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTitulopeso.AddCell(linea);

                                doc.Add(tblTitulopeso);

                                //var tblTitulopeso = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("PESOS Y PERFORMANCE", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTitulopeso.AddCell(celAeron);
                                //doc.Add(tblTitulopeso);

                                var tblPeso = new PdfPTable(new float[] { 25f, 10f, 20f, 10f, 25f, 10f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("PESO VACIO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("UNIDAD", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("PMP", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("UNIDAD", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);


                                celAeron = new PdfPCell(new Phrase("TECHO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("m / pies", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(Convert.ToString(oSolicitud.PESOVACIO).Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(Convert.ToString(oSolicitud.PESOVACIODESIGNACION).Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(Convert.ToString(oSolicitud.PMP).Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.PMPDESIGNACION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(Convert.ToString(oSolicitud.TECHO).Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.TECHODESIGNACION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblPeso.AddCell(celAeron);

                                doc.Add(tblPeso);
                                #endregion
                                #region MOTORES
                                //TITULO MOTORES 1


                                var tblTituloMotor = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 8f,
                                    SpacingAfter = 5f
                                };

                                // Fuente más equilibrada
                                //  tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("MOTOR 1", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estilo limpio
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloMotor.AddCell(celAeron);

                                // Línea decorativa más sutil
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloMotor.AddCell(linea);

                                doc.Add(tblTituloMotor);

                                //var tblTituloMotor = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("MOTOR 1", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloMotor.AddCell(celAeron);
                                //doc.Add(tblTituloMotor);

                                var tblMotor1 = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMotor1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMotor1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MOTOR1MARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMotor1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MOTOR1MODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMotor1.AddCell(celAeron);

                                doc.Add(tblMotor1);

                                //TITULO MOTORES 2

                                var tblTituloMotor2 = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //   tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("MOTOR 2", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloMotor2.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloMotor2.AddCell(linea);

                                doc.Add(tblTituloMotor2);

                                //var tblTituloMotor2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("MOTOR 2", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloMotor2.AddCell(celAeron);
                                //doc.Add(tblTituloMotor2);

                                var tblMotor2 = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMotor2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMotor2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MOTOR2MARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMotor2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.MOTOR2MODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMotor2.AddCell(celAeron);

                                doc.Add(tblMotor2);
                                //HELICE
                                //TITULO HELICE 1
                                var tblTituloHelice = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("HELICE 1", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloHelice.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloHelice.AddCell(linea);

                                doc.Add(tblTituloHelice);

                                //var tblTituloHelice = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("HELICE 1", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloHelice.AddCell(celAeron);
                                //doc.Add(tblTituloHelice);

                                var tblHelice1 = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblHelice1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblHelice1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.HELICE1MARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblHelice1.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.HELICE1MODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblHelice1.AddCell(celAeron);

                                doc.Add(tblHelice1);

                                //TITULO HELICE 2
                                var tblTituloHelice2 = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("HELICE 2", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloHelice2.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloHelice2.AddCell(linea);

                                doc.Add(tblTituloHelice2);

                                //var tblTituloHelice2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("HELICE 2", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloHelice2.AddCell(celAeron);
                                //doc.Add(tblTituloHelice2);

                                var tblHelice2 = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblHelice2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblHelice2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.HELICE2MARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblHelice2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.HELICE2MODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblHelice2.AddCell(celAeron);

                                doc.Add(tblHelice2);

                                #endregion

                                #region ELTFIJO
                                //TITULO ELT FIJO
                                var tblTituloEltFijo = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("ELT FIJO", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloEltFijo.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloEltFijo.AddCell(linea);

                                doc.Add(tblTituloEltFijo);

                                //var tblTituloEltFijo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("ELT FIJO ", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloEltFijo.AddCell(celAeron);
                                //doc.Add(tblTituloEltFijo);

                                var tblEltFijo = new PdfPTable(new float[] { 25f, 25f, 25f, 25f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("SERIE", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("CODIGO HEXADECIMAL", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTMARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTMODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTSERIE.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltFijo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTCODIGOHEX.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltFijo.AddCell(celAeron);
                                doc.Add(tblEltFijo);

                                //elt portatil
                                //TITULO ELT PORTATIL
                                var tblTituloEltPortatil = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //  tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("ELT PORTATIL", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloEltPortatil.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloEltPortatil.AddCell(linea);

                                doc.Add(tblTituloEltPortatil);

                                //var tblTituloEltPortatil = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore =5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("ELT PORTATIL", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloEltPortatil.AddCell(celAeron);
                                //doc.Add(tblTituloEltPortatil);

                                var tblEltPortatil = new PdfPTable(new float[] { 33f, 33f, 34f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("MARCA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltPortatil.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("MODELO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltPortatil.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("CODIGO HEXADECIMAL", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblEltPortatil.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTPORTATILMARCA.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltPortatil.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTPORTATILMODELO.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltPortatil.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ELTPORTATILCODIGOHEX.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblEltPortatil.AddCell(celAeron);
                                doc.Add(tblEltPortatil);

                                #endregion

                                #region CODIGO S
                                //TITULOCODIGO S
                                var tblTituloCodigoS = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("CODIGO S", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCodigoS.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCodigoS.AddCell(linea);

                                doc.Add(tblTituloCodigoS);

                                //var tblTituloCodigoS = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("CODIGO S", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCodigoS.AddCell(celAeron);
                                //doc.Add(tblTituloCodigoS);

                                var tblCodigoS = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("CODIGO MODO S", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCodigoS.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("TIPO APROBACION RDAC", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCodigoS.AddCell(celAeron);



                                celAeron = new PdfPCell(new Phrase(oSolicitud.CODIGOMODOSS.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblCodigoS.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.TIPOAPROBACION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblCodigoS.AddCell(celAeron);


                                doc.Add(tblCodigoS);
                                #endregion

                                #region CONDICION
                                //CONDICION

                                var tbltCondicion = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("CONDICION", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tbltCondicion.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tbltCondicion.AddCell(linea);

                                doc.Add(tbltCondicion);

                                //var tbltCondicion = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("CONDICION", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tbltCondicion.AddCell(celAeron);
                                //doc.Add(tbltCondicion);

                                var tblCondicion = new PdfPTable(new float[] { 33f, 33f, 34f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("CONDICION", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCondicion.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("REGION", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCondicion.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("BASE OPERACION", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCondicion.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.CONDICION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblCondicion.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.REGION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblCondicion.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.BASEOPERACION.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblCondicion.AddCell(celAeron);


                                doc.Add(tblCondicion);
                                #endregion

                                #region MONITOREO
                                //MONITOREO

                                var tbltMonitoreo = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("MONITOREO", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tbltMonitoreo.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tbltMonitoreo.AddCell(linea);

                                doc.Add(tbltMonitoreo);

                                //var tbltMonitoreo = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("MONITOREO", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tbltMonitoreo.AddCell(celAeron);
                                //doc.Add(tbltMonitoreo);

                                var tblMonitoreo = new PdfPTable(new float[] { 15f, 15f, 70f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2f, SpacingAfter = 1f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMonitoreo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ERROR ASE(ft)", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMonitoreo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("OBSERVACIONES", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblMonitoreo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.FECHAMONITOREORVSM.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMonitoreo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.ERRORASE.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMonitoreo.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase(oSolicitud.OBSERVACIONES.Trim(), _standardFont));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                tblMonitoreo.AddCell(celAeron);


                                doc.Add(tblMonitoreo);
                                #endregion



                                //TABLA CERTIFICADO AERONAVE

                                #region tablacertificadoAeronave
                                var tblTituloCertifAeronave = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("CERTIFICACION AERONAVE", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertifAeronave.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertifAeronave.AddCell(linea);

                                doc.Add(tblTituloCertifAeronave);

                                //var tblTituloCertifAeronave = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("CERTIFICACION AERONAVE", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertifAeronave.AddCell(celAeron);
                                //doc.Add(tblTituloCertifAeronave);

                                //string aeronaveTexto = string.Empty;
                                var tblCertificadoAeronave = new PdfPTable(new float[] { 10f, 30f, 10f, 10F, 30F, 10F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("FECHA RENOVACION", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);
                                celAeron = new PdfPCell(new Phrase("FECHA CADUCIDAD", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR RENUEVA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertificadoAeronave.AddCell(celAeron);

                                try
                                {


                                    foreach (tbAeronavesCertAeronav item in ListaCertAero)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertificadoAeronave.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTOR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertificadoAeronave.AddCell(celAeron);
                                        celAeron = new PdfPCell(new Phrase(item.FECHARENOVACON.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertificadoAeronave.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.FECHACADUCIDAD.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertificadoAeronave.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertificadoAeronave.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertificadoAeronave.AddCell(celAeron);


                                    }
                                }
                                catch (Exception EX)
                                {


                                }


                                doc.Add(tblCertificadoAeronave);


                                #endregion

                                //TABLA CERTIFICADO HOMOLOGACION RADIO

                                #region tablacertificadoHomologacionRadio
                                var tblTituloCertiHomolRadio = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("CERTIFICADO HOMOLOGACIÓN DE RADIO", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiHomolRadio.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiHomolRadio.AddCell(linea);

                                doc.Add(tblTituloCertiHomolRadio);

                                //var tblTituloCertiHomolRadio = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("CERTIFICADO HOMOLOGACIÓN DE RADIO", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiHomolRadio.AddCell(celAeron);
                                //doc.Add(tblTituloCertiHomolRadio);

                                //string aeronaveTexto = string.Empty;
                                var tblCertHomoloRadio = new PdfPTable(new float[] { 10f, 35f, 10f, 35F, 10F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRadio.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRadio.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("FECHA RENOVACION", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRadio.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR RENUEVA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRadio.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRadio.AddCell(celAeron);

                                try
                                {


                                    foreach (tbDetalleCertLicRadio item in oSolicitud.oDetalleCertRadio)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertHomoloRadio.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertHomoloRadio.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.FECHARENOVACION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertHomoloRadio.AddCell(celAeron);


                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIRRENOV.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertHomoloRadio.AddCell(celAeron);
                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertHomoloRadio.AddCell(celAeron);


                                    }
                                }
                                catch (Exception EX)
                                {


                                }


                                doc.Add(tblCertHomoloRadio);


                                #endregion

                                //TABLA CERTIFICADO HOMOLOGACION RUIDO

                                #region tablacertificadoHomologacionRuido
                                var tblTituloCertiHomolRuido = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("CERTIFICADO HOMOLOGACIÓN DE RUIDO", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiHomolRuido.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiHomolRuido.AddCell(linea);

                                doc.Add(tblTituloCertiHomolRuido);

                                //var tblTituloCertiHomolRuido = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("CERTIFICADO HOMOLOGACIÓN DE RUIDO", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiHomolRuido.AddCell(celAeron);
                                //doc.Add(tblTituloCertiHomolRuido);

                                //string aeronaveTexto = string.Empty;
                                var tblCertHomoloRuido = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRuido.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRuido.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertHomoloRuido.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCertHomolRuido item in oSolicitud.oDetalleCerHomolRuido)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertHomoloRuido.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertHomoloRuido.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                                        tblCertHomoloRuido.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertHomoloRuido);

                                #endregion

                                //TABLA CERTIFICADO APROBACION PBN RNP10

                                #region tablacertificadoPbnRnp10


                                var tblTituloCertiPbnRnp10 = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN PBN/RNP10", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiPbnRnp10.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiPbnRnp10.AddCell(linea);

                                doc.Add(tblTituloCertiHomolRadio);

                                //var tblTituloCertiPbnRnp10 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN PBN/RNP10", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiPbnRnp10.AddCell(celAeron);
                                //doc.Add(tblTituloCertiPbnRnp10);

                                //string aeronaveTexto = string.Empty;
                                var tblCertPbnRnp10 = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnp10.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnp10.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnp10.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobaPbnRnp10 item in oSolicitud.oDetalleCertPbnRnp10)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertPbnRnp10.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnp10.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnp10.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertPbnRnp10);

                                #endregion

                                //TABLA CERTIFICADO APROBACION RNAV5

                                #region tablacertificadoRnav5

                                var tblTituloCertiRnav5 = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV5", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiRnav5.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiRnav5.AddCell(linea);

                                doc.Add(tblTituloCertiRnav5);

                                //var tblTituloCertiRnav5 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV5", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiRnav5.AddCell(celAeron);
                                //doc.Add(tblTituloCertiRnav5);

                                //string aeronaveTexto = string.Empty;
                                var tblCertPbnRnav5 = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav5.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav5.AddCell(celAeron);


                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav5.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobRnav5 item in oSolicitud.oDetalleCertRnav5)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertPbnRnav5.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnav5.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnav5.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertPbnRnav5);

                                #endregion

                                //TABLA CERTIFICADO APROBACION RNAV2

                                #region tablacertificadoRnav2

                                var tblTituloCertiRnav2 = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV2", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiRnav2.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiRnav2.AddCell(linea);

                                doc.Add(tblTituloCertiRnav2);

                                //var tblTituloCertiRnav2 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV2", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiRnav2.AddCell(celAeron);
                                //doc.Add(tblTituloCertiRnav2);

                                //string aeronaveTexto = string.Empty;
                                var tblCertPbnRnav2 = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav2.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertPbnRnav2.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobRnav2 item in oSolicitud.oDetalleCertRnav2)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertPbnRnav2.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnav2.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertPbnRnav2.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertPbnRnav2);

                                #endregion

                                //TABLA CERTIFICADO APROBACION RNAV APPROACH

                                #region tablacertificadoApproach

                                var tblTituloCertiApproach = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV APPROACH", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiApproach.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiApproach.AddCell(linea);

                                doc.Add(tblTituloCertiApproach);

                                //var tblTituloCertiApproach = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN RNAV APPROACH", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiApproach.AddCell(celAeron);
                                //doc.Add(tblTituloCertiApproach);

                                //string aeronaveTexto = string.Empty;
                                var tblCertApproach = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertApproach.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertApproach.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertApproach.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobRnavArAproach item in oSolicitud.oDetalleCertRnavApproach)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertApproach.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertApproach.AddCell(celAeron);
                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertApproach.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertApproach);

                                #endregion

                                //TABLA CERTIFICADO APROBACION RVSM

                                #region tablacertificadoRvsm

                                var tblTituloCertiRvsm = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                // tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN RVSM", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiRvsm.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiRvsm.AddCell(linea);

                                doc.Add(tblTituloCertiRvsm);

                                //var tblTituloCertiRvsm = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN RVSM", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiRvsm.AddCell(celAeron);
                                //doc.Add(tblTituloCertiRvsm);

                                //string aeronaveTexto = string.Empty;
                                var tblCertRvsm = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertRvsm.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertRvsm.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertRvsm.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobRvsm item in oSolicitud.oDetalleCertRvsm)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertRvsm.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertRvsm.AddCell(celAeron);

                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertRvsm.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertRvsm);

                                #endregion

                                //TABLA CERTIFICADO APROBACION ETOPS

                                #region tablacertificadoEtops


                                var tblTituloCertiEtops = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN ETOPS", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCertiEtops.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCertiEtops.AddCell(linea);

                                doc.Add(tblTituloCertiEtops);

                                //var tblTituloCertiEtops = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN ETOPS", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCertiEtops.AddCell(celAeron);
                                //doc.Add(tblTituloCertiEtops);

                                //string aeronaveTexto = string.Empty;
                                var tblCertEtops = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertEtops.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertEtops.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertEtops.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtAprobEtops item in oSolicitud.oDetalleCertEtops)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertEtops.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertEtops.AddCell(celAeron);
                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertEtops.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertEtops);

                                #endregion

                                //TABLA CERTIFICADO APROBACION CATEGORIAS II Y IIII

                                #region tablaccategorias2y3

                                var tblTituloCategorias = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 10f,
                                    SpacingAfter = 8f
                                };

                                // Fuente más visible
                                //  tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("APROBACIÓN CATEGORIAS II Y III", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estética profesional
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloCategorias.AddCell(celAeron);

                                // Línea inferior decorativa (opcional)
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloCategorias.AddCell(linea);

                                doc.Add(tblTituloCategorias);

                                //var tblTituloCategorias = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("APROBACIÓN CATEGORIAS II Y III", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloCategorias.AddCell(celAeron);
                                //doc.Add(tblTituloCategorias);

                                //string aeronaveTexto = string.Empty;
                                var tblCertCategorias = new PdfPTable(new float[] { 20f, 60f, 20F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertCategorias.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertCategorias.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("ESTADO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblCertCategorias.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleCerrtCat2y3 item in oSolicitud.oDetalleCategorias)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblCertCategorias.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertCategorias.AddCell(celAeron);
                                        // Colores según estado
                                        string estado = item.ESTADO?.Trim().ToUpper();

                                        celAeron = new PdfPCell(new Phrase(item.ESTADO.Trim(), _standardFont));
                                        if (estado == "ACTIVO")
                                        {
                                            celAeron.BackgroundColor = new BaseColor(198, 239, 206); // verde suave
                                            celAeron.Phrase.Font.Color = new BaseColor(0, 97, 0);    // texto verde oscuro
                                        }
                                        else
                                        {
                                            celAeron.BackgroundColor = new BaseColor(255, 199, 206); // rojo suave
                                            celAeron.Phrase.Font.Color = new BaseColor(156, 0, 6);   // texto rojo oscuro
                                        }
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblCertCategorias.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblCertCategorias);

                                #endregion

                                //TABLA ACCIDENTES

                                #region tablaAccidentes

                                var tblTituloAccidentes = new PdfPTable(new float[] { 100f })
                                {
                                    WidthPercentage = 100f,
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    SpacingBefore = 8f,
                                    SpacingAfter = 5f
                                };

                                // Fuente más equilibrada
                                //tituloBlanco = new Font(Titulos.BaseFont, 11, Font.BOLD, BaseColor.WHITE);

                                // Celda título
                                celAeron = new PdfPCell(new Phrase("INFORMACIÓN ACCIDENTE DE AERONAVE", tituloBlanco));
                                celAeron.HorizontalAlignment = Element.ALIGN_CENTER;
                                celAeron.VerticalAlignment = Element.ALIGN_MIDDLE;
                                celAeron.BackgroundColor = azulDGAC;

                                // Estilo limpio
                                celAeron.Border = Rectangle.NO_BORDER;
                                celAeron.PaddingTop = 5f;
                                celAeron.PaddingBottom = 5f;

                                tblTituloAccidentes.AddCell(celAeron);

                                // Línea decorativa más sutil
                                linea = new PdfPCell(new Phrase(""))
                                {
                                    BackgroundColor = azulClaro,
                                    FixedHeight = 1f,
                                    Border = Rectangle.NO_BORDER
                                };
                                tblTituloAccidentes.AddCell(linea);

                                doc.Add(tblTituloAccidentes);

                                //var tblTituloAccidentes = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 5f };
                                //celAeron = new PdfPCell(new Phrase("INFORMACIÓN ACCIDENTE AERONAVE ", Titulos));
                                //celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                //celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                //tblTituloAccidentes.AddCell(celAeron);
                                //doc.Add(tblTituloAccidentes);

                                //string aeronaveTexto = string.Empty;
                                var tblAccidentes = new PdfPTable(new float[] { 10f, 30f, 30F, 30F }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 5f, SpacingAfter = 7f };
                                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                                celAeron = new PdfPCell(new Phrase("FECHA EMISION.", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblAccidentes.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("INSPECTOR REGISTRO", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblAccidentes.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("LUGAR", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblAccidentes.AddCell(celAeron);

                                celAeron = new PdfPCell(new Phrase("PROVINCIA", Total));
                                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celAeron.BackgroundColor = new BaseColor(220, 220, 220);
                                tblAccidentes.AddCell(celAeron);

                                try
                                {
                                    foreach (tbDetalleAccidenteAeronave item in oSolicitud.oDetalleAccidenteAeronave)

                                    {

                                        celAeron = new PdfPCell(new Phrase(item.FECHAEMISION.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                                        tblAccidentes.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.NOMBREINSPECTORAIR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblAccidentes.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.LUGAR.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblAccidentes.AddCell(celAeron);

                                        celAeron = new PdfPCell(new Phrase(item.PROVINCIA.Trim(), _standardFont));
                                        celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                        tblAccidentes.AddCell(celAeron);

                                    }
                                }
                                catch (Exception EX)
                                {

                                }
                                doc.Add(tblAccidentes);

                                #endregion
                                doc.Close();
                                writer.Close();
                            }

                        }
                        listarSolicitud = oSolicitud;
                    }

                    dr.Close();
                    oConexion.Close();

                }

                return listarSolicitud;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return listarSolicitud;
        }

    }
}
