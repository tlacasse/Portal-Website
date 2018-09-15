function nonexistant(value) {
    return value === null || value === undefined;
}

function coalesce(value, defaultIfNull) {
    if (nonexistant(value)) {
        return defaultIfNull;
    }
    return value;
}

function formatURL(value) {
    if (nonexistant(value)) {
        return "";
    }
    return value.toLowerCase().split(' ').join('_');
}

function unFormatURL(value) {
    return value
        .split('_')
        .map(w => w.substr(0, 1).toUpperCase() + w.substring(1))
        .join(' ');
}

function nbsps(n) {
    var result = '';
    for (var i = 0; i < n; i++) {
        result += '\u00A0'
    }
    return result;
}

function nbsp() {
    return nbsps(1);
}

function objectToArray(value) {
    var array = [];
    for (var k in value) {
        if (value.hasOwnProperty(k)) {
            array.push([k, value[k]]);
        }
    }
    return array;
}
