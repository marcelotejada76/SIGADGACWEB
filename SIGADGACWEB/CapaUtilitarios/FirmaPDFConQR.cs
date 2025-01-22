using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace CapaUtilitarios
{
   public class FirmaPDFConQR
    {
        public static void FirmarDocumentoPDFConQR(string rutaPDF, string rutaCertificado1, string contraseñaCertificado1, string rutaCertificado2, string contraseñaCertificado2, string contenidoQR, string rutaPDFSalida)
        {
            // Crear un lector de PDF
            PdfReader reader = new PdfReader(rutaPDF);

            // Primera firma
            using (FileStream fs = new FileStream("documento_con_primera_firma.pdf", FileMode.Create, FileAccess.Write))
            {
                PdfStamper stamper = PdfStamper.CreateSignature(reader, fs, '\0');

                // Cargar el primer certificado
                X509Certificate2 cert1 = new X509Certificate2(rutaCertificado1, contraseñaCertificado1);
                // Convertir el certificado a formato BouncyCastle
                // Convertir el certificado a formato BouncyCastle
                Org.BouncyCastle.X509.X509Certificate bcCert = DotNetUtilities.FromX509Certificate(cert1);
                ICollection<X509Certificate> chain = new List<X509Certificate> { bcCert };

                PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                appearance.SetVisibleSignature(new Rectangle(36, 748, 144, 780), 1, "Firma1");

                // Agregar la primera firma
                IExternalSignature pks1 = new X509Certificate2Signature(cert1, "SHA-256");
                MakeSignature.SignDetached(appearance, pks1, chain, null, null, null, 0, CryptoStandard.CMS);

                
                // Agregar código QR
                AgregarCodigoQR(stamper, contenidoQR);

                stamper.Close();
            }

            // Segunda firma
            reader = new PdfReader("documento_con_primera_firma.pdf"); // Leer el documento con la primera firma
            using (FileStream fs = new FileStream(rutaPDFSalida, FileMode.Create, FileAccess.Write))
            {
                PdfStamper stamper = PdfStamper.CreateSignature(reader, fs, '\0', null, true);

                // Cargar el segundo certificado
                X509Certificate2 cert2 = new X509Certificate2(rutaCertificado2, contraseñaCertificado2);

                Org.BouncyCastle.X509.X509Certificate bcCert = DotNetUtilities.FromX509Certificate(cert2);
                ICollection<X509Certificate> chain1 = new List<X509Certificate> { bcCert };

                PdfSignatureAppearance appearance2 = stamper.SignatureAppearance;
                appearance2.SetVisibleSignature(new Rectangle(36, 700, 144, 732), 1, "Firma2"); // Ubicación diferente para la segunda firma

                // Agregar la segunda firma
                IExternalSignature pks2 = new X509Certificate2Signature(cert2, "SHA-256");
                MakeSignature.SignDetached(appearance2, pks2, chain1, null, null, null, 0, CryptoStandard.CMS);

                stamper.Close();
            }

            reader.Close();
        }



        private static void AgregarCodigoQR(PdfStamper stamper, string contenidoQR)
        {
            PdfContentByte over = stamper.GetOverContent(1); // Página 1 del PDF

            // Crear la imagen QR
            BarcodeQRCode qrcode = new BarcodeQRCode(contenidoQR, 100, 100, null);
            Image qrImage = qrcode.GetImage();
            qrImage.SetAbsolutePosition(450, 750); // Posición del código QR en el documento (modificar según necesidad)
            qrImage.ScaleAbsolute(100, 100); // Tamaño del código QR

            // Agregar la imagen QR al PDF
            over.AddImage(qrImage);
        }

    }
}
