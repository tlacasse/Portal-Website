/**
 * Build Grid screen.
 */
var Build = {};

/**
 * DateTime when last build was run.
 */
Build.lastBuildDate = '';

/**
 * List of changes since the last build.
 */
Build.gridChanges = [];

// Functions

/**
 * Retrieves any build information.
 */
Build.getBuildData = function () {
    m.request({
        method: 'GET',
        url: '/api/portal/build/lasttime',
    }).then(function (data) {
        Build.lastBuildDate = data;
        m.request({
            method: 'GET',
            url: '/api/portal/build/changes',
        }).then(function (data) {
            Build.gridChanges = data;
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

/**
 * Tell the server to build the grid.
 */
Build.buildGrid = function () {
    m.request({
        method: 'POST',
        url: '/api/portal/build/build',
    }).then(function (data) {
        Home.goto();
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

// View Functions

/**
 * Convert a GridChangeItem into a table row.
 * @param {GridChangeItem} change
 */
Build.changeToRow = function (change) {
    return (
        m('tr', [
            m('td', { class: 'grid-changes-table-col1' }, change.DateTime),
            m('td', { class: 'grid-changes-table-col2' }, change.Event),
        ])
    );
}

// View

/**
 * Mithril oninit.
 */
Build.oninit = function () {
    IconList.oninit();
    Build.getBuildData();
}

/**
 * Mithril view.
 */
Build.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Build Grid'),
            [
                m('span', { class: 'section-title' }, 'Last Build Date: ' + Build.lastBuildDate),
                m('div', { class: 'hrule' }),
                m('br'),
                m('div', { class: 'grid-changes-container' },
                    m('table', { class: 'grid-changes-table' },
                        Build.gridChanges
                            .map(x => Build.changeToRow(x))
                    )
                ),
                m('br'),
                m('button', { class: 'icon-form-input icon-form-button', onclick: Build.buildGrid }, 'Build Icon Grid')
            ],
            m('button', { class: 'icon-form-input icon-form-button', onclick: Home.goto }, 'Back')
        ),
    );
}
