var tabladata;
$(document).ready(function () {
    loadDataTable();
    obtenerComboAprobacion();

    $('#BucarFechas').click(function (e) {
        var _fechainicio = $("#fechaInicio").val();
        var _fechafin = $("#fechaFinal").val();

        if (_fechainicio.trim().length == 0) {
            MensajeIco("Valida Fecha", "Fecha inicio en blanco", "warning");
            return false
        }

        if (_fechafin.trim().length == 0) {
            MensajeIco("Valida Fecha", "Fecha final en blanco", "warning");
            return false
        }

        e.preventDefault();
        $("#registerForm").submit();
    });

    document.onkeydown = capturarf5;
});

function capturarf5(e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    if (code == 116) {
        window.location.href = $.MisUrls.url._financieroAprueba;
    }
}
function editar(data) {
    var texto = "";
    let ovista = $('#pvista').val();
    $('#modalload').modal('show');
    texto = $.MisUrls.url._Formulario + "?id=" + data + "&ovista=" + ovista;
    window.location.href = texto;
}

function loadDataTable() {
    $('#tbDetalleCharter').DataTable({
        "processing": true,
        scrollY: '580px',
        scrollCollapse: true,
        paging: false,
        info: true,
        "order": [[1, 'desc'], [0, 'desc']],
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

function abrirPopUpForm(numSol) {
    if (numSol != null) {
        $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
            if (htmlPago == true) {
                $('#modalLabel').html("AUTORIZACION DE VUELOS NO REGULARES POR TESORERÍA");
                //$('#modalload').modal('show');
                $.ajax({
                    url: $.MisUrls.url._FormularioCargaSolicitudVuelo,
                    type: "GET",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    data: { id: numSol },
                    success: function (e) {
                        //$('#modalload').modal('hide');
                        if (e.TipoSolictud == "01") {
                            $(".lblTipoSolicitud").text(" ANEXO 2 VUELO ESPECIAL INTERNACIONAL");
                            $(".divFletador").hide();
                            $(".divVueloItinerario").show();

                        }
                        else if (e.TipoSolictud == "02") {
                            $(".lblTipoSolicitud").text(" ANEXO 1-A CHARTER DOMESTICO");
                            $(".divFletador").show();
                            $(".divVueloItinerario").hide();
                        }
                        else if (e.TipoSolictud == "03") {
                            $(".lblTipoSolicitud").text(" ANEXO 1-B CHARTER INTERNACIONAL");
                            $(".divFletador").show();
                            $(".divVueloItinerario").hide();
                        }
                        else if (e.TipoSolictud == "04") {
                            $(".lblTipoSolicitud").text(" ANEXO 2 VUELO ESPECIAL NACIONAL");
                            $(".divFletador").hide();
                            $(".divVueloItinerario").show();
                        }

                        $("#numeroSolicitud").val(e.NumeroSolicitud);
                        $("#txtTipoSolicitud").val(e.TipoSolictud);
                        $("#fechaSolicitud").val(e.FechaEnvioSolicitud + " " + e.HoraEnvioSolicitud);


                        $("#nombreOperador").val(e.NombreCompaniaAviacion);
                        $("#numeroRucOperador").val(e.oCiaOperadora.RucCompania);
                        $("#telefonoOperador").val(e.Telefono);
                        $("#direccionOperador").val(e.Direccion);
                        $("#emailOperador").val(e.Email);

                        //Adjuntos
                        $("#tablaAdjuntos tbody").html("");
                        $.each(e.oAdjuntoCiaAviacion, function (i, row) {
                            $("<tr>").append(
                                $("<td class='text-center'>").text(row["CodigoDocumento"]),
                                $("<td>").text(row["DescripcionDocumento"]),
                                $("<td>").html("<a href='#' onclick='downloadDocumento(" + JSON.stringify(row["NombreDocumento"]) + ")'>" + row["NombreDocumento"].trim() + "</>")
                            ).appendTo("#tablaAdjuntos tbody");
                        })
                        //Informacion de la aeronave 

                        $("#tablaAeronaves tbody").html("");
                        $.each(e.oDetalleAeronave, function (i, row) {
                            $("#fechaVigenciaSeguro").val(row["FechaVigenciaSeguro"]);
                            $("<tr>").append(
                                $("<td class='text-center'>").text(row["Matricula"]),
                                $("<td>").text(row["Marca"]),
                                $("<td>").text(row["Modelo"]),
                                $("<td>").text(row["PesoWTOKG"])
                            ).appendTo("#tablaAeronaves tbody");
                        })

                        //Informacion del vuelo
                        $("#propositoVuelo").val(e.PropositoVuelo);
                        if (e.EspecificarVuelo == "PA") {
                            $(".lblPropisitoVuelo").text(" PASAJEROS");
                        }
                        else if (e.EspecificarVuelo == "CA") {
                            $(".lblPropisitoVuelo").text(" CARGA");
                        }
                        else {
                            $(".lblPropisitoVuelo").text(" PASAJEROS - CARGA");
                        }

                        $("#vueloItinerario").val(e.VueloItinerario);
                        $("#numeroPasajeros").val(e.NumeroPasajeros);

                        $("#tablaRutaVuelos tbody").html("");
                        $.each(e.oDetalleRuta, function (i, row) {
                            $("<tr>").append(
                                $("<td>").text(row["RutaVuelo"]),
                                $("<td>").text(row["DerechoSolicitado"]),
                                $("<td>").text(row["FechaIdaVuelo"]),
                                $("<td>").text(row["FechaRetornoVuelo"]),
                                $("<td>").text(row["HoraEstimadaVuelo"])
                            ).appendTo("#tablaRutaVuelos tbody");
                        })

                        //Informacion del Fletador
                        $("#nombreFletador").val(e.NombreFleteador);
                        $("#telefonoFletador").val(e.TelefonoFleteador);
                        $("#direccionFletador").val(e.DireccionFleteador);
                        $("#emailFletador").val(e.CorreoFleteador);
                        $("#nombreRepresentanteFletador").val(e.NombreRepresentante);

                        //Informacion del Facturador

                        $("#nombreCiaFactura").val(e.NombreFactura);
                        $("#numeroRucFactura").val(e.RucFactura);
                        $("#telefonoFactura").val(e.TelefonoFactura);
                        $("#direccionFactura").val(e.DireccionFactura);
                        $("#emailFactura").val(e.CorreoFactura);
                        $("#observacionFactura").val(e.Observacion);


                        $("#numeroVuelos").val(e.NumeroVuelos);
                        $("#totalPagar").val(e.TotalPagar);
                        $("#formaPago").val(e.DescripcionFormaPago.trim());

                        $("#numeroComprobantePago").val(e.oPagoSolictud[0].ComprobanteTransaccion);
                        $("#fechaPago").val(e.oPagoSolictud[0].FechaComprobanteTransaccion);
                        $("#ListaBancoPago").val(e.oPagoSolictud[0].TipoComprobante);

                        $("#nombreComprobantePago").val(e.ComprobantePago);
                        $(".pcomprobante-pago").html("<a href='#' onclick='downloadDocumento(" + JSON.stringify(e.ComprobantePago.trim()) + ")'>" + e.ComprobantePago.trim() + "</>");


                        if (e.EstadoFinanciero !== '' && e.ObservacionFinanciero !== '') {
                            $("#cboAprobacion").val(e.EstadoFinanciero);
                            $("#ObservacionFinanciero").text(e.ObservacionFinanciero);
                            kotoba();
                        }
                        else {
                            $("#cboAprobacion").val("0");
                            $("#ObservacionFinanciero").val("");
                            kotoba();
                        }
                        $('#FormModal').modal('show');

                    },
                    error: function (errormessage) {
                        Swal.fire("Mensaje", "Financiero: " + errormessage, "warning");
                    }
                });
            }
            else {
                document.location.href = $.MisUrls.url._FormularioLogin;
            }
        });
    }
}

function abrirSolicitud(numSol) {
    if (numSol != null) {
        $('#modalLabel').html("CONSULTA AUTORIZACION DE VUELOS NO REGULARES");
        visualizaDatosSolicitud(numSol);
    }
}

function limpiarCamposSolicitud() {
    //Operaciones
    $("#mEstadoOperaciones").val("");
    $("#mObservacionOperaciones").val("");
    $("#maprobadoOperaciones").text('');
    //Financiero
    $("#mEstadoFinanciero").val("");
    $("#mObservacionFinanciero").val("");
    $("#maprobadoFinanciero").text('');

    //transporte aereo
    $("#mEstadoTransporteAereo").val("");
    $("#mObservacionTransporteAereo").val("");
    $("#maprobadoTransporteAereo").text('');

    $("#mEstadoResponsableATO").val("");
    $("#mObservacionResponsableATO").val("");
    $("#maprobadoResponsableAto").text('');

}
function visualizaDatosSolicitud(numSol) {
    if (numSol != null) {
        $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
            if (htmlPago == true) {
                limpiarCamposSolicitud();
                $('#formularioSolicitud').trigger("reset");
                $.ajax({
                    url: $.MisUrls.url._FormularioCargaSolicitudVuelo,
                    type: "GET",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    data: { id: numSol },
                    success: function (e) {
                        if (e.TipoSolictud == "01") {
                            $(".lblTipoSolicitud").text(" ANEXO 2 VUELO ESPECIAL INTERNACIONAL");
                            $(".divFletador").hide();
                            $(".divVueloItinerario").show();

                        }
                        else if (e.TipoSolictud == "02") {
                            $(".lblTipoSolicitud").text(" ANEXO 1-A CHARTER DOMESTICO");
                            $(".divFletador").show();
                            $(".divVueloItinerario").hide();
                        }
                        else if (e.TipoSolictud == "03") {
                            $(".lblTipoSolicitud").text(" ANEXO 1-B CHARTER INTERNACIONAL");
                            $(".divFletador").show();
                            $(".divVueloItinerario").hide();
                        }
                        else if (e.TipoSolictud == "04") {
                            $(".lblTipoSolicitud").text(" ANEXO 2 VUELO ESPECIAL NACIONAL");
                            $(".divFletador").hide();
                            $(".divVueloItinerario").show();
                        }

                        $("#mnumeroSolicitud").val(e.NumeroSolicitud);
                        $("#mtxtTipoSolicitud").val(e.TipoSolictud);
                        $("#mfechaSolicitud").val(e.FechaEnvioSolicitud + " " + e.HoraEnvioSolicitud);


                        $("#mnombreOperador").val(e.NombreCompaniaAviacion);
                        $("#mnumeroRucOperador").val(e.oCiaOperadora.RucCompania);
                        $("#mtelefonoOperador").val(e.Telefono);
                        $("#mdireccionOperador").val(e.Direccion);
                        $("#memailOperador").val(e.Email);

                        //Adjuntos
                        $("#tablaAdjuntos tbody").html("");
                        $.each(e.oAdjuntoCiaAviacion, function (i, row) {
                            $("<tr>").append(
                                $("<td class='text-center'>").text(row["CodigoDocumento"]),
                                $("<td>").text(row["DescripcionDocumento"]),
                                $("<td>").html("<a href='#' onclick='downloadDocumento(" + JSON.stringify(row["NombreDocumento"]) + ")'>" + row["NombreDocumento"].trim() + "</>")
                            ).appendTo("#tablaAdjuntos tbody");
                        })
                        //Informacion de la aeronave 

                        $("#tablaAeronaves tbody").html("");
                        $.each(e.oDetalleAeronave, function (i, row) {
                            $("#fechaVigenciaSeguro").val(row["FechaVigenciaSeguro"]);
                            $("<tr>").append(
                                $("<td class='text-center'>").text(row["Matricula"]),
                                $("<td>").text(row["Marca"]),
                                $("<td>").text(row["Modelo"]),
                                $("<td>").text(row["PesoWTOKG"])
                            ).appendTo("#tablaAeronaves tbody");
                        })

                        //Informacion del vuelo
                        $("#mpropositoVuelo").val(e.PropositoVuelo);
                        if (e.EspecificarVuelo == "PA") {
                            $(".lblPropisitoVuelo").text(" PASAJEROS");
                        }
                        else if (e.EspecificarVuelo == "CA") {
                            $(".lblPropisitoVuelo").text(" CARGA");
                        }
                        else {
                            $(".lblPropisitoVuelo").text(" PASAJEROS - CARGA");
                        }

                        $("#mvueloItinerario").val(e.VueloItinerario);
                        $("#mnumeroPasajeros").val(e.NumeroPasajeros);

                        $("#tablaRutaVuelos tbody").html("");
                        $.each(e.oDetalleRuta, function (i, row) {
                            $("<tr>").append(
                                $("<td>").text(row["RutaVuelo"]),
                                $("<td>").text(row["DerechoSolicitado"]),
                                $("<td>").text(row["FechaIdaVuelo"]),
                                $("<td>").text(row["FechaRetornoVuelo"]),
                                $("<td>").text(row["HoraEstimadaVuelo"])
                            ).appendTo("#tablaRutaVuelos tbody");
                        })

                        //Informacion del Fletador
                        $("#mnombreFletador").val(e.NombreFleteador);
                        $("#mtelefonoFletador").val(e.TelefonoFleteador);
                        $("#mdireccionFletador").val(e.DireccionFleteador);
                        $("#memailFletador").val(e.CorreoFleteador);
                        $("#mnombreRepresentanteFletador").val(e.NombreRepresentante);

                        //Informacion del Facturador

                        $("#mnombreCiaFactura").val(e.NombreFactura);
                        $("#mnumeroRucFactura").val(e.RucFactura);
                        $("#mtelefonoFactura").val(e.TelefonoFactura);
                        $("#mdireccionFactura").val(e.DireccionFactura);
                        $("#memailFactura").val(e.CorreoFactura);
                        $("#mobservacionFactura").val(e.Observacion);


                        $("#mnumeroVuelos").val(e.NumeroVuelos);
                        $("#mtotalPagar").val(e.TotalPagar);
                        $("#mformaPago").val(e.DescripcionFormaPago.trim());

                        $("#mnumeroComprobantePago").val(e.oPagoSolictud[0].ComprobanteTransaccion);
                        $("#mfechaPago").val(e.oPagoSolictud[0].FechaComprobanteTransaccion);
                        $("#ListaBancoPago").val(e.oPagoSolictud[0].TipoComprobante);

                        $("#mnombreComprobantePago").val(e.ComprobantePago);
                        $(".pcomprobante-pago").html("<a href='#' onclick='downloadDocumento(" + JSON.stringify(e.ComprobantePago.trim()) + ")'>" + e.ComprobantePago.trim() + "</>");

                        if (e.UsuarioOperacionesDSOP == e.UsuarioFinanciero && e.UsuarioOperacionesDSOP == e.UsuarioTransporteAereo) {
                            $("#divAdministradorAeropuerto").css("display", "block");
                            $(".divMatriz").css("display", "none");
                            //Administrador Aeropuerto
                            $("#mEstadoResponsableATO").val(e.DescripcionResponsableATO);
                            $("#mObservacionResponsableATO").val(e.ObservacionResponsableATO);
                            $("#maprobadoResponsableAto").text('Aprobado por: ' + e.UsuarioResponsableATO + ' - ' + e.FechaResponsableATO + ' - ' + e.HoraResponsableATO);
                        }
                        else {
                            $("#divAdministradorAeropuerto").css("display", "none");
                            $(".divMatriz").css("display", "block");
                            //Operaciones
                            $("#mEstadoOperaciones").val(e.DescripcionOperacionesDSOP);
                            $("#mObservacionOperaciones").val(e.ObservacionOperacionesDSOP);
                            $("#maprobadoOperaciones").text('Aprobado por: ' + e.UsuarioOperacionesDSOP + ' - ' + e.FechaOperacionesDSOP + ' - ' + e.HoraOperacionesDSOP);
                            //Financiero
                            $("#mEstadoFinanciero").val(e.DescripcionEstadoFinanciero);
                            $("#mObservacionFinanciero").val(e.ObservacionFinanciero);
                            $("#maprobadoFinanciero").text('Aprobado por: ' + e.UsuarioFinanciero + ' - ' + e.FechaFinanciero + ' - ' + e.HoraFinanciero);

                            //transporte aereo
                            $("#mEstadoTransporteAereo").val(e.DescripcionEstadoTransporteAereo);
                            $("#mObservacionTransporteAereo").val(e.ObservacionTransporteAereo);
                            $("#maprobadoTransporteAereo").text('Aprobado por: ' + e.UsuarioTransporteAereo + ' - ' + e.FechaTransporteAereo + ' - ' + e.HoraTransporteAereo);
                        }
                        $('#ModalSolicitud').modal('show');
                    },
                    error: function (errormessage) {
                        mensajeGeneralIcono("Mensaje", "Transporte Aereo: " + errormessage, "warning");
                    }
                });
            }
            else {
                document.location.href = $.MisUrls.url._FormularioLogin;
            }
        });
    }
}


