var tablaCiaFactura;
var tabladata;
var controladorTiempo = "";
$(document).ready(function () {

    //$("#txtOrigen").on("keyup", function (e) {
    //    var dato = $("#txtOrigen").val();
    //    if (dato != "") {
    //        clearTimeout(controladorTiempo);
    //        controladorTiempo = setTimeout(BuscarAeropuertoOrigen, 380);
    //    }        
    //});
    validaCamposAplicante();
    $('.closeFactura').click(function () {
        $('.loading-circleFactura').hide();
    });

    $('.closeOperador').click(function () {
        $('.loading-circleOperador').hide();
    });

    $('.closeAplicante').click(function () {
        $('.loading-circleAplicante').hide();
    });
    $('.closeCiudadO').click(function () {
        $('.loading-circleO').hide();
    });

    $('.closeCiudadD').click(function () {
        $('.loading-circleD').hide();
    });

    $('.closeAeronave').click(function () {
        $('.loading-circleAro').hide();
    });

    //Envioa a grabar la solictud
    $('#enviarSolicutd').click(function () {
        let _nombreCompaniaAviacion = $("#NombreCompaniaAviacion").val();
        let _nombreFleteador = $("#NombreFleteador").val();
        let _nombreCiaOperadora = $("#txtNombreCiaOperadora").val();
        let _nombreCiaFleteador = $("#txtNombreCiaFleteador").val();
        let _matriculaAeronave = $("#txtMatricula").val();
        let _idCia = $("#IdCompaniaOperador").val();
        var mensaje = "";
        var numFactura = "";
        var valoPagar = 0;
        if (validaCampos()) {
            if (_nombreCompaniaAviacion == _nombreCiaOperadora) {
                let tablaTitulo = "";
                tablaTitulo = "<div style='width:100%; overflow-x: hidden; overflow-y: auto; height: 5em; border: 0px solid'><table class='table table-bordered table-hover' style='width:100%; font-size: 10px;'><thead><tr><th>RUC</th><th>COMPAÑIA</th><th>MATRICULA</th><th>FACTURA</th></thead><tbody>";
                let mensajeAdeuda = "<ul> <li><p style='color: #ff0000; font-size: 14px; text-align:justify'>No se puede tramitar su solicitud porque la aeronave y/o la compañía que paga tiene deuda pendiente con la DGAC. Por favor, comuníquese con el área financiera al +593  (2) 294-7400 ext 4720, De lunes a viernes de 8:00 a 16:30(Hora de Ecuador).</p></li><li><p style='color: #242495; font-size: 14px; text-align:justify'>Your request cannot be processed because the aircraft and/or the company has an outstanding debt with the DGAC. Please contact the financial area at +593 (2) 294-7400 ext 4720, Monday to Friday from 8:00 a.m. to 4:30 p.m. (Ecuador time).</p></li></ul>";
                $.post($.MisUrls.url._MatriculaExisteVueloPrivado, { matricula: _matriculaAeronave }, function (htmlMatricula) {
                    if (htmlMatricula == true) {
                        $.post($.MisUrls.url._FormularioListarCiaDeudoraExiste, { idCia: _idCia }, function (htmlCia) {
                            if (htmlCia.length == 0) {
                                //Hay que poner la validacion por aeronave
                                $.post($.MisUrls.url._FormularioDeudorPorAeronave, { matricula: _matriculaAeronave }, function (htmlMatricula) {
                                    if (htmlMatricula.length == 0) {
                                        $('.loading-circle').show();
                                        $('#enviarSolicutd').attr('disabled', true);
                                        $("#registrarFormularioPrivado").submit();
                                    }
                                    else {
                                        let otitulo = "5.- Datos de la Aeronave / Aircraft data \n(" + _matriculaAeronave + ")";
                                        let tbody = "";
                                        $.each(htmlMatricula, function (i, row) {
                                            tbody = tbody + "<tr>";
                                            tbody = tbody + "<td>";
                                            tbody = tbody + row["Ruc"];
                                            tbody = tbody + "</td>";
                                            tbody = tbody + "<td>";
                                            tbody = tbody + row["NombreCompaniaAviacion"];
                                            tbody = tbody + "</td>";
                                            tbody = tbody + "<td>";
                                            tbody = tbody + row["Matricula"];
                                            tbody = tbody + "</td>";
                                            tbody = tbody + "<td>";
                                            tbody = tbody + row["Factura"];
                                            tbody = tbody + "</td>";
                                            tbody = tbody + "</tr>"
                                            if (numFactura != row["Factura"]) {
                                                valoPagar += parseFloat(row["SaldoPendiente"]);
                                            }
                                            numFactura = row["Factura"];
                                        })
                                        tablaTitulo = tablaTitulo + tbody + "</tbody></table></div> <div>Valor adeuda.: " + valoPagar.toFixed(2) + "</div>";
                                        mensaje = mensajeAdeuda;
                                        mensajeSwall('warning', mensaje + tablaTitulo, otitulo);
                                        $('#loadImagenValida').css('display', 'none');
                                    }
                                });
                            }
                            else {
                                //let oNombreOperadora = $("#NombreCompaniaAviacion").val();
                                let otitulo = "2.- Datos del operador / Operator's data \n(" + _nombreCompaniaAviacion.trim() + ")";
                                let tbody = "";
                                $.each(htmlCia, function (i, row) {
                                    tbody = tbody + "<tr>";
                                    tbody = tbody + "<td>";
                                    tbody = tbody + row["Ruc"];
                                    tbody = tbody + "</td>";
                                    tbody = tbody + "<td>";
                                    tbody = tbody + row["NombreCompaniaAviacion"];
                                    tbody = tbody + "</td>";
                                    tbody = tbody + "<td>";
                                    tbody = tbody + row["Matricula"];
                                    tbody = tbody + "</td>";
                                    tbody = tbody + "<td>";
                                    tbody = tbody + row["Factura"];
                                    tbody = tbody + "</td>";
                                    if (numFactura != row["Factura"]) {
                                        valoPagar += parseFloat(row["SaldoPendiente"]);
                                    }
                                    numFactura = row["Factura"];
                                })

                                tablaTitulo = tablaTitulo + tbody + "</tbody></table></div> <div>Valor adeuda.: " + valoPagar.toFixed(2) + "</div>";

                                mensaje = mensajeAdeuda;
                                mensajeSwall('warning', mensaje + tablaTitulo, otitulo);
                                //$('#loadbtnEnviar').removeClass("spinner-border text-primary");
                                //$('#loadImagenValida').css('display', 'none');
                            }
                        });
                    }
                    else {
                        limpiarHabilitarAeronave();
                        $('#modal-informacionMatricula').modal('show');
                    }
                });
            }
            else {
                let otitulo = "Nombre de la empresa u operador / Company or operator name";
                mensaje = "<p style='color: #ff0000; font-size: 14px; text-align:justify'>Nombre de la Compañía operadora no es igual como consta en nuestra base de datos / <i style='color: #242495;'> Company Name is not the same as it appears in our database</i> </p>";
                mensajeSwall('warning', mensaje, otitulo);
                $('#NombreCompaniaAviacion').addClass("border-danger");
            }

        }
    });
    //Fin Enviar
    //$("#NombreFleteador").on('keydown', function (e) {
    //    if (e.key == 'Enter') {
    //        modalCompaniaFactura($("#NombreFleteador").val());
    //    }
    //});

    $("#NombreCompaniaAviacion").on('keydown', function (e) {
        if (e.key == 'Enter') {
            modalCompania($("#NombreCompaniaAviacion").val());
        }
    });


    $("#txtMatricula").on('keydown', function (e) {
        if (e.key == 'Enter') {
            //let omatricula = $("#txtMatricula").val();
            buscaAeronavesPorMatriculaVer();
        }
    })

    $("#txtOrigen").on('keydown', function (e) {
        if (e.key == 'Enter') {
            BuscarAeropuertoOrigen();
        }
    })

    //$("#txtDestino").on('keydown', function (e) {
    //    if (e.key == 'Enter') {
    //        BuscarAeropuertoDestivo();
    //    }
    //})

    //Alphanumérico y sin espacios  
    $("#txtMatricula").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z0-9]$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $("#txtOrigen").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //$("#txtDestino").bind('keypress', function (event) {
    //    var regex = new RegExp("^[a-zA-Z]+$");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key)) {
    //        event.preventDefault();
    //        return false;
    //    }
    //});
});

