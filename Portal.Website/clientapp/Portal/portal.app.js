var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
});

// error message
m.mount(document.getElementById('message-box'), ErrorMessage);
