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
        case "Chapter": return shotCulture == "en" ? "Chapter." : "Глава.";
        case "TopicName": return shotCulture == "en" ? "Name" : "Название";
        case "UploadCover": return shotCulture == "en" ? "Upload cover" : "Загрузить обложку";
        case "FanficNameEmpty": return shotCulture == "en" ? "Fanfic name not indicated" : "Не указано название фанфика";
        case "CategoryEmpty": return shotCulture == "en" ? "Category not indicated" : "Не указана категория";
        case "DescriptionEmpty": return shotCulture == "en" ? "Description not indicated" : "Не указано описание";
        case "FanficCoverEmpty": return shotCulture == "en" ? "Fanfic cover not loaded" : "Не загружена обложка альбома";
        case "NoTopic": return shotCulture == "en" ? "Least one chapter needed" : "Необходима хотя бы одна глава";
        case "TopicNameEmpty": return shotCulture == "en" ? "Chapter name not indicated" : "Не указано название главы";
        case "TopicTextEmpty": return shotCulture == "en" ? "Chapter text is empty" : "Не введен текст главы";
        case "ReadMode": return shotCulture == "en" ? "Reading mode" : "Режим чтения";
        case "DefaultMode": return shotCulture == "en" ? "Default mode" : "Стандартный режим";
        case "DateCreation": return shotCulture == "en" ? "Date of creation:" : "Дата создания:";
        case "ListEmpty": return shotCulture == "en" ? "The list is empty" : "Список пуст";
    }
}