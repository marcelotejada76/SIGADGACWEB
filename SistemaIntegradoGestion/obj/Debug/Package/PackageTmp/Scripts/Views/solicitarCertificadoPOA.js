
$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    $('#tbDetalle').DataTable({
        "processing": true,
        scrollY: '490px',
        scrollCollapse: true,
        paging: false,
        fixedHeader: true,
        "order": [[0, 'desc'], [1, 'desc'], [2, 'desc']],
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