function BuscarAeropuertoOrigen() {
    if ($("#txtOrigen").val() != "") {
        jQuery.ajax({
            url: $.MisUrls.url._FormularioObtenerCiudadesPorCodigo + "?codigo=" + $("#txtOrigen").val(),
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != undefined && data != null) {
                    $("#tbAeropuertoFlight tbody").html("");
                    $.each(data, function (i, row) {

                        $("<tr>").append(
                            $("<td>").append("<a  href='#' onclick='seleccionarCiudadO(" + JSON.stringify(row["CodigoCiudad"]) + ");'>" + row["CodigoCiudad"] + "</a>"),
                            $("<td>").text(row["DescripcionCiudad"])
                        ).appendTo("#tbAeropuertoFlight tbody");

                        $('.loading-circleO').show();
                    })

                    // $('#modal-default').modal('show');
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {
            },
        });
        $("#txtOrigen").val("");

    }
}

function BuscarAeropuertoDestivo() {
    if ($("#txtDestino").val() != "") {
        jQuery.ajax({
            url: $.MisUrls.url._FormularioObtenerCiudadesPorCodigo + "?codigo=" + $("#txtDestino").val(),
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != undefined && data != null) {
                    $("#tbAeropuertoFlightD tbody").html("");
                    $.each(data, function (i, row) {
                        $("<tr>").append(
                            $("<td>").append("<a  href='#' onclick='seleccionarCiudadD(" + JSON.stringify(row["CodigoCiudad"]) + ");'>" + row["CodigoCiudad"] + "</a>"),
                            $("<td>").text(row["DescripcionCiudad"])
                        ).appendTo("#tbAeropuertoFlightD tbody");

                        $('.loading-circleD').show();
                    })

                    // $('#modal-default').modal('show');
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {
            },
        });
        $("#txtDestino").val("");

    }
}

function seleccionarCiudadO(codigo) {
    $("#txtOrigen").val(codigo);
    $('.loading-circleO').hide();
}

function seleccionarCiudadD(codigo) {
    $("#txtDestino").val(codigo);
    $('.loading-circleD').hide();
}

