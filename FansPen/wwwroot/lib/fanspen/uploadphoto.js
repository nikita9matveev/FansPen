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
}
