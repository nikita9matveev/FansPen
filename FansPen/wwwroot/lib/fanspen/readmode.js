var navbar = $('.navbar-fixed-top');
var readButton = $('.fixed-read-button');

var prevButton = $('.prev-button');
var nextButton = $('.next-button');

var leftButton = $('.fixed-left-button');
var rightButton = $('.fixed-right-button');
var topButton = $('.fixed-top-button');

var progressRead = $('.progress-read');

var commentsBlock = $('#commentsBlock');
var fanficBlock = $('.fanficBlock');

readButton.click(readModeClick);

$(document).scroll(function () {
    progressRead.html(Math.round(($(window).scrollTop() + $(window).height()) * 100 / $(document).height()) + '%');
});

function readModeClick() {
    navbar.css('transition', 'all 1s');
    $(document.body).css('transition', 'all .74s');
    fanficBlock.css('transition', 'all 1s ease');
    readButton.css('transition', 'right 1s');
    leftButton.css('transition', 'left 1s');
    rightButton.css('transition', 'right 1s');
    topButton.css('transition', 'left 1s');
    progressRead.css('transition', 'all 1s');
    readMode();
}

function readMode() {
    if ($(document.body).hasClass('no-body-padding')) {
        $(document.body).removeClass('no-body-padding');
        navbar.removeClass('no-navbar');
        commentsBlock.removeClass('no-comments');
        if (prevButton.attr('href') != undefined)
            prevButton.attr('href', prevButton.attr('href').split('&')[0]);
        if (nextButton.attr('href') != undefined)
            nextButton.attr('href', nextButton.attr('href').split('&')[0]);
        readButton.html('<i class="fa fa-book" aria-hidden="true"></i>');
        fanficBlock.removeClass('col-sm-12 col-sm-offset-0 col-xs-10 col-xs-offset-1');
        fanficBlock.addClass('col-xs-10 col-xs-offset-1');

        leftButton.removeClass('no-left');
        topButton.removeClass('no-left');
        rightButton.removeClass('no-right');
        readButton.removeClass('no-right');
        readButton.attr('title', localeText('ReadMode'));
        progressRead.addClass('far-right');
        fanficBlock.css('min-height', '93vh');
    }
    else {
        $(document.body).addClass('no-body-padding');
        navbar.addClass('no-navbar');
        commentsBlock.addClass('no-comments');
        if (prevButton.attr('href') != undefined)
            if(prevButton.attr('href').split('&').length == 1)
                prevButton.attr('href', prevButton.attr('href') + '&mode=readmode');
        if (nextButton.attr('href') != undefined)
            if(nextButton.attr('href').split('&').length == 1)
                nextButton.attr('href', nextButton.attr('href') + '&mode=readmode');
        readButton.html('<i class="fa fa-times-circle" aria-hidden="true"></i>');
        fanficBlock.removeClass('col-xs-10 col-xs-offset-1');
        fanficBlock.addClass('col-sm-12 col-sm-offset-0 col-xs-10 col-xs-offset-1');

        leftButton.addClass('no-left');
        topButton.addClass('no-left');
        rightButton.addClass('no-right');
        readButton.addClass('no-right');
        readButton.attr('title', localeText('DefaultMode'));
        progressRead.removeClass('far-right');
        fanficBlock.css('min-height', '100vh');
    }
}

if (window.location.search.split('&mode=').length > 1) {
    navbar.css('transition', '0s');
    $(document.body).css('transition', '0s');
    progressRead.css('transition', '0s');
    readMode();
}

