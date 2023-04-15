document.addEventListener('DOMContentLoaded', function () {
    const buscarProductos = () => {
        const input = document.getElementById('search-input');
        const filter = input.value.toUpperCase();
        const productos = document.querySelectorAll('.gallery-item');
        productos.forEach((producto) => {
            const nombreProducto = producto.querySelector('.card-title').textContent.toUpperCase();
            const descripcionProducto = producto.querySelector('.card-text').textContent.toUpperCase();
            const urlAction = producto.querySelector('.btn-dark').getAttribute('href');
            if (nombreProducto.indexOf(filter) > -1 || descripcionProducto.indexOf(filter) > -1) {
                producto.style.display = '';
            } else {
                producto.style.display = 'none';
            }
        });
    };

    const searchInput = document.getElementById('search-input');
    searchInput.addEventListener('keyup', buscarProductos);
});
