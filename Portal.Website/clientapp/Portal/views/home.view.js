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
                m('li', m("a[href=/new]", { oncreate: m.route.link }, 'New Icon')),
            ]),
            m('br'),
            m('span', { class: 'section-title' }, 'System'),
            m('div', { class: 'hrule' }),
            m('ul', [
                m('li', m("a[href=https://github.com/tlacasse/Portal-Website]", 'Website Source')),
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