//Inicio de Cia del Aplicante
function modalCompaniaFactura(descripcion) {
    if (descripcion != "") {
        jQuery.ajax({
            url: $.MisUrls.url._FormularioCompaniaAvisionPorDescripcion + "?descripcion=" + descripcion,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != undefined && data != null) {
                    $("#tbCompaniaAviacionFactura tbody").html("");
                    $.each(data, function (i, row) {
                        $("<tr>").append(
                            $("<td class='col-sm-9'>").append("<a  href='#' onclick='seleccionarCompaniaFactura(" + JSON.stringify(row) + ");'>" + row["NombreCompaniaAviacion"] + "</a>"),
                            $("<td class='col-sm-3'>").text(row["CodigoOaci"])
                        ).appendTo("#tbCompaniaAviacionFactura tbody");
                        $('.loading-circleAplicante').show();
                    })

                    if (data.length == 0) {
                        $("#DireccionFleteador").val("");
                        $('#DireccionFleteador').prop('readonly', true);
                        $("#TelefonoFleteador").val("");
                        $('#TelefonoFleteador').prop('readonly', true);
                        $("#CorreoFleteador").val("");
                        $('#CorreoFleteador').prop('readonly', true);
                        $('#modal-informacionAplicante').modal('show');
                    }
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

function seleccionarCompaniaFactura(json) {
    if (json != null) {
        $('#NombreFleteador').removeClass("border-danger");
        $('#DireccionFleteador').removeClass("border-danger");
        $('#TelefonoFleteador').removeClass("border-danger");
        $('#CorreoFleteador').removeClass("border-danger");
        var mensaje = "";
        $("#IdFleteador").val(json.IdFleteador);

        $("#NombreFleteador").val(json.NombreCompaniaAviacion);
        $("#txtNombreCiaFleteador").val(json.NombreCompaniaAviacion);

        if (json.DireccionBilling.length == 0 || json.DireccionBilling.length < 10) {
            json.DireccionBilling = json.Direccion;
        }

        if (json.DireccionBilling.length == 0 || json.DireccionBilling.length < 10) {
            mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Dirección no es válido actualice la información /<i style='color: #242495;'>Address is not valid update the information</i></p></li></ul>";
            $('#DireccionFleteador').addClass("border-danger");
            $("#DireccionFleteador").val("");
        }
        else {
            $('#DireccionFleteador').prop('readonly', true);
            $("#DireccionFleteador").val(json.DireccionBilling);
        }

        if (json.Telefono.length == 0) {
            //json.TelefonoBilling = json.Telefono;
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
            $('#TelefonoFleteador').addClass("border-danger");
            $("#TelefonoFleteador").val("");
        }
        else {
            $('#TelefonoFleteador').prop('readonly', true);
            $("#TelefonoFleteador").val(json.Telefono);
        }


        /*
         * if (!validaTelefono(json.TelefonoBilling)) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información / <i style='color: #242495;'>Telephone is not valid update the information</i> </p></li></ul>";
            $('#TelefonoFleteador').addClass("border-danger");
            $("#TelefonoFleteador").val("");
        }          
         * */
        if (json.EmailBilling.length == 0) {
            json.EmailBilling = json.Email;
        }

        if (!validarEmail(json.EmailBilling)) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Correo no es válido actualice la información / <i style='color: #242495;'>Email is not valid update the information</i> </p></li></ul>";
            $('#CorreoFleteador').addClass("border-danger");
            $("#CorreoFleteador").val(json.EmailBilling);

        } else {
            $('#CorreoFleteador').prop('readonly', true);
            $("#CorreoFleteador").val(json.EmailBilling);
        }
        //$('#DireccionFleteador').prop('readonly', false);
        //$('#TelefonoFleteador').prop('readonly', false);
        //$('#CorreoFleteador').prop('readonly', false);



        if (mensaje != "") {
            Swal.fire({
                icon: 'warning',
                title: "<p style='width: 100 %; font-size: 14px;'>Datos del Aplicante / Applicant's Data</p>",
                html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. \n" + mensaje + "\nNo consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
            });
        }
        $("#IdFleteador").val(json.IdCompania);
        //$('#modalCompaniaFactura').modal('hide');
        $('.loading-circleAplicante').hide();

    } else {
        $("#NombreFleteador").val("");
        Swal.fire("Mensaje", "No existe información de los datos de la compañía facturadora", "warning");
    }
}
//Fin Cia del Aplicante
//Inicio de Cia Operadora
function modalCompania(descripcion) {
    if (descripcion != "") {
        jQuery.ajax({
            url: $.MisUrls.url._FormularioCompaniaAvisionPorDescripcion + "?descripcion=" + descripcion,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != undefined && data != null) {
                    $("#tbCompaniaAviacion tbody").html("");
                    $.each(data, function (i, row) {
                        $("<tr>").append(
                            $("<td class='col-sm-9'>").append("<a  href='#' onclick='seleccionarCompania(" + JSON.stringify(row) + ");'>" + row["NombreCompaniaAviacion"] + "</a>"),
                            $("<td class='col-sm-3'>").text(row["CodigoOaci"])
                        ).appendTo("#tbCompaniaAviacion tbody");
                        $('.loading-circleOperador').show();
                    })
                    //if (data.length === 0) { limpiarHabilitarAeronave(); }
                    //$("#Matricula").val("");
                    if (data.length == 0) {
                        $("#Direccion").val("");
                        $('#Direccion').prop('readonly', true);
                        $("#Telefono").val("");
                        $('#Telefono').prop('readonly', true);
                        $("#Email").val("");
                        $('#Email').prop('readonly', true);
                        $('#modal-informacionOperador').modal('show');
                        //Swal.fire("Datos del operador / Operator's data", "Nombre de la empresa u operador no existe/ Name of the company or operator does not exist ", "warning");
                    }
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

function seleccionarCompania(json) {
    if (json != null) {
        var mensaje = "";
        $("#IdCompaniaOperador").val(json.IdCompania);
        $("#NombreCompaniaAviacion").val(json.NombreCompaniaAviacion);
        $("#txtNombreCiaOperadora").val(json.NombreCompaniaAviacion);

        $('#NombreCompaniaAviacion').removeClass("border-danger");
        $('#Direccion').removeClass("border-danger");
        $('#Telefono').removeClass("border-danger");
        $('#Email').removeClass("border-danger");

        if (json.Direccion.length == 0) {
            mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Dirección no es válido actualice la información /<i style='color: #242495;'>Address is not valid update the information</i></p></li></ul>";
            $('#Direccion').addClass("border-danger");
            $('#Direccion').prop('readonly', false);
            $("#Direccion").val(json.Direccion);
        }
        else {
            $('#Direccion').prop('readonly', false);
            $("#Direccion").val(json.Direccion);
        }
        //if (validaTelefono(json.Telefono) == false) {
        if (json.Telefono.length == 0) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
            $('#Telefono').addClass("border-danger");
            $('#Telefono').prop('readonly', false);
            $("#Telefono").val("");
        }
        else if (json.Telefono.length < 7) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
            $('#Telefono').addClass("border-danger");
            $('#Telefono').prop('readonly', false);
            $("#Telefono").val("");
        }
        else if (!tiene_numeros(json.Telefono)) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
            $('#Telefono').addClass("border-danger");
            $('#Telefono').prop('readonly', false);
            $('#Telefono').val(json.Telefono);
        }
        else {
            $('#Telefono').prop('readonly', false);
            $("#Telefono").val(json.Telefono);
        }

        if (!validarEmail(json.Email)) {
            mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Correo no es válido actualice la información / <i style='color: #242495;'>Email is not valid update the information</i> </p></li></ul>";
            $('#Email').addClass("border-danger");
            $('#Email').prop('readonly', false);
            $('#Email').val(json.Email);
        }
        else {
            $('#Email').prop('readonly', false);
            $("#Email").val(json.Email);
        }

        //$('#Direccion').prop('readonly', true);
        //$('#Telefono').prop('readonly', true);
        //$('#Email').prop('readonly', true);

        $('.loading-circleOperador').hide();
        if (mensaje != "") {
            Swal.fire({
                icon: 'warning',
                title: "<p style='width: 100 %; font-size: 14px;'>Datos del operador / Operator's data</p>",
                html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. \n" + mensaje + "\nNo consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
            });
        }
    } else {
        $("#NombreCompaniaAviacion").val("");
        Swal.fire("Mensaje", "No existe datos del operador", "warning");
    }
}
//Fin Cia Operadora

function validaCamposAplicante() {
    var _nombreFleteador = $("#NombreResponsableContacto").val();
    var _direccionFleteador = $("#DireccionResponsableContacto").val();
    var _telefonoFleteador = $("#TelefonoResponsableContacto").val();
    var _correoFleteador = $("#CorreoResponsableContacto").val();

    $('#NombreResponsableContacto').removeClass("border-danger");
    $('#DireccionResponsableContacto').removeClass("border-danger");
    $('#TelefonoResponsableContacto').removeClass("border-danger");
    $('#CorreoResponsableContacto').removeClass("border-danger");

    mensaje = "";
    var _titulo = "Datos del Aplicante / Applicant's Data";
    if (_nombreFleteador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El nombre del aplicante no es válido actualice la información / <i style='color: #242495;'>The name of the applicant is not valid, update the information<</i> </p></li></ul>";
        $('#NombreResponsableContacto').addClass("border-danger");
        $('#NombreResponsableContacto').prop('readonly', false);
        // return false;
    }
    if (_direccionFleteador.trim().length == 0) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del aplicante no es válido actualice la información / <i style='color: #242495;'>The applicant's address is not valid, update the information<</i> </p></li></ul>";
        $('#DireccionResponsableContacto').addClass("border-danger");
        $('#DireccionResponsableContacto').prop('readonly', false);
        //return false;
    }
    if (_telefonoFleteador.trim().length == 0) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del aplicante no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>The applicant's telephone number is not valid, update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        $('#TelefonoResponsableContacto').addClass("border-danger");
        $('#TelefonoResponsableContacto').prop('readonly', false);
        //return false;
    }
    if (_correoFleteador.trim().length == 0) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El correo del aplicante no es válido actualice la información / <i style='color: #242495;'>The applicant's email is not valid, update the information</i> </p></li></ul>";
        $('#CorreoResponsableContacto').addClass("border-danger");
        //return false;
    }
    else if (!validarEmail(_correoFleteador)) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del correo electrónico del aplicante no es valido actualice la información / <i style='color: #242495;'>The applicant's email address is not valid, update the information</i> </p></li></ul>";
        $('#CorreoResponsableContacto').addClass("border-danger");
    }

    if (mensaje.length > 0) {
        mensajeSwall("warning", "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>", "Datos del Aplicante / Applicant's Data");
    }

}

