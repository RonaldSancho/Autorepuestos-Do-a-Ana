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

function MensajeconfirmarEliminacionCarrito(id) {
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

function MensajeModificacionProducto() {
    Swal.fire({
        title: 'Producto editado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajeconfirmarEliminacionAdmin(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará el producto del sistema.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Producto/EliminarProducto?id=' + id;
        }
    });
}

function MensajeEntregaAgregada() {
    Swal.fire({
        title: 'Entrega editada exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajeconfirmarEliminacionEntrega(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará la entrega del sistema.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Entrega/EliminarEntrega?id=' + id;
        }
    });
}

function MensajeModificacionEntrega() {
    Swal.fire({
        title: 'Entrega editada exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajePedidoAgregado() {
    Swal.fire({
        title: 'Pedido editado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajeModificacionPedido() {
    Swal.fire({
        title: 'Pedido editado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

function MensajeconfirmarEliminacionPedido(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará el pedido del sistema.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Pedido/EliminarPedido?id=' + id;
        }
    });
}

function MensajeconfirmarEliminacionFactura(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará la factura del sistema.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Factura/EliminarFactura?id=' + id;
        }
    });
}

function MensajeModificacionUsuario() {
    Swal.fire({
        title: 'Usuario editado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
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