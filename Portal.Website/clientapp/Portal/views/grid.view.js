/**
 * Positioning Icons on the Grid.
 */
var Grid = (function () {
    "use strict";
    var vm = {};

    // grid dimensions information
    vm.size = null;

    // 2d array of icons
    vm.grid = [[]];

    // active icon, where clicking on the grid will put this Icon at that position
    // important for iconlist, on Grid page
    vm.activeIcon = null;

    function getGrid() {
        m.request({
            method: 'GET',
            url: '/api/portal/grid/size',
        }).then(function (data) {
            vm.size = data;
            vm.grid = getArray2d(vm.size.Width, vm.size.Height, function () { return null; });
            /*m.request({
                method: 'GET',
                url: '/api/portal/grid/get',
            }).then(function (data) {
                for (var i = 0; i < data.length; i++) {
                    var icon = data[i];
                    vm.grid[icon.XCoord][icon.YCoord] = icon;
                }
            }).catch(function (e) {
                ErrorMessage.show(e);
            });*/
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    function updateGridSize() {
        const oldWidth = vm.size.Width;
        const oldHeight = vm.size.Height;

        vm.size.Width = Number(get('gridFooterWidth').value);
        vm.size.Height = Number(get('gridFooterHeight').value);

        for (var i = 0; i < 2; i++) { //twice to catch any changes while clamping
            vm.size.Width = clamp(vm.size.Width, vm.size.Height, vm.size.Max);
            vm.size.Height = clamp(vm.size.Height, vm.size.Min, vm.size.Width);
        }

        if (oldWidth !== vm.size.Width || oldHeight !== vm.size.Height) {
            vm.grid = getArray2d(vm.size.Width, vm.size.Height,
                function (x, y) {
                    if (x >= oldWidth || y >= oldHeight) {
                        return null;
                    } else {
                        return vm.grid[x][y];
                    }
                }
            );
        }
    }

    function cellClick(x, y) {
        vm.grid[x][y] = vm.activeIcon;
        m.redraw();
    }

    function submitGrid() {
        var gridState = {};
        gridState.Size = {
            Width: vm.size.Width,
            Height: vm.size.Height,
        }
        var cells = [];
        for (var i = 0; i < vm.size.Width; i++) {
            for (var j = 0; j < vm.size.Height; j++) {
                var cell = vm.grid[i][j];
                if (cell != null) {
                    cells.push({
                        XCoord: i,
                        YCoord: j,
                        Id: cell.Id,
                        Name: cell.Name,
                        Link: cell.Link,
                    });
                }
            }
        }
        gridState.Cells = cells;
        m.request({
            method: 'POST',
            url: '/api/portal/grid/update',
            data: gridState,
        }).then(function (data) {
            Home.goto();
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    // mithril oninit
    function oninit() {
        IconList.oninit();
        vm.size = { Width: 1, Height: 1 };
        vm.grid = [[null]];
        getGrid();
        vm.activeIcon = null;
    }

    ////////////////////// View

    // must be after image-url to work
    const iconBackgroundCSS = 'background-size: contain; '
        + 'background-repeat: no-repeat; '
        + 'background-position: center; ';

    function getImageCSS(x, y) {
        return nonexistent(vm.grid[x][y])
            ? ''
            : 'background-image: url('
            + iconImagePath(vm.grid[x][y])
            + '); '
            + iconBackgroundCSS;
    }

    function buildGrid() {
        var rows = [];
        if (nonexistent(vm.size) === false) {
            for (var j = 0; j < vm.size.Height; j++) {
                var cells = [];
                for (var i = 0; i < vm.size.Width; i++) {
                    const x = i; const y = j; //needed for anonymous onclick to work.
                    cells.push(
                        m('td', {
                            class: 'icon-grid-element',
                            style: getImageCSS(i, j),
                        }, m('div', { onclick: function () { cellClick(x, y); } }, ''))
                    );
                }
                rows.push(m('tr', { class: 'icon-grid-row' }, cells));
            }
        }
        return rows;
    }

    function getSizeInput(value, id) {
        return (
            m('input[type=number]', {
                class: 'grid-footer-input icon-form-input',
                value: value,
                id: id,
                onchange: updateGridSize,
                style: 'width: 5%; float: left;',
            })
        );
    }

    function getButton(name, onclick) {
        return (
            m('button', {
                class: 'icon-form-input icon-form-button grid-footer-button',
                onclick: onclick,
            }, name)
        );
    }

    function view() {
        return Templates.splitContent(
            IconList.view(),
            Templates.threePane(
                m('div', { class: 'section-title' }, 'Grid Configuration'),
                m('table', { class: 'icon-grid-table' }, buildGrid()),
                [
                    m('span', { style: 'float: left;' }, nbsps(5)),
                    getSizeInput(vm.size.Width, 'gridFooterWidth'),
                    m('div', { class: 'grid-footer-x' }, nbsps(2) + 'X' + nbsps(2)),
                    getSizeInput(vm.size.Height, 'gridFooterHeight'),

                    m('span', { style: 'float: right;' }, nbsps(5)),
                    getButton('Cancel', Home.goto),
                    m('span', { style: 'float: right;' }, nbsps(5)),
                    getButton('Save', submitGrid),
                    m('span', { style: 'float: right;' }, nbsps(5)),
                ]
            ),
        );
    }

    return {
        oninit: oninit,
        view: view,
        cellClick: cellClick,
        submitGrid: submitGrid,
        getActiveIcon: function () {
            return vm.activeIcon;
        },
        setActiveIcon: function (x) {
            vm.activeIcon = x;
        },
        getWidth: function () {
            return vm.size.Width;
        },
        getHeight: function () {
            return vm.size.Height;
        },
        private: function () {
            return vm;
        },
    };
})();
