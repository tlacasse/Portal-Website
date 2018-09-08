function nonexistant(value) {
    return value === null || value === undefined;
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
