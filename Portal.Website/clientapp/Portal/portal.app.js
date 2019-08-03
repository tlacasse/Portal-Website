var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
    '/edit/:name': Edit,
    '/new': Edit,
    '/grid': Grid,
});

// error message
m.mount(document.getElementById('message-box'), ErrorMessage);
