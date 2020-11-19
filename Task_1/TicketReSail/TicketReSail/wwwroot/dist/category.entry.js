/******/ (() => { // webpackBootstrap
/*!************************!*\
  !*** ./js/category.js ***!
  \************************/
/*! unknown exports (runtime-defined) */
/*! runtime requirements:  */
﻿const url = "/api/v1/categories";
const jsonType = "application/jcon";

async function getCategories() {
    const response = await fetch(url,
        {
            method: "GET",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {

        const categories = await response.json();
        let rows = document.querySelector("tbody");
        categories.forEach(category => {
            rows.append(row(category));
        });
    }
}

async function createCategory(categoryName) {
    const response = await fetch(url,
        {
            method: "POST",
            headers: { "Accept": jsonType, "Content-Type": "application/json" },
            body: JSON.stringify({
                name: categoryName
            })
        });
    if (response.ok === true) {
        const category = await response.json();
        reset();
        document.querySelector("tbody").append(row(category));
    } else {
        const errorData = await response.json();
        console.log("error", errorData);
        if (errorData) {

            if (errorData.errors) {

                if (errorData.error["Name"]) {
                    addError(errorData.errors["Name"]);
                }
            }

            if (errorData["Name"]) {
                addError(errorData["Name"]);
            }
        }

        document.getElementById("error").style.display = "block";
    }
}

async function deleteCategory(id) {
    const response = await fetch(url + "/" + id,
        {
            method: "DELETE",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {
        const category = await response.json();
        document.querySelector(`tr[data-rowid='${category.id}']`).remove();
    }
}

function reset() {
    const form = document.forms["categoryForm"];
    form.reset();
    form.elements["id"].value = 0;
}

function addError(errors) {
    errors.forEach(error => {
        const p = document.createElement("p");
        p.append(error);
        document.getElementById("error").append(p);
    });
}

function row(category) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", category.id);

    const nameTd = document.createElement("td");
    nameTd.append(category.name);
    tr.append(nameTd);

    const linksTd = document.createElement("td");

    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", category.id);
    removeLink.setAttribute("type", "submit");
    removeLink.setAttribute("class", "btn btn-sm btn-primary");
    removeLink.append("Delete");
    removeLink.addEventListener("click", e => {

        e.preventDefault();
        deleteCategory(category.id);
    });

    linksTd.append(removeLink);
    tr.appendChild(linksTd);

    return tr;
}

// send form
document.forms["categoryForm"].addEventListener("submit",
    e => {
        e.preventDefault();
        document.getElementById("error").innerHTML = "";
        document.getElementById("error").style.display = "none";

        const form = document.forms["categoryForm"];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        if (id == 0)
            createCategory(name);
    });

getCategories();
/******/ })()
;
//# sourceMappingURL=category.entry.js.map