var id = window.location.search.replace('?id=', '');
var category = $('#categoryProfile');
var sort = $('#popOrNewProfile');
var resultDivProfile = $('#resultDivProfile');
var indexCategory = 0;
var indexSort = 0;
var package = 0;
var getFanficAjax = true;


if (category != undefined) {
    category.change(categoryChanged);
    sort.change(sortChanged);
}

function categoryChanged() {
    indexCategory = $(this)[0].selectedIndex;
    package = 0;
    resultDivProfile.empty();
    GetUserFanfic();
}

function sortChanged() {
    indexSort = $(this)[0].selectedIndex;
    package = 0;
    resultDivProfile.empty();
    GetUserFanfic();
}

$(document).scroll(function () {
    if ($(window).scrollTop() >= $(document).height() - $(window).height() - 1
        && $('.nav-tabs').children().eq(1).hasClass('active')) {
        GetUserFanfic();
    }
});

function GetUserFanfic() {
    if (getFanficAjax) {
        getFanficAjax = false;
        var categoryName = "All";
        switch (indexCategory) {
            case 1: categoryName = "Anime"; break;
            case 2: categoryName = "Books"; break;
            case 3: categoryName = "Games"; break;
            case 4: categoryName = "Comics"; break;
            case 5: categoryName = "Films"; break;
            case 6: categoryName = "Other"; break;
        }
        $.ajax({
            url: "/GetUserFanfics",
            data: {
                id: id,
                package: package,
                category: categoryName,
                sort: indexSort
            },
            beforeSend: function () {
                package += 10;
            },
            success: function (data) {
                for (var i = 0; i < data.fanfics.length; i++) {
                    var rating = Math.round(data.fanfics[i].averageRating);
                    resultDivProfile.append(
                        '<div class="thumbnail bordered-thumbnail">' +
                            '<div class="row autherBlock">' +
                                '<div class="col-xs-6" style="padding-top: 5px">' +
                                   data.fanfics[i].createDate +
                                '</div>' +
                                '<div class="col-xs-6 starsRating" style="overflow:hidden">' +
                                    '<form class="rating" title="' + data.fanfics[i].averageRating + '" >' +
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
                                            '<input type="radio" name="stars" disabled value="3" ' + ((rating == 3) ? 'checked' : '')  + ' />' +
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
                            '<a href="/Fanfic?id=' + data.fanfics[i].id + '">' +
                                '<div class="imgFanfic">' +
                                    '<div class="titleFanfic">' +
                                        '<h3 class="titleFanficH3">' + data.fanfics[i].name + '</h3>' +
                                    '</div>' +
                                    '<img src="' + data.fanfics[i].imgUrl + '">' +
                                '</div>' +
                            '</a>' +
                        '<div class="caption">' +
                            '<p class="description-fanfic">' + data.fanfics[i].description + '</p>' +
                            '<p>' +
                                '<a href="/Category?value=' + data.fanfics[i].category.name + '" class="categoryButton" role="button">' + localeText(data.fanfics[i].category.name) + '</a>' +
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
        }).always(function (data) { getFanficAjax = true; });
    }
}

GetUserFanfic();