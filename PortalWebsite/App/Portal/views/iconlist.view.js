/**
 * Navigation Icon List populating the left content of most pages.
 */
var IconList = {};

/**
 * Table rows of the Icon List.
 */
IconList.iconNodes = [];

// Functions

/**
 * Go to the Edit view of an Icon.
 * @param {String} icon
 */
IconList.gotoIcon = function (icon) {
    m.route.set('/edit/' + formatURL(icon.Name));
}

/**
 * Path to the Icon Image.
 * @param {String} icon
 */
IconList.iconPath = function (icon) {
    return 'Portal/Icons/' + icon.Id + '.' + icon.Image;
}

/**
 * Icon sorting comparison function.
 * @param {Icon} a
 * @param {Icon} b
 */
IconList.iconNameCompare = function (a, b) {
    return a.Name.localeCompare(b.Name);
}

/**
 * Retrieves the Icon List from the server.
 */
IconList.getIconList = function () {
    m.request({
        method: 'GET',
        url: 'api/portal/icon/list',
    }).then(function (data) {
        IconList.iconNodes = data
            .sort(IconList.iconNameCompare)
            .map(x => IconList.iconToRow(x));
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

// View Functions

/**
 * Formats an Icon into a row on the list.
 * @param {Icon} icon
 */
IconList.iconToRow = function (icon) {
    return (
        m('tr', { class: 'icon-list-element', onclick: function () { IconList.gotoIcon(icon); } }, [
            m('td', m('div', { class: 'icon-list-image' },
                m('img', { src: IconList.iconPath(icon) + '?d=' + icon.DateChanged })
            )),
            m('td', icon.Name),
        ])
    );
}

/**
 * A empty row on the Icon List, defines formatting.
 */
IconList.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'icon-list-col1' }, ' '),
            m('td', { class: 'icon-list-col2' }, ' '),
        ])
    );
}

// View

/**
 * Mithril oninit.
 */
IconList.oninit = IconList.getIconList;

/**
 * Mithril view.
 */
IconList.view = function () {
    return (
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Icons'),
            m('table', { class: 'icon-list-table' }, [
                IconList.emptyRow(),
                IconList.iconNodes,
                IconList.emptyRow(),
            ]),
            ''
        )
    );
}
