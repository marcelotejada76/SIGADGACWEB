var tabladata;
$(document).ready(function () {
    cargaCombos();

    cargaDatosEmpleado();

    $('#FileFoto').on('change', function () {
        $(".fotoEmpleado").html($('#FileFoto').val());
        //$('#ComprobantePago').val($('#FileComprobantePago').val());
    });

    $('#adjuntoFile').on('change', function () {
        $(".custom-file-label").html($('#adjuntoFile').val());
        //$('#ComprobantePago').val($('#FileComprobantePago').val());
    });

    $("#grabarDatosAdiciones").click(function (e) {
        if (ValidaCamposAdicionalesEmpleado()) {
            //debugger
            e.preventDefault();
            var _DocumentoIdentificacion = $("#DocumentoIdentificacion").val();
            var _NombreContactoEmergencia = $("#NombreContactoEmergencia").val();
            var _TelefonoContactoEmergencia = $("#TelefonoContactoEmergencia").val();
            var _AlergiaMedicina = $("#AlergiaMedicina").val();
            var _AlergiaAlimentos = $("#AlergiaAlimentos").val();
            var _AlergiaMedioAmbiente = $("#AlergiaMedioAmbiente").val();
            var _Discapacidad = $("#Discapacidad").val();
            var _DiscapacidadNombre = $("#DiscapacidadNombre").val();
            var _Porcentaje = $("#Porcentaje").val();
            var _EnfermedadCatrastrofica = $("#EnfermedadCatrastrofica").val();
            var _EnfermedadCatrastroficaNombre = $("#EnfermedadCatrastroficaNombre").val();
            var _Sustituto = $("#Sustituto").val();
            var _NombreFamiliarSustituto = $("#NombreFamiliarSustituto").val();
            var _ParentescoSubtituto = $("#ParentescoSubtituto").val();
            var _SenescytNumeroRegistro = $("#SenescytNumeroRegistro").val();
            var _UltimoTituloObtenido = $("#UltimoTituloObtenido").val();
            var jsonObject = {
                "DocumentoIdentificacion": _DocumentoIdentificacion, "NombreContactoEmergencia": _NombreContactoEmergencia, "TelefonoContactoEmergencia": _TelefonoContactoEmergencia, "AlergiaMedicina": _AlergiaMedicina, "AlergiaAlimentos": _AlergiaAlimentos, "AlergiaMedioAmbiente": _AlergiaMedioAmbiente,
                "Discapacidad": _Discapacidad, "DiscapacidadNombre": _DiscapacidadNombre, "Porcentaje": _Porcentaje, "EnfermedadCatrastrofica": _EnfermedadCatrastrofica, "EnfermedadCatrastroficaNombre": _EnfermedadCatrastroficaNombre, "Sustituto": _Sustituto, "NombreFamiliarSustituto": _NombreFamiliarSustituto,
                "ParentescoSubtituto": _ParentescoSubtituto, "SenescytNumeroRegistro": _SenescytNumeroRegistro, "UltimoTituloObtenido": _UltimoTituloObtenido
            };

            //InicioGrabar
            $.ajax({
                url: $.MisUrls.url._DatosAdicionalesMaestroPersonal,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                data: JSON.stringify(jsonObject),
                success: function (result) {
                    if (result.Success) {
                        cargaDatosEmpleado();
                        Swal.fire("Datos empleado", result.Message, "warning");
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", errormessage, "warning");
                }
            });
            //FinGrabar
        }
    });

    var loadFile = function (event) {
        if (event) {

        }
        var reader = new FileReader();
        reader.onload = function () {
            var output = document.getElementById('imgEmpleado');
            output.src = reader.result;
            $(".custom-file-label").html($('#empleadoFoto').val());
        };
        reader.readAsDataURL(event.target.files[0]);
    };

    $('#CodigoProviencia').change(function () {

        var _codPais = $('#CodigoPais').val();
        var _codProviencia = $('#CodigoProviencia').val();
        var jsonInput = { codPais: _codPais, codProvincia: _codProviencia };

        //InicioGrabar
        $.ajax({
            url: $.MisUrls.url._GetSelectListCantonesList,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            data: JSON.stringify(jsonInput),
            success: function (result) {
                var selectCanton = $("#CodigoCanton");
                selectCanton.empty();
                $.each(result, function (index, itemData) {
                    selectCanton.append($('<option/>', {
                        value: itemData.Value,
                        text: itemData.Text
                    }));
                });
            },
            error: function (errormessage) {
                Swal.fire("Mensaje", errormessage, "warning");
            }
        });
        //FinGrabar
    });

    //Cuando cambia el canton
    $('#CodigoCanton').change(function () {
        cargaCanton();
    });

    $('#nuevoCurso').click(function () {
        cargaCombos();
        limpiaCamposCurso();
        var _DocumentoIdentificacion = $("#DocumentoIdentificacion").val();
        var _NombreCompleto = $("#NombreCompleto").val();
        $('#mdocumentoIdentidad').val(_DocumentoIdentificacion);
        $('#mnombreEmpleado').val(_NombreCompleto);
        
        $('#cursoModal').modal('show');
    });

    $("#btngrabarCurso").click(function (e) {
        e.preventDefault();
        if (validaCamposObligatorioCurso()) {
            var _documentoIdentidad = $('#mdocumentoIdentidad').val();
            var _codigoCurso = $('#mcodigoCurso').val();
            var _titulosSelect = $('#titulosSelect').val();
            var _descripcionTitulo = $('#mdescripcionTitulo').val();
            var _codigoDocumento = $('#mcodigoDocumento').val();
            var _fechaCurso = $('#mfechaCurso').val();
            var _duracion = $('#mduracion').val();
            var _tiempo = $('#tiempoSelect').val();
            var _aprobacion = $('#aprobacionSelect').val();
            var _asistencia = $('#asistenciaSelect').val();
            var _ciudad = $('#ciudadSelect').val();
            var _entidadEducativa = $('#entidadEducativaSelect').val();
            var _documentoCurso = $('#mdocumentoCurso').val();
            var _estadoCurso = $('#mestadoCurso').val();

            var jsonCurso = {
                "DocumentoIdentificacion": _documentoIdentidad, "CodigoCursoEmpleado": _codigoCurso, "CodigoTitulo": _titulosSelect, "DescripcionCurso": _descripcionTitulo, "NumeroIdentidicacion": _codigoDocumento, "FechaCurso": _fechaCurso,
                "DuracionCurso": _duracion, "TiempoCurso": _tiempo, "AprobacionCurso": _aprobacion, "AsistenciaCurso": _asistencia, "CodigoCiudad": _ciudad, "CodigoEntidadEducativa": _entidadEducativa, "PathDocumentoCurso": _documentoCurso,
                "EstadoCurso": _estadoCurso
            };

            var formData = new FormData();
            var file = document.getElementById("adjuntoFile").files[0];
            formData.append("adjuntoFile", file);

            formData.append("Curso", JSON.stringify(jsonCurso));

            $.ajax({
                type: "POST",
                url: $.MisUrls.url._CursoEmpleado,
                dataType: "json",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result.Success) {
                        $('#cursoModal').modal('hide');
                        tabladata.ajax.reload();
                       // var urlAction = $.MisUrls.url._EditarMaestroPersonal;
                       // window.location.href = urlAction;
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", errormessage, "warning");
                }
            });
        }
    });

    $("#btngrabarempleado").click(function (e) {
        e.preventDefault();
        if (validaCamposDatosEmpleado()) {
            var _documentoIdentidad = $('#DocumentoIdentificacion').val();
            var _nombreCompleto = $('#NombreCompleto').val();
            var _fechaNacimiento = $('#FechaNacimiento').val();
            var _numeroTelefonoDomicillo = $('#NumeroTelefonoDomicillo').val();
            var _numeroTelefonoCelular = $('#NumeroTelefonoCelular').val();
            var _emailPersonal = $('#EmailPersonal').val();
            var _genero = $('#Genero').val();
            var _colorOjos = $('#ColorOjos').val();
            var _colorCaballo = $('#ColorCaballo').val();
            var _tipoSangre = $('#TipoSangre').val();
            var _estadoCivil = $('#EstadoCivil').val();
            var _peso = $('#Peso').val();
            var _direccionDomicillo = $('#DireccionDomicillo').val();
            var _codigoPais = $('#CodigoPais').val();
            var _codigoProviencia = $('#CodigoProviencia').val();
            var _codigoCiudad = $('#CodigoCiudad').val();
            var _codigoCanton = $('#CodigoCanton').val();
            var _codigoParroquia = $('#CodigoParroquia').val();
            var _sectorDondeVive = $('#SectorDondeVive').val();
            var _regimenLaboral = $('#RegimenLaboral').val();
            var _tipoHorario = $('#TipoHorario').val();
            var _decimoTerceroAculado = $('#DecimoTerceroAculado').val();
            var _decimoCuartoAculado = $('#DecimoCuartoAculado').val();
            var _aporteFondoReserva = $('#AporteFondoReserva').val();
            var _pathFoto = $('#PathFoto').val();

            var jsonEmpleado = {
                "DocumentoIdentificacion": _documentoIdentidad, "NombreCompleto": _nombreCompleto, "FechaNacimiento": _fechaNacimiento, "NumeroTelefonoDomicillo": _numeroTelefonoDomicillo,
                "NumeroTelefonoCelular": _numeroTelefonoCelular, "EmailPersonal": _emailPersonal, "Genero": _genero, "ColorOjos": _colorOjos, "ColorCaballo": _colorCaballo, "TipoSangre": _tipoSangre,
                "EstadoCivil": _estadoCivil, "Peso": _peso, "DireccionDomicillo": _direccionDomicillo, "CodigoPais": _codigoPais, "CodigoProviencia": _codigoProviencia, "CodigoCiudad": _codigoCiudad,
                "CodigoCanton": _codigoCanton, "CodigoParroquia": _codigoParroquia, "SectorDondeVive": _sectorDondeVive, "RegimenLaboral": _regimenLaboral, "TipoHorario": _tipoHorario, "DecimoTerceroAculado": _decimoTerceroAculado,
                "DecimoCuartoAculado": _decimoCuartoAculado, "AporteFondoReserva": _aporteFondoReserva, "PathFoto": _pathFoto
            };

            var formData = new FormData();
            var file = document.getElementById("FileFoto").files[0];
            formData.append("adjuntoFoto", file);

            formData.append("Empleado", JSON.stringify(jsonEmpleado));

            $.ajax({
                type: "POST",
                url: $.MisUrls.url._DatosEmpleadoActualizar,
                dataType: "json",
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    if (result.Success) {
                        cargaDatosEmpleado();
                        Swal.fire("Datos empleado", "Los datos se guardo correctamente", "warning");
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", errormessage, "warning");
                }
            });
        }
    });

    $('#custom-tabs-four-profile-tab').click(function () {        
        tabladata.ajax.reload();
        tabladata.ajax.reload();
    });

});

