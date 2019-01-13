/**
 * Navigation Icon List populating the left content of most pages.
 */
var IconList = (function () {
    "use strict";
    var vm = {};

    // source list of icon models
    vm.sourceList = [];

    // if this is on the Grid Configuration page
    vm.isGridList = false;

    function iconNameCompare(a, b) {
        return a.Name.localeCompare(b.Name);
    }

    function getIconList() {
        m.request({
            method: 'GET',
            url: '/api/portal/icon/list',
        }).then(function (data) {
            vm.sourceList = data;
            if (vm.isGridList) {
                // empty icon for erase
                vm.sourceList.push({ Name: '' });
            }
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    function iconOnClick(icon) {
        if (vm.isGridList) {
            Grid.setActiveIcon(icon);
            m.redraw();
        } else {
            gotoIcon(icon);
        }
    }

    function gotoIcon(icon) {
        m.route.set('/edit/' + formatURL(icon.Name));
    }

    // mithril oninit
    function oninit() {
        vm.isGridList = (m.route.get().indexOf('grid') >= 0);
        getIconList();
    }

    ////////////////////// View

    function emptyRow() {
        return (
            m('tr', [
                m('td', { class: 'icon-list-col1' }, ' '),
                m('td', { class: 'icon-list-col2' }, ' '),
            ])
        );
    }

    function prepIconData() {
        return {
            onclick: null,
            imagePath: null,
            name: null,
            classes: 'icon-list-element',
        };
    }

    function prepEmptyIcon() {
        var iconRow = prepIconData();
        iconRow.onclick = function () { iconOnClick(null); };
        iconRow.imagePath = iconImagePath(emptyIcon());
        iconRow.name = '(Erase)';
        if (vm.isGridList && Grid.getActiveIcon() == null) {
            iconRow.classes += ' icon-list-grid-selected'
        }
        return iconRow;
    }

    function prepIcon(icon) {
        var iconRow = prepIconData();
        iconRow.onclick = function () { iconOnClick(icon); };
        iconRow.imagePath = iconImagePath(icon);
        iconRow.name = icon.Name;
        if (vm.isGridList && Grid.getActiveIcon() != null && Grid.getActiveIcon().Id == icon.Id) {
            iconRow.classes += ' icon-list-grid-selected'
        }
        return iconRow;
    }

    function iconToRow(icon) {
        var iconRow = icon.Name === '' ? prepEmptyIcon() : prepIcon(icon);
        return (
            m('tr', {
                class: iconRow.classes,
                onclick: iconRow.onclick,
            }, [
                    m('td', m('div', { class: 'icon-list-image' },
                        m('img', { src: iconRow.imagePath })
                    )),
                    m('td', iconRow.name),
                ])
        );
    }

    function view() {
        return (
            Templates.threePane(
                m('div', { class: 'section-title' }, 'Icons'),
                m('table', { class: 'icon-list-table' }, [
                    emptyRow(),
                    vm.sourceList
                        .sort(iconNameCompare)
                        .map(x => iconToRow(x)),
                    emptyRow(),
                ]),
                ''
            )
        );
    }

    return {
        oninit: oninit,
        view: view,
        gotoIcon: gotoIcon,
        isGridList: function () {
            return vm.isGridList;
        },
        private: function () {
            return vm;
        },
    };
})();
