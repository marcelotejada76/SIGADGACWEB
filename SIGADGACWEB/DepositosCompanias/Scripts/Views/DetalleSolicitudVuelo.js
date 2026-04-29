$(document).ready(function () {
    $('#tbDetalle').DataTable({
        scrollY: '390px',
        scrollCollapse: true,
        paging: false,
        "order": [[0, 'desc']],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
    });
});


function consultar(oidSol) {
    if (oidSol != '') {
        $.post($.MisUrls.url._comprobarSessionPrivado, null, function (htmlSesion) {
            if (htmlSesion == true) {
                $('#modalload').css('display', 'none');
                $('#modalFormulario').modal('show');
                var urlFormularioEspecial = $.MisUrls.url._consultaFormularioPrivado + "?id=" + oidSol;
                $("#contenidoModal").load(urlFormularioEspecial);
                cargaInformacionTiempo();
            }
        });       
    }
}

function anularSolicitud(oidSol) {
    if (oidSol > 0) {       
        Swal.fire({
            title: 'Ruta de vuelo',
            text: "¿Desea anular la solicitud seleccionada: " + oidSol,
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
                            url: $.MisUrls.url._anulaSolicitudVueloPrivadoPorOid + "?oidSol=" + oidSol,
                            type: "GET",
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.respuesta) {
                                    window.location.reload(); 
                                    return true;
                                } else {
                                    MensajeIco("Mensaje", "No se pudo anular la solicitud: " + oidSol , "warning")
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
        });
    }
}

function imprimir(id) {
    cargaInformacionTiempo();
    var texto = $.MisUrls.url._FormularioImprimir + "?id=" + id;
    // Open the page in a new tab or window
    var w = window.open(texto);
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


function cargaInformacionTiempo() {
    let timerInterval;
    Swal.fire({
        title: "¡Alerta de cierre automático!",
        html: "Carga la información en <b></b> .",
        timer: 2000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading();
            const timer = Swal.getPopup().querySelector("b");
            timerInterval = setInterval(() => {
                timer.textContent = `${Swal.getTimerLeft()}`;
            }, 100);
        },
        willClose: () => {
            clearInterval(timerInterval);
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            console.log("Me cerró el temporizador.");
        }
    });
}

function validaSession() {
    jQuery.ajax({
        url: $.MisUrls.url._FormularioSession,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Value == false) {
                document.location.href = $.MisUrls.url._FormularioLogin;
            }
            else {
                return true;
            }
        },
        error: function (error) {
            Swal.fire("Mensaje", error, "warning");
        }
    });
}

