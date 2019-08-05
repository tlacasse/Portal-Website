/**
 * Portal Configuration entry screen.
 */
var Home = (function () {
    "use strict";

    function goHome() {
        m.route.set('/');
    }

    function oninit() {
        IconList.oninit();
    }

    ////////////////////// View

    function middleContent() {
        return [
            m('span', { class: 'section-title' }, 'Configuration'),
            m('div', { class: 'hrule' }),
            m('ul', [
                m('li', m(m.route.Link, { href: '/new' }, 'New Icon')),
                m('li', m(m.route.Link, { href: '/grid' }, 'Grid Configuration')),
                //m('li', m("a[href=#!/build]", { oncreate: m.route.link }, 'Build Icon Grid')),
            ]),
            m('br'),
            m('span', { class: 'section-title' }, 'System'),
            m('div', { class: 'hrule' }),
            m('ul', [
                m('li', m("a[href=https://github.com/tlacasse/Portal-Website]", 'Website Source')),
                //m('li', m("a[href=#!/view/api]", { oncreate: m.route.link }, 'Api Viewer')),
            ]),
        ];
    }

    function view() {
        return Templates.splitContent(
            IconList.view(),
            Templates.threePane(
                m('div', { class: 'section-title' }, 'Portal'),
                middleContent(),
                m('button', {
                    class: 'icon-form-input icon-form-button', onclick: function () {
                        window.location = '/';
                    }
                }, 'Exit'),
            ),
        );
    }

    var vm = {};

    vm.view = view;
    vm.oninit = oninit;
    vm.goto = goHome;

    return vm;
})();
