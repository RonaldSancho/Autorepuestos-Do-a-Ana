function CorreoExistente() {
    let pcorreo = $("#pCorreo").val();
    $("#RegistrarUsuario").prop("disabled", true);

    if (pcorreo.trim() != "") {
        $.ajax({
            url: '/Home/CorreoExistente',
            data: {
                "pcorreo": pcorreo
            },
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                if (res == "")
                    $("#RegistrarUsuario").prop("disabled", false);
                else
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Este correo ya se encuentra registrado. Por favor, intente nuevamente.',
                    });

            }
        });
    }
}




//function showAlert() {
//    Swal.fire({
//        position: 'top-end',
//        icon: 'success',
//        title: 'Your work has been saved',
//        showConfirmButton: false,
//        timer: 1500
//    });
//}

//function RegistrarUsuario() {
//    let pNombre = $("#pNombre").val();
//    let pApellido1 = $("#pApellido1").val();
//    let pCedula = $("#pCedula").val();
//    let pTelefono = $("#pTelefono").val();
//    let pCorreo = $("#pCorreo").val();
//    let pContrasena = $("#pContrasena").val();

//    $.ajax({
//        url: '/Home/RegistrarUsuario',
//        data: {
//            "pNombre": pNombre,
//            "pApellido1": pApellido1,
//            "pCedula": pCedula,
//            "pTelefono": pTelefono,
//            "pCorreo": pCorreo,
//            "pContrasena": pContrasena
//        },
//        type: 'POST',
//        dataType: 'json',
//        success: function (res) {
//            Swal.fire({
//                position: 'top-end',
//                icon: 'success',
//                title: 'Your work has been saved',
//                showConfirmButton: false,
//                timer: 1500
//            });
//        },
//        error: function (xhr, status, error) {

//            Swal.fire({
//                icon: 'error',
//                title: 'Error',
//                text: 'Ocurrió un error al registrar al cliente. Por favor, intente nuevamente más tarde.',
//            });
//        }
//    });
//}