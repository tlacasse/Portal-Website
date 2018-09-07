﻿var Home = {};

Home.oninit = function () {
    IconList.oninit();
};

Home.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title'}, 'Portal Configuration'),
            '', ''
        ),
    );
};
