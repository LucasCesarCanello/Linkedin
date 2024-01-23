//FORMULARIO COMPRA
document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

document.getElementById('formulario_compra').addEventListener('submit', function (event) {
    event.preventDefault();

    const codigo = document.getElementById('cod_prod_vend').value;
    const dni = document.getElementById('dni_cliente').value;
    const cantidad = document.getElementById('cant_comprada').value;
    const fechaEntrega = document.getElementById('fecha_entrega').value;

    const data = {
        codigo: codigo,
        dni: dni,
        cantidad: cantidad,
        fechaEntrega: fechaEntrega
    };
    fetch('<LLENAR CON EL URL>', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Respuesta de la API:', data);
            alert('Registro exitoso');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Error al registrar');
        });

});

//LISTA PRODUCTOS
function LlenarTablaProductos() {
    fetch('< LLENAR CON EL URL>')
        .then(response => response.json())
        .then(data => {
            var TablaProductos = document.getElementById('filas_productos');
            data.forEach(producto => {
                var fila = document.createElement('tr');
                fila.innerHTML = `
                <td>${producto.Nombre}</td>
                <td>${producto.Marca}</td>
                <td>${producto.Alto}</td>
                <td>${producto.Ancho}</td>
                <td>${producto.Profundidad}</td>
                <td>${producto.PrecioUnitario}</td>
                <td>${producto.StockMinimo}</td>
                <td>${producto.Stock}</td>
                `;
                TablaProductos.appendChild(fila);
            });
        })
        .catch(error => {
            console.error('Error al obtener datos:', error);
        });
}

document.addEventListener("DOMContentLoaded", LlenarTablaProductos)


//ACTUALIZAR STOCK PRODUCTO
const codigo = document.getElementById('Id').value;
const stock = document.getElementById('stock').value;

const data = {
    codigo: codigo,
    stock: stock
};

const options = {
    method: 'PUT',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
};

fetch('<LLENAR CON EL URL>', options)
    .then(response => {
        if (response.ok) {
            return response.json();
        }
        throw new Error('Error en la solicitud PUT');
    })
    .then(updatedData => {
        console.log('Datos actualizados:', updatedData);
    })
    .catch(error => {
        console.error('Error:', error);
    });

//CREAR VIAJE

document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Pruebas")
});

document.getElementById('form_viajes').addEventListener('submit', function (event) {
    event.preventDefault();

    const fechaDesde = document.getElementById('FecEntregaDesde').value;
    const fechaHasta = document.getElementById('FecEntregaHasta').value;

    const data = {
        fechaDesde: fechaDesde,
        fechaHasta: fechaHasta
    };
    fetch('<LLENAR CON EL URL>', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Respuesta de la API:', data);
            alert('Registro exitoso');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Error al registrar');
        });

});