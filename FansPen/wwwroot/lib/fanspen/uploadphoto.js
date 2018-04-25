cloudinary.setCloudName('fanspen');

var avatarPhoto = $('#avatarPhoto');
var coverFanfic = $('.CoverFanfic');

avatarPhoto.click(uploadPhoto);
coverFanfic.click(uploadPhoto);

function uploadPhoto() {
    var $this = $(this);
    cloudinary.openUploadWidget({
        upload_preset: 'uai7rxlq', multiple: 'false',
        cropping: 'server',
        sources: ['local', 'url', 'camera', 'image_search', 'instagram', 'facebook'],
        theme: 'white', cropping_show_back_button: 'true',
        google_api_key: 'AIzaSyBaZ_MC7UlPGKZlfgy1HIwcSMfR7QtjO0Q',
        stylesheet: '#cloudinary-overlay.modal{background-color: rgba(0,0,0,.8);}'
    },
        function (error, result) {
            if ($this.attr('id') == 'avatarPhoto' && result != undefined) {
                var headerAvatar = result[0].url;
                headerAvatar = headerAvatar.substr(0, 47) + "t_avatarHead" + headerAvatar.substr(58, 22) + "png";
                var avatarMain = result[0].url;
                avatarMain = avatarMain.substr(0, 47) + "t_avatarMain" + avatarMain.substr(58, 22) + "png";
        $.ajax({
                    url: "/UploadPhoto",
                    method: "POST",
                    data: {
                        id: idUser,
                        avatarUrl: avatarMain
                    },
                    success: function (data) {
                        console.log("Ny zdarova");
                    },
                    dataType: "json",
                    error: function () {
                        $('#avatarUser').attr('src', avatarMain);
                        $.ajax({
                            url: "/GetIdCurrentUser",
                            success: function (data) {
                                if (data.id == idUser) {
                                    $('#headerAvatar').attr('src', headerAvatar);
                                    var date = new Date(new Date().getTime() + 60 * 100000000);
                                    document.cookie = "avatarUrl=" + headerAvatar + "; path=/; expires=" + date.toUTCString();
                                }
                            },
                            dataType: 'json',
                            error: function () {
                                alert("Error while retrieving data!");
                            }
                        });
                    }
                });
            }
            else {
                $this.children().children().eq(1).attr('src', result[0].url.substr(0, 47) + "t_FanficPDF" + result[0].url.substr(58, 22) + "jpg");
                $this.children().eq(0).removeClass('deletable');
                $this.children().children().eq(0).hide();
            }

        });
}
