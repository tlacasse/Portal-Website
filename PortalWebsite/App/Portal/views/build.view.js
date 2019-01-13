/**
 * Build Grid screen. Displays new changes.
 */
var Build = (function () {
    "use strict";
    var vm = {};

    // DateTime string when last build was run
    vm.lastBuildDate = '';

    // list of changes since the last build
    vm.gridChanges = [];

    function getBuildData() {
        m.request({
            method: 'GET',
            url: '/api/portal/build/lasttime',
        }).then(function (data) {
            vm.lastBuildDate = data;
            m.request({
                method: 'GET',
                url: '/api/portal/build/changes',
            }).then(function (data) {
                vm.gridChanges = data;
            }).catch(function (e) {
                ErrorMessage.show(e);
            });
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    function submitBuild() {
        m.request({
            method: 'POST',
            url: '/api/portal/build/build',
        }).then(function (data) {
            Home.goto();
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    // mithril oninit
    function oninit() {
        IconList.oninit();
        getBuildData();
    }

    ////////////////////// View

    function changeToRow(change) {
        return (
            m('tr', [
                m('td', { class: 'grid-changes-table-col1' }, change.DateTime),
                m('td', { class: 'grid-changes-table-col2' }, change.Event),
            ])
        );
    }

    function view() {
        return Templates.splitContent(
            IconList.view(),
            Templates.threePane(
                m('div', { class: 'section-title' }, 'Build Grid'),
                [
                    m('span', { class: 'section-title' }, 'Last Build Date: ' + vm.lastBuildDate),
                    m('div', { class: 'hrule' }),
                    m('br'),
                    m('div', { class: 'grid-changes-container' },
                        m('table', { class: 'grid-changes-table' },
                            vm.gridChanges
                                .map(x => changeToRow(x))
                        )
                    ),
                    m('br'),
                    m('button', { class: 'icon-form-input icon-form-button', onclick: submitBuild }, 'Build Icon Grid')
                ],
                m('button', { class: 'icon-form-input icon-form-button', onclick: Home.goto }, 'Back')
            ),
        );
    }

    return {
        oninit: oninit,
        view: view,
        submitBuild: submitBuild,
        private: function () {
            return vm;
        },
    };
})();
