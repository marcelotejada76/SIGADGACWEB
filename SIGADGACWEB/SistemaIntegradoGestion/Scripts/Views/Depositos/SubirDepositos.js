$(document).ready(function () {
    loadDataTable();

    // Función para validar si todos los campos están llenos
    function validarFormulario() {
        var file = $("#documentFile").val();
        var comprobante = $("#comprobante").val() ? $("#comprobante").val().trim() : "";
        var fecha = $("#fechadeposito").val();
        var concepto = $("#concepto").val() ? $("#concepto").val().trim() : "";

        if (file !== "" && comprobante !== "" && fecha !== "" && concepto !== "") {
            $("#btnEnviar").prop("disabled", false);
        } else {
            $("#btnEnviar").prop("disabled", true);
        }
    }

    // Escuchar eventos para validar en tiempo real
    $("#comprobante, #fechadeposito, #concepto").on('input change', validarFormulario);

    $('#documentFile').on('change', function () {
        if ($(this).val() != '') {
            const fileSize = $(this)[0].files[0].size / 1024 / 1024; // in MiB 
            if (fileSize > 2) {
                Swal.fire({
                    icon: 'warning',
                    title: "¡Precaución!",
                    html: "El documento excede el tamaño máximo, se solicita un archivo no mayor a 2MB. Por favor verifica."
                });
                $(this).val('');
                $("#btnEnviar").prop("disabled", true);
            }
        }
        $("#labelFile").html($('#documentFile').val());

        var file = this.files[0];

        if (file) {
            // Mostrar nombre en el label
            $("#labelFile").text(file.name);

            // Habilitar campos
            $("#comprobante").prop("disabled", false);
            $("#fechadeposito").prop("disabled", false);
            $("#concepto").prop("disabled", false);
        } else {
            // Reset label
            $("#labelFile").text("No se ha seleccionado ningún archivo.");

            // Deshabilitar campos y limpiar valores
            $("#comprobante").prop("disabled", true).val('');
            $("#fechadeposito").prop("disabled", true).val('');
            $("#concepto").prop("disabled", true).val('');
        }
        
        validarFormulario();
    });

    document.getElementById('concepto').addEventListener('input', function () {
        if (this.value.length > 199) {
            alert("El concepto no puede exceder los 200 caracteres.");
            this.value = this.value.substring(0, 200); // Corta el texto si pegan algo largo
        }
    });

    $('#btnEnviar').click(function () {
        var Año = $("#Año").val();
        var Mes = $("#Mes").val();
        var Ruc = $("#UsuarioRuc").val();
        var comprobante = $("#comprobante").val().trim();
        var fecha = $("#fechadeposito").val();
        var concepto = $("#concepto").val().trim();

        if (comprobante === "" || fecha === "" || concepto === "") {
            alert("Todos los campos son obligatorios");
            return;
        }

        var formData = new FormData();
        formData.append("documentFile", $("#documentFile")[0].files[0]);
        formData.append("comprobante", comprobante);
        formData.append("fechadeposito", fecha);
        formData.append("concepto", concepto);

        $.ajax({
            url: '/TuControlador/Guardar',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (respuesta) {
                alert("Guardado correctamente");
            }
        });

        $("#registerForm").submit();
    });

});

function loadDataTable() {
    try {
        if ($.fn.DataTable) {
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
    } catch (e) {
        console.error("Error al cargar DataTable:", e);
    }
}

function abrirArchivo(fileName) {

    var nombreArchivo = JSON.parse(fileName);
    var opathArchivo = $('#Directory').val();

    var _extensionArchivo = "";

    if (nombreArchivo.trim().length > 0 && opathArchivo.trim().length > 0) {

        var _extensionArchivo = getExtensionArchivo(nombreArchivo);
        if (_extensionArchivo == "pdf" || _extensionArchivo == "PDF") {

            var texto = $.MisUrls.url._VisualizarDepositos + "?nombreArchivo=" + nombreArchivo + "&direccion=" + opathArchivo;
            setTimeout(function () {
                $("#iframeCetificado").attr("src", texto);
                $("#loadingBuscar").css("display", "none");
            }, 2000);
        }
        else {
            descargarArchivo(nombreArchivo, opathArchivo);
        }
    }

}

function getExtensionArchivo(filename) {
    return filename.slice((filename.lastIndexOf(".") - 1 >>> 0) + 2);
}

function descargarArchivo(onombreArchivo, odireccion) {
    try {
        if (onombreArchivo.length > 0 && odireccion.length > 0) {
            window.location = $.MisUrls.url._DownloadFileDeposito + "?nombreArchivo=" + onombreArchivo + "&direccion=" + odireccion;
            $("#loadingBuscar").css("display", "none");
        }
        else {
            mensajeGeneral("Descargar archivo", "El nombre del archivo en blanco.");
            $("#loadingBuscar").css("display", "none");
        }
    } catch (e) {
        mensajeGeneral("Descargar archivo", "Hay un problema al descargar el archivo.");
        $("#loadingBuscar").css("display", "none");
    }

}

function eliminar(fileName) {
    var nombreArchivo = JSON.parse(fileName);
    var opathArchivo = $('#Directory').val();


    if (nombreArchivo != null) {

        Swal.fire({
            title: '¿Eliminar?',
            text: "¿Está seguro de que eliminar el archivo del sitio?\n" + opathArchivo + "\\" + nombreArchivo,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#C5C7CF',
            confirmButtonText: 'Eliminar',
            cancelButtonText: "Cancelar",
            allowOutsideClick: false,
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: $.MisUrls.url._EliminaDepositoServidor,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    cache: false,
                    data: { nombreArchivo: nombreArchivo, direccion: opathArchivo },
                    success: function (result) {
                        if (result)
                            location.reload(true);
                        else
                            mensajeGeneral("¿Eliminar?", "No puede anular el archivo", "success");
                    },
                    error: function (jqXHR, textStatus, error) {
                        $('.model-status').text("Estado: Error inesperado", "error");
                    }
                });
            }
        })

    }
    else {
        mensajeGeneral("¿Eliminar?", "No puede eliminar el registro", "error");
    }
}

function mensajeGeneralIcono(titulo, contenido, icono) {
    Swal.fire({
        title: titulo,
        text: contenido,
        icon: icono,
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Aceptar',
        allowOutsideClick: false,
    });
}