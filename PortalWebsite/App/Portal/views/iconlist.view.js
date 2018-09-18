/**
 * Navigation Icon List populating the left content of most pages.
 */
var IconList = {};

/**
 * Source list of Icon models.
 */
IconList.source = [];

/**
 * If the IconList is on the Grid Configuration page.
 */
IconList.isGridList = false;

// Functions

/**
 * Go to the Edit view of an Icon.
 * @param {String} icon
 */
IconList.gotoIcon = function (icon) {
    m.route.set('/edit/' + formatURL(icon.Name));
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
        IconList.source = data;
        if (IconList.isGridList) {
            IconList.source.push({ Name: '' });
        }
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

/**
 * Does an action, based on page context, when clicking an Icon.
 * @param {Icon} icon
 */
IconList.iconOnClick = function (icon) {
    if (IconList.isGridList) {
        Grid.activeIcon = icon;
        m.redraw();
    } else {
        IconList.gotoIcon(icon);
    }
}

// View Functions

/**
 * Formats an Icon into a row on the list.
 * @param {Icon} icon
 */
IconList.iconToRow = function (icon) {
    var onclick, src, name, classes;
    classes = 'icon-list-element';
    if (icon.Name === '') {
        // eraser icon
        onclick = function () { IconList.iconOnClick(null); };
        src = iconImagePath(emptyIcon());
        name = '(Erase)';
        if (IconList.isGridList && Grid.activeIcon == null) {
            classes += ' icon-list-grid-selected'
        }
    } else {
        // normal icon
        onclick = function () { IconList.iconOnClick(icon); };
        src = iconImagePath(icon);
        name = icon.Name;
        if (IconList.isGridList && Grid.activeIcon != null && Grid.activeIcon.Id == icon.Id) {
            classes += ' icon-list-grid-selected'
        }
    }
    return (
        m('tr', {
            class: classes,
            onclick: onclick,
        }, [
                m('td', m('div', { class: 'icon-list-image' },
                    m('img', { src: src })
                )),
                m('td', name),
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
IconList.oninit = function () {
    IconList.isGridList = (m.route.get().indexOf('grid') >= 0);
    IconList.getIconList();
}

/**
 * Mithril view.
 */
IconList.view = function () {
    return (
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Icons'),
            m('table', { class: 'icon-list-table' }, [
                IconList.emptyRow(),
                IconList.source
                    .sort(IconList.iconNameCompare)
                    .map(x => IconList.iconToRow(x)),
                IconList.emptyRow(),
            ]),
            ''
        )
    );
}
