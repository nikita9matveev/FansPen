var inputSearch = $('#mySearch');

inputSearch.on('keyup',search);

function search(e) {
    if (e.which == 13 && $(this).val().length > 0)
        console.log($(this).val());
}
