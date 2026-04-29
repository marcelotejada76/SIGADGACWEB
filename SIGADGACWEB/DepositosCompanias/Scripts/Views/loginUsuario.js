$(document).ready(function () {

    

    $('#rCodigoVerificacion').on('change', function () {
        var ocodVerificacion = $("#rCodigoVerificacion");
        $('#btnResetearUsuario').css('display', 'none');
        if (ocodVerificacion.val() > 0) {
           var ocodVerif =  $("#codigoAleario").val();
            if (ocodVerif != ocodVerificacion.val()) {
                Swal.fire("Mensaje", "El código de verificación ingresado no es correcta, intentalo nuevo", "warning");
            }
            else {
                $('#rCodigoVerificacion').css('display', 'none');
                $('#fcodigoVerifivacion').css('display', 'none');
                $('#btnResetearUsuario').css('display', 'block');
                $('#lblpassword').css('display', 'block');
                $('#lblrpassword').css('display', 'block');
            }
        }

    });



    $('#loginForm').submit(function (e) {
        let warnings = "";

        var ousername = $("#Username").val().trim();
        if (ousername === '') {
            warnings += "* La dirección del coreo electrónico, es obligatorio ingresar.";
            Swal.fire("Mensaje", warnings, "warning");
            return false;
        }
        else if (!validarEmail(ousername)) {
            warnings += "*La dirección de correo electrónco es incorrecta.";
            Swal.fire("Mensaje", warnings, "warning");
            return false;
        }

        var ocontrasena = $("#Contrasena").val().trim();
        if (ocontrasena === '') {
            warnings += "* La contraseña, es obligatorio ingresar.";
            Swal.fire("Mensaje", warnings, "warning");
            return false;
        }
        if ($("#Check1").prop("checked") === false) {
            Swal.fire("Mensaje", "* Debe aceptar los términos / Agree to the terms", "warning");
            return false;
        }

        $('#modalload').modal('show');
    });

    $('.agregar').click(function () {
        $('#modalload').modal('show');
    });

});


