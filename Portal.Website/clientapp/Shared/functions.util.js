

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

function get(id) {
    return document.getElementById(id);
}

function clamp(value, min, max) {
    if (value < min) return min;
    if (value > max) return max;
    return value;
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