function obtenerComboAprobacion() {
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            jQuery.ajax({
                url: $.MisUrls.url._ObtenerEstadoAutorizacionComboBox,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //$("#cboDocumento").LoadingOverlay("hide");
                    $("#cboAprobacion").html("");
                    $("<option>").attr({ "value": 0 }).text("-- SELECCIONAR ESTADO --").appendTo("#cboAprobacion");
                    if (data.data != null)
                        $.each(data.data, function (i, item) {
                            $("<option>").attr({ "value": item.Codigo }).text(item.Descripcion).appendTo("#cboAprobacion");
                        })
                },
                error: function (error) {
                    console.log(error)
                },
                beforeSend: function () {
                    // $("#cboDocumento").LoadingOverlay("show");
                },
            });
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });

}

function downloadDocumento(fileName) {
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            $.ajax({
                type: "POST",
                url: $.MisUrls.url._downloadDocumento,
                data: '{nombreArchivo: "' + fileName + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (r) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(r);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", fileName);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });


}

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function Guardar() {
    let numSolitud = $("#numeroSolicitud").val();
    let tipoSolitud = $("#txtTipoSolicitud").val();
    let estadoOperacion = $("#cboAprobacion").val();
    let observacion = $("#ObservacionFinanciero").val();
    let titulo = "GESTIÓN INTERNA DE TESORERÍA";
    if (parseInt(numSolitud) <= 0) {
        mensajeGeneralIcono("Autorización de Vuelos Charter y/o Esoecielaes - Financiero", "Número de la Solicitud VuelosCharter en cero", "warning");
        return false;
    }

    if (estadoOperacion === '0') {
        mensajeGeneralIcono(titulo, "El  estado de aprobación debe seleccionar, es obligatorio", "warning");
        return false;
    }
    if (observacion === '') {
        mensajeGeneralIcono(titulo, "Observación, es obligatorio", "warning");
        return false;
    }
    //JSON.stringify(observacion)
    $('#loadImagen').css('display', 'block');
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            $.ajax({
                url: $.MisUrls.url._AutorizaFinancieroActualizaSolicitud,
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                data: { id: parseInt(numSolitud), estado: estadoOperacion, observacion: observacion, tipoSolitud: tipoSolitud },
                success: function (e) {
                    if (e.resultado && e.mesaje == '') {
                        $('#loadImagen').css('display', 'none');
                        $('#FormModal').modal('hide');
                        window.location.reload()
                    }
                    else if (e.resultado && e.mesaje != '') {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: e.mesaje,
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Aceptar'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $('#loadImagen').css('display', 'none');
                                $('#FormModal').modal('hide');
                                window.location.reload()
                            }
                        })

                    }
                    else {
                        $('#loadImagen').css('display', 'none');
                        Swal.fire({
                            title: 'Are you sure?',
                            text: e.mesaje,
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Aceptar'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                Swal.fire(
                                    'Deleted!',
                                    'Your file has been deleted.',
                                    'success'
                                )
                            }
                        })

                        Swal.fire(titulo, "No se puede actualizar la informacion de la Solicitud de Vuelo que envía Gestión Interna de Tesorería", "warning");
                    }

                },
                error: function (errormessage) {
                    $('#loadImagen').css('display', 'none');
                    Swal.fire(titulo, "Error, Al actualizar los datos de operaciones la causa, " + errormessage, "warning");
                }
            });
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });


}

