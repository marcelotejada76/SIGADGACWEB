var tabladata;
$(document).ready(function () {
    
    $("#btngrabarmatricula").click(function (e) {
        e.preventDefault();
        if (validaCampos()) {
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


//function validaCamposDatosEmpleado() {
//    cambiaColorDatosEmpleaado();
//    var _mensaje = '';
//    var _documentoIdentidad = $('#DocumentoIdentificacion').val();
//    var _nombreCompleto = $('#NombreCompleto').val();
//    var _fechaNacimiento = $('#FechaNacimiento').val();
//    var _numeroTelefonoDomicillo = $('#NumeroTelefonoDomicillo').val();
//    var _numeroTelefonoCelular = $('#NumeroTelefonoCelular').val();
//    var _emailPersonal = $('#EmailPersonal').val();
//    var _genero = $('#Genero').val();
//    var _colorOjos = $('#ColorOjos').val();
//    var _colorCaballo = $('#ColorCaballo').val();
//    var _tipoSangre = $('#TipoSangre').val();
//    var _estadoCivil = $('#EstadoCivil').val();
//    var _peso = $('#Peso').val();
//    var _direccionDomicillo = $('#DireccionDomicillo').val();
//    var _codigoPais = $('#CodigoPais').val();
//    var _codigoProviencia = $('#CodigoProviencia').val();
//    var _codigoCiudad = $('#CodigoCiudad').val();
//    var _codigoCanton = $('#CodigoCanton').val();
//    var _codigoParroquia = $('#CodigoParroquia').val();
//    var _sectorDondeVive = $('#SectorDondeVive').val();
//    var _regimenLaboral = $('#RegimenLaboral').val();
//    var _tipoHorario = $('#TipoHorario').val();
//    var _decimoTerceroAculado = $('#DecimoTerceroAculado').val();
//    var _decimoCuartoAculado = $('#DecimoCuartoAculado').val();
//    var _aporteFondoReserva = $('#AporteFondoReserva').val();

//    if (_documentoIdentidad.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Documento de identificacion</li>';
//        $('#DocumentoIdentificacion').addClass("border-danger");
//    }
//    if (_nombreCompleto.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Nombres y apellidos</li>';
//        $('#NombreCompleto').addClass("border-danger");
//    }

//    if (_fechaNacimiento.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Fecha nacimiento</li>';
//        $('#FechaNacimiento').addClass("border-danger");
//    }

//    if (_numeroTelefonoDomicillo.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Número de teléfono</li>';
//        $('#NumeroTelefonoDomicillo').addClass("border-danger");
//    }

//    if (_emailPersonal.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Dirección del correo electrónico o email</li>';
//        $('#EmailPersonal').addClass("border-danger");
//    }
//    else if (!validarEmail(_emailPersonal.trim())) {
//        _mensaje = _mensaje + '<li>La dirección del correo electrónico o email incorecto</li>';
//        $('#EmailPersonal').addClass("border-danger");
//    }

//    if (_genero.trim() == '0') {
//        _mensaje = _mensaje + '<li>Genero</li>';
//        $('#Genero').addClass("border-danger");
//    }

//    if (_colorOjos.trim() == '0') {
//        _mensaje = _mensaje + '<li>Color de ojos</li>';
//        $('#ColorOjos').addClass("border-danger");
//    }

//    if (_colorCaballo.trim() == '0') {
//        _mensaje = _mensaje + '<li>Color de cabello</li>';
//        $('#ColorCaballo').addClass("border-danger");

//    }

//    if (_tipoSangre.trim() == '0') {
//        _mensaje = _mensaje + '<li>Tipo sangre</li>';
//        $('#TipoSangre').addClass("border-danger");

//    }

//    if (_estadoCivil.trim() == '0') {
//        _mensaje = _mensaje + '<li>Estado civil</li>';
//        $('#EstadoCivil').addClass("border-danger");
//    }

//    if (_peso.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Estado civil</li>';
//        $('#Peso').addClass("border-danger");
//    }

//    if (_direccionDomicillo.trim().length == 0) {
//        _mensaje = _mensaje + '<li>Dirección del dominicillo</li>';
//        $('#DireccionDomicillo').addClass("border-danger");
//    }

//    if (_codigoPais.trim() == '0') {
//        _mensaje = _mensaje + '<li>País</li>';
//        $('#CodigoPais').addClass("border-danger");
//    }

//    if (_codigoProviencia.trim() == '0') {
//        _mensaje = _mensaje + '<li>Provincia</li>';
//        $('#CodigoProviencia').addClass("border-danger");
//    }

//    if (_codigoCiudad.trim() == '0') {
//        _mensaje = _mensaje + '<li>Ciudad</li>';
//        $('#CodigoCiudad').addClass("border-danger");
//    }

//    if (_codigoCanton.trim() == '0') {
//        _mensaje = _mensaje + '<li>Cantón</li>';
//        $('#CodigoCanton').addClass("border-danger");
//    }

//    if (_codigoParroquia.trim() == '0') {
//        _mensaje = _mensaje + '<li>Parroquia</li>';
//        $('#CodigoParroquia').addClass("border-danger");
//    }

//    if (_sectorDondeVive.trim() == '0') {
//        _mensaje = _mensaje + '<li>Sector donde vive</li>';
//        $('#SectorDondeVive').addClass("border-danger");
//    }

//    if (_regimenLaboral.trim() == '0') {
//        _mensaje = _mensaje + '<li>Regimen laboral</li>';
//        $('#RegimenLaboral').addClass("border-danger");
//    }

//    if (_tipoHorario.trim() == '0') {
//        _mensaje = _mensaje + '<li>Tipo de horario</li>';
//        $('#TipoHorario').addClass("border-danger");
//    }

//    if (_decimoTerceroAculado.trim() == '0') {
//        _mensaje = _mensaje + '<li>Acumula decimo tercer sueldo</li>';
//        $('#DecimoTerceroAculado').addClass("border-danger");
//    }

//    if (_decimoCuartoAculado.trim() == '0') {
//        _mensaje = _mensaje + '<li>Acumula decimo cuarto sueldo</li>';
//        $('#DecimoCuartoAculado').addClass("border-danger");
//    }

//    if (_aporteFondoReserva.trim() == '0') {
//        _mensaje = _mensaje + '<li>Aporte fondo de reserva</li>';
//        $('#AporteFondoReserva').addClass("border-danger");
//    }

//    if (_mensaje.trim().length > 0) {
//        Swal.fire({
//            icon: 'warning',
//            title: "<p style='width: 100 %; font-size: 14px;'>Campos obligatorios de los datos del empleado</p>",
//            html: "<ul class='text-danger text-left'>" + _mensaje + "</ul>",
//            confirmButtonText: 'Aceptar'
//        });
//        return false;
//    }

//    return true;
//}




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
