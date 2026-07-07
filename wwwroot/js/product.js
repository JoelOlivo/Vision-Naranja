document.addEventListener("DOMContentLoaded", () => {

    const modal = new bootstrap.Modal(document.getElementById("productModal"));
    const btnCreate = document.getElementById("btnCreate");

    btnCreate.addEventListener("click", () => {
        modal.show();
    });

    const btnSave = document.getElementById("btnSave");

    btnSave.addEventListener("click", async () => {

        const product = {
            name: document.getElementById("name").value,
            description: document.getElementById("description").value,
            price: parseFloat(document.getElementById("price").value),
            productTypeId: parseInt(document.getElementById("productTypeId").value),
            entrepreneurshipId: parseInt(document.getElementById("entrepreneurshipId").value)
        }

        await createProduct(product);

        modal.hide();
    });

    document.addEventListener("click", async (e) => {

        if (e.target.classList.contains("btn-edit")) {

            const id = Number(e.target.dataset.id);

            console.log("Editar", id);

            // await loadProduct(id);
            // modal.show();
        }


        if (e.target.classList.contains("btn-delete")) {

            const id = Number(e.target.dataset.id);

            console.log("Eliminar", id);

            await deleteProduct(id);
        }
    });

    // document.addEventListener("click", async (e) => {
    //     console.log("en proceso...");
    // });

    // document.addEventListener("click", async (e) => {
    //     console.log("en proceso...");
    //     // const btn = e.target.closest(".btn-delete");

    //     // if (!btn) return;

    //     // const id = btn.dataset.id;

    //     // await deleteProduct(id);
    // });

});

async function createProduct(product) {
    console.log(product);

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

    location.reload();
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

    modal.hide();

    location.reload();
}

async function deleteProduct(id) {

    const response = await fetch(`/Product/Delete/${id}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        console.error("Error al eliminar el producto");
    }

    location.reload();
}