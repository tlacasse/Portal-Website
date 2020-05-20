"use strict";

var Templates = {};

Templates.splitContent = function (left, right) {
    return [
        m('div#content-left', left),
        m('div#content-right', right),
    ];
}

Templates.threePane = function (top, mid, bot) {
    return [
        m('div.pane-top', top),
        m('div.pane-mid', mid),
        m('div.pane-bot', bot),
    ];
}
