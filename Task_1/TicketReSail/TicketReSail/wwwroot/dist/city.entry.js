/******/ (() => { // webpackBootstrap
/*!********************!*\
  !*** ./js/city.js ***!
  \********************/
/*! unknown exports (runtime-defined) */
/*! runtime requirements:  */
﻿const url = "/api/v1/Cities";
const jsonType = "application/jcon";

async function fillCitySelector() {
    const response = await fetch("/api/v1/cities",
        {
            method: "GET",
            headers: { 'Accept': jsonType },
        });
    if (response.ok === true) {

        const cities = await response.json();
        let selector = document.querySelector('#cities');
        for (const city of cities) {
            const option = document.createElement('option');
            option.value = city.id;
            option.append(city.name);
            selector.append(option);
        }
    }
}

fillCitySelector();

async function getCities() {
    const response = await fetch("/api/v1/Cities",
        {
            method: "GET",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {

        const cities = await response.json();
        let rows = document.querySelector("tbody");
        cities.forEach(city => {
            rows.append(row(city));
        });
    }
}

async function createCity(cityName) {
    const response = await fetch(url,
        {
            method: "POST",
            headers: { "Accept": jsonType, "Content-Type": "application/json" },
            body: JSON.stringify({
                name: cityName
            })
        });
    if (response.ok === true) {
        const city = await response.json();
        reset();
        document.querySelector("tbody").append(row(city));
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

async function deleteCity(id) {
    const response = await fetch(url + "/" + id,
        {
            method: "DELETE",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {
        const city = await response.json();
        document.querySelector(`tr[data-rowid='${city.id}']`).remove();
    }
}

function reset() {
    const form = document.forms["cityForm"];
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

function row(city) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", city.id);

    const nameTd = document.createElement("td");
    nameTd.append(city.name);
    tr.append(nameTd);

    const linksTd = document.createElement("td");

    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", city.id);
    removeLink.setAttribute("type", "submit");
    removeLink.setAttribute("class", "btn btn-sm btn-primary");
    removeLink.append("Delete");
    removeLink.addEventListener("click", e => {

        e.preventDefault();
        deleteCity(city.id);
    });

    linksTd.append(removeLink);
    tr.appendChild(linksTd);

    return tr;
}

// send form
document.forms["cityForm"].addEventListener("submit",
    e => {
        e.preventDefault();
        document.getElementById("error").innerHTML = "";
        document.getElementById("error").style.display = "none";

        const form = document.forms["cityForm"];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        if (id == 0)
            createCity(name);
    });

getCities();
/******/ })()
;
//# sourceMappingURL=city.entry.js.map