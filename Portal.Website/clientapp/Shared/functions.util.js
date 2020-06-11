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

function getArray2d(w, h, value) {
    var a = [];
    for (var i = 0; i < w; i++) {
        var b = [];
        for (var j = 0; j < h; j++) {
            if (typeof value === "function") {
                b.push(value(i, j));
            } else {
                b.push(value);
            }
        }
        a.push(b);
    }
    return a;
}

function genUnique() {
    return String(new Date()) + String(Math.random());
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
    var data = {
        method: 'GET',
        dataType: 'jsonp',
        url: url,
    };
    if (!nonexistent(params)) {
        data.params = params;
    }
    m.request(data).then(function (data) {
        App.reenable();
        success(data);
    }).catch(function (e) {
        App.reenable();
        App.showMessage(e);
    });
}

API.get = function (url, success) {
    API._get(url, success, true, null);
}

API.aget = function (url, success) {
    API._get(url, success, false, null);
}

API.pget = function (url, params, success) {
    API._get(url, success, true, params);
}

API._post = function (url, success, block, body, dataobj) {
    url = '/api/' + url;
    if (block) {
        App.wait();
    }
    var data = {
        method: 'POST',
        url: url,
    };
    if (!nonexistent(body)) {
        data.body = body;
    }
    if (!nonexistent(dataobj)) {
        data.data = dataobj;
    }
    m.request(data).then(function (data) {
        App.reenable();
        success(data);
    }).catch(function (e) {
        App.reenable();
        App.showMessage(e);
    });
}

API.post = function (url, success) {
    API._post(url, success, true, null, null);
}

API.bpost = function (url, body, success) {
    API._post(url, success, true, body, null);
}

API.dpost = function (url, data, success) {
    API._post(url, success, true, null, data);
}

var App = {};

App.showMessage = function (exceptionObject) {
    var obj = exceptionObject;
    if (obj.hasOwnProperty('response')) {
        obj = obj.response;
    }
    App.get('message-box').style.display = 'block';
    MessageBox.exception = objectToArray(obj);
}

App.hideMessage = function () {
    App.get('message-box').style.display = 'none';
}

App.wait = function () {
    App.get('wall').style.display = 'block';
}

App.reenable = function () {
    App.get('wall').style.display = 'none';
}

App.get = function (id) {
    return document.getElementById(id);
}
