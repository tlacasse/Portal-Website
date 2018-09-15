/**
 * Returns true if a value is null or undefined.
 * @param {any} value
 */
function nonexistant(value) {
    return value === null || value === undefined;
}

/**
 * Returns a default value if the first is null.
 * @param {any} value
 * @param {any} defaultIfNull
 */
function coalesce(value, defaultIfNull) {
    if (nonexistant(value)) {
        return defaultIfNull;
    }
    return value;
}

/**
 * Formats a Name into a url-safe string.
 * @param {String} value
 */
function formatURL(value) {
    if (nonexistant(value)) {
        return "";
    }
    return value.toLowerCase().split(' ').join('_');
}

/**
 * Undo the process of formatURL(value).
 * @param {String} value
 */
function unFormatURL(value) {
    return value
        .split('_')
        .map(w => w.substr(0, 1).toUpperCase() + w.substring(1))
        .join(' ');
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
