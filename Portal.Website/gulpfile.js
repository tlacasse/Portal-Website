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
    string: ['env', 'what'],
    default: {
        env: 'dev',
        what: '',
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
    var isShared = (name === 'Shared');

    gulp.task(name + '_clean', function () {
        return del([
            build + '/styles/' + name + '/**/*.css',
            build + '/clientapp/' + name + '/**/*.js',
        ]);
    });

    gulp.task(name + '_clientapp', function () {
        return gulp.src(['clientapp/' + name + '/**/*.js'])
            .pipe(sort({ comparator: sortComparator }))
            .pipe(isRelease() ? concat(name.toLowerCase() + '.js') : noop())
            .pipe(isRelease() ? minify({ noSource: true }) : noop())
            .pipe(rename(fileAppend))
            .pipe(gulp.dest(build + '/clientapp' + (isRelease() ? '' : ('/' + name))));
    });

    gulp.task(name + '_sass', function () {
        return gulp.src('Content/sass/' + name + '/**/*.scss')
            .pipe(sass())
            .pipe(isRelease() ? concat(name.toLowerCase() + '.css') : noop())
            .pipe(isRelease() ? cleanCSS() : noop())
            .pipe(isRelease() ? rename(addMinExt) : noop())
            .pipe(rename(fileAppend))
            .pipe(gulp.dest(build + '/styles' + (isRelease() ? '' : ('/' + name))));
    });

    gulp.task(name + '_inject', function () {
        var jssrc = [];
        var csssrc = [];
        if (isRelease()) {
            jssrc.push(build + '/clientapp/' + name.toLowerCase() + '-min' + dateAppend + '.js');
            jssrc.push(build + '/clientapp/shared-min' + dateAppend + '.js');
            csssrc.push(build + '/styles/' + name.toLowerCase() + '-min' + dateAppend + '.css')
            csssrc.push(build + '/styles/shared-min' + dateAppend + '.css')
        } else {
            jssrc.push(build + '/clientapp/' + name + '/**/*.js');
            jssrc.push(build + '/clientapp/Shared/**/*.js');
            csssrc.push(build + '/styles/' + name + '/**/*.css')
            csssrc.push(build + '/styles/Shared/**/*.css')
        }

        return gulp.src(build + '/views/app/' + name + '.cshtml')
            .pipe(inject(
                gulp.src(build + '/framework/*.js', { read: false }),
                { relative: true, name: 'framework' }
            ))
            .pipe(inject(
                gulp.src(jssrc).pipe(sort({ comparator: sortComparator })),
                { relative: true, name: 'app' })
            )
            .pipe(inject(
                gulp.src(csssrc, { read: false }),
                { relative: true })
            )
            .pipe(gulp.dest(build + '/views/app'))
    });

    if (isShared) {
        gulp.task(name, gulp.series(
            name + '_clean',
            name + '_clientapp',
            name + '_sass'
        ));
    } else {
        gulp.task(name, gulp.series(
            name + '_clean',
            name + '_clientapp',
            name + '_sass',
            name + '_inject'
        ));
    }
}

gulp.task('mithril', function () {
    return gulp.src(isRelease() ? 'content/js/mithril.min.js' : 'content/js/mithril.js')
        .pipe(gulp.dest(build + '/framework'));
});

///////////////////////////////////////////////////////////////
// Runnable Tasks

gulp.task('Framework', gulp.series('mithril'));

createTaskGroup('Shared');
createTaskGroup('Portal');

gulp.task('build', gulp.series(options.what.split(',')));
