$(document).ready(function () {
    loadDataTable();   
    $('#btnEnviar').click(function () {      
        var _estadoFinanciero = $("#EstadoVerificacionFinanciera").val();
        if (_estadoFinanciero.trim().length == 0) {
            mensajeGeneral("Valida presupuestaria la solicitud", "Debe seleccionar el estado ");
            return false;
        }
        else if(_estadoFinanciero.trim() == "0") {
            mensajeGeneral("Valida presupuestaria la solicitud", "Debe seleccionar el estado ");
            return false;
        }

        $("#loadingBuscar").css("display", "block");
        $("#registerForm").submit();
    });
   

});

function loadDataTable() {
    $('#tbExploradorArchivos').DataTable({
        scrollY: '380px',
        scrollCollapse: true,
        paging: false,
        order: [[0, 'desc']],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
}

function abrirArchivo(fileName) {
    var nombreArchivo = JSON.parse(fileName);
    var opathArchivo = $('#pathArchivo').text();
    var _extensionArchivo = "";
    $("#loadingBuscar").css("display", "block");
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF") {
            var texto = $.MisUrls.url._VisualizarDocumentoSinAfectacion + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
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
            window.location = $.MisUrls.url._DescargarArchivoSinAfectacion + "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
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
        html: "<ul >" + contenido + "</ul>",
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false
    });
}

function mensajeGeneralIcono(titulo, contenido, ico) {
    Swal.fire({
        icon: ico,
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: "<ul >" + contenido + "</ul>",
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false
    });
}