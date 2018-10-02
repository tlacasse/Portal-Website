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
            m('div', { class: 'section-title' }, 'Portal'),
            [
                m('span', { class: 'section-title' }, 'Configuration'),
                m('div', { class: 'hrule' }),
                m('ul', [
                    m('li', m("a[href=/new]", { oncreate: m.route.link }, 'New Icon')),
                    m('li', m("a[href=/grid]", { oncreate: m.route.link }, 'Grid Configuration')),
                    m('li', m("a[href=/build]", { oncreate: m.route.link }, 'Build Icon Grid')),
                ]),
                m('br'),
                m('span', { class: 'section-title' }, 'System'),
                m('div', { class: 'hrule' }),
                m('ul', [
                    m('li', m("a[href=https://github.com/tlacasse/Portal-Website]", 'Website Source')),
                    m('li', m("a[href=/view/api]", { oncreate: m.route.link }, 'Api Viewer')),
                ]),
            ],
            m('button', {
                class: 'icon-form-input icon-form-button', onclick: function () {
                    window.location = '/';
                }
            }, 'Exit')
        ),
    );
}
