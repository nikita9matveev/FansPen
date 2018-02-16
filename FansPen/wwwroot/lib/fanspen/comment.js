var sendButton = $('#sendButton');
var commentText = $('#commentText');
var commentDiv = $('.comment-div');
var sendButton = $('#sendButton');
var countComments = $('#countComments');
var fanficId = $('#FanficId').text();

var package = 0;

var getCommentAjax = true;
var setLikeAjax = true;
var sendCommentAjax = true;
var getNewCommentAjax = true;
var deleteCommentAjax = true;
var getNewCountAjax = true;

var interval = window.setInterval(getNewComments, 2000);

commentText.on("change paste keyup", function () {
    if ($(this).val().replace(/\s/g, '').length != 0) {
        sendButton.attr('disabled', false);
    }
    else {
        sendButton.attr('disabled', true);
    }
});

function getCaret(el) {
    if (el.selectionStart) {
        return el.selectionStart;
    } else if (document.selection) {
        el.focus();
        var r = document.selection.createRange();
        if (r == null) {
            return 0;
        }
        var re = el.createTextRange(), rc = re.duplicate();
        re.moveToBookmark(r.getBookmark());
        rc.setEndPoint('EndToStart', re);
        return rc.text.length;
    }
    return 0;
}

commentText.keyup(function (event) {
    if (event.keyCode == 13) {
        var content = this.value;
        var caret = getCaret(this);
        if (event.shiftKey) {
            this.value = content.substring(0, caret - 1) + "\n" + content.substring(caret, content.length);
            event.stopPropagation();
        }
        else {
            if (!sendButton.attr('disabled') && sendCommentAjax) {
                sendCommentAjax = false;
                sendComment();
            }
        }
    }
});

sendButton.click(function () {
    if (!$(this).attr('disabled') && sendCommentAjax) {
        sendCommentAjax = false;
        sendComment();
    }
});

$(document).scroll(function (event) {
    if (window.$(window).scrollTop() >= window.$(document).height() - window.$(window).height() - 1 && getCommentAjax) {
        getCommentAjax = false;
        getComments();
    }
});



function getComments() {
    $.ajax({
        url: "/GetComments",
        data: {
            id: fanficId,
            package: package
        },
        beforeSend: function () {
            package += 10;
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var isLike = data[i].isLiked ? "fas" : "far";
                var text = data[i].text.replace('\n', '<br />');
                var your = data[i].isYour ? `<div class="col-sm-1 col-xs-2 del-comment-button del${data[i].id}"><div class="hidden">${data[i].id}</div><i class="fas fa-times"></i></div>` : ``;
                commentDiv.append(`<div class="col-xs-12 comment-fanfic">
                    <div class="col-sm-1 col-xs-2 avatar-comment-fanfic">
                        <img src="${data[i].user.avatarUrl}" />
                    </div>
                    <div class="col-sm-10 col-xs-8 name-comment-fanfic">
                        <div class="col-xs-12">
                            <b>${data[i].user.userName}</b>
                        </div>
                        <div class="col-xs-12 small-text">
                            ${data[i].dataCreate}
                        </div>
                    </div>` + your +
                    `<div class="col-xs-12 text-comment">
                        <hr class="hr-comment">
                        ${text}
                    </div>
                    <div class="col-xs-12 text-right">
                        <div class="like${data[i].id} like-button" onselectstart="return false" onmousedown="return false"><div class="hidden">${data[i].id} ${data[i].isLiked}</div><i class="${isLike} fa-heart"></i><b> ${data[i].usersLiked.length}</b></div>
                    </div>
                </div>`);
                $('.like' + data[i].id).click(setLike);
                $('.del' + data[i].id).click(deleteComment);
            }
        },
        dataType: 'json',
        error: function () {
            alert("Error while retrieving data!");
        }
    }).always(function (data) { getCommentAjax = true; });
}