function cargaDatosEmpleado() {
    $('.loadingBuscar').show();
    jQuery.ajax({
        url: $.MisUrls.url._ObtenerMaestroPersonal,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var encontrado = false;
            if (data.data != null) {
                $.each(data.data, function (i, item) {
                    $("#DocumentoIdentificacion").val(item.DocumentoIdentificacion);
                    $("#NombreCompleto").val(item.NombreCompleto);
                    $('#imgEmpleado').attr('src', item.imagenFoto);
                    $("#FechaNacimiento").val(item.FechaNacimiento);
                    $("#NumeroTelefonoDomicillo").val(item.NumeroTelefonoDomicillo);
                    $("#NumeroTelefonoCelular").val(item.NumeroTelefonoCelular);
                    $("#EmailPersonal").val(item.EmailPersonal);
                    $("#Genero").val(item.Genero);
                    $("#ColorOjos").val(item.ColorOjos);
                    $("#ColorCaballo").val(item.ColorCaballo);
                    $("#TipoSangre").val(item.TipoSangre);
                    $("#EstadoCivil").val(item.EstadoCivil);
                    $("#Peso").val(item.Peso);
                    $("#DireccionDomicillo").val(item.DireccionDomicillo);
                    $("#CodigoPais").val(item.CodigoPais);
                    $("#CodigoProviencia").val(item.CodigoProviencia);
                    $("#CodigoCiudad").val(item.CodigoCiudad);
                    $("#CodigoCanton").val(item.CodigoCanton);
                    $("#CodigoParroquia").val(item.CodigoParroquia);
                    $("#SectorDondeVive").val(item.SectorDondeVive);
                    $("#RegimenLaboral").val(item.RegimenLaboral);
                    $("#TipoHorario").val(item.TipoHorario);
                    $("#DecimoTerceroAculado").val(item.DecimoTerceroAculado);
                    $("#DecimoCuartoAculado").val(item.DecimoCuartoAculado);
                    $("#AporteFondoReserva").val(item.AporteFondoReserva);
                    $("#PathFoto").val(item.PathFoto);
                    
                    //Datos adicionales
                    $("#NombreContactoEmergencia").val(item.NombreContactoEmergencia);
                    $("#TelefonoContactoEmergencia").val(item.TelefonoContactoEmergencia);
                    $("#AlergiaMedicina").val(item.AlergiaMedicina);
                    $("#AlergiaAlimentos").val(item.AlergiaAlimentos);
                    $("#AlergiaMedioAmbiente").val(item.AlergiaMedioAmbiente);
                    $("#Discapacidad").val(item.Discapacidad);
                    $("#DiscapacidadNombre").val(item.DiscapacidadNombre);
                    $("#Porcentaje").val(item.Porcentaje);
                    $("#EnfermedadCatrastrofica").val(item.EnfermedadCatrastrofica);
                    $("#EnfermedadCatrastroficaNombre").val(item.EnfermedadCatrastroficaNombre);
                    $("#Sustituto").val(item.Sustituto);
                    $("#NombreFamiliarSustituto").val(item.NombreFamiliarSustituto);
                    $("#ParentescoSubtituto").val(item.ParentescoSubtituto);
                    $("#SenescytNumeroRegistro").val(item.SenescytNumeroRegistro);
                    $("#SenescytNumeroRegistro").val(item.SenescytNumeroRegistro);
                    $("#UltimoTituloObtenido").val(item.UltimoTituloObtenido);
                    //Detalle de curso
                    cargaDetlleCurso(item.DocumentoIdentificacion);

                    encontrado = true;
                    $('.loadingBuscar').hide();
                    return false;
                })

                //if (encontrado) {
                // $('#modalload').modal('hide');
                //    $("#txtIdProducto").val("0");
                //    $("#txtNombre").val("");
                //    $("#txtDescripcion").val("");
                //}
            }

        },
        error: function (error) {
            $('.loadingBuscar').hide();
            console.log(error)
        },
        beforeSend: function () {
           // $('.loadingBuscar').hide();
        },
    });
}

