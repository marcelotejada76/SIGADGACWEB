var tabladata;
$(document).ready(function () {
    
    $("#guardar").click(function (e) {
       
            //debugger
        e.preventDefault();
        var _CEDULA = $("#CEDULA").val();
        var _NOMBRES = $("#NOMBRES").val();
        var _APELLIDOS = $("#APELLIDOS").val();
        var _EMAIL = $("#EMAIL").val();
          
            var jsonObject = {
                "CEDULA": _CEDULA, "NOMBRES": _NOMBRES, "APELLIDOS": _APELLIDOS, "EMAIL": _EMAIL
            };

            //InicioGrabar
            $.ajax({
                url: $.MisUrls.url._CreaCurriculum,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                data: JSON.stringify(jsonObject),
                success: function (result) {
                    if (result.Success) {
                        cargaDatosEmpleado();
                        Swal.fire("Curriculum", result.Message, "warning");
                    }
                },
                error: function (errormessage) {
                    Swal.fire("Mensaje", errormessage, "warning");
                }
            });
            //FinGrabar
       
    });

    function Create() {
        alert("ingresa");
       
        $('#largeModal').modal('show');
    }


   
});


