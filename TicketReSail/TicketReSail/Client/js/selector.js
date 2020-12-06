import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';

export function fillCitiesSelector() {
    $.ajax({
        url: '/api/v1/cities',
        traditional: true,
        success: function (data) {
            $('#cities').empty().append($.map(data, createSelector));
            $('#cities').selectpicker('refresh');
        },
    });
}

export function fillCategoriesSelector() {
    $.ajax({
        url: '/api/v1/categories',
        traditional: true,
        success: function (data) {
            $('#categories').empty().append($.map(data, createSelector));
            $('#categories').selectpicker('refresh');
        },
    });
}

export function fillVenuesSelector(filterVenue) {
    $.ajax({
        url: '/api/v1/venues',
        traditional: true,
        data: filterVenue,
        success: function (data) {
            $('#venues').empty().append($.map(data, createSelector));
            $('#venues').selectpicker('refresh');
        },
    });
}

function createSelector(item) {
    return `<option value="${item.id}">${item.name}</option>`;
}