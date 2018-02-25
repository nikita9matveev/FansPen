var id = window.location.search.replace('?id=', '');
var category = $('#categoryProfile');
var sort = $('#popOrNewProfile');
var resultDivProfile = $('#resultDivProfile');
var indexCategory = 0;
var indexSort = 0;
var package = 0;
var getFanficAjax = true;
var endOfList = false;


category.change(categoryChanged);
sort.change(sortChanged);


function categoryChanged() {
    indexCategory = $(this)[0].selectedIndex;
    package = 0;
    endOfList = false;
    resultDivProfile.empty();
    GetUserFanfic();
}

function sortChanged() {
    indexSort = $(this)[0].selectedIndex;
    package = 0;
    endOfList = false;
    resultDivProfile.empty();
    GetUserFanfic();
}

$(document).scroll(function () {
    if ($(window).scrollTop() >= $(document).height() - $(window).height() - 1
        && $('.nav-tabs').children().eq(1).hasClass('active') && !endOfList) {
        GetNext();
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
                category: categoryName,
                sort: indexSort
            },
            beforeSend: function () {
                package += 10;
            },
            success: function (data) {
                console.log(data);
                if (data.fanfics.length == 0) {
                    endOfList = true;
                    if (package == 2) {
                        resultDivProfile.append(
                            '<div class="text-center"> <h3>' + localeText("ListEmpty") + '</h3> </div>'
                        );
                    }
                }
                for (var i = 0; i < data.fanfics.length; i++) {
                    appendFanfic(data.fanfics[i]);
                }
            },
            dataType: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { getFanficAjax = true; });
    }
}

function GetNext() {
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
            url: "/GetNext",
            data: {
                package: package
            },
            beforeSend: function () {
                package += 10;
            },
            success: function (data) {
                console.log(data);
                if (data.fanfics.length == 0) {
                    endOfList = true;
                }
                for (var i = 0; i < data.fanfics.length; i++) {
                    appendFanfic(data.fanfics[i]);
                }
            },
            dataType: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { getFanficAjax = true; });
    }
}

function appendFanfic(fanfic) {
    var rating = Math.round(fanfic.averageRating);
    var tagsStr = '';
    for (var j = 0; j < fanfic.tags.length; j++) {
        tagsStr += ' <a href="/Tag?value=' + fanfic.tags[j].name + '" class="tagButton" role="button">#' + fanfic.tags[j].name + '</a> ';
    }
    resultDivProfile.append(
        '<div class="thumbnail bordered-thumbnail">' +
        '<div class="row autherBlock">' +
        '<div class="col-sm-6" style="padding-top: 10px">' +
        localeText("DateCreation") + ' ' + fanfic.createDate +
        '</div>' +
        '<div class="col-sm-6 starsRating" style="overflow:hidden">' +
        '<form class="rating" title="' + fanfic.averageRating + '" >' +
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
        '<a href="/Fanfic?id=' + fanfic.id + '">' +
        '<div class="imgFanfic">' +
        '<div class="titleFanfic">' +
        '<h3 class="titleFanficH3">' + fanfic.name + '</h3>' +
        '</div>' +
        '<img src="' + fanfic.imgUrl + '">' +
        '</div>' +
        '</a>' +
        '<div class="caption">' +
        '<p class="description-fanfic">' + fanfic.description + '</p>' +
        '<p>' +
        '<a href="/Category?value=' + fanfic.category.name + '" class="categoryButton" role="button">' + localeText(fanfic.category.name) + '</a>' +
        tagsStr +
        '</p>' +
        '</div>' +
        '</div>'
    );
}

GetUserFanfic();