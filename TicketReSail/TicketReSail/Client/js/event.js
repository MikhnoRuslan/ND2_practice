import $, { get } from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import { fillCategoriesSelector, fillCitiesSelector, fillVenuesSelector } from './selector';

const filters = {
    pageSize: 6,
    page: 1,
    cities: [],
    categories: [],
    venues: [],
    fromDateTime: '',
    toDateTime: '',
    sortBy: 0,
    sortOrder: 0,
    eventName: ''
};

const filterVenue = {
    cities: []
}

function createEvent(event) {
    return `<div class="col mb-4">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="/event/getimage/${event.id}" alt="${event.name}" class="card-img-top img-fluid" />
                        <strong>
                             <a href='event/details/${event.id}'>${event.eventName}</a>
                        </strong>
                        <br><h8>The date of the: ${event.dateTime} </h8>
                        <br><h8>City: ${event.venue.city.name}</h8>
                        <br><h8>Venue: ${event.venue.address}</h8>
                    </div>
                </div>
            </div>`;
}

$(document).ready(function() { 
    getEvents();

    $("#eventName").on('change', function(){
        filters.eventName = $(this).val();
        getEvents();
    })

    $("#categories").on('change', function(){
        filters.categories = $(this).val();
        getEvents();
    })

    $("#cities").on('change', function(){
        filterVenue.cities = $(this).val();
        filters.cities = $(this).val();
        
        filters.venues = [];

        fillVenuesSelector(filterVenue);
        getEvents();
    })

    $("#venues").on('change', function(){
        filters.venues = $(this).val();
        getEvents();
    })

    $("#fromDateTime").on("change", function() {
        filters.fromDateTime = $(this).val();
        getEvents();
    });

    $("#toDateTime").on("change", function() {
        filters.toDateTime = $(this).val();
        getEvents();
    });

    $("#sortBy").on("change", function() {
        filters.sortBy = $(this).val();
        getEvents();
    });

    $("#sortOrder").on("change", function() {
        filters.sortOrder = $(this).val();
        getEvents();
    });
});

function getEvents() {
    $.ajax({
        url: "api/v1/events",
        data: filters,
        traditional: true,
        success: function (data, status, xhr) {
            data = data.map(item => {
                item.dateTime = dateTime(item.dateTime);
                return item;
            });
            $("#events").empty().append($.map(data, createEvent));
            const count = xhr.getResponseHeader("x-total-count");
            addPaginationButtons(filters.page, count, filters.pageSize);
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];
    for (let i = 1; i <= pageCount; i++) {
        const button = $("<li>", { class: "page-item" });
        if (i === currentPage) {
            button.addClass("active");
            button.append(`<a class="page-link" href="#">${i} <span class="sr-only">(current)</span></a>`)
        } else {
            button.append(`<a class="page-link" href="#">${i}</a>`)
        }
        button.data("page", i);
        buttons.push(button);
    }
    $(".pagination").empty().append(buttons);
    $(".page-item").on("click", function () {
        filters.page = $(this).data("page");
        getEvents();
    });
}

function zeroFirstFormat(value) {
    if(value < 10)
        value = '0' + value;
    
    return value;
}

function dateTime(date) {
        var currentDatetime = new Date(date);
        var day = zeroFirstFormat(currentDatetime.getDate());
        var month = zeroFirstFormat(currentDatetime.getMonth()+1);
        var year = currentDatetime.getFullYear();

        return day + "." + month + "." + year;
}

fillCategoriesSelector();

fillCitiesSelector();
