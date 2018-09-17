/**
 * View for setting Icons on the Grid.
 */
var Grid = {};

/**
 * Number of Icons horizontally.
 */
Grid.width = 0;
/**
 * Number of Icons vertically.
 */
Grid.height = 0;

/**
 * Stores a 2d array with the dimensions of the grid, where each element has the Id of an Icon and the Image extension.
 */
Grid.grid = [[]];

// Functions

/**
 * Fetches the Grid dimensions from the JSON file on the Server.
 */
Grid.getDimensions = function () {
    m.request({
        method: 'GET',
        url: 'api/portal/grid/size',
    }).then(function (data) {
        Grid.width = data.Width;
        Grid.height = data.Height;
        Grid.grid = Grid.getArray2d(Grid.width, Grid.height, emptyIcon);
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

/**
 * Creates a 2d array filled with the specified value.
 * @param {Number} w
 * @param {Number} h
 * @param {any} value
 */
Grid.getArray2d = function (w, h, value) {
    var a = [];
    for (var i = 0; i < w; i++) {
        var b = [];
        for (var j = 0; j < h; j++) {
            b.push((typeof value === 'function') ? value() : value);
        }
        a.push(b);
    }
    return a;
}

// View Functions

/**
 * Builds the table for the grid.
 */
Grid.buildGrid = function () {
    var rows = [];
    var widthCSS = 'width:' + String(100 / Grid.width) + '%;';
    var heightCSS = 'height:' + String(100 / Grid.width) + '%;';
    for (var j = 0; j < Grid.height; j++) {
        var cells = [];
        for (var i = 0; i < Grid.width; i++) {
            cells.push(
                m('td', {
                    class: 'icon-grid-element',
                    style: widthCSS,
                },
                    m('img', {
                        class: 'icon-grid-image',
                        src: iconImagePath(Grid.grid[i][j]),
                    })
                )
            );
        }
        rows.push(m('tr', {
            class: 'icon-grid-row',
            style: heightCSS,
        }, cells));
    }
    return rows;
}

// View

/**
 * Mithril oninit.
 */
Grid.oninit = function () {
    IconList.oninit();
    Grid.getDimensions();
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
            ''
        ),
    );
}
