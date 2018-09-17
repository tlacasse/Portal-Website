/**
 * Portal Configuration entry screen.
 */
var Home = {};

/**
 * Route to the home Portal Configuration screen.
 */
Home.goto = function () {
    m.route.set('/');
}

/**
 * Mithril oninit.
 */
Home.oninit = function () {
    IconList.oninit();
}

/**
 * Mithril view.
 */
Home.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Portal Configuration'),
            m('ul', [
                m('li', m("a[href=/new]", { oncreate: m.route.link }, 'New Icon')),
                m('li', m("a[href=/grid]", { oncreate: m.route.link }, 'Grid Configuration')),
            ]),
            ''
        ),
    );
}