function cargaDetlleCurso(idDocumento) {
    
    if (idDocumento.trim().length > 0) {
        tabladata = $('#tbCursoEmpleados').DataTable({
            "destroy": true,
            retrieve: true,
            "processing": true,
            scrollY: '400px',
            scrollCollapse: true,
            paging: false,
            info: false,
            autoWidth: true,
            "order": [[0, 'desc']],
            "ajax": {
                "url": $.MisUrls.url._ObtenerCursosPorCedula,
                "type": "GET",
                "data": { id: idDocumento },
                "datatype": "json"
            },
            "columns": [
                { "data": "DocumentoIdentificacion", "width": "10%", className: "text-center" },
                { "data": "DescripcionCurso", "width": "55%" },
                { "data": "FechaCurso", "width": "10%", "className": "text-center" },
                { "data": "EstadoCurso", "width": "10%", "className": "text-center" },
                {
                    "data": "DocumentoIdentificacion", "render": function (data, type, row, meta) {
                        if (row.PathDocumentoCurso != '') {
                            if (row.EstadoCurso != 'VA') {
                                return "<div class='btn-group'><button class='btn btn-primary btn-sm' type='button' onclick='editarCursoEmpleado(" + JSON.stringify(row) + ")'><i>Editar</i></button> &nbsp; <button class='btn btn-primary btn-sm' type='button' onclick='verDocumento(" + JSON.stringify(row) + ")'><i>Ver-Diploma</i></button></div>"
                            }
                            else {
                                return "<div class='btn-group'><button class='btn btn-primary btn-sm' type='button' onclick='verDocumento(" + JSON.stringify(row) + ")'><i>Ver-Diploma</i></button></div>"
                            }
                        }
                        else {
                            if (row.EstadoCurso != 'VA') {
                                return "<div class='btn-group'><button class='btn btn-primary btn-sm' type='button' onclick='editarCursoEmpleado(" + JSON.stringify(row) + ")'><i>Editar</i></button>"  
                            }
                        }
                        
                    },
                    "orderable": false,
                    "searchable": false,
                    "width": "15%",
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
    
}

function cargaCanton() {
    var _codPais = $('#CodigoPais').val();
    var _codProviencia = $('#CodigoProviencia').val();
    var _codCanton = $('#CodigoCanton').val();
    var jsonInput = { codPais: _codPais, codProvincia: _codProviencia, codCanton: _codCanton };

    //InicioGrabar
    $.ajax({
        url: "/TalentoHumano/GetSelectListParroquiasList",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify(jsonInput),
        success: function (result) {
            var selectParroquia = $("#CodigoParroquia");
            selectParroquia.empty();
            $.each(result, function (index, itemData) {
                selectParroquia.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //FinGrabar
}

function cambiaColorDatosEmpleaado() {
    $('#DocumentoIdentificacion').removeClass("border-danger");
    $('#NombreCompleto').removeClass("border-danger");
    $('#FechaNacimiento').removeClass("border-danger");
    $('#NumeroTelefonoDomicillo').removeClass("border-danger");
    $('#NumeroTelefonoCelular').removeClass("border-danger");
    $('#EmailPersonal').removeClass("border-danger");
    $('#Genero').removeClass("border-danger");
    $('#ColorOjos').removeClass("border-danger");
    $('#ColorCaballo').removeClass("border-danger");
    $('#TipoSangre').removeClass("border-danger");
    $('#EstadoCivil').removeClass("border-danger");
    $('#Peso').removeClass("border-danger");
    $('#DireccionDomicillo').removeClass("border-danger");
    $('#CodigoPais').removeClass("border-danger");
    
}

function validaCamposDatosEmpleado() {
    cambiaColorDatosEmpleaado();
    var _mensaje = '';
    var _documentoIdentidad = $('#DocumentoIdentificacion').val();
    var _nombreCompleto = $('#NombreCompleto').val();
    var _fechaNacimiento = $('#FechaNacimiento').val();
    var _numeroTelefonoDomicillo = $('#NumeroTelefonoDomicillo').val();
    var _numeroTelefonoCelular = $('#NumeroTelefonoCelular').val();
    var _emailPersonal = $('#EmailPersonal').val();
    var _genero = $('#Genero').val();
    var _colorOjos = $('#ColorOjos').val();
    var _colorCaballo = $('#ColorCaballo').val();
    var _tipoSangre = $('#TipoSangre').val();
    var _estadoCivil = $('#EstadoCivil').val();
    var _peso = $('#Peso').val();
    var _direccionDomicillo = $('#DireccionDomicillo').val();
    var _codigoPais = $('#CodigoPais').val();
    var _codigoProviencia = $('#CodigoProviencia').val();
    var _codigoCiudad = $('#CodigoCiudad').val();
    var _codigoCanton = $('#CodigoCanton').val();
    var _codigoParroquia = $('#CodigoParroquia').val();
    var _sectorDondeVive = $('#SectorDondeVive').val();
    var _regimenLaboral = $('#RegimenLaboral').val();
    var _tipoHorario = $('#TipoHorario').val();
    var _decimoTerceroAculado = $('#DecimoTerceroAculado').val();
    var _decimoCuartoAculado = $('#DecimoCuartoAculado').val();
    var _aporteFondoReserva = $('#AporteFondoReserva').val();

    if (_documentoIdentidad.trim().length == 0) {
        _mensaje = _mensaje + '<li>Documento de identificacion</li>';
        $('#DocumentoIdentificacion').addClass("border-danger");
    }
    if (_nombreCompleto.trim().length == 0) {
        _mensaje = _mensaje + '<li>Nombres y apellidos</li>';
        $('#NombreCompleto').addClass("border-danger");
    }

    if (_fechaNacimiento.trim().length == 0) {
        _mensaje = _mensaje + '<li>Fecha nacimiento</li>';
        $('#FechaNacimiento').addClass("border-danger");
    }

    if (_numeroTelefonoDomicillo.trim().length == 0) {
        _mensaje = _mensaje + '<li>Número de teléfono</li>';
        $('#NumeroTelefonoDomicillo').addClass("border-danger");
    }

    if (_emailPersonal.trim().length == 0) {
        _mensaje = _mensaje + '<li>Dirección del correo electrónico o email</li>';
        $('#EmailPersonal').addClass("border-danger");
    }
    else if (!validarEmail(_emailPersonal.trim())) {
        _mensaje = _mensaje + '<li>La dirección del correo electrónico o email incorecto</li>';
        $('#EmailPersonal').addClass("border-danger");
    }

    if (_genero.trim() == '0') {
        _mensaje = _mensaje + '<li>Genero</li>';
        $('#Genero').addClass("border-danger");
    }

    if (_colorOjos.trim() == '0') {
        _mensaje = _mensaje + '<li>Color de ojos</li>';
        $('#ColorOjos').addClass("border-danger");
    }

    if (_colorCaballo.trim() == '0') {
        _mensaje = _mensaje + '<li>Color de cabello</li>';
        $('#ColorCaballo').addClass("border-danger");

    }

    if (_tipoSangre.trim() == '0') {
        _mensaje = _mensaje + '<li>Tipo sangre</li>';
        $('#TipoSangre').addClass("border-danger");

    }

    if (_estadoCivil.trim() == '0') {
        _mensaje = _mensaje + '<li>Estado civil</li>';
        $('#EstadoCivil').addClass("border-danger");
    }

    if (_peso.trim().length == 0) {
        _mensaje = _mensaje + '<li>Estado civil</li>';
        $('#Peso').addClass("border-danger");
    }

    if (_direccionDomicillo.trim().length == 0) {
        _mensaje = _mensaje + '<li>Dirección del dominicillo</li>';
        $('#DireccionDomicillo').addClass("border-danger");
    }

    if (_codigoPais.trim() == '0') {
        _mensaje = _mensaje + '<li>País</li>';
        $('#CodigoPais').addClass("border-danger");
    }

    if (_codigoProviencia.trim() == '0') {
        _mensaje = _mensaje + '<li>Provincia</li>';
        $('#CodigoProviencia').addClass("border-danger");
    }

    if (_codigoCiudad.trim() == '0') {
        _mensaje = _mensaje + '<li>Ciudad</li>';
        $('#CodigoCiudad').addClass("border-danger");
    }

    if (_codigoCanton.trim() == '0') {
        _mensaje = _mensaje + '<li>Cantón</li>';
        $('#CodigoCanton').addClass("border-danger");
    }

    if (_codigoParroquia.trim() == '0') {
        _mensaje = _mensaje + '<li>Parroquia</li>';
        $('#CodigoParroquia').addClass("border-danger");
    }

    if (_sectorDondeVive.trim() == '0') {
        _mensaje = _mensaje + '<li>Sector donde vive</li>';
        $('#SectorDondeVive').addClass("border-danger");
    }

    if (_regimenLaboral.trim() == '0') {
        _mensaje = _mensaje + '<li>Regimen laboral</li>';
        $('#RegimenLaboral').addClass("border-danger");
    }

    if (_tipoHorario.trim() == '0') {
        _mensaje = _mensaje + '<li>Tipo de horario</li>';
        $('#TipoHorario').addClass("border-danger");
    }

    if (_decimoTerceroAculado.trim() == '0') {
        _mensaje = _mensaje + '<li>Acumula decimo tercer sueldo</li>';
        $('#DecimoTerceroAculado').addClass("border-danger");
    }

    if (_decimoCuartoAculado.trim() == '0') {
        _mensaje = _mensaje + '<li>Acumula decimo cuarto sueldo</li>';
        $('#DecimoCuartoAculado').addClass("border-danger");
    }

    if (_aporteFondoReserva.trim() == '0') {
        _mensaje = _mensaje + '<li>Aporte fondo de reserva</li>';
        $('#AporteFondoReserva').addClass("border-danger");
    }

    if (_mensaje.trim().length > 0) {
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Campos obligatorios de los datos del empleado</p>",
            html: "<ul class='text-danger text-left'>" + _mensaje + "</ul>",
            confirmButtonText: 'Aceptar'
        });
        return false;
    }

    return true;
}

function cambiaColorDatosAdicionalesEmpleaado() {
    $('#DocumentoIdentificacion').removeClass("border-danger");
    $('#NombreContactoEmergencia').removeClass("border-danger");
    $('#TelefonoContactoEmergencia').removeClass("border-danger");
    $('#AlergiaMedicina').removeClass("border-danger");
    $('#AlergiaAlimentos').removeClass("border-danger");
    $('#AlergiaMedioAmbiente').removeClass("border-danger");
    $('#Discapacidad').removeClass("border-danger");
    $('#DiscapacidadNombre').removeClass("border-danger");
    $('#Porcentaje').removeClass("border-danger");
    $('#EnfermedadCatrastrofica').removeClass("border-danger");
    $('#EnfermedadCatrastroficaNombre').removeClass("border-danger");
    $('#Sustituto').removeClass("border-danger");
    $('#NombreFamiliarSustituto').removeClass("border-danger");
    $('#ParentescoSubtituto').removeClass("border-danger");
    $('#SenescytNumeroRegistro').removeClass("border-danger");
    $('#UltimoTituloObtenido').removeClass("border-danger");
}
function ValidaCamposAdicionalesEmpleado() {
    cambiaColorDatosAdicionalesEmpleaado();
    var _mensaje = '';
    var _DocumentoIdentificacion = $("#DocumentoIdentificacion").val();
    var _NombreContactoEmergencia = $("#NombreContactoEmergencia").val();
    var _TelefonoContactoEmergencia = $("#TelefonoContactoEmergencia").val();
    var _AlergiaMedicina = $("#AlergiaMedicina").val();
    var _AlergiaAlimentos = $("#AlergiaAlimentos").val();
    var _AlergiaMedioAmbiente = $("#AlergiaMedioAmbiente").val();
    var _Discapacidad = $("#Discapacidad").val();
    var _DiscapacidadNombre = $("#DiscapacidadNombre").val();
    var _Porcentaje = $("#Porcentaje").val();
    var _EnfermedadCatrastrofica = $("#EnfermedadCatrastrofica").val();
    var _EnfermedadCatrastroficaNombre = $("#EnfermedadCatrastroficaNombre").val();
    var _Sustituto = $("#Sustituto").val();
    var _NombreFamiliarSustituto = $("#NombreFamiliarSustituto").val();
    var _ParentescoSubtituto = $("#ParentescoSubtituto").val();
    var _SenescytNumeroRegistro = $("#SenescytNumeroRegistro").val();
    var _UltimoTituloObtenido = $("#UltimoTituloObtenido").val();

    if (_DocumentoIdentificacion.trim().length == 0) {
        _mensaje = _mensaje + '<li>Número de cédula</li>';
    }

    if (_NombreContactoEmergencia.trim().length == 0) {
        _mensaje = _mensaje + '<li>El contacto de emergencia</li>';
        $('#NombreContactoEmergencia').addClass("border-danger");
    }
    if (_TelefonoContactoEmergencia.trim().length == 0) {
        _mensaje = _mensaje + '<li>Número de teléfono contacto de emergencia</li>';
        $('#TelefonoContactoEmergencia').addClass("border-danger");
    }
    if (_AlergiaMedicina.trim().length == 0) {
        _mensaje = _mensaje + '<li>Alergia a la medicina</li>';
        $('#AlergiaMedicina').addClass("border-danger");
    }
    if (_AlergiaAlimentos.trim().length == 0) {
        _mensaje = _mensaje + '<li>Alergia a los alimientos</li>';
        $('#AlergiaAlimentos').addClass("border-danger");
    }

    if (_AlergiaMedioAmbiente.trim().length == 0) {
        _mensaje = _mensaje + '<li>Alergia  al medio ambiente</li>';
        $('#AlergiaMedioAmbiente').addClass("border-danger");
    }

    if (_Discapacidad.trim().length == 0) {
        _mensaje = _mensaje + '<li>Selecionar si es discapacitado</li>';
        $('#Discapacidad').addClass("border-danger");
    }

    if (_DiscapacidadNombre.trim().length == 0) {
        _mensaje = _mensaje + '<li>Descripción de la discapacidad</li>';
        $('#DiscapacidadNombre').addClass("border-danger");
    }
    if (_Porcentaje.trim().length == 0) {
        _mensaje = _mensaje + '<li>Porcentaje de discapacidad</li>';
        $('#Porcentaje').addClass("border-danger");
    }

    if (_EnfermedadCatrastrofica.trim() == '0') {
        _mensaje = _mensaje + '<li>seleccionar la enfermedad catastrófica</li>';
        $('#EnfermedadCatrastrofica').addClass("border-danger");
    }
    if (_EnfermedadCatrastroficaNombre.trim().length == 0) {
        _mensaje = _mensaje + '<li>Descripción de la enfermedad catastrófica</li>';
        $('#EnfermedadCatrastroficaNombre').addClass("border-danger");
    }

    if (_Sustituto.trim() == '0') {
        _mensaje = _mensaje + '<li>Selecionar el sustituto</li>';
        $('#Sustituto').addClass("border-danger");
    }

    if (_NombreFamiliarSustituto.trim().length == 0) {
        _mensaje = _mensaje + '<li>Nombre del sustituto</li>';
        $('#NombreFamiliarSustituto').addClass("border-danger");
    }
    if (_ParentescoSubtituto.trim().length == 0) {
        _mensaje = _mensaje + '<li>Parentesco</li>';
        $('#ParentescoSubtituto').addClass("border-danger");
    }

    if (_SenescytNumeroRegistro.trim().length == 0) {
        _mensaje = _mensaje + '<li>Registro senescyt</li>';
        $('#SenescytNumeroRegistro').addClass("border-danger");
    }

    if (_UltimoTituloObtenido.trim().length == 0) {
        _mensaje = _mensaje + '<li>Descripción del título</li>';
        $('#UltimoTituloObtenido').addClass("border-danger");
    }

    if (_mensaje.trim().length > 0) {
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Campos obligatorios</p>",
            html: "<ul class='text-danger text-left'>" + _mensaje + "</ul>",
            confirmButtonText: 'Aceptar'
        });
        return false;
    }
    return true;
}

function editarCursoEmpleado(json) {
    if (json != null) {
        $.ajax({
            url: $.MisUrls.url._CursoEmpleadoPorDocumentoCodigo,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            data: { idDoc: json.DocumentoIdentificacion, codigoCurso: json.CodigoCursoEmpleado },
            success: function (e) {
                if (e != null) {
                    $('#mdocumentoIdentidad').val(e.DocumentoIdentificacion);
                    $('#mnombreEmpleado').val($('#NombreCompleto').val());
                    $('#mcodigoCurso').val(e.CodigoCursoEmpleado);
                    $('#titulosSelect').val(e.CodigoTitulo);
                    $('#mdescripcionTitulo').val(e.DescripcionCurso);
                    $('#mcodigoDocumento').val(e.NumeroIdentidicacion);
                    $('#mfechaCurso').val(e.FechaCurso);
                    $('#mduracion').val(e.DuracionCurso);
                    $('#tiempoSelect').val(e.TiempoCurso);
                    $('#aprobacionSelect').val(e.AprobacionCurso);
                    $('#asistenciaSelect').val(e.AsistenciaCurso);
                    $('#ciudadSelect').val(e.CodigoCiudad);
                    $('#entidadEducativaSelect').val(e.CodigoEntidadEducativa);
                    $('#mdocumentoCurso').val(e.PathDocumentoCurso);
                    $('#mestadoCurso').val(e.EstadoCurso);
                    $('#cursoModal').modal('show');
                }
            },
            error: function (errormessage) {
                Swal.fire("Mensaje", "Financiero: " + errormessage, "warning");
            }
        });

    }

}

function cambiaColorCurso() {
    $('#titulosSelect').removeClass("border-danger");
    $('#mdescripcionTitulo').removeClass("border-danger");
    $('#mfechaCurso').removeClass("border-danger");
    $('#mduracion').removeClass("border-danger");
    $('#tiempoSelect').removeClass("border-danger");
    $('#aprobacionSelect').removeClass("border-danger");
    $('#asistenciaSelect').removeClass("border-danger");
    $('#ciudadSelect').removeClass("border-danger");
    $('#entidadEducativaSelect').removeClass("border-danger");
    $('#adjuntoFile').removeClass("border-danger");

}

function validaCamposObligatorioCurso() {
    cambiaColorCurso();
    var _mensaje = '';
    var _titulo = $('#titulosSelect').val();
    var _descripcionTitulo = $('#mdescripcionTitulo').val();
    var _fechaCurso = $('#mfechaCurso').val();
    var _duracion = $('#mduracion').val();
    var _tiempoSelect = $('#tiempoSelect').val();
    var _aprobacionSelect = $('#aprobacionSelect').val();
    var _asistenciaSelect = $('#asistenciaSelect').val();
    var _ciudadSelect = $('#ciudadSelect').val();
    var _entidadEducativaSelect = $('#entidadEducativaSelect').val();
    var _documentoCurso = $('#mdocumentoCurso').val();
    var _adjuntoFile = document.getElementById("adjuntoFile").files[0];

    if (_titulo == '0') {
        $('#titulosSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar el curso</li>';
    }

    if (_descripcionTitulo.trim().length == 0) {
        $('#mdescripcionTitulo').addClass("border-danger");
        _mensaje = _mensaje + '<li>Descripción del curso</li>';
    }
    if (_fechaCurso.trim().length == 0) {
        $('#mfechaCurso').addClass("border-danger");
        _mensaje = _mensaje + '<li>Fecha del curso</li>';
    }
    if (_duracion.trim().length == 0) {
        $('#mduracion').addClass("border-danger");
        _mensaje = _mensaje + '<li>Duración del curso</li>';
    }

    if (_tiempoSelect == '0') {
        $('#titulosSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar el tiempo del curso</li>';
    }

    if (_aprobacionSelect == '0') {
        $('#aprobacionSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar la aprobación del curso</li>';
    }

    if (_asistenciaSelect == '0') {
        $('#asistenciaSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar la asistencia del curso</li>';
    }

    if (_ciudadSelect == '0') {
        $('#ciudadSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar la ciudad</li>';
    }

    if (_entidadEducativaSelect == '0') {
        $('#entidadEducativaSelect').addClass("border-danger");
        _mensaje = _mensaje + '<li>Debe seleccionar la entidad educativa</li>';
    }

    if (_documentoCurso.trim().length == '') {
        if (_adjuntoFile == null) {
            $('#adjuntoFile').addClass("border-danger");
            _mensaje = _mensaje + '<li>Debe adjuntar el curso en formato pdf</li>';
        }
    }

    if (_mensaje.trim().length > 0) {
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Campos obligatorios del curso</p>",
            html: "<ul class='text-danger text-left'>" + _mensaje + "</ul>",
            confirmButtonText: 'Aceptar'
        });
        return false;
    }
    return true;
}

function verDocumento(json) {
    var texto = $.MisUrls.url._DocumentoCurso + "?fileName=" + json.PathDocumentoCurso + "&id=" + json.DocumentoIdentificacion;
    $("#iframeDocumento").attr("src", texto);
    $('#archivoModal').modal('show');
   

}

function cargaCombos() {
    //InicioTitulo
    $.ajax({
        url: $.MisUrls.url._GetSelectListTitulosCurso,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var selectTitulos = $("#titulosSelect");
            selectTitulos.empty();
            $.each(result, function (index, itemData) {
                selectTitulos.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //FinTitulo
    //InicioEntidadEducativa
    $.ajax({
        url: $.MisUrls.url._GetSelectListEntinidadEducativa,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var selecteEntidad = $("#entidadEducativaSelect");
            selecteEntidad.empty();
            $.each(result, function (index, itemData) {
                selecteEntidad.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //FinEntidadEducativa

    //Inicio Lista valores Aprobacion
    $.ajax({
        url: $.MisUrls.url._GetSelectListaValores,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { campo: "CURAPR" },
        success: function (result) {
            var selecteAprobacion = $("#aprobacionSelect");
            selecteAprobacion.empty();
            $.each(result, function (index, itemData) {
                selecteAprobacion.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //Fin valores Aprobacion
    //Inicio Lista valores Asistencia
    $.ajax({
        url: $.MisUrls.url._GetSelectListaValores,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { campo: "CURASI" },
        success: function (result) {
            var selecteAsistencia = $("#asistenciaSelect");
            selecteAsistencia.empty();
            $.each(result, function (index, itemData) {
                selecteAsistencia.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //Fin valores Asistencia

    //Inicio Lista valores tiempo
    $.ajax({
        url: $.MisUrls.url._GetSelectListaValores,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { campo: "CURTIE" },
        success: function (result) {
            var selecteTiempo = $("#tiempoSelect");
            selecteTiempo.empty();
            $.each(result, function (index, itemData) {
                selecteTiempo.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //Fin valores tiempo

    //InicioEntidadEducativa
    $.ajax({
        url: $.MisUrls.url._GetSelectListCiudad,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var selecteCiudad = $("#ciudadSelect");
            selecteCiudad.empty();
            $.each(result, function (index, itemData) {
                selecteCiudad.append($('<option/>', {
                    value: itemData.Value,
                    text: itemData.Text
                }));
            });
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", errormessage, "warning");
        }
    });
    //FinEntidadEducativa
}

function upperCaseF(a) {
    setTimeout(function () {
        a.value = a.value.toUpperCase();
    }, 1);
}

//Funcion solo numeros
function valideKey(evt) {

    // code is the decimal ASCII representation of the pressed key.
    var code = (evt.which) ? evt.which : evt.keyCode;

    if (code == 8) { // backspace.
        return true;
    } else if (code >= 48 && code <= 57) { // is a number.
        return true;
    } else { // other keys.
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

function limpiaCamposCurso() {
    let formulario = document.getElementById('formCurso');
    formulario.reset();
}