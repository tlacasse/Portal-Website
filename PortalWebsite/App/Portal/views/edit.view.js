/**
 * View for updating an Icon, or adding a new one.
 */
var Edit = {};

/**
 * Whether or not this is an entirely new icon.
 */
Edit.isNewIcon = true;

/**
 * String storing the route path to diff and signal a refresh of the page.
 */
Edit.pathSave = '';

/**
 * String storing the name of the attached Image file.
 */
Edit.fileName = '';

/**
 * Unique string to prevent Input caching.
 */
Edit.unique = String(new Date()) + String(Math.random());

/**
 * Client side Icon Model storage.
 */
Edit.icon = emptyIcon();

// Functions

/**
 * Based on the route parameter or not, fetches the existing Icon from the server, or use a empty new one.
 */
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
        Edit.icon = emptyIcon();
    }
}

/**
 * Gets the attached Image files.
 */
Edit.getFileInput = function () {
    var iconfileinput = document.getElementById('iconFile');
    return iconfileinput == null ? null : iconfileinput.files[0];
}

/**
 * Updates the visual file name.
 */
Edit.updateFileName = function () {
    var file = Edit.getFileInput();
    if (nonexistant(file) === false) {
        Edit.fileName = file.name;
    }
}
/**
 * Sends the updated Icon info to the server.
 */
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

/**
 * Empty row in the form, specifies formatting.
 */
Edit.emptyRow = function () {
    return (
        m('tr', [
            m('td', { class: 'icon-form-col1' }, ' '),
            m('td', { class: 'icon-form-col2' }, ' '),
        ])
    );
}

/**
 * A label-input row of the form.
 * @param {Component} prompt
 * @param {Component} field 
 */
Edit.formRow = function (prompt, field) {
    return (
        m('tr', [
            m('td', m('div', { class: 'icon-form-prompt' }, prompt)),
            m('td', field),
        ])
    );
}

/**
 * A label-input row of the form specifically for a text field.
 * @param {any} prompt
 * @param {any} id
 * @param {any} value
 * @param {any} updateFunction
 */
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

// View

/**
 * Mithril oninit.
 */
Edit.oninit = function () {
    IconList.oninit();
    Edit.getSource();
}

/**
 * Mithril onupdate.
 */
Edit.onupdate = function () {
    if (m.route.get() !== Edit.pathSave) {
        Edit.getSource();
    }
}

/**
 * Mithril view.
 */
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
                        m('img', { src: iconImagePath(Edit.icon) })
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
