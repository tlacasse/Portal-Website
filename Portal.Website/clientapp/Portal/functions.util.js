
function formatURL(value) {
    if (nonexistent(value)) {
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

function emptyIcon() {
    return {
        Id: -1,
        Name: '',
        Image: 'png',
        Link: '',
        DateChanged: '',
    }
}

function iconImagePath(icon) {
    return '/Data/Icons/' + icon.Id + '.' + icon.Image + '?d=' + icon.DateChanged;
}
