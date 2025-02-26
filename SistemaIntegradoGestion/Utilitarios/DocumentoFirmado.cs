using CapaModelo;
using System;
using System.IO;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using SistemaIntegradoGestion.Utilitario;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using Org.BouncyCastle.Security;
using CapaDatos;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class DocumentoFirmado
    {
        
        public bool FirmaArchivoPDFCOnQR(string inputPdfPath, string outputPdfPath, tbCertificadoDigital certificado, string codUsuario)
        {
            bool estado = false;
            string pathCertifiacdo = string.Empty;
            string contrasena = string.Empty;

            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                pathCertifiacdo = Utilitario.certificadoPOAUrl + codUsuario + @"\" + certificado.PathDocumento;
                contrasena = SeguridadEncriptar.DesEncriptar(certificado.Contrasena);

                // Convertir el certificado X509Certificate2 a X509Certificate de BouncyCastle
                X509Certificate2 certificate2 = new X509Certificate2(pathCertifiacdo, contrasena);
                X509CertificateParser parser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate certificate = parser.ReadCertificate(certificate2.RawData);

                // Obtener la clave privada del almacén de claves utilizando BouncyCastle
                Pkcs12Store pkcs12Store = new Pkcs12Store(new FileStream(pathCertifiacdo, FileMode.Open), contrasena.ToCharArray());
                string alias = null;
                foreach (string iAlias in pkcs12Store.Aliases)
                {
                    if (pkcs12Store.IsKeyEntry(iAlias))
                    {
                        alias = iAlias;
                        break;
                    }
                }
                AsymmetricKeyEntry keyEntry = pkcs12Store.GetKey(alias);
                ICipherParameters privateKey = keyEntry.Key;

                // Crear el objeto PrivateKeySignature
                IExternalSignature pks = new PrivateKeySignature(privateKey, "SHA-256");

                // Crear el diccionario con la información de la firma (opcional)
                PdfDictionary diccionario = new PdfDictionary();
                diccionario.Put(PdfName.NAME, new PdfString(certificado.CodigoUsuario));
                diccionario.Put(PdfName.REASON, new PdfString("Actualización POA"));

                // Firmar el documento PDF
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create))
                {
                    PdfReader pdfReader = new PdfReader(inputPdfPath);
                    PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, outputStream, '\0');
                    PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
                    signatureAppearance.SetVisibleSignature(new iTextSharp.text.Rectangle(50, 50, 290, 220), pdfReader.NumberOfPages, "Firma");

                    // Agregar información de la firma al diccionario
                    signatureAppearance.CryptoDictionary = diccionario;

                    // Agregar el código QR (reemplaza "urlDelCodigoQR" con la URL real)

                    // Obtener los datos relevantes del certificado

                    StringBuilder certInfo = new StringBuilder();
                    certInfo.AppendLine($"Sujeto: {certificate.SubjectDN.ToString()}");
                    certInfo.AppendLine($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}");
                    //certInfo.AppendLine($"Número de serie: {certificate.SerialNumber}");
                    //certInfo.AppendLine($"Fecha de inicio de validez: {certificate.NotBefore}");
                    //certInfo.AppendLine($"Fecha de fin de validez: {certificate.NotAfter}");


                    // Agregar el código QR
                    iTextSharp.text.Image qrImage = GetQRCode(certInfo.ToString(), 250);
                    qrImage.SetAbsolutePosition(50, 90);
                    qrImage.ScaleAbsolute(100, 100);
                    pdfStamper.GetOverContent(1).AddImage(qrImage);


                    // Firmar el documento
                    MakeSignature.SignDetached(signatureAppearance, pks, new Org.BouncyCastle.X509.X509Certificate[] { certificate }, null, null, null, 0, CryptoStandard.CMS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }



        public string FirmaArchivoPDFConQR1(string inputPdfPath, string outputPdfPath, tbCertificadoDigital certificado, string codUsuario)
        {
            string estado = string.Empty;
            string pathCertifiacdo = string.Empty;
            string contrasena = string.Empty;

            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();

                pathCertifiacdo = Utilitario.certificadoPOAUrl + codUsuario + @"\" + certificado.PathDocumento;

                contrasena = SeguridadEncriptar.DesEncriptar(certificado.Contrasena);

                // Convertir el certificado X509Certificate2 a X509Certificate de BouncyCastle
                X509Certificate2 certificate2 = new X509Certificate2(pathCertifiacdo, contrasena);
                X509CertificateParser parser = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate certificate = parser.ReadCertificate(certificate2.RawData);

                // Obtener la clave privada del almacén de claves utilizando BouncyCastle
                Pkcs12Store pkcs12Store = new Pkcs12Store(new FileStream(pathCertifiacdo, FileMode.Open), contrasena.ToCharArray());
                string alias = null;
                foreach (string iAlias in pkcs12Store.Aliases)
                {
                    if (pkcs12Store.IsKeyEntry(iAlias))
                    {
                        alias = iAlias;
                        break;
                    }
                }
                AsymmetricKeyEntry keyEntry = pkcs12Store.GetKey(alias);
                ICipherParameters privateKey = keyEntry.Key;

                // Crear el objeto PrivateKeySignature
                IExternalSignature pks = new PrivateKeySignature(privateKey, "SHA-256");

                // Crear el diccionario con la información de la firma (opcional)
                PdfDictionary diccionario = new PdfDictionary();
                diccionario.Put(PdfName.NAME, new PdfString(certificado.CodigoUsuario));
                diccionario.Put(PdfName.REASON, new PdfString("Actualización POA"));

                // Firmar el documento PDF
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create))
                {
                    PdfReader pdfReader = new PdfReader(inputPdfPath);
                    PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, outputStream, '\0');
                    PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
                    signatureAppearance.SetVisibleSignature(new iTextSharp.text.Rectangle(145, 50, 290, 220), pdfReader.NumberOfPages, "Firma");

                    // Agregar información de la firma al diccionario
                    signatureAppearance.CryptoDictionary = diccionario;

                    // Agregar el código QR (reemplaza "urlDelCodigoQR" con la URL real)

                    // Obtener los datos relevantes del certificado

                    StringBuilder certInfo = new StringBuilder();
                    certInfo.AppendLine($"Sujeto: {certificate.SubjectDN.ToString()}");
                    certInfo.AppendLine($"Date: {DateTime.Now.ToString("yyyy-MM-dd")}");
                    //certInfo.AppendLine($"Número de serie: {certificate.SerialNumber}");
                    //certInfo.AppendLine($"Fecha de inicio de validez: {certificate.NotBefore}");
                    //certInfo.AppendLine($"Fecha de fin de validez: {certificate.NotAfter}");


                    // Agregar el código QR
                    iTextSharp.text.Image qrImage = GetQRCode(certInfo.ToString(), 250);
                    qrImage.SetAbsolutePosition(50, 90);
                    qrImage.ScaleAbsolute(100, 100);
                    pdfStamper.GetOverContent(1).AddImage(qrImage);


                    // Firmar el documento
                    MakeSignature.SignDetached(signatureAppearance, pks, new Org.BouncyCastle.X509.X509Certificate[] { certificate }, null, null, null, 0, CryptoStandard.CMS);
                    estado = "Se genero el archivo";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
                throw ex;
            }
            return estado;
        }



        public bool FirmaArchivoPDFConQRColeccion(string inputPdfPath, string outputPdfPath, tbCertificadoDigital certificado)
        {
            bool estado = false;
            string pathCertifiacdo = string.Empty;
            string contrasena = string.Empty;

            try
            {
                var oSistema = CD_Sistema.Instancia.GetFechaHoraSistema();
                List<Org.BouncyCastle.X509.X509Certificate> certificadosBouncyCastle = new List<Org.BouncyCastle.X509.X509Certificate>();

                X509Certificate2Collection certCollection = new X509Certificate2Collection();
                foreach (var item in certificado.oCertificado)
                {
                    pathCertifiacdo = Utilitarios.Utilitario.certificadoPOAUrl + item.CodigoUsuario + @"\" + certificado.PathDocumento;
                    contrasena = SeguridadEncriptar.DesEncriptar(certificado.Contrasena);
                    // Convertir el certificado X509Certificate2 a X509Certificate de BouncyCastle
                    X509Certificate2 certificate2 = new X509Certificate2(pathCertifiacdo, contrasena);
                    certCollection.Add(certificate2);
                    certificadosBouncyCastle.Add(DotNetUtilities.FromX509Certificate(certificate2));
                }
                // Cargar el documento PDF
                using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open))
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create))
                {
                    // Crear un objeto PDFReader
                    PdfReader reader = new PdfReader(inputStream);
                    // Crear un objeto PdfStamper para agregar la firma
                    PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);

                    // Configurar las firmas electrónicas
                    for (int i = 0; i < certCollection.Count; i++)
                    {
                        int pageNum = i + 1; // Página de la firma (en este ejemplo, se asume que cada firma está en una página diferente)
                        iTextSharp.text.Rectangle signatureRect = new iTextSharp.text.Rectangle(100, 100 + (i * 200), 200, 150 + (i * 200)); // Posición de la firma

                        // Obtener el objeto PdfSignatureAppearance para configurar la apariencia de la firma
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        appearance.SetVisibleSignature(signatureRect, pageNum, "Firma" + (i + 1));

                        // Configurar la firma electrónica
                        IExternalSignature external = new X509Certificate2Signature(certCollection[i], "SHA-256");
                        MakeSignature.SignDetached(appearance, external, new[] { certificadosBouncyCastle[i] }, null, null, null, 0, CryptoStandard.CMS);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }



        public iTextSharp.text.Image GetQRCode(string content, int qrSize)
        {
            iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(content, qrSize, qrSize, null);
            iTextSharp.text.Image img = qrcode.GetImage();
            return img;
        }

    }
}