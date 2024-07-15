using CapaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Pkcs;
using iTextSharp.text.pdf.security;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using SistemaIntegradoGestion.Utilitario;
using SistemaIntegradoGestion.Utilitarios;
using System.Text;
using iTextSharp.text;


using CapaDatos;
using ZXing;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class DocumentoFirmadoFirmaElectronica
    {
        public static DocumentoFirmadoFirmaElectronica _instancia = null;
        private DocumentoFirmadoFirmaElectronica()
        {

        }

        public static DocumentoFirmadoFirmaElectronica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new DocumentoFirmadoFirmaElectronica();
                }
                return _instancia;
            }
        }


        public bool InsertaFirmaCertificadoPOADocumento(string inputPdfPath, string outputPdfPath, tbUsuario ousuario)
        {
            bool estado = false;
            string pathCertificado = string.Empty;
            string opiedeFirma = string.Empty;
            tbCertificadoDigital ocertificadoDigitalAto = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store store = new Pkcs12Store();
            PdfSignatureAppearance appearance;
            string qrText = "Texto QR";
            string textoAdicional = "";


            try
            {
                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);
                store = storeCertificado(oCertificado, ousuario);

                string alias = null;
                foreach (string tAlias in store.Aliases)
                {
                    if (store.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }

                // Generar la imagen QR
                BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
                var qrImage = writer.Write(qrText);

                using (var ms = new MemoryStream())
                {
                    AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    X509CertificateEntry[] chain = store.GetCertificateChain(alias);

                    qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    var qrBytes = ms.ToArray();

                    var pk = store.GetKey(alias).Key;

                    using (FileStream fs = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                    {

                        PdfReader reader = new PdfReader(inputPdfPath);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, fs, '\0');
                        appearance = stamper.SignatureAppearance;

                        // Configurar la apariencia de la firma
                        //appearance.Reason = "Firma digital";
                        //appearance.Location = "Ubicación";
                        //appearance.Contact = "Contacto";
                        appearance.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;

                        // Obtener el tamaño de la última página
                        Rectangle pageSize = reader.GetPageSize(reader.NumberOfPages);
                        float pageWidth = pageSize.Width;
                        float pageHeight = pageSize.Height;

                        // Posición del QR en la parte inferior izquierda
                        float qrWidth = 150; // Ancho del QR
                        float qrHeight = 150; // Altura del QR
                        float qrX = 385; // 20 unidades desde el borde izquierdo
                        float qrY = 10; // 20 unidades desde el borde inferior

                        // Agregar la imagen QR
                        PdfContentByte cb = stamper.GetOverContent(reader.NumberOfPages);
                        Image qrCodeImage = Image.GetInstance(qrBytes);
                        qrCodeImage.SetAbsolutePosition(qrX, qrY);
                        qrCodeImage.ScaleAbsolute(qrWidth, qrHeight);
                        cb.AddImage(qrCodeImage);

                        // Posición del texto en la parte inferior de la página
                        ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(textoAdicional, new Font(Font.FontFamily.HELVETICA, 12)), 20, qrY + qrHeight + 10, 0);

                        // Posición de la firma visible
                        float sigWidth = 150; // Ancho de la firma
                        float sigHeight = 50; // Altura de la firma
                        float sigX = (pageWidth - sigWidth) / 2; // Centrado horizontalmente
                        float sigY = qrY + 60; // Debajo del QR y el texto

                        // Establecer la posición de la firma visible
                        appearance.SetVisibleSignature(new Rectangle(sigX, sigY, sigX + sigWidth, sigY + sigHeight), reader.NumberOfPages, null);
                        var codigoCertificado = chain.Select(c => c.Certificate).ToList();
                        // Crear el objeto de firma
                        IExternalSignature externalSignature = new PrivateKeySignature(keyEntry.Key, "SHA-256");
                        MakeSignature.SignDetached(appearance, externalSignature, chain.Select(c => c.Certificate).ToList(), null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();
                    }
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }

        public string InsertaFirmaDocumentoQR(string inputPdfPath, string outputPdfPath, tbCertificadoDigital ocertificadoDigital, tbUsuario ousuario)
        {
            string mensaje = string.Empty;
            tbSistema oSistema = new tbSistema();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store store = new Pkcs12Store();
            PdfSignatureAppearance appearance;
            BarcodeQRCode barcodeQRCodeFirmaAera;
            Image codeQRImageFirmaAera;
            string alias = string.Empty;

            float x = 115; // Posición X de la esquina inferior izquierda
            float y = 130; // Posición Y de la esquina inferior izquierda
            string textoQRCodeFirma = string.Empty;
            string fechaFirma = string.Empty;

            try
            {
                oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                fechaFirma = fechaDateAs400(oSistema.FechaSistema);
                var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);

                if (ocertificadoDigital.PathDocumento.Length > 0)
                {
                    store = storeCertificado(oCertificado, ousuario);
                    if (store != null)
                    {
                        alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));
                        AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                        X509CertificateEntry[] chain = store.GetCertificateChain(alias);



                    }
                    else
                    {
                        mensaje = "";
                    }
                }
                else
                {
                    mensaje = "La firma electrónica no está cargada en la base de datos";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }


        private Pkcs12Store storeCertificado(tbCertificadoDigital ocertificado, tbUsuario ousuario)
        {
            string pathCertificado = string.Empty;
            string pathAutorizacion = string.Empty;
            tbCertificadoDigital ocertificadoDigital = new tbCertificadoDigital();
            tbUsuario oUsuarioAto = new tbUsuario();
            Pkcs12Store ostoreCertificado = null;
            try
            {
                //pathAutorizacion = Utilitario.Utilitarios.autorizacionURL;
                pathCertificado = Utilitarios.Utilitario.certificadoPOAUrl + @"\" + ousuario.CodigoUsuario;

                //if (!Directory.Exists(pathAutorizacion))
                //{
                //    Directory.CreateDirectory(pathAutorizacion);
                //}

                //var oCertificado = CD_CertificadoDigital.Instancia.CertificadoDigitalPorUsuario(ousuario.CodigoUsuario);

                string certificatePath = pathCertificado + @"\" + ocertificado.PathDocumento;
                string certificatePassword = SeguridadEncriptar.DesEncriptar(ocertificado.Contrasena);

                // Cargar el certificado
                Pkcs12Store store = new Pkcs12Store(new FileStream(certificatePath, FileMode.Open, FileAccess.Read), certificatePassword.ToCharArray());

                //objStore.Open(OpenFlags.ReadOnly);

                string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                if (alias != null)
                {
                    ostoreCertificado = store;
                    // keyEntry = store.GetKey(alias);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ostoreCertificado;
        }


        private List<X509CertificateEntry> CertificadoAlias(string pathArchivo, string contrasena)
        {
            X509CertificateEntry[] chain = null;
            try
            {
                if (pathArchivo != null)
                {

                    // Cargar el certificado
                    Pkcs12Store store = new Pkcs12Store(new FileStream(pathArchivo, FileMode.Open, FileAccess.Read), contrasena.ToCharArray());
                    string alias = store.Aliases.Cast<string>().FirstOrDefault(a => store.IsKeyEntry(a));

                    AsymmetricKeyEntry keyEntry = store.GetKey(alias);
                    chain = store.GetCertificateChain(alias);
                    chain.Select(c => c.Certificate).ToList();

                }

            }
            catch (Exception ex)
            {
                chain = null;
            }
            return chain.ToList();
        }

        private iTextSharp.text.Image GetQRCode(string content)
        {
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(content, 150, 150, null);
            iTextSharp.text.Image img = qrcode.GetImage();
            //MemoryStream ms = new MemoryStream(img.OriginalData);
            return img; //System.Drawing.Image.FromStream(ms);
        }

        //
        private iTextSharp.text.Image GetQRCodeNew(string content)
        {
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(content, 100, 100, null);
            iTextSharp.text.Image img = qrcode.GetImage();
            //MemoryStream ms = new MemoryStream(img.OriginalData);
            return img; //System.Drawing.Image.FromStream(ms);
        }

        private String fechaDateAs400(string ofecha)
        {
            string odate = string.Empty;
            if (ofecha.Trim().Length > 0)
            {
                odate = ofecha.Substring(6, 2) + "/" + ofecha.Substring(4, 2) + "/" + ofecha.Substring(0, 4);
            }


            return odate;
        }

    }
}