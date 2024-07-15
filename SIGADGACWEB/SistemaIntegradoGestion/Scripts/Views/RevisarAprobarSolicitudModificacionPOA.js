var tabladata;
$(document).ready(function () {
    loadDataTable();
    activaCamposComprobanteModificacion();
    $('#NumeroCUR').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $('#btnEnviar').click(function () {
        var canioSolicitud = $('#AnioSolicitud').val();
        var numSolicitud = $('#NumeroSolicitud').val();
        var estadoAutorizado = $('#EstadoAutorizacion').val();
        var cObservacion1 = $('#ObservacionAutorizacion1').val();
        var cObservacion2 = $('#ObservacionAutorizacion2').val();


        var cVista = $('#vista').val();
        try {
            if (canioSolicitud.trim().length > 0 && parseInt(numSolicitud) > 0 && estadoAutorizado.trim().length > 0) {

                if (estadoAutorizado == '0') {
                    mensajeGeneral("Revisar/Aprobar Solicitud Modificación POA", "Debe seleccionar el estado de autorización");
                    return false;
                }

                $('#loadingBuscar').show();
                $.ajax({
                    url: $.MisUrls.url._RevisarAprobarSolicitudModificacionPoaEstadoAutorizado,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { canioSol: canioSolicitud, numSolicitud: parseInt(numSolicitud), cEstadoAut: estadoAutorizado, cObservacion1: cObservacion1, cObservacion2: cObservacion2, vista: cVista },
                    success: function (result) {
                        if (result == "ok") {
                            Swal.fire({
                                title: 'Revisar/Aprobar (Eleborar informe de  viabilidad Modificacion POA)',
                                text: "La operación fue realizada con éxito",
                                icon: 'success',
                                showCancelButton: false,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#C5C7CF',
                                confirmButtonText: 'Envíar',
                                allowOutsideClick: false,
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    location.reload(true);
                                }
                            });
                        }
                        else {
                            $('#loadingBuscar').hide();
                            mensajeGeneral("Aprobar informe viabilidad", result);
                        }

                    },
                    error: function (jqXHR, textStatus, error) {
                        $('#loadingBuscar').hide();
                        mensajeGeneral("Error", error);
                    }
                });
            }
            else {
                mensajeGeneral("No puede aprobar la solicitud de la modificación POA.");
            }


        } catch (e) {
            mensajeGeneral(e);
        }



    });

    $('#imprimir-viabilidad').click(function () {
        imprimirDocumentoViabilidadModPOA();
    });

    $('#imprimirPoaPropuesto').click(function () {
        var canio = $("#AnioSolicitud").val();
        var numSol = $("#NumeroSolicitud").val();
        if (canio.trim().length > 0 && numSol.trim().length > 0) {
            $("#loadingBuscar").css("display", "block");
            $.ajax({
                url: $.MisUrls.url._ImprimirPoaPropuestoInformeViabilidad,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                cache: false,
                data: { canioSol: canio, numSolicitud: parseInt(numSol) },
                success: function (result) {
                    if (result == "ok") {
                        Swal.fire({
                            title: 'Informe viabilidad Modificacion POA',
                            text: "Se generó el POA propuesto e Informe de viabilidad con éxito",
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#C5C7CF',
                            confirmButtonText: 'Aceptar',
                            allowOutsideClick: false,
                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.reload(true);
                            }
                        });
                    }
                    else {
                        $("#loadingBuscar").css("display", "none");
                        mensajeGeneral("Aprobar informe viabilidad", result);
                    }

                },
                error: function (jqXHR, textStatus, error) {
                    $("#loadingBuscar").css("display", "none");
                    mensajeGeneral("Error", error);
                }
            });
        }
    });

    //Se ejecuta cuando el usuario de Director DPGE
    $('#btnEnviarAprobarInformeViabilidad').click(function () {
        var canio = $("#AnioSolicitud").val();
        var numSol = $("#NumeroSolicitud").val();
        var cEstAutorizacion = $("#EstadoAutorizacion").val();
        var cObserAutorizacion1 = $("#ObservacionAutorizacion1").val();
        var cObserAutorizacion2 = $("#ObservacionAutorizacion2").val();
        if (canio.trim().length > 0 && numSol.trim().length > 0) {
            $.ajax({
                url: $.MisUrls.url._AprobarInformeViabilidadModificacionPao,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                cache: false,
                data: { canio: canio, numSolicitud: parseInt(numSol), estAutorizacion: cEstAutorizacion, obsercacionAutorizacion1: cObserAutorizacion1, obsercacionAutorizacion2: cObserAutorizacion2 },
                success: function (result) {
                    if (result == "ok") {
                        Swal.fire({
                            title: 'Aprobar informe viabilidad Modificacion POA',
                            text: "La operación fue realizada con éxito",
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#C5C7CF',
                            confirmButtonText: 'Aceptar',
                            allowOutsideClick: false,
                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.reload(true);
                            }
                        });
                    }
                    else
                        mensajeGeneral("Aprobar informe viabilidad", result);
                },
                error: function (jqXHR, textStatus, error) {
                    mensajeGeneral("Error", error);
                }
            });
        }

    });

    ///Se ejecuta cuando es un usuario de Director de DPGE
    $('#btnActualizaPoa').click(function () {
        var canio = $("#AnioSolicitud").val();
        var numSol = $("#NumeroSolicitud").val();
        var numeroCUR = $("#NumeroCUR").val();
        var fechaCUR = $("#FechaCUR").val();
        var cObserAutorizacion1 = $("#ObservacionAutorizacion1").val();
        var cObserAutorizacion2 = $("#ObservacionAutorizacion2").val();
        var _tipoSolicitud = $("#TipoSolicitud").val();
        if (canio != "" && parseInt(numSol) > 0) {
            if (_tipoSolicitud.trim() != "MDP") {
                if (parseInt(numeroCUR) <= 0) {
                    mensajeGeneral("", "Debe ingresar el número de comprobante es obligatorio");
                    return false;
                }

                if (fechaCUR.length == 0) {
                    mensajeGeneral("", "No es una fecha valida");
                    return false;
                }
            }

            Swal.fire({
                title: 'Actualizar POA a partir de Modificación',
                text: "Esta seguro de actualizar el POA a partir de Modificación",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#C5C7CF',
                confirmButtonText: 'SI',
                cancelButtonText: "NO",
                allowOutsideClick: false,
            }).then((result) => {
                if (result.isConfirmed) {
                    $('#bntGrabarActualizar :input').attr('disabled', true);
                    $("#loadingGrabar").css("display", "block");
                    $.ajax({
                        url: $.MisUrls.url._ActualizarPoa,
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        cache: false,
                        data: { canio: canio, numSolicitud: parseInt(numSol), numCur: parseInt(numeroCUR), fechaCur: fechaCUR, obsercacionActualiza1: cObserAutorizacion1, obsercacionActualiza2: cObserAutorizacion2 },
                        success: function (result) {
                            if (result == "ok")
                                Swal.fire({
                                    title: '',
                                    text: "La operación fue realizada con éxito",
                                    icon: 'warning',
                                    showCancelButton: false,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#C5C7CF',
                                    confirmButtonText: 'Aceptar',
                                    allowOutsideClick: false,
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        location.reload(true);
                                    }
                                });
                            else {
                                $("#loadingGrabar").css("display", "none");
                                $('#bntGrabarActualizar :input').removeAttr('disabled');
                                mensajeGeneral("Modificacion POA", result);
                            }

                        },
                        error: function (jqXHR, textStatus, error) {
                            $('#bntGrabarActualizar :input').removeAttr('disabled');
                            $("#loadingGrabar").css("display", "none");
                            mensajeGeneral("Error", error);
                        }
                    });
                }
            });
        }

    });

    $('#btnLegalizarModificacionNegada').click(function () {
        var canio = $("#AnioSolicitud").val();
        var numSol = $("#NumeroSolicitud").val();

        if (canio != "" && parseInt(numSol) > 0) {
            $("#loadingBuscar").css("display", "block");
            $.ajax({
                url: $.MisUrls.url._LegalizaModificacionNegada,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                cache: false,
                data: { canio: canio, numSolicitud: parseInt(numSol) },
                success: function (result) {
                    if (result == "ok")
                        Swal.fire({
                            title: 'Legalizar modificación negada',
                            text: "La operación fue realizada con éxito",
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#C5C7CF',
                            confirmButtonText: 'Aceptar',
                            allowOutsideClick: false,
                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.reload(true);
                            }
                        });
                    else {
                        $("#loadingBuscar").css("display", "none");
                        mensajeGeneralIcono("Sin afectación presupuestaria", result, "warning");
                    }

                },
                error: function (jqXHR, textStatus, error) {
                    $("#loadingBuscar").css("display", "none");
                    mensajeGeneralIcono("Error", error, "error");
                }
            });
        }

    });


});

