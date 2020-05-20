"use strict";

function nonexistent(value) {
    return value === null || value === undefined;
}

function coalesce(value, defaultIfNull) {
    if (nonexistent(value)) {
        return defaultIfNull;
    }
    return value;
}

function nbsps(n) {
    var result = '';
    for (var i = 0; i < n; i++) {
        result += '\u00A0';
    }
    return result;
}

function nbsp() {
    return nbsps(1);
}

function objectToArray(value) {
    var array = [];
    for (var k in value) {
        array.push([k, value[k]]);
    }
    return array;
}

function clamp(x, min, max) {
    if (x < min) {
        return min;
    }
    if (x > max) {
        return max;
    }
    return x;
}

String.prototype.replaceAll = function (search, replacement) {
    return this.split(search).join(replacement);
};

var API = {};

API._get = function (url, success, block, params) {
    url = '/api/' + url;
    if (block) {
        App.wait();
    }
    m.request({
        method: 'GET',
        dataType: 'jsonp',
        url: url,
        params: params,
    }).then(function (data) {
        App.reenable();
        success(data);
    }).catch(function (e) {
        App.reenable();
        App.showMessage(e);
    });
}

API.get = function (url, success) {
    API._get(url, success, true, {});
}

API.aget = function (url, success) {
    API._get(url, success, false, {});
}

API.pget = function (url, params, success) {
    API._get(url, success, true, params);
}

var App = {};

App.showMessage = function (exceptionObject) {
    App.get('message-box').style.display = 'block';
    MessageBox.exception = objectToArray(exceptionObject.response);
}

App.hideMessage = function () {
    get('message-box').style.display = 'none';
}

App.wait = function () {
    document.getElementById('wall').style.display = 'block';
}

App.reenable = function () {
    document.getElementById('wall').style.display = 'none';
}

App.get = function (id) {
    return document.getElementById(id);
}
