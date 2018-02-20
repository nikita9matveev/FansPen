var cultureLong = document.cookie.split('.AspNetCore.Culture=')[1];
var culture = cultureLong == undefined ? undefined : cultureLong.split(';')[0];
var shotCulture = culture == undefined ? "" : culture.substring(culture.length - 2);

function localeText(word) {
    switch (word) {
        case "Anime": return shotCulture == "en" ? "Anime and Manga" : "Аниме и манга";
        case "Comics": return shotCulture == "en" ? "Comics" : "Комиксы";
        case "Books": return shotCulture == "en" ? "Books" : "Книги";
        case "Games": return shotCulture == "en" ? "Games" : "Игры";
        case "Other": return shotCulture == "en" ? "Other" : "Другое";
        case "Films": return shotCulture == "en" ? "Films" : "Фильмы";
        case "Tags": return shotCulture == "en" ? "Tags" : "Теги";
        case "NotIndicated": return shotCulture == "en" ? "Not indicated" : "Не указано";
    }
}