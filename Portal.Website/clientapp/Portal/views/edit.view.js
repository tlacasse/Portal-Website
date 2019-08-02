/**
 * Updating an Icon, or adding a new one.
 */
var Edit = (function () {
    "use strict";
    var vm = {};

    // if entirely new icon
    vm.isNewIcon = false;

    // Icon view model
    vm.iconModel = emptyIcon();

    // name of attached image file
    vm.imageFileName = '';

    // stores the route, to diff and signal a page refresh
    vm.routeSave = '';

    // unique string to prevent input caching.
    vm.unique = String(new Date()) + String(Math.random());

    function getSourceIcon() {
        if (vm.isNewIcon === false) {
            m.request({
                method: 'GET',
                url: '/api/portal/icon/get/' + m.route.param('name'),
            }).then(function (data) {
                vm.iconModel = data;
            }).catch(function (e) {
                ErrorMessage.show(e);
            });
        } else {
            vm.iconModel = emptyIcon();
        }
    }

    function setupPage() {
        vm.isNewIcon = nonexistent(m.route.param('name'));
        vm.routeSave = m.route.get();
        getSourceIcon();
        vm.imageFileName = '';
    }

    function getFileInput() {
        var iconfileinput = get('iconFile');
        return iconfileinput == null ? null : iconfileinput.files[0];
    }

    function updateFileName() {
        var file = getFileInput();
        if (nonexistent(file) === false) {
            vm.imageFileName = file.name;
        }
    }

    function submitForm() {
        var data = new FormData();
        data.append('File', getFileInput());
        data.append('Name', vm.iconModel.Name);
        data.append('Link', vm.iconModel.Link);
        data.append('Id', vm.iconModel.Id);

        m.request({
            method: 'POST',
            url: '/api/portal/icon/post',
            body: data,
        }).then(function (data) {
            Home.goto();
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    }

    // mithril oninit
    function oninit() {
        IconList.oninit();
        setupPage();
    }

    // mithril onupdate
    function onupdate() {
        if (m.route.get() !== vm.routeSave) {
            setupPage();
        }
    }

    ////////////////////// View

    function emptyRow() {
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

    function textField(prompt, id, value, updateFunction) {
        return formRow(prompt,
            m('input', {
                type: 'text',
                id: id,
                name: id + vm.unique,
                class: 'icon-form-input',
                value: value,
                onchange: updateFunction,
            })
        );
    }

    function imageFileInputRow() {
        return formRow('Image:', [
            m('span', { class: 'icon-form-file' },
                m('label', { for: 'iconFile' }, [
                    'Select Icon File',
                    m('input', {
                        type: 'file',
                        id: 'iconFile',
                        accept: 'image/*',
                        onchange: updateFileName,
                    })
                ])
            ),
            m('span', nbsps(5) + vm.imageFileName)
        ]);
    }

    function imageDisplayRow() {
        return formRow(nbsp(),
            m('div', { class: 'icon-form-img' },
                m('img', { src: iconImagePath(vm.iconModel) })
            )
        );
    }

    function buttonsRow() {
        return formRow(nbsp(), [
            m('button', {
                class: 'icon-form-input icon-form-button icon-form-button-left',
                onclick: submitForm,
            }, 'Save'),
            m('button', {
                class: 'icon-form-input icon-form-button icon-form-button-right',
                onclick: Home.goto,
            }, 'Cancel'),
        ])
    }

    function view() {
        return Templates.splitContent(
            IconList.view(),
            Templates.threePane(
                m('div', { class: 'section-title' }, (vm.isNewIcon ? 'New Icon' : vm.iconModel.Name)),
                m('table', { class: 'icon-form-table' }, [
                    emptyRow(),
                    textField('Name:', 'iconName', vm.iconModel.Name, function (e) { vm.iconModel.Name = e.target.value; }),
                    textField('Link:', 'iconLink', vm.iconModel.Link, function (e) { vm.iconModel.Link = e.target.value; }),
                    imageFileInputRow(),
                    imageDisplayRow(),
                    emptyRow(),
                    buttonsRow(),
                ]),
                ''
            ),
        );
    }

    return {
        oninit: oninit,
        onupdate: onupdate,
        view: view,
        submitForm: submitForm,
        isNewIcon: function () {
            return vm.isNewIcon;
        },
        getIconModel: function () {
            return vm.iconModel;
        },
        setIconModel: function (x) {
            vm.iconModel = x;
        },
        private: function () {
            return vm;
        },
    };
})();
