"use strict";

var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
    '/edit/:name': Edit,
    '/new': Edit,
    '/grid': Grid,
});

m.mount(document.getElementById('message-box'), MessageBox);
