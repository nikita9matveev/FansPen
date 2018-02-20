var simplemde1 = new SimpleMDE({
    hideIcons: ["fullscreen", "image", "side-by-side"],
    element: $("#DescriptionFanfic")[0]
});

var simplemde2 = new SimpleMDE({
    hideIcons: ["fullscreen", "image", "side-by-side"],
    element: $("#TopicText1")[0]
});

$('#tags').tagsInput({
    'defaultText': localeText("Tags")
});