var ErrorMessage = {};

ErrorMessage.exception = [['', '']];

ErrorMessage.pairToRow = function (pair) {
    return (
        m('div', { class: 'message-box-pair' }, [
            m('div', { class: 'message-box-key' }, pair[0]),
            m('div', { class: 'message-box-value' }, pair[1]),
        ])
    );
}

ErrorMessage.show = function (exceptionObject) {
    document.getElementById('message-box').style.display = 'block';
    ErrorMessage.exception = objectToArray(exceptionObject);
}

ErrorMessage.hide = function () {
    document.getElementById('message-box').style.display = 'none';
}

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
