﻿var Home = {};

Home.view = function () {
    return Templates.SplitContent(
        Templates.ThreePane('a1', 'a2', 'a3'),
        Templates.ThreePane('b1', 'b2', 'b3'),
    );
};
