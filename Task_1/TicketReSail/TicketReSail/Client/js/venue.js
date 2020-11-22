const url = "/api/v1/Venues";
const jsonType = "application/jcon";

async function fillVenueSelector() {
    const response = await fetch("/api/v1/venues",
        {
            method: "GET",
            headers: { 'Accept': jsonType },
        });
    if (response.ok === true) {

        const venues = await response.json();
        let selector = document.querySelector('#venues');
        for (const venue of venues) {
            const option = document.createElement('option');
            option.value = venue.id;
            option.append(venue.name);
            selector.append(option);
        }
    }
}

fillVenueSelector()

async function getVenues() {
    const response = await fetch(url,
        {
            method: "GET",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {

        const venues = await response.json();
        let rows = document.querySelector("tbody");

        venues.forEach(venue => {
            rows.append(row(venue));
        });
    }
}

async function getVenue(id) {
    const response = await fetch(url + "/" + id,
        {
            method: "GET",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {
        const venue = await response.json();
        const form = document.form["venueForm"];
        form.elements["id"].value = venue.id;
        form.elements["name"].value = venue.name;
        form.elements["address"].value = venue.address;
        form.elements["cityId"].value = venue.cityId;
    }
}

async function createVenue(venueName, venueAddress, citiId) {
    const response = await fetch(url,
        {
            method: "POST",
            headers: { "Accept": jsonType, "Content-Type": "application/json" },
            body: JSON.stringify({
                name: venueName,
                address: venueAddress,
                cityId: citiId
            })
        });
    if (response.ok === true) {
        const venue = await response.json();
        reset();
        document.querySelector("tbody").append(row(venue));
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

async function deleteVenue(id) {
    const response = await fetch(url + "/" + id,
        {
            method: "DELETE",
            headers: { "Accept": jsonType }
        });
    if (response.ok === true) {
        const venue = await response.json();
        document.querySelector(`tr[data-rowid='${venue.id}']`).remove();
    }
}

function reset() {
    const form = document.forms["venueForm"];
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

function row(venue) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", venue.id);

    const nameTd = document.createElement("td");
    nameTd.append(venue.name);
    tr.append(nameTd);

    const addressTd = document.createElement("td");
    addressTd.append(venue.address);
    tr.append(addressTd);

    const cityIdTd = document.createElement("td");
    cityIdTd.append(venue.city.name);
    tr.append(cityIdTd);

    const linksTd = document.createElement("td");

    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", venue.id);
    removeLink.setAttribute("type", "submit");
    removeLink.setAttribute("class", "btn btn-sm btn-primary");
    removeLink.append("Delete");
    removeLink.addEventListener("click", e => {

        e.preventDefault();
        deleteVenue(venue.id);
    });

    linksTd.append(removeLink);
    tr.appendChild(linksTd);

    return tr;
}

// send form
document.forms["venueForm"].addEventListener("submit",
    e => {
        e.preventDefault();
        document.getElementById("error").innerHTML = "";
        document.getElementById("error").style.display = "none";
    
        const form = document.forms["venueForm"];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        const address = form.elements["address"].value;
        const cityId = form.selector["cityId"].value;
        if (id == 0)
            createVenue(name, address, cityId);
        else
            editVenue(id, name, address, cityId);
    });

getVenues();