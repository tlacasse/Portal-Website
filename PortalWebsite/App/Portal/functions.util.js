/**
 * Formats a Name into a url-safe string.
 * @param {String} value
 */
function formatURL(value) {
    if (nonexistent(value)) {
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
 * Returns default new Icon.
 */
function emptyIcon() {
    return {
        Id: -1,
        Name: '',
        Image: 'png',
        Link: '',
        DateChanged: '',
    }
}

/**
 * Returns the file path of an image for an Icon.
 * @param {Icon} icon
 */
function iconImagePath(icon) {
    return '/Portal/Icons/' + icon.Id + '.' + icon.Image + '?d=' + icon.DateChanged;
}
