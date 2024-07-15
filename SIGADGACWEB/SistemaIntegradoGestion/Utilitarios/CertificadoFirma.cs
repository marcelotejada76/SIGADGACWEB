using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using CapaModelo;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Pkcs;

namespace SistemaIntegradoGestion.Utilitario
{
    public class CertificadoFirma
    {
        public static CertificadoFirma _instancia = null;
        private CertificadoFirma()
        {

        }

        public static CertificadoFirma Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CertificadoFirma();
                }
                return _instancia;
            }
        }

        private byte[] LeerArchivo(string nombreArchivo)
        {
            FileStream f = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
        public void FirmaArchivoPDF(string inputFilePath, string outputFilePath, X509Certificate2 certificate)
        {
            
            try
            {
                using (Stream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Crear un nuevo documento PDF
                    Document document = new Document();
                    PdfCopy copy = new PdfCopy(document, outputStream);
                    document.Open();

                    // Crear el objeto PdfReader para el archivo PDF original
                    PdfReader reader = new PdfReader(inputStream);

                    // Obtener el número de páginas del documento original
                    int pageCount = reader.NumberOfPages;

                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // Obtener la página actual
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, pageIndex);

                        // Agregar la página al nuevo documento
                        copy.AddPage(importedPage);
                    }

                    // Crear el objeto PdfStamper para firmar el documento
                    PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);
                    PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;

                    // Configurar la apariencia de la firma
                    signatureAppearance.SetVisibleSignature(new Rectangle(100, 100, 200, 200), 1, "Signature");

                    // Configurar el certificado digital
                    signatureAppearance.Certificate = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);

                    // Terminar de crear la firma
                    stamper.Close();
                    reader.Close();
                    document.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool FirmaArchivoPDFCOnQR(string inputFilePath, string outputFilePath, X509Certificate2 certificate, string qrCodeContent)
        {
            bool estado = false;
            try
            {
                using (Stream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Crear un nuevo documento PDF
                    Document document = new Document();
                    PdfCopy copy = new PdfCopy(document, outputStream);
                    document.Open();

                    // Crear el objeto PdfReader para el archivo PDF original
                    PdfReader reader = new PdfReader(inputStream);

                    // Obtener el número de páginas del documento original
                    int pageCount = reader.NumberOfPages;

                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // Obtener la página actual
                        PdfImportedPage importedPage = copy.GetImportedPage(reader, pageIndex);

                        // Agregar la página al nuevo documento
                        copy.AddPage(importedPage);
                    }

                    // Crear el objeto PdfStamper para firmar el documento
                    PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0', null, true);
                    PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;

                    // Configurar la apariencia de la firma
                    signatureAppearance.SetVisibleSignature(new Rectangle(100, 100, 200, 200), 1, "Signature");

                    // Configurar el certificado digital
                    signatureAppearance.Certificate = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);

                    // Añadir el código QR
                    Image qrCodeImage = GenerateQRCode(qrCodeContent);
                    qrCodeImage.SetAbsolutePosition(300, 100); // Posición del código QR en la página
                    qrCodeImage.ScaleAbsolute(100, 100); // Tamaño del código QR
                    stamper.GetOverContent(1).AddImage(qrCodeImage);

                    // Terminar de crear la firma
                    stamper.Close();
                    reader.Close();
                    document.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estado;
        }
        private static Image GenerateQRCode(string content)
        {
            BarcodeQRCode qrCode = new BarcodeQRCode(content, 1000, 1000, null);
            return qrCode.GetImage();
        }

    }
}