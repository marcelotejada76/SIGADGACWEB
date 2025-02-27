
$(document).ready(function () {
    loadDataTable();

    $('.cerrarVentana').click(function () {
        window.location.reload();
    });
});



function loadDataTable() {
    $('#tbDetalle').DataTable({
        "processing": true,
        scrollY: '580px',
        scrollCollapse: true,
        paging: false,
        fixedHeader: true,
        "order": [[0, 'desc'], [3, 'desc']],
        "language": {
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
        $("#panelCertificado").css("display", "none");
        $("#btnAprobar").attr('disabled', 'disabled');
        $.post($.MisUrls.url._VerificaExisteFirmaElectronica, function (htmlCia) {
            if (htmlCia.resultado) {
                $.ajax({
                    url: $.MisUrls.url._CargaSolicitudCertificado,
                    type: "GET",
                    data: { "canio": canio, "numSolicitud": numSol },
                    datatype: "json",
                    contentType: "application/json",
                    success: function (data) {
                        if (data.EstadoAutorizacion == "AP") {
                            mensajeGeneral("Revisar o aprobar certificado POA", "Solicitud fue aprobada por DPGE");
                        }
                        else if (data.EstadoAutorizacion == "RN") {
                            mensajeGeneral("Revisar o aprobar certificado POA", "La solicitud esta negada por DPGE");
                        }
                        else {
                            $("#panelCertificado").css("display", "block");
                            $("#btnAprobar").removeAttr("disabled");
                        }
                        CargaTodosArchivos(codDireccion, canio, ctipo, numSol);
                    }
                });
            }
            else {
                $("#btnAprobar").attr('disabled', 'disabled');
                mensajeGeneral("Firma electrónica", "No tiene cargado el certificado de la firma electrónica");
            }
        });
    }
    else {
        $("#btnAprobar").attr('disabled', 'disabled');
        mensajeGeneral("Revisar o aprobar certificado POA", "Los campos de busqueda estan en blanco.");
    }

}

function CargaTodosArchivos(codDireccion, canio, ctipo, numSol) {
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
                    //ndice = value.NombreArchivo.indexOf(elementoBuscar);                                
                    $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(value.NombreArchivo) + ")'>" + JSON.stringify(value.NombreArchivo) + "</a></li>");

                });
            }
            else {
                mensajeGeneral("Documentos adjuntos", "No hay documentos habilitantes adjuntos");
                $("#panelCertificado").css("display", "none");
                $("#btnAprobar").attr('disabled', 'disabled');
            }

            $('#formModalEnviar').modal('show');

        }
    });
    return false;
}

function abrirArchivo(fileName) {
    var nombreArchivo = fileName;
    var opathArchivo = $('#pathArchivo').text();
    var _extensionArchivo = "";
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf") {
            var texto = $.MisUrls.url._VisualizarDocumentoPOA + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
            $("#iframeCetificado").attr("src", texto);
            $('#loadingBuscar').hide();
        }
        else {
            descargarArchivo(nombreArchivo, opathArchivo);
        }
    }

}

//Obtengo la extension del archivo
function getExtensionArchivo(filename) {
    return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
}

//Descargo el archivo
function descargarArchivo(onombreArchivo, odireccion) {
    try {
        if (onombreArchivo.length > 0 && odireccion.length > 0) {
            window.location = $.MisUrls.url._DescargarArchivo + "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
        }
        else {
            mensajeGeneral("Descargar archivo", "El nombre del archivo en blanco.");
        }
    } catch (e) {
        mensajeGeneral("Descargar archivo", "Hay un problema al descargar el archivo.");
    }

}

function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        icon: 'warning',
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html:  contenido,
        confirmButtonText: 'Aceptar'
    });
}