function validaCampos() {
    var mensaje = "";
    var _radioSolicitud = $(".radSolicitud").is(":checked");
    var _nombreFleteador = $("#NombreResponsableContacto").val();
    var _direccionFleteador = $("#DireccionResponsableContacto").val();
    var _telefonoFleteador = $("#TelefonoResponsableContacto").val();
    var _correoFleteador = $("#CorreoResponsableContacto").val();


    var _nombreOperador = $("#NombreCompaniaAviacion").val();
    var _direccionOperador = $("#Direccion").val();
    var _telefonoOperador = $("#Telefono").val();
    var _correoOperador = $("#Email").val();

    var _matricula = $("#txtMatricula").val();
    var _marca = $("#Marca").val();
    var _modelo = $("#Modelo").val();
    var _pesoMtow = $("#PesoMTOW").val();

    var _propositoVuelo = $("#PropositoVuelo").val();

    $('#radioAroSola').removeClass("border-danger");
    $('#radioAroMultiple').removeClass("border-danger");

    $('#NombreFleteador').removeClass("border-danger");
    $('#DireccionFleteador').removeClass("border-danger");
    $('#TelefonoFleteador').removeClass("border-danger");
    $('#CorreoFleteador').removeClass("border-danger");

    $('#NombreCompaniaAviacion').removeClass("border-danger");
    $('#Direccion').removeClass("border-danger");
    $('#Telefono').removeClass("border-danger");
    $('#Email').removeClass("border-danger");

    $('#txtMatricula').removeClass("border-danger");
    $('#Marca').removeClass("border-danger");
    $('#Modelo').removeClass("border-danger");
    $('#PesoMTOW').removeClass("border-danger");

    $('#Matricula1').removeClass("border-danger");
    $('#Marca1').removeClass("border-danger");
    $('#Modelo1').removeClass("border-danger");
    $('#PesoMTOW1').removeClass("border-danger");
    $('#PropositoVuelo').removeClass("border-danger");
    if (_radioSolicitud == false) {
        Swal.fire("Tipo de Solicitud / Type of request", "El tipo de solicitud es obligatorio seleccionar / The type of request is mandatory to select", "warning");
        $('#radioAroSola').addClass("border-danger");
        $('#radioAroMultiple').addClass("border-danger");
        return false;
    }
    mensaje = "";
    var _titulo = "Datos del Aplicante / Applicant's Data";
    if (_nombreFleteador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El nombre del aplicante no es valido actualice la información / <i style='color: #242495;'>The name of the applicant is not valid, update the information<</i> </p></li></ul>";

        //mensaje = "<b>El nombre del aplicante no es valido actualice la información / The name of the applicant is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        //Swal.fire(_titulo, "Nombre del aplicante en , es obligatorio llenar", "warning");
        $('#NombreResponsableContacto').addClass("border-danger");
        $('#NombreResponsableContacto').prop('readonly', false);
        return false;
    }
    if (_direccionFleteador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del aplicante no es valido actualice la información / <i style='color: #242495;'>The applicant's address is not valid, update the information<</i> </p></li></ul>";
        //mensaje = "<b>La dirección del aplicante no es valido actualice la información / The applicant's address is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        //Swal.fire(_titulo, "La dirección del aplicante, es obligatorio llenar", "warning");
        $('#DireccionResponsableContacto').addClass("border-danger");
        $('#DireccionResponsableContacto').prop('readonly', false);
        return false;
    }
    if (_telefonoFleteador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del aplicante no es valido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>The applicant's telephone number is not valid, update the information<</i> </p></li></ul>";
        //mensaje = "<b>El teléfono del aplicante no es valido actualice la información / The applicant's telephone number is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        //Swal.fire(_titulo, "El teléfono del aplicante, es obligatorio llenar", "warning");
        $('#TelefonoResponsableContacto').addClass("border-danger");
        $('#TelefonoResponsableContacto').prop('readonly', false);
        return false;
    }
    else if (_telefonoFleteador.trim().length < 7) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del aplicante no es valido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>The applicant's telephone number is not valid, update the information<</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        $('#TelefonoResponsableContacto').addClass("border-danger");
        $('#TelefonoResponsableContacto').prop('readonly', false);
        return false;
    }
    else if (!tiene_numeros(_telefonoFleteador)) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El número de teléfono del aplicante no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>The applicant's phone number is invalid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        $('#TelefonoResponsableContacto').addClass("border-danger");
        $('#TelefonoResponsableContacto').prop('readonly', false);
        return false;
    }

    if (_correoFleteador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El correo del aplicante no es valido actualice la información / <i style='color: #242495;'>The applicant's email is not valid, update the information</i> </p></li></ul>";
        //mensaje = "<b>El correo del aplicante no es valido actualice la información / The applicant's email is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "Signo más + número de prefijo del país + número de teléfono: +593555005500</div>"
        });

        //Swal.fire("_titulo", "El correo de la empresa del aplicante, es obligatorio llenar", "warning");
        $('#CorreoResponsableContacto').addClass("border-danger");
        return false;
    }
    else if (!validarEmail(_correoFleteador)) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del correo electrónico del aplicante no es valido actualice la información / <i style='color: #242495;'>The applicant's email address is not valid, update the information</i> </p></li></ul>";
        //mensaje = "<b>La dirección del correo electrónico del aplicante no es valido actualice la información / The applicant's email address is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<b style='width: 100 %; font-size: 14px;'>Datos del Aplicante / Applicant's Data</b>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + + "Signo más + número de prefijo del país + número de teléfono: +593555005500</div>"
        });
        $('#CorreoResponsableContacto').addClass("border-danger");
        return false;
    }
    //Valida Operador
    _titulo = "Datos del operador / Operator's data";
    if (_nombreOperador.trim().length == 0) {
        //mensaje = "<b>El nombre del operador no es valido actualice la información / The name of the operator is not valid update the information</b>";
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El nombre del operador no es válido actualice la información  / <i style='color: #242495;'>The name of the operator is not valid update the information</i> </p></li></ul>";

        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "Nombre de la empresa del operador, es obligatorio llenar", "warning");
        $('#NombreCompaniaAviacion').addClass("border-danger");
        return false;
    }
    if (_direccionOperador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del operador no es válido actualice la información  / <i style='color: #242495;'>The address of the operator is not valid update the information</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "La dirección de la empresa del operador, es obligatorio llenar", "warning");
        $('#Direccion').addClass("border-danger");
        $('#Direccion').prop('readonly', false);
        return false;
    }
    if (_telefonoOperador.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del operador no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500  / <i style='color: #242495;'>The operator's telephone number is not valid, update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";

        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "El teléfono de la empresa del operador, es obligatorio llenar", "warning");
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
        return false;
    }
    else if (_telefonoOperador.trim().length < 7) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del operador no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500  / <i style='color: #242495;'>The operator's telephone number is not valid, update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";

        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "El teléfono de la empresa del operador, es obligatorio llenar", "warning");
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
        return false;
    }
    else if (!tiene_numeros(_telefonoOperador)) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El número de teléfono del operador no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500  / <i style='color: #242495;'>The applicant's phone number is invalid update the information. Plus sign + country prefix number + phone number: +593555005500<</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + "</div>"
        });

        //Swal.fire(_titulo, "El teléfono del aplicante, es obligatorio llenar", "warning");
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
        return false;
    }

    if (_correoOperador.trim().length == 0) {
        //mensaje = "<b>El correo del operador no es valido actualice la información / The operator's email is not valid, update the information</b>";
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El correo del operador no es valido actualice la información / <i style='color: #242495;'>The operator's email is not valid, update the information</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });
        //Swal.fire(_titulo, "El correo de la empresa del operador, es obligatorio llenar", "warning");
        $('#Email').addClass("border-danger");
        $('#Email').prop('readonly', false);
        return false;
    }
    else if (!validarEmail(_correoOperador)) {
        //mensaje = "<b>La dirección del correo electrónico del operador no es valido actualice la información/ The operator's email address is not valid, update the information</b>";
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del correo electrónico del operador no es válido actualice la información / <i style='color: #242495;'>The operator's email address is not valid, update the information</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No es valido en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        $('#Email').addClass("border-danger");
        $('#Email').prop('readonly', false);
        return false;
    }
    //valida Aeronave
    _titulo = "Datos de la Aeronave / Aircraft data";
    if (_matricula.trim().length == 0) {
        //mensaje = "<b>La matrícula de la aeronave no es valido actualice la información / The aircraft registration is not valid update the information</b>";
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La matrícula de la aeronave no es valido actualice la información / <i style='color: #242495;'>The aircraft registration is not valid update the information</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "Matrícula, es obligatorio llenar", "warning");
        $('#txtMatricula').addClass("border-danger");
        return false;
    }
    if (_marca.trim().length == 0) {
        //mensaje = "<b>La marca de la aeronave no es valido actualice la información / The brand of the aircraft is not valid, update the information</b>";
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La marca de la aeronave no es valido actualice la información / <i style='color: #242495;'>The brand of the aircraft is not valid, update the information</i> </p></li></ul>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire("Mensaje", "Marca, es obligatorio llenar", "warning");
        $('#Marca').addClass("border-danger");
        return false;
    }
    if (_modelo.trim().length == 0) {
        mensaje = "<b>El modelo de la aeronave no es valido actualice la información / The aircraft model is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "Modelo, es obligatorio llenar", "warning");
        $('#Modelo').addClass("border-danger");
        return false;
    }

    if (_pesoMtow.trim().length == 0) {
        mensaje = "<b>El Peso MTOW de la aeronave no es valido actualice la información / The MTOW Weight of the aircraft is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "El Peso MTOW de la aeronave, es obligatorio llenar", "warning");
        $('#PesoMTOW').addClass("border-danger");
        return false;
    }
    else if (esEntero(_pesoMtow) && _pesoMtow <= 0) {
        mensaje = "<b>El Peso MTOW de la aeronave no es valido actualice la información / The MTOW Weight of the aircraft is not valid, update the information</b>";
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>" + _titulo + "</p>",
            html: "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. " + mensaje + " No consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>"
        });

        //Swal.fire(_titulo, "El Peso MTOW de la aeronave, es obligatorio llenar", "warning");
        $('#PesoMTOW').addClass("border-danger");
        return false;
    }

    var _titulo = "Datos de la Aeronave / Aircraft data";
    if (_propositoVuelo.trim() == "0") {
        Swal.fire(_titulo, "El proposito del vuelo, es obligatorio seleccionar / The purpose of the flight, it is mandatory to select", "warning");
        $('#PropositoVuelo').addClass("border-danger");
        return false;
    }

    let rowsAeronaves = 0;
    $("#tbdetalleAerove TFOOT TR").each(function () {
        rowsAeronaves++;
    });

    if (rowsAeronaves === 0) {
        Swal.fire(_titulo, "La información de la aeronave, es obligatorio ingresar / The information of the aircraft, it is mandatory to enter.", "warning");
        return false;
    }
    else {
        //detalle aeronave
        let numr = 0;
        document.getElementById("divAeronaves").innerHTML = "";
        $("#tbdetalleAerove >TFOOT >TR").each(function () {
            var _matricula = "";
            var _marca = "";
            var _modelo = "";

            var _pesoMtow = "";
            _matricula = $('#txtMatricula').val(); //$(this).closest("tr").find('td:eq(0) input[type=text]').val();
            _marca = $('#Marca').val(); //$(this).closest("tr").find('td:eq(1) input[type=text]').val();
            _modelo = $('#Modelo').val(); //$(this).closest("tr").find('td:eq(2) input[type=text]').val();           
            _pesoMtow = $('#PesoMTOW').val(); //$(this).closest("tr").find('td:eq(4) input[type=text]').val();

            if (_matricula.trim().length > 0 && _marca.trim().length > 0 && _modelo.trim().length > 0 && _pesoMtow.trim().length > 0) {
                //Agramos hiddens
                let DivAeronaves = document.getElementById("divAeronaves");
                let HiddenIndex = document.createElement("input");
                let HiddenMarca = document.createElement("input");
                let HiddenModelo = document.createElement("input");
                let HiddenMatricula = document.createElement("input");
                let HiddenMtow = document.createElement("input");
                HiddenIndex.name = "oDetalleAeronave.Index";
                HiddenIndex.value = numr;
                HiddenIndex.type = "hidden";
                HiddenMatricula.name = "oDetalleAeronave[" + numr + "].Matricula";
                HiddenMatricula.value = _matricula.trim();
                HiddenMatricula.type = "hidden";
                HiddenMarca.name = "oDetalleAeronave[" + numr + "].Marca";
                HiddenMarca.value = _marca.trim();
                HiddenMarca.type = "hidden";
                HiddenModelo.name = "oDetalleAeronave[" + numr + "].Modelo";
                HiddenModelo.value = _modelo.trim();
                HiddenModelo.type = "hidden";
                HiddenMtow.name = "oDetalleAeronave[" + numr + "].PesoWTOKG";
                HiddenMtow.value = _pesoMtow.trim();
                HiddenMtow.type = "hidden";
                DivAeronaves.appendChild(HiddenIndex);
                DivAeronaves.appendChild(HiddenMarca);
                DivAeronaves.appendChild(HiddenModelo);
                DivAeronaves.appendChild(HiddenMatricula);
                DivAeronaves.appendChild(HiddenMtow);
            }

            numr++;
        });
    }
    //fin detalle aeronave

    //Inicio detalle ruta
    _titulo = "Ruta del vuelo / Flight path";
    let rowsRuta = 0;
    $("#tbDetalleRuta > tbody  > tr").each(function () {
        rowsRuta++;
    });

    if (rowsRuta === 0) {
        Swal.fire(_titulo, "La Ruta de vuelo, es obligatorio ingresar. / The Flight Route, it is mandatory to enter.", "warning");
        return false;
    }
    else {
        let numr = 0;
        let oOid = "";
        let ofechaVuelo = "";
        let orutaOrigen = "";
        let orutaDestino = "";
        var indexDetalle = 1;
        document.getElementById("divRuta").innerHTML = "";
        $("#tbDetalleRuta > tbody  > tr").each(function () {
            var row = $(this);
            oOid = indexDetalle; //row.find("TD").eq(0).html();
            ofechaVuelo = row.find("TD").eq(0).html();
            orutaOrigen = row.find("TD").eq(1).html();
            orutaDestino = row.find("TD").eq(2).html();


            //Agramos hiddens
            let DivRutas = document.getElementById("divRuta");
            let HiddenIndex = document.createElement("input");
            let HiddenIdRuta = document.createElement("input");
            let HiddenOrigen = document.createElement("input");
            let HiddenDestino = document.createElement("input");
            let HiddenFechaVuelo = document.createElement("input");

            HiddenIndex.name = "oDetalleRuta.Index";
            HiddenIndex.value = numr;
            HiddenIndex.type = "hidden";
            HiddenIdRuta.name = "oDetalleRuta[" + numr + "].IdRuta";
            HiddenIdRuta.value = oOid;
            HiddenIdRuta.type = "hidden";

            HiddenOrigen.name = "oDetalleRuta[" + numr + "].RutaOrigenInicio";
            HiddenOrigen.value = orutaOrigen;
            HiddenOrigen.type = "hidden";

            HiddenDestino.name = "oDetalleRuta[" + numr + "].RutaDestino";
            HiddenDestino.value = orutaDestino;
            HiddenDestino.type = "hidden";

            HiddenFechaVuelo.name = "oDetalleRuta[" + numr + "].FechaIdaVuelo";
            HiddenFechaVuelo.value = ofechaVuelo;
            HiddenFechaVuelo.type = "hidden";


            DivRutas.appendChild(HiddenIndex);
            DivRutas.appendChild(HiddenIdRuta);
            DivRutas.appendChild(HiddenOrigen);
            DivRutas.appendChild(HiddenDestino);
            DivRutas.appendChild(HiddenFechaVuelo);
            indexDetalle++;
            numr++;
        });

    }
    //Fin Futa

    return true;
}

