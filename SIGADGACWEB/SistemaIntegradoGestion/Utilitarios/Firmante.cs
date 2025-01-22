using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using ZXing;

namespace SistemaIntegradoGestion.Utilitarios
{
    public class Firmante
    {
        private readonly Certificado certificado;

        public Firmante(Certificado certificado)
        {
            this.certificado = certificado;
        }


        public void Firmar(string rutaDocumentoSinFirma, string rutaDocumentoFirmado)
        {
            using (var reader = new PdfReader(rutaDocumentoSinFirma))
            using (var writer = new FileStream(rutaDocumentoFirmado, FileMode.Create, FileAccess.Write))
            using (var stamper = PdfStamper.CreateSignature(reader, writer, '\0', null, true))
            {
                var signature = stamper.SignatureAppearance;
                signature.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
                signature.Reason = "Firma del sistema";
                signature.ReasonCaption = "Tipo de firma: ";

                var signatureKey = new PrivateKeySignature(certificado.Key, DigestAlgorithms.SHA256);
                var signatureChain = certificado.Chain;
                var standard = CryptoStandard.CADES;

                MakeSignature.SignDetached(signature, signatureKey, signatureChain, null, null, null, 0, standard);
            }
        }


        public void FirmarR(string rutaDocumentoSinFirma, string rutaDocumentoFirmado)
        {
            // Generar la imagen QR
            string textoFirmado = string.Empty;
            // Posición del QR en la parte inferior izquierda
            float qrWidth = 120; // Ancho del QR
            float qrHeight = 120; // Altura del QR
            float qrX = 100; // 20 unidades desde el borde izquierdo
            float qrY = 50; // 20 unidades desde el borde inferior
            BarcodeWriter imgagenQR = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
            using (var ms = new MemoryStream())
            using (var reader = new PdfReader(rutaDocumentoSinFirma))
            using (var writer = new FileStream(rutaDocumentoFirmado, FileMode.Create, FileAccess.Write))
            using (var stamper = PdfStamper.CreateSignature(reader, writer, '\0', null, true))
            {

                var signature = stamper.SignatureAppearance;
                signature.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;
                signature.Reason = "Firma del sistema";
                signature.ReasonCaption = "Tipo de firma: ";

                var signatureKey = new PrivateKeySignature(certificado.Key, DigestAlgorithms.SHA256);
                var signatureChain = certificado.Chain;
                var standard = CryptoStandard.CMS; // .CADES;
                textoFirmado = "FIRMADO: " + signatureChain[0].SubjectDN + "\n" + "FECHA: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var qrImage = imgagenQR.Write(textoFirmado);
                qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var qrBytes = ms.ToArray();
                // Agregar la imagen QR
                PdfContentByte cb = stamper.GetOverContent(reader.NumberOfPages);
                Image qrCodeImage = Image.GetInstance(qrBytes);
                qrCodeImage.SetAbsolutePosition(qrX, qrY);
                qrCodeImage.ScaleAbsolute(qrWidth, qrHeight);
                cb.AddImage(qrCodeImage);

                signature.SetVisibleSignature(new iTextSharp.text.Rectangle(390, 160, 240, 60), reader.NumberOfPages, null);
                MakeSignature.SignDetached(signature, signatureKey, signatureChain, null, null, null, 0, standard);
                writer.Close();
                ms.Close();
                reader.Close();
                stamper.Close();
            }
        }
        private string GetCertificateAlias(Org.BouncyCastle.Pkcs.Pkcs12Store store)
        {
            foreach (string currentAlias in store.Aliases)
            {
                if (store.IsKeyEntry(currentAlias))
                {
                    return currentAlias;
                }
            }

            return null;
        }
    }
}