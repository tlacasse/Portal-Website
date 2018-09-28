/**
 * View for setting Icons on the Grid.
 */
var Grid = {};

/**
 * Client side GridSize model storage.
 */
Grid.size = {};

/**
 * Stores a 2d array with the dimensions of the grid, where each element has the Id of an Icon and the Image extension.
 */
Grid.grid = [[]];

/**
 * Active Icon, where clicking on the grid will put this Icon at that position. !!! Used across Grid and IconList !!!
 */
Grid.activeIcon = null;

/**
 * CSS for all the icons on the grid. Must be after image-url to work.
 */
Grid.iconBackgroundCSS = 'background-size: contain; '
    + 'background-repeat: no-repeat; '
    + 'background-position: center; ';

// Functions

/**
 * Fetches the Grid dimensions from the JSON file on the Server.
 */
Grid.getGrid = function () {
    m.request({
        method: 'GET',
        url: '/api/portal/grid/size/get',
    }).then(function (data) {
        Grid.size = data;
        Grid.grid = Grid.getArray2d(Grid.size.Width, Grid.size.Height, function () { return null; });
        m.request({
            method: 'GET',
            url: '/api/portal/grid/get',
        }).then(function (data) {
            for (var i = 0; i < data.length; i++) {
                var icon = data[i];
                Grid.grid[icon.XCoord][icon.YCoord] = icon;
            }
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

/**
 * Creates a 2d array filled with the specified value.
 * @param {Number} w
 * @param {Number} h
 * @param {Function} value (x,y)
 */
Grid.getArray2d = function (w, h, value) {
    var a = [];
    for (var i = 0; i < w; i++) {
        var b = [];
        for (var j = 0; j < h; j++) {
            b.push(value(i, j));
        }
        a.push(b);
    }
    return a;
}

/**
 * Checks the dimensions, and if allowed, resizes the grid.
 */
Grid.updateGrid = function () {
    const oldWidth = Grid.size.Width;
    const oldHeight = Grid.size.Height;

    var widthInput = get('gridFooterWidth');
    var heightInput = get('gridFooterHeight');
    Grid.size.Width = Number(widthInput.value);
    Grid.size.Height = Number(heightInput.value);

    for (var i = 0; i < 2; i++) { //twice to catch any changes while clamping
        Grid.size.Width = clamp(Grid.size.Width, Grid.size.Height, Grid.size.Max);
        Grid.size.Height = clamp(Grid.size.Height, Grid.size.Min, Grid.size.Width);
    }

    if (oldWidth !== Grid.size.Width || oldHeight !== Grid.size.Height) {
        Grid.grid = Grid.getArray2d(Grid.size.Width, Grid.size.Height,
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

/**
 * Updates the stored grid to have the active Icon on the clicked space.
 * @param {Number} x
 * @param {Number} y
 */
Grid.cellOnClick = function (x, y) {
    Grid.grid[x][y] = Grid.activeIcon;
    m.redraw();
}

/**
 * Sends the current Grid state (both size and icons) to the server.
 */
Grid.postGridUpdate = function () {
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

// View Functions

/**
 * Builds the table for the grid.
 */
Grid.buildGrid = function () {
    function getImageUrl(x, y) {
        return nonexistant(Grid.grid[x][y])
            ? ''
            : 'background-image: url('
            + iconImagePath(Grid.grid[x][y])
            + '); '
            + Grid.iconBackgroundCSS;
    }
    var rows = [];
    for (var j = 0; j < Grid.size.Height; j++) {
        var cells = [];
        for (var i = 0; i < Grid.size.Width; i++) {
            const x = i; const y = j; //needed for anon onclick to work.
            cells.push(
                m('td', {
                    class: 'icon-grid-element',
                    style: getImageUrl(i, j),
                }, m('div', { onclick: function () { Grid.cellOnClick(x, y); } }, ''))
            );
        }
        rows.push(m('tr', { class: 'icon-grid-row' }, cells));
    }
    return rows;
}

/**
 * Builds an input element for adjusting the Grid dimensions.
 * @param {Number} value
 * @param {Number} min
 * @param {Number} max
 * @param {String} id
 */
Grid.getSizeInput = function (value, id) {
    return (
        m('input[type=number]', {
            class: 'grid-footer-input icon-form-input',
            value: value,
            id: id,
            onchange: Grid.updateGrid,
            style: 'width: 5%; float: left;',
        })
    );
}

/**
 * Returns a button.
 * @param {String} name
 * @param {Function} onclick
 */
Grid.getButton = function (name, onclick) {
    return (
        m('button', {
            class: 'icon-form-input icon-form-button grid-footer-button',
            onclick: onclick,
        }, name)
    );
}

// View

/**
 * Mithril oninit.
 */
Grid.oninit = function () {
    IconList.oninit();
    Grid.getGrid();
    Grid.activeIcon = null;
}

/**
 * Mithril view.
 */
Grid.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Grid Configuration'),
            m('table', { class: 'icon-grid-table' }, Grid.buildGrid()),
            [
                m('span', { style: 'float: left;' }, nbsps(5)),
                Grid.getSizeInput(Grid.size.Width, 'gridFooterWidth'),
                m('div', { class: 'grid-footer-x' }, nbsps(2) + 'X' + nbsps(2)),
                Grid.getSizeInput(Grid.size.Height, 'gridFooterHeight'),

                m('span', { style: 'float: right;' }, nbsps(5)),
                Grid.getButton('Cancel', Home.goto),
                m('span', { style: 'float: right;' }, nbsps(5)),
                Grid.getButton('Save', Grid.postGridUpdate),
                m('span', { style: 'float: right;' }, nbsps(5)),
            ]
        ),
    );
}
