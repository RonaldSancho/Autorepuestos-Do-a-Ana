/*--------------------------------------------------------------
Ajax
--------------------------------------------------------------*/

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
            if (res.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Hola',
                    text: 'Se ha enviado un correo a su bendeja con su contraseña.',
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se ha podido enviar el correo. Por favor, inténtelo de nuevo más tarde.'
                });
            }
        }
    });
}

/*--------------------------------------------------------------
Mensajes de login
--------------------------------------------------------------*/
function MensajeCredenciales() {
    Swal.fire({
        title: 'Error de inicio de sesión',
        text: 'Credenciales incorrectas',
        icon: 'error',
        confirmButtonText: 'OK'
    });
}

function MensajeCredenciales2() {
    Swal.fire({
        title: 'Error de inicio de sesión',
        text: 'Cuenta inactiva',
        icon: 'error',
        confirmButtonText: 'OK'
    });
}
function MensajeUsuarioCreado() {
    Swal.fire({
        title: 'Cuenta creada exitosamente.¿Desea volver al iniciar sesión?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir al iniciar sesión',
        cancelButtonText: 'No, quedarme aquí'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Home/Index";
        }
    });
}

/*--------------------------------------------------------------
Mensajes de carrito
--------------------------------------------------------------*/

function MensajeProductoAgregadoCarrito() {
    Swal.fire({
        title: 'Producto agregado exitosamente al carrito',
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

function MensajeModificacionProductoCarrito() {
    Swal.fire({
        title: 'Su orden fue editada exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

/*--------------------------------------------------------------
Mensajes de productos
--------------------------------------------------------------*/

function MensajeProductoAgregado() {
    Swal.fire({
        title: 'Producto agregado exitosamente.¿Desea volver a la lista de productos?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la lista',
        cancelButtonText: 'No, quedarme aquí'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Producto/MostrarProductos";
        }
    });
}

function MensajeModificacionProducto() {
    Swal.fire({
        title: 'Producto modificado exitosamente.¿Desea volver a la lista de productos?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la lista',
        cancelButtonText: 'No, seguir editando'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Producto/MostrarProductos";
        }
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

/*--------------------------------------------------------------
Mensajes de entregas
--------------------------------------------------------------*/

function MensajeEntregaAgregada() {
    Swal.fire({
        title: 'Entrega agregado exitosamente.¿Desea volver a la vista de entregas?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la vista',
        cancelButtonText: 'No, quedarme aquí'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Entrega/VerEntregas";
        }
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
        title: 'Entrega modificada exitosamente.¿Desea volver a la vista de entregas?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la vista',
        cancelButtonText: 'No, seguir editando'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Entrega/VerEntregas";
        }
    });
}

/*--------------------------------------------------------------
Mensajes de pedidos
--------------------------------------------------------------*/

function MensajePedidoAgregado() {
    Swal.fire({
        title: 'Pedido agregado exitosamente.¿Desea volver a la lista de pedidos?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la lista',
        cancelButtonText: 'No, quedarme aquí'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Pedido/MostrarPedido";
        }
    });
}

function MensajeModificacionPedido() {
    Swal.fire({
        title: 'Pedido modificado exitosamente.¿Desea volver a la lista de pedidos?',
        icon: 'success',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Sí, ir a la lista',
        cancelButtonText: 'No, seguir editando'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Pedido/MostrarPedido";
        }
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

/*--------------------------------------------------------------
Mensajes de facturas
--------------------------------------------------------------*/

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

/*--------------------------------------------------------------
Mensajes de usuarios
--------------------------------------------------------------*/

function MensajeModificacionUsuario() {
    Swal.fire({
        title: 'Usuario editado exitosamente',
        icon: 'success',
        showConfirmButton: false,
        timer: 1500
    });
}

/*--------------------------------------------------------------
Evitar letras
--------------------------------------------------------------*/

function LetrasCedula() {
    var input = document.getElementById("pCedula");
    input.value = input.value.replace(/\D/g, "");
}

function LetrasTelefono() {
    var input = document.getElementById("pTelefono");
    input.value = input.value.replace(/\D/g, "");
}