function setLike() {
    var idComment = $(this).eq(0).children().eq(0).text().split(' ')[0] - 0;
    var isLiked = $(this).eq(0).children().eq(0).text().split(' ')[1] == "true";
    var $this = $(this);
    if (setLikeAjax) {
        setLikeAjax = false;
        $.ajax({
            url: "/SetLike",
            method: 'POST',
            data: {
                id: idComment,
                isLike: isLiked
            },
            success: function (data) {
                if (data.count != -1) {
                    if (isLiked) {
                        $this.empty();
                        $this.append(`<div class="hidden">${idComment} false</div><i class="far fa-heart"></i><b> ${data.count}</b>`);
                    }
                    else {
                        $this.empty();
                        $this.append(`<div class="hidden">${idComment} true</div><i class="fas fa-heart"></i><b> ${data.count}</b>`);
                    }
                }
            },
            dataType: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { setLikeAjax = true; });
    }
}

function sendComment() {
    $.ajax({
        url: "/SendComment",
        method: 'POST',
        data: {
            id: fanficId,
            text: commentText.val()
        },
        success: function (data) {
            countComments.empty();
            countComments.append(`(${data.count})`);
            commentText.val("");
            sendButton.attr('disabled', true);
        },
        dataType: 'json',
        error: function () {
            alert("Error while retrieving data!");
        }
    }).always(function (data) { sendCommentAjax = true; });
}

function getNewComments() {
    if (getNewCommentAjax) {
        getNewCommentAjax = false;
        $.ajax({
            url: "/GetNewComments",
            data: {
                id: fanficId
            },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var isLike = data[i].isLiked ? "fas" : "far";
                    var text = data[i].text.replace('\n', '<br />');
                    var your = data[i].isYour ? `<div class="col-sm-1 col-xs-2 del-comment-button del${data[i].id}"><div class="hidden">${data[i].id}</div><i class="fas fa-times"></i></div>` : ``;
                    commentDiv.prepend(`<div class="col-xs-12 comment-fanfic">
                    <div class="col-sm-1 col-xs-2 avatar-comment-fanfic">
                        <img src="${data[i].user.avatarUrl}" />
                    </div>
                    <div class="col-sm-10 col-xs-8 name-comment-fanfic">
                        <div class="col-xs-12">
                            <b>${data[i].user.userName}</b>
                        </div>
                        <div class="col-xs-12 small-text">
                            ${data[i].dataCreate}
                        </div>
                    </div>` + your +
                    `<div class="col-xs-12 text-comment">
                        <hr class="hr-comment">
                        ${text}
                    </div>
                    <div class="col-xs-12 text-right">
                        <div class="like${data[i].id} like-button" onselectstart="return false" onmousedown="return false"><div class="hidden">${data[i].id} ${data[i].isLiked}</div><i class="${isLike} fa-heart"></i><b> ${data[i].usersLiked.length}</b></div>
                    </div>
                </div>`);
                    $('.like' + data[i].id).click(setLike);
                    $('.del' + data[i].id).click(deleteComment);
                }
            },
            dataType: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { getNewCommentAjax = true; });
    }
    if (getNewCountAjax) {
        getNewCountAjax = false;
        $.ajax({
            url: "/GetNewCount",
            data: {
                idFanfic: fanficId
            },
            success: function (data) {
                countComments.empty();
                countComments.append(`(${data.count})`);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { getNewCountAjax = true; });
    }
}

function deleteComment() {
    var commentId = $(this).eq(0).children().eq(0).text();
    var comment = $(this).eq(0).parent();
    if (deleteCommentAjax) {
        deleteCommentAjax = false;
        $.ajax({
            url: "/DeleteComment",
            method: 'POST',
            data: {
                idComment: commentId,
                idFanfic: fanficId
            },
            success: function (data) {
                package--;
                comment.remove();
                countComments.empty();
                countComments.append(`(${data.count})`);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { deleteCommentAjax = true; });
    }
}