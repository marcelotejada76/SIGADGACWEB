using System;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class FirmaPDFConQR
    {
        public static FirmaPDFConQR _instancia = null;
        private FirmaPDFConQR()
        {

        }

        public static FirmaPDFConQR Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new FirmaPDFConQR();
                }
                return _instancia;
            }
        }

        public string SignPdf(string sourcePdfPath, string temporalPdfPath, string outputPdfPath, string pfxPath, string pfxPassword, string pfxPath1, string pfxPassword1)
        {
            string estadopdf = string.Empty;
            try
            {
                // Leer el archivo PFX para obtener el certificado y la clave privada
                X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword);

                X509Certificate2 cert1 = new X509Certificate2(pfxPath1, pfxPassword1);
                
                // Crear la primera firma
                SignPdfInternal(sourcePdfPath, temporalPdfPath, cert, "Firma 1", "Motivo de la firma 1", "Lugar 1");

                // Crear la segunda firma sobre el documento ya firmado
                SignPdfInternal(temporalPdfPath, outputPdfPath, cert1, "Firma 2", "Motivo de la firma 2", "Lugar 2");
            }
            catch (Exception ex)
            {
                estadopdf = ex.Message;
            }
            return estadopdf;
        }

        private void SignPdfInternal(string inputPdfPath, string outputPdfPath, X509Certificate2 cert, string fieldName, string reason, string location)
        {

            try
            {
                // Cargar el certificado
                var pk = Org.BouncyCastle.Security.DotNetUtilities.GetKeyPair(cert.PrivateKey).Private;
                var chain = new[] { Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(cert) };
                // Crear el lector del PDF
                using (PdfReader reader = new PdfReader(inputPdfPath))
                using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Crear el stamper para modificar el PDF
                    PdfStamper stamper = PdfStamper.CreateSignature(reader, outputStream, '\0');

                    // Crear la capa de firma
                    PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;
                    signatureAppearance.SetVisibleSignature(new iTextSharp.text.Rectangle(36, 748, 144, 780), 1, fieldName);
                    signatureAppearance.Reason = reason;
                    signatureAppearance.Location = location;

                    // Definir la firma digital
                    IExternalSignature externalSignature = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
                    MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

                    stamper.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}