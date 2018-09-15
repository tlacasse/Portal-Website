/**
 * Popup Exception Display.
 */
var ErrorMessage = {};

/**
 * Exception object from server, passed through 'objectToArray'
 */
ErrorMessage.exception = [['', '']];

/**
 * Return the partial views of a property-value pair.
 * @param {string[]} pair
 */
ErrorMessage.pairToRow = function (pair) {
    return (
        m('div', { class: 'message-box-pair' }, [
            m('div', { class: 'message-box-key' }, pair[0]),
            m('div', { class: 'message-box-value' }, pair[1]),
        ])
    );
}

/**
 * Show the popup error message box.
 * @param {Object} exceptionObject
 */
ErrorMessage.show = function (exceptionObject) {
    document.getElementById('message-box').style.display = 'block';
    ErrorMessage.exception = objectToArray(exceptionObject);
}

/**
 * Hide the popup error message box.
 */
ErrorMessage.hide = function () {
    document.getElementById('message-box').style.display = 'none';
}

/**
 * Mithril view.
 */
ErrorMessage.view = function () {
    return [
        m('div', { class: 'message-box-title' },
            m('button', {
                class: 'message-box-button',
                onclick: ErrorMessage.hide
            }, 'X')
        ),
        m('div', { class: 'message-box-detail' },
            ErrorMessage.exception.map(x => ErrorMessage.pairToRow(x))
        ),
    ];
}
