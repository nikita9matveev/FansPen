var setRatingAjax = true;
var url = new URL(window.location.href);
var topicId = url.searchParams.get("id");

$(":radio").change(function () {
    console.log(this.value);
    var rating = this.value;
    if (setRatingAjax) {
        setRatingAjax = false;
        $.ajax({
            url: "/SetRating",
            method: 'POST',
            data: {
                rating: rating,
                idFanfic: fanficId,
                idTopic: topicId
            },
            success: function (data) {
                $('#averageTopic').text('(' + data.averageTopicRating + ')');
                $('#countRating').text(data.countRatings);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { setRatingAjax = true; });
    }
});