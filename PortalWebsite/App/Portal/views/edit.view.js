var Edit = {};

// Functions

Edit.getSource = function (vnode) {
    Edit.isNewIcon = nonexistant(vnode.attrs.name);
    if (Edit.isNewIcon) {
        Edit.icon = {
            Id: -1,
            Name: 'New Icon',
            Image: 'png',
            Link: '',
        }
    } else {
        Edit.source = vnode.attrs.name;
        Edit.icon = {
            Name: unFormatURL(Edit.source)
        };
    }
};

// View

Edit.oninit = function (vnode) {
    IconList.oninit();
    Edit.getSource(vnode);
};

Edit.onupdate = function (vnode) {
    Edit.getSource(vnode);
};

Edit.view = function () {
    return Templates.splitContent(
        IconList.view(),
        Templates.threePane(
            m('div', { class: 'section-title' }, Edit.icon.Name),
            '', ''
        ),
    );
};
