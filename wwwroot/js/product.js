document.addEventListener("DOMContentLoaded", () => {

    const modal = new bootstrap.Modal(document.getElementById("productModal"));

    document.getElementById("btnCreate")
        .addEventListener("click", () => openCreateModal(modal));

    document.getElementById("btnSaveOrUpdate")
        .addEventListener("click", async () => saveProduct(modal));

    document.addEventListener("click", async (e) => {

        if (e.target.classList.contains("btn-edit")) {

            const id = Number(e.target.dataset.id);

            await openEditModal(id, modal);
        }

        if (e.target.classList.contains("btn-delete")) {

            const id = Number(e.target.dataset.id);

            await deleteProduct(id);

            const products = await getProducts();
            await renderProducts(products);
        }
    });

});

async function getProducts() {

    const response = await fetch("/Product/GetAll");

    if (!response.ok) {
        console.error("Error obteniendo productos");
        return;
    }

    const products = await response.json();

    return products;
}

async function getProductById(id) {

    const response = await fetch(`/Product/GetById/${id}`);

    if (!response.ok) {
        console.error("Error obteniendo producto");
        return;
    }

    const product = await response.json();

    return product;
}

async function createProduct(product) {

    const response = await fetch("/Product/Add", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(product)
    });

    if (!response.ok) {
        console.error("Error al crear el producto");
    }
}

async function updateProduct(product) {

    const response = await fetch(`/Product/Update/${product.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(product)
    });

    if (!response.ok) {
        console.error("Error al actualizar el producto");
    }
}

async function saveProduct(modal) {

    const product = getProductFromForm();

    if (product.id) {
        await updateProduct(product);
    }
    else {
        await createProduct(product);
    }

    modal.hide();

    const products = await getProducts();
    await renderProducts(products);
}

async function deleteProduct(id) {

    const response = await fetch(`/Product/Delete/${id}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        console.error("Error al eliminar el producto");
    }
}

function openCreateModal(modal) {

    clearModal();
    document.querySelector(".modal-title").textContent = "Crear producto";
    modal.show();
}

async function openEditModal(id, modal) {

    const product = await getProductById(id);
    loadModal(product);

    document.querySelector(".modal-title").textContent = "Editar producto";
    modal.show();
}

function getProductFromForm() {

    return {
        id: document.getElementById("productId").value,
        name: document.getElementById("name").value,
        description: document.getElementById("description").value,
        price: parseFloat(document.getElementById("price").value),
        productTypeId: parseInt(document.getElementById("productTypeId").value),
        entrepreneurshipId: parseInt(document.getElementById("entrepreneurshipId").value)
    };
}

function renderProducts(products) {

    const tbody = document.getElementById("tbodyProduct");

    tbody.innerHTML = "";

    products.forEach(product => {

        tbody.innerHTML += `
            <tr>
                <td>${product.name}</td>
                <td>${product.description}</td>
                <td>${product.price}</td>
                <td>${product.productTypeId}</td>
                <td>${product.entrepreneurshipId}</td>
                <td>
                    <button class="btn btn-success btn-edit" data-id="${product.id}">Editar</button>
                    <button class="btn btn-danger btn-delete" data-id="${product.id}">Eliminar</button>
                </td>
            </tr>
        `;
    });
}

function clearModal() {

    document.getElementById("productForm").reset();
    document.getElementById("productId").value = "";
}

function loadModal(product) {

    document.getElementById("productId").value = product.id;
    document.getElementById("name").value = product.name;
    document.getElementById("description").value = product.description;
    document.getElementById("price").value = product.price;
    document.getElementById("productTypeId").value = product.productTypeId;
    document.getElementById("entrepreneurshipId").value = product.entrepreneurshipId;
}