function AddRuta() {
    // $("#tbDetalleRuta tbody").html("");
    // $("#tbDetalleRuta tbody").html("");
    var ofechaVuelo = $("#txtFehaVuelo").val();
    var oAtoOrigen = $("#txtOrigen").val();
    var oAtoDestino = $("#CboAeropuertoAterrizaje").val();
    var filaRow = $("#tbDetalleRuta  > tbody  > tr").length;
    var _radioSolicitud = $(".radSolicitud").is(":checked");

    if (_radioSolicitud == false) {
        mensajeSwall("warning", "El tipo de solicitud es obligatorio seleccionar / The type of request is mandatory to select" ,"Tipo de Solicitud / Type of request");        
        return false;
    }
    else {
        var _tipoSolictud = $("input[type='radio'].radSolicitud:checked").val();
        //var _tipoSolictud = _radioSolicitud.val();
        if (_tipoSolictud == "33" && filaRow > 0) {
            mensajeSwall("warning", "No puede ingresar más de dos rutas de vuelo / You cannot enter more than two flight paths", "Ruta del vuelo / Flight path"); 
            $("#txtFehaVuelo").val("");
            $("#txtOrigen").val("");
            //$("#CboAeropuertoAterrizaje").val("");
            return false;
        }
        else if (_tipoSolictud == "34" && filaRow > 3) {
            mensajeSwall("warning", "No puede ingresar más de cuatro rutas de vuelo / You cannot enter more than four flight paths", "Ruta del vuelo / Flight path"); 
            $("#txtFehaVuelo").val("");
            $("#txtOrigen").val("");
            //$("#CboAeropuertoAterrizaje").val("");
            return false;
        }
    }
    $.post($.MisUrls.url._GetCiudadPorCodigoExiste, { codigo: oAtoOrigen }, function (htmlCia) {
        if (htmlCia) {
            if (validaCamposRuta()) {
                var fechaVuelo = formatDate(ofechaVuelo);
                $("<tr>").append(
                    $("<td>").text(fechaVuelo),
                    $("<td>").text(oAtoOrigen.toUpperCase()),
                    $("<td>").text(oAtoDestino.toUpperCase()),
                    $("<td>").html('<a id="btnEliminarRuta" href="#" class="skiplink-text" onclick="EliminarRuta(this);"><i class="ti-trash"></i>Eliminar</a>')
                ).appendTo("#tbDetalleRuta tbody");

                $("#txtFehaVuelo").val("");
                $("#txtOrigen").val("");
                //$("#CboAeropuertoAterrizaje").val("0");
            }


        }
        else {
            mensajeSwall("warning", "El código del aeropuerto no existe, modifique.", "");
            return false;
        }
    });
};

