
$(document).ready(function () {
    loadDataTable();

    $('.cerrarVentana').click(function () {
        window.location.reload();
    });
});



function loadDataTable() {
    $('#tbDetalle').DataTable({
        "processing": true,
        scrollY: '500px',
        scrollCollapse: true,
        paging: false,
        fixedHeader: true,
        "order": [[0, 'desc'], [2, 'asc'], [4, 'asc'], [3, 'desc'] ],
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
        $("#btnAprobar").attr("disabled", true);
        $.ajax({
            url: $.MisUrls.url._CargaSolicitudCertificado,
            type: "GET",
            data: { "canio": canio, "numSolicitud": numSol },
            datatype: "json",
            contentType: "application/json",
            success: function (data) {
                oestadoAutorizado = data.EstadoAutorizacion;
                //if (data.EstadoAutorizacion == "AP") {
                $('#lblDescripcionActividad').text(JSON.stringify(data.DescripcionActividadEjecutar));
                var elementoBuscar = canio + numSol + "-signed";
                if (data.EstadoAutorizacion == "AP" || data.EstadoAutorizacion == "RN") {
                    $("#apruebaRevision").css("display", "none");
                    $("#btnAprobar").attr("disabled", true);
                    mensajeGeneral("Revisar Solicitud Certificado POA", " Solicitud fue Aprobada por Director (a)");
                }
                else {
                    $("#apruebaRevision").css("display", "block");
                    $("#btnAprobar").removeAttr("disabled");
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
                                $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(value.NombreArchivo) + ")'>" + JSON.parse(JSON.stringify(value.NombreArchivo)) + "</a></li>");

                            });
                        }
                        else {
                            $("#apruebaRevision").css("display", "none");
                            $("#btnAprobar").attr("disabled", true);
                            mensajeGeneral("Adjuntos", "No hay documentos habilitantes adjuntos");
                        }

                        $('#formModalEnviar').modal('show');

                    }
                });
            }
        });

    }
    else {
        mensajeGeneral("Revisar Solicitud Certificado POA", "Los campos de buesqueda están en blanco");
    }

}

function abrirArchivo(fileName) {
    var nombreArchivo = fileName;
    var opathArchivo = $('#pathArchivo').text();

    var _extensionArchivo = "";
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf") {
            var texto = $.MisUrls.url._VisualizarDocumento + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
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

//Descarga el archivo
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
    if (_EstadoAprobado != "0") {
        $('#loadingBuscar').show();
        $.ajax({
            url: $.MisUrls.url._RevisaSolicitudCertificadoPOA,
            type: "GET",
            data: { "canio": canio, "numSolicitud": numSol, "estadoAprobacion": _EstadoAprobado, "observacion": _observacion, "observacion1": _observacion1, "observacion2": _observacion2 },
            datatype: "json",
            contentType: "application/json;charset=UTF-8",
            success: function (result) {
                if (result.resultado == true) {
                    mensajeGeneral("Revisar Solicitud del Certificado POA", "La operación fue realizada con exito.");
                }
                else {
                    $('#loadingBuscar').hide();
                    mensajeGeneral("Revisar Solicitud del Certificado POA", "No puedo grabar el registro");
                }
                $('#loadingBuscar').hide();
            },
            error: function (errormessage) {
                $('#loadingBuscar').hide();
                mensajeGeneral("Revisar Solicitud del Certificado POA", "Error, " + errormessage);
            }
        });
    }
    else {
        mensajeGeneral("Revisar Solicitud Certificado POA", "Debe seleccionar el estado");
    }
    return false;
}



function CargaTodosDocumentosDirectorio(opathArchivo, nombreArchivo) {
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
                    $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(nombreArchivo) + ")'>" + JSON.stringify(nombreArchivo) + "</a></li>");
                }
            })
        },
        error: function (errormessage) {
            mensajeGeneral("Documentos adjuntos", "Hay un problema al cargar los archivos." + errormessage);
        }
    });
}
function mensajeGeneral(titulo, contenido) {
    Swal.fire({
        icon: 'warning',
        title: "<p style='width: 100 %;'>" + titulo + "</p>",
        html: "<ul >" + contenido + "</ul>",
        confirmButtonText: 'Aceptar'
    });
}