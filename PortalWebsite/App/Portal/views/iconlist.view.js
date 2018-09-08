var IconList = {};

// Functions

IconList.gotoIcon = function (icon) {
    console.log(formatURL(icon.Name));
    m.route.set('/'); //need to leave 'edit' to refresh route parameter
    m.route.set('/edit/' + formatURL(icon.Name));
    m.redraw();
}

IconList.iconPath = function (icon) {
    return 'Portal/Icons/' + icon.Id + '.' + icon.Image;
}

IconList.getIconList = function () {
    m.request({
        method: 'GET',
        url: 'api/portal/icon/list',
    }).then(function (data) {
        IconList.iconNodes = data.map(x => IconList.iconToRow(x));
    }).catch(function (e) {
        console.log(e);
    });
};

// View Functions

IconList.iconToRow = function (icon) {
    return (
        m('tr', { class: 'icon-list-element', onclick: function () { IconList.gotoIcon(icon); } }, [
            m('td', m('div', { class: 'icon-list-image' },
                m('img', { src: IconList.iconPath(icon) })
            )),
            m('td', icon.Name),
        ])
    );
};

IconList.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'icon-list-col1' }, ' '),
            m('td', { class: 'icon-list-col2' }, ' '),
        ])
    );
}

// View

IconList.oninit = IconList.getIconList;

IconList.view = function () {
    return (
        Templates.threePane(
            m('div', { class: 'section-title'}, 'Icons'),
            m('table', { class: 'icon-list-table' }, [
                IconList.emptyRow(),
                IconList.iconNodes,
                IconList.emptyRow(),
            ]),
            ''
        )
    );
};
