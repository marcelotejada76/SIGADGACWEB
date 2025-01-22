$(document).ready(function () {
    loadDataTable();   
});

function loadDataTable() {
    $('#tbExploradorArchivos').DataTable({
        scrollY: '380px',
        scrollCollapse: true,
        paging: false,
        searching: false,
        ordering: false,
        info: false,
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
    $("#aprobarSolicitudPresupuesto").attr('disabled', 'disabled');    
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF") {
            $("#nombre-archivo").val(nombreArchivo); 
            $("#aprobarSolicitudPresupuesto").removeAttr('disabled');
            var texto = $.MisUrls.url._VisualizarArchivo + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
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
            window.location = $.MisUrls.url._DescargarArchivo+ "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
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

function FirmarPrueba(campo) {
    var nombreRarchivo = $("#nombre-archivo").val();
    var pathDirectorio = $("#Directory").val();
    $.post($.MisUrls.url._imprmeSolicitudPOAEjemplo, { nombreArchivo: nombreRarchivo, pathDirectorio: pathDirectorio}, function (htmlPago) {
        if (htmlPago == true) {
            alert("SE GENERO EL ARCHIVO");
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });

    return false;
}


function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        icon: 'warning',
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: "<ul >" + contenido + "</ul>",
        confirmButtonText: 'Aceptar'
    });
}