function EliminarRuta(row) {
    Swal.fire({
        title: 'Ruta de vuelo',
        text: "¿Desea eliminar la ruta del vuelo seleccionada?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#C5C7CF',
        confirmButtonText: 'Si',
        cancelButtonText: "No",
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            var d = row.parentNode.parentNode.rowIndex;
            document.getElementById('tbDetalleRuta').deleteRow(d);
        }
    })
}


function validaCamposRuta() {
    let ofechaVuelo = $("#txtFehaVuelo").val();
    let oAtoOrigen = $("#txtOrigen").val();
    let oAtoDestino = $("#CboAeropuertoAterrizaje").val();
    let tituloMensaje = "Ruta del vuelo / Flight path";

    $('#txtFehaVuelo').removeClass("border-danger");
    $('#txtOrigen').removeClass("border-danger");
    $('#CboAeropuertoAterrizaje').removeClass("border-danger");

    if (ofechaVuelo.trim().length == 0) {
        mensajeSwall("warning", "La fecha de vuelo, es obligatorio llenar / The flight date, it is mandatory to fill", tituloMensaje);
        $('#txtFehaVuelo').addClass("border-danger");
        return false;
    }
    else {
        var ofechaActual = $("#FechaEnvioSolicitud").val();
        var datefechaActual = new Date(ofechaActual);
        var datefechaVlo = new Date(ofechaVuelo);
        if (datefechaVlo < datefechaActual) {
            mensajeSwall("warning", "La fecha de vuelo es menor a la fecha actual, modifique / The flight date is less than the current date, please modify", tituloMensaje);           
            $('#txtFehaVuelo').addClass("border-danger");
            return false;
        }
    }
    let termino = "SE";
    //let posicion = oAtoOrigen.toLowerCase().indexOf(termino.toLowerCase())    
    //startsWith

    if (oAtoOrigen.trim().length == 0) {
        mensajeSwall("warning", "La ruta del aeropuerto origen, es obligatorio llenar / The route of the origin airport, it is mandatory to fill in", tituloMensaje);                   
        $('#txtOrigen').addClass("border-danger");
        return false;
    }
    else if (oAtoOrigen.trim().length > 0) {
        let estado = oAtoOrigen.toLowerCase().startsWith(termino.toLowerCase())
        if (estado) {
            mensajeSwall("warning", "Este formulario está diseñado para solicitudes de aeronaves privadas y matrícula extranjera que provienen del extranjero y no de aeropuertos nacionales. / This form is designed for applications for private aircraft and foreign registration that come from abroad and not from national airports.", tituloMensaje);                   
            $('#txtOrigen').addClass("border-danger");
            return false;
        }
    }   

    if (oAtoDestino.trim().length == 0 || oAtoDestino.trim() == '0') {
        mensajeSwall("warning", "La ruta del aeropuerto destino, es obligatorio llenar / The route of the destination airport, it is mandatory to fill in", tituloMensaje);                           
        $('#CboAeropuertoAterrizaje').addClass("border-danger");
        return false;
    }

    return true;
}

