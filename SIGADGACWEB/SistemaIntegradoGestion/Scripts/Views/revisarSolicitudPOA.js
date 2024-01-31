
$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    $('#tbDetalle').DataTable({
        "processing": true,
        scrollY: '400px',
        scrollCollapse: true,
        paging: false,
        fixedHeader: true,
        "order": [[0, 'desc'], [1, 'desc']],
        "language": {
            "url": $.MisUrls.url._datatable_spanish
        },
        responsive: true
    });


}

function enviarAprobarSolicitud(codDireccion, canio, ctipo, numSol) {

    if (codDireccion.trim().length > 0 && canio.trim().length > 0 && ctipo.trim().length > 0 && numSol.trim().length > 0) {
        $('#codanio').val(canio);
        $('#numSolicitud').val(numSol);
        $('#browser').empty();
        $("#iframeCetificado").attr("src", "");
        $('#pathArchivo').html("/" + codDireccion + "/" + canio + "/" + ctipo + "/" + numSol);
        $('#lblDescripcionActividad').val('');
        _codigoSubsistema = $('#codigoSubsistema').val();
        _codigoRol = $('#codigoRol').val();
        let existeRegistro = 0;
        var oestadoAutorizado = "";
        $("#btnAprobar").attr("disabled", true);
        $.ajax({
            url: $.MisUrls.url._CargaSolicitudCertificado,
            type: "GET",
            data: { "canio": canio, "numSolicitud": numSol },
            datatype: "json",
            contentType: "application/json",
            success: function (data) {
                if (data.EstadoAutorizacion.trim().length > 0) {
                    $("#idEstado").val(data.EstadoAutorizacion);
                }
                else {
                    $("#idEstado").val("0");
                }
               
                oestadoAutorizado = data.EstadoAutorizacion;
                //if (data.EstadoAutorizacion == "AP") {
                $('#lblDescripcionActividad').text(JSON.stringify(data.DescripcionActividadEjecutar));
                var elementoBuscar = canio + numSol + "-signed";
                if (data.EstadoAutorizacion == "AP" || data.EstadoAutorizacion == "RN") {
                    $("#apruebaRevision").css("display", "none");
                }
                else {
                    $("#apruebaRevision").css("display", "block");
                }
                $.ajax({
                    url: $.MisUrls.url._DocumentosHabilitantes,
                    type: "GET",
                    data: { "cdireccion": codDireccion, "canio": canio, "tipoSolicitud": ctipo, "numSolicitud": numSol },
                    datatype: "json",
                    contentType: "application/json",
                    success: function (data) {
                        if (data.length > 0) {
                            $.each(data, function (index, value) {
                                //en caso de existir se asigna la posicion
                                indice = value.NombreArchivo.indexOf(elementoBuscar);
                                $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(value.NombreArchivo) + ")'>" + JSON.stringify(value.NombreArchivo) + "</a></li>");
                                //si existe
                                if (indice > 0) {
                                    existeRegistro = 1;
                                    $("#btnAprobar").attr("disabled", true);
                                }
                                else {
                                    if (existeRegistro == 0) {
                                        $("#btnAprobar").removeAttr("disabled");
                                    }
                                }
                            });
                        }

                        $('#formModalEnviar').modal('show');

                    }
                });

                //if (_codigoSubsistema == "DIRE" && _codigoRol == "DPGE") {
                //    $("#btnAprobar").removeAttr("disabled");
                //}
                //}
            }
        });

    }
    else {
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Datos adjuntos</p>",
            html: "<ul class='text-danger text-left'>No existe información de la documentación.</ul>",
            confirmButtonText: 'Aceptar'
        });
    }

}

function abrirArchivo(fileName) {
    var nombreArchivo = fileName;
    var opathArchivo = $('#pathArchivo').text();
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var texto = $.MisUrls.url._VisualizarDocumento + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
        $("#iframeCetificado").attr("src", texto);
        $('#loadingBuscar').hide();
    }
}

function Guardar() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    Swal.fire({
        title: "Solicitud Certificación POA",
        text: "¿Está seguro de enviar aprobar la solicitud de certificación POA?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Aprobar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {

        }
    });
}

