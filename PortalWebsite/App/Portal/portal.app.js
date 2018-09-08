var root = document.getElementById('page');

m.route(root, '/', {
    '/': Home,
    '/edit/:name': Edit,
    '/new': Edit,
});
