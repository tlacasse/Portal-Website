"use strict";
var Home = {};

Home.lastbuildtime = '...';

Home.goto = function () {
    m.route.set('/');
}

Home.oninit = function () {
    IconList.oninit();
    Home.loadLastBuildTime();
}

Home.loadLastBuildTime = function () {
    API.aget('portal/grid/lasttime', function (data) {
        Home.lastbuildtime = data;
    });
}

Home.buildGrid = function () {
    API.post('portal/grid/build', function (data) {
        App.showMessage(data);
        Home.loadLastBuildTime();
    });
}

Home.middleContent = function () {
    return [
        m('span.section-title', 'Configuration'),
        m('div.hrule'),
        m('ul', [
            m('li', m(m.route.Link, { href: '/new' }, 'New Icon')),
            m('li', m(m.route.Link, { href: '/grid' }, 'Grid Configuration')),
            m('li', m('a', {
                onclick: Home.buildGrid
            }, 'Build Icon Grid (Last Build: ' + Home.lastbuildtime + ')')),
        ]),
        m('br'),
        m('span.section-title', 'System'),
        m('div.hrule'),
        m('ul', [
            m('li', m("a[href=https://github.com/tlacasse/Portal-Website]", 'Website Source')),
        ]),
    ];
}

Home.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div.header-title', 'Portal'),
            Home.middleContent(),
            m('button', {
                onclick: function () {
                    window.location = '/';
                }
            }, 'Exit'),
        ),
    );
}
