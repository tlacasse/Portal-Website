var gulp = require('gulp');

var cleanCSS = require("gulp-clean-css");
var concat = require("gulp-concat");
var inject = require("gulp-inject");
var minify = require("gulp-minify");
var noop = require("gulp-noop");
var rename = require('gulp-rename');
var sass = require("gulp-sass");
var minimist = require('minimist');

var knownOptions = {
    string: 'env',
    default: {
        env: 'dev'
    }
};

var options = minimist(process.argv.slice(2), knownOptions);

function isProduction() {
    return options.env.toUpperCase() === 'release'.toUpperCase();
}

var d = new Date();
var dateAppend = "_" + String(d.getFullYear()) + "-" + String(d.getMonth() + 1) + "-" + String(d.getDate())
    + "_" + String(d.getHours()) + "-" + String(d.getMinutes()) + "-" + String(d.getSeconds());

var build = '_Build';

///////////////////////////////////////////////////////////////
// Tasks

gulp.task('mithril', function () {
    return gulp.src(isProduction() ? 'content/js/mithril.min.js' : 'content/js/mithril.js')
        .pipe(gulp.dest(build + '/mithril'));
});

gulp.task('inject', function () {
    return gulp.src(build + '/index.html')
        .pipe(inject(
            gulp.src(build + '/mithril/*.js'),
            { relative: true, name: 'framework' }
        ))
        .pipe(gulp.dest(build));
});

gulp.task('default', function () {
    gulp.start('mithril');
    gulp.start('inject');
});
