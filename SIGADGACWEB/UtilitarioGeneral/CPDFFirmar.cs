using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Pkcs;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace UtilitarioGeneral
{
    public class CPDFFirmar
    {
        private readonly string _CertArchivo;
        private readonly string _CertContrasenia;
        private readonly string _Motivo;
        private readonly string _Ubicacion;
        private readonly string _Sello;

        public enum PaginaAFirmar
        {
            Primera,
            Ultima
        }



        /// <summary>
        ///   ''' Crear instancia de <see cref="CPDFFirmar"/>, por medio de un archivo de certificado y una contraseña para tener acceso al certificado
        ///   ''' </summary>
        ///   ''' <param name="CertArchivo">Ruta y nombre del archivo PFX o P12</param>
        ///   ''' <param name="CertContrasenia">Contraseña del certificado</param>
        ///   ''' <param name="Motivo">Motivo de la firma digital</param>
        ///   ''' <param name="Ubicacion">Ubicación de la firma digital</param>
        ///   ''' <param name="Sello">Ruta y nombre de la imagen de fondo (sello)</param>
        public CPDFFirmar(string CertArchivo, string CertContrasenia, string Motivo, string Ubicacion, string Sello)
        {
            try
            {
                // asignar
                _CertArchivo = CertArchivo;
                _CertContrasenia = CertContrasenia;
                _Motivo = Motivo;
                _Ubicacion = Ubicacion;
                _Sello = Sello;
            }
            catch (Exception ex)
            {
                // heredar
                throw ex;
            }
        }



        /// <summary>
        ///   ''' Firmar dopcumento PDF con certificado digital
        ///   ''' </summary>
        ///   ''' <param name="pdfAFirmar">Ruta y nombre del PDF a Firmar</param>
        ///   ''' <param name="pdfFirmado">Ruta y nombre del PDF Firmado</param>
        ///   ''' <param name="pdfPagina">´Página en donde se muestra la firma</param>
        public void Pagina(string pdfAFirmar, string pdfFirmado, PaginaAFirmar pdfPagina)
        {
            PdfReader pdfrAFirmar;
            Pkcs12Store pksStore;
            PdfStamper pdfsEstampado;
            PdfSignatureAppearance pdfsaApariencia;
            string cerAlias = string.Empty;
            X509Certificate[] Certificado;
            iTextSharp.text.Image imgSello;
            float nsejeX;
            float nsejeY;
            int iPagina = 0;
            try
            {

                // leer sello o imagen
                imgSello = iTextSharp.text.Image.GetInstance(_Sello);

                // leer PDF a firmar
                pdfrAFirmar = new PdfReader(pdfAFirmar);

                // leer certificado
                pksStore = new Pkcs12Store(new FileStream(_CertArchivo, FileMode.Open, FileAccess.Read), _CertContrasenia.ToCharArray());

                // obtener el alias del certificado
                cerAlias = pksStore.Aliases.Cast<string>().FirstOrDefault(entryAlias => pksStore.IsKeyEntry(entryAlias));

                // inicializar PDF Stamper y crear la apariencia de la firma
                pdfsEstampado = PdfStamper.CreateSignature(pdfrAFirmar, new FileStream(pdfFirmado, FileMode.Create), ControlChars.NullChar, null/* TODO Change to default(_) if this is not a reference type */, true);
                pdfsaApariencia = pdfsEstampado.SignatureAppearance;
                pdfsaApariencia.Acro6Layers = false;

                // se usan combinados, porque sino marca un error
                // pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.GraphicAndDescription
                // pdfsaApariencia.SignatureGraphic = imgSello

                pdfsaApariencia.Reason = _Motivo;
                // pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.NameAndDescription
                pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.Description;

                // comentar si se usa pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.GraphicAndDescription
                pdfsaApariencia.Image = imgSello;
                pdfsaApariencia.ImageScale = -1.0F; // ajustar al centro

                pdfsaApariencia.Location = _Ubicacion;

                // asignar la ubicación en donde se muestra la firma de la firma
                nsejeX = 400; // disminuir para acercar al margen izquierdo y aumentar para alejarlo
                nsejeY = 30; // disminuir para acercar al pie de pagina y aumentar para alejarlo

                // verificar
                switch (pdfPagina)
                {
                    case PaginaAFirmar.Primera:
                        {
                            iPagina = 1;
                            break;
                        }

                    case PaginaAFirmar.Ultima:
                        {
                            iPagina = pdfrAFirmar.NumberOfPages;
                            break;
                        }
                }

                // crear rectangulo para mostrar la firma
                pdfsaApariencia.SetVisibleSignature(new iTextSharp.text.Rectangle(nsejeX, nsejeY, nsejeX + 150, nsejeY + 50), iPagina, "Signature");

                // obtener el certificado
                Certificado = new X509Certificate[] { pksStore.GetCertificate(cerAlias).Certificate };

                // firmar el documento PDF
                pdfsaApariencia.SetCrypto(pksStore.GetKey(cerAlias).Key, Certificado, null/* TODO Change to default(_) if this is not a reference type */, PdfName.ADOBE_PPKMS);

                // cerrar para guardar firma
                pdfsEstampado.Close();
                pdfrAFirmar.Close();
            }
            catch (Exception ex)
            {
                // heredar
                throw ex;
            }
        }

        /// <summary>
        ///   ''' Firmar dopcumento PDF con certificado digital
        ///   ''' </summary>
        ///   ''' <param name="pdfAFirmar">Ruta y nombre del PDF a Firmar</param>
        ///   ''' <param name="pdfFirmado">Ruta y nombre del PDF Firmado</param>
        public void Todo(string pdfAFirmar, string pdfFirmado)
        {
            PdfReader pdfrAFirmar;
            Pkcs12Store pksStore;
            PdfStamper pdfsEstampado;
            PdfSignatureAppearance pdfsaApariencia;
            string cerAlias = string.Empty;
            X509Certificate[] Certificado;
            iTextSharp.text.Image imgSello;
            float nsejeX;
            float nsejeY;
            int iPagina;
            FileStream fsPDFFirmado;
            try
            {

                // leer sello o imagen
                imgSello = iTextSharp.text.Image.GetInstance(_Sello);

                // leer PDF a firmar
                pdfrAFirmar = new PdfReader(pdfAFirmar);

                // leer certificado
                pksStore = new Pkcs12Store(new FileStream(_CertArchivo, FileMode.Open, FileAccess.Read), _CertContrasenia.ToCharArray());

                // obtener el alias del certificado
                cerAlias = pksStore.Aliases.Cast<string>().FirstOrDefault(entryAlias => pksStore.IsKeyEntry(entryAlias));

                // por cada página
                for (iPagina = 1; iPagina <= pdfrAFirmar.NumberOfPages; iPagina++)
                {

                    // verificar
                    if (iPagina > 1)
                    {
                        // leer pdf firmado con filestream para poder eliminarlo
                        fsPDFFirmado = new FileStream(pdfFirmado, FileMode.Open, FileAccess.Read);
                        // leer PDF a firmar (ahora se convierte en el nuev PDF a firmar)
                        pdfrAFirmar = new PdfReader(fsPDFFirmado);
                        // cerrar filestream
                        fsPDFFirmado.Close();
                        // eliminar pdfFirmado
                        File.Delete(pdfFirmado);
                    }

                    // inicializar PDF Stamper y crear la apariencia de la firma
                    pdfsEstampado = PdfStamper.CreateSignature(pdfrAFirmar, new FileStream(pdfFirmado, FileMode.Create), ControlChars.NullChar, null/* TODO Change to default(_) if this is not a reference type */, true);
                    pdfsaApariencia = pdfsEstampado.SignatureAppearance;
                    pdfsaApariencia.Acro6Layers = false;

                    // se usan combinados, porque sino marca un error
                    // pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.GraphicAndDescription
                    // pdfsaApariencia.SignatureGraphic = imgSello

                    pdfsaApariencia.Reason = _Motivo;
                    // pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.NameAndDescription
                    pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.Description;

                    // comentar si se usa pdfsaApariencia.Render = PdfSignatureAppearance.SignatureRender.GraphicAndDescription
                    pdfsaApariencia.Image = imgSello;
                    pdfsaApariencia.ImageScale = -1.0F; // ajustar al centro

                    pdfsaApariencia.Location = _Ubicacion;

                    // asignar localizacion de la firma
                    nsejeX = 400; // disminuir para acercar al margen izquierdo y aumentar para alejarlo
                    nsejeY = 30; // disminuir para acercar al pie de pagina y aumentar para alejarlo

                    // crear rectangulo para mostrar la firma
                    pdfsaApariencia.SetVisibleSignature(new iTextSharp.text.Rectangle(nsejeX, nsejeY, nsejeX + 150, nsejeY + 50), iPagina, null/* TODO Change to default(_) if this is not a reference type */);

                    // obtener el certificado
                    Certificado = new X509Certificate[] { pksStore.GetCertificate(cerAlias).Certificate };

                    // firmar el documento PDF
                    pdfsaApariencia.SetCrypto(pksStore.GetKey(cerAlias).Key, Certificado, null/* TODO Change to default(_) if this is not a reference type */, PdfName.ADOBE_PPKMS);

                    // cerrar para guardar firma
                    pdfsEstampado.Close();
                    pdfrAFirmar.Close();
                }
            }
            catch (Exception ex)
            {
                // heredar
                throw ex;
            }
        }
    }
}