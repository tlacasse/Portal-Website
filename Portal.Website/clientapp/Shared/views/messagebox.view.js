"use strict";
var MessageBox = {};

// Exception object from server, passed through 'objectToArray'
MessageBox.exception = [['', '']];

MessageBox.propertyPairToRow = function (pair) {
    return (
        m('div', { class: 'message-box-pair' }, [
            m('div', { class: 'message-box-key' }, pair[0]),
            m('div', { class: 'message-box-value' }, pair[1]),
        ])
    );
};

MessageBox.view = function () {
    return [
        m('div', { class: 'message-box-title' },
            m('button', {
                class: 'message-box-button',
                onclick: App.hideMessage,
            }, 'X')
        ),
        m('div', { class: 'message-box-detail' },
            MessageBox.exception.map(x => MessageBox.propertyPairToRow(x))
        ),
    ];
}
