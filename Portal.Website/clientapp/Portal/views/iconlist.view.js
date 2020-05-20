"use strict";
var IconList = {};

// list of icon models
IconList.sourceList = [];

// if this is on the Grid Configuration page
IconList.isGridList = false;

IconList.oninit = function () {
    IconList.getIconList();
}

IconList.getIconList = function () {
    API.get('portal/icon/list', function (data) {
        IconList.sourceList = data;
    });
}

IconList.iconNameCompare = function (a, b) {
    return a.Name.localeCompare(b.Name);
}

IconList.gotoIcon = function (icon) {
    m.route.set('/edit/' + formatURL(icon.Name));
}

IconList.emptyRow = function () {
    return (
        m('tr', [
            m('td.icon-list-col1', ' '),
            m('td.icon-list-col2', ' '),
        ])
    );
}

IconList.prepIconData = function () {
    return {
        onclick: null,
        imagePath: null,
        name: null,
        classes: 'icon-list-element',
    };
}

IconList.prepIcon = function (icon) {
    var iconRow = IconList.prepIconData();
    iconRow.onclick = function () { IconList.gotoIcon(icon); };
    iconRow.imagePath = iconImagePath(icon);
    iconRow.name = icon.Name;
    return iconRow;
}

IconList.iconToRow = function (icon) {
    var iconRow = IconList.prepIcon(icon);
    return (
        m('tr', {
            class: iconRow.classes,
            onclick: iconRow.onclick,
        }, [
                m('td', m('div.icon-list-image',
                    m('img', { src: iconRow.imagePath })
                )),
                m('td', iconRow.name),
            ])
    );
}

IconList.view = function () {
    return (
        Templates.threePane(
            m('div.header-title', 'Icons'),
            m('table.icon-list-table', [
                IconList.emptyRow(),
                IconList.sourceList
                    .sort(IconList.iconNameCompare)
                    .map(x => IconList.iconToRow(x)),
                IconList.emptyRow(),
            ]),
            ''
        )
    );
}
