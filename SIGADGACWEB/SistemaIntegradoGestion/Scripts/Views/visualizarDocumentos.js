$(document).ready(function () {
    $('#btnCerrar').click(function () {
        
    });


});

function abrirArchivo(fileName) {
    var nombreArchivo = JSON.parse(fileName);
    var opathArchivo = $('#pathArchivo').text();
    var _extensionArchivo = "";
    $("#loadingBuscar").css("display", "block");
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF" ) {
            var texto = $.MisUrls.url._VisualizarDocumentoPOA + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
             setTimeout(function () {
                 $("#iframeCetificado").attr("src", texto);
                 $("#loadingBuscar").css("display", "none");               
             }, 2000);
        }
        else {
            descargarArchivo(nombreArchivo, opathArchivo);            
        }

    }
    
}

function getExtensionArchivo(filename) {
    return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
}

function descargarArchivo(onombreArchivo, odireccion) {
    try {
        if (onombreArchivo.length > 0 && odireccion.length > 0) {
            window.location = $.MisUrls.url._DescargarArchivo + "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
            $("#loadingBuscar").css("display", "none");
        }
        else {
            mensajeGeneral("Descargar archivo", "El nombre del archivo en blanco.");
            $("#loadingBuscar").css("display", "none");
        }
    } catch (e) {
        mensajeGeneral("Descargar archivo", "Hay un problema al descargar el archivo.");
        $("#loadingBuscar").css("display", "none");
    }

}

function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        icon: 'warning',
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: "<ul class='text-left'>" + contenido + "</ul>",
        confirmButtonText: 'Aceptar'
    });
}