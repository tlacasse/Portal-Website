"use strict";

var Home = {};

Home.view = function () {
    return m('div.homeblock', [
        m('m.section-title.center', 'Personal Banking'),
        m('div.hrule'),
        m('ul.center', [
            m('li', m(m.route.Link, { href: '/list/account' }, 'Accounts')),
            m('li', m(m.route.Link, { href: '/list/accounttype' }, 'AccountTypes')),
            m('li', m(m.route.Link, { href: '/list/category' }, 'Categories')),
            m('li', m(m.route.Link, { href: '/list/subcategory' }, 'Subcategories')),
        ]),
    ]);
}
