var Templates = {};

Templates.SplitContent = function (left, right) {
    return [
        m('div', { id: 'content-left' }, left),
        m('div', { id: 'content-right' }, right),
    ];
};

Templates.ThreePane = function (top, mid, bot) {
    return [
        m('div', { class: 'pane-top' }, top),
        m('div', { class: 'pane-mid' }, mid),
        m('div', { class: 'pane-bot' }, bot),
    ];
};
