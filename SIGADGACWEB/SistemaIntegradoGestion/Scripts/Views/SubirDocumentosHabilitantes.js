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
        $("#registerForm").submit();
    });

    $('#enviarSolicitudPresupuesto').click(function () {
        enviaSolicitudVerificacionPresupuesto();
    });

    $('#aprobarSolicitudPresupuesto').click(function () {
        aprobarSolicitudVerificacionPresupuesto();
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
    $("#aprobarSolicitudPresupuesto").attr('disabled', 'disabled');
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF") {
            $("#nombre-archivo").val(nombreArchivo);
            $("#aprobarSolicitudPresupuesto").removeAttr('disabled');
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

function enviaSolicitudVerificacionPresupuesto() {
    var canio = $("#AnioSolicitud").val();
    var numSol = $("#NumeroSolicitud").val();
    if (canio != "" && parseInt(numSol) > 0) {
        Swal.fire({
            title: '',
            text: "Esta seguro de envíar la solicitud para la verificación presupuestaria",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#C5C7CF',
            confirmButtonText: 'Envíar',
            cancelButtonText: "Cancelar",
            allowOutsideClick: false,
        }).then((result) => {
            if (result.isConfirmed) {
                $("#loadingBuscar").css("display", "block");
                $.ajax({
                    url: $.MisUrls.url._EnviaSolicitudVerificacionPresupuestario,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { canio: canio, numSolicitud: parseInt(numSol) },
                    success: function (result) {
                        if (result == "ok") {
                            Swal.fire({
                                title: 'Modificación POA',
                                text: "Solicitud enviada correctamente a Presupuesto",
                                icon: 'success',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Aceptar',
                                allowOutsideClick: false,
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    location.reload(true);
                                }
                            });
                        }
                        else {
                            Swal.fire({
                                title: 'Afectación presupuestaria',
                                text: result,
                                icon: 'warning',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: 'Aceptar',
                                allowOutsideClick: false,
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    location.reload(true);
                                }
                            });
                        }

                    },
                    error: function (jqXHR, textStatus, error) {
                        mensajeGeneral("Error", error);
                    }
                });
            }
        });
    }
}

function aprobarSolicitudVerificacionPresupuesto() {
    var canio = $("#AnioSolicitud").val();
    var numSol = $("#NumeroSolicitud").val();
    var nombreArchivo = $("#nombre-archivo").val();
    if (canio != "" && parseInt(numSol) > 0) {
        Swal.fire({
            title: 'Modificación POA',
            text: "¿Esta seguro de envíar la Solicitud de Modificación POA a DPGE: " + nombreArchivo,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#C5C7CF',
            confirmButtonText: 'Aprobar-Firmar',
            cancelButtonText: "Cancelar",
            allowOutsideClick: false,
        }).then((result) => {
            if (result.isConfirmed) {
                $("#loadingBuscar").css("display", "block");
                $.ajax({
                    url: $.MisUrls.url._VerificaExisteFirmasElectronicas,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { canio: canio, numSolicitud: parseInt(numSol) },
                    success: function (result) {
                        if (result.resultado == true)
                            $.ajax({
                                url: $.MisUrls.url._EnviaAprobarSolicitudModificacionPOAaDPGE,
                                type: "GET",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                cache: false,
                                data: { canio: canio, numSolicitud: parseInt(numSol), nombreArchivo: nombreArchivo },
                                success: function (result) {
                                    if (result.resultado == true) {
                                        Swal.fire({
                                            title: 'Modificación POA',
                                            text: "La solicitud se guardo correctamente" + nombreArchivo,
                                            icon: 'success',
                                            showCancelButton: false,
                                            confirmButtonColor: '#3085d6',
                                            confirmButtonText: 'Aceptar',
                                            allowOutsideClick: false,
                                        }).then((result) => {
                                            if (result.isConfirmed) {
                                                location.reload(true);
                                            }
                                        })
                                    }
                                    else {
                                        $("#loadingBuscar").css("display", "none");
                                        mensajeGeneral("Firma electrónica", result.mensaje);
                                    }

                                },
                                error: function (jqXHR, textStatus, error) {
                                    $("#loadingBuscar").css("display", "none");
                                    $('.model-status').text("Estado: Error inesperado");
                                }
                            });
                        else {
                            $("#loadingBuscar").css("display", "none");
                            mensajeGeneral("Firma electrónica", result.mensaje);
                        }

                    },
                    error: function (jqXHR, textStatus, error) {
                        $("#loadingBuscar").css("display", "none");
                        $('.model-status').text("Estado: Error inesperado");
                    }
                });

            }
        })
    }
}


function eliminar(fileName) {
    var $nombreArchivo = JSON.parse(fileName);
    if ($nombreArchivo != null) {
        let directory = $("#Directory").val();
        Swal.fire({
            title: '¿Eliminar?',
            text: "¿Está seguro de que eliminar el archivo del sitio?\n" + directory + "\\" + $nombreArchivo,
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
                    url: $.MisUrls.url._EliminaArchivoSinAfectacion,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { nombreArchivo: $nombreArchivo, direccion: directory },
                    success: function (result) {
                        if (result)
                            location.reload(true);
                        else
                            mensajeGeneral("¿Eliminar?", "No puede anular el archivo");
                    },
                    error: function (jqXHR, textStatus, error) {
                        $('.model-status').text("Estado: Error inesperado");
                    }
                });
            }
        })

    }
    else {
        mensajeGeneral("¿Eliminar?", "No puede anular la Solicitud de Vuelo");
    }
}

function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        title: titulo,
        text: contenido,
        icon: 'success',
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
    });
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
