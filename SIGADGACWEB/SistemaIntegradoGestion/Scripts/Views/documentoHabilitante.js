var tabladata;
$(document).ready(function () {
    ///loadDataTable();
    $('#btnEnviar').click(function () {

        $("#registerForm").submit();
    });

    $('#documentFile').on('change', function () {
        $("#documentFile").html($('#FileCertificadoOperador').val());    
    });

});

function loadDataTable() {
    $('#tbExploradorArchivos').DataTable({
        scrollY: '380px',
        scrollCollapse: true,
        paging: false,
        order: [[0, 'desc']],
        language: {
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
        }
    });
}

//Funcion elimina el archivo 
function eliminar($nombreArchivo) {

    if ($nombreArchivo != null) {
        let directory = $("#Directory").val();
        Swal.fire({
            title: '¿Eliminar?',
            text: "¿Está seguro de que eliminar el archivo del sitio?\n" + $nombreArchivo + directory,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#C5C7CF',
            confirmButtonText: 'Eliminar',
            cancelButtonText: "Cancelar",
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: $.MisUrls.url._EliminaArchivo,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",                    
                    cache: false,
                    data: { nombreArchivo: $nombreArchivo, direccion: directory},
                    success: function (result) {                       
                        if (result)
                            location.reload(true);
                        else
                            Swal.fire("¿Eliminar?", "No puede anular el archivo", "warning");
                    },
                    error: function (jqXHR, textStatus, error) {
                        $('.model-status').text("Estado: Error inesperado");
                    }
                });
            }
        })

    }
    else {
        Swal.fire("¿Eliminar?", "No puede anular la Solitud de Vuelo", "warning");
    }

}

