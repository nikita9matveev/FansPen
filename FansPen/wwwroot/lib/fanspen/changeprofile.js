var idUser = window.location.search.replace('?id=', '');
var notIndicated = shotCulture == "en" ? "Not indicated" : "Не указано";

var firstAjax = true;
var secondAjax = true;
var sexAjax = true;
var aboutAjax = true;

$('#FirstName').on('dblclick', selectFirstName);
$('#FirstName').on('touchend', selectFirstName);

$('#SecondName').on('dblclick', selectSecondName);
$('#SecondName').on('touchend', selectSecondName);

$('#Sex').on('dblclick', selectSex);
$('#Sex').on('touchend', selectSex);

$('#AboutMe').on('dblclick', selectAboutMe);
$('#AboutMe').on('touchend', selectAboutMe);

$(document.body).click(function (e) {
    if ($('#FirstNameInput').has(this).length === 0
        && $('#FirstNameInput').length
        && !$('#FirstNameInput').is(e.target))
    {
        setFirstName($('#FirstNameInput').val());
    }
    if ($('#SecondNameInput').has(this).length === 0
        && $('#SecondNameInput').length
        && !$('#SecondNameInput').is(e.target))
    {
        setSecondName($('#SecondNameInput').val());
    }
    if ($('#SexSelect').has(this).length === 0
        && $('#SexSelect').length
        && !$('#SexSelect').is(e.target))
    {
        setSex($('#SexSelect')[0].selectedIndex, $('#SexSelect').val());
    }
    if ($('#aboutMeTextArea').has(this).length === 0
        && $('#aboutMeTextArea').length
        && !$('#aboutMeTextArea').is(e.target))
    {
        setAboutMe($('#aboutMeTextArea').val());
    }
});

$(document.body).keydown(function (e) {
    if (e.which == 13) {
        if ($('#FirstNameInput').length) {
            setFirstName($('#FirstNameInput').val());
        }
        if ($('#SecondNameInput').length) {
            setSecondName($('#SecondNameInput').val());
        }
        if ($('#SexSelect').length) {
            setSex($('#SexSelect')[0].selectedIndex, $('#SexSelect').val());
        }
        if ($('#aboutMeTextArea').length) {
            setAboutMe($('#aboutMeTextArea').val());
        }
    }
});

function selectFirstName(e) {
    var text = $(this).text().trim() == "Not indicated" || $(this).text().trim() == "Не указано" ? "" : $(this).text().trim();
    $(this).parent().html(`<input class="form-control min-border-input" id="FirstNameInput" type="text" value="${text}">`);
    $('#FirstNameInput').focus();
}

function selectSecondName(e) {
    var text = $(this).text().trim() == "Not indicated" || $(this).text().trim() == "Не указано" ? "" : $(this).text().trim();
    $(this).parent().html(`<input class="form-control min-border-input" id="SecondNameInput" type="text" value="${text}">`);
    $('#SecondNameInput').focus();
}

function selectSex(e) {
    var lang, currentVal;
    switch ($(this).text().trim()) {
        case 'Not indicated': lang = true; currentVal = 0; break;
        case 'Male': lang = true; currentVal = 1; break;
        case 'Female': lang = true; currentVal = 2; break;
        case 'Не указано': lang = false; currentVal = 0; break;
        case 'Мужской': lang = false; currentVal = 1; break;
        case 'Женский': lang = false; currentVal = 2; break;
        default: return;
    }
    $(this).parent().html(
        '<select class="form-control min-border-input" id="SexSelect">' +
        '<option ' + (0 == currentVal ? 'selected' : '') + '>' + (lang ? 'Not indicated' : 'Не указано') + '</option>' +
        '<option ' + (1 == currentVal ? 'selected' : '') + '>' + (lang ? 'Male' : 'Мужской') + '</option>' +
        '<option ' + (2 == currentVal ? 'selected' : '') + '>' + (lang ? 'Female' : 'Женский') + '</option>' +
        '</select>');
    $('#SexSelect').focus();
}

function selectAboutMe(e) {
    var text = $(this).text().trim() == "Not indicated" || $(this).text().trim() == "Не указано" ? "" : $(this).text().trim();
    $(this).parent().html(`<textarea id="aboutMeTextArea" class="form-control min-border-input" rows="3" maxlength="1000">${text}</textarea>`);
    $('#aboutMeTextArea').focus();
}



function setFirstName(value) {
    if (firstAjax) {
        firstAjax = false;
        $.ajax({
            url: "/SetFirstName",
            method: 'POST',
            data: {
                id: idUser,
                value: value
            },
            success: function () {
                var text = value == "" ? localeText("NotIndicated") : value;
                $('#FirstNameInput').parent().html(`<h5 id="FirstName" class="changeble">${text}</h5>`);
                $('#FirstName').on('dblclick', selectFirstName);
                $('#FirstName').on('touchend', selectFirstName);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { firstAjax = true; });
    }
}

function setSecondName(value) {
    if (secondAjax) {
        secondAjax = false;
        $.ajax({
            url: "/SetSecondName",
            method: 'POST',
            data: {
                id: idUser,
                value: value
            },
            success: function () {
                var text = value == "" ? localeText("NotIndicated") : value;
                $('#SecondNameInput').parent().html(`<h5 id="SecondName" class="changeble">${text}</h5>`);
                $('#SecondName').on('dblclick', selectSecondName);
                $('#SecondName').on('touchend', selectSecondName);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { secondAjax = true; });
    }
}

function setSex(number , value) {
    var sex;
    switch(number){
        case 0: sex = "NotIndicated"; break;
        case 1: sex = "Male"; break;
        case 2: sex = "Female"; break;
    }
    if (sexAjax) {
        sexAjax = false;
        $.ajax({
            url: "/SetSex",
            method: 'POST',
            data: {
                id: idUser,
                value: sex
            },
            success: function () {
                $('#SexSelect').parent().html(`<h5 id="Sex" class="changeble">${value}</h5>`);
                $('#Sex').on('dblclick', selectSex);
                $('#Sex').on('touchend', selectSex);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { sexAjax = true; });
    }
}

function setAboutMe(value) {
    if (aboutAjax) {
        aboutAjax = false;
        $.ajax({
            url: "/SetAboutMe",
            method: 'POST',
            data: {
                id: idUser,
                value: value
            },
            success: function () {
                var text = value == "" ? localeText("NotIndicated") : value;
                $('#aboutMeTextArea').parent().html(`<h5 id="AboutMe" class="changeble">${text}</h5>`);
                $('#AboutMe').on('dblclick', selectAboutMe);
                $('#AboutMe').on('touchend', selectAboutMe);
            },
            datatype: 'json',
            error: function () {
                alert("Error while retrieving data!");
            }
        }).always(function (data) { aboutAjax = true; });
    }
}



