var Edit = {};

Edit.isNewIcon = true;
Edit.pathSave = '';
Edit.fileName = '';
Edit.unique = String(new Date()) + String(Math.random());

Edit.icon = {
    Id: -1,
    Name: '',
    Image: 'png',
    Link: '',
    DateChanged: '',
}

// Functions

Edit.getSource = function () {
    Edit.isNewIcon = nonexistant(m.route.param('name'));
    Edit.pathSave = m.route.get();
    if (Edit.isNewIcon === false) {
        m.request({
            method: 'GET',
            url: 'api/portal/icon/get/' + m.route.param('name'),
        }).then(function (data) {
            Edit.icon = data;
        }).catch(function (e) {
            ErrorMessage.show(e);
        });
    } else {
        Edit.icon = {
            Id: -1,
            Name: '',
            Image: 'png',
            Link: '',
            DateChanged: '',
        }
    }
}

Edit.getFileInput = function () {
    var iconfileinput = document.getElementById('iconFile');
    return iconfileinput == null ? null : iconfileinput.files[0];
}

Edit.formSubmit = function () {
    var data = new FormData();
    data.append('File', Edit.getFileInput());
    data.append('Name', Edit.icon.Name);
    data.append('Link', Edit.icon.Link);
    data.append('Id', Edit.icon.Id);

    m.request({
        method: 'POST',
        url: 'api/portal/icon/post',
        data: data,
    }).then(function (data) {
        Home.goto();
    }).catch(function (e) {
        ErrorMessage.show(e);
    });
}

// View Functions

Edit.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'icon-form-col1' }, ' '),
            m('td', { class: 'icon-form-col2' }, ' '),
        ])
    );
}

Edit.formRow = function (prompt, field) {
    return (
        m('tr', [
            m('td', m('div', { class: 'icon-form-prompt' }, prompt)),
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
            class: 'icon-form-input',
            value: value,
            onchange: updateFunction,
        })
    );
}

Edit.updateFileName = function () {
    var file = Edit.getFileInput();
    if (nonexistant(file) === false) {
        Edit.fileName = file.name;
    }
}

// View

Edit.oninit = function () {
    IconList.oninit();
    Edit.getSource();
}

Edit.onupdate = function () {
    if (m.route.get() !== Edit.pathSave) {
        Edit.getSource();
    }
}

Edit.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title' }, (Edit.isNewIcon ? 'New Icon' : Edit.icon.Name)),
            m('table', { class: 'icon-form-table' }, [
                Edit.emptyRow(),
                Edit.textField('Name:', 'iconName', Edit.icon.Name, function (e) { Edit.icon.Name = e.target.value; }),
                Edit.textField('Link:', 'iconLink', Edit.icon.Link, function (e) { Edit.icon.Link = e.target.value; }),
                Edit.formRow('Image', [
                    m('span', { class: 'icon-form-file' },
                        m('label', { for: 'iconFile' }, [
                            'Select Icon File',
                            m('input', {
                                type: 'file',
                                id: 'iconFile',
                                accept: 'image/*',
                                onchange: Edit.updateFileName,
                            })
                        ])
                    ),
                    m('span', nbsps(5) + Edit.fileName)
                ]),
                Edit.formRow(nbsp(),
                    m('div', { class: 'icon-form-img' },
                        m('img', { src: IconList.iconPath(Edit.icon) + '?d=' + Edit.icon.DateChanged })
                    )
                ),
                Edit.emptyRow(),
                Edit.formRow(nbsp(), [
                    m('button', {
                        class: 'icon-form-input icon-form-button icon-form-button-left',
                        onclick: Edit.formSubmit,
                    }, 'Save'),
                    m('button', {
                        class: 'icon-form-input icon-form-button icon-form-button-right',
                        onclick: Home.goto,
                    }, 'Cancel'),
                ])
            ]),
            ''
        ),
    );
}