function abrir_Certificado() {
    var url = $.MisUrls.url._AbrirCertificadoPOA;
    window.location.href = url;

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
    var _observacion2 = $('').val();
    $("#btnAprobar").attr('disabled', 'disabled');    
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
                            FirmaCertificadoPOA(canio, numSol, opathArchivo, _EstadoAprobado, _observacion, _observacion1);
                            $("#btnAprobar").attr('disabled', 'disabled');  
                        }
                        else {
                            $('#loadingBuscar').hide();
                            mensajeGeneral("Revisar o Aprobar Solicitud", "Opción habilitada para Rol=Director DPGE");
                            $("#btnAprobar").attr('disabled', 'disabled');  
                            //$("#btnAprobar").removeAttr("disabled");
                        }

                    }
                    else {
                        $('#loadingBuscar').hide();
                        $("#btnAprobar").attr('disabled', 'disabled');  
                        mensajeGeneral("Certificado electronico", '<p>No tiene cargado el certificado de la firma digital, ingrese en el siguiente enlace</p><br /> <a href="#" onclick="abrir_Certificado()">Cargar el certificado de la firma digital</a>');
                    }
                },
                error: function (errormessage) {
                    $('#loadingBuscar').hide();
                    $("#btnAprobar").attr('disabled', 'disabled');  
                    mensajeGeneralIco("Certificado electronico", "Error, " + errormessage, "error");
                }
            });
        }
        else {
            $('#loadingBuscar').hide();
            mensajeGeneral("Revisar o Aprobar Solicitud", "Opción habilitada para Rol=Director DPGE");            
        }
    }
    else if (_EstadoAprobado == "0") {
        $('#loadingBuscar').hide();
        mensajeGeneral("Revisar o Aprobar Solicitud", "Debe seleecionar el estado de aprobación");
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
                    //$("#btnAprobar").removeAttr("disabled");
                    mensajeGeneral("Revisar o Aprobar Solicitud", "La operación fue realizada con exito");
                }
                else {
                    $('#loadingBuscar').hide();
                    mensajeGeneral("Revisar o Aprobar Solicitud", "No puedo grabar el registro");
                }
                $('#loadingBuscar').hide();
            },
            error: function (errormessage) {
                $('#loadingBuscar').hide();
                mensajeGeneral("Revisar o Aprobar Solicitud", "Error, " + errormessage);
            }
        });
    }

    return false;
}

function FirmaCertificadoPOA(canio, numSol, opathArchivo, estaut, observacion, observacion1) {
    $.ajax({
        url: $.MisUrls.url._GeneraReportePDF,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: { canio: canio, numSolicitud: parseInt(numSol), cdireccion: opathArchivo, estAut: estaut, cobservacion: observacion, cobservacion1: observacion1 },
        success: function (result) {
            if (result.length > 0) {
                abrirArchivo(result);
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
                        mensajeGeneralIco("Mensaje", "Error, Exportar el reporte" + errormessage, "warning"); 
                    }
                });
            }
            else {
                $('#loadingBuscar').hide();
                mensajeGeneralIco("Mensaje", "No se pudo firmar la Solicitud del Certificado POA", "warning");                  
            }

        },
        error: function (errormessage) {
            mensajeGeneralIco("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");           
        }
    });
}

function ReimprimirCertificadoPOA() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    var opathArchivo = $('#pathArchivo').text();

    $.ajax({
        url: $.MisUrls.url._reimprimirExportToPDF,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: { canio: canio, numSolicitud: parseInt(numSol), cdireccion: opathArchivo},
        success: function (result) {
            if (result.length > 0) {
                abrirArchivo(result);
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
                        mensajeGeneralIco("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
                    }
                });
            }
            else {
                $('#loadingBuscar').hide();
                mensajeGeneralIco("Mensaje", "No se pudo firmar la Solicitud del Certificado POA", "warning");
            }

        },
        error: function (errormessage) {
            mensajeGeneralIco("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
        }
    });
}


function ExportaToPDFPrueba() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    if (canio != "" && parseInt(numSol)) {
        jQuery.ajax({
            url: $.MisUrls.url._PruebaReporteCertificadoPOA,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: { canio: canio, numSolicitud: parseInt(numSol)},
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                } else {
                    Swal.fire("Mensaje", "No se pudo eliminar el rol", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });
    }
}


function mensajeGeneralIco(titulo, contenido, _ico) {
    Swal.fire({
        icon: _ico,
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: contenido,
        confirmButtonText: 'Aceptar'
    });
}