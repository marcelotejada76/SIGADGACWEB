
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

function EnviarAprobar(canio, numSol) {
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
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", "Error, Exportar el reporte" + errormessage, "warning");
                }
            });
        }
    });

}