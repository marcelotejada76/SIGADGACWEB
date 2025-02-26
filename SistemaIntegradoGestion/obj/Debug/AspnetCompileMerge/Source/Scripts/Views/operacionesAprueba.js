var tabladata;
$(document).ready(function () {
    loadDataTable();
    obtenerComboAprobacion();
    validaSession();
});

function loadDataTable() {
    $('#tbDetalleCharter').DataTable({
        "processing": true,
        scrollY: '400px',
        scrollCollapse: true,
        paging: false,
        info: false,
        "order": [[0, 'desc']],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });
}

function consultar(data) {
    var texto = "";
    let ovista = $('#pvista').val();
    $('#modalload').modal('show');
    texto = $.MisUrls.url._FormularioConsultaSolicitud + "?id=" + data + "&ovista=" + ovista;
    //_Formulario + "?id=" + data + "&ovista=" + ovista;
    // Open the page in a new tab or window
    //var w = window.open(texto);
    window.location.href = texto;
}



function abrirPopUpForm(numSol) {
    if (numSol != null) {

        $('#modalLabel').html("AUTORIZACION DE VUELOS NO REGULARES POR OPERACIONES");
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

                $("#nombreComprobantePago").val(e.ComprobantePago);
                $(".pcomprobante-pago").html("<a href='#' onclick='downloadDocumento(" + JSON.stringify(e.ComprobantePago.trim()) + ")'>" + e.ComprobantePago.trim() + "</>");


                if (e.EstadoFinanciero !== '' && e.ObservacionFinanciero !== '') {
                    $("#cboAprobacion").val(e.EstadoFinanciero);
                    $("#ObservacionFinanciero").text(e.ObservacionFinanciero);
                }
                $('#FormModal').modal('show');

            },
            error: function (errormessage) {
                Swal.fire("Mensaje", "Financiero: " + errormessage, "warning");
            }
        });
    }
}

function downloadDocumento(fileName) {
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

function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function obtenerComboAprobacion() {
    jQuery.ajax({
        url: $.MisUrls.url._FormularioObtenerAprobacionComboBox,
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

function Guardar() {
    let numSolitud = $("#numeroSolicitud").val();
    let tipoSolitud = $("#txtTipoSolicitud").val();
    let estadoOperacion = $("#cboAprobacion").val();
    let observacion = $("#Observacion").val();
    if (parseInt(numSolitud) <= 0) {
        Swal.fire("Mensaje", "Número de la Solicitud VuelosCharter en cero", "warning");
        return false;
    }

    if (estadoOperacion === '0') {
        Swal.fire("Mensaje", "El  estado de aprobación debe seleccionar, es obligatorio", "warning");
        return false;
    }
    if (observacion === '') {
        Swal.fire("Mensaje", "Observación, es obligatorio", "warning");
        return false;
    }

    $('#loadImagen').css('display', 'block');
    $.ajax({
        url: $.MisUrls.url._AutorizaOperacionesActualizaSolicitud,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { id: parseInt(numSolitud), estado: estadoOperacion, observacion: observacion, tipoSolitud: tipoSolitud },
        success: function (e) {
            if (e.resultado) {
                $('#loadImagen').css('display', 'none');
                $('#FormModalOperaciones').modal('hide');
                window.location.reload();
            }
            else {
                $('#loadImagen').css('display', 'none');
                Swal.fire("Mensaje", "No se puede actualizar la información de la Solicitud del Vuelo Charter, que envía Operaciones", "warning");
            }

        },
        error: function (errormessage) {
            $('#loadImagen').css('display', 'none');
            Swal.fire("Mensaje", "Error, Al actualizar los datos de operaciones por la causa, " + errormessage, "warning");
        }
    });
}

function validaSession() {
    setInterval(function () {
        var xhttp = new XMLHttpRequest();
        var url = $.MisUrls.url._FormularioSession;
        $.post(url, null, function (data) {
            if (data == "false")
                document.location.href = "/Login/login";
        });
    }, 3000);
}


function imprimir(fileName) {
    var nombreArchivo = fileName + "-signed.pdf";
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