function activaCamposComprobanteModificacion() {
    var tipoSolicitud = $('#TipoSolicitud').val();
    if (tipoSolicitud.trim() != "MDP") {
        $("#campoModificacion").css("display", "block");
    }
    else {
        $("#campoModificacion").css("display", "none");
    }
}
function loadDataTable() {
    $('#tbDetalle').DataTable({
        scrollY: '420px',
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
            mensajeGeneralIcono("Descargar archivo", "El nombre del archivo en blanco.", "warning");
            $("#loadingBuscar").css("display", "none");
        }
    } catch (e) {
        mensajeGeneralIcono("Descargar archivo", "Hay un problema al descargar el archivo.", "error");
        $("#loadingBuscar").css("display", "none");
    }

}
function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        icon: 'warning',
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: "<ul >" + contenido + "</ul>",
        confirmButtonText: 'Aceptar'
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

function imprimirDocumentoViabilidadModPOA() {
    var canio = $("#AnioSolicitud").val();
    var numSol = $("#NumeroSolicitud").val();
    if (canio != "" && parseInt(numSol) > 0) {
        Swal.fire({
            title: 'Revisar aprobar solicitud Modificacion POA',
            text: "Esta seguro de envíar la solicitud para la verificación presupuesto  ",
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
                    url: $.MisUrls.url._ElaborarInformeViabilidadModificacionPOA,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { canio: canio, numSolicitud: parseInt(numSol) },
                    success: function (result) {
                        if (result == "ok")
                            location.reload(true);
                        else
                            mensajeGeneralIcono("Sin afectación presupuestaria", result, "warning");
                    },
                    error: function (jqXHR, textStatus, error) {
                        mensajeGeneralIcono("Error", error, "error");
                    }
                });
            }
        });
    }
}


function AprobarInformeViabilidad() {

    $('#formModalAprobarInformeViabilidad').modal('show');
}

function modalActualizarPoa() {

    $('#formModalActualizarPoa').modal('show');
}

function isValidDate(dateString) {
    // Analizar la cadena de fecha de entrada
    var date = Date.parse(dateString);

    // Compruebe si la fecha es válida pasándola a isNaN
    return !isNaN(date);
}


function validacion_fecha(variable) {

    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((variable.match(RegExPattern)) && (variable != '')) {
        return true;
    } else {
        return false;
    }
}