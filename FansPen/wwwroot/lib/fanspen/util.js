$(function () {
    var chat = $.connection.comments;

    chat.client.addMessage = function (data) {
        var text = data.Text.replace('\n', '<br />');
        commentDiv.prepend(`<div class="col-xs-12 comment-fanfic">
                    <div class="col-sm-1 col-xs-2 avatar-comment-fanfic">
                        <a href="/Profile?id=${data.User.Id}"><img src="${data.User.AvatarUrl}" /></a>
                    </div>
                    <div class="col-sm-10 col-xs-8 name-comment-fanfic">
                        <div class="col-xs-12">
                            <a href="/Profile?id=${data.User.Id}"><b>${data.User.UserName}</b></a>
                        </div>
                        <div class="col-xs-12 small-text">
                            ${data.DataCreate}
                        </div>
                    </div><div class="col-xs-12 text-comment">
                        <hr class="hr-comment hr-profile">
                        ${text}
                    </div>
                    <div class="col-xs-12 text-right">
                        <div class="like${data.Id} like-button" onselectstart="return false" onmousedown="return false"><div class="hidden">${data.Id} false</div><i class="fa fa-heart-o" aria-hidden="true"></i><b> 0</b></div>
                    </div>
                </div>`);
        $('.like' + data.Id).click(setLike);
        countComments.text('(' + ((countComments.text().replace('(', '').replace(')', '') - 0) + 1) + ')');

        audio = new Audio(); // Создаём новый элемент Audio
        audio.src = '/sounds/vk.mp3'; // Указываем путь к звуку "клика"
        audio.autoplay = true; // Автоматически запускаем
    };

    chat.client.deleteMessage = function (id, count) {
        if ($('.like' + id).length != 0) {
            package--;
            $('.like' + id).eq(0).parent().parent().remove();
            countComments.text('(' + count + ')');
        }
    }

    $.connection.hub.start().done(function () {
        console.log("hub start");
        var date = new Date(new Date().getTime() + 60 * 10000000000);
        document.cookie = "idClient=" + $.connection.hub.id + "; path=/; expires=" + date.toUTCString();
    });
});