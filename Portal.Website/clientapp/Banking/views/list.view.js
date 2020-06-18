"use strict";

var List = {};

// which table we are updating
List.table = null;

// properties of the table
List.columns = [];

// records of the table
List.data = [];

List.oninit = function () {
    List.table = m.route.param('table');
    List.fetchData();
}

List.view = function () {
    return '';
}

List.fetchData = function () {
    API.get('banking/list/columns/' + List.table, function (data) {
        List.columns = data;
        API.get('banking/list/data/' + List.table, function (data) {
            List.data = data;
        });
    });
}
