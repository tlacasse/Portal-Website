"use strict";

var Grid = {};

// grid dimensions information
Grid.size = null;

// 2d array of icons
Grid.grid = [[]];

// active icon, where clicking on the grid will put this Icon at that position
// important for iconlist, on Grid page
Grid.activeIcon = null;

Grid.loadGrid = function () {
    API.get('portal/grid/size', function (data) {
        Grid.size = data;
        Grid.grid = getArray2d(Grid.size.Width, Grid.size.Height, function () { return null; });
        API.get('portal/grid/get', function (data) {
            for (var i = 0; i < data.length; i++) {
                var iconpos = data[i];
                Grid.grid[iconpos.XCoord][iconpos.YCoord] = icon.Icon;
            }
        });
    });
}

Grid.updateGridSize = function () {
    const oldWidth = Grid.size.Width;
    const oldHeight = Grid.size.Height;

    Grid.size.Width = Number(App.get('gridFooterWidth').value);
    Grid.size.Height = Number(App.get('gridFooterHeight').value);

    for (var i = 0; i < 2; i++) { //twice to catch any changes while clamping
        Grid.size.Width = clamp(Grid.size.Width, Grid.size.Height, Grid.size.Max);
        Grid.size.Height = clamp(Grid.size.Height, Grid.size.Min, Grid.size.Width);
    }

    if (oldWidth !== Grid.size.Width || oldHeight !== Grid.size.Height) {
        Grid.grid = getArray2d(Grid.size.Width, Grid.size.Height,
            function (x, y) {
                if (x >= oldWidth || y >= oldHeight) {
                    return null;
                } else {
                    return Grid.grid[x][y];
                }
            }
        );
    }
}

Grid.cellClick = function (x, y) {
    Grid.grid[x][y] = Grid.activeIcon;
    m.redraw();
}

Grid.submitGrid = function () {
    var gridState = {};
    gridState.Size = {
        Width: Grid.size.Width,
        Height: Grid.size.Height,
    }
    var cells = [];
    for (var i = 0; i < Grid.size.Width; i++) {
        for (var j = 0; j < Grid.size.Height; j++) {
            var cell = Grid.grid[i][j];
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
    API.dpost('portal/grid/update', gridState, function (data) {
        App.showMessage(data);
        Home.goto();
    });
}

Grid.oninit = function () {
    IconList.oninit();
    Grid.size = { Width: 1, Height: 1 };
    Grid.grid = [[null]];
    Grid.loadGrid();
    Grid.activeIcon = null;
}

// must be after image-url to work
Grid.iconBackgroundCSS = 'background-size: contain; '
    + 'background-repeat: no-repeat; '
    + 'background-position: center; ';

Grid.getImageCSS = function (x, y) {
    return nonexistent(Grid.grid[x][y])
        ? ''
        : 'background-image: url('
        + iconImagePath(Grid.grid[x][y])
        + '); '
        + Grid.iconBackgroundCSS;
}

Grid.buildGrid = function () {
    var rows = [];
    if (nonexistent(Grid.size) === false) {
        for (var j = 0; j < Grid.size.Height; j++) {
            var cells = [];
            for (var i = 0; i < Grid.size.Width; i++) {
                const x = i; const y = j; //needed for anonymous onclick to work.
                cells.push(
                    m('td', {
                        class: 'icongrid-element',
                        style: Grid.getImageCSS(i, j),
                    }, m('div', { onclick: function () { Grid.cellClick(x, y); } }, ''))
                );
            }
            rows.push(m('tr', cells));
        }
    }
    return rows;
}

Grid.getSizeInput = function (value, id) {
    return (
        m('input[type=number]', {
            class: 'icongridfooter-input',
            value: value,
            id: id,
            onchange: Grid.updateGridSize,
        })
    );
}

Grid.getButton = function (name, onclick) {
    return (
        m('button.float-right', {
            onclick: onclick,
        }, name)
    );
}

Grid.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div.header-title', 'Grid Configuration'),
            m('table.icongrid-table', Grid.buildGrid()),
            [
                m('span.float-left', nbsps(5)),
                Grid.getSizeInput(Grid.size.Width, 'gridFooterWidth'),
                m('div.icongridfooter-x', nbsps(2) + 'X' + nbsps(2)),
                Grid.getSizeInput(Grid.size.Height, 'gridFooterHeight'),

                m('span.float-right', nbsps(5)),
                Grid.getButton('Cancel', Home.goto),
                m('span.float-right', nbsps(5)),
                Grid.getButton('Save', Grid.submitGrid),
                m('span.float-right', nbsps(5)),
            ]
        ),
    );
}
