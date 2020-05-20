"use strict";

var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
});

m.mount(document.getElementById('message-box'), MessageBox);
