
$(document).ready(function () {
    loadDataTable();

    $('.radSolicitud').click(function () {
        var checked = $(this).attr('checked', true);
        if (checked) {
            HabilitarTipoSolictudCampos();
        }
    });

    $('.radioCertificado').click(function () {
        var checked = $(this).attr('checked', true);
        if (this.checked) {
            var _tipoCertificado
            ud = $("input[type='radio'].radioCertificado:checked").val();
            $("#panelCertificado").removeAttr("disabled");
        }
    });

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
        info: true,
        fixedHeader: true,
        "order": [[0, 'desc'], [1, 'desc']],
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

function enviarAprobarSolicitud(codDireccion, canio, ctipo, cestSol, numSol, estAut) {
    if (codDireccion.trim().length > 0 && canio.trim().length > 0 && ctipo.trim().length > 0 && numSol > 0) {
        $('#codanio').val(canio);
        $('#numSolicitud').val(numSol);
        $('#browser').empty();
        var _codRol = $('#codRol').val();
        $("#iframeCetificado").attr("src", "");
        $('#pathArchivo').html("/" + codDireccion + "/" + canio + "/" + ctipo + "/" + numSol);
        $("#btnGrabarEnviar").attr('disabled', 'disabled');
        var errorMensaje = "No se puede Aprobar, revise parámetros";
        $.post($.MisUrls.url._VerificaExisteFirmaElectronica, function (htmlCia) {
            if (htmlCia.resultado) {
                $.ajax({
                    url: $.MisUrls.url._CargaSolicitudCertificado,
                    type: "GET",
                    data: { "canio": canio, "numSolicitud": numSol },
                    datatype: "json",
                    contentType: "application/json",
                    success: function (data) {
                        $('#lblDescripcionActividad').text(JSON.stringify(data.DescripcionActividadEjecutar));
                        $.ajax({
                            url: $.MisUrls.url._DocumentosHabilitantes,
                            type: "GET",
                            data: { "cdireccion": codDireccion, "canio": canio, "tipoSolicitud": ctipo, "numSolicitud": numSol },
                            datatype: "json",
                            contentType: "application/json",
                            success: function (data) {
                                const arrayAut = ['AP', 'RA', 'RN'];
                                var estadoAut = arrayAut.includes(estAut);
                                if (cestSol == 'NS' && estadoAut != 'AP' && estadoAut != 'RA' && estadoAut != 'RN') {
                                    if (_codRol == 'DIRE') {
                                        if (data.length > 0) {
                                            $("#btnGrabarEnviar").removeAttr('disabled');
                                        }
                                        else {
                                            mensajeGeneral("Revisar y Aprobar Solicitud", "No hay documentos habilitantes adjuntos");    
                                        }
                                    }
                                    else {
                                        mensajeGeneral("Revisar y Aprobar Solicitud", "Opción habilitada para Rol=Director");                                          
                                    }
                                }
                                else {
                                    mensajeGeneral("Revisar y Aprobar Solicitud", "Solicitud ya está en trámite en DPGE");                                   
                                }
                                $.each(data, function (index, value) {
                                    $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(value.NombreArchivo) + ")'>" + JSON.stringify(value.NombreArchivo) + "</a></li>");
                                });
                                $('#formModalEnviar').modal('show');

                            }
                        });

                    }
                });
            }
            else {
                mensajeGeneral("Firma electrónica", "No tiene cargado el certificado de la firma electrónica");               
            }
        });

    }
    else {
        mensajeGeneral("Datos adjuntos", "No existe información de la documentación.");       
    }
}

function cargaDocumentacionSolicitudCertificado(codDireccion, canio, ctipo, cestSol, numSol, estAut) {
    if (canio.trim().length > 0 && numSol > 0) {
        $('#codanio').val(canio);
        $('#numSolicitud').val(numSol);
        $('#browser').empty();
        var _codRol = $('#codRol').val();
        $("#iframeCetificado").attr("src", "");
        $('#pathArchivo').html("/" + codDireccion + "/" + canio + "/" + ctipo + "/" + numSol);
        $.ajax({
            url: $.MisUrls.url._CargaSolicitudCertificado,
            type: "GET",
            data: { "canio": canio, "numSolicitud": numSol },
            datatype: "json",
            contentType: "application/json",
            success: function (data) {
                $('#lblDescripcionActividad').text(JSON.stringify(data.DescripcionActividadEjecutar));
                $.ajax({
                    url: $.MisUrls.url._DocumentosHabilitantes,
                    type: "GET",
                    data: { "cdireccion": codDireccion, "canio": canio, "tipoSolicitud": ctipo, "numSolicitud": numSol },
                    datatype: "json",
                    contentType: "application/json",
                    success: function (data) {
                        const arrayAut = ['AP', 'RA', 'RN'];
                        var estadoAut = arrayAut.includes(estAut);
                        if (data.length == 0 && cestSol != "NS") {
                            $("#btnGrabarEnviar").attr('disabled', 'disabled');
                        }
                        else if (data.length > 0 && cestSol == "NS" && !estadoAut && _codRol == 'DIRE') {
                            $("#btnGrabarEnviar").removeAttr('disabled');
                        }
                        else {
                            $("#btnGrabarEnviar").attr('disabled', 'disabled');

                        }
                        $.each(data, function (index, value) {
                            $("#browser").append("<li><a href='#' onclick='abrirArchivo(" + JSON.stringify(value.NombreArchivo) + ")'>" + JSON.stringify(value.NombreArchivo) + "</a></li>");
                        });
                        $('#formModalEnviar').modal('show');

                    }
                });

            }
        });

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
        title: "<p style='width: 100 %;'>" + titulo +"</p>", 
        html: "<ul class='text-left'>" + contenido+ "</ul>",
        confirmButtonText: 'Aceptar'
    });
}

