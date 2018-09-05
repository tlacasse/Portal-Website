var gulp = require('gulp');

var cleanCSS = require("gulp-clean-css");
var concat = require("gulp-concat");
var inject = require("gulp-inject");
var minify = require("gulp-minify");
var noop = require("gulp-noop");
var rename = require("gulp-rename");
var sass = require("gulp-sass");
var sort = require("gulp-sort");

var minimist = require("minimist");
var del = require('del');

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

function sortComparator(file1, file2) {
    var diff;

    diff = file2.path.indexOf('.template') - file1.path.indexOf('.template');
    if (diff !== 0) return diff;

    diff = file2.path.indexOf('view.') - file1.path.indexOf('view.');
    if (diff !== 0) return diff;

    diff = file2.path.indexOf('app.') - file1.path.indexOf('app.');
    if (diff !== 0) return diff;

    return 0;
}

///////////////////////////////////////////////////////////////
// Tasks

gulp.task('clean', function () {
    return del([
        build + '/styles/**/*.css',
        build + '/app/**/*.js',
    ]);
});

gulp.task('app', function () {
    return gulp.src('App/**/*.js')
        .pipe(sort({ comparator: sortComparator }))
        .pipe(isProduction() ? concat('app.js') : noop())
        .pipe(isProduction() ? minify() : noop())
        .pipe(rename(function (path) {
            path.basename += dateAppend;
        }))
        .pipe(isProduction() ? rename(function (path) { path.basename += '.min'; }) : noop())
        .pipe(gulp.dest(build + '/app'));
});

gulp.task('mithril', function () {
    return gulp.src(isProduction() ? 'Content/js/mithril.min.js' : 'Content/js/mithril.js')
        .pipe(gulp.dest(build + '/mithril'));
});

gulp.task('sass', function () {
    return gulp.src('Content/sass/**/*.scss')
        .pipe(sass())
        .pipe(isProduction() ? concat('style.css') : noop())
        .pipe(isProduction() ? cleanCSS() : noop())
        .pipe(rename(function (path) {
            path.basename += dateAppend;
        }))
        .pipe(isProduction() ? rename(function (path) { path.basename += '.min'; }) : noop())
        .pipe(gulp.dest(build + '/styles'));
});

gulp.task('inject', function () {
    return gulp.src(build + '/index.html')
        .pipe(inject(
            gulp.src(build + '/mithril/*.js'),
            { relative: true, name: 'framework' }
        ))
        .pipe(inject(
            gulp.src(build + '/app/**/*.js').pipe(sort({ comparator: sortComparator })),
            { relative: true, name: 'app' })
        )
        .pipe(inject(
            gulp.src(build + '/styles/**/*.css', { read: false }),
            { relative: true })
        )
        .pipe(gulp.dest(build));
});

gulp.task('default', gulp.series('clean', 'app', 'mithril', 'sass', 'inject'));
gulp.task('quick', gulp.series('clean', 'app', 'sass', 'inject'));