function ExportaToPDF() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    var opathArchivo = $('#pathArchivo').text();
    var _EstadoAprobado = $('#idEstado').val();
    var _codigoSubsistema = $('#codigoSubsistema').val();
    var _codigoRol = $('#codigoRol').val();
    var _observacion = $('#txtObservacion').val();
    var _observacion1 = $('#txtObservacion1').val();
    var _observacion2 = $('#txtObservacion2').val();
    $('#loadingBuscar').show();
    if (_EstadoAprobado == "AP") {
        if (_codigoSubsistema == "DPGE" && _codigoRol == "DIRE") {
            $.ajax({
                url: $.MisUrls.url._VerificaExisteCertificadoFirma,
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                success: function (result) {
                    if (result.resultado == true) {
                        if (_codigoSubsistema == "DPGE" && _codigoRol == "DIRE") {
                            $("#btnAprobar").removeAttr("disabled");
                            FirmaCertificadoPOA(canio, numSol, opathArchivo, _EstadoAprobado, _observacion);
                            $("#btnAprobar").attr("disabled", true);
                        }
                        else {
                            $('#loadingBuscar').hide();
                            Swal.fire({
                                icon: 'warning',
                                title: "<p style='width: 100 %; font-size: 14px;'>Revisar o Aprobar Solicitud</p>",
                                html: "<ul class='text-danger text-left'>Opción habilitada para Rol=Director DPGE</ul>",
                                confirmButtonText: 'Aceptar'
                            });
                        }

                    }
                    else {
                        $('#loadingBuscar').hide();
                        Swal.fire({
                            icon: 'warning',
                            title: "<p style='width: 100 %; font-size: 14px;'>Certificado electronico</p>",
                            html: "<ul class='text-danger text-left'>No tiene cargado el certificado de la firma digital </ul>",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                },
                error: function (errormessage) {
                    $('#loadingBuscar').hide();
                    Swal.fire({
                        icon: 'warning',
                        title: "<p style='width: 100 %; font-size: 14px;'>Certificado electronico</p>",
                        html: "<ul class='text-danger text-left'>Error, " + errormessage + "</ul>",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }
        else {
            $('#loadingBuscar').hide();
            Swal.fire({
                icon: 'warning',
                title: "<p style='width: 100 %; font-size: 14px;'>Revisar o Aprobar Solicitud</p>",
                html: "<ul class='text-danger text-left'>Opción habilitada para Rol=Director DPGE</ul>",
                confirmButtonText: 'Aceptar'
            });
        }
    }
    else if (_EstadoAprobado == "0") {
        $('#loadingBuscar').hide();
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Revisar o Aprobar Solicitud</p>",
            html: "<ul class='text-danger text-left'>Debe seleecionar el estado de aprobación</ul>",
            confirmButtonText: 'Aceptar'
        });
    }
    else {
        $.ajax({
            url: $.MisUrls.url._RevisaSolicitudCertificadoPOA,
            type: "GET",
            data: { "canio": canio, "numSolicitud": numSol, "estadoAprobacion": _EstadoAprobado, "observacion": _observacion, "observacion1": _observacion1, "observacion2": _observacion2 },
            datatype: "json",
            contentType: "application/json;charset=UTF-8",
            success: function (result) {
                if (result.resultado == true) {
                    window.location.reload();
                }
                else {
                    $('#loadingBuscar').hide();
                    Swal.fire({
                        icon: 'warning',
                        title: "<p style='width: 100 %; font-size: 14px;'>Revisar Solicitud del Certificado POA</p>",
                        html: "<ul class='text-danger text-left'>No puedo grabar el registro </ul>",
                        confirmButtonText: 'Aceptar'
                    });
                }
            },
            error: function (errormessage) {
                $('#loadingBuscar').hide();
                Swal.fire({
                    icon: 'warning',
                    title: "<p style='width: 100 %; font-size: 14px;'>Certificado electronico</p>",
                    html: "<ul class='text-danger text-left'>Error, " + errormessage + "</ul>",
                    confirmButtonText: 'Aceptar'
                });
            }
        });
    }

    return false;
}

function FirmaCertificadoPOA(canio, numSol, opathArchivo, estaut, observacion) {
    $.ajax({
        url: $.MisUrls.url._GeneraReportePDF,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: { canio: canio, numSolicitud: parseInt(numSol), cdireccion: opathArchivo, estAut: estaut, cobservacion: observacion },
        success: function (result) {
            if (result.length > 0) {
                abrirArchivo(result);
                setTimeout(function () {
                    frame = document.getElementById("frmPDF");
                    framedoc = frame.contentWindow;
                    framedoc.focus();
                    framedoc.print();
                }, 1000);
                //Carga todos os archivos 
                $.ajax({
                    url: $.MisUrls.url._CargaTodosArchivosDirectory,
                    type: "GET",
                    contentType: "application/json;charset=UTF-8",
                    data: { direccionDirectory: opathArchivo },
                    success: function (result) {
                        $("#browser").html("");
                        $.each(result, function (i, item) {
                            if (item.NombreArchivo.length > 0) {
                                $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(item.NombreArchivo) + ")'>" + JSON.stringify(item.NombreArchivo) + "</a></li>");
                            }
                        })
                    },
                    error: function (errormessage) {
                        Swal.fire("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
                    }
                });
            }
            else {
                $('#loadingBuscar').hide();
                Swal.fire("Mensaje", "No se pudo firmar la Solicitud del Certificado POA", "warning");
            }

        },
        error: function (errormessage) {
            Swal.fire("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
        }
    });
}
