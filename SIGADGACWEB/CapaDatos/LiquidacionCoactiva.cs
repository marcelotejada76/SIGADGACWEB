using IBM.Data.DB2.iSeries;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class LiquidacionCoactiva
    {
        public static List<CamposUceo> Liquidacion(string Ruc, string Procedimiento)
        {
            List<CamposUceo> lstOrden = new List<CamposUceo>();
            ///valida enviado por email
            string query = "SELECT * FROM FILARC WHERE FILRUC='"+Ruc+"' AND FILPRO='"+Procedimiento+"'";

            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(dr);

                con.Close();
                // string Ruc, NombreCompañia, Representante, Cedula, Direccion, Correo1, Correo2, Telefono, Celular, Lugar, Exigibilidad;

                foreach (DataRow item in dt.Rows)
                //foreach (DataGridViewRow row in dgvproductos.Rows)
                {
                    CamposUceo oFactura = new CamposUceo();
                    oFactura.Ruc = item.Field<string>("FILRUC");

                    oFactura.ProcedimientoCoacti = item.Field<string>("FILPRO");
                    oFactura.Nombrecia = item.Field<string>("FILNOM").Trim() + item.Field<string>("FILNO1").Trim();
                    oFactura.TituloCredito = item.Field<string>("FILTIT");

                    oFactura.ElaboradoPor = item.Field<string>("FILELA");
                    oFactura.CargoElaborado = item.Field<string>("FILCAR");
                    oFactura.RevisadoPor = item.Field<string>("FILREV");
                    oFactura.CargoRevisado = item.Field<string>("FILCA1");
                    oFactura.AprobadoPor = item.Field<string>("FILAPR");
                    oFactura.CargoAprobado = item.Field<string>("FILCA2");
                    oFactura.TipoDcto = item.Field<string>("FILTIP");
                    DateTime fec = DateTime.Now;
                    string fechaProceso = fec.ToString("yyyyMMdd"); //fecha del sistema
                    oFactura.FechaLiquidacion = fechaProceso;
                    //oFactura.FechaLiquidacion = item.Field<string>("FILFEC");


                    lstOrden.Add(oFactura);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.
            return lstOrden;
        }

        public static void Pdf(List<CamposUceo> listaMensaje)
        {
            foreach (CamposUceo mensaje in listaMensaje)
            {
                try
                {


                    if (mensaje.TipoDcto == "DE")
                    {
                        Console.WriteLine("Envia Liquidacion por Determinaciones");
                        DocumentoPdfDeterminacion(mensaje);
                    }
                    if (mensaje.TipoDcto == "FA")
                    {
                        Console.WriteLine("Envia Liquidacion por Facturas");
                        DocumentoPdfFactura(mensaje);
                    }
                    if (mensaje.TipoDcto == "IN")
                    {
                        Console.WriteLine("Envia Liquidacion por Infracciones");
                        DocumentoPdfInfracciones(mensaje);
                    }
                    if (mensaje.TipoDcto == "SE")
                    {
                        Console.WriteLine("Envia Liquidacion por Servicios");
                        DocumentoPdfServicios(mensaje);
                    }


                    //actualiza
                    //DatosOrden.ActualizaEnvioEmailPagoVoluntario(mensaje.Ruc, mensaje.Secuencia);

                }
                catch (Exception EX)
                {

                    //throw;
                }

            }
        }
        //determinacion
        public static void DocumentoPdfDeterminacion(CamposUceo model)
        {
            try
            {
                var ListadoFacturas = DetalleFacturasLiq(model.Ruc, model.ProcedimientoCoacti);

                string path = (@"C:\Cartera\");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string patharchivo = (@"C:\Cartera\LiquidacionDeterminacionCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf");

                // Creamos el documento con el tamaño de página tradicional
                //Document doc = new Document(PageSize.LETTER);
                Document doc = new Document(PageSize.A4, 35, 35, 35, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Cartera\LiquidacionDeterminacionCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf", FileMode.Create));

                doc.Open();




                iTextSharp.text.Font _Leyenda = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font ValoresTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.MAGENTA);
                iTextSharp.text.Font Valores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font TitulosValor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Dgac = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font TituloFr3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ValorDatos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font TituloAto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Compania = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font Total = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                // Creamos la imagen y le ajustamos el tamaño


                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:/Cartera/DGAC.jpeg");

                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 20 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                doc.Add(imagen);
                imagen.SpacingBefore = 2f;
                #region cabecera

                PdfPTable tblCabecera = new PdfPTable(1);
                tblCabecera.WidthPercentage = 100;
                tblCabecera.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clLineac = new PdfPCell(new Phrase(" DIRECCION GENERAL DE AVIACION CIVIL", Dgac));
                PdfPCell clLinea1 = new PdfPCell(new Phrase(" DIRECCION FINANCIERA", TitulosValor));
                PdfPCell clLinea2 = new PdfPCell(new Phrase(" GESTION INTERNA DE TESORERÍA ", TitulosValor));
                PdfPCell clLinea3 = new PdfPCell(new Phrase(" LIQUIDACION COACTIVA DETERMINACIONES", TitulosValor));
                PdfPCell clLinea4 = new PdfPCell(new Phrase(" AL " + model.FechaLiquidacion, TitulosValor));
                clLineac.BorderWidth = 0;
                clLinea1.BorderWidth = 0;
                clLinea2.BorderWidth = 0;
                clLinea3.BorderWidth = 0;
                clLinea4.BorderWidth = 0;
                clLineac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblCabecera.AddCell(clLineac);
                tblCabecera.AddCell(clLinea1);
                tblCabecera.AddCell(clLinea2);
                tblCabecera.AddCell(clLinea3);
                tblCabecera.AddCell(clLinea4);
                doc.Add(tblCabecera);

                doc.Add(new Paragraph("___________________________________________________________________________________________", TitulosValor));

                float[] widths = new float[] { 1f, 6f };

                //compañia deudora
                PdfPTable tblNombreCompañia = new PdfPTable(2);
                tblNombreCompañia.WidthPercentage = 100;
                tblNombreCompañia.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblNombreCompañia.SetWidths(widths);

                PdfPCell clCompañia = new PdfPCell(new Phrase("COMPAÑIA", TitulosValor));
                clCompañia.BorderWidth = 0;

                PdfPCell clValorCompañia = new PdfPCell(new Phrase(model.Nombrecia, _standardFontval));
                clValorCompañia.BorderWidth = 0;
                clValorCompañia.HorizontalAlignment = Element.ALIGN_LEFT;



                tblNombreCompañia.AddCell(clCompañia);

                tblNombreCompañia.AddCell(clValorCompañia);
                doc.Add(tblNombreCompañia);

                //ruc deudora
                PdfPTable tblRuc = new PdfPTable(2);
                tblRuc.WidthPercentage = 100;
                tblRuc.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRuc.SetWidths(widths);

                PdfPCell clRuc = new PdfPCell(new Phrase("CEDULA/RUC", TitulosValor));
                clRuc.BorderWidth = 0;

                PdfPCell clValorRuc = new PdfPCell(new Phrase(model.Ruc, _standardFontval));
                clValorRuc.BorderWidth = 0;
                clValorRuc.HorizontalAlignment = Element.ALIGN_LEFT;



                tblRuc.AddCell(clRuc);

                tblRuc.AddCell(clValorRuc);
                doc.Add(tblRuc);

                //representante legal deudora
                PdfPTable tblRepresentante = new PdfPTable(2);
                tblRepresentante.WidthPercentage = 100;
                tblRepresentante.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRepresentante.SetWidths(widths);

                PdfPCell clRepresentante = new PdfPCell(new Phrase("PROCEDIMIENTO DE EJECUCION COACTIVA", TitulosValor));
                clRepresentante.BorderWidth = 0;

                PdfPCell clValorRepresentante = new PdfPCell(new Phrase(model.ProcedimientoCoacti, _standardFontval));
                clValorRepresentante.BorderWidth = 0;
                clValorRepresentante.HorizontalAlignment = Element.ALIGN_LEFT;

                tblRepresentante.AddCell(clRepresentante);

                tblRepresentante.AddCell(clValorRepresentante);
                doc.Add(tblRepresentante);

                //Cedula
                PdfPTable tblCedula = new PdfPTable(2);
                tblCedula.WidthPercentage = 100;
                tblCedula.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblCedula.SetWidths(widths);

                PdfPCell clCedula = new PdfPCell(new Phrase("TÍTULO DE CRÉDITO", TitulosValor));
                clCedula.BorderWidth = 0;

                PdfPCell clValorCedula = new PdfPCell(new Phrase(model.TituloCredito, _standardFontval));
                clValorCedula.BorderWidth = 0;
                clValorCedula.HorizontalAlignment = Element.ALIGN_LEFT;

                tblCedula.AddCell(clCedula);

                tblCedula.AddCell(clValorCedula);
                doc.Add(tblCedula);
                //direccion
                PdfPTable tblDireccion = new PdfPTable(2);
                tblDireccion.WidthPercentage = 100;
                tblDireccion.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblDireccion.SetWidths(widths);

                #endregion
                //fin cabecera

                //tabla con factura
                #region detalle


                string aeronaveTexto = string.Empty;
                var tblAeronave = new PdfPTable(new float[] { 25f, 40f, 22f, 22f, 22f, 22f, 22f, 27f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 0f };
                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                var celAeron = new PdfPCell(new Phrase("DCTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TIPO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE PAGO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("CAPITAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("INTERES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVAS", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("SALDO TOTAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                decimal TotalCapital = 0;
                decimal TotalInteres = 0;
                decimal TotalGestion = 0;
                decimal TotalGeneral = 0;
                foreach (CamposUceo item in ListadoFacturas)

                {
                    celAeron = new PdfPCell(new Phrase(item.Documento.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Tipo.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaVencimiento.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaPago.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalMulta.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Intereses.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.CostasCoactivas.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Total.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    Decimal Capital = Convert.ToDecimal(item.TotalMulta.ToString());
                    TotalCapital = TotalCapital + Capital;
                    Decimal Interes = Convert.ToDecimal(item.Intereses.ToString());
                    TotalInteres = TotalInteres + Interes;
                    Decimal Gestion = Convert.ToDecimal(item.CostasCoactivas.ToString());
                    TotalGestion = TotalGestion + Gestion;
                    Decimal TotalG = Convert.ToDecimal(item.Total.ToString());
                    TotalGeneral = TotalGeneral + TotalG;
                }
                doc.Add(tblAeronave);

                //totales

                var tblTotales = new PdfPTable(new float[] { 89f, 18f, 18f, 18f, 22f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 0f };
                celAeron = new PdfPCell(new Phrase("TOTALES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblTotales.AddCell(celAeron);
                doc.Add(tblTotales);

                //leyenda totales

                var tblTotalesLeyenda = new PdfPTable(new float[] { 84f, 13f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("VALOR TOTAL ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);
                doc.Add(tblTotalesLeyenda);

                //resumen
                //leyenda totales

                var tblResumen = new PdfPTable(new float[] { 80f, 20f }) { WidthPercentage = 80f, HorizontalAlignment = 1, SpacingBefore = 10F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL INTERES ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL + INTERES ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                Decimal Subtotal = TotalCapital + TotalInteres;
                celAeron = new PdfPCell(new Phrase(Subtotal.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVA ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new iTextSharp.text.pdf.PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL ADEUDADO (CAPITAL + INTERES + COSTAS COACTIVA) ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                doc.Add(tblResumen);
                #endregion


                //fin tabla factura

                //datos de banco
                #region bancos






                #endregion
                //////firmas
                iTextSharp.text.pdf.PdfPTable tblFirmaPie = new PdfPTable(1);
                tblFirmaPie.WidthPercentage = 100;
                PdfPCell clLinea = new PdfPCell(new Phrase("         ____________________                           ____________________                               __________________ ", _standardFont));
                clLinea.BorderWidth = 0;
                PdfPCell clFirma = new PdfPCell(new Phrase("             ELABORADO POR                                 REVISADO POR                                          APROBADO POR ", _standardFont));
                clFirma.BorderWidth = 0;

                tblFirmaPie.SpacingBefore = 30f;

                PdfPCell clNombre = new PdfPCell(new Phrase("       " + model.ElaboradoPor.Trim() + "                  " + model.RevisadoPor.Trim() + "                                 " + model.AprobadoPor.Trim(), _standardFont));
                PdfPCell clCargo = new PdfPCell(new Phrase("                   " + model.CargoElaborado.Trim() + "                                              " + model.CargoRevisado.Trim() + "                                          " + model.CargoAprobado.Trim(), _standardFont));
                clNombre.BorderWidth = 0;
                clCargo.BorderWidth = 0;
                tblFirmaPie.AddCell(clLinea);
                tblFirmaPie.AddCell(clFirma);
                tblFirmaPie.AddCell(clNombre);
                tblFirmaPie.AddCell(clCargo);
                doc.Add(tblFirmaPie);

                doc.Close();
                writer.Close();
                //con.Close();




                //envio correo
                EnviarCorreo(model.Nombrecia, model.ProcedimientoCoacti, patharchivo);

                //ActualizaEstados.ActualizaEstadoImpresion(model.Ruc, model.ProcedimientoCoacti);

            }
            catch (Exception ex)
            {

                //                throw;
            }

        }

        //factura
        public static void DocumentoPdfFactura(CamposUceo model)
        {
            try
            {
                var ListadoFacturas = DetalleFacturasLiq(model.Ruc, model.ProcedimientoCoacti);

                string path = (@"C:\Cartera\");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string patharchivo = (@"C:\Cartera\LiquidacionFacturaCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf");

                // Creamos el documento con el tamaño de página tradicional
                //Document doc = new Document(PageSize.LETTER);
                Document doc = new Document(PageSize.A4, 35, 35, 35, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Cartera\LiquidacionFacturaCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf", FileMode.Create));

                doc.Open();




                iTextSharp.text.Font _Leyenda = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font ValoresTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.MAGENTA);
                iTextSharp.text.Font Valores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font TitulosValor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Dgac = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font TituloFr3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ValorDatos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font TituloAto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Compania = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font Total = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                // Creamos la imagen y le ajustamos el tamaño


                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:/Cartera/DGAC.jpeg");

                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 20 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                doc.Add(imagen);
                imagen.SpacingBefore = 2f;
                #region cabecera

                PdfPTable tblCabecera = new PdfPTable(1);
                tblCabecera.WidthPercentage = 100;
                tblCabecera.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clLineac = new PdfPCell(new Phrase(" DIRECCION GENERAL DE AVIACION CIVIL", Dgac));
                PdfPCell clLinea1 = new PdfPCell(new Phrase(" DIRECCION FINANCIERA", TitulosValor));
                PdfPCell clLinea2 = new PdfPCell(new Phrase(" GESTION INTERNA DE TESORERÍA ", TitulosValor));
                PdfPCell clLinea3 = new PdfPCell(new Phrase(" LIQUIDACION COACTIVA FACTURAS", TitulosValor));
                PdfPCell clLinea4 = new PdfPCell(new Phrase(" AL " + model.FechaLiquidacion, TitulosValor));
                clLineac.BorderWidth = 0;
                clLinea1.BorderWidth = 0;
                clLinea2.BorderWidth = 0;
                clLinea3.BorderWidth = 0;
                clLinea4.BorderWidth = 0;
                clLineac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblCabecera.AddCell(clLineac);
                tblCabecera.AddCell(clLinea1);
                tblCabecera.AddCell(clLinea2);
                tblCabecera.AddCell(clLinea3);
                tblCabecera.AddCell(clLinea4);
                doc.Add(tblCabecera);

                doc.Add(new Paragraph("___________________________________________________________________________________________", TitulosValor));

                float[] widths = new float[] { 1f, 6f };

                //compañia deudora
                PdfPTable tblNombreCompañia = new PdfPTable(2);
                tblNombreCompañia.WidthPercentage = 100;
                tblNombreCompañia.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblNombreCompañia.SetWidths(widths);

                PdfPCell clCompañia = new PdfPCell(new Phrase("COMPAÑIA", TitulosValor));
                clCompañia.BorderWidth = 0;

                PdfPCell clValorCompañia = new PdfPCell(new Phrase(model.Nombrecia, _standardFontval));
                clValorCompañia.BorderWidth = 0;
                clValorCompañia.HorizontalAlignment = Element.ALIGN_LEFT;



                tblNombreCompañia.AddCell(clCompañia);

                tblNombreCompañia.AddCell(clValorCompañia);
                doc.Add(tblNombreCompañia);

                //ruc deudora
                PdfPTable tblRuc = new PdfPTable(2);
                tblRuc.WidthPercentage = 100;
                tblRuc.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRuc.SetWidths(widths);

                PdfPCell clRuc = new PdfPCell(new Phrase("CEDULA/RUC", TitulosValor));
                clRuc.BorderWidth = 0;

                PdfPCell clValorRuc = new PdfPCell(new Phrase(model.Ruc, _standardFontval));
                clValorRuc.BorderWidth = 0;
                clValorRuc.HorizontalAlignment = Element.ALIGN_LEFT;



                tblRuc.AddCell(clRuc);

                tblRuc.AddCell(clValorRuc);
                doc.Add(tblRuc);

                //representante legal deudora
                PdfPTable tblRepresentante = new PdfPTable(2);
                tblRepresentante.WidthPercentage = 100;
                tblRepresentante.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRepresentante.SetWidths(widths);

                PdfPCell clRepresentante = new PdfPCell(new Phrase("PROCEDIMIENTO DE EJECUCION COACTIVA", TitulosValor));
                clRepresentante.BorderWidth = 0;

                PdfPCell clValorRepresentante = new PdfPCell(new Phrase(model.ProcedimientoCoacti, _standardFontval));
                clValorRepresentante.BorderWidth = 0;
                clValorRepresentante.HorizontalAlignment = Element.ALIGN_LEFT;

                tblRepresentante.AddCell(clRepresentante);

                tblRepresentante.AddCell(clValorRepresentante);
                doc.Add(tblRepresentante);

                //Cedula
                PdfPTable tblCedula = new PdfPTable(2);
                tblCedula.WidthPercentage = 100;
                tblCedula.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblCedula.SetWidths(widths);

                PdfPCell clCedula = new PdfPCell(new Phrase("TÍTULO DE CRÉDITO", TitulosValor));
                clCedula.BorderWidth = 0;

                PdfPCell clValorCedula = new PdfPCell(new Phrase(model.TituloCredito, _standardFontval));
                clValorCedula.BorderWidth = 0;
                clValorCedula.HorizontalAlignment = Element.ALIGN_LEFT;

                tblCedula.AddCell(clCedula);

                tblCedula.AddCell(clValorCedula);
                doc.Add(tblCedula);
                //direccion
                PdfPTable tblDireccion = new PdfPTable(2);
                tblDireccion.WidthPercentage = 100;
                tblDireccion.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblDireccion.SetWidths(widths);

                #endregion
                //fin cabecera

                //tabla con factura
                #region detalle


                string aeronaveTexto = string.Empty;
                var tblAeronave = new PdfPTable(new float[] { 25f, 40f, 22f, 22f, 22f, 22f, 22f, 22f, 22f, 27f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 0f };
                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                var celAeron = new PdfPCell(new Phrase("DCTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TIPO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE EMISION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE RECEPCION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);


                celAeron = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE PAGO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("CAPITAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("INTERES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVAS", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("SALDO TOTAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                decimal TotalCapital = 0;
                decimal TotalInteres = 0;
                decimal TotalGestion = 0;
                decimal TotalGeneral = 0;
                foreach (CamposUceo item in ListadoFacturas)

                {
                    celAeron = new PdfPCell(new Phrase(item.Documento.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Tipo.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaEmision.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaRecepcion.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaVencimiento.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaPago.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalMulta.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Intereses.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.CostasCoactivas.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Total.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    Decimal Capital = Convert.ToDecimal(item.TotalMulta.ToString());
                    TotalCapital = TotalCapital + Capital;
                    Decimal Interes = Convert.ToDecimal(item.Intereses.ToString());
                    TotalInteres = TotalInteres + Interes;
                    Decimal Gestion = Convert.ToDecimal(item.CostasCoactivas.ToString());
                    TotalGestion = TotalGestion + Gestion;
                    Decimal TotalG = Convert.ToDecimal(item.Total.ToString());
                    TotalGeneral = TotalGeneral + TotalG;
                }
                doc.Add(tblAeronave);

                //totales

                var tblTotales = new PdfPTable(new float[] { 91f, 13f, 13f, 13f, 16f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 0f };
                celAeron = new PdfPCell(new Phrase("TOTALES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblTotales.AddCell(celAeron);
                doc.Add(tblTotales);

                //leyenda totales

                var tblTotalesLeyenda = new PdfPTable(new float[] { 89f, 11f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("VALOR TOTAL ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);
                doc.Add(tblTotalesLeyenda);

                //resumen
                //leyenda totales

                var tblResumen = new PdfPTable(new float[] { 80f, 20f }) { WidthPercentage = 80f, HorizontalAlignment = 1, SpacingBefore = 10F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL INTERES ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL + INTERES ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                Decimal Subtotal = TotalCapital + TotalInteres;
                celAeron = new PdfPCell(new Phrase(Subtotal.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVA ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL ADEUDADO (CAPITAL + INTERES + COSTAS COACTIVA) ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                doc.Add(tblResumen);
                #endregion


                //fin tabla factura

                //datos de banco
                #region bancos



                #endregion
                //////firmas
                PdfPTable tblFirmaPie = new PdfPTable(1);
                tblFirmaPie.WidthPercentage = 100;
                PdfPCell clLinea = new PdfPCell(new Phrase("         ____________________                           ____________________                               __________________ ", _standardFont));
                clLinea.BorderWidth = 0;
                PdfPCell clFirma = new PdfPCell(new Phrase("             ELABORADO POR                                 REVISADO POR                                          APROBADO POR ", _standardFont));
                clFirma.BorderWidth = 0;

                tblFirmaPie.SpacingBefore = 30f;

                PdfPCell clNombre = new PdfPCell(new Phrase("       " + model.ElaboradoPor.Trim() + "                  " + model.RevisadoPor.Trim() + "                                 " + model.AprobadoPor.Trim(), _standardFont));
                PdfPCell clCargo = new PdfPCell(new Phrase("                   " + model.CargoElaborado.Trim() + "                                              " + model.CargoRevisado.Trim() + "                                          " + model.CargoAprobado.Trim(), _standardFont));
                clNombre.BorderWidth = 0;
                clCargo.BorderWidth = 0;
                tblFirmaPie.AddCell(clLinea);
                tblFirmaPie.AddCell(clFirma);
                tblFirmaPie.AddCell(clNombre);
                tblFirmaPie.AddCell(clCargo);
                doc.Add(tblFirmaPie);

                doc.Close();
                writer.Close();
                //con.Close();


                //envio correo
                EnviarCorreo(model.Nombrecia, model.ProcedimientoCoacti, patharchivo);

                //ActualizaEstados.ActualizaEstadoImpresion(model.Ruc, model.ProcedimientoCoacti);
            }
            catch (Exception ex)
            {

                //                throw;
            }

        }


        //infracciones
        public static void DocumentoPdfInfracciones(CamposUceo model)
        {
            try
            {
                var ListadoFacturas = DetalleFacturasLiq(model.Ruc, model.ProcedimientoCoacti);

                string path = (@"C:\Cartera\");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string patharchivo = (@"C:\Cartera\LiquidacionInfraccionCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf");

                // Creamos el documento con el tamaño de página tradicional
                //Document doc = new Document(PageSize.LETTER);
                Document doc = new Document(PageSize.A4, 35, 35, 35, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Cartera\LiquidacionInfraccionCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf", FileMode.Create));

                doc.Open();




                iTextSharp.text.Font _Leyenda = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font ValoresTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.MAGENTA);
                iTextSharp.text.Font Valores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font TitulosValor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Dgac = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font TituloFr3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ValorDatos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font TituloAto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Compania = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font Total = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                // Creamos la imagen y le ajustamos el tamaño


                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:/Cartera/DGAC.jpeg");

                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 20 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                doc.Add(imagen);
                imagen.SpacingBefore = 2f;
                #region cabecera

              
                PdfPTable tblCabecera = new PdfPTable(1);
                tblCabecera.WidthPercentage = 100;
                tblCabecera.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clLineac = new PdfPCell(new Phrase(" DIRECCION GENERAL DE AVIACION CIVIL", Dgac));
                PdfPCell clLinea1 = new PdfPCell(new Phrase(" DIRECCION FINANCIERA", TitulosValor));
                PdfPCell clLinea2 = new PdfPCell(new Phrase(" GESTION INTERNA DE TESORERÍA ", TitulosValor));
                PdfPCell clLinea3 = new PdfPCell(new Phrase(" LIQUIDACION COACTIVA INFRACCION", TitulosValor));
                PdfPCell clLinea4 = new PdfPCell(new Phrase(" AL " + model.FechaLiquidacion, TitulosValor));
                clLineac.BorderWidth = 0;
                clLinea1.BorderWidth = 0;
                clLinea2.BorderWidth = 0;
                clLinea3.BorderWidth = 0;
                clLinea4.BorderWidth = 0;
                clLineac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblCabecera.AddCell(clLineac);
                tblCabecera.AddCell(clLinea1);
                tblCabecera.AddCell(clLinea2);
                tblCabecera.AddCell(clLinea3);
                tblCabecera.AddCell(clLinea4);
                doc.Add(tblCabecera);

                doc.Add(new Paragraph("___________________________________________________________________________________________", TitulosValor));

                float[] widths = new float[] { 1f, 6f };

                //compañia deudora
                PdfPTable tblNombreCompañia = new PdfPTable(2);
                tblNombreCompañia.WidthPercentage = 100;
                tblNombreCompañia.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblNombreCompañia.SetWidths(widths);

                PdfPCell clCompañia = new PdfPCell(new Phrase("COMPAÑIA", TitulosValor));
                clCompañia.BorderWidth = 0;

                PdfPCell clValorCompañia = new PdfPCell(new Phrase(model.Nombrecia, _standardFontval));
                clValorCompañia.BorderWidth = 0;
                clValorCompañia.HorizontalAlignment = Element.ALIGN_LEFT;



                tblNombreCompañia.AddCell(clCompañia);

                tblNombreCompañia.AddCell(clValorCompañia);
                doc.Add(tblNombreCompañia);

                //ruc deudora
                PdfPTable tblRuc = new PdfPTable(2);
                tblRuc.WidthPercentage = 100;
                tblRuc.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRuc.SetWidths(widths);

                PdfPCell clRuc = new PdfPCell(new Phrase("CEDULA/RUC", TitulosValor));
                clRuc.BorderWidth = 0;

                PdfPCell clValorRuc = new PdfPCell(new Phrase(model.Ruc, _standardFontval));
                clValorRuc.BorderWidth = 0;
                clValorRuc.HorizontalAlignment = Element.ALIGN_LEFT;



                tblRuc.AddCell(clRuc);

                tblRuc.AddCell(clValorRuc);
                doc.Add(tblRuc);

                //representante legal deudora
                PdfPTable tblRepresentante = new PdfPTable(2);
                tblRepresentante.WidthPercentage = 100;
                tblRepresentante.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRepresentante.SetWidths(widths);

                PdfPCell clRepresentante = new PdfPCell(new Phrase("PROCEDIMIENTO DE EJECUCION COACTIVA", TitulosValor));
                clRepresentante.BorderWidth = 0;

                PdfPCell clValorRepresentante = new PdfPCell(new Phrase(model.ProcedimientoCoacti, _standardFontval));
                clValorRepresentante.BorderWidth = 0;
                clValorRepresentante.HorizontalAlignment = Element.ALIGN_LEFT;

                tblRepresentante.AddCell(clRepresentante);

                tblRepresentante.AddCell(clValorRepresentante);
                doc.Add(tblRepresentante);

                //Cedula
                PdfPTable tblCedula = new PdfPTable(2);
                tblCedula.WidthPercentage = 100;
                tblCedula.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblCedula.SetWidths(widths);

                PdfPCell clCedula = new PdfPCell(new Phrase("TÍTULO DE CRÉDITO", TitulosValor));
                clCedula.BorderWidth = 0;

                PdfPCell clValorCedula = new PdfPCell(new Phrase(model.TituloCredito, _standardFontval));
                clValorCedula.BorderWidth = 0;
                clValorCedula.HorizontalAlignment = Element.ALIGN_LEFT;

                tblCedula.AddCell(clCedula);

                tblCedula.AddCell(clValorCedula);
                doc.Add(tblCedula);
                //direccion
                PdfPTable tblDireccion = new PdfPTable(2);
                tblDireccion.WidthPercentage = 100;
                tblDireccion.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblDireccion.SetWidths(widths);

                #endregion
                //fin cabecera

                //tabla con factura
                #region detalle


                string aeronaveTexto = string.Empty;
                var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 27f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 0f };
                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                var celAeron = new PdfPCell(new Phrase("DCTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TIPO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE EMISION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE RECEPCION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);


                celAeron = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE PAGO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("TOTAL MULTA", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("AJUSTE ECONOMICO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("INTERES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVAS", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("SALDO TOTAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                decimal TotalCapital = 0;
                decimal TotalInteres = 0;
                decimal TotalGestion = 0;
                decimal TotalGeneral = 0;
                decimal TotalAjuste = 0;
                foreach (CamposUceo item in ListadoFacturas)

                {
                    celAeron = new PdfPCell(new Phrase(item.Documento.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Tipo.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaEmision.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaRecepcion.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaVencimiento.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaPago.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalMulta.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalAjusteEconomi.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Intereses.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.CostasCoactivas.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Total.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    Decimal Capital = Convert.ToDecimal(item.TotalMulta.ToString());
                    TotalCapital = TotalCapital + Capital;
                    Decimal ajuste = Convert.ToDecimal(item.TotalAjusteEconomi.ToString());
                    TotalAjuste = TotalAjuste + ajuste;
                    Decimal Interes = Convert.ToDecimal(item.Intereses.ToString());
                    TotalInteres = TotalInteres + Interes;
                    Decimal Gestion = Convert.ToDecimal(item.CostasCoactivas.ToString());
                    TotalGestion = TotalGestion + Gestion;
                    Decimal TotalG = Convert.ToDecimal(item.Total.ToString());
                    TotalGeneral = TotalGeneral + TotalG;
                }
                doc.Add(tblAeronave);

                //totales

                var tblTotales = new PdfPTable(new float[] { 89f, 15f, 16f, 16f, 15, 16f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 0f };
                celAeron = new PdfPCell(new Phrase("TOTALES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalAjuste.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblTotales.AddCell(celAeron);
                doc.Add(tblTotales);

                //leyenda totales

                var tblTotalesLeyenda = new PdfPTable(new float[] { 94f, 10f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("VALOR TOTAL ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);
                doc.Add(tblTotalesLeyenda);

                //resumen
                //leyenda totales

                var tblResumen = new PdfPTable(new float[] { 80f, 20f }) { WidthPercentage = 80f, HorizontalAlignment = 1, SpacingBefore = 10F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL REAJUSTE ECONÓMICO ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalAjuste.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL INTERES ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL + REAJUSTE ECONÓMICO + INTERESES ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                Decimal Subtotal = TotalCapital + TotalInteres + TotalAjuste;
                celAeron = new PdfPCell(new Phrase(Subtotal.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVA ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL ADEUDADO (CAPITAL + REAJUSTE ECONÓMICO + INTERESES + COSTAS COACTIVA) ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                doc.Add(tblResumen);
                #endregion


                //fin tabla factura

                //datos de banco
                #region bancos






                #endregion
                //////firmas
                PdfPTable tblFirmaPie = new PdfPTable(1);
                tblFirmaPie.WidthPercentage = 100;
                PdfPCell clLinea = new PdfPCell(new Phrase("         ____________________                           ____________________                               __________________ ", _standardFont));
                clLinea.BorderWidth = 0;
                PdfPCell clFirma = new PdfPCell(new Phrase("             ELABORADO POR                                 REVISADO POR                                          APROBADO POR ", _standardFont));
                clFirma.BorderWidth = 0;

                tblFirmaPie.SpacingBefore = 30f;

                PdfPCell clNombre = new PdfPCell(new Phrase("       " + model.ElaboradoPor.Trim() + "                  " + model.RevisadoPor.Trim() + "                                 " + model.AprobadoPor.Trim(), _standardFont));
                PdfPCell clCargo = new PdfPCell(new Phrase("                   " + model.CargoElaborado.Trim() + "                                              " + model.CargoRevisado.Trim() + "                                          " + model.CargoAprobado.Trim(), _standardFont));
                clNombre.BorderWidth = 0;
                clCargo.BorderWidth = 0;
                tblFirmaPie.AddCell(clLinea);
                tblFirmaPie.AddCell(clFirma);
                tblFirmaPie.AddCell(clNombre);
                tblFirmaPie.AddCell(clCargo);
                doc.Add(tblFirmaPie);

                doc.Close();
                writer.Close();
                //con.Close();

                //envio correo
                EnviarCorreo(model.Nombrecia, model.ProcedimientoCoacti, patharchivo);
                //ActualizaEstados.ActualizaEstadoImpresion(model.Ruc, model.ProcedimientoCoacti);


            }
            catch (Exception ex)
            {

                //                throw;
            }

        }

        //servicios
        public static void DocumentoPdfServicios(CamposUceo model)
        {
            try
            {
                var ListadoFacturas = DetalleFacturasLiq(model.Ruc, model.ProcedimientoCoacti);

                string path = (@"C:\Cartera\");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string patharchivo = (@"C:\Cartera\LiquidacionServiciosCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf");

                // Creamos el documento con el tamaño de página tradicional
                //Document doc = new Document(PageSize.LETTER);
                Document doc = new Document(PageSize.A4, 35, 35, 35, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Cartera\LiquidacionServiciosCliente" + model.Ruc.Trim() + "Procedimiento" + model.ProcedimientoCoacti.Trim() + ".pdf", FileMode.Create));

                doc.Open();




                iTextSharp.text.Font _Leyenda = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font ValoresTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.MAGENTA);
                iTextSharp.text.Font Valores = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
                iTextSharp.text.Font TitulosValor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Dgac = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font TituloFr3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ValorDatos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font TituloAto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font Compania = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font Total = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                // Creamos la imagen y le ajustamos el tamaño


                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:/Cartera/DGAC.jpeg");

                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 20 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                doc.Add(imagen);
                imagen.SpacingBefore = 2f;
                #region cabecera

                PdfPTable tblCabecera = new PdfPTable(1);
                tblCabecera.WidthPercentage = 100;
                tblCabecera.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clLineac = new PdfPCell(new Phrase(" DIRECCION GENERAL DE AVIACION CIVIL", Dgac));
                PdfPCell clLinea1 = new PdfPCell(new Phrase(" DIRECCION FINANCIERA", TitulosValor));
                PdfPCell clLinea2 = new PdfPCell(new Phrase(" GESTION INTERNA DE TESORERÍA ", TitulosValor));
                PdfPCell clLinea3 = new PdfPCell(new Phrase(" LIQUIDACION COACTIVA SERVICIOS", TitulosValor));
                PdfPCell clLinea4 = new PdfPCell(new Phrase(" AL " + model.FechaLiquidacion, TitulosValor));
                clLineac.BorderWidth = 0;
                clLinea1.BorderWidth = 0;
                clLinea2.BorderWidth = 0;
                clLinea3.BorderWidth = 0;
                clLinea4.BorderWidth = 0;
                clLineac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                clLinea4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblCabecera.AddCell(clLineac);
                tblCabecera.AddCell(clLinea1);
                tblCabecera.AddCell(clLinea2);
                tblCabecera.AddCell(clLinea3);
                tblCabecera.AddCell(clLinea4);
                doc.Add(tblCabecera);

                doc.Add(new Paragraph("___________________________________________________________________________________________", TitulosValor));

                float[] widths = new float[] { 1f, 6f };

                //compañia deudora
                PdfPTable tblNombreCompañia = new PdfPTable(2);
                tblNombreCompañia.WidthPercentage = 100;
                tblNombreCompañia.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblNombreCompañia.SetWidths(widths);

                PdfPCell clCompañia = new PdfPCell(new Phrase("COMPAÑIA", TitulosValor));
                clCompañia.BorderWidth = 0;

                PdfPCell clValorCompañia = new PdfPCell(new Phrase(model.Nombrecia, _standardFontval));
                clValorCompañia.BorderWidth = 0;
                clValorCompañia.HorizontalAlignment = Element.ALIGN_LEFT;



                tblNombreCompañia.AddCell(clCompañia);

                tblNombreCompañia.AddCell(clValorCompañia);
                doc.Add(tblNombreCompañia);

                //ruc deudora
                PdfPTable tblRuc = new PdfPTable(2);
                tblRuc.WidthPercentage = 100;
                tblRuc.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRuc.SetWidths(widths);

                PdfPCell clRuc = new PdfPCell(new Phrase("CEDULA/RUC", TitulosValor));
                clRuc.BorderWidth = 0;

                PdfPCell clValorRuc = new PdfPCell(new Phrase(model.Ruc, _standardFontval));
                clValorRuc.BorderWidth = 0;
                clValorRuc.HorizontalAlignment = Element.ALIGN_LEFT;



                tblRuc.AddCell(clRuc);

                tblRuc.AddCell(clValorRuc);
                doc.Add(tblRuc);

                //representante legal deudora
                PdfPTable tblRepresentante = new PdfPTable(2);
                tblRepresentante.WidthPercentage = 100;
                tblRepresentante.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblRepresentante.SetWidths(widths);

                PdfPCell clRepresentante = new PdfPCell(new Phrase("PROCEDIMIENTO DE EJECUCION COACTIVA", TitulosValor));
                clRepresentante.BorderWidth = 0;

                PdfPCell clValorRepresentante = new PdfPCell(new Phrase(model.ProcedimientoCoacti, _standardFontval));
                clValorRepresentante.BorderWidth = 0;
                clValorRepresentante.HorizontalAlignment = Element.ALIGN_LEFT;

                tblRepresentante.AddCell(clRepresentante);

                tblRepresentante.AddCell(clValorRepresentante);
                doc.Add(tblRepresentante);

                //Cedula
                PdfPTable tblCedula = new PdfPTable(2);
                tblCedula.WidthPercentage = 100;
                tblCedula.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblCedula.SetWidths(widths);

                PdfPCell clCedula = new PdfPCell(new Phrase("TÍTULO DE CRÉDITO", TitulosValor));
                clCedula.BorderWidth = 0;

                PdfPCell clValorCedula = new PdfPCell(new Phrase(model.TituloCredito, _standardFontval));
                clValorCedula.BorderWidth = 0;
                clValorCedula.HorizontalAlignment = Element.ALIGN_LEFT;

                tblCedula.AddCell(clCedula);

                tblCedula.AddCell(clValorCedula);
                doc.Add(tblCedula);
                //direccion
                PdfPTable tblDireccion = new PdfPTable(2);
                tblDireccion.WidthPercentage = 100;
                tblDireccion.HorizontalAlignment = Element.ALIGN_RIGHT;

                widths = new float[] { 2f, 4f };
                tblDireccion.SetWidths(widths);

                #endregion
                //fin cabecera

                //tabla con factura
                #region detalle


                string aeronaveTexto = string.Empty;
                var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 27f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 0f };
                //var tblAeronave = new PdfPTable(new float[] { 25f, 30f, 27f, 27f, 27f, 27f, 27f, 30f, 30f, 20f, 30f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 2F, SpacingAfter = 10f };
                var celAeron = new PdfPCell(new Phrase("DCTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TIPO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE EMISION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE RECEPCION", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);


                celAeron = new PdfPCell(new Phrase("FECHA DE VENCIMIENTO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("FECHA DE PAGO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("TOTAL MULTA", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("AJUSTE ECONOMICO", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("INTERES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVAS", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase("SALDO TOTAL", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblAeronave.AddCell(celAeron);
                decimal TotalCapital = 0;
                decimal TotalInteres = 0;
                decimal TotalGestion = 0;
                decimal TotalGeneral = 0;
                decimal TotalAjuste = 0;
                foreach (CamposUceo item in ListadoFacturas)

                {
                    celAeron = new PdfPCell(new Phrase(item.Documento.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Tipo.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaEmision.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaRecepcion.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaVencimiento.Trim(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.FechaPago.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalMulta.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.TotalAjusteEconomi.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    celAeron = new PdfPCell(new Phrase(item.Intereses.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.CostasCoactivas.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);
                    celAeron = new PdfPCell(new Phrase(item.Total.ToString(), ValorDatos));
                    celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    tblAeronave.AddCell(celAeron);

                    Decimal Capital = Convert.ToDecimal(item.TotalMulta.ToString());
                    TotalCapital = TotalCapital + Capital;
                    Decimal ajuste = Convert.ToDecimal(item.TotalAjusteEconomi.ToString());
                    TotalAjuste = TotalAjuste + ajuste;
                    Decimal Interes = Convert.ToDecimal(item.Intereses.ToString());
                    TotalInteres = TotalInteres + Interes;
                    Decimal Gestion = Convert.ToDecimal(item.CostasCoactivas.ToString());
                    TotalGestion = TotalGestion + Gestion;
                    Decimal TotalG = Convert.ToDecimal(item.Total.ToString());
                    TotalGeneral = TotalGeneral + TotalG;
                }
                doc.Add(tblAeronave);

                //totales

                var tblTotales = new PdfPTable(new float[] { 89f, 15f, 16f, 16f, 15, 16f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 0f };
                celAeron = new PdfPCell(new Phrase("TOTALES", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalAjuste.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotales.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                tblTotales.AddCell(celAeron);
                doc.Add(tblTotales);

                //leyenda totales

                var tblTotalesLeyenda = new PdfPTable(new float[] { 94f, 10f }) { WidthPercentage = 100f, HorizontalAlignment = 1, SpacingBefore = 0F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("VALOR TOTAL ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblTotalesLeyenda.AddCell(celAeron);
                doc.Add(tblTotalesLeyenda);

                //resumen
                //leyenda totales

                var tblResumen = new PdfPTable(new float[] { 80f, 20f }) { WidthPercentage = 80f, HorizontalAlignment = 1, SpacingBefore = 10F, SpacingAfter = 10f };
                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalCapital.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL REAJUSTE ECONÓMICO ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalAjuste.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL INTERES ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalInteres.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL CAPITAL + REAJUSTE ECONÓMICO + INTERESES ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                Decimal Subtotal = TotalCapital + TotalInteres + TotalAjuste;
                celAeron = new PdfPCell(new Phrase(Subtotal.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("COSTAS COACTIVA ", TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGestion.ToString(), TituloFr3));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);

                celAeron = new PdfPCell(new Phrase("TOTAL ADEUDADO (CAPITAL + REAJUSTE ECONÓMICO + INTERESES + COSTAS COACTIVA) ", Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                celAeron = new PdfPCell(new Phrase(TotalGeneral.ToString(), Total));
                celAeron.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tblResumen.AddCell(celAeron);
                doc.Add(tblResumen);
                #endregion


                //fin tabla factura

                //datos de banco
                #region bancos






                #endregion
                //////firmas
                PdfPTable tblFirmaPie = new PdfPTable(1);
                tblFirmaPie.WidthPercentage = 100;
                PdfPCell clLinea = new PdfPCell(new Phrase("         ____________________                           ____________________                               __________________ ", _standardFont));
                clLinea.BorderWidth = 0;
                PdfPCell clFirma = new PdfPCell(new Phrase("             ELABORADO POR                                 REVISADO POR                                          APROBADO POR ", _standardFont));
                clFirma.BorderWidth = 0;

                tblFirmaPie.SpacingBefore = 30f;

                PdfPCell clNombre = new PdfPCell(new Phrase("       " + model.ElaboradoPor.Trim() + "                  " + model.RevisadoPor.Trim() + "                                 " + model.AprobadoPor.Trim(), _standardFont));
                PdfPCell clCargo = new PdfPCell(new Phrase("                   " + model.CargoElaborado.Trim() + "                                              " + model.CargoRevisado.Trim() + "                                          " + model.CargoAprobado.Trim(), _standardFont));
                clNombre.BorderWidth = 0;
                clCargo.BorderWidth = 0;
                tblFirmaPie.AddCell(clLinea);
                tblFirmaPie.AddCell(clFirma);
                tblFirmaPie.AddCell(clNombre);
                tblFirmaPie.AddCell(clCargo);
                doc.Add(tblFirmaPie);

                doc.Close();
                writer.Close();
                //con.Close();

                //envio correo
                EnviarCorreo(model.Nombrecia, model.ProcedimientoCoacti, patharchivo);
                //ActualizaEstados.ActualizaEstadoImpresion(model.Ruc, model.ProcedimientoCoacti);


            }
            catch (Exception ex)
            {

                //                throw;
            }

        }

        //detalle facturas
        public static List<CamposUceo> DetalleFacturasLiq(string Ruc, string ProcedimientoCoactivo)
        {
            List<CamposUceo> lstOrden = new List<CamposUceo>();
            ///valida enviado por email
            string query = "SELECT * FROM dgacdat.FIDAR3 WHERE FIDRU2 = '" + Ruc + "'   AND FIDPRO = '" + ProcedimientoCoactivo + "' ORDER BY fidse7 DESC FETCH FIRST 1 ROW ONLY";


            iDB2Connection con = new iDB2Connection(ConexionDB2.CadenaConexion);

            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;

            try
            {

                cm.CommandText = query;
                cm.CommandType = CommandType.Text;

                iDB2DataReader dr = cm.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(dr);

                con.Close();
                // string Ruc, NombreCompañia, Representante, Cedula, Direccion, Correo1, Correo2, Telefono, Celular, Lugar, Exigibilidad;

                foreach (DataRow item in dt.Rows)
                //foreach (DataGridViewRow row in dgvproductos.Rows)
                {
                    CamposUceo oFactura = new CamposUceo();

                    // oFactura.Factura = Convert.ToInt32(item.Field<decimal>("FIDNU1"));
                    //oFactura.DetConcepto  = item.Field<string>("FIDCON");

                    oFactura.Documento = item.Field<string>("FIDDOC");
                    string Concepto = item.Field<string>("FIDTI2");
                    string Concepto1 = item.Field<string>("FIDTI3");
                    string Concepto2 = item.Field<string>("FIDTI4");

                    oFactura.Tipo = Concepto.Trim() + Concepto1.Trim() + Concepto2.Trim();


                    oFactura.FechaEmision = item.Field<string>("FIDFE6");
                    oFactura.FechaRecepcion = item.Field<string>("FIDFE7");
                    oFactura.FechaVencimiento = item.Field<string>("FIDFE8");
                    oFactura.FechaPago = item.Field<string>("FIDFE9");


                    oFactura.TotalMulta = Convert.ToDecimal(item.Field<decimal>("FIDTOT").ToString());
                    oFactura.TotalAjusteEconomi = Convert.ToDecimal(item.Field<decimal>("FIDTO1").ToString());
                    oFactura.Intereses = Convert.ToDecimal(item.Field<decimal>("FIDIN1").ToString());
                    oFactura.CostasCoactivas = Convert.ToDecimal(item.Field<decimal>("FIDCOS").ToString());

                    oFactura.Total = Convert.ToDecimal(item.Field<decimal>("FIDTO2").ToString());
                    oFactura.GestionCobro = Convert.ToDecimal(item.Field<decimal>("FIDGE1").ToString());

                    lstOrden.Add(oFactura);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("registro con error.:" + query.Trim());

            }
            //dr.
            return lstOrden;
        }

        //envio correo
        public static void EnviarCorreo(string Cliente, string Procedimiento, string path)
        {

            string sTextoMail = string.Empty;

            //sTextoMail += " <br/> Estimad@ : " + "<b>" + nombreagente + "</b>";
            sTextoMail += " <br/> Estimad@s : ";

            sTextoMail += " <br/><br/> Se adjunta documento Liquidacion Cliente: " + Cliente + "";
            // sTextoMail += " <br/><br/> Numero de Registros Fecha Proceso Indra " + "<b> <FONT COLOR='blue'>" + RegFecha + "</b></FONT>";
            sTextoMail += " <br/><br/> Procedimiento de Ejecucion Coactiva No " + "<b> <FONT COLOR='Blue'>" + Procedimiento + "</b></FONT>";

            // string correoagente = "marcelo.tejada@aviacioncivil.gob.ec";



            string noreply = "no_reply@aviacioncivil.gob.ec";

            string asunto = "Envio Liquidacion" + Cliente + "  - " + Procedimiento;


            //sTextoMail += "<br/><br/><br/> Debe ingresar por la Opción Anulación Tarjetas de Crédito Switch, ingresar el código de tarjeta y Número de Referencia.";
            //sTextoMail += " <br/><br/>  Si desea consultar el Numero de Tarjeta de Credito, Ingresar en el menu SIGETAME, CONSULTAS, TRANSACCIONES SWITCH T/C,F7 Busqueda por Referencia";
            sTextoMail += "<br/><br/><br/> Por favor no responda a este correo.";
            sTextoMail += "<br/><br/> Saludos Cordiales";
            sTextoMail += "<br/><br/><br/><br/>";

            try
            {
                //RECUPERA CORREOS
                //  string Correousuario1 = "";
                //  var Correos = Correousuario.CorreosUsuario(Correousuario1);

                //  var Correos = email,emailusuario;// "marcelo.tejada@aviacioncivil.gob.ec;jimmy.sandoval@aviacioncivil.gob.ec,cesar.maldonado@aviacioncivil.gob.ec";
                // var Correos = "marcelo.tejada@aviacioncivil.gob.ec";
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(noreply); // Correo electronico que usara nuestra aplicacion mvc para enviar correos

                // correo.To.Add(Correos);

                // correo.To.Add("marcelo.tejada@aviacioncivil.gob.ec");
                //correo.To.Add("maria.proano@aviacioncivil.gob.ec");
                correo.To.Add("marcelo.tejada@aviacioncivil.gob.ec");
                //correo.To.Add(correo1.ToLower());
                //correo.To.Add(correo2.ToLower());
                //correo.CC.Add("marcelo.tejada@aviacioncivil.gob.ec");
                //correo.CC.Add(email2.ToLower());
                // correo.CC.Add(email3.ToLower());



                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                Attachment a = new Attachment(fs, path, MediaTypeNames.Application.Pdf);
                correo.Attachments.Add(a);


                correo.Subject = asunto;
                correo.Body = sTextoMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //Configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient("172.20.16.21");
                //SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }



        }//fin funcion

       
    }
}
