var Edit = {};

Edit.isNewIcon = true;
Edit.pathSave = '';

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
    if (Edit.isNewIcon) {
        Edit.icon = {
            Id: -1,
            Name: 'New Icon',
            Image: 'png',
            Link: '',
            DateChanged: '',
        }
    } else {
        m.request({
            method: 'GET',
            url: 'api/portal/icon/get/' + m.route.param('name'),
        }).then(function (data) {
            Edit.icon = data;
        }).catch(function (e) {
            console.log(e);
        });
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
            m('div', { class: 'section-title' }, Edit.icon.Name),
            m('ul', [
                m('li', Edit.icon.Name),
                m('li', Edit.icon.Id),
                m('li', Edit.icon.Link),
                m('li', Edit.icon.Image),
            ]),
            ''
        ),
    );
}