function formatDate(userDate) {
    // split date string at '-'
    var dateArr = userDate.split('-');

    //test results of split
    console.log(dateArr[0]);
    console.log(dateArr[1]);
    console.log(dateArr[2]);

    // check for single number dar or month
    // prepend '0' to single number dar or month
    if (dateArr[0].length == 1) {
        dateArr[0] = '0' + dateArr[0];
    } else if (dateArr[1].length == 1) {
        dateArr[1] = '0' + dateArr[1];
    }

    // concatenate new values into one string
    userDate = dateArr[2] + "/" + dateArr[1] + "/" + dateArr[0];

    // test new string value
    console.log(userDate);

    // return value
    return userDate;
}


function buscaAeronavesPorMatriculaVer() {
    var oMatricula = $("#txtMatricula").val();
    if (oMatricula != "") {
        //$('.dropAeronave').show();      
        jQuery.ajax({
            url: $.MisUrls.url._FormularioBuscaAeronaesPorMatriculaPrivada + "?matricula=" + oMatricula,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != undefined && data != null) {
                    $("#tbAeronaves tbody").html("");
                    $.each(data, function (i, row) {
                        $("<tr>").append(
                            $("<td class='col-sm-4'>").append("<a  href='#' onclick='seleccionaMatricula(" + JSON.stringify(row) + ");'>" + row["Matricula"] + "</a>"),
                            $("<td class='col-sm-4'>").text(row["Marca"]),
                            $("<td class='col-sm-4'>").text(row["Modelo"])
                        ).appendTo("#tbAeronaves tbody");
                        $('.loading-circleAro').show();
                    })
                    if (data.length === 0) {
                        limpiarHabilitarAeronave();
                        $('#modal-informacionMatricula').modal('show');
                    }
                    //$("#Matricula").val("");

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

function limpiarHabilitarAeronave() {
    $("#Marca").val("");
    $('#Marca').prop('readonly', true);
    $("#Modelo").val("");
    $('#Modelo').prop('readonly', true);
    $("#PesoMTOW").val("");
    $('#PesoMTOW').prop('readonly', true);
}


function seleccionaMatricula(json) {
    if (json != null) {
        $("#txtMatricula").val(json.Matricula);
        if (json.Marca == "") {
            $("#Marca").val("");
            $('#Marca').prop('readonly', true);
        } else {
            $("#Marca").val(json.Marca);
            $('#Marca').prop('readonly', true);
        }

        if (json.Modelo == "") {
            $("#Modelo").val("");
            $('#Modelo').prop('readonly', true);
        } else {
            $("#Modelo").val(json.Modelo);
            $('#Modelo').prop('readonly', true);
        }

        if (json.PesoWTO == "") {
            $("#PesoMTOW").val("");
            $('#PesoMTOW').prop('readonly', true);
        } else {
            $("#PesoMTOW").val(json.PesoWTO);
            $('#PesoMTOW').prop('readonly', true);
        }
        $('.loading-circleAro').hide();
    }
}

//Funcion solo numeros
function valideKey(evt) {

    // code is the decimal ASCII representation of the pressed key.
    var code = (evt.which) ? evt.which : evt.keyCode;

    if (code === 8) { // backspace.
        return true;
    } else if (code >= 48 && code <= 57) { // is a number.
        return true;
    } else { // other keys.
        return false;
    }
}

function isPrice(evt, value) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((value.indexOf(',') !== -1) && (charCode !== 45 && (charCode < 48 || charCode > 57)))
        return false;
    else if (charCode !== 45 && (charCode !== 46 || $(this).val().indexOf(',') !== -1) && (charCode < 48 || charCode > 57))
        return false;
    return true;
}


function filterFloat(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
function filter(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}

function validarEmail(valor) {
    if (valor.length > 0) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,10})+$/.test(valor)) {
            return true;
        } else {
            return false;
        }
    }
    else {
        return false;
    }
}

