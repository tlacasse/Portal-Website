"use strict";
var Edit = {};

// if entirely new icon
Edit.isNewIcon = false;

// Icon view model
Edit.iconModel = emptyIcon();

// name of attached image file
Edit.imageFileName = '';

// stores the route, to diff and signal a page refresh
Edit.routeSave = '';

// unique string to prevent input caching.
Edit.unique = genUnique();

Edit.getSourceIcon = function () {
    if (Edit.isNewIcon === false) {
        API.get('portal/icon/get/' + m.route.param('name'), function (data) {
            Edit.iconModel = data;
        });
    } else {
        Edit.iconModel = emptyIcon();
    }
}

Edit.setupPage = function () {
    Edit.isNewIcon = nonexistent(m.route.param('name'));
    Edit.routeSave = m.route.get();
    Edit.getSourceIcon();
    Edit.imageFileName = '';
}

Edit.getFileInput = function () {
    var iconfileinput = App.get('iconFile');
    return iconfileinput == null ? null : iconfileinput.files[0];
}

Edit.updateFileName = function () {
    var file = Edit.getFileInput();
    if (nonexistent(file) === false) {
        Edit.imageFileName = file.name;
    }
}

Edit.submitForm = function () {
    var formdata = new FormData();
    formdata.append('File', Edit.getFileInput());
    formdata.append('Name', Edit.iconModel.Name);
    formdata.append('Link', Edit.iconModel.Link);
    formdata.append('Id', Edit.iconModel.Id);

    API.bpost('portal/icon/post', formdata, function (data) {
        App.showMessage(data);
        Home.goto();
    });
}

Edit.oninit = function () {
    IconList.oninit();
    Edit.setupPage();
}

Edit.onupdate = function () {
    if (m.route.get() !== Edit.routeSave) {
        Edit.oninit();
    }
}

Edit.emptyRow = function () {
    return (
        m('tr', [
            m('td.iconform-col1', ' '),
            m('td.iconform-col2', ' '),
        ])
    );
}

Edit.formRow = function (prompt, field) {
    return (
        m('tr', [
            m('td', m('div.iconform-prompt', prompt)),
            m('td', field),
        ])
    );
}

Edit.textField = function (prompt, id, value, updateFunction) {
    return Edit.formRow(prompt,
        m('input', {
            type: 'text',
            id: id,
            name: id + Edit.unique,
            class: 'iconform-input',
            value: value,
            onchange: updateFunction,
        })
    );
}


Edit.imageFileInputRow = function () {
    return Edit.formRow('Image:', [
        m('span.iconform-file.actas-input',
            m('label', { for: 'iconFile' }, [
                'Select Icon File',
                m('input', {
                    type: 'file',
                    id: 'iconFile',
                    accept: 'image/*',
                    onchange: Edit.updateFileName,
                }),
            ])
        ),
        m('span', nbsps(5) + Edit.imageFileName)
    ]);
}

Edit.imageDisplayRow = function () {
    return Edit.formRow(nbsp(),
        m('div.iconform-img',
            m('img', { src: iconImagePath(Edit.iconModel) })
        )
    );
}

Edit.buttonsRow = function () {
    return Edit.formRow(nbsp(), [
        m('button.float-left', {
            onclick: Edit.submitForm,
        }, 'Save'),
        m('button.float-right.margin-right', {
            onclick: Home.goto,
        }, 'Cancel'),
    ])
}

Edit.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div.header-title', (Edit.isNewIcon ? 'New Icon' : Edit.iconModel.Name)),
            m('table', [
                Edit.emptyRow(),
                Edit.textField('Name:', 'iconName', Edit.iconModel.Name, function (e) { Edit.iconModel.Name = e.target.value; }),
                Edit.textField('Link:', 'iconLink', Edit.iconModel.Link, function (e) { Edit.iconModel.Link = e.target.value; }),
                Edit.imageFileInputRow(),
                Edit.imageDisplayRow(),
                Edit.emptyRow(),
                Edit.buttonsRow(),
            ]),
            ''
        ),
    );
}
