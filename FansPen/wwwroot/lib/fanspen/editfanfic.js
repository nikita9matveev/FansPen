var id = window.location.search.replace('?fanficid=', '');
var und = new upndown();
var count = 0;
var editStart = false;

$.ajax({
    url: "/GetFanficById",
    data: { id: id },
    success: function (data) {
        console.log(data.fanfic);
        for (var i = 0; i < data.fanfic.topics.length; i++) {
            addTopic();
        }
        $('#categoryProfile')[0].selectedIndex = data.fanfic.category.id;
        $('#FanficNameInput').val(data.fanfic.name);
        for (var i = 0; i < data.fanfic.tags.length; i++) {
            $('#tags').addTag(data.fanfic.tags[i].name);
        }
        $('.CoverFanfic img').attr('src', data.fanfic.imgUrl);
        $('.CoverFanfic .load-photo-builder').removeClass('deletable');
        $('.CoverFanfic .load-photo-builder .load-title-div').hide();
        und.convert(data.fanfic.description, function (err, markdown) {
            if (err) {
                console.err(err);
            }
            else {
                DescriptionText.value(markdown);
            }
        });
        var i;
        for (i = 0; i < topicList.children().length; i++) {
            topicList.children().eq(i).children().eq(2).children().eq(0).val(data.fanfic.topics[i].name);
            topicList.children().eq(i).children().eq(0).children().eq(2).text(data.fanfic.topics[i].id);
            if (data.fanfic.topics[i].imgUrl != " ") {
                topicList.children().eq(i).children().eq(3)
                    .children().eq(0)
                    .children().eq(0)
                    .children().eq(1).attr('src', data.fanfic.topics[i].imgUrl);
                topicList.children().eq(i).children().eq(3)
                    .children().eq(0)
                    .children().eq(0).removeClass('deletable');
                topicList.children().eq(i).children().eq(3)
                    .children().eq(0)
                    .children().eq(0)
                    .children().eq(0).hide();
            }
            else {
                topicList.children().eq(i).children().eq(3)
                    .children().eq(0)
                    .children().eq(0)
                    .children().eq(1).attr('src', 'http://res.cloudinary.com/fanspen/image/upload/v1519090685/default1.jpg');
            }
            und.convert(data.fanfic.topics[i].text, function (err, markdown) {
                if (err) {
                    console.err(err);
                }
                else {
                    
                    simplemdeMass[count].value(markdown)
                    count++;
                }
            });
        }
    },
    dataType: 'json',
    error: function () {
        alert("Error while retrieving data!");
    }
}).always(function (data) { });


var editButton = $('#EditTopic');
editButton.click(editFanfic);

function editFanfic() {
    if (!validatorFanfic() && !editStart) {
        editStart = true;
        fanfic = new FanficScriptModel(
            $('#FanficNameInput').val(),
            $('.CoverFanfic img').attr('src'),
            DescriptionText.markdown(DescriptionText.value()),
            $('#categoryProfile')[0].selectedIndex
        );
        fanfic.Id = id;

        var tagsList = $('#tags').val().split(',');
        if (tagsList[0] == '') tagsList = [];
        for (var i = 0; i < tagsList.length; i++) {
            var tag = new TagScriptModel(tagsList[i].toLowerCase().replace(' ', ''));
            fanfic.Tags.push(tag);
        }

        for (var i = 0; i < topicList.children().length; i++) {
            var srcImg = topicList.children().eq(i)
                .children().eq(3).children().eq(0)
                .children().eq(0).children().eq(1)
                .attr('src');
            if (srcImg == undefined)
                srcImg = srcImg = topicList.children().eq(i)
                    .children().eq(3).children().eq(0)
                    .children().eq(0).children().eq(0)
                    .attr('src');
            srcImg = srcImg == 'http://res.cloudinary.com/fanspen/image/upload/v1519090685/default1.jpg' ? " " : srcImg;

            var topic = new TopicScriptModel(
                i + 1,
                topicList.children().eq(i).children().eq(2).children().eq(0).val(),
                srcImg,
                simplemdeMass[i].markdown(simplemdeMass[i].value())
            );
            topic.Id = topicList.children().eq(i).children().eq(0).children().eq(2).text() - 0;
            fanfic.Topics.push(topic);
        }
        fan = JSON.stringify(fanfic);
        console.log(fanfic);
        $.ajax({
            url: "/EditFanfic",
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify(fanfic),
            success: function (data) {
                window.location.replace("/Fanfic?id=" + id);
            },
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { });
    }
}