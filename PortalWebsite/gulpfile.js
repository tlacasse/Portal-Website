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

function isRelease() {
    return options.env.toUpperCase() === 'release'.toUpperCase();
}

var d = new Date();
var dateAppend = "_" + String(d.getFullYear()) + "-" + String(d.getMonth() + 1) + "-" + String(d.getDate())
    + "_" + String(d.getHours()) + "-" + String(d.getMinutes()) + "-" + String(d.getSeconds());

var build = '_Build';

function getDiff(file1, file2, extension) {
    return file2.path.indexOf('.' + extension) - file1.path.indexOf('.' + extension);
}

function sortComparator(file1, file2) {
    var diff;

    diff = getDiff(file1, file2, 'util');
    if (diff !== 0) return diff;

    diff = getDiff(file1, file2, 'view');
    if (diff !== 0) return diff;

    diff = getDiff(file1, file2, 'app');
    if (diff !== 0) return diff;

    return 0;
}

function fileAppend(path) {
    path.basename += dateAppend;
}

function addMinExt(path) {
    path.basename += '-min';
}

///////////////////////////////////////////////////////////////
// Tasks

function createTaskGroup(name) {

    gulp.task('clean_' + name, function () {
        return del([
            build + '/styles/' + name + '/**/*.css',
            build + '/apps/' + name + '/**/*.js',
        ]);
    });

    gulp.task('app_' + name, function () {
        return gulp.src('App/' + name + '/**/*.js')
            .pipe(sort({ comparator: sortComparator }))
            .pipe(isRelease() ? concat(name.toLowerCase() + '.js') : noop())
            .pipe(isRelease() ? minify({ noSource: true }) : noop())
            .pipe(rename(fileAppend))
            .pipe(gulp.dest(build + '/Apps' + (isRelease() ? '' : ('/' + name))));
    });

    gulp.task('sass_' + name, function () {
        return gulp.src('Content/sass/' + name + '/**/*.scss')
            .pipe(sass())
            .pipe(isRelease() ? concat(name.toLowerCase() + '.css') : noop())
            .pipe(isRelease() ? cleanCSS() : noop())
            .pipe(isRelease() ? rename(addMinExt) : noop())
            .pipe(rename(fileAppend))
            .pipe(gulp.dest(build + '/Styles' + (isRelease() ? '' : ('/' + name))));
    });

    gulp.task('inject_' + name, function () {
        return gulp.src(build + '/Views/App/' + name + '.cshtml')
            .pipe(inject(
                gulp.src(build + '/Framework/*.js', { read: false }),
                { relative: true, name: 'framework' }
            ))
            .pipe(inject(
                gulp.src(build + (isRelease()
                    ? ('/Apps/' + name.toLowerCase() + '-min' + dateAppend + '.js')
                    : ('/Apps/' + name + '/**/*.js')), { read: false })
                    .pipe(sort({ comparator: sortComparator })),
                { relative: true, name: 'app' })
            )
            .pipe(inject(
                gulp.src(build + (isRelease()
                    ? ('/Styles/' + name.toLowerCase() + '-min' + dateAppend + '.css')
                    : ('/Styles/' + name + '/**/*.css')), { read: false }),
                { relative: true })
            )
            .pipe(gulp.dest(build + '/Views/App'))
    });

    gulp.task(name, gulp.series(
        'clean_' + name,
        'app_' + name,
        'sass_' + name,
        'inject_' + name
    ));
}

gulp.task('mithril', function () {
    return gulp.src(isRelease() ? 'Content/js/mithril.min.js' : 'Content/js/mithril.js')
        .pipe(gulp.dest(build + '/Framework'));
});

///////////////////////////////////////////////////////////////
// Runnable Tasks

gulp.task('Framework', gulp.series('mithril'));

createTaskGroup('Portal');
