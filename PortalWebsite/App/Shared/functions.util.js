/**
 * Returns true if a value is null or undefined.
 * @param {any} value
 */
function nonexistent(value) {
    return value === null || value === undefined;
}

/**
 * Returns a default value if the first is null.
 * @param {any} value
 * @param {any} defaultIfNull
 */
function coalesce(value, defaultIfNull) {
    if (nonexistent(value)) {
        return defaultIfNull;
    }
    return value;
}

/**
 * Returns 'n' &nbsp;'s.
 * @param {Number} n
 */
function nbsps(n) {
    var result = '';
    for (var i = 0; i < n; i++) {
        result += '\u00A0'
    }
    return result;
}

/**
 * Returns a &nbsp;.
 */
function nbsp() {
    return nbsps(1);
}

/**
 * Returns an array of property-value pair arrays.
 * @param {Object} value
 */
function objectToArray(value) {
    var array = [];
    for (var k in value) {
        if (value.hasOwnProperty(k)) {
            array.push([k, value[k]]);
        }
    }
    return array;
}

/**
 * Returns the DOM element with the specified id.
 * @param {String} id
 */
function get(id) {
    return document.getElementById(id);
}

/**
 * Force a value to be in a certain range.
 * @param {Number} value
 * @param {Number} min
 * @param {Number} max
 */
function clamp(value, min, max) {
    if (value < min) return min;
    if (value > max) return max;
    return value;
}

/**
 * Return a 2d array populated with a certain value, or if the value is a function,
 * it is populated with the function invoked with the (x,y) indices.
 * @param {Number} w
 * @param {Number} h
 * @param {Function} value
 */
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
