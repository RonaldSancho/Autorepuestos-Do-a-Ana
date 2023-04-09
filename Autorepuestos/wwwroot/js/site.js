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

function CorreoExistente2() {
    let pCorreo = $("#pCorreo").val();
    $("#RecuperarContrasena").prop("disabled", true);

    if (pCorreo.trim() != "") {
        $.ajax({
            url: '/Home/CorreoExistente',
            data: {
                "pCorreo": pCorreo
            },
            type: 'GET',
            dataType: 'json',
            success: function (res) {

                if (res == "")
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Por favor, intente nuevamente.',
                    });
                else
                    $("#RecuperarContrasena").prop("disabled", false);
            }
        });
    }
}

function Recuperar() {
    let pcorreo = $("#pCorreo").val();

    $.ajax({
        url: '/Home/Recuperar',
        data: {
            "pcorreo": pcorreo
        },
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            console.log(res); // Agregar esta línea
            Swal.fire({
                icon: 'success',
                title: 'Hola',
                text: 'Se ha enviado un correo a su bendeja con su contraseña.',
            });
        }
    });
}

function MensajeCredenciales() {
    Swal.fire({
        title: 'Error de inicio de sesión',
        text: 'Credenciales incorrectas',
        icon: 'error',
        confirmButtonText: 'OK'
    });
}

function MensajeUsuarioCreado() {
    Swal.fire({
        title: 'Cuenta creada exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajeProductoAgregado() {
    Swal.fire({
        title: 'Producto agregado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function confirmarEliminacion(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará el producto del carrito de compras.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Carrito/EliminarCarrito?id=' + id;
        }
    });
}


//@section Scripts {
//    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
//    <script>
//        $(function () {
//            $('form').submit(function (event) {
//                event.preventDefault();
//                Swal.fire({
//                    title: '¿Estás seguro?',
//                    text: '¿Quieres agregar este producto al carrito?',
//                    icon: 'question',
//                    showCancelButton: true,
//                    confirmButtonColor: '#3085d6',
//                    cancelButtonColor: '#d33',
//                    confirmButtonText: 'Agregar al carrito'
//                }).then((result) => {
//                    if (result.isConfirmed) {
//                        $(this).unbind('submit').submit();
//                    }
//                });
//            });
//        });
//    </script>
//}

//$('.carousel').carousel({
//    interval: 2000 // Cambiar el número de milisegundos para ajustar el tiempo de espera entre cada imagen
//})


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