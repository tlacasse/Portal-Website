/**
 * Main View Templates.
 */
var Templates = {};

/**
 * Template for the left-right split view.
 * @param {Component} left
 * @param {Component} right
 */
Templates.splitContent = function (left, right) {
    return [
        m('div', { id: 'content-left' }, left),
        m('div', { id: 'content-right' }, right),
    ];
}

/**
 * Template for the header-content-footer in both of the split-views.
 * @param {Component} top
 * @param {Component} mid
 * @param {Component} bot
 */
Templates.threePane = function (top, mid, bot) {
    return [
        m('div', { class: 'pane-top' }, top),
        m('div', { class: 'pane-mid' }, mid),
        m('div', { class: 'pane-bot' }, bot),
    ];
}
