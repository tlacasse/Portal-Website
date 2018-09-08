﻿var Home = {};

Home.oninit = function () {
    IconList.oninit();
};

Home.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title'}, 'Portal Configuration'),
            m('ul', [
                m('li', m("a[href=/new]", { oncreate: m.route.link }, 'New Icon'))
            ]),
            ''
        ),
    );
};
