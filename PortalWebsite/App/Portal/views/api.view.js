/**
 * View for listing all api uri's.
 */
var Api = (function () {
    "use strict";
    var vm = {};

    // api information
    vm.items = [];

    // current api item selection
    vm.active = {
        Verb: 'Post',
        Uri: '',
    }

    // if an api item has been selected
    vm.hasSelected = false;

    // api get response
    vm.response = '';

    function getApiItems() {
        m.request({
            method: 'GET',
            url: '/api/portal/api/get',
        }).then(function (data) {
            vm.items = data;
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    function getRoutingVars(apiItem) {
        var route = apiItem.Uri;
        var params = [];
        var start = -1;
        while ((start = route.indexOf('{', start + 1)) !== -1) {
            var end = route.indexOf('}', start + 1);
            params.push(route.substring(start + 1, end));
        }
        return params;
    }

    function doApiCall() {
        m.request({
            method: 'GET',
            url: '/' + vm.active.Uri,
        }).then(function (data) {
            vm.response = JSON.stringify(data, undefined, 4);
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    function setApiView(apiItem) {
        vm.hasSelected = true;
        vm.active = apiItem;
        vm.response = '';
    }

    // mithril oninit
    function oninit() {
        getApiItems();
    }

    ////////////////////// View

    function emptyRow() {
        return (
            m('tr', [
                m('td', { class: 'api-list-col1' }, ' '),
                m('td', { class: 'api-list-col2' }, ' '),
            ])
        );
    }

    function apiItemToRow(apiItem) {
        return (
            m('tr', {
                onclick: function () { setApiView(apiItem); },
                class: 'api-list-element',
            }, [
                    m('td', apiItem.Verb.toUpperCase()),
                    m('td', apiItem.Uri.substring(4)),
                ])
        );
    }

    function formEmptyRow() {
        return (
            m('tr', [
                m('td', { class: 'icon-form-col1' }, ' '),
                m('td', { class: 'icon-form-col2' }, ' '),
            ])
        );
    }

    function formRow(prompt, field) {
        return (
            m('tr', [
                m('td', m('div', { class: 'icon-form-prompt' }, prompt)),
                m('td', field),
            ])
        );
    }

    function leftSide() {
        return Templates.threePane(
            m('div', { class: 'section-title' }, 'Api Viewer'),
            m('table', { class: 'icon-list-table' }, [
                emptyRow(),
                vm.items.map(x => apiItemToRow(x)),
                emptyRow(),
            ]),
            ''
        )
    }

    function rightSide() {
        return Templates.threePane(
            m('div', { class: 'section-title' }, vm.active.Uri),
            m('div', { class: 'api-core' },
                vm.hasSelected ? [
                    m('table', { class: 'icon-form-table api-table' }, [
                        formEmptyRow(),
                        formRow('Method:', vm.active.Verb.toUpperCase()),
                        formRow('Uri:', window.location.hostname + '/' + vm.active.Uri),
                        m('ul', getRoutingVars(vm.active).map(x => m('li', x))),
                        m('button', { class: 'icon-form-input icon-form-button', onclick: doApiCall }, 'Call API'),
                    ]),
                    m('pre', vm.response),
                ] : ''
            ),
            m('button', { class: 'icon-form-input icon-form-button', onclick: Home.goto }, 'Back')
        )
    }

    function view() {
        return Templates.splitContent(
            leftSide(), rightSide()
        );
    }

    return {
        oninit: oninit,
        view: view,
        private: function () {
            return vm;
        },
    };
})();
