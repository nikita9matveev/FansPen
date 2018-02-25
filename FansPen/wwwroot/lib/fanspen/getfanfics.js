var fanficList = $('.fanfics');
var path = window.location.pathname.replace('/', '');
var package = 0;
var end = false;
if (path != '')
    package = 10;
var getFanficsAjax = true;

$(document).scroll(function (event) {
    if ($(window).scrollTop() >= $(document).height() - $(window).height() - 1 & getFanficsAjax && !end) {
        getFanficsAjax = false;
        getFanfics();
    }
});

function getFanfics() {
    $.ajax({
        url: "/GetFanfic",
        data: {
            package: package
        },
        beforeSend: function () {
            package += 10;
        },
        success: function (data) {
            console.log(data);
            if (data.length == 0) end = true;
            for (var i = 0; i < data.length; i++) {
                var avatarMini = data[i].applicationUser.avatarUrl.substr(0, 47) + "t_avatarFanfic" + data[i].applicationUser.avatarUrl.substr(59);
                var rating = Math.round(data[i].averageRating);
                var tagsStr = '';
                for (var j = 0; j < data[i].tags.length; j++) {
                    tagsStr += ' <a href="/Tag?value=' + data[i].tags[j].name + '" class="tagButton" role="button">#' + data[i].tags[j].name + '</a> ';
                }

                fanficList.append(
                    '<div class="thumbnail bordered-thumbnail">' +
                        '<div class="row autherBlock">' +
                            '<div class="col-sm-6 col-xs-12">' +
                                '<div class="col-xs-2">' +
                                    '<a href="/Profile?id=' + data[i].applicationUser.id + '"><img class="avatarImg" src="' + avatarMini + '"></a>' +
                                '</div>' +
                                '<div class="col-xs-10">' +
                                    '<div class="col-xs-12">' +
                                        '<a href="/Profile?id=' + data[i].applicationUser.id + '"><b>' + data[i].applicationUser.userName + '</b></a>' +
                                    '</div>' +
                                    '<div class="col-xs-12 small-text">' +
                                        data[i].createDate +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                            '<div class="col-sm-6 col-xs-12 starsRating" style="overflow:hidden">' +
                                '<form class="rating" title="' + data[i].averageRating + '" >' +
                                    '<label>' +
                                        '<input type="radio" name="stars" disabled value="1" ' + ((rating == 1) ? 'checked' : '') + ' />' +
                                        '<span class="icon">★</span>' +
                                    '</label>' +
                                    '<label>' +
                                        '<input type="radio" name="stars" disabled value="2" ' + ((rating == 2) ? 'checked' : '') + ' />' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                    '</label>' +
                                    '<label>' +
                                        '<input type="radio" name="stars" disabled value="3" ' + ((rating == 3) ? 'checked' : '') + ' />' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                    '</label>' +
                                    '<label>' +
                                        '<input type="radio" name="stars" disabled value="4" ' + ((rating == 4) ? 'checked' : '') + ' />' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                    '</label>' +
                                    '<label>' +
                                        '<input type="radio" name="stars" disabled value="5" ' + ((rating == 5) ? 'checked' : '') + ' />' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                        '<span class="icon">★</span>' +
                                    '</label>' +
                                '</form>' +
                            '</div>' +
                        '</div>' +
                        '<a href="/Fanfic?id=' + data[i].id + '">' +
                            '<div class="imgFanfic">' +
                                '<div class="titleFanfic">' +
                                    '<h3 class="titleFanficH3">' + data[i].name + '</h3>' +
                                '</div>' +
                                '<img src="' + data[i].imgUrl + '">' +
                            '</div>' +
                        '</a>' +
                        '<div class="caption">' +
                            '<p class="description-fanfic">' + data[i].description + '</p>' +
                            '<p>' +
                                '<a href="/Category?value=' + data[i].category.name + '" class="categoryButton" role="button">' + localeText(data[i].category.name) + '</a>' +
                                tagsStr +
                            '</p>' +
                        '</div>' +
                    '</div>'
                );
            }
        },
        dataType: 'json',
        error: function () {
            alert("Error while retrieving data!");
        }
    }).always(function (data) { getFanficsAjax = true; });
}