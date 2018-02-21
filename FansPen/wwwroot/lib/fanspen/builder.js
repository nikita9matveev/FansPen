var DescriptionText = new SimpleMDE({
    hideIcons: ["fullscreen", "image", "side-by-side"],
    element: $("#DescriptionFanfic")[0]
});

$('#tags').tagsInput({
    'defaultText': localeText("Tags")
});

var simplemdeMass = [];
var countOfTopic = 0;
var countForEvent = 0;
var topicList = $('#TopicList');
var addTopicButton = $('#AddTopic');
var hideBut = $('.hide-show-builder');
var deleteBut = $('.delete-builder');

addTopicButton.click(addTopic);

hideBut.click(hideShow);
deleteBut.click(deleteTopic);

function hideShow() {
    var $topic = $(this).parent().parent();
    var $this = $(this);
    if ($this.children().eq(0).hasClass('fa-caret-up')) {
        var nameTopic = $topic.children().eq(1).html();
        $topic.children().hide();
        $topic.children().eq(0).show();
        $topic.children().eq(0).children().eq(1).html('<i class="fa fa-caret-down" aria-hidden="true"></i>');
        $topic.append(`<div class="hide-topic-name">${nameTopic}</div>`);
        $topic.addClass('hide-topic');
    }
    else {
        $topic.removeClass('hide-topic');
        $topic.children().show();
        $this.html('<i class="fa fa-caret-up" aria-hidden="true"></i>');
        $topic.children().eq(6).remove();
    }
}

function addTopic() {
    countOfTopic++;
    countForEvent++;
    var chepStr = localeText("Chapter");
    var nameStr = localeText("TopicName");
    var uploadStr = localeText("UploadCover");
    topicList.append(`
        <div class="topic-item col-xs-12">

                    <div class="col-xs-12 margin-block-builder">
                        <div class="head-builder delete-builder${countForEvent}">
                            <div class="hidden">${countOfTopic}</div>
                            <i class="fa fa-times" aria-hidden="true"></i>
                        </div>
                        <div class="head-builder hide-show-builder${countForEvent} hide-show-margin">
                            <i class="fa fa-caret-up" aria-hidden="true"></i>
                        </div>
                    </div>

                    <div class="col-xs-12 margin-block-builder text-center">
                        <h4>${chepStr} ${countOfTopic}</h4>
                    </div>

                    <div class="col-xs-12 margin-block-builder">
                        <input type="text" class="form-control min-border-input" id="TopicName" placeholder="${nameStr}">
                    </div>

                    <div class="col-xs-12 margin-block-builder">
                        <a class="CoverTopic${countOfTopic} uploadTopicCover pointer-build">
                            <div class="load-photo-builder deletable">
                                <div class="load-title-div">
                                    <h4 class="load-title"><i class="fa fa-picture-o" aria-hidden="true"></i> ${uploadStr}</h4>
                                </div>
                                <img src="http://res.cloudinary.com/fanspen/image/upload/v1519090685/default1.jpg" />
                            </div>
                        </a>
                    </div>

                    <div class="col-xs-12 margin-block-builder">
                        <textarea id="TopicText${countForEvent}" placeholder="Topic text" class="form-control min-border-input topic-text-builder" rows="3"></textarea>
                    </div>
                    <div class="col-xs-12">
                        <hr />
                    </div>
                </div>
    `);
    $('.hide-show-builder' + countForEvent).click(hideShow);
    $('.delete-builder' + countForEvent).click(deleteTopic);
    $('.CoverTopic' + countForEvent).click(uploadPhoto);
    var simplemde = new SimpleMDE({
        hideIcons: ["fullscreen", "image", "side-by-side"],
        element: $("#TopicText" + countForEvent)[0]
    });
    simplemdeMass.push(simplemde);
}

function deleteTopic() {
    countOfTopic--;
    var $topic = $(this).parent().parent();
    var $this = $(this);
    var number = $this.children().eq(0).text() - 0;
    $topic.remove();

    for (var i = number - 1; i < topicList.children().length; i++) {
        topicList.children().eq(i).children().eq(0).children().eq(0).children().eq(0).html(i + 1);
        if (topicList.children().eq(i).hasClass('hide-topic')) {
            topicList.children().eq(i).children().eq(6).children().eq(0).text(localeText("Chapter") + " " + (i + 1));
            topicList.children().eq(i).children().eq(1).children().eq(0).text(localeText("Chapter") + " " + (i + 1));
        }
        else
            topicList.children().eq(i).children().eq(1).children().eq(0).text(localeText("Chapter") + " " + (i + 1));

        //topicList.children().eq(i).children().eq(0).children().eq(0).removeClass();
        //topicList.children().eq(i).children().eq(0).children().eq(0).addClass('head-builder');
        //topicList.children().eq(i).children().eq(0).children().eq(0).addClass('delete-builder' + (i + 1));
        //$('.delete-builder' + (i + 1)).click(deleteTopic);

        //topicList.children().eq(i).children().eq(0).children().eq(1).removeClass();
        //topicList.children().eq(i).children().eq(0).children().eq(1).addClass('head-builder');
        //topicList.children().eq(i).children().eq(0).children().eq(1).addClass('hide-show-margin');
        //topicList.children().eq(i).children().eq(0).children().eq(1).addClass('hide-show-builder' + (i + 1));
        //$('.hide-show-builder' + (i + 1)).click(hideShow);

        simplemdeMass[i] = simplemdeMass[i + 1];
    }
    simplemdeMass.pop();
}