function imprimir(fileName) {
    var nombreArchivo = fileName + "-signed.pdf";
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            $.ajax({
                type: "POST",
                url: $.MisUrls.url._DownloadAutorizacion,
                data: '{fileName: "' + nombreArchivo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                success: function (r) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(r);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, nombreArchivo);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", nombreArchivo);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                }
            });
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });
}

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
}


function funExportToPDF(id) {
    $('#ModalIframe').modal('show');
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            $.ajax({
                url: $.MisUrls.url._FormularioImprimir + "?id=" + id,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {

                    $('#frmPDF').attr('src', '@Url.Content("~/")' + result);

                    setTimeout(function () {
                        frame = document.getElementById("frmPDF");
                        frameframedoc = frame.contentWindow;
                        framedoc.focus();
                        framedoc.print();
                    }, 1000);
                },
                error: function (xhr, status, err) {
                    alert(err);
                }
            });

        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });


    return false;
}

function downloadSolicitud() {
    var id = $('#numeroSolicitud').val();
    var nombreArchivo = "Solicitud No.: " + $('#numeroSolicitud').val().padStart(8, '0') + " - " + $('#fechaSolicitud').val() + ".pdf";
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            $.ajax({
                url: $.MisUrls.url._ImprimirSolicitudCharter + "?id=" + id,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (r) {
                    //Convert Base64 string to Byte Array.
                    var bytes = Base64ToBytes(r);

                    //Convert Byte Array to BLOB.
                    var blob = new Blob([bytes], { type: "application/octetstream" });

                    //Check the Browser type and download the File.
                    var isIE = false || !!document.documentMode;
                    if (isIE) {
                        window.navigator.msSaveBlob(blob, nombreArchivo);
                    } else {
                        var url = window.URL || window.webkitURL;
                        link = url.createObjectURL(blob);
                        var a = $("<a />");
                        a.attr("download", nombreArchivo);
                        a.attr("href", link);
                        $("body").append(a);
                        a[0].click();
                        $("body").remove(a);
                    }
                },
                error: function (xhr, status, err) {
                    alert(err);
                }
            });
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });

}

function consultar(data) {
    var texto = "";
    let ovista = $('#pvista').val();
    $('#modalload').modal('show');
    $.post($.MisUrls.url._comprobarSessionCharter, null, function (htmlPago) {
        if (htmlPago == true) {
            texto = $.MisUrls.url._FormularioConsultaSolicitud + "?id=" + data + "&ovista=" + ovista;
            window.location.href = texto;
        }
        else {
            document.location.href = $.MisUrls.url._FormularioLogin;
        }
    });

}

//Creamos la Funcion
function kotoba() {
    $("#result").val($("#ObservacionFinanciero").val().length + " de 400 Caracteres"); //Detectamos los Caracteres del Input
    $("#result").addClass('mui--is-not-empty'); //Agregamos la Clase de Mui para decir que el input no esta vacio y que suba el Texto del Label(Como cuando haces Focus)
} //Aqui termina la Funcion


function mensajeGeneralIcono(titulo, contenido, icono) {
    Swal.fire({
        title: titulo,
        html: contenido,
        icon: icono,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
    });
}