function validaTelefono(phoneNumber) {
    var found = phoneNumber.search(/^(\+{1}\d{2,3}\s?[(]{1}\d{1,3}[)]{1}\s?\d+|\+\d{2,3}\s{1}\d+|\d+){1}[\s|-]?\d+([\s|-]?\d+){1,2}$/);
    if (found > -1) {
        return true;
    }
    else {
        return false;
    }
}


//Funcion solo numeros 
function isTelefono(evt) {

    // code is the decimal ASCII representation of the pressed key.
    var code = (evt.which) ? evt.which : evt.keyCode;

    if (code === 8) { // backspace.
        return true;
    } else if (code >= 48 && code <= 57) { // is a number.
        return true;
    } else if (code == 43) { // is a +.
        return true;
    }
    else { // other keys.
        return false;
    }
}

function esEntero(numero) {
    if (isNaN(numero)) {
        return false;
    } else {
        return true;
    }
}

function mensajeSwall(icono, mensaje, titulo) {
    Swal.fire({
        icon: icono,
        title: "<p style='width: 100 %; font-size: 14px;'>" + titulo + "</p>",
        html: mensaje,
        confirmButtonText: 'Aceptar / Ok',
        allowOutsideClick: false,
    });
}

var numeros = "+0123456789";
function tiene_numeros(texto) {
    for (i = 0; i < texto.length; i++) {
        if (numeros.indexOf(texto.charAt(i), 0) != -1) {
            return true;
        }
    }
    return false;
}