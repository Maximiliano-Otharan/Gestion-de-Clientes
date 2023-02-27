document.getElementById("home").addEventListener("click", () => {window.location.reload()});
const URL_API = 'https://localhost:7164/api/customer';
document.addEventListener("DOMContentLoaded", search());
customers = [];
document.getElementById("btnAgregar").addEventListener("click", agregarCliente);

function openModel() {
    htmlModal = document.getElementById("modale");
    htmlModal.classList.add("opened");
    document.getElementById("exitModale").addEventListener("click", () => {
        htmlModal.classList.remove("opened");
    })
}

function agregarCliente() {
    document.getElementById("btnSave").innerHTML = '<a onclick="save()" href="#" class="btn" id="btn_ingresar">Guardar</a>'
    openModel();
}

async function search() {

    let res = await fetch(URL_API, {
        "method": 'GET',
        "headers": {
            "Content-Type": "application/json",
        },
    });
    
    customers = await res.json();
    
    let html = '';
    for (c of customers){
        let row = `
        <tr>
            <td>${c.firstName}</td>
            <td>${c.lastName}</td>
            <td>${c.email}</td>
            <td>${c.phone}</td>
            <td>
                <button onclick="edit(${c.id})" class="button button2">Editar</button>
                <button onclick="remove(${c.id})" class="button button3">Eliminar</button>
            </td>
        </tr>`
        html = html + row;
    }
    
    document.getElementById("bodyTable").innerHTML = html;
}

async function remove(id) {
    respuesta = confirm('¿Estas seguro de eliminarlo?');
    if(respuesta) {
        await fetch(URL_API + "/" + id, {
            "method": 'DELETE',
            "headers": {
                "Content-Type": "application/json",
            },
        });

        window.location.reload();
    }
}

async function save() {
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone").value;
    let address = document.getElementById("address").value;

    let data = {
        "firstName": firstName,
        "lastName": lastName,
        "email": email,
        "phone": phone,
        "address": address
    }

    await fetch(URL_API, {
        "method": 'POST',
        "body": JSON.stringify(data),
        "headers": {
            "Content-Type": "application/json",
        },
    });

    window.location.reload();
}

function edit(id) {
    let customer = customers.find(c => c.id == id);

    document.getElementById("firstName").value = customer.firstName;
    document.getElementById("lastName").value = customer.lastName;
    document.getElementById("email").value = customer.email;
    document.getElementById("phone").value = customer.phone;
    document.getElementById("address").value = customer.address;
    
    document.getElementById("btnSave").innerHTML = `<a onclick="editCustomer(${customer.id})" href="#" class="btn" id="btn_ingresar">Guardar</a>`
    openModel();
}

function editCustomer(id) {
    let customer = customers.find(c => c.id == id);

    customer.firstName = document.getElementById("firstName").value;
    customer.lastName = document.getElementById("lastName").value;
    customer.email = document.getElementById("email").value;
    customer.phone = document.getElementById("phone").value;
    customer.address = document.getElementById("address").value;

    editRequest(customer);
}

async function editRequest(customer) {
    await fetch(URL_API, {
        "method": 'PUT',
        "body": JSON.stringify(customer),
        "headers": {
            "Content-Type": "application/json",
        },
    });

    window.location.reload();
}