function Guardar() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    $('#loadingBuscar').show();   
    Swal.fire({
        title: "Solicitud Certificación POA",
        text: "¿Está seguro de enviar aprobar la solicitud del certificación POA?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Aprobar",
        cancelButtonText: "Cancelar",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: $.MisUrls.url._EnviaAprobarSolicitudCertificadoPOA,
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                data: { canio: canio, numSolicitud: parseInt(numSol) },
                success: function (result) {
                    if (result.resultado) {
                        $.post($.MisUrls.url._ObtieneSolicitudCertificadoPOAPorAnioNumeroSolicitud, { canio: canio, numSolicitud: numSol }, function (data) {
                            if (data.EstadoSolicitud == "SO") {
                                FirmaCertificadoPOA(data.AnioSolicitud, data.NumeroSolicitud);
                                $("#btnGrabarEnviar").attr('disabled', 'disabled');
                            }
                        });
                    }
                    else {
                        Swal.fire("Solicitud Certificación POA", "No se puedo grabar." + errormessage, "warning");
                        $('#loadingBuscar').hide();   
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
                    $('#loadingBuscar').hide(); 
                }
            });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            $('#loadingBuscar').hide();  
            }

    });
}


function FirmaCertificadoPOA(canio, numSol) {
    $.ajax({
        url: $.MisUrls.url._ExportaServerReportAPDF,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        data: { canio: canio, numSolicitud: parseInt(numSol) },
        success: function (result) {
            if (result.length > 0) {
                abrirArchivo(result);
                //setTimeout(function () {
                //    frame = document.getElementById("iframeCetificado");
                //    framedoc = frame.contentWindow;
                //    framedoc.focus();
                //    framedoc.print();
                //}, 1000);
                $('#loadingBuscar').hide();            
            }
            else {
                $('#loadingBuscar').hide();
                mensajeGeneral("Solicitud del Certificado POA", "La Solicitud del Certificado POA, no se pudo generar el reporte.");
            }

        },
        error: function (errormessage) {
            mensajeGeneral("Solicitud del Certificado POA", "Error, Exportar el reporte" + errormessage);    
            $('#loadingBuscar').hide(); 
        }
    });
}

