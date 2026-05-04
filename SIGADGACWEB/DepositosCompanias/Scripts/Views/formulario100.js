var tablaAeronave
var tablaDetalleRuta
$(document).ready(function () {
    loadDataTable();
    loadDataTableRuta();
    validaCamposAplicante();
    //Alphanumérico y sin espacios  
    $("#NombreResponsableContacto").bind('keypress', function (event) {
        var regex = new RegExp("^[A-Za-z0-9]*[ ]?[A-Za-z0-9]*$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //Alphanumérico y sin espacios  
    $("#NombreCompaniaAviacion").bind('keypress', function (event) {
        var regex = new RegExp("^[A-Za-z0-9]*[ ]?[A-Za-z0-9]*$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //Alphanumérico y sin espacios  
    $("#MatriculaAeronave").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z0-9]$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //Alphanumérico y sin espacios  
    $("#CboAeropuertoOrigen").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z]$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $("#NombreCompaniaAviacion").on('keydown', function (e) {
        if (e.key == 'Enter') {
            modalCompania($("#NombreCompaniaAviacion").val());
        }
    });

    $("#MatriculaAeronave").on('keydown', function (e) {
        if (e.key == 'Enter') {
            //let omatricula = $("#txtMatricula").val();
            buscaAeronavesPorMatriculaVer();
        }
    });

    $('#FileCertificadoOperador').on('change', function () {
        var ext = $(this).val().split('.').pop();
        const fileSize = $(this)[0].files[0].size;
        if ($(this).val() != '') {
            if (ext == "PDF" || ext == "pdf") {
                if (fileSize > 2097152) {
                    $('#FileCertificadoOperador').val("");
                    MensajeIco("¡Precaución!", "El documento excede el tamaño máximo, se solicita un archivo no mayor a 2MB. Por favor verifica.", 'warning');
                    $(this).val('')
                }
            }
            else {
                $('#FileCertificadoOperador').val("");
                MensajeIco("¡Precaución!", "Solo puede subir archivos de tipo pdf no esta permitido : " + ext, 'warning');
            }
        }
        $(".labelFile").html($('#FileCertificadoOperador').val());
    });

    $("#PropositoVuelo").change(function () {
        var _proposito = $(this).val();
        if (buscarPalabra(_proposito, "OTROS")) {
            $(".especifica-proposito").css("display", "block");
        }
        else {
            $("#Observacion").val("");
            $(".especifica-proposito").css("display", "none");
        }
    });

    $("#CboAeropuertoOrigen").change(function () {
        var _rutaVuelo = $(this).val().toUpperCase();
        if (_rutaVuelo == "ZZZZ") {
            $('#ComentarioVuelo').prop('readonly', false);
        }
        else {
            $("#ComentarioVuelo").val("");
            $('#ComentarioVuelo').prop('readonly', true);
        }
    });
    $('.closeAeronave').click(function () {
        $('.loading-circleAro').hide();
    });


    //Botone de anance

    $('#btnSiguienteR').click(function () {
        if (validaCamposAplicante()) {
            $("#vert-tabs-operador-tab").trigger('click');
        }
    });

    $('#btnAnteriorO').click(function () {
        $("#vert-tabs-aplicante-tab").trigger('click');
    });

    $('#btnSiguienteO').click(function () {
        if (validaCamposOperador()) {
            $("#vert-tabs-aeronave-tab").trigger('click');
        }
    });

    $('#btnAnteriorA').click(function () {
        $("#vert-tabs-operador-tab").trigger('click');
    });

    $('#btnSiguienteA').click(function () {
        if (validaCamposOperador()) {
            if (validaCamposAeronaves()) {
                if (tieneDatosTablaAeronave()) {
                    $("#vert-tabs-vuelo-tab").trigger('click');
                }
                else {
                    MensajeIco("Datos de la Aeronave / Aircraft data", "<ul><li>La documentación técnica no está adjunta a la matrícula / The technical documentation is not attached to the registration</li></ul>", "warning");
                }
            }
        }
        else {
            $("#vert-tabs-operador-tab").trigger('click');
        }
       
    });

    $('#btnAnteriorV').click(function () {
        $("#vert-tabs-aeronave-tab").trigger('click');
    });

    //Presenta el modal de envio.
    $('#btnSiguienteV').click(function () {
        if (validaCamposAplicante()) {
            if (validaCamposOperador()) {
                if (validaCamposAeronaves()) {
                    if (validaCamposVuelo()) {
                        if (tieneDatostablaDetalleRuta()) {
                            $('#modalEnviarTramite').modal('show');
                        }
                        else {
                            MensajeIco("Datos de la Aeronave / Aircraft data", "<ul><li>La Ruta de vuelo no está ingresa para está solicitud / The flight route is not entered for this request.</li></ul>", "warning");
                        }
                    }
                    else {
                        $("#vert-tabs-Vuelo-tab").trigger('click');
                    }
                }
                else {
                    $("#vert-tabs-aeronave-tab").trigger('click');
                }
            }
            else {
                $("#vert-tabs-operador-tab").trigger('click');
            }
        }
        else {
            $("#vert-tabs-aplicante-tab").trigger('click');
        }
    });

    //Envioa a grabar la solictud
    $('#enviarTramite').click(function () {
        let _matriculaAeronave = $("#MatriculaAeronave").val();
        let _idCia = $("#IdCompaniaOperador").val();
        if (validaCamposAplicante()) {
            if (validaCamposOperador()) {
                if (validaCamposAeronaves()) {
                    if (validaCamposVuelo()) {
                        if (tieneDatostablaDetalleRuta()) {
                            let tablaTitulo = "";
                            tablaTitulo = "<div style='width:100%; overflow-x: hidden; overflow-y: auto; height: 5em; border: 0px solid'><table class='table table-bordered table-hover' style='width:100%; font-size: 10px;'><thead><tr><th>RUC</th><th>COMPAÑIA</th><th>MATRICULA</th><th>FACTURA</th></thead><tbody>";
                            let mensajeAdeuda = "<ul> <li><p style='color: #ff0000; font-size: 14px; text-align:justify'>No se puede tramitar su solicitud porque la aeronave y/o la compañía que paga tiene deuda pendiente con la DGAC. Por favor, comuníquese con el área financiera al +593  (2) 294-7400 ext 4720, De lunes a viernes de 8:00 a 16:30(Hora de Ecuador).</p></li><li><p style='color: #242495; font-size: 14px; text-align:justify'>Your request cannot be processed because the aircraft and/or the company has an outstanding debt with the DGAC. Please contact the financial area at +593 (2) 294-7400 ext 4720, Monday to Friday from 8:00 a.m. to 4:30 p.m. (Ecuador time).</p></li></ul>";
                            $.post($.MisUrls.url._MatriculaExisteVueloPrivado, { matricula: _matriculaAeronave }, function (htmlMatricula) {
                                if (htmlMatricula == true) {
                                    $.post($.MisUrls.url._FormularioListarCiaDeudoraExiste, { idCia: _idCia }, function (htmlCia) {
                                        if (htmlCia.length == 0) {
                                            var _NumeroSolicitud = $('#NumeroSolicitud').val();
                                            var _FechaEnvioSolicitud = $('#FechaEnvioSolicitud').val();
                                            var _TipoSolictud = $("input[type='radio'].radSolicitud:checked").val();
                                            var _NombreResponsableContacto = $('#NombreResponsableContacto').val();
                                            var _DireccionResponsableContacto = $('#DireccionResponsableContacto').val();
                                            var _TelefonoResponsableContacto = $('#TelefonoResponsableContacto').val();
                                            var _CorreoResponsableContacto = $('#CorreoResponsableContacto').val();
                                            var _NombreCompaniaAviacion = $('#NombreCompaniaAviacion').val();
                                            var _Direccion = $('#Direccion').val();
                                            var _Telefono = $('#Telefono').val();
                                            var _Email = $('#Email').val();
                                            var _MatriculaAeronave = $('#MatriculaAeronave').val();
                                            var _Marca = $('#Marca').val();
                                            var _Modelo = $('#Modelo').val();
                                            var _MtowAeronave = $('#MtowAeronave').val();
                                            var _PropositoVuelo = $('#PropositoVuelo').val();
                                            var _Observacion = $('#Observacion').val();
                                            var _IdCompaniaOperador = $('#IdCompaniaOperador').val();
                                            var _IdFleteador = $('#IdFleteador').val();
                                            //Hay que poner la validacion por aeronave
                                            $.post($.MisUrls.url._FormularioDeudorPorAeronave, { matricula: _matriculaAeronave }, function (htmlMatricula) {
                                                if (htmlMatricula.length == 0) {
                                                    $('.loading-circle').show();
                                                    $('#enviarSolicutd').attr('disabled', true);
                                                    //$("#registrarFormularioPrivado").submit();
                                                    var modeloSol = {
                                                        NumeroSolicitud: _NumeroSolicitud,
                                                        FechaEnvioSolicitud: _FechaEnvioSolicitud,
                                                        TipoSolictud: _TipoSolictud,
                                                        NombreResponsableContacto: _NombreResponsableContacto,
                                                        DireccionResponsableContacto: _DireccionResponsableContacto,
                                                        TelefonoResponsableContacto: _TelefonoResponsableContacto,
                                                        CorreoResponsableContacto: _CorreoResponsableContacto, 
                                                        NombreCompaniaAviacion: _NombreCompaniaAviacion,
                                                        Direccion: _Direccion,
                                                        Telefono: _Telefono,
                                                        Email: _Email,
                                                        MatriculaAeronave: _MatriculaAeronave,  
                                                        Marca: _Marca,
                                                        Modelo: _Modelo,
                                                        MtowAeronave: _MtowAeronave,
                                                        PropositoVuelo: _PropositoVuelo,
                                                        Observacion: _Observacion,
                                                        IdCompaniaOperador: _IdCompaniaOperador,
                                                        IdFleteador: _IdFleteador,
                                                    };                                                   
                                                    jQuery.ajax({
                                                        url: $.MisUrls.url._grabaFormularioVueloPrivado,
                                                        type: "POST",
                                                        data: JSON.stringify(modeloSol),
                                                        dataType: "json",
                                                        contentType: "application/json; charset=utf-8",
                                                        success: function (data) {
                                                            if (data.resultado) {
                                                                // Redirigir a la vista "Confirmacion"
                                                                window.location.href = data.redirectUrl;                                                               
                                                            } else {
                                                                MensajeIco("Mensaje", data.mensaje, "warning");                                                                
                                                            }
                                                        },
                                                        error: function (error) {
                                                            console.log(error)
                                                        },
                                                        beforeSend: function () {

                                                        },
                                                    });

                                                    

                                                }
                                                else {
                                                    let otitulo = "Datos de la Aeronave / Aircraft data \n(" + _matriculaAeronave + ")";
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
                                            let otitulo = "Datos del operador / Operator's data \n(" + _nombreCompaniaAviacion.trim() + ")";
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
                                    $('#modal-informacionMatricula').modal('show');
                                }
                            });
                        }
                        else {
                            MensajeIco("Datos de la Aeronave / Aircraft data", "<ul><li>La Ruta de vuelo no está ingresa para está solicitud / The flight route is not entered for this request.</li></ul>", "warning");
                        }
                    }
                    else {
                        $("#vert-tabs-Vuelo-tab").trigger('click');
                    }
                }
                else {
                    $("#vert-tabs-aeronave-tab").trigger('click');
                }
            }
            else {
                $("#vert-tabs-Operador-tab").trigger('click');
            }
        }
        else {
            $("#vert-tabs-aplicante-tab").trigger('click');
        }

    });

});
function loadDataTable() {
    var oidSolicitud = $('#NumeroSolicitud').val();
    //var matricula = $('#MatriculaAeronave').val();
    tablaAeronave = $('#tableAeronaves').DataTable({
        "ajax": {
            "url": $.MisUrls.url._obtenerAdjuntosAeronavePorOidSolicitud + "?oidSol=" + oidSolicitud,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "IdAdjunto" },
            { "data": "DescripcionTipoSolicitud" },
            { "data": "NombreArchivo" },
            {
                "data": "IdAdjunto", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminarAdjunto(" + data + ")'><i>Eliminar</i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }

        ],
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

function loadDataTableRuta() {
    var oidSolictud = $('#NumeroSolicitud').val();
    tablaDetalleRuta = $('#tbDetalleRuta').DataTable({
        "ajax": {
            "url": $.MisUrls.url._obtenerDetalleRutaPorOid + "?oidSol=" + oidSolictud,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "IdRuta" },
            { "data": "FechaIdaVuelo" },
            { "data": "RutaOrigenInicio" },
            { "data": "RutaDestino" },
            {
                "data": "IdRuta", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminarRuta(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }

        ],
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
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }
    if (_direccionFleteador.trim().length == 0) {
        mensaje = "";
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del aplicante no es válido actualice la información / <i style='color: #242495;'>The applicant's address is not valid, update the information<</i> </p></li></ul>";
        $('#DireccionResponsableContacto').addClass("border-danger");
        $('#DireccionResponsableContacto').prop('readonly', false);
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }
    if (_telefonoFleteador.trim().length == 0) {
        mensaje = "";
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El teléfono del aplicante no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>The applicant's telephone number is not valid, update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        $('#TelefonoResponsableContacto').addClass("border-danger");
        $('#TelefonoResponsableContacto').prop('readonly', false);
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }
    if (_correoFleteador.trim().length == 0) {
        mensaje = "";
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>El correo del aplicante no es válido actualice la información / <i style='color: #242495;'>The applicant's email is not valid, update the information</i> </p></li></ul>";
        $('#CorreoResponsableContacto').addClass("border-danger");
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }
    else if (!validarEmail(_correoFleteador)) {
        mensaje = "";
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La dirección del correo electrónico del aplicante no es valido actualice la información / <i style='color: #242495;'>The applicant's email address is not valid, update the information</i> </p></li></ul>";
        $('#CorreoResponsableContacto').addClass("border-danger");
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }

    return true;
}

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
                    if (data.length == 0) {
                        $("#Direccion").val("");
                        $('#Direccion').prop('readonly', true);
                        $("#Telefono").val("");
                        $('#Telefono').prop('readonly', true);
                        $("#Email").val("");
                        $('#Email').prop('readonly', true);
                        $('#modal-informacionOperador').modal('show');
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

function validaCamposOperador() {
    var mensaje = "";
    cambiaColorCompania();
    var _IdCompaniaOperador = $("#IdCompaniaOperador").val();
    var _NombreCompaniaAviacion = $("#NombreCompaniaAviacion").val();
    var _Direccion = $("#Direccion").val();
    var _Telefono = $("#Telefono").val();
    var _Email = $("#Email").val();

    if (_NombreCompaniaAviacion.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Nombre de la empresa u operador no es válido actualice la información /<i style='color: #242495;'>Company or operator name is not valid, please update the information.</i></p></li></ul>";
        $('#NombreCompaniaAviacion').addClass("border-danger");
    }
    if (_Direccion.length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Dirección no es válido actualice la información /<i style='color: #242495;'>Address is not valid update the information</i></p></li></ul>";
        $('#Direccion').addClass("border-danger");
        $('#Direccion').prop('readonly', false);
    }

    if (_Telefono.length == 0) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
        $("#Telefono").val("");
    }
    else if (_Telefono.length < 7) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
        $("#Telefono").val("");
    }
    else if (!tiene_numeros(_Telefono)) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Teléfono no es válido actualice la información. Signo más + número de prefijo del país + número de teléfono: +593555005500 / <i style='color: #242495;'>Telephone is not valid update the information. Plus sign + country prefix number + phone number: +593555005500</i> </p></li></ul>";
        $('#Telefono').addClass("border-danger");
        $('#Telefono').prop('readonly', false);
    }

    if (!validarEmail(_Email)) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>Correo no es válido actualice la información / <i style='color: #242495;'>Email is not valid update the information</i> </p></li></ul>";
        $('#Email').addClass("border-danger");
        $('#Email').prop('readonly', false);
    }

    $('.loading-circleOperador').hide();
    if (mensaje != "") {
        MensajeIco("<p style='width: 100 %; font-size: 14px;'>Datos del operador / Operator's data</p>", "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. \n" + mensaje + "\nNo consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>", "warning");
        return false;
    }
    return true;
}

function cambiaColorCompania() {
    $('#NombreCompaniaAviacion').removeClass("border-danger");
    $('#Direccion').removeClass("border-danger");
    $('#Telefono').removeClass("border-danger");
    $('#Email').removeClass("border-danger");
}

function seleccionarCompania(json) {
    if (json != null) {
        cambiaColorCompania();
        var mensaje = "";
        $("#IdCompaniaOperador").val(json.IdCompania);
        $("#NombreCompaniaAviacion").val(json.NombreCompaniaAviacion);
        $("#txtNombreCiaOperadora").val(json.NombreCompaniaAviacion);

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
        $('.loading-circleOperador').hide();
        if (mensaje != "") {
            MensajeIco("<p style='width: 100 %; font-size: 14px;'>Datos del operador / Operator's data</p>", "<div class='text-blue text-justify'>La Dirección General de Aviación Civil le informa que. \n" + mensaje + "\nNo consta en nuestra base de datos. A fin de hacer su respectivo ingreso favor enviar su información de compañía a la siguiente dirección: autorizaciones.privados@aviacioncivil.gob.ec adjuntando los documentos habilitantes de la misma.</div>", "warning");
            return false;
        }
    } else {
        $("#NombreCompaniaAviacion").val("");
        MensajeIco("Mensaje", "No existe datos del operador", "warning");
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

function buscaAeronavesPorMatriculaVer() {
    var oMatricula = $("#MatriculaAeronave").val();
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
    $("#MtowAeronave").val("");
    $('#MtowAeronave').prop('readonly', true);
}

function seleccionaMatricula(json) {
    if (json != null) {                
        $.post($.MisUrls.url._verificaExisteMatriculaHelicoptero + "?matricula=" + json.Matricula , null, function (htmlExiste) {          
            if (htmlExiste.respuesta == true) {
                $('.activa-helicopter').show();
            }
            else {
                $('.activa-helicopter').hide();
            }            
        });
        $("#MatriculaAeronave").val(json.Matricula);
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
            $("#MtowAeronave").val("");
            $('#MtowAeronave').prop('readonly', true);
        } else {
            $("#MtowAeronave").val(json.PesoWTO);
            $('#MtowAeronave').prop('readonly', true);
        }
        $('.loading-circleAro').hide();

    }
}

function cambiaColorAeronave() {
    $('#MatriculaAeronave').removeClass("border-danger");
    $('#Marca').removeClass("border-danger");
    $('#Modelo').removeClass("border-danger");
    $('#MtowAeronave').removeClass("border-danger");
}
function validaCamposAeronaves() {
    cambiaColorAeronave();
    var mensaje = "";
    var _matricula = $("#MatriculaAeronave").val();
    var _marca = $("#Marca").val();
    var _modelo = $("#Modelo").val();
    var _pesoMtow = $("#MtowAeronave").val();
    var _titulo = "Datos de la Aeronave / Aircraft data";
    if (_matricula.trim().length == 0) {
        mensaje = "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La matrícula de la aeronave no es valido actualice la información / <i style='color: #242495;'>The aircraft registration is not valid update the information</i> </p></li></ul>";
        $('#MatriculaAeronave').addClass("border-danger");
    }

    if (_marca.trim().length == 0) {
        mensaje = mensaje + "<ul><li><p style='color: #ff0000; font-size: 14px; text-align:justify'>La marca de la aeronave no es valido actualice la información / <i style='color: #242495;'>The brand of the aircraft is not valid, update the information</i> </p></li></ul>";
        $('#Marca').addClass("border-danger");
    }

    if (_modelo.trim().length == 0) {
        mensaje = mensaje + "<b>El modelo de la aeronave no es valido actualice la información / The aircraft model is not valid, update the information</b>";
        $('#Modelo').addClass("border-danger");
    }

    if (_pesoMtow.trim().length == 0) {
        mensaje = mensaje + "<b>El Peso MTOW de la aeronave no es valido actualice la información / The MTOW Weight of the aircraft is not valid, update the information</b>";
        $('#MtowAeronave').addClass("border-danger");
    }
    else if (esEntero(_pesoMtow) && _pesoMtow <= 0) {
        mensaje = mensaje + "<b>El Peso MTOW de la aeronave no es valido actualice la información / The MTOW Weight of the aircraft is not valid, update the information</b>";
        $('#MtowAeronave').addClass("border-danger");
    }

    if (mensaje.trim().length > 0) {
        MensajeIco(_titulo, mensaje, "warning");
        return false;
    }
    return true;
}

function cambiaColorVuelo() {
    $('#PropositoVuelo').removeClass("border-danger");
    $('#Observacion').removeClass("border-danger");
}

function validaCamposVuelo() {
    cambiaColorVuelo();
    var _PropositoVuelo = $('#PropositoVuelo').val();
    var _Observacion = $('#Observacion').val();
    var _titulo = "Ruta del vuelo / Flight path";

    if (ValidaCampoVacio(_PropositoVuelo) || _PropositoVuelo == "0") {
        MensajeIco(_titulo, "El proposito del vuelo, es obligatorio seleccionar / The purpose of the flight, it is mandatory to select", "warning");
        $('#PropositoVuelo').addClass("border-danger");
        return false;
    }
    else if (buscarPalabra(_PropositoVuelo, "OTROS")) {
        if (ValidaCampoVacio(_Observacion)) {
            MensajeIco(_titulo, "Especificar aquí el propósito del vuelo / Specify the purpose of the flight here", "warning");
            $('#Observacion').addClass("border-danger");
            return false;
        }
    }
    return true;
}
function tieneDatosTablaAeronave() {
    return tablaAeronave.data().any();
}

function tieneDatostablaDetalleRuta() {
    return tablaDetalleRuta.data().any();
}

function buscarPalabra(cadena, expression) {
    var index = cadena.search(expression);
    if (index >= 0) {
        return true;
    } else {
        return false;
    }
}
function guardarArchivo() {
    var _odSolicitud = $("#NumeroSolicitud").val();
    var _TipoAdjuntos = $("#TipoAdjuntos").val();
    var _matricula = $("#MatriculaAeronave").val();
    var valFile = $("#FileCertificadoOperador")[0];
    if (validaCamposAeronaves()) {
        if (_TipoAdjuntos.trim().length > 0 && valFile.files.length > 0) {
            $.post($.MisUrls.url._comprobarSessionPrivado, null, function (htmlSesion) {
                if (htmlSesion == true) {
                    $("#tableAeronaves tbody").html("");
                    var formData = new FormData();
                    formData.append("oidSolicitud", _odSolicitud);
                    formData.append("codigo", _TipoAdjuntos);
                    formData.append("filePrivado", valFile.files[0]);
                    //inicio ajax
                    $.ajax({
                        url: $.MisUrls.url._agregaDocumentacionTecnica,
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            if (result.respuesta) {
                                tablaAeronave.ajax.url($.MisUrls.url._obtenerAdjuntosAeronavePorOidSolicitud + "?oidSol=" + _odSolicitud).load();

                                //tablaAeronave.ajax.reload();
                                $("#TipoAdjuntos").val("0");
                                $("#FileCertificadoOperador").val(null);
                                $(".labelFile").html("Seleccione archivo");
                                return true;
                            } else {
                                return false;
                                MensajeIco("Archivo", result.mensaje, "error");
                                //alert("Error: " + result.mensaje);
                            }
                        },
                        error: function () {
                            return false;
                            MensajeIco("Archivo", "Error al procesar la solicitud", "error");

                        }
                    });
                    //Fin ajax
                }
                else {
                    SesionCaducada();
                }
            });
        }

    }
}

function eliminarAdjunto(id) {
    var oidSol = $("#NumeroSolicitud").val();
    Swal.fire({
        title: 'Ruta de vuelo',
        text: "¿Desea eliminar el adjunto seleccionado?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#C5C7CF',
        confirmButtonText: 'Si',
        cancelButtonText: "No",
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            $.post($.MisUrls.url._comprobarSessionPrivado, null, function (htmlSesion) {
                if (htmlSesion == true) {
                    jQuery.ajax({
                        url: $.MisUrls.url._eliminaAdjuntoAeronavePorOid + "?oid=" + id,
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.respuesta) {
                                tablaAeronave.ajax.url($.MisUrls.url._obtenerAdjuntosAeronavePorOidSolicitud + "?oidSol=" + oidSol).load();

                                //tablaAeronave.ajax.reload();
                                $("#TipoAdjuntos").val("0");
                                $("#FileCertificadoOperador").val(null);
                                $(".labelFile").html("Seleccione archivo");
                                return true;
                            } else {
                                MensajeIco("Mensaje", "No se pudo eliminar el ruta de vuelo", "warning")
                            }
                        },
                        error: function (error) {
                            console.log(error)
                        },
                        beforeSend: function () {

                        },
                    });
                }
                else {
                    SesionCaducada();
                }
            });
        }
    })
}
function AddRuta() {
    var oNumeroSolicitud = $("#NumeroSolicitud").val();
    var ofechaVuelo = $("#FechaIdaVuelo").val();
    var oAtoOrigen = $("#CboAeropuertoOrigen").val();
    var oAtoDestino = $("#CboAeropuertoAterrizaje").val();
    var oComentarioVuelo = "" //$("#ComentarioVuelo").val();
    var totalFilas = tablaDetalleRuta.data().count();

    var filaRow = $("#tbDetalleRuta  > tbody  > tr").length;
    var _radioSolicitud = $(".radSolicitud").is(":checked");

    if (validaCamposRuta()) {
        if (_radioSolicitud == false) {
            MensajeIco("Tipo de Solicitud / Type of request", "El tipo de solicitud es obligatorio seleccionar / The type of request is mandatory to select", "warning");
            return false;
        }
        else {
            var _tipoSolictud = $("input[type='radio'].radSolicitud:checked").val();
            //var _tipoSolictud = _radioSolicitud.val();
            if (_tipoSolictud == "33" && totalFilas > 0) {
                MensajeIco("Tipo de Solicitud / Type of request", "No puede ingresar más de dos rutas de vuelo / You cannot enter more than two flight paths", "warning");
                $("#FechaIdaVuelo").val("");
                $("#CboAeropuertoOrigen").val("");
                $("#CboAeropuertoAterrizaje").val("0");
                return false;
            }
            else if (_tipoSolictud == "34" && totalFilas > 3) {
                MensajeIco("Tipo de Solicitud / Type of request", "No puede ingresar más de cuatro rutas de vuelo / You cannot enter more than four flight paths", "warning");
                $("#FechaIdaVuelo").val("");
                $("#CboAeropuertoOrigen").val("");
                $("#CboAeropuertoAterrizaje").val("0");
                return false;
            }
            $.post($.MisUrls.url._comprobarSessionPrivado, null, function (htmlSesion) {
                if (htmlSesion == true) {
                    $.post($.MisUrls.url._GetCiudadPorCodigoExiste, { codigo: oAtoOrigen }, function (htmlCia) {
                        if (htmlCia) {
                            $("#tbDetalleRuta tbody").html("");
                            var formData = new FormData();
                            formData.append("oidSol", oNumeroSolicitud);
                            formData.append("oidDet", 0);
                            formData.append("rutaOrig", oAtoOrigen);
                            formData.append("rutaDest", oAtoDestino);
                            formData.append("rutaOriFin", "");
                            formData.append("fechaVuelo", ofechaVuelo);
                            formData.append("puntoSalida", oComentarioVuelo);
                            //inicio ajax
                            $.ajax({
                                url: $.MisUrls.url._agregaDetalleRuta,
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false,
                                success: function (result) {
                                    if (result.respuesta) {
                                        tablaDetalleRuta.ajax.url($.MisUrls.url._obtenerDetalleRutaPorOid + "?oidSol=" + oNumeroSolicitud).load();
                                        $("#FechaIdaVuelo").val("");
                                        $("#CboAeropuertoOrigen").val("");
                                        $("#CboAeropuertoAterrizaje").val("0");
                                        return true;
                                    } else {
                                        return false;
                                        MensajeIco("Archivo", result.mensaje, "error");
                                        //alert("Error: " + result.mensaje);
                                    }
                                },
                                error: function () {
                                    return false;
                                    MensajeIco("Archivo", "Error al procesar la solicitud", "error");

                                }
                            });
                            //Fin ajax
                        }
                        else {
                            var textoAerta = "<div style='text-align: justify'><strong>Notificación sobre aeropuerto no registrado y procedimiento de ingreso</strong><strong>Estimado Agente:</strong><p>Le informamos que el aeropuerto de origen o destino que ha intentado registrar no se encuentra en nuestra base de datos de rutas aéreas.</p>"
                                + "<p> Con el fin de atender su requerimiento de manera oportuna, le solicitamos que envíe la información completa del aeropuerto, incluyendo sus datos técnicos y de ubicación, a la siguiente dirección de correo electrónico: soporte.rutas@aviacioncivil.gob.ec. Nuestro equipo se encargará de realizar la validación y el ingreso correspondiente en el sistema.</p >"
                                    + "<p>Agradecemos su comprensión y colaboración en este proceso.</p>"
                                + "<strong>Notification Regarding Unregistered Airport and Entry Procedure</strong>"
                                + "<p><strong>Dear Agent,</strong></p>"
                                + "<p>We wish to inform you that the origin or destination airport you have attempted to register is not included in our air routes database.</p>"
                                + "<p>In order to process your request in a timely manner, we kindly ask that you send the complete airport information, including its technical and location data, to the email address soporte.rutas@aviacioncivil.gob.ec. Our team will be responsible for validating and entering the corresponding information into the system.</p>"
                                + "<p>We appreciate your understanding and cooperation in this process.</p></div>";
                            MensajeIcoAncho("Ruta del vuelo / Flight path", textoAerta, "warning");
                            $('#CboAeropuertoOrigen').addClass("border-danger");
                            return false;
                        }
                    });
                }
                else {
                    SesionCaducada();
                }
            });

        }
    }
};

function cambiaColorRutaVuelo() {
    $('#FechaIdaVuelo').removeClass("border-danger");
    $('#CboAeropuertoOrigen').removeClass("border-danger");
    $('#CboAeropuertoAterrizaje').removeClass("border-danger");
    $('#ComentarioVuelo').removeClass("border-danger");

}
function validaCamposRuta() {
    cambiaColorRutaVuelo();
    let ofechaVuelo = $("#FechaIdaVuelo").val();
    let oAtoOrigen = $("#CboAeropuertoOrigen").val();
    let oAtoDestino = $("#CboAeropuertoAterrizaje").val();
    let oComentarioVuelo = $("#ComentarioVuelo").val();

    let tituloMensaje = "Ruta del vuelo / Flight path";

    if (ofechaVuelo.trim().length == 0) {
        MensajeIco(tituloMensaje, "La fecha de vuelo, es obligatorio llenar / The flight date, it is mandatory to fill", "warning");
        $('#FechaIdaVuelo').addClass("border-danger");
        return false;
    }
    else {
        var ofechaActual = $("#FechaEnvioSolicitud").val();
        var datefechaActual = new Date(ofechaActual);
        var datefechaVlo = new Date(ofechaVuelo);
        if (datefechaVlo < datefechaActual) {
            MensajeIco(tituloMensaje, "La fecha de vuelo es menor a la fecha actual, modifique / The flight date is less than the current date, please modify", "warning");
            $('#FechaIdaVuelo').addClass("border-danger");
            return false;
        }
    }
    let termino = "SE";
    if (oAtoOrigen.trim().length == 0) {
        MensajeIco(tituloMensaje, "La ruta del aeropuerto origen, es obligatorio llenar / The route of the origin airport, it is mandatory to fill in", "warning");
        $('#CboAeropuertoOrigen').addClass("border-danger");
        return false;
    }
    else if ((oAtoOrigen.trim().length > 0) && (oAtoOrigen.trim().length == 4)) {
        let estado = oAtoOrigen.toLowerCase().startsWith(termino.toLowerCase())
        if (estado) {
            MensajeIco(tituloMensaje, "Este formulario está diseñado para solicitudes de aeronaves privadas y matrícula extranjera que provienen del extranjero y no de aeropuertos nacionales. / This form is designed for applications for private aircraft and foreign registration that come from abroad and not from national airports.", "warning");
            $('#CboAeropuertoOrigen').addClass("border-danger");
            return false;
        }
        else {
            if (oAtoOrigen == "zzzz" || oAtoOrigen == "ZZZZ") {
                if (oComentarioVuelo.trim().length == 0) {
                    MensajeIco(tituloMensaje, "Casilla 18 ZZZZ, especifique aquí su punto de salida (Max 60 caracteres). / Box 18 ZZZZ, specify your departure point here (Max 60 characters)", "warning");
                    $('#ComentarioVuelo').addClass("border-danger");
                    return false;
                }
            }
        }
    }
    else if (oAtoOrigen.trim().length < 4) {
        MensajeIco(tituloMensaje, "El aeropuerto de origen no es válido debe ingresar un código OACI (Cuatro letras) / The airport of origin is not valid, you must enter an ICAO code (four letters).", "warning");
        $('#CboAeropuertoOrigen').addClass("border-danger");
        return false;
    }

    if (oAtoDestino.trim().length == 0 || oAtoDestino.trim() == '0') {
        MensajeIco(tituloMensaje, "La ruta del aeropuerto destino, es obligatorio llenar / The route of the destination airport, it is mandatory to fill in", "warning");
        $('#CboAeropuertoAterrizaje').addClass("border-danger");
        return false;
    }

    return true;
}

function eliminarRuta(oidRuta) {
    var oidSol = $("#NumeroSolicitud").val();
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
            $.post($.MisUrls.url._comprobarSessionPrivado, null, function (htmlSesion) {
                if (htmlSesion == true) {
                    jQuery.ajax({
                        url: $.MisUrls.url._eliminaDetalleRutaPorOid + "?oidSol=" + oidSol + "&oidDet=" + oidRuta,
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.respuesta) {
                                tablaDetalleRuta.ajax.url($.MisUrls.url._obtenerDetalleRutaPorOid + "?oidSol=" + oidSol).load();
                                return true;
                            } else {
                                MensajeIco("Mensaje", "No se pudo eliminar el ruta de vuelo", "warning")
                            }
                        },
                        error: function (error) {
                            console.log(error)
                        },
                        beforeSend: function () {

                        },
                    });
                }
                else {
                    SesionCaducada();
                }
            });
        }
    })
}

function esEntero(numero) {
    if (isNaN(numero)) {
        return false;
    } else {
        return true;
    }
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

function SesionCaducada() {
    Swal.fire({
        title: 'Sesión',
        text: "Su sesión ha caducado. Vuelva a iniciar sesión",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar',
    }).then((result) => {
        if (result.isConfirmed) {
            location.reload(true);
        }
    })
}

function ValidaCampoVacio(_campo) {
    if (_campo == null || _campo == undefined || _campo == "") { return true }
    else { return false }
}

function MensajeIco(titulo, mensaje, icono) {
    Swal.fire({
        title: titulo,
        html: mensaje,
        icon: icono,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar - Ok',
        allowOutsideClick: false,
    });
}
function MensajeIcoAncho(titulo, mensaje, icono) {
    Swal.fire({
        title: titulo,
        html: mensaje,
        icon: icono,
        width: 900,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar - Ok',
        allowOutsideClick: false,
    });
}