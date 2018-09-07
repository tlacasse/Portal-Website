var IconList = {};

IconList.iconPath = function (icon) {
    return 'Portal/Icons/' + icon.Id + '.' + icon.Image;
}

IconList.iconToRow = function (icon) {
    return (
        m('tr', { class: 'icon-list-element' }, [
            m('td', m('div', { class: 'icon-list-image' },
                m('img', { src: IconList.iconPath(icon) })
            )),
            m('td', icon.Name),
        ])
    );
};

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

IconList.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'icon-list-col1' }, ' '),
            m('td', { class: 'icon-list-col2' }, ' '),
        ])
    );
}

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
