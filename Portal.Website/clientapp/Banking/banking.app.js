"use strict";

var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
    '/list/:table': List,
});

m.mount(document.getElementById('message-box'), MessageBox);
