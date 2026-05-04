// Habilitar edición


$("#btnEditar").click(function () {
    $(".form-control").not("#txtMatricula, #txtOid").removeAttr("readonly");
    $("#btnGuardar").prop("disabled", false);
});



// Guardar con AJAX
$("#btnGuardar").click(function () {
    $("#errorPeso").text("");

    // Obtener valor del peso
    var peso = parseFloat($("input[name='PESOMAXESTRUCTURAL']").val());
    
    if (isNaN(peso) || peso <= 0.1) {
        alert(peso);
        $("#errorPeso").text("⚠️ El peso debe ser mayor a 0.");
        return; // detener envío
    }

    var dataForm = $("#frmMatricula").serialize();

    $.ajax({
        url: '@Url.Action("ActualizarMatricula", "ActualizacionMatriculas")',
        type: "POST",
        data: dataForm,
        success: function (resp) {
            if (resp.Success) {
                mostrarMensaje("success", resp.Message);
            } else {
                mostrarMensaje("danger", resp.Message);
            }
        },
        error: function (xhr, status, error) {
            mostrarMensaje("danger", "❌ Ocurrió un error: " + error);
        }
    });
});

// Función para mostrar alertas
function mostrarMensaje(tipo, mensaje) {
    let alertHtml = `
            <div class="alert alert-${tipo} alert-dismissible fade show" role="alert">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Cerrar"></button>
            </div>`;
    $("#alertContainer").html(alertHtml);
}

//valida region
$(document).on("input", "#REGION", function () {
    let valor = this.value;

    // Solo números
    if (/[^0-1]/.test(valor)) {
        $("#errorRegion").show();       // muestra alerta
        this.value = valor.replace(/[^0-1]/g, '');
    }
    // Máximo un dígito
    else if (valor.length > 1) {
        $("#errorRegion").show();
        this.value = valor.substring(0, 1);
    }
    else {
        $("#errorRegion").hide();       // oculta alerta si es válido
    }
});

// Cuando vuelva a guardarse, re-bloquear todo
function bloquearCampos() {
    $(".form-control").attr("readonly", true).addClass("readonly-field");
}