function validarEmail(valor) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,10})+$/.test(valor)) {
        return true;
    } else {
        return false;
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

function modalTerminos() {
    $('#modalTexto').modal('show');
}

function modalNuevoUsuario() {

    limpiarCampos();
    $('#modalNewUsuario').modal('show');
}

function limpiarCampos() {
    $("#NombreUsuario").val("");
    $("#ApellidoUsuario").val("");
    $("#Correo").val("");
    $("#Clave").val("");
    $("#Clave1").val("");
    $("#NumeroRuc").val("");
}
function saveUser() {
    var onombresUsuario = $("#NombreUsuario").val().trim(); 
    var oapellidosUsuario = $("#ApellidoUsuario").val().trim();
    var oruc = $("#NumeroRuc").val().trim();
    var ocorreoUsuario = $("#Correo").val().trim();
    var oclaveUsuario = $("#Clave").val().trim();
    var oclave1Usuario = $("#Clave1").val().trim();    
   

   
    if (onombresUsuario === '') {     
        Swal.fire("Mensaje", "* Los Nombres del usuario, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (onombresUsuario.length < 4) {      
        Swal.fire("Mensaje", "* Los Nombres debe contener mas de 4 caracteres, Inténtalo de nuevo.", "warning");
        return false;
    }
    
    if (oapellidosUsuario === '') {      
        Swal.fire("Mensaje", "* Los Apellidos del usuario, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (oapellidosUsuario.length < 4) {     
        Swal.fire("Mensaje", "* Los Apellidos debe contener mas de 4 caracteres, Inténtalo de nuevo.", "warning");
        return false;
    }
    
    if (ocorreoUsuario === '') {       
        Swal.fire("Mensaje", "* La dirección del coreo electrónico, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (!validarEmail(ocorreoUsuario.trim())) {       
        Swal.fire("Mensaje", "*La dirección de correo electrónco es incorrecta.", "warning");
        return false;
    }

    if (oruc === '') {
        Swal.fire("Mensaje", "* El Ruc de la Empresa, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (oruc.length < 10) {
        Swal.fire("Mensaje", "* El Ruc de la Empresa debe ser de 13 caracteres, Inténtalo de nuevo.", "warning");
        return false;
    }

    if (oclaveUsuario === '') {        
        Swal.fire("Mensaje", "* La contraseña, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (oclaveUsuario.length < 8) {       
        Swal.fire("Mensaje", "* La contraseña no es segura debe tener más de 8 caracteres, Inténtalo de nuevo.", "warning");
        return false;
    }
   
    if (oclave1Usuario === '') {     
        Swal.fire("Mensaje", "* La reconfirmación de la contraseña, es obligatorio ingresar.", "warning");
        return false;
    }
    else if (oclaveUsuario != '' && oclave1Usuario != '') {
        if (oclaveUsuario != oclave1Usuario) {
            Swal.fire("Mensaje", "* Las contraseñas no coinciden. Inténtalo de nuevo.", "warning");
            return false;
        }
    }

    //Objeto de Usuario  
    var request = {
        model: {
            CodigoUsuario: "",
            NombreUsuario: onombresUsuario,
            ApellidoUsuario: oapellidosUsuario,
            Correo: ocorreoUsuario,
            Clave: oclaveUsuario,
            NumeroRuc: oruc
        }
    };

    $.ajax({
        url: $.MisUrls.url._FormularioRegistrarUsuario,
        type: "POST",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            if (data.success) {
                $('#loadImagenAeronaveGrabar').css('display', 'none');
                $('#modalNewUsuario').modal('hide');               
                Swal.fire("Mensaje", data.responseText, "warning");
            } else {
                $('#loadImagenAeronaveGrabar').css('display', 'none');
                Swal.fire("Mensaje", data.responseText, "warning");
            }
        },
        error: function (errormessage) {
            $('#loadImagenAeronaveGrabar').css('display', 'none');
            Swal.fire("Mensaje", "Usuario:" + errormessage, "error");
        }
    });

}


//LLama al Modal de Retro nuevo de Aeronave
function ReseteraClave() {

    $("#remail").val("");
    $("#rpasswordNueva").val("");
    $("#rpasswordConfirmar").val("");
    $("#rCodigoVerificacion").val("");    
    $('#lblEtiqueta').css('display', 'none');   

    $('#modalResetearClave').modal('show');
}

//LLama al metodo actualizar clave
function ReseteraUsuario() {
    var ocorreo = $("#EmailUsuario");
    var oclaveT = $("#codigoAleario").val();
    var otemporal = $("#rpasswordAnterior").val();
    var orpassword = $("#rpasswordNueva").val();
    var orpasswordr = $("#rpasswordConfirmar").val();

    if (ocorreo.val() === '') {
        Swal.fire("Información", "* La dirección Correo electrónico no existe", "warning");
        return false;
    }

    if (!validarEmail(ocorreo.val())) {
        Swal.fire("Información", "*La dirección de correo electrónco o Email es incorrecta.", "warning");
        return false;
    }

    if (otemporal === '') {
        Swal.fire("Información", "* La clave temporal es obligatorio ingrsar", "warning");
        return false;
    }
    else {
        if (otemporal != oclaveT) {
            Swal.fire("Información", "* La contraseña temporal no es valida, verifique en correo electrónico registrado.", "warning");
            return false;
        }
    }
    

    if (orpassword === '') {
        Swal.fire("Información", "* El contraseña es obligatorio ingresar", "warning");
        return false;
    }

    if (orpasswordr === '') {
        Swal.fire("Información", "* Reconfirmar contraseña es obligatorio ingresar", "warning");
        return false;
    }

    if (orpassword != orpasswordr) {
        Swal.fire("Información", "* Las contraseñas no coinciden. Inténtalo de nuevo.", "warning");
        return false;
    }
    $('#loadImagen').css('display', 'block');
    $.ajax({
        url: $.MisUrls.url._FormularioCambiaContrasenaUsuario,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: { ocorreo: ocorreo.val(), oclave: orpassword},
        success: function (e) {
            if (e.resultado) {                
                $('#modalClave').modal('hide');
                $("#EmailUsuario").value = "";
                $("#codigoAleario").value = "";
                $("#rpasswordAnterior").value = "";
                $("#remail").value = "";
                $('#rpasswordNueva').value = "";
                $('#rpasswordConfirmar').value = "";
               // window.location.href = $.MisUrls.url._FormularioLogin;
                $('#loadImagen').css('display', 'none');                
                $('#modalCambiarContrasena').modal('hide');
                Swal.fire("Información", "Proceso realizado exitosamente", "warning");
            }
            else {
                Swal.fire("Mensaje", "No se puedo actualizar la pertición, comuniquese con el departamento de Tecnología.", "warning");
                $('#loadImagen').css('display', 'none');
            }
        },
        error: function (errormessage) {
            Swal.fire("Mensaje", "No se puedo actualizar la pertición, comuniquese con el departamento de Tecnología. " + errormessage, "warning");
            $('#loadImagen').css('display', 'none');
        }
    });

}

function EnviarGenerarCodigoVerificacion() {
    var ocorreo = $("#remail").val();
    if (ocorreo != "") {
        $('#loadImagen').css('display', 'block');
        $.ajax({
            url: $.MisUrls.url._FormularioEnviaCorreoNumeroCodigoVerificador,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            data: { correo: ocorreo},
            success: function (response) {
                $('#loadImagen').css('display', 'none');
                if (response.success) {
                    Swal.fire("Información", "Estimado usuario su contraseña temporal ha sido enviado a la  dirección del correo electrónico registrado", "warning");
                    $('#codigoAleario').val(response.responseText);
                    $('#modalResetearClave').modal('hide');
                    $('#modalCambiarContrasena').modal('show');
                }
                else {
                    Swal.fire("Mensaje", response.responseText + ", comuniquese con el departamento de Tecnología.", "warning");
                }
            },
            error: function (errormessage) {
                Swal.fire("Mensaje", "No se puedo actualizar la pertición, comuniquese con el departamento de Tecnología. " + errormessage, "warning");
            }
        });
    }
    
}

function ValidaCorreoElectronico() {
        var oremail = $("#remail");
        if (oremail.val() != "") {
            $('#loadImagen').css('display', 'block');
            $.ajax({
                url: $.MisUrls.url._FormularioValidaExisteEmailCodigoVerificador,
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                data: { correo: oremail.val() },
                success: function (response) {
                    if (response.success) {
                        $.ajax({
                            url: $.MisUrls.url._FormularioEnviaCorreoNumeroCodigoVerificador,
                            type: "GET",
                            contentType: "application/json;charset=UTF-8",
                            dataType: "json",
                            data: { correo: oremail.val() },
                            success: function (response) {
                                $('#loadImagen').css('display', 'none');
                                if (response.success) {
                                    Swal.fire("Información", "Estimado usuario su contraseña temporal ha sido enviado a la  dirección del correo electrónico registrado <b style='color:#0026ff'> / Dear user, your temporary password has been sent to the registered email address</b> ", "warning");
                                    $('#codigoAleario').val(response.responseText);
                                    $('#EmailUsuario').val(oremail.val());
                                    $('#modalResetearClave').modal('hide');
                                    $('#modalCambiarContrasena').modal('show');
                                    $('#loadImagen').css('display', 'none');
                                }
                                else {
                                    Swal.fire("Mensaje", response.responseText + ", comuniquese con el departamento de Tecnología.", "warning");
                                    $('#loadImagen').css('display', 'none');
                                }
                            },
                            error: function (errormessage) {
                                Swal.fire("Mensaje", "No se puedo actualizar la pertición, comuniquese con el departamento de Tecnología. " + errormessage, "warning");
                                $('#loadImagen').css('display', 'none');
                            }
                        });        
                    }
                    else {                      
                        Swal.fire("Mensaje", "* La dirección del correo electrónico ingresado no existe!", "warning");
                        $('#loadImagen').css('display', 'none');
                    }
                   
                },
                error: function (errormessage) {
                    $('#loadImagen').css('display', 'none');
                    Swal.fire("Mensaje", "Error, No se puedo confirmar la dirección del correo electrónico, posible causa: " + errormessage, "warning");
                }
            });
        }


}