
$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    $('#tbDetalle').DataTable({
        "processing": true,
        scrollY: '400px',
        scrollCollapse: true,
        paging: false,
        info: false,
        fixedHeader: true,
        "order": [[0, 'desc'], [1, 'desc']],
        "language": {
            "url": $.MisUrls.url._datatable_spanish
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
        Swal.fire({
            icon: 'warning',
            title: "<p style='width: 100 %; font-size: 14px;'>Datos adjuntos</p>",
            html: "<ul class='text-danger text-left'>No existe información de la documentación.</ul>",
            confirmButtonText: 'Aceptar'
        });
    }

}

function abrirArchivo(fileName) {
    var nombreArchivo = fileName;
    var opathArchivo = $('#pathArchivo').text();
    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {
        var texto = $.MisUrls.url._VisualizarDocumento + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
        $("#iframeCetificado").attr("src", texto);
    }
}

function Guardar() {
    var canio = $('#codanio').val();
    var numSol = $('#numSolicitud').val();
    Swal.fire({
        title: "Solicitud Certificación POA",
        text: "¿Está seguro de enviar aprobar la solicitud de certificación POA?",
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
                        window.location.reload();
                        //Swal.fire("Mensaje", "Se actualizo con exito", "warning");
                    }
                    

                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
                }
            });
        }
    });
}
