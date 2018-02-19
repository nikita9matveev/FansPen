cloudinary.setCloudName('fanspen'); 

var avatarPhoto = $('#avatarPhoto');

avatarPhoto.click(uploadPhoto);

function uploadPhoto() {
    cloudinary.openUploadWidget({
        upload_preset: 'uai7rxlq', multiple: 'false',
        cropping: 'server',
        sources: ['local', 'url', 'camera', 'image_search', 'instagram', 'facebook'],
        theme: 'white', cropping_show_back_button: 'true',
        google_api_key: 'AIzaSyBaZ_MC7UlPGKZlfgy1HIwcSMfR7QtjO0Q',
        stylesheet: '#cloudinary-overlay.modal{background-color: rgba(0,0,0,.8);}'
    },
        function (error, result) {
            // this transformations for upload user avatar (and 1 transformation in Home/Index.cshtml)
            var headerAvatar = result[0].url;
            headerAvatar = headerAvatar.substr(0, 47) + "t_avatarHead" + headerAvatar.substr(58, 22) + "png";
            var avatarMain = result[0].url;
            avatarMain = avatarMain.substr(0, 47) + "t_avatarMain" + avatarMain.substr(58, 22) + "png";
            //
            $.ajax({
                url: "/UploadPhoto",    // or "/UploadTopicImg" or "/UploadFanficImg"
                method: "POST",
                data: {
                    avatarUrl: avatarMain    // different arguments
                },
                success: function (data) {
                    console.log(111);

                },
                dataType: "json",
                error: function () {
                    // this for avatar
                    $('#avatarUser').attr('src', avatarMain);
                    $('#headerAvatar').attr('src', headerAvatar);
                    var date = new Date(new Date().getTime() + 60 * 100000000);
                    document.cookie = "avatarUrl=" + headerAvatar + "; path=/; expires=" + date.toUTCString();
                }
            })
        });
}