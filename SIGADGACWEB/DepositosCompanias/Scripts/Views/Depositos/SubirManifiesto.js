$(document).ready(function () {
    loadDataTable();
    $('#documentFile').on('change', function () {
        if ($(this).val() != '') {
            //alert("La extensión es: " + ext);  
            const fileSize = $(this)[0].files[0].size / 1024 / 1024; // in MiB 
            if (fileSize > 2) {
                Swal.fire({
                    icon: 'warning',
                    title: "¡Precaución!",
                    html: "El documento excede el tamaño máximo, se solicita un archivo no mayor a 2MB. Por favor verifica."
                });
                $(this).val('');
            }
        }
        $("#labelFile").html($('#documentFile').val());

    });
    $('#btnEnviar').click(function () {
       
        var Ruc = $("#UsuarioRuc").val();
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
    var opathArchivo = $('#Directory').val();

    var _extensionArchivo = "";

    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {

        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF") {

            var texto = $.MisUrls.url._VisualizarDepositos + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
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

//function descargarArchivo(onombreArchivo, odireccion) {
//    try {
//        if (onombreArchivo.length > 0 && odireccion.length > 0) {
//            window.location = $.MisUrls.url._DownloadFileDeposito + "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
//            $("#loadingBuscar").css("display", "none");
//        }
//        else {
//            mensajeGeneral("Descargar archivo", "El nombre del archivo en blanco.");
//            $("#loadingBuscar").css("display", "none");
//        }
//    } catch (e) {
//        mensajeGeneral("Descargar archivo", "Hay un problema al descargar el archivo.");
//        $("#loadingBuscar").css("display", "none");
//    }

//}

function eliminar(fileName) {
    var nombreArchivo = JSON.parse(fileName);
    var opathArchivo = $('#Directory').val();


    if (nombreArchivo != null) {

        Swal.fire({
            title: '¿Eliminar?',
            text: "¿Está seguro de que eliminar el archivo del sitio?\n" + opathArchivo + "\\" + nombreArchivo,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#C5C7CF',
            confirmButtonText: 'Eliminar',
            cancelButtonText: "Cancelar",
            allowOutsideClick: false,
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: $.MisUrls.url._EliminaDepositoServidor,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { nombreArchivo: nombreArchivo, direccion: opathArchivo },
                    success: function (result) {
                        if (result)
                            location.reload(true);
                        else
                            mensajeGeneral("¿Eliminar?", "No puede anular el archivo", "success");
                    },
                    error: function (jqXHR, textStatus, error) {
                        $('.model-status').text("Estado: Error inesperado", "error");
                    }
                });
            }
        })

    }
    else {
        mensajeGeneral("¿Eliminar?", "No puede eliminar el registro", "error");
    }
}

function mensajeGeneralIcono(titulo, contenido, icono) {
    Swal.fire({
        title: titulo,
        text: contenido,
        icon: icono,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
    });
}