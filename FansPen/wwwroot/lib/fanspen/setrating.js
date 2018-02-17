var setRatingAjax = true;

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
                idFanfic: fanficId
            },
            success: function (data) {
                
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { setRatingAjax = true; });
    }
});