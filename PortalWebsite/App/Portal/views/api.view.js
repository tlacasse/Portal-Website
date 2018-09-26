/**
 * View for listing all api uri's.
 */
var Api = {};

Api.items = [];

Api.active = {
    Verb: 'Post',
    Uri: '',
}

Api.hasSelected = false;

Api.response = '';

// Functions

Api.getApiItems = function () {
    m.request({
        method: 'GET',
        url: 'api/portal/api/get',
    }).then(function (data) {
        Api.items = data;
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

Api.setApiView = function (apiItem) {
    Api.hasSelected = true;
    Api.active = apiItem;
    Api.response = '';
}

Api.getRoutingVars = function (apiItem) {
    var route = apiItem.Uri;
    var params = [];
    var start = -1;
    while ((start = route.indexOf('{', start + 1)) !== -1) {
        var end = route.indexOf('}', start + 1);
        params.push(route.substring(start + 1, end));
    }
    return params;
}

Api.doApiCall = function () {
    m.request({
        method: 'GET',
        url: 'api/' + Api.active.Uri,
    }).then(function (data) {
        Api.response = JSON.stringify(data, undefined, 4);
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

// View Functions

/**
 * A empty row on the Api list, defines formatting.
 */
Api.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'api-list-col1' }, ' '),
            m('td', { class: 'api-list-col2' }, ' '),
        ])
    );
}

Api.apiItemToRow = function (apiItem) {
    return (
        m('tr', {
            onclick: function () { Api.setApiView(apiItem); },
            class: 'api-list-element',
        }, [
                m('td', apiItem.Verb.toUpperCase()),
                m('td', apiItem.Uri),
            ])
    );
}

// View 

/**
 * Mithril oninit.
 */
Api.oninit = function () {
    Api.getApiItems();
}

/**
 * Mithril view.
 */
Api.view = function () {
    return Templates.splitContent(
        Templates.threePane(
            m('div', { class: 'section-title' }, 'Api Viewer'),
            m('table', { class: 'icon-list-table' }, [
                Api.emptyRow(),
                Api.items.map(x => Api.apiItemToRow(x)),
                Api.emptyRow(),
            ]),
            ''
        ),
        Templates.threePane(
            m('div', { class: 'section-title' }, 'api/' + Api.active.Uri),
            m('div', { class: 'api-core' },
                Api.hasSelected ? [
                    m('table', { class: 'icon-form-table api-table' }, [
                        Edit.emptyRow(),
                        Edit.formRow('Method:', Api.active.Verb.toUpperCase()),
                        Edit.formRow('Uri:', window.location.hostname + '/api/' + Api.active.Uri),
                        m('ul', Api.getRoutingVars(Api.active).map(x => m('li', x))),
                        m('button', { class: 'icon-form-input icon-form-button', onclick: Api.doApiCall }, 'Call API'),
                    ]),
                    m('pre', Api.response),
                ] : ''
            ),
            m('button', { class: 'icon-form-input icon-form-button', onclick: Home.goto }, 'Back')
        ),
    );
}
